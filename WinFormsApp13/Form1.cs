using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp13
{
    public partial class Form1 : Form
    {
        private Button firstClicked = null;
        private Button secondClicked = null;
        private int score = 0;
        private int matchedPairs = 0;
        private bool isChecking = false;

        public Form1()
        {
            InitializeComponent();
            SetupEvents();
            InitializeGame();
        }

        private void SetupEvents()
        {
            button1.Click += Card_Click;
            button2.Click += Card_Click;
            button3.Click += Card_Click;
            button4.Click += Card_Click;
            button5.Click += Card_Click;
            button6.Click += Card_Click;
            button7.Click += Card_Click;
            button8.Click += Card_Click;

            button9.Click += (s, e) => InitializeGame(); 

            button10.Click += (s, e) =>
            {
                DialogResult result = MessageBox.Show("Çıxış etmək istədiyinizə əminsiniz?", "Təsdiq", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    this.Close();
                }
            };
        }

        private void InitializeGame()
        {
            score = 0;
            matchedPairs = 0;
            label2.Text = "0";
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            firstClicked = null;
            secondClicked = null;
            isChecking = false;

            List<Color> colors = new List<Color>
            {
                Color.Red, Color.Red,
                Color.Blue, Color.Blue,
                Color.Green, Color.Green,
                Color.Yellow, Color.Yellow
            };

            Random rand = new Random();
            colors = colors.OrderBy(x => rand.Next()).ToList();

            Button[] buttons = { button1, button2, button3, button4, button5, button6, button7, button8 };

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Tag = colors[i];
                buttons[i].BackColor = Color.WhiteSmoke;
                buttons[i].Text = "";
                buttons[i].Enabled = true;
            }
        }

        private void Card_Click(object sender, EventArgs e)
        {
            if (isChecking) return;

            Button clickedCard = sender as Button;

            if (clickedCard == null || clickedCard.BackColor != Color.WhiteSmoke)
                return;

            clickedCard.BackColor = (Color)clickedCard.Tag;

            if (firstClicked == null)
            {
                firstClicked = clickedCard;
                return;
            }

            secondClicked = clickedCard;
            isChecking = true;

            if ((Color)firstClicked.Tag == (Color)secondClicked.Tag)
            {
                score++;
                matchedPairs += 2;
                label2.Text = score.ToString();

                pictureBox1.Visible = true;  
                pictureBox2.Visible = false;

                firstClicked.Enabled = false;
                secondClicked.Enabled = false;
                ResetSelection();

                if (matchedPairs == 8)
                {
                    MessageBox.Show("Təbriklər, bütün kartları tapdınız! Yekun xal: " + score, "Oyun Bitdi");
                }
            }
            else
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = true;  
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            if (firstClicked != null) firstClicked.BackColor = Color.WhiteSmoke;
            if (secondClicked != null) secondClicked.BackColor = Color.WhiteSmoke;

            ResetSelection();
        }

        private void ResetSelection()
        {
            firstClicked = null;
            secondClicked = null;
            isChecking = false;
        }
    }
}