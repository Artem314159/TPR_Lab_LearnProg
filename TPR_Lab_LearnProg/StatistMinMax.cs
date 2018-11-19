using System;
using System.Linq;

namespace TPR_Lab_LearnProg
{
    public class StatistMinMaxCriterionTask
    {
        private readonly int m, n;
        private double[,] MatrQ;
        private double[,] MatrZ;

        public double[,] GetMatrQ
        {
            get
            {
                double[,] copyQ = new double[MatrQ.GetLength(0), MatrQ.GetLength(1)];
                Array.Copy(MatrQ, copyQ, MatrQ.Length);
                return copyQ;
            }
        }
        public double[,] GetMatrZ
        {
            get
            {
                double[,] copyL = new double[MatrZ.GetLength(0), MatrZ.GetLength(1)];
                Array.Copy(MatrZ, copyL, MatrZ.Length);
                return copyL;
            }
        }

        public StatistMinMaxCriterionTask(double[,] matrQ, double[,] matrZ)
        {
            if (matrQ != null && matrZ != null)
            {
                m = matrQ.GetLength(0);
                n = matrQ.GetLength(1);
                MatrQ = new double[m, n];
                MatrZ = new double[matrZ.GetLength(0), n];
                Array.Copy(matrQ, MatrQ, matrQ.Length);
                Array.Copy(matrZ, MatrZ, matrZ.Length);
            }
        }

        public void Calculate()
        {
            // Matrix of generalized losses
            double[,] matrI = StatistMinMax.CreateMatrI(MinMax.CreateMatrL(MatrQ), MatrZ);

        }
    }

    /// <summary>
    /// Statistical MinMax criterion
    /// </summary>
    public static class StatistMinMax
    {
        public static bool ZIsCorrect(double[,] matrZ)
        {
            if (matrZ == null) throw new ArgumentNullException("MatrZ");

            bool flag = true;
            int m = matrZ.GetLength(0);
            int n = matrZ.GetLength(1);

            for (int j = 0; j < n; j++)
            {
                double checker = 0;
                for (int i = 0; i < m; i++)
                {
                    checker += matrZ[i, j];
                }
                if (Math.Abs(checker - 1) > 0.0001) flag = false;
            }
            return flag;
        }

        /// <summary>
        /// Create matrix of generalized losses
        /// </summary>
        /// <param name="matrL">Matrix of losses [m x n]</param>
        /// <param name="matrZ">Matrix of probabilities of nature [r x n]</param>
        /// <returns></returns>
        public static double[,] CreateMatrI(double[,] matrL, double[,] matrZ)
        {
            if (matrL == null)
                throw new ArgumentNullException("MatrL");
            if (matrZ == null)
                throw new ArgumentNullException("MatrZ");
            // L always must have the minimum element equal to zero
            if (matrL.Cast<double>().Min() != 0)
                throw new ArgumentException("MatrL isn't correct.", "MatrL");
            if (matrL.GetLength(1) != matrZ.GetLength(1) || !ZIsCorrect(matrZ))
                throw new ArgumentException("MatrZ isn't correct.", "MatrZ");

            int m = matrL.GetLength(0), n = matrL.GetLength(1), r = matrZ.GetLength(0);
            // Count of statistical decision functions
            int M = (int)Math.Pow(m, r);

            // Matrix of generalized losses
            double[,] matrI = new double[M, n];

            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrI[i, j] = 0;
                    byte[] g = GetStatistDecisionFunc(r, m, i);
                    //         r-1
                    //I[i, j]=  Σ  (Z[k, j] * L[g[k], j])
                    //         k=0
                    for (int k = 0; k < r; k++)
                    {
                        matrI[i, j] += matrZ[k, j] * matrL[g[k], j];
                    }
                }
            }
            return matrI;
        }

        private static byte[] GetStatistDecisionFunc(int r, int m, int M)
        {
            byte[] g = new byte[r];
            for (int i = 0; i < r; i++)
            {
                g[i] = (byte)(M % m);
                M /= m;
            }
            Array.Reverse(g);
            return g;
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
        public static double[,] CreateMatrL(double[,] matrQ)
        {
            int m = matrQ.GetLength(0), n = matrQ.GetLength(1);
            double max = matrQ[0, 0];

            //Matrix of losses
            double[,] matrL = new double[matrQ.GetLength(0), matrQ.GetLength(1)];
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
