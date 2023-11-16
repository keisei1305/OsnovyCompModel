using Graph.Services;
using OxyPlot;
using OxyPlot.Legends;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Models
{
    internal class Exercise1Model
    {
        private readonly byte parameterAccuracy = 2;
        private readonly byte tempAccuracy = 4;
        private readonly double[] inputX = { 2, 4, 6, 8, 10, 12 };
        private readonly double[] inputY = { 2.4, 2.9, 3.0, 3.5, 3.6, 3.7 };
        private readonly int size = 6;

        private PlotModel _myModel;
        private FunctionSeries linearFunction;
        private FunctionSeries powerFunction;
        private FunctionSeries expFunction;
        private FunctionSeries quadricFunction;
        private Func<double, double>[] _functions = new Func<double, double>[4];
        private string[] _functionNames = new string[4];

        public PlotModel MyModel
        {
            get => _myModel;
        }
        public Func<double,double>[] Functions
        {
            get => _functions;
        }
        public string[] FunctionNames
        {
            get => _functionNames;
        }

        public double[] InputX
        {
            get => inputX;
        }

        public double[] InputY
        {
            get => inputY;
        }

        private void GetLinearFunc()
        {
            var coefs = MathMethods.MethodKramera(MakeSystem(inputX, inputY, 2));
            coefs[0] = Math.Round(coefs[0], parameterAccuracy);
            coefs[1] = Math.Round(coefs[1], parameterAccuracy);
            _functions[0] = x => coefs[1] * x + coefs[0];
            _functionNames[0] = $"y={coefs[1]}x+{coefs[0]}";
            linearFunction = new FunctionSeries(_functions[0], inputX[0], inputX[size - 1], 0.1, _functionNames[0])
            {
                Color = OxyColor.FromRgb(127, 0, 0)
            };
        }

        private void GetPowerFunc()
        {
            var tempX = inputX.Select(x => Math.Log(x)).ToArray();
            var tempY = inputY.Select(y => Math.Log(y)).ToArray();
            var coefs = MathMethods.MethodKramera(MakeSystem(tempX, tempY, 2));
            coefs[0] = Math.Round(Math.Exp(coefs[0]), parameterAccuracy);
            coefs[1] = Math.Round(coefs[1], parameterAccuracy);
            _functions[1] = x => Math.Pow(x, coefs[1]) * coefs[0];
            _functionNames[1] = $"y=x^{coefs[1]}*{coefs[0]}";
            powerFunction = new FunctionSeries(_functions[1], inputX[0], inputX[size - 1], 0.1, _functionNames[1])
            {
                Color = OxyColor.FromRgb(255, 144, 0)
            };
        }

        private void GetExpFunc()
        {
            var tempY = inputY.Select(y => Math.Log(y)).ToArray();
            var coefs = MathMethods.MethodKramera(MakeSystem(inputX, tempY, 2));
            coefs[0] = Math.Round(Math.Exp(coefs[0]), parameterAccuracy);
            coefs[1] = Math.Round(coefs[1], parameterAccuracy);
            _functions[2] = x => Math.Exp(x * coefs[1]) * coefs[0];
            _functionNames[2] = $"y={coefs[0]}*e^({coefs[1]}*x)";
            expFunction = new FunctionSeries(_functions[2], inputX[0], inputX[size - 1], 0.1, _functionNames[2])
            {
                Color = OxyColor.FromRgb(0, 144, 255)
            };
        }

        private void GetQuadriacFunc()
        {
            var coefs = MathMethods.MethodKramera(MakeSystem(inputX, inputY, 3));
            coefs[0] = Math.Round(coefs[0], parameterAccuracy);
            coefs[1] = Math.Round(coefs[1], parameterAccuracy);
            coefs[2] = Math.Round(coefs[2], parameterAccuracy);
            _functions[3] = x => coefs[2] * Math.Pow(x, 2) + coefs[1] * x + coefs[0];
            _functionNames[3] = $"y={coefs[2]}*x^2+{coefs[1]}x+{coefs[0]}";
            quadricFunction = new FunctionSeries(_functions[3], inputX[0], inputX[size - 1], 0.1, _functionNames[3])
            {
                Color = OxyColor.FromRgb(0, 144, 0)
            };
        }

        private Matrix MakeSystem(double[] listX, double[] listY, int dim)
        {
            int n = listX.Length;
            Matrix matrix = new Matrix(dim, dim + 1);
            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    double sumA = 0;
                    for (int k = 0; k < n; k++)
                    {
                        sumA += Math.Pow(listX[k], i + j);
                    }
                    sumA = Math.Round(sumA, tempAccuracy);
                    matrix[i, j] = sumA;
                }
                double sumB = 0;
                for (int j = 0; j < n; j++)
                {
                    sumB += listY[j] * Math.Pow(listX[j], i);
                }
                sumB = Math.Round(sumB, tempAccuracy);
                matrix[i, dim] = sumB;
            }
            return matrix;
        }

        public Exercise1Model()
        {
            _myModel = new PlotModel { Title = "Апроксимация экспериментальных данных методом наименьших квадратов" };
            var points = new LineSeries
            {
                Color = OxyColors.Red,
                MarkerStroke = OxyColors.Red,
                MarkerType = MarkerType.Circle,
                StrokeThickness = 0,
                MarkerSize = 4,
                Title = "Экспереминтальные точки"
            };
            GetQuadriacFunc();
            GetLinearFunc();
            GetPowerFunc();
            GetExpFunc();
            MyModel.Series.Add(linearFunction);
            MyModel.Series.Add(powerFunction);
            MyModel.Series.Add(expFunction);
            MyModel.Series.Add(quadricFunction);

            MyModel.Legends.Add(new Legend()
            {
                LegendTitle = "Legend",
                LegendPosition = LegendPosition.RightBottom,
                LegendSize = new OxyPlot.OxySize(2, 1)
            });

            for (int i = 0; i < inputX.Length; i++)
            {
                points.Points.Add(new OxyPlot.DataPoint(inputX[i], inputY[i]));
            }
            MyModel.Series.Add(points);
        }
    }
}
