using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace iContacts2
{
    class Birth
    {
        public int FindBirth(String x, int l)
        {
            String line;
            try
            {
                StreamReader find = new StreamReader("contacts.txt");
                int i = 1;
                do
                {
                    line = find.ReadLine();
                    if (line == null) break;
                    if (i > l )
                    {
                        if (line.Length < 5) line += "aaaaaaa"; //gia na mhn mas vgalei provlhma to substing

                        if (line.Substring(0, x.Length).Contains(x))
                        {

                            find.Close();
                            return i;
                        }
                    }

                    i++;

                } while (!find.EndOfStream);
                find.Close();
                return -1;
            }
            catch (Exception a)
            {
                Console.WriteLine(a.Message);
                return -1;
            }
        }
    }
}

/* 
            flag3 = 0; flag1 = 0;
            comboBox2.Items.Clear();
            richTextBox1.Clear();
            String line3;
            int i = 1; int ns = -1;
            if (eyresh.Text != "") {
                ns = src.FindByName(eyresh.Text, ns);
}
          
         
            while (ns != -1)
            {
                flag3++;
                if (comboBox1.SelectedIndex == 1) ns -= 1;
                if (comboBox1.SelectedIndex == 2) ns -= 2;
                i = 1;
                try
                {
                    StreamReader search = new StreamReader("contacts.txt");
                    do
                    {
                        if (i < ns) search.ReadLine();
                            else
                            {

                                if (i == ns + 2)
                                {
                                    line2 = search.ReadLine();
                                    richTextBox1.Text += line2 + "\n";
                                    comboBox2.Items.Add(line2);
                                }
                                else
                                {
                                    line3 = search.ReadLine();
                                    if (line3 != " ")
                                    {
                                        richTextBox1.Text += line3 + "\n";
                                    }
                                }
                            }
                        i++;
                    } while (i < ns + 8);
                    if (comboBox1.SelectedIndex == 1) ns += 1;
                    if (comboBox1.SelectedIndex == 2) ns += 2;
                    ns = src.FindByName(eyresh.Text, ns);
                    flag1++;
                    search.Close();
                    richTextBox1.Text += "\n";
                }
                catch (Exception a)
                {
                    Console.WriteLine(a.Message);
                }

            }
            if (flag1 == 0)
            {
                MessageBox.Show("Contact Not Found");
                
            }

            else
            {
                richTextBox1.Visible = true;
                label9.Visible = true;
            }
            */