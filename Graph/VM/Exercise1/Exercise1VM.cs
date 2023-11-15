using System;
using Graph.Infrustructure.Commands;
using Graph.VM;
using System.Linq;
using OxyPlot;
using OxyPlot.Series;
using Graph.Services;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using OxyPlot.Legends;

namespace Graph
{
    internal class Exercise1VM:ViewModel
    {
        private readonly byte parameterAccuracy = 2;
        private readonly byte tempAccuracy = 4;
        private readonly double[] inputX = { 2, 4, 6, 8, 10, 12 };
        private readonly double[] inputY = { 2.4, 2.9, 3.0, 3.5, 3.6, 3.7 };
        private readonly int size = 6;

        private PlotModel _myModel;
        private bool linearIsHidden = true;
        private bool powerIsHidden = true;
        private bool expIsHidden = true;
        private bool quadricIsHidden = true;

        private FunctionSeries linearFunction;
        private FunctionSeries powerFunction;
        private FunctionSeries expFunction;
        private FunctionSeries quadricFunction;
        private Func<double, double>[] functions = new Func<double, double>[4];
        private string[] functionNames = new string[4];

        public PlotModel MyModel
        {
            get => _myModel;
            private set
            {
                _myModel = value;
                OnPropertyChanged();
            }
        }

        #region Commands
        public ICommand ShowLinearCommand { get; }
        private bool CanShowLinearCommandExecute(object p) => true;
        private void OnShowLinearCommandExecuted(object p)
        {
            if (linearIsHidden)
            {
                MyModel.Series.Remove(linearFunction);
                MyModel.InvalidatePlot(true);
            }
            else
            {
                MyModel.Series.Add(linearFunction);
                MyModel.InvalidatePlot(true);
            }
            linearIsHidden = !linearIsHidden;
        }
        public ICommand ShowPowerCommand { get; }
        private bool CanShowPowerCommandExecute(object p) => true;
        private void OnShowPowerCommandExecuted(object p)
        {
            if (powerIsHidden)
            {
                MyModel.Series.Remove(powerFunction);
                MyModel.InvalidatePlot(true);
            }
            else
            {
                MyModel.Series.Add(powerFunction);
                MyModel.InvalidatePlot(true);
            }
            powerIsHidden = !powerIsHidden;
        }
        public ICommand ShowExpCommand { get; }
        private bool CanShowExpCommandExecute(object p) => true;
        private void OnShowExpCommandExecuted(object p)
        {
            if (expIsHidden)
            {
                MyModel.Series.Remove(expFunction);
                MyModel.InvalidatePlot(true);
            }
            else
            {
                MyModel.Series.Add(expFunction);
                MyModel.InvalidatePlot(true);
            }
            expIsHidden = !expIsHidden;
        }
        public ICommand ShowQuadricCommand { get; }
        private bool CanShowQuadricCommandExecute(object p) => true;
        private void OnShowQuadricCommandExecuted(object p)
        {
            if (quadricIsHidden)
            {
                MyModel.Series.Remove(quadricFunction);
                MyModel.InvalidatePlot(true);
            }
            else
            {
                MyModel.Series.Add(quadricFunction);
                MyModel.InvalidatePlot(true);
            }
            quadricIsHidden = !quadricIsHidden;
        }

        public ICommand UpdateModel { get; }
        private bool CanUpdateModelExecute(object p) => true;
        private void OnUpdateModelExecuted(object p)
        {
            MyModel.ResetAllAxes();
            MyModel.InvalidatePlot(true);
        }

        public ICommand OpenData { get; }
        private bool CanOpenDataExecute(object p) => true;
        private void OnOpenDataExecuted(object p)
        {
            var wnd = new Graph.Views.CheckAccuracyWindow(inputX,inputY,functions,functionNames);
            wnd.Show();
        }
        #endregion

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
                    sumA = Math.Round(sumA, tempAccuracy);
                    matrix[i, j] = sumA;
                }
                double sumB = 0;
                for(int j=0; j<n; j++)
                {
                    sumB += listY[j] * Math.Pow(listX[j], i);
                }
                sumB = Math.Round(sumB, tempAccuracy);
                matrix[i, dim] = sumB;
            }
            return matrix;
        }

        private void getLinearFunc()
        {
            var coefs = MathMethods.MethodKramera(MakeSystem(inputX, inputY, 2));
            coefs[0] = Math.Round(coefs[0], parameterAccuracy);
            coefs[1] = Math.Round(coefs[1], parameterAccuracy);
            functions[0] = x => coefs[1] * x + coefs[0];
            functionNames[0] = $"y={coefs[1]}x+{coefs[0]}";
            linearFunction = new FunctionSeries(functions[0], inputX[0], inputX[size-1], 0.1, functionNames[0]);
            linearFunction.Color = OxyColor.FromRgb(127,0,0);
        }

        private void getPowerFunc()
        {
            var tempX = inputX.Select(x => Math.Log(x)).ToArray();
            var tempY = inputY.Select(y => Math.Log(y)).ToArray();
            var coefs = MathMethods.MethodKramera(MakeSystem(tempX, tempY, 2));
            coefs[0] = Math.Round(Math.Exp(coefs[0]), parameterAccuracy);
            coefs[1] = Math.Round(coefs[1], parameterAccuracy);
            functions[1] = x => Math.Pow(x, coefs[1]) * coefs[0];
            functionNames[1] = $"y=x^{coefs[1]}*{coefs[0]}";
            powerFunction = new FunctionSeries(functions[1], inputX[0], inputX[size - 1], 0.1, functionNames[1]);
            powerFunction.Color = OxyColor.FromRgb(255, 144, 0);
        }

        private void getExpFunc()
        {
            var tempY = inputY.Select(y => Math.Log(y)).ToArray();
            var coefs = MathMethods.MethodKramera(MakeSystem(inputX, tempY, 2));
            coefs[0] = Math.Round(Math.Exp(coefs[0]), parameterAccuracy);
            coefs[1] = Math.Round(coefs[1], parameterAccuracy);
            functions[2] = x => Math.Exp(x * coefs[1]) * coefs[0];
            functionNames[2] = $"y={coefs[0]}*e^({coefs[1]}*x)";
            expFunction = new FunctionSeries(functions[2], inputX[0], inputX[size - 1], 0.1, functionNames[2]);
            expFunction.Color = OxyColor.FromRgb(0, 144, 255);
        }

        private void getQuadriacFunc()
        {
            var coefs = MathMethods.MethodKramera(MakeSystem(inputX, inputY, 3));
            coefs[0] = Math.Round(coefs[0], parameterAccuracy);
            coefs[1] = Math.Round(coefs[1], parameterAccuracy);
            coefs[2] = Math.Round(coefs[2], parameterAccuracy);
            functions[3] = x => coefs[2] * Math.Pow(x, 2) + coefs[1] * x + coefs[0];
            functionNames[3] = $"y={coefs[2]}*x^2+{coefs[1]}x+{coefs[0]}";
            quadricFunction = new FunctionSeries(functions[3], inputX[0], inputX[size - 1], 0.1, functionNames[3]);
            quadricFunction.Color = OxyColor.FromRgb(0, 144, 0);
        }

        public Exercise1()
        {
            ShowLinearCommand = new LambdaCommand(OnShowLinearCommandExecuted,CanShowLinearCommandExecute);
            ShowPowerCommand = new LambdaCommand(OnShowPowerCommandExecuted, CanShowPowerCommandExecute);
            ShowExpCommand = new LambdaCommand(OnShowExpCommandExecuted, CanShowExpCommandExecute);
            ShowQuadricCommand = new LambdaCommand(OnShowQuadricCommandExecuted, CanShowQuadricCommandExecute);
            UpdateModel = new LambdaCommand(OnUpdateModelExecuted, CanUpdateModelExecute);
            OpenData = new LambdaCommand(OnOpenDataExecuted, CanOpenDataExecute);

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

            MyModel.Legends.Add(new Legend()
            {
                LegendTitle = "Legend",
                LegendPosition = LegendPosition.RightBottom,
                LegendSize = new OxyPlot.OxySize(2,1)
            });

            for (int i=0; i<inputX.Length; i++)
            {
                points.Points.Add(new OxyPlot.DataPoint(inputX[i], inputY[i]));
            }
            MyModel.Series.Add(points);
        }
    }
}
