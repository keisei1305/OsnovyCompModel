using System;
using System.Linq;
using OxyPlot;
using OxyPlot.Series;
using Graph.VM.Exercise1;
using System.Collections.Generic;
using Graph.Services;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    class Exercise1
    {
        private readonly byte parameterAccuracy = 2;
        private readonly byte tempAccuracy = 4;
        private readonly double[] inputX = { 2, 4, 6, 8, 10, 12 };
        private readonly double[] inputY = { 2.4, 2.9, 3.0, 3.5, 3.6, 3.7 };
        private readonly int size = 6;

        private FunctionSeries linearFunction;
        private FunctionSeries powerFunction;
        private FunctionSeries expFunction;
        private FunctionSeries quadricFunction;

        public PlotModel MyModel { get; private set; }

        private Matrix MakeSystem(double[] listX, double[] listY, int dim)
        {
            int n = listX.Length;
            Matrix matrix = new Matrix(dim,dim+1);
            for(int i=0; i<dim; i++)
            {
                for(int j=0; j<dim; j++)
                {
                    double sumA = 0;
                    for(int k=0; k<n; k++)
                    {
                        sumA += Math.Pow(listX[k], i+j);
                    }
                    matrix[i, j] = sumA;
                }
                double sumB = 0;
                for(int j=0; j<n; j++)
                {
                    sumB += listY[j] * Math.Pow(listX[j], i);
                }
                matrix[i, dim] = sumB;
            }
            return matrix;
        }

        private void getLinearFunc()
        {
            var coefs = MathMethods.MethodKramera(MakeSystem(inputX, inputY, 2));
            coefs[0] = Math.Round(coefs[0], tempAccuracy);
            coefs[1] = Math.Round(coefs[1], tempAccuracy);
            linearFunction = new FunctionSeries(x => coefs[1] * x + coefs[0], inputX[0], inputX[size-1], 0.1, $"y={coefs[1]}x+{coefs[0]}");
        }

        private void getPowerFunc()
        {
            var tempX = inputX.Select(x => Math.Log(x)).ToArray();
            var tempY = inputY.Select(y => Math.Log(y)).ToArray();
            var coefs = MathMethods.MethodKramera(MakeSystem(tempX, tempY, 2));
            coefs[0] = Math.Round(Math.Exp(coefs[0]), tempAccuracy);
            coefs[1] = Math.Round(coefs[1], tempAccuracy);
            powerFunction = new FunctionSeries(x => Math.Pow(x,coefs[1])*coefs[0], inputX[0], inputX[size - 1], 0.1, $"y=x^{coefs[1]}*{coefs[0]}");
        }

        private void getExpFunc()
        {
            var tempY = inputY.Select(y => Math.Log(y)).ToArray();
            var coefs = MathMethods.MethodKramera(MakeSystem(inputX, tempY, 2));
            coefs[0] = Math.Round(Math.Exp(coefs[0]), tempAccuracy);
            coefs[1] = Math.Round(coefs[1], tempAccuracy);
            expFunction = new FunctionSeries(x => Math.Exp(x*coefs[1]) * coefs[0], inputX[0], inputX[size - 1], 0.1, $"y={coefs[0]}*e^({coefs[1]}*x)");
        }

        private void getQuadriacFunc()
        {
            var coefs = MathMethods.MethodKramera(MakeSystem(inputX, inputY, 3));
            coefs[0] = Math.Round(coefs[0], tempAccuracy);
            coefs[1] = Math.Round(coefs[1], tempAccuracy);
            coefs[2] = Math.Round(coefs[2], tempAccuracy);
            quadricFunction = new FunctionSeries(x => coefs[2]*Math.Pow(x,2)+coefs[1] * x + coefs[0], inputX[0], inputX[size - 1], 0.1, $"y={coefs[2]}*x^2+{coefs[1]}x+{coefs[0]}");
        }

        public Exercise1()
        {
            var coefs = MathMethods.MethodKramera(MakeSystem(inputX, inputY, 2));
            MyModel = new PlotModel { Title = "Апроксимация экспериментальных данных методом наименьших квадратов" };
            var points = new LineSeries
            {
                Color = OxyColors.Red,
                MarkerStroke = OxyColors.Red,
                MarkerType = MarkerType.Circle,
                StrokeThickness = 0,
                MarkerSize = 4,
                Title="Экспереминтальные точки"
            };
            getQuadriacFunc();
            getLinearFunc();
            getPowerFunc();
            getExpFunc();
            MyModel.Series.Add(linearFunction);
            MyModel.Series.Add(powerFunction);
            MyModel.Series.Add(expFunction);
            MyModel.Series.Add(quadricFunction);
            for(int i=0; i<inputX.Length; i++)
            {
                points.Points.Add(new OxyPlot.DataPoint(inputX[i], inputY[i]));
            }
            MyModel.Series.Add(points);
        }
    }
}
