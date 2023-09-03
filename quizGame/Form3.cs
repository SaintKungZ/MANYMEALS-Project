using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quizGame
{
    public partial class ScoreForm : Form
    {
        public ScoreForm()
        {
            InitializeComponent();
        }
        DataTable table = new DataTable();
     

        private void import_Click(object sender, EventArgs e)
        {
            string connetionString = "server=10.1.3.40;database=64102010070;uid=64102010070;pwd=64102010070;";
            MySqlConnection cnn = new MySqlConnection(connetionString);
            MySqlCommand cmd = new MySqlCommand("SELECT PlayerName, Score, TotalQuestions, TimeElapsedSeconds FROM myquizdatabase", cnn);
            cnn.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            cnn.Close();
        }
    }

}
