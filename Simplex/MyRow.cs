using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplex
{
    // Класс для хранения Функции и ограничений
    public class MyRow
    {
       // Коофициенты при переменных в строке
       public double[] factors { get; protected set; }
       // Знак строки
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
