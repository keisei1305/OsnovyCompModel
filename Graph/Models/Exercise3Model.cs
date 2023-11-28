using System;
using System.Collections.Generic;
using Graph.Services;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Models
{
    public class Exercise3Model
    {
        static private double GetMiddleNum(double p)
        {
            var s = p.ToString();
            if (s.Length < 8)
            {
                while (s.Length != 10)
                {
                    s += "0";
                }
            }
            s = s.Substring(4,4);
            return Convert.ToDouble(s);
        }
        static private IEnumerable<double> Neimon(double p, int n)
        {
            double previousNum = p;
            yield return previousNum;
            for(int i=0; i<n; i++)
            {
                previousNum = GetMiddleNum(Math.Pow(previousNum,2))/10000;
                yield return previousNum;
            }
        }

        static private IEnumerable<double> ModifiedNeimon(double p1,double p2, int n)
        {
            double result;
            for(int i=0; i <= n; i++)
            {
                result = GetMiddleNum(p1 * p2)/10000;
                yield return result;
                p1 = p2;
                p2 = result;
            }
        }

        static private IEnumerable<double> Lemer(double p,double g, int n)
        {
            double result = p;
            for(int i=0; i<n; i++)
            {
                result = g * result % 1;
                yield return Math.Round(result,4);
            }
        }

        static private IEnumerable<double> CongruantMultLemer(int a, int m, int p,int n)
        {
            double result = p;
            for(int i=0; i<n; i++)
            {
                result = a * result % m;
                yield return Math.Round(result / m,4);
            }
        }

        static public IEnumerable<IEnumerable<double>> GetMatrix()
        {
            yield return Neimon(0.5830, 6);
            yield return ModifiedNeimon(0.5836, 0.2176, 6);
            yield return Lemer(0.585, 927, 5);
            yield return CongruantMultLemer(265, 129, 122, 4);
        }
    }
}
