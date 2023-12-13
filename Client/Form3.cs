using Microsoft.EntityFrameworkCore.Storage;
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

namespace Client
{
    public partial class Form3 : Form
    {
        private TcpClient tcpClient;
        private NetworkStream clientStream;
        public Form3()
        {
            InitializeComponent();

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            ConnectoToServer();
        }

        private void ConnectoToServer()
        {
            tcpClient = new TcpClient();
            tcpClient.Connect("127.0.0.1", 9000);
            clientStream = tcpClient.GetStream();
        }

        private void ReceiveQuotes()
        {
            try
            {
                byte[] message = new byte[4096];
                int byteRead = clientStream.Read(message, 0, 4096);
                string receiveQuote = System.Text.Encoding.UTF8.GetString(message);
                listBox1.Invoke((MethodInvoker)(() => listBox1.Items.Add(receiveQuote)));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка получения цитаты {ex.ToString()}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread recevieThread = new Thread(new ThreadStart(ReceiveQuotes));
            recevieThread.Start();
        }

      
    }
}
