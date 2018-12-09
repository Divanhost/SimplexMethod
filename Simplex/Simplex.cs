using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplex
{
    public class Simplex
    {
        // Таблица для рачсетов
        List<MyRow> table;
        // Количество строк и столбцов
        int m, n;
        // Список базисных переменных
        List<int> basis; 

        public Simplex(List<MyRow> data)
        {
            //Установка количества строк и столбцов
            m = SimplexInputCreator.rowsCount + 1;
            n = SimplexInputCreator.variablesСount+1;
            table = new List<MyRow>(m);
            basis = new List<int>();

            table.InsertRange(0,data);
            //Заполнение table исходными данными
            for (int i = 0; i < table.Count; i++)
            {
                table[i] = new MyRow(table[i].factors.Length + SimplexInputCreator.rowsCount-1, table[i].factors, table[i].sign);
                if (i == 0) continue;
                
                if (table[i].sign.Equals("<="))
                    table[i].factors[SimplexInputCreator.variablesСount + i] = 1;
                else
                    table[i].factors[SimplexInputCreator.variablesСount + i] = -1;
                basis.Add(SimplexInputCreator.variablesСount + i);
                
            }
            
            n = table[0].factors.Length;
        }

        // Вычисление решения
        public List<MyRow> Calculate(double[] result)
        {
            // Ведущие столбец и строка
            int mainCol, mainRow; 

            while (!IsItEnd())
            {
                mainCol = findMainCol();
                mainRow = findMainRow(mainCol);
                basis[mainRow-1] = mainCol;
                // Обнуление новой таблицы
                List<MyRow> newTable = new List<MyRow>(m);
                for(int i = 0; i < m; i++)
                {
                    newTable.Add(new MyRow(n-1, new double[n-1], "<="));
                }
                // Вычисление новой ведущей строки
                for (int j = 0; j < n; j++)
                {
                    newTable[mainRow].factors[j] = table[mainRow].factors[j] / table[mainRow].factors[mainCol];
                }
                // Пересчитываем элементы симплекс-таблицы
                for (int i = 0; i < m; i++)
                {
                    if (i == mainRow)
                        continue;

                    for (int j = 0; j < n; j++)
                    {
                        newTable[i].factors[j] = table[i].factors[j] - table[i].factors[mainCol] * newTable[mainRow].factors[j];
                    }
                }
                table = newTable;
            }

            //заносим в result найденные значения X
            for (int i = 0; i < result.Length; i++)
            {
                int k = basis.IndexOf(i+1);
                if (k != -1)
                    result[i] = table[k+1].factors[0];
                else
                    result[i] = 0;
            }

            return table;
        }

        // Условие завершения алгоритма
        private bool IsItEnd()
        {
            bool flag = true;
            if (SimplexInputCreator.minMax.Equals("max"))
            {
                if (table[0].factors.Count((e) => e < 0) != 0)
                {
                    flag = false;
                }
            }
            if (SimplexInputCreator.minMax.Equals("min"))
            {
                if (table[0].factors.Count((e) => e > 0) != 0)
                {
                    flag = false;
                }
            }
            return flag;
        }

        // Нахождение ведущего столбца
        private int findMainCol()
        {
            int mainCol = 1;
            if (SimplexInputCreator.minMax.Equals("max"))
            {
                for (int i = 2; i < n; i++)
                {
                    if (table[0].factors[i] < table[0].factors[mainCol])
                        mainCol = i;
                }
            }
            if (SimplexInputCreator.minMax.Equals("min"))
            {
                for (int i = 2; i < n; i++)
                {
                    if (table[0].factors[i] > table[0].factors[mainCol])
                        mainCol = i;
                }
            }
            return mainCol;
        }

        // Нахождение ведущей строки
        private int findMainRow(int mainCol)
        {
            int mainRow = -1;

            for (int i = 1; i < m; i++)
                if(table[i].factors[mainCol]>0)
                {
                    mainRow = i;
                    break;
                }
            for (int i = mainRow + 1; i < m; i++)
                if ((table[i].factors[mainCol] > 0) && ((table[i].factors[0] / table[i].factors[mainCol]) < (table[mainRow].factors[0] / table[mainRow].factors[mainCol])))
                    mainRow = i;
            return mainRow;
        }


    }
}