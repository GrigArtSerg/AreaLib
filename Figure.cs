using System;
using System.Runtime;
using System.Collections.Generic;

namespace AreaLib
{
    public class Figure
    {
        readonly List<double> FigureSides;
        readonly bool IsRightTriangle;

        /// <summary>
        /// Конструтор создания фигуры
        /// </summary>
        /// <param name="sides">массив со значениями сторон</param>
        public Figure(double[] sides)
        {
            ValuesCheck(sides);

            if (sides.Length == 3)
            {
                double perimeter = Perimeter(sides);
                double maxSide = Math.Max(sides[0], Math.Max(sides[1], sides[2]));
                if (perimeter - maxSide - maxSide < Staff.Accuracy)
                {
                    throw new Exception("Наибольшая сторона треугольника должна быть меньше суммы других сторон");
                }

                IsRightTriangle = GetIsRightTriangle(sides, maxSide);
            }

            foreach (double side in sides)
            {
                FigureSides.Add(side);
            }
        }

        public double GetArea(List<double> FigureSides)
        {
            double Area = 0;

            switch (FigureSides.Count)
            {
                case 1:
                    return GetTriangleArea(FigureSides);
                case 3:

                    return Area;
                default:
                    throw new Exception($"Не описано метода для вчисления площади фигуры" +
                    $"с данным количеством сторон {FigureSides.Count}");
            }
        }

        private double GetTriangleArea(List<double> figureSides)
        {
            double Area;
            Area = Math.Sqrt(Perimeter(figureSides)*)
            return Area;
        }

        private void ValuesCheck(double[] sides)
        {
            for (int i = 0; i < sides.Length; i++)
            {
                if (sides[i] < Staff.Accuracy) throw new Exception($"Неверно указана сторона с индексом {i}");
            }
        }

      
        private bool GetIsRightTriangle(double[] sides, double maxSide)
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

        public double Perimeter(List<double> sides)
        {
            double perimeter = 0;
            foreach (double side in sides) perimeter += side;
            return perimeter;
        }
    }
}
