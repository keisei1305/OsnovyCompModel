using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Models
{
    public class Exercise2Model
    {
        static public List<PlotModel> GetModels()
        {
            var models = new List<PlotModel>();

            for (int i = 0; i < 4; i++)
            {
                models.Add(new PlotModel { Title = $"{i}" });
            }


            return models;
        }
    }
}
