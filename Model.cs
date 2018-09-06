using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTBF2
{
    class Model1
    {
    
            string name;
            int count;
            double total;
            public Model1(string inName, double incoming)
            {
                name = inName;
                count = 1;
                total = incoming;

            }

            public string getName()
            {
                return this.name;
            }

            public int getCount()
            {
                return this.count;
            }

            public double getTotal()
            {
                return this.total;
            }

            public double getAverage()
            {
                return this.total / this.count;

            }

            public void addCount()
            {
                this.count = this.count + 1;
            }

            public void addTotal(double newTotal)
            {
                this.total = this.total + newTotal;
            }
        }
    }

