using Graph.Infrustructure.Commands;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Windows.Input;

namespace Graph.VM.Exercise1
{
    public class Exercise1VM:ViewModel
    {
        private PlotModel _myModel;
        private bool linearIsHidden = true;
        private bool powerIsHidden = true;
        private bool expIsHidden = true;
        private bool quadricIsHidden = true;

        private FunctionSeries linearFunction;
        private FunctionSeries powerFunction;
        private FunctionSeries expFunction;
        private FunctionSeries quadricFunction;

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
            var wnd = new Graph.Views.CheckAccuracyWindow();
            wnd.Show();
        }
        #endregion

        public Exercise1VM(PlotModel plotModel)
        {
            ShowLinearCommand = new LambdaCommand(OnShowLinearCommandExecuted, CanShowLinearCommandExecute);
            ShowPowerCommand = new LambdaCommand(OnShowPowerCommandExecuted, CanShowPowerCommandExecute);
            ShowExpCommand = new LambdaCommand(OnShowExpCommandExecuted, CanShowExpCommandExecute);
            ShowQuadricCommand = new LambdaCommand(OnShowQuadricCommandExecuted, CanShowQuadricCommandExecute);
            UpdateModel = new LambdaCommand(OnUpdateModelExecuted, CanUpdateModelExecute);
            OpenData = new LambdaCommand(OnOpenDataExecuted, CanOpenDataExecute);

            MyModel = plotModel;
            linearFunction = (FunctionSeries)MyModel.Series[0];
            powerFunction = (FunctionSeries)MyModel.Series[1];
            expFunction = (FunctionSeries)MyModel.Series[2];
            quadricFunction = (FunctionSeries)MyModel.Series[3];
        }
    }
}
