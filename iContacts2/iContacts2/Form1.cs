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
    public partial class Form1 : Form
    {
        int n, flag1 = 0, flag3 = 0, flag = 0, clicked = 0;
        String line2;
        Search src = new Search();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Birthday();
            ((DataGridViewTextBoxColumn)dataGridView1.Columns[2]).MaxInputLength = 10;
            comboBox1.SelectedIndex = 0;
            namet.Focus();
            for (n = 0; n < 7; n++)
            {
                dataGridView1.Columns[n].ReadOnly = true;
            }
            ReloadList();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Birthday()
        {
            richTextBox2.Clear(); 
            int b = 0; int i;
            Birth birthday = new Birth(); //neo antikeimeno ths klashs Birth
            b = birthday.FindBirth(DateTime.Now.ToString("d/M/"), b);
            //h sunarthsh epistrefei sthn metavlith b thn thesh sthn opoia vrhke thn sxetikh hmeromhnia thn prwth fora
            //an den thn vrei katholoy epistrefei -1

            while (b != -1) //true oso exoume vrei kapoia hmeromhnia idia me thn shmerinh
            {
                i = 1;
                try
                {
                    StreamReader search = new StreamReader("contacts.txt");
                    do
                    {
                        if  (i == b - 5) //gia na pame apo thn thesh ths hmeromhnias sthn thesh tou onomatos
                        {
                            richTextBox2.Text += search.ReadLine() + "\n"; //grafoume to onoma
                            break;
                        }
                        else search.ReadLine(); 
                        i++;
                    } while (true); //stamataei efoson vrhke to onoma
                    b = birthday.FindBirth(DateTime.Now.ToString("d/M"), b); //xana h idia diadikasia
                    search.Close();
                }
                catch (Exception a)
                {
                    Console.WriteLine(a.Message);
                }

            }
        }
        private void button2_Click(object sender, EventArgs e)
        {

            if (emailt.Text.Length < 10  || emailt.Text == "Ex. someone@example.com")
            {
                MessageBox.Show("Wrong Email");
            }
            else if (!emailt.Text.Substring(emailt.Text.Length - 5, 5).Contains(".") || !emailt.Text.Substring(3, emailt.Text.Length - 3).Contains("@"))
            {
                MessageBox.Show("Wrong Email");
            }
            else if (phonet.Text.Length != 10)
            {
                MessageBox.Show("Wrong Telephone");
            }
            else
            { //elegxos textboxewn
                if (flag == 8 && namet.Text != "" && phonet.Text != "") //apokleioume thn periptwsh na mhn valoun katholou onoma h thlefwno
                {

                    try
                    {
                        StreamWriter sw = new StreamWriter("contacts.txt", true);
                        sw.WriteLine(namet.Text);
                        sw.WriteLine(surt.Text);
                        sw.WriteLine(phonet.Text);
                        sw.WriteLine(emailt.Text);
                        sw.WriteLine(addresst.Text + " " + numbert.Text);
                        sw.Write(dateTimePicker1.Value.Day + "/");
                        sw.Write(dateTimePicker1.Value.Month + "/");
                        sw.WriteLine(dateTimePicker1.Value.Year);
                        sw.WriteLine(cityt.Text);
                        sw.WriteLine(countryt.Text);
                        sw.Close();
                    }
                    catch (Exception a)
                    {
                        Console.WriteLine("Exception 3: " + a.Message);
                    }
                    ReloadList();
                    Birthday();
                }
                else
                {
                    MessageBox.Show("Fill All The Boxes");
                }
            }
        }
        private void ReloadList(){

            try
            {
                StreamReader reload = new StreamReader("contacts.txt");

                dataGridView1.Rows.Clear();
                do
                {
                    n = dataGridView1.Rows.Add();
                    for (int j = 0; j < 8; j++) dataGridView1.Rows[n].Cells[j].Value = reload.ReadLine();

                } while (!reload.EndOfStream);
                reload.Close();
            }
            catch (Exception a)
            {
                Console.WriteLine(a.Message);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {

            int j = 0; flag1 = 0; flag3 = 0;
            if (comboBox1.SelectedIndex == 1) j=1;
            if (comboBox1.SelectedIndex == 2) j= 2;
            comboBox2.Items.Clear();
            richTextBox1.Visible = true;
            richTextBox1.Clear();
            if (eyresh.Text !="")
            {
                for (int i = 0; i < dataGridView1.Rows.Count -1; i++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value.ToString() == eyresh.Text) //vrethike to zhtoumeno
                    {
                        comboBox2.Items.Add(dataGridView1.Rows[i].Cells[2].Value.ToString());
                        for (int l = 0; l < 8; l++) //ektipwsh olhs ths grammhs
                        {
                            richTextBox1.Text += dataGridView1.Rows[i].Cells[l].Value.ToString() + "\n";
                        }
                        flag3++;
                        flag1++;
                    }
                }
                if (flag1 == 0) MessageBox.Show("Contact Not Found");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) //elexos ama einai se edit mode 
            {
                timer1.Enabled = true; //timer gia enalagh xrwmatos 
                for (n = 0; n < 7; n++)
                {
                    dataGridView1.Columns[n].ReadOnly = false;
                }
                button3.Text = "SAVE"; //allazoume to koumpi apo edit se save
             
            }
            else
            {
                dataGridView1.GridColor = Color.Black;
                timer1.Enabled = false;
                button3.Text = "EDIT";
                for (n = 0; n < 7; n++)
                {
                    dataGridView1.Columns[n].ReadOnly = true;
                }
                try
                {
                    StreamWriter sw = new StreamWriter("contacts.txt");
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                             sw.WriteLine(dataGridView1.Rows[i].Cells[j].Value); //apo datagrid sto arxeio
                        }
                    }
                    sw.Close();
                }
                catch (Exception a)
                {
                    Console.WriteLine("Exception 4 :" + a.Message);
                }
                Birthday();
            }
        }


        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (dataGridView1.GridColor == Color.Red)
            {
                dataGridView1.GridColor = Color.White;
            }
            else dataGridView1.GridColor = Color.Red; //enallagh xrwmatos
          
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int dlt;

            if (clicked == 0)
            {
                button1.PerformClick();//Perform Search
                if (flag3 > 1)//more than 1 person with the same Name
                {
                    comboBox2.Visible = true;
                    label19.Visible = true;
                    comboBox2.SelectedIndex = 0;
                    button4.BackColor = Color.Red;
                    clicked = 1; 
                    comboBox1.Enabled = false;

                }
                else if(flag3==1)
                {
                    dlt = src.FindByName(eyresh.Text, 0);
                    dataGridView1.Rows.Remove(dataGridView1.Rows[dlt / 8]); //diagrafoume thn grammh pou htan to onoma 
                    button3.PerformClick(); button3.PerformClick();//Edit And Save (dhladh vazoume oti uparxei sto datagrid sto arxeio 
                    ReloadList();
                    richTextBox1.Visible = false;
                    label9.Visible = false;
                    eyresh.Text = "";
                }
                
            }
            else
            {
                dlt = src.FindByName(comboBox2.Text, 0);
                dataGridView1.Rows.Remove(dataGridView1.Rows[dlt / 8]);
                button3.PerformClick(); button3.PerformClick(); //edit and save
                ReloadList();
                if (MessageBox.Show("Do you want to delete another Contact with that "+ comboBox1.Text.Substring(3, comboBox1.Text.Length-3) + ": " +eyresh.Text, "Delete", MessageBoxButtons.YesNo) == DialogResult.No){
                    clicked=0;
                    richTextBox1.Clear();
                    comboBox2.Visible=false;
                    label19.Visible=false;
                    eyresh.Text="";
                    button4.BackColor = Color.White;
                    comboBox1.Enabled = true;
                    if (richTextBox1.Text == "")
                    {
                        comboBox1.Enabled = true;
                        richTextBox1.Visible = false;
                        label9.Visible = false;
                        button4.BackColor = Color.White;
                    }
                }else{
                    
                    button1.PerformClick();//Refresh Details

                    if(comboBox2.Items.Count>0){
                        comboBox2.SelectedIndex = 0;
                    }
                    else
                    {
                        
                        clicked = 0;
                        eyresh.Text = "";
                        comboBox2.Visible = false;
                        label19.Visible = false;
                        button4.BackColor = Color.White;
                    }
                    if (richTextBox1.Text == "")
                    {
                        comboBox1.Enabled = true;
                        richTextBox1.Visible = false;
                        label9.Visible = false;
                    }

                }
            }

        }

        private void namet_MouseUp(object sender, MouseEventArgs e)
        {

            if (namet.Text == "Ex.Timmys") { flag++; namet.Text = ""; } 
        }

        private void surt_MouseUp(object sender, MouseEventArgs e)
        {
            if (surt.Text == "Ex. Alepis") { flag++; surt.Text = ""; } 
        }

        private void phonet_MouseUp(object sender, MouseEventArgs e)
        {
            if (phonet.Text == "Ex. 698456978") { phonet.Text = ""; flag++; }
        }

        private void emailt_MouseUp(object sender, MouseEventArgs e)
        {
            if (emailt.Text == "Ex. someone@example.com") { emailt.Text = ""; flag++; }
        }

        private void addresst_MouseUp(object sender, MouseEventArgs e)
        {
            if (addresst.Text == "Ex. Panepisthmiou") { addresst.Text = ""; flag++; }
        }

        private void numbert_MouseUp(object sender, MouseEventArgs e)
        {
            if (numbert.Text == "Ex.49") { numbert.Text = ""; flag++; }
        }

        private void cityt_MouseUp(object sender, MouseEventArgs e)
        {
            if (cityt.Text == "Ex. Athens") { cityt.Text = ""; flag++; }
        }

        private void countryt_MouseUp(object sender, MouseEventArgs e)
        {
            if (countryt.Text == "Ex. Greece") { flag++; countryt.Text = ""; }
        }

        private void namet_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
                e.IsInputKey = true;
        }

        private void surt_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
                e.IsInputKey = true;
        }

        private void phonet_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
                e.IsInputKey = true;
        }

        private void emailt_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
                e.IsInputKey = true;
        }

        private void addresst_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
                e.IsInputKey = true;
        }

        private void numbert_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
                e.IsInputKey = true;
        }

        private void dateTimePicker1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
                e.IsInputKey = true;
        }

        private void cityt_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
                e.IsInputKey = true;
        }

        private void countryt_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
                e.IsInputKey = true;
        }

        private void phonet_KeyPress(object sender, KeyPressEventArgs e)
        {
            const char Delete = (char)8;
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Delete;
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            MessageBox.Show((dataGridView1.CurrentCell.ColumnIndex).ToString());
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
        }

        private void emailt_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2.PerformClick();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3.PerformClick();
        }

        private void applicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Made By: \n \n Thodoris Glampedakis \n Teodoro Gomes \n Panagiotis Mpountouris");
        }

        private void namet_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
