using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (AppConect db = new AppConect())
            {
                var logBD = db.client.Where(c => c.Login == textBox1.Text).ToList();
                var passDB = db.client.Where(c => c.Password == textBox2.Text).ToList();
                if (logBD.Any() && passDB.Any())
                {
                    
                    Form3 f3 = new Form3();
                    f3.Show();
                    this.Hide();
                }
                else
                    MessageBox.Show("Не вериный(е) логин или(и) пароль");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }

     

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }




}