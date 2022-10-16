using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Server
{
    internal class TcpConnection
    {
        public string who;
        public DateTime whenconn;
        public Dictionary<string,string> queries_answers;
        public DateTime whendisconn;
        public TcpConnection(IPEndPoint who, DateTime whenconn)
        {
            this.who = $"{who.Address}:{who.Port}";
            this.whenconn = whenconn;
            queries_answers= new Dictionary<string,string>();
        }
        public override string ToString()
        {
            return $"{who} connected at {whenconn.ToShortTimeString()}";
        }
    }

    internal class Program
    {
        static Dictionary<string, double> ExchangeCourse;
        static TcpListener listener=null;
        static IPEndPoint ep = null;
        static List<TcpConnection> clients;
        static int attemptCount;
        static int maximumConnectionsCount;
        static List<TcpClient> currentlyConnected;
        static KeyValuePair<string, string> logpass;
        static void Main(string[] args)
        {
            clients = new List<TcpConnection>();
            currentlyConnected= new List<TcpClient>();
            Task.Run(LoadExchanges);
            string ips;
            string ports;
            IPAddress address;
            int port;
            string attemptss;
            int attempts=0;
            string maximumConnectionss;
            int maximumConnections=0;
            string login;
            string pass;
            do
            {
                Console.Clear();
                Console.WriteLine("Enter IP:");
                ips = Console.ReadLine();
                Console.WriteLine("Enter port:");
                ports = Console.ReadLine();
                Console.WriteLine("Enter count of attempts before cooldown:");
                attemptss = Console.ReadLine();
                Console.WriteLine("Enter count of maximum connections:");
                maximumConnectionss = Console.ReadLine();
                Console.WriteLine("Enter login to access the server:");
                login=Console.ReadLine();
                Console.WriteLine("Enter password to access the server:");
                pass=Console.ReadLine();
            } while (string.IsNullOrEmpty(pass)||string.IsNullOrEmpty(login)||!IPAddress.TryParse(ips, out address)||!int.TryParse(ports,out port)||!int.TryParse(attemptss,out attempts) || !int.TryParse(maximumConnectionss, out maximumConnections)|| maximumConnections < 1||attempts<1);

            Console.Clear();
            ep = new IPEndPoint(address,port);
            listener = new TcpListener(ep);
            attemptCount = attempts;
            maximumConnectionsCount = maximumConnections;
            logpass = new KeyValuePair<string, string>(login,pass);
            Task.Run(StartServer);



            Console.WriteLine("Working...");
            Console.ReadLine();
        }

        private static void LoadExchanges()
        {
            ExchangeCourse = new Dictionary<string, double>();
            XmlDocument xml = new XmlDocument();
            xml.Load("https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange");
            var list=xml.SelectNodes("//currency");
            foreach (XmlNode item in list)
            {
                string key = null;
                double val = 0;
                foreach (XmlNode child in item.ChildNodes)
                {
                    if(child.Name=="cc")
                    {
                        key = child.InnerText;
                    }
                    else if(child.Name=="rate")
                    {
                        val = double.Parse(child.InnerText, CultureInfo.InvariantCulture);
                    }
                }
                if(key!=null&&val!=0)
                    ExchangeCourse.Add(key,val);
            }
        }

        private static void StartServer()
        {
            try
            {
                listener.Start();
                while(true)
                {
                    var client = listener.AcceptTcpClient();
                    var s = client.GetStream();
                    var sb = new StringBuilder();
                    int bytes;
                    byte[] buff = new byte[1024];
                    do
                    {
                        bytes = s.Read(buff, 0, 1024);
                        sb.Append(Encoding.UTF8.GetString(buff, 0, bytes));
                    } while (s.DataAvailable);
                    if (sb.ToString() == "can connect?")
                    {
                        sb.Clear();
                        if (currentlyConnected.Count < maximumConnectionsCount)
                        {
                            buff = Encoding.UTF8.GetBytes("yes");
                            s.Write(buff,0,buff.Length);
                            buff=new byte[1024];
                            do
                            {
                                bytes = s.Read(buff, 0, buff.Length);
                                sb.Append(Encoding.UTF8.GetString(buff,0,bytes));
                            } while (s.DataAvailable);
                            if (sb.ToString().Split('\n')[0]==logpass.Key&& sb.ToString().Split('\n')[1] == logpass.Value)
                            {
                                buff = Encoding.UTF8.GetBytes("access granted");
                                s.Write(buff, 0, buff.Length);
                                currentlyConnected.Add(client);
                                Task.Run(() => ClientHandling(client));
                            }
                            else
                            {
                                buff = Encoding.UTF8.GetBytes("Wrong log or pass");
                                s.Write(buff,0,buff.Length);
                            }
                        }
                        else if (currentlyConnected.Count >= maximumConnectionsCount)
                        {
                            buff = Encoding.UTF8.GetBytes("no");
                            s.Write(buff, 0, buff.Length);
                        }
                    }
                    
                }
            }
            catch(Exception e) 
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (listener != null)
                {
                    listener.Stop();
                    listener = null;
                }
            }
        }

        private static void ClientHandling(TcpClient client)
        {
            int localCount = attemptCount;
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 60000;
            timer.Tick += (object sender, EventArgs ea) => { localCount = attemptCount;timer.Stop(); };
            var conn = new TcpConnection(client.Client.RemoteEndPoint as IPEndPoint, DateTime.Now);
            clients.Add(conn);
            Console.WriteLine(conn);
            while (client.Connected)
            {
                if (localCount == 0 && !timer.Enabled)
                {
                    timer.Start();
                }
                var stream = client.GetStream();
                var builder = new StringBuilder();
                int bytes;
                byte[] buff = new byte[1024];
                do
                {
                    bytes = stream.Read(buff, 0, buff.Length);
                    builder.Append(Encoding.UTF8.GetString(buff, 0, bytes));
                } while (stream.DataAvailable);
                if (builder.ToString() == "exiting")
                    break;
                if(localCount == 0 && timer.Enabled)
                {
                    buff = Encoding.UTF8.GetBytes($"Timeout is {timer.Interval/1000} seconds! Wait a little bit...");
                    stream.Write(buff,0,buff.Length);
                    continue;
                }
                string answer = "";
                if (ExchangeCourse.ContainsKey(builder.ToString()))
                {
                    answer = $"The rate of {builder.ToString()} is {ExchangeCourse[builder.ToString()].ToString()} UAH";
                }
                else
                {
                    answer = "No such rate found.";
                }

                buff = Encoding.UTF8.GetBytes(answer);
                stream.Write(buff, 0, buff.Length);
                if(!conn.queries_answers.ContainsKey(builder.ToString()))
                    conn.queries_answers.Add(builder.ToString(), answer);
                if(localCount!=0)
                    localCount--;
            }
            conn.whendisconn = DateTime.Now;
            Console.WriteLine($"{conn.who} disconnected at {conn.whendisconn.ToShortTimeString()}");
            currentlyConnected.Remove(client);
        }
    }
}
