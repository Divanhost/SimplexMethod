using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplex
{
    static class SimplexInputCreator
    {
        static public int variablesСount;
        static public int rowsCount;
        static public int formsNeeded;
        static public List<MyRow> rows;
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
                    tmp += d +" ";
                }
                s.Add(tmp);

            }

            for(int i =0; i < result.Length; i++)
            {
                s.Add(string.Format("X[{0}]={1}", i + 1, result[i]));
            }
            
            return s;
        }
    }
}
