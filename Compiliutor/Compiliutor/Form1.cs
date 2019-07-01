using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compiliutor
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
            richTextBox1.KeyPress += Compilaytor_KeyPress;
        }


        class bluend
        {

            public void check(RichTextBox richTextBox1)
            {
                MatchCollection[] matches = new MatchCollection[]
                {
                Regex.Matches(richTextBox1.Text, @"\b(int)\b"),
                Regex.Matches(richTextBox1.Text, @"\bfloat\b"),
                Regex.Matches(richTextBox1.Text, @"\bdouble\b")
                };


                foreach (var item in matches)
                {
                    foreach (var match in item.Cast<Match>())
                    {
                        richTextBox1.Select(match.Index, match.Length);
                        richTextBox1.SelectionColor = Color.Blue;
                    }
                }

            }
        }
        class Yellow
        {

            public void check(RichTextBox richTextBox1)
            {


                MatchCollection[] matches1 = new MatchCollection[]
                {
                 Regex.Matches(richTextBox1.Text, @"\bif\b"),
                Regex.Matches(richTextBox1.Text, @"\belse\b"),
                Regex.Matches(richTextBox1.Text, @"\bswitch\b")
                };
                foreach (var item in matches1)
                {
                    foreach (var match in item.Cast<Match>())
                    {
                        richTextBox1.Select(match.Index, match.Length);
                        richTextBox1.SelectionColor = Color.Yellow;
                    }
                }
            }

        }
        class Orange
        {
            public void check(RichTextBox richTextBox1)
            {
                MatchCollection[] matches2 = new MatchCollection[]
                {
                 Regex.Matches(richTextBox1.Text, @"\bclass\b"),
                Regex.Matches(richTextBox1.Text, @"\bStruct\b"),
                Regex.Matches(richTextBox1.Text, @"\benum\b")
                };
                foreach (var item in matches2)
                {
                    foreach (var match in item.Cast<Match>())
                    {
                        richTextBox1.Select(match.Index, match.Length);
                        richTextBox1.SelectionColor = Color.Orange;
                    }
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            richTextBox1.Font = new Font("Consolas", 45f, FontStyle.Bold);
            richTextBox1.BackColor = Color.White;
        }
        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;

            for (int i = 1; i < richTextBox1.Lines.Length; i++)
            {
                richTextBox2.Text += i.ToString() + '\r' + '\n';
            }

            var currentSelStart = richTextBox1.SelectionStart;
            var currentSelLength = richTextBox1.SelectionLength;

            richTextBox1.SelectAll();
            richTextBox1.SelectionColor = SystemColors.WindowText;


            bluend blue_obj = new bluend();
            Yellow yellow_obj = new Yellow();
            Orange orange_obj = new Orange();

            blue_obj.check(richTextBox1);
            yellow_obj.check(richTextBox1);
            orange_obj.check(richTextBox1);


            richTextBox1.Select(currentSelStart, currentSelLength);
            richTextBox1.SelectionColor = SystemColors.WindowText;
        }

        private void keypressed(Object o, KeyPressEventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog.FileName;
            System.IO.File.WriteAllText(filename, richTextBox1.Text);
            MessageBox.Show("Файл сохранен");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            richTextBox1.SelectionColor = Color.Blue;
            richTextBox1.ForeColor = Color.Blue;

            string filename = openFileDialog1.FileName;
            string fileText = System.IO.File.ReadAllText(filename);
            richTextBox1.Text = fileText;
            MessageBox.Show("Файл открыт");
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Contains(textBox1.Text)) 
            {
                int index = richTextBox1.Text.IndexOf(textBox1.Text);  
                string str1, str2;
                str1 = richTextBox1.Text.Substring(0, index); 
                str2 = richTextBox1.Text.Substring((index + textBox1.TextLength), (richTextBox1.TextLength - (index + textBox1.TextLength))); 
                string result = str1 + textBox2.Text + str2; 
                richTextBox1.Clear(); 
                richTextBox1.AppendText(result); 
                richTextBox1.Select(str1.Length, textBox2.Text.Length); 
              
            }
            else
                MessageBox.Show("Такого слова в RichTextBox не найдено"); 
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        ////

        private void Compilaytor_KeyPress(object sender, KeyPressEventArgs e)
        {

            switch (e.KeyChar)
            {
                case '(':
                    richTextBox1.Text += ")";
                    richTextBox1.SelectionStart = richTextBox1.Text.Length - 1;
                    break;
                case '{':
                    richTextBox1.Text += "}";
                    richTextBox1.SelectionStart = richTextBox1.Text.Length - 1;
                    break;
                case '[':
                    richTextBox1.Text += "]";
                    richTextBox1.SelectionStart = richTextBox1.Text.Length - 1;
                    break;
                case ')':
                    richTextBox1.Select(richTextBox1.SelectionStart, 1);
                    if (richTextBox1.SelectedText == ")")
                    {
                        richTextBox1.Text = richTextBox1.Text.Remove(richTextBox1.SelectionStart, 1);
                        richTextBox1.SelectionStart += richTextBox1.TextLength;
                    }
                    break;
                case '}':
                    richTextBox1.Select(richTextBox1.SelectionStart, 1);
                    if (richTextBox1.SelectedText == "}")
                    {
                        richTextBox1.Text = richTextBox1.Text.Remove(richTextBox1.SelectionStart, 1);
                        richTextBox1.SelectionStart += richTextBox1.TextLength;
                    }
                    break;
                case ']':
                    richTextBox1.Select(richTextBox1.SelectionStart, 1);
                    if (richTextBox1.SelectedText == "]")
                    {
                        richTextBox1.Text = richTextBox1.Text.Remove(richTextBox1.SelectionStart, 1);
                        richTextBox1.SelectionStart += richTextBox1.TextLength;
                    }
                    break;
                default:
                    break;
            }
        }
        ///
        private void button4_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Contains(textBox3.Text))
            {
                 int index = richTextBox1.Text.IndexOf(textBox3.Text);
                richTextBox1.Focus();
               
                richTextBox1.SelectionStart = index;
            }
            else
                MessageBox.Show("Такого слова в RichTextBox не найдено");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bool g = true;
            while (g == true)
            {
                if (richTextBox1.Text.Contains(textBox1.Text))
                {
                    int index = richTextBox1.Text.IndexOf(textBox1.Text);
                    string str1, str2;
                    str1 = richTextBox1.Text.Substring(0, index);
                    str2 = richTextBox1.Text.Substring((index + textBox1.TextLength), (richTextBox1.TextLength - (index + textBox1.TextLength)));
                    string result = str1 + textBox2.Text + str2;
                    richTextBox1.Clear();
                    richTextBox1.AppendText(result);
                    richTextBox1.Select(str1.Length, textBox2.Text.Length);

                }
                else
                {
                    MessageBox.Show("Такого слова в RichTextBox не найдено");
                    g = false;
                }
            }
        }
    }
}