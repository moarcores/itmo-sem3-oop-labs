using System;

namespace Lab5_Serialization
{
    [Serializable]
    public class Dot
    {
        public double X { get; set; }
        public double Y { get; set; }

        public override string ToString()
        {
            return "X - " + this.X + " Y - " + this.Y;
        }

        public Dot(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public Dot() {}
    }
} 