using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Graph.VM;
using Graph.Models;
using OxyPlot;
using Graph.Infrustructure.Commands;

namespace Graph.VM.Exercise2
{
    public class Exercise2VM : ViewModel
    {
        List<PlotModel> models;
        PlotModel _currentModel;
        public PlotModel CurrentModel
        {
            get => _currentModel;
            set
            {
                _currentModel = value;
                OnPropertyChanged();
            }
        }
        byte currentIndex = 0;
        string _absoluteError;
        string _relativeError;
        short _pointsQuantity;
        public string RelativeError
        {
            get => _relativeError;
            set
            {
                _relativeError = value;
                OnPropertyChanged();
            }
        }
        public string AbsoluteError
        {
            get => _absoluteError;
            set
            {
                _absoluteError = value;
                OnPropertyChanged();
            }
        }
        public short PointsQuantity
        {
            get => _pointsQuantity;
            set
            {
                _pointsQuantity = value;
                OnPropertyChanged();
            }
        }

        public ICommand NextModelCommand { get; }
        private bool CanNextModelCommandExecute(object p) => currentIndex < 3;
        private void OnNextModelCommandExecuted(object p)
        {
            currentIndex++;
            CurrentModel = models[currentIndex];
        }

        public ICommand PreviousModelCommand { get; }
        private bool CanPreviousModelCommandExecute(object p) => currentIndex > 0;
        private void OnPreviousModelCommandExecuted(object p)
        {
            currentIndex--;
            CurrentModel = models[currentIndex];
        }

        public Exercise2VM()
        {
            NextModelCommand = new LambdaCommand(OnNextModelCommandExecuted, CanNextModelCommandExecute);
            PreviousModelCommand = new LambdaCommand(OnPreviousModelCommandExecuted, CanPreviousModelCommandExecute);
            models = Exercise2Model.GetModels();
            CurrentModel = models[0];
        }
    }
}
