using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MultiChatClient {
    public partial class ChatForm_C : Form {

        public Point p;
        Pen pen = new Pen(Color.Gold, 5); //팬색 및 두께
       string id = null; //채팅 id
        string msg; 
        string tts;

        string dot_x; //보낼x좌표
        string dot_y; //보낼y좌표
        string received_x; //받은 x좌표
        string received_y; //받은 y좌표
        int re_x; //받는 x좌표
        int re_y; //받은 y좌표
        int shape = 0; //그릴 모양

        delegate void AppendTextDelegate(Control ctrl, string s);
        AppendTextDelegate _textAppender;
        Socket mainSock;
        IPAddress thisAddress;
        string nameID;

        public ChatForm_C() {
            InitializeComponent();
            mainSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            _textAppender = new AppendTextDelegate(AppendText);
        }

        void AppendText(Control ctrl, string s) {
            if (ctrl.InvokeRequired) ctrl.Invoke(_textAppender, ctrl, s);
            else {
                string source = ctrl.Text;
                ctrl.Text = source + Environment.NewLine + s;
            }
        }

        void OnFormLoaded(object sender, EventArgs e) {

            if (thisAddress == null)
            {
                // 로컬호스트 주소를 사용한다.
                thisAddress = IPAddress.Loopback;
                // txtAddress.Text = "127.0.0.1";
                txtAddress.Text = thisAddress.ToString();
            }
            else
            {
                thisAddress = IPAddress.Parse(txtAddress.Text);
            }
        }

        void OnConnectToServer(object sender, EventArgs e) {
            if (mainSock.Connected) {
                MsgBoxHelper.Error("이미 연결되어 있습니다!");
                return;
            }

            int port = 15000;  //고정

            nameID = txtID.Text.Trim(); //ID
            if (string.IsNullOrEmpty(nameID))
            {
                MsgBoxHelper.Warn("ID가 입력되지 않았습니다!");
                txtID.Focus();
                return;
            }

            // 서버에 연결
            try {
                mainSock.Connect(txtAddress.Text, port); }
            catch (Exception ex) {
                MsgBoxHelper.Error("연결에 실패했습니다!\n오류 내용: {0}", MessageBoxButtons.OK, ex.Message);
                return;
            }

            // 서버로 ID 전송
            SendID();

            // 연결 완료, 서버에서 데이터가 올 수 있으므로 수신 대기한다.
            AsyncObject obj = new AsyncObject(4096);
            obj.WorkingSocket = mainSock;
            mainSock.BeginReceive(obj.Buffer, 0, obj.BufferSize, 0, DataReceived, obj);
        }
        void SendID()
        {
            // 문자열을 utf8 형식의 바이트로 변환한다.
            byte[] bDts = Encoding.UTF8.GetBytes("id:" + nameID);

            // 서버에 전송한다.
            mainSock.Send(bDts);

            // 연결 완료되었다는 메세지를 띄워준다.
            AppendText(txtHistory, nameID + "의 id로 서버와 연결되었습니다.");
        }

        void DataReceived(IAsyncResult ar) {
             // BeginReceive에서 추가적으로 넘어온 데이터를 AsyncObject 형식으로 변환한다.
            AsyncObject obj = (AsyncObject)ar.AsyncState;
            string con = "님이 접속하였습니다.";
            // 데이터 수신을 끝낸다.
            try
            {
                int received = obj.WorkingSocket.EndReceive(ar); 


                if (received <= 0)
                {
                    AppendText(txtHistory, string.Format("{0}님이 접속 해제하였습니다.", nameID));

                    obj.WorkingSocket.Disconnect(true);
                    obj.WorkingSocket.Close();
                    this.Close();
                    return;
                }
            }
            catch (ObjectDisposedException e)
            {
                Console.WriteLine("에러: " + e);
            }
            
            // 받은 데이터가 없으면(연결끊어짐) 끝낸다.


            // 텍스트로 변환한다.
            string text = Encoding.UTF8.GetString(obj.Buffer);

            //byte[] 수신

            // : 기준으로 짜른다.
            // tokens[0] - 보낸 사람 ID
            // tokens[1] - 보낸 메세지
            //tokens[2] - 받은 x좌표 값
            //tokens[3] - 받은 y좌표 값
            string[] tokens = text.Split(':');

            if (tokens[0].Equals("id"))
            {// 새로 접속한 클라이언트가가 "id:자신의_ID" 전송함
                id = tokens[1];
                AppendText(txtHistory, string.Format("'[접속] ID : {0}님이 접속하였습니다.'", id));
       
            }
                       
            else { 
            id = tokens[0];
            msg = tokens[1]; //인덱스 배열 범위 설정(서버)
            received_x = tokens[2];
            received_y = tokens[3];
            

                AppendText(txtHistory, string.Format("[받음]{0}: {1}",id, msg));
                Console.WriteLine(msg); 
                Console.WriteLine(tokens[1]);
                Console.WriteLine(tokens[2]);
                Console.WriteLine(tokens[3]);
               
                AppendText(txtHistory, string.Format("[받음]{0}: x좌표{1}, y좌표{2}", id, received_x, received_y));

            }
            
            // 텍스트박스에 추가해준다.
            // 비동기식으로 작업하기 때문에 폼의 UI 스레드에서 작업을 해줘야 한다.
            // 따라서 대리자를 통해 처리한다.
            
            // 클라이언트에선 데이터를 전달해줄 필요가 없으므로 바로 수신 대기한다.
            // 데이터를 받은 후엔 다시 버퍼를 비워주고 같은 방법으로 수신을 대기한다.
            obj.ClearBuffer();

            // 수신 대기
            obj.WorkingSocket.BeginReceive(obj.Buffer, 0, 4096, 0, DataReceived, obj);
            try
            {

            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("예외 오류: " + e.Message);
                Console.WriteLine("예외 라인: " + e.StackTrace);
            }

        }

        void OnSendData(object sender, EventArgs e) {
            // 서버가 대기중인지 확인한다.
            if (!mainSock.IsBound) {
                MsgBoxHelper.Warn("서버가 실행되고 있지 않습니다!");
                return;
            }

            // 보낼 텍스트
            tts = txtTTS.Text.Trim();
                     

            if (string.IsNullOrEmpty(tts)) {
                MsgBoxHelper.Warn("텍스트가 입력되지 않았습니다!");
                txtTTS.Focus();
                return;
            }

            // ID 와 메세지를 담도록 만든다.
            /*
            // 문자열을 utf8 형식의 바이트로 변환한다.
            byte[] bDts = Encoding.UTF8.GetBytes(nameID + ':' + tts);

            // 서버에 전송한다.
            mainSock.Send(bDts);

            // 전송 완료 후 텍스트박스에 추가하고, 원래의 내용은 지운다.
            AppendText(txtHistory, string.Format("[보냄]{0}: {1}", nameID, tts));
            txtTTS.Clear();
        
            */
                        
            dot_x = p.X.ToString(); //x좌표를 int형에서 string 값으로 서버에게 보냄
            dot_y = p.Y.ToString(); //y좌표를 int형에서 string 값으로 서버에게 보냄

            // 문자열을 utf8 형식의 바이트로 변환한다.
            byte[] x = Encoding.UTF8.GetBytes(nameID + ':' + tts + ':' + dot_x + ':' +dot_y);

            // 서버에 전송한다.
            mainSock.Send(x);

            // 전송 완료 후 텍스트박스에 추가하고, 원래의 내용은 지운다.
            AppendText(txtHistory, string.Format("[보냄]{0}: {1}, x좌표{2}, y좌표{3}", nameID, tts, dot_x, dot_y));
            Console.WriteLine(dot_x);
            Console.WriteLine(dot_y);

            txtTTS.Clear();
        }
    
        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (mainSock != null)
                {
                    mainSock.Disconnect(false);
                    mainSock.Close();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("에러: " + ex);
            }
           
        }

        void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            
            p.X = e.X;                  //팬 현재위치
            p.Y = e.Y;                  //팬 현재위치
          
        }

        void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
          

            if (e.Button == MouseButtons.Left)
            {
                Graphics g = pictureBox1.CreateGraphics();      //객체선언

                Rectangle rect = new Rectangle(e.X, e.Y, p.X, p.Y); // 좌표x, 좌표y, 크기

                if( shape == 1) //사각형
                {
                    g.DrawRectangle(pen, rect);
                    g.Dispose();
                }
                else if(shape == 2) //원
                {
                    g.DrawEllipse(pen, rect);
                    g.Dispose();
                }
                else if(shape == 3) //직선
                {
                    g.DrawLine(pen, p.X, p.Y, e.X, e.Y);          //선그리고
                    p.X = e.X;                                      //위치
                    p.Y = e.Y;                                      //위치                      
                    g.Dispose();                                    //객체해제
                }
            }

        }
        private void PictureBox1_Click(object sender, EventArgs e)
        {
            if (!mainSock.IsBound)
            {
                MsgBoxHelper.Warn("서버가 실행되고 있지 않습니다!");
                return;
            }
            
            dot_x = p.X.ToString();
            dot_y = p.Y.ToString();
            byte[] x = Encoding.UTF8.GetBytes(nameID + ':' + tts + ':' + dot_x + ':' + dot_y + ':');
            mainSock.Send(x);
            AppendText(txtHistory, string.Format("[보냄]: {0}, x좌표{1}, y좌표{2}", tts, dot_x, dot_y));

                }
        
        private void Button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null; //모두 지우기
            txtHistory.Text = null; //채팅창 내용 모두 지우기
            received_x = null; //받은 x좌표값 초기화
            received_y = null; //받은 y좌표값 초기화
            

        }
        
        private void button2_Click(object sender, EventArgs e) //사각형 그리기
        {
            shape = 1;
        }

        private void button3_Click(object sender, EventArgs e) //원 그리기
        {
            shape = 2;
        }

        private void button4_Click(object sender, EventArgs e) //직선 그리기
        {
            shape = 3;
        }

        private void TxtAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.Black, 5); //팬색 및 두께
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.Red, 5); //팬색 및 두께
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.Blue, 5); //팬색 및 두께
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.White, 4); //팬색 및 두께
        }

        private void button5_Click(object sender, EventArgs e) //사각형 받아오기
        {
            re_x = Convert.ToInt32(received_x);
            re_y = Convert.ToInt32(received_y);
            Console.WriteLine("받은 좌표1: " + received_x + "," + received_y);
            Console.WriteLine("받은 좌표2: " + re_x + "," + re_y);
            p.X = re_x;
            p.Y = re_y;
            Graphics g = pictureBox1.CreateGraphics();      //객체선언
            Rectangle re = new Rectangle(re_x, re_y, re_x, re_y); // 좌표x, 좌표y, 크기
            g.DrawRectangle(pen, re);
            g.Dispose();

        }

        private void button6_Click(object sender, EventArgs e) //원  받아오기
        {
            re_x = Convert.ToInt32(received_x);
            re_y = Convert.ToInt32(received_y);
            Console.WriteLine("받은 좌표1: " + received_x + "," + received_y);
            Console.WriteLine("받은 좌표2: " + re_x + "," + re_y);
            p.X = re_x;
            p.Y = re_y;
            Graphics g = pictureBox1.CreateGraphics();      //객체선언
            Rectangle re = new Rectangle(re_x, re_y, re_x, re_y); // 좌표x, 좌표y, 크기
            g.DrawEllipse(pen, re);
            g.Dispose();
        }
                private void button7_Click(object sender, EventArgs e) //직선 받아오기
        {
            /*
            re_x = Convert.ToInt32(received_x);
            re_y = Convert.ToInt32(received_y);
            Console.WriteLine("받은 좌표1: " + received_x + "," + received_y);
            Console.WriteLine("받은 좌표2: " + re_x + "," + re_y);

            Graphics g = pictureBox1.CreateGraphics();      //객체선언

            g.DrawLine(pen, re_x, re_x, p.Y, re_y); //선그리기
            p.X = re_x;
            p.Y = re_y;
            g.Dispose();*/
        }
    }
}