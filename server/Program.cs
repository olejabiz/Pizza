using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    class Program
    {
        static void Main(string[] args)
        {
            // Устанавливаем для сокета локальную конечную точку
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 11000);

            // Создаем сокет Tcp/Ip
            Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Назначаем сокет локальной конечной точке и слушаем входящие сокеты
            try
            {
                sListener.Bind(ipEndPoint);
                sListener.Listen(10);

                // Начинаем слушать соединения
                while (true)
                {
                    Console.WriteLine("Ожидаем соединение через порт " + ipEndPoint);
                    // Программа приостанавливается, ожидая входящее соединение
                    Socket handler = sListener.Accept();
                    string data = null;

                    // Мы дождались клиента, пытающегося с нами соединиться

                    byte[] bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);

                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);

                    // Показываем данные на консоли
                    Console.Write("Полученный текст: " + data + "\n\n");

                    void Pepperoni()
                    {
                        Console.WriteLine("Пепперони ");
                        string reply = "Вес: 560г.\n Состав: Грибы шампиньоны, Моцарелла, Пепперони, Томатный пицца - соус.\n Цена: 600 руб.";
                        byte[] msg = Encoding.UTF8.GetBytes(reply);
                        handler.Send(msg);
                    }
                    void Fourseasons()
                    {
                        Console.WriteLine("Четыре сезона ");
                        string reply = "Вес: 750г.\n Состав: Шампиньоны, Курица, Моцарелла, Соус барбекю, Томаты.\n Цена: 700 руб. ";
                        byte[] msg = Encoding.UTF8.GetBytes(reply);
                        handler.Send(msg);
                    }
                    void Fourcheeses()
                    {
                        Console.WriteLine("Четыре сыра ");
                        string reply = "Вес: 555г.\n Состав: Моцарелла, Сливочный пицца-соус, Сыр Дор Блю, Сыр Чеддер.\n Цена: 500 руб. ";
                        byte[] msg = Encoding.UTF8.GetBytes(reply);
                        handler.Send(msg);
                    }
                    void Margarita()
                    {
                        Console.WriteLine("Маргарита ");
                        string reply = "Вес: 540г.\n Состав: Моцарелла, Помидоры, Томатный пицца-соус.\n Цена: 550 руб. ";
                        byte[] msg = Encoding.UTF8.GetBytes(reply);
                        handler.Send(msg);
                    }
                    void Gavayskaya()
                    {
                        Console.WriteLine("Гавайская ");
                        string reply = "Вес:455г.\n Состав: Ананас, Курица, Моцарелла, Соус сливочный.\n Цена: 450 руб. ";
                        byte[] msg = Encoding.UTF8.GetBytes(reply);
                        handler.Send(msg);
                    }


                    switch (data)
                    {
                        case "Пепперони":
                            Pepperoni();
                            break;
                        case "4 сезона":
                            Fourseasons();
                            break;
                        case "4 сыра":
                            Fourcheeses();
                            break;
                        case "Маргарита":
                            Margarita();
                            break;
                        case "Гавайская":
                            Gavayskaya();
                            break;
                    }


                    // Отправляем ответ клиенту\


                    if (data.IndexOf("<TheEnd>") > -1)
                    {
                        Console.WriteLine("Сервер завершил соединение с клиентом.");
                        break;
                    }

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
