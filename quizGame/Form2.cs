using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Media;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace quizGame
{
    public partial class Form2 : Form
    {
        // variables list for this quiz game
        private DateTime startTime;
        int correctAnswer;
        int questionNumber = 1;
        int score;
        int percentage;
        int totalQuestions;
        int timeElapsed = 0;
        Timer timer = new Timer();

        string playerName = "";

        public Form2()
        {
            InitializeComponent();
            
            askQuestion(questionNumber);

            totalQuestions = 9;

            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            Controls.Add(lblTimer);

            // Start the timer
            startTime = DateTime.Now;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeElapsed++;
            var elapsed = DateTime.Now - startTime;

            // Convert time elapsed to minutes and seconds format
            var minutes = elapsed.Minutes;
            var seconds = elapsed.Seconds;


            // Update label with the new time format
            lblTimer.Text = $"Time Elapsed: {minutes} minute(s), {seconds} second(s)";
        }

        private void ClickAnswerEvent(object sender, EventArgs e)
        {
            var senderObject = (System.Windows.Forms.Button)sender;
            int buttonTag = Convert.ToInt32(senderObject.Tag);

            if (buttonTag == correctAnswer)
            {
                score++;
            }

            if (questionNumber == totalQuestions)
            {
                // Stop the timer
                timer.Stop();

                // Work out the time elapsed in minutes and seconds format

                // Work out the percentage here
                percentage = (int)Math.Round((double)(100 * score) / totalQuestions);

                // Prompt the user for their name
                string playerName = "";
                using (var form = new Form())
                {
                    var label = new Label() { Left = 50, Top = 20, Text = "Please enter your name:" };
                    var textBox = new System.Windows.Forms.TextBox() { Left = 50, Top = 40, Width = 200 };
                    var buttonOk = new System.Windows.Forms.Button() { Text = "OK", Left = 120, Width = 50, Top = 70, DialogResult = DialogResult.OK };
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.ClientSize = new System.Drawing.Size(300, 120);
                    form.Controls.Add(textBox);
                    form.Controls.Add(label);
                    form.Controls.Add(buttonOk);
                    form.AcceptButton = buttonOk;
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        playerName = textBox.Text;
                    }
                }

                // Write score, time elapsed, and player name to file

                string connectionString = "server=10.1.3.40;port=3306;database=64102010070;uid=64102010070;password=64102010070";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {

                    connection.Open();

                    MySqlCommand command = new MySqlCommand("INSERT INTO myquizdatabase (PlayerName, Score, TotalQuestions, Percentage, TimeElapsedSeconds, DateCreated) VALUES (@playerName, @score, @totalQuestions, @percentage, @timeElapsedSeconds, @dateCreated)", connection);
                    // Convert timeElapsed to TimeSpan format
                    TimeSpan timeSpan = TimeSpan.FromSeconds(timeElapsed);

                    // Get the formatted time string
                    string timeElapsedFormatted = timeSpan.ToString(@"hh\:mm\:ss");



                    // Insert the formatted timeElapsed into the database

                    command.Parameters.AddWithValue("@playerName", playerName);
                    command.Parameters.AddWithValue("@score", score);
                    command.Parameters.AddWithValue("@totalQuestions", totalQuestions);
                    command.Parameters.AddWithValue("@percentage", percentage);
                    command.Parameters.AddWithValue("@timeElapsedSeconds", timeElapsedFormatted);
                    command.Parameters.AddWithValue("@dateCreated", DateTime.Now);
                    command.ExecuteNonQuery();
                }



                DialogResult result = MessageBox.Show("Quiz Ended" + Environment.NewLine +
                                     "You have answered " + score + " questions correctly" + Environment.NewLine +
                                     "Your total percentage is " + percentage + " %" + Environment.NewLine +
                                     "Click OK to play again", "Quiz Result", MessageBoxButtons.OKCancel);

                if (result == DialogResult.Cancel)
                {
                    DialogResult confirmResult = MessageBox.Show("Do you want to return to MainMenu?", "Confirm", MessageBoxButtons.YesNo);

                    if (confirmResult == DialogResult.Yes)
                    {
                        // สร้างและเรียกใช้ Form1 ใหม่
                        Form1 form1 = new Form1();
                        form1.Show();
                        this.Hide(); // ซ่อน Form2 แทนที่จะปิด
                    }
                    else if (confirmResult == DialogResult.No)
                    {
                        Application.Exit(); // ปิดโปรแกรม
                    }
                }

                score = 0;
                questionNumber = 0;
                timeElapsed = 0;
                askQuestion(questionNumber);
                startTime = DateTime.Now;

                timer.Start(); // Restart the timer

            }

            questionNumber++;
            askQuestion(questionNumber);
        }



        private void askQuestion(int qnum)
        {
            switch (qnum)
            {
                case 1:
                    pictureBox1.Image = Properties.Resources.tofu;
                    pictureBox2.Image = Properties.Resources.pork;


                    button1.Text = "แกงจืด";
                    button2.Text = "ไข่ตุ๋น";
                    button3.Text = "ผัดเต้าหู้ไข่หมูสับ";
                    button4.Text = "ผัดเต้าหู้หมูสับน้ำพริกเผา";
                    correctAnswer = 1;
                    break;
                case 2:
                    pictureBox1.Image = Properties.Resources.makua;
                    pictureBox2.Image = Properties.Resources.chili;


                    button1.Text = "น้ำพริก";
                    button2.Text = "แกงเขียวหวาน";
                    button3.Text = "ผัดเผ็ดมะเขือเปราะ";
                    button4.Text = "แกงเผ็ดกะทิมะเขือเปราะ";
                    correctAnswer = 2;
                    break;
                case 3:
                    pictureBox1.Image = Properties.Resources.jun;
                    pictureBox2.Image = Properties.Resources.vet;


                    button1.Text = "ขนมจีน";
                    button2.Text = "ก๋วยเตี๋ยวส้นจันทร์";
                    button3.Text = "ก๋วยเตี๋ยว";
                    button4.Text = "ผัดไทย";
                    correctAnswer = 4;
                    break;
                case 4:
                    pictureBox1.Image = Properties.Resources.egg;
                    pictureBox2.Image = Properties.Resources.puykuk;


                    button1.Text = "ขาหมู";
                    button2.Text = "ไข่พะโล้";
                    button3.Text = "ไข่เจียว";
                    button4.Text = "ไข่ตุ๋น";
                    correctAnswer = 2;
                    break;
                case 5:
                    pictureBox1.Image = Properties.Resources.kung;
                    pictureBox2.Image = Properties.Resources.woonsen;



                    button1.Text = "กุ้งอบวุ้นเส้น";
                    button2.Text = "ยำกุ้ง";
                    button3.Text = "วุ้นเส้นอบกุ้ง";
                    button4.Text = "เส้นอบวุ้นกุ้ง";

                    correctAnswer = 1;

                    break;

                case 6:

                    pictureBox1.Image = Properties.Resources.papaya;
                    pictureBox2.Image = Properties.Resources.bean;



                    button1.Text = "ต้ำสม";
                    button2.Text = "ส้ำตม";
                    button3.Text = "ส้มตำ";
                    button4.Text = "ต้มสำ";

                    correctAnswer = 3;

                    break;

                case 7:

                    pictureBox1.Image = Properties.Resources.pigblood;
                    pictureBox2.Image = Properties.Resources.pork;



                    button1.Text = "ต้มเลือดหมู";
                    button2.Text = "ก๋วยเตี๋ยว";
                    button3.Text = "ต้มเลือดไก่";
                    button4.Text = "ต้มเลือดเป็ด";

                    correctAnswer = 1;

                    break;

                case 8:

                    pictureBox1.Image = Properties.Resources.kana;
                    pictureBox2.Image = Properties.Resources.bigline;



                    button1.Text = "ก๋วยเตี๋ยวคั่วไก่";
                    button2.Text = "ราดหน้า";
                    button3.Text = "ผัดซีอิ๊ว";
                    button4.Text = "ก๋วยเตี๋ยวคั่วไก่";

                    correctAnswer = 3;

                    break;

                case 9:

                    pictureBox1.Image = Properties.Resources.pork;
                    pictureBox2.Image = Properties.Resources.chili;

                    

                    button1.Text = "ยำใหญ่";
                    button2.Text = "ยำไก่";
                    button3.Text = "ยำหมูสับ";
                    button4.Text = "ลาบ";

                    correctAnswer = 4;

                    break;


            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void lblQuestion_Click(object sender, EventArgs e)
        {

        }
    }

}