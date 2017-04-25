using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieSummary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "Please enter movie";
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            label1.BackColor = Color.Transparent;
            label1.Visible = false;
            label2.BackColor = Color.Transparent;
            label2.Visible = false;
            label3.BackColor = Color.Transparent;
            label3.Visible = false;
            label4.BackColor = Color.Transparent;
            label4.Visible = false;
            label5.BackColor = Color.Transparent;
            label5.Visible = false;
            label6.BackColor = Color.Transparent;
            label6.Visible = false;

            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Visible = false;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                textBox1.ForeColor = SystemColors.GrayText;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=movies";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                String query = "INSERT INTO logs (movie_name) VALUES ('" + textBox1.Text + "')";

                MySqlCommand command = new MySqlCommand(query, conn);
                int response = command.ExecuteNonQuery();               
            }

            string movie = textBox1.Text;

            string url1 = "http://www.omdbapi.com/?t=" + movie;

            WebClient wc = new WebClient();
            string mystring1 = wc.DownloadString(url1);                       //Download whole json file
            JObject json = JObject.Parse(mystring1);                          //Take json object

            var item1 = json["Title"];
            label1.Text = (string)item1;                                      //Display title when button is pressed            
            label1.Visible = true;

            var item2 = json["Year"];
            label2.Text = (string)item2;                                      //Display year when button is pressed            
            label2.Visible = true;

            var item3 = json["Genre"];
            label3.Text = (string)item3;                                      //Display genre when button is pressed            
            label3.Visible = true;

            var item4 = json["Awards"];
            label4.Text = (string)item4;                                      //Display awards when button is pressed            
            label4.Visible = true;

            var item5 = json["Metascore"];
            label5.Text = (string)item5;                                      //Display metascore when button is pressed            
            label5.Visible = true;

            var item6 = json["imdbRating"];
            label6.Text = (string)item6;                                      //Display imbscore when button is pressed            
            label6.Visible = true;

            var item7 = json["Poster"];                                       //Take image from json
            string url2 = item7 + ".jpg";
            pictureBox1.ImageLocation = url2;                                 //Display icon when button is pressed
            pictureBox1.Visible = true;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)         //Enter instead of button
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
                e.SuppressKeyPress = true;                                   //These two lines to turn of sound
                e.Handled = true;
            }
        }
    }
}

