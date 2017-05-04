using System;
using System.Collections.Generic;
using System.Text;

namespace brianparks.Cherwell
{
    /* NOTE: Data structures minimally implemented to support Matrix.cs */
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point() {}
        public Point(int x, int y) { X = x; Y = y; }
        public override string ToString()
        {
            return String.Format("({0}, {1})", X, Y);
        }
        public static bool operator== (Point self, Point other)
        {
            return self.X == other.X && self.Y == other.Y;
        }
        public static bool operator!= (Point self, Point other)
        {
            return self.X != other.X || self.Y != other.Y;
        }
    }

    public class Face
    {
        private List<Point> _points = new List<Point>();
        public List<Point> Vertices { get { return _points; } }
        public void Add(Point point) { _points.Add(point); }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{ ");
            foreach (var pt in _points)
            {
                sb.Append(pt);
                sb.Append(" ");
            }
            sb.Append("}");
            return sb.ToString();
        }
    }
}
