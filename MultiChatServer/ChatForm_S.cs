using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using System.IO;

namespace MultiChatServer
{
    public partial class ChatForm_S : Form
    {

        public Point p;
        Pen pen = new Pen(Color.GreenYellow, 5); //팬색 및 두께
        

        
        string id = null;
        string msg;
        string tts;
       
        string send_x; //보낼 x좌표
        string send_y; //보낼 y좌표
        string received_x; //받은 x좌표
        string received_y; //받은 y좌표
        int re_x; //받은 x좌표
        int re_y; //받은 y좌표

        int shape = 0; //그릴 모양

        delegate void AppendTextDelegate(Control ctrl, string s);
        AppendTextDelegate _textAppender;
        Socket mainSock;
        IPAddress thisAddress;
        Dictionary<String, Socket> connectedClients;
        int clientNum = 0;

        public ChatForm_S()
        {
            InitializeComponent();
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round; //팬 시작 끝 둥글게
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round; // 팬 끝 둥글게

            mainSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            _textAppender = new AppendTextDelegate(AppendText);
            connectedClients = new Dictionary<string, Socket>();
            clientNum = 0; //초기화

        }

        void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

            p.X = e.X;                  //팬 현재위치
            p.Y = e.Y;                  //팬 현재위치
            Console.WriteLine(p.X + "," + p.Y + ",e점" + e.X + "," + e.Y);

        }

        void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                Graphics g = pictureBox1.CreateGraphics();      //객체선언

              
                Rectangle Re = new Rectangle(e.X, e.Y, p.X, p.Y); // 좌표x, 좌표y, 크기

                if (shape == 1) //사각형 그리기
                {                    
                      g.DrawRectangle(pen, Re);
                      g.Dispose();
                }
                else if(shape == 2) //원 그리기
                {
                    g.DrawEllipse(pen, Re);
                    g.Dispose();
                }
                else if(shape == 3) //직선
                {
                    Graphics g2 = pictureBox1.CreateGraphics();
                    g2.DrawLine(pen, p.X, p.Y, e.X, e.Y);
                        p.X = e.X;                                      //위치
                        p.Y = e.Y;
                        g2.Dispose();
                }
                else
                {
                    /*  g.DrawLine(pen, p.X, p.Y, e.X, e.Y); //선그리기
                      p.X = e.X;                                     //위치
                      p.Y = e.Y;
                      g.Dispose();*/


                }
            }
        }
      
        void AppendText(Control ctrl, string s)
        {
            if (ctrl.InvokeRequired) ctrl.Invoke(_textAppender, ctrl, s);
            else
            {
                string source = ctrl.Text;
                ctrl.Text = source + Environment.NewLine + s;
            }
        }

        void OnFormLoaded(object sender, EventArgs e)
        {
            IPHostEntry he = Dns.GetHostEntry(Dns.GetHostName());
            // 처음으로 발견되는 ipv4 주소를 사용한다.
            foreach (IPAddress addr in he.AddressList)
            {
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    AppendText(txtHistory, addr.ToString());
                }
                if (thisAddress == null)
                {
                    thisAddress = IPAddress.Parse("127.0.0.1");
                    txtAddress.Text = thisAddress.ToString(); //서버 주소에 강제로 입력하기
                }

            }
        }
        void BeginStartServer(object sender, EventArgs e)
        {
            int port;
            if (!int.TryParse(txtPort.Text, out port))
            { //문자열을 int port로 변환
                MsgBoxHelper.Error("포트 번호가 잘못 입력되었거나 입력되지 않았습니다.");
                txtPort.Focus();
                txtPort.SelectAll();
                return;
            }

            thisAddress = IPAddress.Parse(txtAddress.Text);
            if (thisAddress == null)
            {// 로컬호스트 주소를 사용한다.                
                thisAddress = IPAddress.Loopback;
                txtAddress.Text = thisAddress.ToString();
            }

            // 서버에서 클라이언트의 연결 요청을 대기하기 위해
            // 소켓을 열어둔다.
            IPEndPoint serverEP = new IPEndPoint(thisAddress, port);

            mainSock.Bind(serverEP);
            mainSock.Listen(10);


            AppendText(txtHistory, string.Format("서버 시작: @{0}", serverEP));
            // 비동기적으로 클라이언트의 연결 요청을 받는다.
            mainSock.BeginAccept(AcceptCallback, null);

        }
        
        void AcceptCallback(IAsyncResult ar)
        {
            // 클라이언트의 연결 요청을 수락한다.
            Socket client = mainSock.EndAccept(ar);

            // 또 다른 클라이언트의 연결을 대기한다.
            mainSock.BeginAccept(AcceptCallback, null);

            AsyncObject obj = new AsyncObject(4096);// 4096 buffer size
            obj.WorkingSocket = client;

            AppendText(txtHistory, string.Format("클라이언트 접속 : @{0}", client.RemoteEndPoint));

            // 클라이언트의 ID 데이터를 받는다.
            client.BeginReceive(obj.Buffer, 0, 4096, 0, DataReceived, obj);
        }
        
        void DataReceived(IAsyncResult ar)
        {
            // BeginReceive에서 추가적으로 넘어온 데이터를 AsyncObject 형식으로 변환한다.
            AsyncObject obj = (AsyncObject)ar.AsyncState;

            
            // 데이터 수신을 끝낸다.
            int received = obj.WorkingSocket.EndReceive(ar);

            // 받은 데이터가 없으면(연결끊어짐) 끝낸다.
            if (received <= 0)
            {
                AppendText(txtHistory, string.Format("{0}님이 방을 나갔습니다.", id));

                if (clientNum > 0)
                {
                    foreach (KeyValuePair<string, Socket> clients in connectedClients)
                    {
                        if (obj.WorkingSocket == clients.Value)
                        {
                            string key = clients.Key;
                            try
                            {
                                connectedClients.Remove(key);
                            }
                            catch
                            {
                             
                            }
                           // break;
                        }
                    }
                }
                obj.WorkingSocket.Disconnect(true);
                obj.WorkingSocket.Close();
                clientNum--;
                //AppendText(txtHistory, string.Format("클라이언트 접속해제완료{0}", clientNum));

                return;
            }

            // 텍스트로 변환한다.
            string text = Encoding.UTF8.GetString(obj.Buffer);
            //AppendText(txtHistory, text); //받은 메시지를 출력함.

            // : 기준으로 짜른다.
            // tokens[0] - 보낸 사람 ID
            // tokens[1] - 보낸 메세지
            string[] tokens = text.Split(':');
            if (tokens[0].Equals("id"))
            {
                clientNum++;
                id = tokens[1];
                AppendText(txtHistory, string.Format("[접속]ID : {0}님이 접속하였습니다", id));

                
                // 연결된 클라이언트 리스트에 추가해준다.
                connectedClients.Add(id, obj.WorkingSocket);

            }
            
            else
            {
                id = tokens[0];
                msg = tokens[1];
                received_x = tokens[2]; //x좌표를 받는다
                received_y = tokens[3]; //y좌표를 받는다

                AppendText(txtHistory, string.Format("[받음]{0}: {1}", id, msg));
                Console.WriteLine(tokens[0]);
                Console.WriteLine(tokens[1]);
                Console.WriteLine(tokens[2]);
                Console.WriteLine(tokens[3]);
                AppendText(txtHistory, string.Format("[받음]{0}: x좌표{1}, y좌표{2}", id, received_x, received_y));
                
            }
            // 텍스트박스에 추가해준다.
            // 비동기식으로 작업하기 때문에 폼의 UI 스레드에서 작업을 해줘야 한다.
            // 따라서 대리자를 통해 처리한다.
            
            // 전체 클라이언트에게 데이터를 보낸다.
            sendAll(obj.WorkingSocket, obj.Buffer);
            
            // 데이터를 받은 후엔 다시 버퍼를 비워주고 같은 방법으로 수신을 대기한다.
            obj.ClearBuffer();
            // 수신 대기
            obj.WorkingSocket.BeginReceive(obj.Buffer, 0, 4096, 0, DataReceived, obj);
        }

        void sendAll(Socket except, byte[] buffer)
        {
            foreach (KeyValuePair<string, Socket> clients in connectedClients)
            {
                Socket socket = clients.Value;
                if (socket != except)
                {
                    try { socket.Send(buffer); }
                    catch
                    {// 오류 발생하면 전송 취소하고 삭제
                        try { socket.Dispose(); } catch { }

                    }
                }
            }
        }
               
        void OnSendData(object sender, EventArgs e)
        {
            // 서버가 대기중인지 확인한다.
            if (!mainSock.IsBound)
            {
                MsgBoxHelper.Warn("서버가 실행되고 있지 않습니다!");
                return;
            }

            // 보낼 텍스트
            tts = txtTTS.Text.Trim();

            send_x = p.X.ToString();
            send_y = p.Y.ToString();

            if (string.IsNullOrEmpty(tts))
            {
                MsgBoxHelper.Warn("텍스트가 입력되지 않았습니다!");
                txtTTS.Focus();
                return;
            }
            /*
             // 문자열을 utf8 형식의 바이트로 변환한다.
             byte[] bDts = Encoding.UTF8.GetBytes("Server" + ':' + tts);
             
             // 연결된 모든 클라이언트에게 전송한다.
             sendAll(null, bDts);
             
             // 전송 완료 후 텍스트박스에 추가하고, 원래의 내용은 지운다.
             AppendText(txtHistory, string.Format("[보냄]server: {0}", tts));
             txtTTS.Clear();
             */

            // 문자열을 utf8 형식의 바이트로 변환한다.
            byte[] x = Encoding.UTF8.GetBytes("Server" + ':' + tts + ':' + send_x + ':' + send_y + ':' );
            txtTTS.Clear();

            // 연결된 모든 클라이언트에게 전송한다.
            sendAll(null, x);

            // 전송 완료 후 텍스트박스에 추가하고, 원래의 내용은 지운다.
            AppendText(txtHistory, string.Format("[보냄]server: {0}, x좌표{1}, y좌표{2}", tts, send_x, send_y));
            Console.WriteLine(send_x);
            Console.WriteLine(send_y);
            txtTTS.Clear();

        }

        private void txtTTs_keyup(object sender, KeyEventArgs e)
        {
            string tts = txtTTS.Text.Trim();
            if (e.KeyCode == Keys.Enter)
            {
                byte[] bDts = Encoding.UTF8.GetBytes("Server" + ':' + tts);


                // 연결된 모든 클라이언트에게 전송한다.
                sendAll(null, bDts);
            }
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                mainSock.Close();
            }
            catch { }
        }

        public byte[] ImageToByteArray(System.Drawing.Image image)
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.ToArray();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

            if (!mainSock.IsBound)
            {
                MsgBoxHelper.Warn("서버가 실행되고 있지 않습니다!");
                return;
            }
            
            send_x = p.X.ToString();
            send_y = p.Y.ToString();
            byte[] x = Encoding.UTF8.GetBytes("Server" + ':' + tts + ':' + send_x + ':' + send_y + ':');
            sendAll(null, x);
            AppendText(txtHistory, string.Format("[보냄]server: {0} x좌표{1}, y좌표{2}", tts, send_x, send_y));
                        
        }
        private void Button1_Click(object sender, EventArgs e) //모두 지우기 버튼
        {
            pictureBox1.Image = null; //모두 지우기
            txtHistory.Text = null; //채팅창 내용 모두 지우기
            received_x = null; //x좌표 받은값 초기화
            received_y = null; //y좌표 받은값 초기화
            send_x = null;
            send_y = null;

        }
        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.Black, 5); //팬색 및 두께
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.Red, 5); //팬색 및 두께
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.Blue, 5); //팬색 및 두께
        }

        private void RadioButton5_CheckedChanged(object sender, EventArgs e)
        {
            pen = new Pen(Color.White, 4); //팬색 및 두께
        }
               
        void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            

        }
        
        private void tblMainLayout_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtHistory_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
           
        }
        private void button2_Click(object sender, EventArgs e)  //사각형 받아오기
        {
            re_x = Convert.ToInt32(received_x); //string x좌표 값을 int형으로 변환
            re_y = Convert.ToInt32(received_y); //string y좌표 값을 int형으로 변환
            Console.WriteLine("받은 좌표1: " + received_x + "," + received_y);
            Console.WriteLine("받은 좌표2: " + re_x + "," + re_y);
            p.X = re_x;
            p.Y = re_y;
            Graphics g = pictureBox1.CreateGraphics();      //객체선언
            Rectangle re = new Rectangle(re_x, re_y, p.X, p.Y); // 좌표x, 좌표y, 크기
            g.DrawRectangle(pen, re); //픽쳐박스에 그리기
            g.Dispose();
        }
        private void button3_Click(object sender, EventArgs e) //원 받아오기
        {
            re_x = Convert.ToInt32(received_x);
            re_y = Convert.ToInt32(received_y);
            Console.WriteLine("받은 좌표1: " + received_x + "," + received_y);
            Console.WriteLine("받은 좌표2: " + re_x + "," + re_y);
            p.X = re_x;
            p.Y = re_y;
            Graphics g = pictureBox1.CreateGraphics();      //객체선언
            Rectangle re = new Rectangle(re_x, re_y, p.X, p.Y); // 좌표x, 좌표y, 크기
            g.DrawEllipse(pen, re); ;
            g.Dispose();
        }

        private void button7_Click(object sender, EventArgs e)  //직선받아오기
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
            g.Dispose(); // 선이 그려지기는 하는데 계속 다른 방향으로 그려져서 프로그램에서는 뺍니다. */
            
        }

        private void button4_Click(object sender, EventArgs e) //사각형 그리기
        {
            shape = 1;

            }

        private void button5_Click(object sender, EventArgs e) //원 그리기
        {
            
            shape = 2;
        }

        private void button6_Click(object sender, EventArgs e) //직선 그리기
        {
            shape = 3;
        }
               
    }
    }

