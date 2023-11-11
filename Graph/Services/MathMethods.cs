using System;
using Graph.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Services
{
    public class MathMethods
    {
        public static double[] MethodKramera(Matrix system)
        {
            var matrix = new Matrix(system.Row,system.Col-1);
            var freeCoef = new double[system.Row];
            for(int i=0; i < matrix.Row; i++)
            {
                for(int j=0; j<matrix.Col; j++)
                {
                    matrix[i, j] = system[i, j];
                }
                freeCoef[i] = system[i, system.Col - 1];
            }
            double mainDet = Matrix.Determ(matrix);
            var result = new double[matrix.Col];
            for(int i=0; i<matrix.Col; i++)
            {
                for(int j=0; j<matrix.Col; j++)
                {
                    double temp = freeCoef[j];
                    freeCoef[j] = matrix[i, j];
                    matrix[i, j] = temp;
                }
                result[i] = Matrix.Determ(matrix)/mainDet;
                for (int j = 0; j < matrix.Col; j++)
                {
                    double temp = freeCoef[j];
                    freeCoef[j] = matrix[i, j];
                    matrix[i, j] = temp;
                }
            }
            return result;
        }
    }
}
