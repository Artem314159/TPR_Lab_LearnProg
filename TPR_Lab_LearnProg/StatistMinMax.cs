using System;
using System.Collections.Generic;
using System.Linq;

namespace TPR_Lab_LearnProg
{
    public class StatistMinMaxCriterionTask
    {
        #region Private fields
        private readonly int m, n;
        private double[,] MatrQ;
        private double[,] MatrL;
        private double[,] MatrZ;
        private List<Point> MatrI;
        #endregion

        #region Matrix getters
        public double[,] GetMatrQ
        {
            get { return CopyMatr(MatrQ); }
        }
        public double[,] GetMatrL
        {
            get
            {
                if(MatrL == null)
                    MatrL = MinMax.CreateMatrL(GetMatrQ);
                return CopyMatr(MatrL);
            }
        }
        public double[,] GetMatrZ
        {
            get { return CopyMatr(MatrZ); }
        }
        public List<Point> GetMatrI
        {
            get
            {
                if (MatrI == null)
                {
                    // Matrix of generalized losses
                    double[,] matrI = StatistMinMax.CreateMatrI(GetMatrL, GetMatrZ);
                    MatrI = Matr2DToPointList(matrI);
                }
                return MatrI.ToList();
            }
        }

        private double[,] CopyMatr(double[,] Matr)
        {
            double[,] copy = new double[Matr.GetLength(0), Matr.GetLength(1)];
            Array.Copy(Matr, copy, Matr.Length);
            return copy;
        }
        #endregion
        
        public StatistMinMaxCriterionTask(double[,] matrQ, double[,] matrZ)
        {
            if (matrQ == null || matrQ.Length == 0)
                throw new ArgumentNullException("matrQ");
            else if (matrZ == null || matrZ.Length == 0)
                throw new ArgumentNullException("matrZ");
            else
            {
                m = matrQ.GetLength(0);
                n = matrQ.GetLength(1);
                MatrQ = new double[m, n];
                MatrZ = new double[matrZ.GetLength(0), n];
                Array.Copy(matrQ, MatrQ, matrQ.Length);
                Array.Copy(matrZ, MatrZ, matrZ.Length);
            }
        }

        public StatistMinMaxCriterionTask()
        {
            n = 2;  m = 1;
            MatrQ = new double[,] { { 0, 0 } };
            MatrZ = new double[,] { { 1, 1 } };
        }

        private List<Point> Matr2DToPointList(double[,] Matr)
        {
            if (Matr == null) return null;
            if (Matr.GetLength(1) != 2)
                throw new ArgumentException("Matr hasn't 2 dimensions.", "Matr");
            List<Point> result = new List<Point>();
            int l = Matr.GetLength(0);
            for (int i = 0; i < l; i++)
            {
                result.Add(new Point(Matr[i, 0], Matr[i, 1]));
            }
            return result;
        }

        public List<Point> CalcConvexHull()
        {
            List<Point> ConvexHull = JarvisMarch.JarvisMarch2D(GetMatrI);
            ConvexHull.Add(ConvexHull[0]);
            return ConvexHull;
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
            if (matrL == null || matrZ == null) return null;

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
                    matrI[i, j] = Math.Round(matrI[i, j], 3);
                }
            }
            return matrI;
        }

        public static byte[] GetStatistDecisionFunc(int r, int m, int M)
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
            if (matrQ == null)
                return null;
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

        public static double[] GetSolution(List<Point> convexHull, out Point point)
        {
            int endInd = convexHull.IndexOf(convexHull.Where(p => p.Y == convexHull.Min(_ => _.Y)).First());
            double[] pointsValues = new double[endInd + 1];
            double[] segmentValues = new double[endInd];
            for (int i = 0; i < endInd + 1; i++)
            {
                pointsValues[i] = 0.5 * (convexHull[i].X + convexHull[i].Y + Math.Abs(convexHull[i].X - convexHull[i].Y));
            }
            double[] pList = new double[endInd];
            for (int i = 0; i < endInd; i++)
            {
                pList[i] = (convexHull[i + 1].Y - convexHull[i + 1].X) /
                    (convexHull[i].X - convexHull[i].Y - convexHull[i + 1].X + convexHull[i + 1].Y);
                if (pList[i] >= 0 && pList[i] <= 1)
                {
                    segmentValues[i] = convexHull[i].X * pList[i] + convexHull[i + 1].X * (1 - pList[i]);
                }
                else
                {
                    segmentValues[i] = 1d / 0;
                }
            }
            if (pointsValues.Min() <= segmentValues.Min())
            {
                point = new Point(Math.Round(pointsValues.Min(), 3), Math.Round(pointsValues.Min(), 3));
                double[] res = new double[endInd + 1];
                int ind = Array.FindIndex(pointsValues, _ => _ == pointsValues.Min());
                res[ind] = 1;
                return res;
            }
            else
            {
                point = new Point(Math.Round(segmentValues.Min(), 3), Math.Round(segmentValues.Min(), 3));
                double[] res = new double[endInd + 1];
                int ind = Array.FindIndex(segmentValues, _ => _ == segmentValues.Min());
                res[ind] = pList[ind];
                res[ind + 1] = 1 - pList[ind];
                return res;
            }
        }
    }

    public static class JarvisMarch
    {
        public static List<Point> JarvisMarch2D(List<Point> startPoints)
        {
            if (startPoints == null)
                throw new ArgumentNullException("startPoints");
            if (startPoints.Count <=3)
                return startPoints;

            List<Point> ConvexHull = new List<Point>();

            // Get leftmost point
            Point LeftMostPoint = startPoints.Where(p => p.X == startPoints.Min(min => min.X)).First();
            Point EndPoint;

            do
            {
                ConvexHull.Add(LeftMostPoint);
                EndPoint = startPoints[0];

                for (int i = 1; i < startPoints.Count; i++)
                {
                    if ((LeftMostPoint == EndPoint) || (FindRotation(LeftMostPoint, EndPoint, startPoints[i]) < 0))
                        EndPoint = startPoints[i];
                }

                LeftMostPoint = EndPoint;

            }
            while (EndPoint != ConvexHull[0]);

            return ConvexHull;
        }

        /// <summary>
        /// Function defines from what party from a vector AB there is a point C
        /// </summary>
        /// <param name="A">Initial point</param>
        /// <param name="B">Terminal point</param>
        /// <param name="C">Third point</param>
        /// <returns>The positive returned value corresponds to a left-hand side, 
        /// the negative — right.</returns>
        public static double FindRotation(Point A, Point B, Point C)
        {
            return (B.X - A.X) * (C.Y - B.Y) - (B.Y - A.Y) * (C.X - B.X);
        }
    }
}
