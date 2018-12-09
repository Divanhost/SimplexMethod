using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplex
{
    public class Simplex
    {
        //data - симплекс таблица без базисных переменных
        List<MyRow> table1;
        int m, n;

        List<int> basis; //список базисных переменных

        public Simplex(List<MyRow> data)
        {
            m = SimplexInputCreator.rowsCount + 1;
            n = SimplexInputCreator.variablesСount+1;
            table1 = new List<MyRow>(m);
            basis = new List<int>();

            table1.InsertRange(0,data);
            
            for (int i = 0; i < table1.Count; i++)
            {
                table1[i] = new MyRow(table1[i].factors.Length + SimplexInputCreator.rowsCount-1, table1[i].factors, table1[i].sign);
                if (i == 0) continue;
                table1[i].factors[SimplexInputCreator.variablesСount + i] = 1;
                basis.Add(SimplexInputCreator.variablesСount + i);
                
            }
            
            n = table1[0].factors.Length;
        }

        //result - в этот массив будут записаны полученные значения X
        public List<MyRow> Calculate(double[] result)
        {
            int mainCol, mainRow; //ведущие столбец и строка

            while (!IsItEnd())
            {
                mainCol = findMainCol();
                mainRow = findMainRow(mainCol);
                basis[mainRow-1] = mainCol;
                List<MyRow> newTable = new List<MyRow>(m);
                for(int i = 0; i < m; i++)
                {
                    newTable.Add(new MyRow(n-1, new double[n-1], "<="));
                }
                for (int j = 0; j < n; j++)
                {
                    newTable[mainRow].factors[j] = table1[mainRow].factors[j] / table1[mainRow].factors[mainCol];
                }
                for (int i = 0; i < m; i++)
                {
                    if (i == mainRow)
                        continue;

                    for (int j = 0; j < n; j++)
                    {
                        newTable[i].factors[j] = table1[i].factors[j] - table1[i].factors[mainCol] * newTable[mainRow].factors[j];
                    }
                }
                table1 = newTable;
            }

            //заносим в result найденные значения X
            for (int i = 0; i < result.Length; i++)
            {
                int k = basis.IndexOf(i+1);
                if (k != -1)
                    result[i] = table1[k+1].factors[0];
                else
                    result[i] = 0;
            }

            return table1;
        }

        private bool IsItEnd()
        {
            bool flag = true;
           if( table1[0].factors.Count((e) => e < 0) !=0)
            {
                flag = false;
            }
            return flag;
        }

        private int findMainCol()
        {
            int mainCol = 0;
            for (int i = 1; i < n; i++)
            {
                if (table1[0].factors[i] < table1[0].factors[mainCol])
                    mainCol = i;
            }
            return mainCol;
        }

        private int findMainRow(int mainCol)
        {
            int mainRow = -1;

            for (int i = 1; i < m; i++)
                if(table1[i].factors[mainCol]>0)
                {
                    mainRow = i;
                    break;
                }
            for (int i = mainRow + 1; i < m; i++)
                if ((table1[i].factors[mainCol] > 0) && ((table1[i].factors[0] / table1[i].factors[mainCol]) < (table1[mainRow].factors[0] / table1[mainRow].factors[mainCol])))
                    mainRow = i;
            return mainRow;
        }


    }
}