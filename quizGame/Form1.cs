using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace quizGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SoundPlayer bg = new SoundPlayer(@"D:\Quiz-Game3\quizGame\Resources\bateatbanana.wav");
            bg.Play();
        }
        private void btn_start_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form2 quiz = new Form2();

            quiz.Closed += (s, args) => this.Close();

            quiz.Show();
        }

        private void btn_score_Click(object sender, EventArgs e)
        {
            ScoreForm scoreForm = new ScoreForm();
            scoreForm.ShowDialog();
        }

        
           // string connetionString = null;
           // MySqlConnection cnn;
            //connetionString = "server=10.1.3.40;database=64102010070;uid=64102010070;pwd=64102010070;";
            //cnn = new MySqlConnection(connetionString);
            //try
            //{
             //   cnn.Open();
            //    MessageBox.Show("Connection Open ! ");
             //   cnn.Close();
           // }
           // catch (Exception ex)
           // {
              //  MessageBox.Show("Can not open connection ! ");
           // }
        }
    
}
