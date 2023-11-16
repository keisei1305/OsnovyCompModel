using Graph.VM.Exercise1;

namespace Graph.Infrustructure.Locators
{
    internal class LocatorEx1
    {
        public Exercise1VM Ex1VM { get; set; }
        public CheckAccuracyVM CheckAccuracyVM { get; set; }

        public LocatorEx1()
        {
            var ModelEx1 = new Graph.Models.Exercise1Model();
            Ex1VM = new Exercise1VM(ModelEx1.MyModel);
            CheckAccuracyVM = new CheckAccuracyVM(ModelEx1.InputX,ModelEx1.InputY,
                ModelEx1.Functions,ModelEx1.FunctionNames);
        }
    }
}
