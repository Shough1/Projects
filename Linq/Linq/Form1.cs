using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Linq
{
    public partial class Form1 : Form
    {
        Dictionary<string, Elements> chemDict = null;
        BindingSource bs = new BindingSource();
        bool error = false;
        public Form1()
        {
            InitializeComponent();
            
        }


        private void Form1_Load(object sender, EventArgs e)
        {
   
            chemDict = Helper.CreateEntry();
            dataGridView1.DataSource = bs;
            
        }

        static List<string> GetSplitList(string s)
        {
            List<string> temp = new List<string>();
            string tempS = "";
            for (int i = 0; i < s.Length; i++)
            {

                if (char.IsUpper(s[i]) && i != 0)
                {
                    temp.Add(tempS);
                    tempS = "";
                }

                tempS += s[i];

                if (i == s.Length - 1)
                {
                    temp.Add(tempS);
                }

            }

            
            
            return temp;
        }


        private void btnAtomic_Click(object sender, EventArgs e)
        {
      
            dataGridView1.DataSource = bs;
            List<Elements> elmtList = new List<Elements>();
            //num, nam, sym, mass
            var stuff = Helper.CreateEntry();
            foreach (KeyValuePair<string, Elements> kvp in stuff)
            {
                elmtList.Add(kvp.Value);
            }
            bs.DataSource = from n in elmtList orderby n.AtomNum
                            select new
                            {
                                AtomicNumber = n.AtomNum,
                                Name = n.Name,
                                Symbol = n.Symbol,
                                Mass = n.MMass
                            };
        }


        private void btnName_Click(object sender, EventArgs e)
        {
   
            dataGridView1.DataSource = bs;
            List<Elements> elmtList = new List<Elements>();
            //num, nam, sym, mass
            var stuff = Helper.CreateEntry();
            foreach (KeyValuePair<string, Elements> kvp in stuff)
            {
                elmtList.Add(kvp.Value);
            }
            bs.DataSource = from n in elmtList orderby n.Name
                            select new
                            {
                                AtomicNumber = n.AtomNum,
                                Name = n.Name,
                                Symbol = n.Symbol,
                                Mass = n.MMass
                            };
        }

        private void btnChar_Click(object sender, EventArgs e)
        {
      
            dataGridView1.DataSource = bs;
            List<Elements> elmtList = new List<Elements>();
            //num, nam, sym, mass
            var stuff = Helper.CreateEntry();
            foreach (KeyValuePair<string, Elements> kvp in stuff)
            {
                elmtList.Add(kvp.Value);
            }
            bs.DataSource = from n in elmtList
                            orderby n.Symbol where n.Symbol.Length == 1
                            select new
                            {
                                AtomicNumber = n.AtomNum,
                                Name = n.Name,
                                Symbol = n.Symbol,
                                Mass = n.MMass
                            };
        }

        private void tbChemical_TextChanged(object sender, EventArgs e)
        {
            error = false;
            List<string> unValidated = GetSplitList(tbChemical.Text);

            List<string> validated = Validate(unValidated, chemDict);

           
            bs.DataSource = from n in validated
                            select new
                            {
                                AtomicNumber = chemDict[RemoveNumber(n)].Name,
                                Count = RemoveString(n),
                                Max = chemDict[RemoveNumber(n)].MMass,
                                TotalMass = chemDict[RemoveNumber(n)].MMass * RemoveString(n)
                            };

            var v = (from n in validated select n).ToList();

            double d = 0.00;

            foreach(var q in v)
            {
                d += chemDict[RemoveNumber(q)].MMass * RemoveString(q);
            }

            
            if(validated.Count == 0 && tbChemical.Text != "")
            {
                textBox2.BackColor = Color.Red;
            }
            else if(!error)
            {
                textBox2.BackColor = BackColor;
            }
           
            textBox2.Text = d.ToString("F4") + " g / mol";


            dataGridView1.Columns[0].HeaderText = "Element";
        }

        private List<string> Validate(List<string> l, Dictionary<string, Elements> d)
        {
            List<string> temp = new List<string>();

            foreach(string value in l) //value has the number
            {
                int number = RemoveString(value);
                string noNumber = value;
                
                if(Regex.IsMatch(value, @"[A-Za-z]"))
                    noNumber = RemoveNumber(value);

                bool stop = false;
                int iterations = noNumber.Length;
                int firstRuns = iterations;
                
                do
                {
                    if (d.Keys.Contains(noNumber))
                    {
                        if(iterations == firstRuns)
                        {
                            temp.Add(value);
                        }
                        else
                        {
                            temp.Add(noNumber + "1");
                            textBox2.BackColor = Color.Yellow;
                            error = true;
                        }
                        
                        stop = true;
                    }

                    noNumber = noNumber.Remove(noNumber.Length - 1, 1);
                    iterations--;
                }
                while (!stop && iterations > 0);
                
        
            }
            return temp;
        }

       
        static List<string> RemoveNumber(List<string> l)
        {
            List<string> newL = new List<string>();

            foreach (string value in l.ToList())
            {
                string s = Regex.Replace(value, @"[0-9]", "");
                newL.Add(s);
            }

            return newL;
        }

        static string RemoveNumber(string l)
        {
            string s = Regex.Replace(l, @"[0-9]", "");           
            return s;
        }

        static int RemoveString(string l)
        {

            string s = Regex.Replace(l, @"[A-Za-z]", "");

            if (s == "")
                return 1;

            int i = 0;
            try
            {
                i = int.Parse(s);
            }
            catch { }

            return i;
        }

    }
}
