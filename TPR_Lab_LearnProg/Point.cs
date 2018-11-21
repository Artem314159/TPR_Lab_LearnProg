using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPR_Lab_LearnProg
{
    public struct Point : IEquatable<Point>
    {
        public double x;
        public double y;

        public Point(double x1, double y1)
        {
            x = x1;
            y = y1;
        }

        public bool Equals(Point other)
        {
            return this.x == other.x && this.y == other.y;
        }

        public static bool operator ==(Point left, Point right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Point left, Point right)
        {
            return !left.Equals(right);
        }
    }
}
