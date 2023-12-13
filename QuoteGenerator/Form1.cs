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
        "Сильный не тот, кто не падает, а тот, кто встает после каждого падения.", "Жизнь — это не та игра, в которую можно поиграть дважды. Но можно играть умнее.", "Счастье — это не конечная точка, а способ путешествия.","Лучший способ предсказать будущее — создать его.","Сложные времена — это тест нашей стойкости, а не приговор нашему счастью.",
        "Невозможное возможно, если ты готов пойти на невозможное.", "Жизнь — это искусство находить красоту в моменте.",
        "Не бойся перемен, бойся застоя. В изменениях скрыты новые возможности.", "Секрет успеха — начать действовать, даже если ты не уверен в своих силах.",
        "Самая большая опасность — это упустить возможность.", "Счастье — это не тот, кто у тебя есть, а то, как ты этим пользуешься.",
        "Будущее принадлежит тем, кто верит в красоту своих мечтаний.", "Успех — это не конечная точка, а новый старт.",
        "Твоя уверенность — это твой ключ к победе. Верь в себя!", "Чем больше ты знаешь, тем меньше ты боишься.",
        "Сильный человек — не тот, кто всегда побеждает, а тот, кто не сдаётся.","Жизнь коротка, чтобы тратить её на ненависть и обиды.",
        "Не откладывай на завтра то, что можешь сделать сегодня.", "Трудности — это лестница, по которой поднимаются те, кто не боится взлететь.",
        "Возможности не приходят сами — их создают.", "Счастье — это не в том, чтобы иметь всё, а в том, чтобы ценить то, что есть.",
        "Лучший способ предсказать будущее — создать его.", "Жизнь — это искусство находить радость в мелочах.",
        "Будь как маяк, несмотря на бурю, всегда оставайся ярким.", "Только тот, кто рискует, может выиграть.",
        "Тайна успеха — в том, чтобы начать с того, что ты имеешь, где ты есть.", "Будь героем своей собственной истории, а не зрителем в чужой.",
        "Самое важное — это не останавливаться, даже когда трудно.", "Счастье — это выбор, а не результат.",
        "Не бойся ошибок, они — твои лучшие учителя.", "Твоя судьба не определена звездами, а тем, как ты используешь свои возможности.",
        "Будь как изменением, которое ты хочешь видеть в мире.", "Важно не то, как много, а как хорошо ты живешь.",
        "Секрет успеха — в постоянном движении вперед.", "Жизнь — это приключение, и каждый день — новая страница в твоей истории.",
        "Нет легких путей к успеху, но твоя отвага и настойчивость создадут тебе дорогу.", "Будь как солнце, всегда свети впереди себя.",
        "Счастье — это нечто внутреннее, и его источник", "Твоя уверенность — это твой лучший наставник, следуй за ней.",
        "Лучший способ предсказать будущее — это создать его своими руками."
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
                        MessageBox.Show("Сервер перегружен, выполните подключение позже");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при прослушивании клиентов: {ex.Message}");
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
                MessageBox.Show($"Ошибка при обработке сообщения клиента: {ex.Message}");
            }
            finally
            {
                tcpClient.Close();
                connectedClients.Remove(tcpClient);
            }
        }
    }

}