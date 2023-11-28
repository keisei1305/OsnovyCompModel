using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Models
{
    public class Exercise2Model
    {
        static private readonly int n = 11;
        static private readonly int N = 1000;

        static private PlotModel GetTriangle()
        {
            double xLeft = 0;
            double xRight = 38;
            double yBottom = 0;
            double yTop = 20.9;
            var model = new PlotModel ();
            Func<double, double> triangleFunc = new Func<double, double>(x => {
                if (x < 20.9) return 10 * x / n;
                return 10 * (x - 20) / (n - 20) + 20;
            });
            var func1 = new FunctionSeries(x => 0, xLeft, xRight, 0.1) { Color = OxyColor.FromRgb(0,127,0)};
            var func2 = new FunctionSeries(triangleFunc, xLeft, xRight, 0.1) { Color = OxyColor.FromRgb(0, 127, 0) };
            model.Series.Add(func1);
            model.Series.Add(func2);

            var points = new LineSeries
            {
                Color = OxyColors.Red,
                MarkerStroke = OxyColors.Red,
                MarkerType = MarkerType.Circle,
                StrokeThickness = 0,
                MarkerSize = 4,
                Title = "Рандомные точки"
            };
            double xi, yi;
            var r = new Random();
            int count = 0;
            for (int i=0; i<N; i++)
            {
                xi = r.NextDouble() * xRight;
                yi = r.NextDouble() * yTop;
                if (yi < triangleFunc(xi))
                    count++;
                points.Points.Add(new DataPoint(xi,yi));
            }
            double S;
            S = (xRight - xLeft)*(yTop-yBottom)*count / N;
            model.Series.Add(points);
            model.Title = $"Площадь фигуры ={S}\n" +
                $"Абсолютная порещность равна ={Math.Abs(361-S)}\n" +
                $"Количество точек ={N}";
            return model;
        }

        static private PlotModel GetIntegral()
        {
            double xLeft = 0;
            double xRight = 5;
            double yTop = Math.Sqrt(29);
            double yBottom = 3*Math.Sqrt(2);
            var model = new PlotModel();
            var funcIntegral = new Func<double, double>(x => Math.Sqrt(29-n*Math.Pow(Math.Cos(x),2)));
            var func = new FunctionSeries(funcIntegral,0,5,0.1);
            model.Series.Add(func);

            var points = new LineSeries
            {
                Color = OxyColors.Red,
                MarkerStroke = OxyColors.Red,
                MarkerType = MarkerType.Circle,
                StrokeThickness = 0,
                MarkerSize = 4,
                Title = "Рандомные точки"
            };
            double xi, yi;
            var r = new Random();
            int count = 0;
            for (int i = 0; i < N; i++)
            {
                xi = r.NextDouble() * xRight;
                yi = r.NextDouble() * (yTop -yBottom) + yBottom;
                if (yi < funcIntegral(xi))
                    count++;
                points.Points.Add(new DataPoint(xi, yi));
            }
            model.Series.Add(points);
            double S = (xRight - xLeft) * (yTop - yBottom) * count / N;
            model.Title = $"Площадь фигуры ={S}\n" +
                $"Абсолютная порещность равна ={Math.Abs(3.09 - S)}\n" +
                $"Количество точек ={N}";
            return model;
        }

        static private PlotModel GetCircle()
        {
            double xLeft = -2*n;
            double xRight = 0;
            double yTop = 2*n;
            double yBottom = 0;

            var model = new PlotModel() { Title="Круг"};
            //x=a+r*cos(t) y = b+r*sin(t)
            var functionCircle = new Func<double, double, double>((x,y)=>Math.Pow(x+n,2)+Math.Pow(y-n,2));
            var func = new FunctionSeries(x=>-n + n*Math.Cos(x),y=> n + n*Math.Sin(y),0,Math.PI*2,0.05);
            model.Series.Add(func);
            var points = new LineSeries
            {
                Color = OxyColors.Red,
                MarkerStroke = OxyColors.Red,
                MarkerType = MarkerType.Circle,
                StrokeThickness = 0,
                MarkerSize = 4,
                Title = "Рандомные точки"
            };
            double xi, yi;
            var r = new Random();
            int count = 0;
            for (int i = 0; i < N; i++)
            {
                xi = r.NextDouble() * (xRight - xLeft) + xLeft;
                yi = r.NextDouble() * (yTop - yBottom) + yBottom;
                if (functionCircle(xi, yi) < Math.Pow(n, 2))
                    count++;
                points.Points.Add(new DataPoint(xi, yi));
            }
            double pi = 4 * (double)count / N;
            model.Title = $"Число пи ={pi}\n" +
                $"Абсолютная порещность равна ={Math.Abs(3.1415 - pi)}\n" +
                $"Количество точек ={N}";
            model.Series.Add(points);
            return model;
        }

        static private PlotModel GetPolar()
        {
            int A = n + 10, B = n - 10;
            var model = new PlotModel() { Title = "Круги", PlotType=PlotType.Polar };
            //t=>21*Math.Pow(Math.Cos(t),2)+Math.Pow(Math.Sin(t),2)
            var func = new FunctionSeries(t => 21 * Math.Pow(Math.Cos(t), 2) + Math.Pow(Math.Sin(t), 2), t=>t,0,Math.PI*2,0.05);
            model.Series.Add(func);
            return model;
        }
        static public (List<PlotModel>,string) GetModels()
        {
            var models = new List<PlotModel>();
            models.Add(GetTriangle());
            models.Add(GetIntegral());
            models.Add(GetCircle());
            models.Add(GetPolar());

            return (models,"");
        }
    }
}
