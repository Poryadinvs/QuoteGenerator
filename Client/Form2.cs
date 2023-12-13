using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.Logging;

namespace Client
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


            if (textBox2.Text == textBox3.Text)
            {
                string log = textBox1.Text;
                string pass = textBox3.Text;

                using (AppConect db = new AppConect())
                {
                    Client client = new Client { Login = log, Password = pass };

                    db.client.Add(client);
                    db.SaveChanges();
                    MessageBox.Show("Регистрация успешна");
                }
            }
            else
            {
                MessageBox.Show("Пароль не сходится");
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }
    }

    //public class Client
    //{
    //    public int ID { get; set; }
    //    public string ?Login { get; set; }
    //    public string ?Password { get; set; }
    //}


    //public class AppConect : DbContext
    //{
    //    public DbSet<Client> client => Set<Client>();

    //    public AppConect() => Database.EnsureCreated();

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        optionsBuilder.UseSqlite("Data Source=client.db");
    //    }
    //}



}
