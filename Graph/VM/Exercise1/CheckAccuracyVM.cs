using System;
using OxyPlot;
using OxyPlot.Series;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OxyPlot.Axes;

namespace Graph.VM.Exercise1
{
    internal class CheckAccuracyVM:ViewModel
    {
        public PlotModel MyModel { get; set; }
        public DataTable Table { get; set; }
        public CheckAccuracyVM(double[] InputX,double[] InputY, Func<double,double>[] functions, string[] functionNames)
        {
            double[] sumdy = new double[4];
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
                sumdy[i] = sum;
                Table.Rows.Add(row);
            }
            MyModel = new PlotModel { Title = "Апроксимация экспериментальных данных методом наименьших квадратов" };
            var s1 = new BarSeries { Title = "sum(dy)", StrokeColor = OxyColors.Black, StrokeThickness = 1 };
            var categoryAxis = new CategoryAxis { Position = AxisPosition.Left };
            for(int i=0; i<4; i++)
            {
                s1.Items.Add(new BarItem { Value = sumdy[i] });
                categoryAxis.Labels.Add(functionNames[i]);
            }
            MyModel.Series.Add(s1);
            MyModel.Axes.Add(categoryAxis);
        }
    }
}
