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
    class Search
    {
        public int FindByName(String x,int l)
        {
            String line;
            try
            {
                StreamReader find = new StreamReader("contacts.txt");

                int i = 1;
                do
                {
                    line = find.ReadLine();
                    if (i > l) // wsta na xekinhsoume na psaxnoume meta apo to prohgoumeno find
                    {

                        if (line == x) //an vroume auto pou psaxnoume 
                        {
                            find.Close();
                            return i; //epistrefoume thn thesh pou to vrhkame
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
