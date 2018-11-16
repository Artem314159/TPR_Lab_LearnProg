using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPR_Lab_LearnProg
{
    /// <summary>
    /// Statistical MinMax criterion
    /// </summary>
    public class StatistMinMax
    {
        public readonly int m, n;
        public Number[,] MatrQ { get; private set; }
        public Number[,] MatrL { get; private set; }

        public StatistMinMax(Number[,] matrQ)
        {
            if (matrQ != null)
            {
                m = matrQ.GetLength(0);
                n = matrQ.GetLength(1);
                MatrQ = new Number[m, n];
                Array.Copy(matrQ, MatrQ, matrQ.Length);
            }
        }

        public bool ZIsCorrect(Number[,] matrZ)
        {
            bool flag = true;
            for (int j = 0; j < n; j++)
            {
                Number checker = 0;
                for (int i = 0; i < m; i++)
                {
                    checker += matrZ[i, j];
                }
            }
            return flag;
        }

        /// <summary>
        /// Matrix of generalized losses
        /// </summary>
        /// <typeparam name="T">IComparable Type</typeparam>
        /// <param name="matrL">Matrix of losses</param>
        /// <param name="matrZ">Matrix of probabilities of nature</param>
        /// <returns></returns>
        public Number[,] CreateMatrI(Number[,] matrL, Number[,] matrZ)
        {
            if()
            //Matrix of losses
            MatrL = MinMax.CreateMatrL(MatrQ);

            //Count of statistical decision functions
            int M = (int)Math.Pow(m, matrZ.GetLength(0));
        }
    }

    /// <summary>
    /// MinMax criterion
    /// </summary>
    public static class MinMax
    {
        /// <summary>
        /// Creatig of matrix of losses
        /// </summary>
        /// <param name="matrQ">Matrix of subjective outcomes</param>
        /// <returns></returns>
        public static Number[,] CreateMatrL(Number[,] matrQ)
        {
            int m = matrQ.GetLength(0), n = matrQ.GetLength(1);
            Number max = matrQ[0, 0];

            //Matrix of losses
            Number[,] matrL = new Number[matrQ.GetLength(0), matrQ.GetLength(1)];
            foreach (var item in matrQ)
            {
                if (max < item)
                    max = item;
            }
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrL[i, j] = max - matrQ[i, j];
                }
            }
            return matrL;
        }
    }
}
