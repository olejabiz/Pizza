using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pizzaprakt
{
    public partial class Form1 : Form
    {
        string message = "";
        public Form1()
        {
            InitializeComponent();
            richTextBox1.Text = "Введите название пиццы, информацию о которой вы хотите получить:";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                SendMessageFromSocket(11000);
            }
            catch
            {

            }
        }
        void SendMessageFromSocket(int port)
        {

            // Буфер для входящих данных
            byte[] bytes = new byte[1024];

            // Соединяемся с удаленным устройством

            // Устанавливаем удаленную точку для сокета
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Соединяем сокет с удаленной точкой
            sender.Connect(ipEndPoint);





            this.message = richTextBox2.Text;

            //fm.RTB1.Text = "Сокет соединяется с {0} "+ sender.RemoteEndPoint.ToString();
            byte[] msg = Encoding.UTF8.GetBytes(message);

            // Отправляем данные через сокет
            int bytesSent = sender.Send(msg);

            // Получаем ответ от сервера
            int bytesRec = sender.Receive(bytes);


            richTextBox1.Text = "\nОтвет от сервера: {0}\n\n" + Encoding.UTF8.GetString(bytes, 0, bytesRec);

            //fm.RTB1.Text = "\nОтвет от сервера: {0}\n\n"+ Encoding.UTF8.GetString(bytes, 0, bytesRec);
            richTextBox2.Clear();
            // Используем рекурсию для неоднократного вызова SendMessageFromSocket()
            /*if (message.IndexOf("<TheEnd>") == -1)
            SendMessageFromSocket(port);*/

            // Освобождаем сокет
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();



        }
    }
}