using System;
using OxyPlot;
using OxyPlot.Series;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Graph.VM
{
    internal class CheckAccuracyVM:ViewModel
    {
        public DataTable Table { get; set; }

        public CheckAccuracyVM()
        {

        }
        public CheckAccuracyVM(double[] InputX,double[] InputY, Func<double,double>[] functions, string[] functionNames)
        {
            Table = new DataTable();
            int n = InputX.Length;
            Table.Columns.Add(new DataColumn("function"));
            for(int i=0; i<n; i++)
            {
                Table.Columns.Add(new DataColumn($"dy{i + 1}"));
            }
            Table.Columns.Add(new DataColumn("sum(dy)"));
            for(int i=0; i<4; i++)
            {
                double sum = 0;
                var row = Table.NewRow();
                row[0] = functionNames[i];
                for (int j = 0; j<n; j++)
                {
                    double delta = Math.Abs(InputY[j] - functions[i](InputX[j]));
                    row[j + 1] = delta;
                    sum += delta;
                }
                row[n + 1] = sum;
                Table.Rows.Add(row);
            }
        }
    }
}
