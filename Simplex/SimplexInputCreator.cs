using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplex
{
    static class SimplexInputCreator
    {
        // Количество Переменных
        static public int variablesСount;
        // Количество ограничений
        static public int rowsCount;
        static public int formsNeeded;
        // Стремление к min/max
        static public string minMax;
        // Строки в задаче
        static public List<MyRow> rows;
        // решение задачи
        static public double[] result;

        static public void AddSpace()
        {
            SimplexInputCreator.rows = new List<MyRow>(SimplexInputCreator.rowsCount);
        }

        static public List<string> CreateSolution()
        {

            double[] result = new double[SimplexInputCreator.variablesСount];
            List<MyRow> resTable = new List<MyRow>() ;
            List<MyRow> tableResult;
            Simplex S = new Simplex(SimplexInputCreator.rows);
            tableResult = S.Calculate(result);

            List<string> s = new List<string>(tableResult.Count);
            foreach(var row in tableResult)
            {
                string tmp = "";
                foreach(double d in row.factors)
                {
                    string stmp = d.ToString();
                    while (stmp.Length < 5)
                    {
                        stmp += " ";
                    }
                    tmp += stmp;
                }
                s.Add(tmp);
            }
            SimplexInputCreator.result = result;
            return s;
            
        }
    }
}
