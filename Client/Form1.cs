using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        TcpClient client;
        Task waitconn;
        public Form1()
        {
            InitializeComponent();
            client = new TcpClient();
        }

        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            if (ConnectBtn.Text == "Connect")
            {
                client = new TcpClient();
                IPAddress address = null ;
                int port;
                if (!IPAddress.TryParse(IpTextBox.Text, out address))
                {
                    MessageBox.Show("Wrong IP");
                    return;
                }
                if (!int.TryParse(PortTextBox.Text,out port))
                {
                    MessageBox.Show("Wrong Port");
                    return;
                }
                waitconn=Task.Run(()=>Connect(address,port));
                ConnectBtn.Text = "Disconnect";
            }
            else
            {
                var s = client.GetStream();
                byte[] buff = Encoding.UTF8.GetBytes("exiting");
                s.Write(buff, 0, buff.Length);
                ConnectBtn.Text = "Connect";
                IpTextBox.Clear();
                PortTextBox.Clear();
                client.Close();
            }
        }

        private void SendMsgBtn_Click(object sender, EventArgs e)
        {
            if(SendMsgBtn.Text.Length==0)
            {
                MessageBox.Show("Empty message cannot be sent");
                return;
            }
            if (client==null||!client.Connected)
            {
                MessageBox.Show("You are not connected!");
                return;
            }
            waitconn.Wait();
            Task.Run(()=>SendAndReceiveMessage(QueryMsgTextBox.Text));
        }

        private void SendAndReceiveMessage(string message)
        {
            var stream = client.GetStream();
            byte[] buff = Encoding.UTF8.GetBytes(message);
            stream.Write(buff,0,buff.Length);
            buff = new byte[256];
            int bytes;
            var builder = new StringBuilder();

            try
            {
                do
                {
                    bytes = stream.Read(buff, 0, 256);
                    builder.Append(Encoding.UTF8.GetString(buff, 0, bytes));
                } while (stream.DataAvailable);

                if (InvokeRequired)
                {
                    Invoke(new Action(() => ServerMsgTextBox.Text = builder.ToString()));
                }
                else
                {
                    ServerMsgTextBox.Text = builder.ToString();
                }
            }
            catch { }
        }

        private void Connect(IPAddress address,int port)
        {
            try
            {
                client.Connect(address, port);
                byte[]buff= Encoding.UTF8.GetBytes("can connect?");
                int bytes;
                var builder = new StringBuilder();
                var s = client.GetStream();
                s.Write(buff,0,buff.Length);
                buff=new byte[256];
                do
                {
                    bytes = s.Read(buff,0,buff.Length);
                    builder.Append(Encoding.UTF8.GetString(buff,0,bytes));
                } while (s.DataAvailable);
                if (builder.ToString() == "no")
                {
                    MessageBox.Show("Server is overloaded. Try later");
                    if (InvokeRequired)
                    {
                        Invoke(new Action(() => ConnectBtn.Text = "Connect"));
                    }
                    else
                    {
                        ConnectBtn.Text = "Connect";
                    }
                    return;
                }
                logic l = new logic();
                l.ShowDialog();
                string query = $"{l.log}\n{l.pass}";
                buff = Encoding.UTF8.GetBytes(query);
                builder.Clear();
                s.Write(buff, 0, buff.Length);
                buff = new byte[256];
                do
                {
                    bytes = s.Read(buff, 0, 256);
                    builder.Append(Encoding.UTF8.GetString(buff,0,bytes)) ;
                } while (s.DataAvailable);
                if(builder.ToString()=="Wrong log or pass")
                    throw new Exception();
                else
                {
                    MessageBox.Show(builder.ToString());
                }
            }
            catch
            {
                MessageBox.Show("An error occured while connecting");
                if(InvokeRequired)
                {
                    Invoke(new Action(()=>ConnectBtn.Text = "Connect"));
                }
                else
                {
                    ConnectBtn.Text = "Connect";
                }
                return;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (client != null && client.Connected)
            {
                var s = client.GetStream();
                byte[] buff = Encoding.UTF8.GetBytes("exiting");
                s.Write(buff, 0, buff.Length);
                client.Close();
            }
        }
    }
}
