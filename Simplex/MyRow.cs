using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplex
{
    public class MyRow
    {
       public double[] factors { get; protected set; }
       public string sign { get; protected set; }

        public MyRow(int size,double[] factors, string sign ="<=")
        {
            this.factors = new double[size+1];
            Array.Clear(this.factors, 0, this.factors.Length);
            factors.CopyTo(this.factors,0);
            this.sign = sign;
        }
    }
}
