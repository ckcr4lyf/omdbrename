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
using System.Xml;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string code = textBox1.Text.Trim();
            string season = textBox2.Text.Trim();
            string url = "http://www.omdbapi.com/?i=" + code + "&Season=" + season + "&r=xml";

            XmlTextReader reader = new XmlTextReader(url);
            while (reader.ReadToFollowing("result"))
            {
                string name = reader.GetAttribute("Title");
                listBox1.Items.Add(name);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            string ext = textBox3.Text.Trim();
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*." + ext);
            foreach (string file in files)
            {
                listBox2.Items.Add(Path.GetFileName(file));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string epno, final, s, cname;
            int sno = int.Parse(textBox2.Text);
            int x = listBox1.Items.Count;
            int y = listBox2.Items.Count;
            if (x == y)
            {
                for (int  i = 1; i <= x; i++)
                {
                    if (i < 10)
                    {
                        epno = "0" + i.ToString();
                    }
                    else
                    {
                        epno = i.ToString();
                    }
                    if (sno < 10)
                    {
                        s = "0" + sno.ToString();
                    }
                    else
                    {
                        s = sno.ToString();
                    }
                    cname = listBox1.Items[i - 1].ToString();
                    cname.Replace('\\', ' ');
                    cname.Replace('/', ' ');
                    cname.Replace(':', ' ');
                    cname.Replace('*', ' ');
                    cname.Replace('?', ' ');
                    cname.Replace('<', ' ');
                    cname.Replace('>', ' ');
                    cname.Replace('|', ' ');
                    final = s + "x" + epno + " - " + cname + "." + textBox3.Text;
                    listBox3.Items.Add(final);
                    File.Move(listBox2.Items[i - 1].ToString(), final);
                }
            }
            else
            {
                MessageBox.Show("Inequal number of names and files!");
            }
        }
    }
}
