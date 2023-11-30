using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph.VM.Exercise3
{
    public class Exercise3VM:ViewModel
    {
        public DataTable Table { get; set; }
        public void InitTable(IEnumerable<IEnumerable<double>> list)
        {
            Table = new DataTable();
            Table.Columns.Add(new DataColumn("Имя метода"));
            for(int i=0; i<6; i++)
            {
                Table.Columns.Add(new DataColumn($"{i+1}"));
            }
            foreach(IEnumerable<double> itemList in list)
            {
                var row = Table.NewRow();
                int i = 1;
                foreach(double item in itemList)
                {
                    row[i] = item;
                    i++;
                }
                Table.Rows.Add(row);
            }
        }
        public Exercise3VM()
        {
            IEnumerable<IEnumerable<double>> list = Graph.Models.Exercise3Model.GetMatrix();
            InitTable(list);
        }
    }
}
