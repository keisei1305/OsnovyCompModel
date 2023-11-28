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
        List<string> messages;
        PlotModel _currentModel;
        string _currentMessage;
        public PlotModel CurrentModel
        {
            get => _currentModel;
            set
            {
                _currentModel = value;
                OnPropertyChanged();
            }
        }
        public string CurrentMessage
        {
            get => _currentMessage;
            set
            {
                _currentMessage = value;
                OnPropertyChanged();
            }
        }
        byte currentIndex = 0;

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
            models = Exercise2Model.GetModels().Item1;
            CurrentModel = models[0];
        }
    }
}
