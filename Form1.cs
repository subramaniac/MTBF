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
using System.Globalization;

namespace MTBF2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private DateTime ConvertStringToDateTime(string input_date)
        {
            string[] formatstring = { "dd/MM/yyyy h:mm:ss tt", "dd-MM-yyyy h:mm:ss tt" };

            DateTime timestamp;
            DateTime.TryParseExact(input_date, formatstring, null, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AdjustToUniversal, out timestamp);

            return timestamp;
        }

        private List<string> ReadDataIntoList(string file_location)
        {
            List<string> output = new List<string>();

            var reader = new StreamReader(file_location);
            while (!reader.EndOfStream)
            {
                output.Add(reader.ReadLine());
            }
            reader.Close();

            return output;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            List<string> fulllist = new List<string>();//full list from SERVICE COMPLETED
            List<string> sn = new List<string>(); // Sepparated list for Serial Numbers
            List<DateTime> dates = new List<DateTime>(); // Sepparated list for Dates
            List<string> customer = new List<string>(); //Sepparated list for Customers 
            List<string> model = new List<string>(); // Sepparated list for Models
            fulllist = ReadDataIntoList(@"C:\Users\Prasanna Subramaniam\Desktop\!Prasanna\MyDatabases\_SERVICECOMPLETED.txt");
            // 1 = Serial Number , 2=Model, 3=Customer, 4=Dates/Times 
            foreach (string s in fulllist)//Variable s holds the value from the list that will be put into the sepparate lists.
            {

                //MessageBox.Show(s); HOW TO VIEW
                sn.Add(s.Split('<')[1]);
                model.Add(s.Split('<')[2]);
                customer.Add(s.Split('<')[3]);
                dates.Add(ConvertStringToDateTime(s.Split('<')[4]));
            };

            int count = 0;
            TimeSpan total = TimeSpan.Zero;
            List<Customer> customerlist = new List<Customer>();
            for (int i = 0; i < sn.Count; i++)
            {
                for (int j = i + 1; j < sn.Count; j++)
                {
                    if (sn[i] == sn[j])
                    {
                        bool found = false;
                        foreach (Customer c in customerlist)
                        {
                            if (customer[i] == c.getName())
                            {
                                c.addCount();
                                c.addTotal((dates[j] - dates[i]).TotalDays);
                                found = true;
                                break;
                            }
                        }

                        if (found == false)
                        {
                            Customer cust = new Customer(customer[i], (dates[j] - dates[i]).TotalDays);
                            customerlist.Add(cust);
                            break;
                        }
                    }
                }
            }
            customerlist = customerlist.OrderBy(o => o.getAverage()).ToList();

            var writer = new StreamWriter("customers.csv");
            writer.WriteLine("Customer Name,Average,Number Of Units");
            foreach (Customer c in customerlist)
            {
                if (c.getCount() > 5)
                {
                    writer.WriteLine(c.getName() + "," + c.getAverage() + "," + c.getCount());
                    listBox1.Items.Add("The MTBF for " + c.getName() + " is " + c.getAverage().ToString("#0.00") + " with " + c.getCount() + " units.");
                }
            }
            writer.Close();

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            List<string> fulllist = new List<string>();//full list from SERVICE COMPLETED
            List<string> sn = new List<string>(); // Sepparated list for Serial Numbers
            List<DateTime> dates = new List<DateTime>(); // Sepparated list for Dates
            List<string> customer = new List<string>(); //Sepparated list for Customers 
            List<string> model = new List<string>(); // Sepparated list for Models
            fulllist = ReadDataIntoList(@"C:\Users\Prasanna Subramaniam\Desktop\!Prasanna\MyDatabases\_SERVICECOMPLETED.txt");
            // 1 = Serial Number , 2=Model, 3=Customer, 4=Dates/Times 
            foreach (string s in fulllist)//Variable s holds the value from the list that will be put into the sepparate lists.
            {
                //MessageBox.Show(s); HOW TO VIEW
                sn.Add(s.Split('<')[1]);
                model.Add(s.Split('<')[2]);
                customer.Add(s.Split('<')[3]);
                dates.Add(ConvertStringToDateTime(s.Split('<')[4]));
            };
            int count = 0;
            TimeSpan total = TimeSpan.Zero;
            for (int i = 0; i < 3939; i++)
            {
                string dummysn;
                for (int j = i + 1; j < 3939; j++)
                {
                    string comparedummy;
                    comparedummy = sn[j];
                    if (sn[i] == sn[j])
                    {
                        TimeSpan timedifference = dates[j] - dates[i];
                        total = total + timedifference;
                        listBox1.Items.Add(sn[i] + "              " + timedifference + "              " + customer[i]);
                        break;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            List<string> fulllist = new List<string>();//full list from SERVICE COMPLETED
            List<string> sn = new List<string>(); // Sepparated list for Serial Numbers
            List<DateTime> dates = new List<DateTime>(); // Sepparated list for Dates
            List<string> customer = new List<string>(); //Sepparated list for Customers 
            List<string> model = new List<string>(); // Sepparated list for Models
            fulllist = ReadDataIntoList(@"C:\Users\Prasanna Subramaniam\Desktop\!Prasanna\MyDatabases\_SERVICECOMPLETED.txt");
            // 1 = Serial Number , 2=Model, 3=Customer, 4=Dates/Times 
            foreach (string s in fulllist)//Variable s holds the value from the list that will be put into the sepparate lists.
            {

                //MessageBox.Show(s); HOW TO VIEW
                sn.Add(s.Split('<')[1]);
                model.Add(s.Split('<')[2]);
                customer.Add(s.Split('<')[3]);
                dates.Add(ConvertStringToDateTime(s.Split('<')[4]));
            };

            int count = 0;
            TimeSpan total = TimeSpan.Zero;
            List<Model1> modellist = new List<Model1>();
            for (int i = 0; i < sn.Count; i++)
            {
                for (int j = i + 1; j < sn.Count; j++)
                {

                    if (sn[i] == sn[j])
                    {
                        if ((dates[j] - dates[i]).TotalDays > 5)
                        {
                        bool found = false;
                        foreach (Model1 m in modellist)
                        {
                            if (model[i] == m.getName())
                            {
                                m.addCount();
                                m.addTotal((dates[j] - dates[i]).TotalDays);
                                found = true;
                                break;
                            }
                        }

                        if (found == false)
                        {

                            Model1 cust = new Model1(model[i], (dates[j] - dates[i]).TotalDays);
                            modellist.Add(cust);
                            break;

                        }
                        }

                    }
                }
            }
                modellist = modellist.OrderBy(o => o.getAverage()).ToList();
                foreach (Model1 m in modellist)
                {
                    if (m.getCount() >= 5)
                    {
                        listBox1.Items.Add("The MTBF for " + m.getName() + " is " + m.getAverage().ToString("#0.00") + " with " + m.getCount() + " units.");
                    }
                }
            
        }
    }
}
