using System;

namespace TPR_Lab_LearnProg
{
    public struct Point : IEquatable<Point>
    {
        public readonly double x;
        public readonly double y;

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

        public override string ToString()
        {
            return $"Point: {{X = {x}; Y = {y}}}";
        }
    }
}
