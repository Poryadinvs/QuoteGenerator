using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Microsoft.EntityFrameworkCore;
namespace QuoteGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            StartServer();
        }

        private TcpListener tcpListener;
        private Thread listenerThread;
        private List<TcpClient> connectedClients = new List<TcpClient>();
        private int maxClients = 4;
        private List<string> quotes = new List<string> {
        "������� �� ���, ��� �� ������, � ���, ��� ������ ����� ������� �������.", "����� � ��� �� �� ����, � ������� ����� �������� ������. �� ����� ������ �����.", "������� � ��� �� �������� �����, � ������ �����������.","������ ������ ����������� ������� � ������� ���.","������� ������� � ��� ���� ����� ���������, � �� �������� ������ �������.",
        "����������� ��������, ���� �� ����� ����� �� �����������.", "����� � ��� ��������� �������� ������� � �������.",
        "�� ����� �������, ����� ������. � ���������� ������ ����� �����������.", "������ ������ � ������ �����������, ���� ���� �� �� ������ � ����� �����.",
        "����� ������� ��������� � ��� �������� �����������.", "������� � ��� �� ���, ��� � ���� ����, � ��, ��� �� ���� �����������.",
        "������� ����������� ���, ��� ����� � ������� ����� ��������.", "����� � ��� �� �������� �����, � ����� �����.",
        "���� ����������� � ��� ���� ���� � ������. ���� � ����!", "��� ������ �� ������, ��� ������ �� �������.",
        "������� ������� � �� ���, ��� ������ ���������, � ���, ��� �� ������.","����� �������, ����� ������� � �� ��������� � �����.",
        "�� ���������� �� ������ ��, ��� ������ ������� �������.", "��������� � ��� ��������, �� ������� ����������� ��, ��� �� ������ ��������.",
        "����������� �� �������� ���� � �� �������.", "������� � ��� �� � ���, ����� ����� ��, � � ���, ����� ������ ��, ��� ����.",
        "������ ������ ����������� ������� � ������� ���.", "����� � ��� ��������� �������� ������� � �������.",
        "���� ��� ����, �������� �� ����, ������ ��������� �����.", "������ ���, ��� �������, ����� ��������.",
        "����� ������ � � ���, ����� ������ � ����, ��� �� ������, ��� �� ����.", "���� ������ ����� ����������� �������, � �� �������� � �����.",
        "����� ������ � ��� �� ���������������, ���� ����� ������.", "������� � ��� �����, � �� ���������.",
        "�� ����� ������, ��� � ���� ������ �������.", "���� ������ �� ���������� ��������, � ���, ��� �� ����������� ���� �����������.",
        "���� ��� ����������, ������� �� ������ ������ � ����.", "����� �� ��, ��� �����, � ��� ������ �� ������.",
        "������ ������ � � ���������� �������� ������.", "����� � ��� �����������, � ������ ���� � ����� �������� � ����� �������.",
        "��� ������ ����� � ������, �� ���� ������ � ������������� �������� ���� ������.", "���� ��� ������, ������ ����� ������� ����.",
        "������� � ��� ����� ����������, � ��� ��������", "���� ����������� � ��� ���� ������ ���������, ������ �� ���.",
        "������ ������ ����������� ������� � ��� ������� ��� ������ ������."
        };

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void StartServer()
        {
            tcpListener = new TcpListener(IPAddress.Any, 9000);
            listenerThread = new Thread(new ThreadStart(ListenForClient));
            listenerThread.Start();
            
        }

        private async void ListenForClient()
        {
            tcpListener.Start();

            try
            {
                while (true)
                {
                    if (connectedClients.Count <= maxClients)
                    {
                        TcpClient client = tcpListener.AcceptTcpClient();
                        connectedClients.Add(client);

                        Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                        clientThread.Start(client);
                    }
                    else
                    {
                        MessageBox.Show("������ ����������, ��������� ����������� �����");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������ ��� ������������� ��������: {ex.Message}");
            }
            finally
            {
                tcpListener.Stop();
            }


        }

      


        private void HandleClientComm(object clientObj)
        {
            TcpClient tcpClient = (TcpClient)clientObj;
            NetworkStream clientStream = tcpClient.GetStream();
            try
            {
                while (true)
                {
                    string quoteToSend = quotes[new Random().Next(quotes.Count)];
                    byte[] quoteData = System.Text.Encoding.UTF8.GetBytes(quoteToSend);
                    clientStream.Write(quoteData, 0, quoteData.Length);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"������ ��� ��������� ��������� �������: {ex.Message}");
            }
            finally
            {
                tcpClient.Close();
                connectedClients.Remove(tcpClient);
            }
        }
    }

}