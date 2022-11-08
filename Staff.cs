using System;
using System.Collections.Generic;
using System.Text;

namespace AreaLib
{
    class Staff
    {
        public const double Accuracy = 0.0000001;

        public void ValuesCheck(double[] sides)
        {
            for (int i = 0; i < sides.Length; i++)
            {
                if (sides[i] < Staff.Accuracy) throw new Exception($"Неверно указана сторона с индексом {i}.");
            }
        }

        public double GetMaxSide(double[] sides)
        {
            double[] partSides = new double[sides.Length - 1];

            for (int i = 1; i < sides.Length - 1; i++)
            {
                partSides[i - 1] = sides[i];
            }

            return Math.Max(sides[0], GetMaxSide(partSides));
        }

        public void TriangleIsTriangle(double[] sides)
        {
            double perimeter = Perimeter(sides);
            double maxSide = GetMaxSide(sides);
            if (perimeter - maxSide - maxSide < Staff.Accuracy)
            {
                throw new Exception("Наибольшая сторона треугольника должна быть меньше суммы других сторон.");
            }
        }

        public bool SetIsRightTriangle(double[] sides, double maxSide)
        {
            double a, b;

            if (sides[0] == maxSide)
            {
                a = sides[1];
                b = sides[2];
            }
            else if (sides[1] == maxSide)
            {
                a = sides[0];
                b = sides[2];
            }
            else
            {
                a = sides[0];
                b = sides[1];
            }

            return Math.Abs(Math.Pow(maxSide, 2) - Math.Pow(a, 2) - Math.Pow(b, 2)) < Staff.Accuracy;
        }

        public double Perimeter(double[] sides)
        {
            double perimeter = 0;
            foreach (double side in sides) perimeter += side;
            return perimeter;
        }
    }
}
