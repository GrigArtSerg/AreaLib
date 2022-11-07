using System;
using System.Runtime;
using System.Collections.Generic;

namespace AreaLib
{
    public class Figure
    {
        readonly List<double> FigureSides;
        readonly int FigureSidesCount;
        readonly bool IsRightTriangle;

        /// <summary>
        /// Конструтор создания фигуры
        /// </summary>
        /// <param name="sides">массив со значениями сторон</param>
        public Figure(double[] sides)
        {
            if (sides.Length == 0) throw new Exception("Фигура должна быть описана хотя бы одним параметром.");

            ValuesCheck(sides);

            FigureSidesCount = sides.Length;

            switch (FigureSidesCount)
            {
                case 1:
                    // NOTE какая предварительная обработка требуется кругу?
                    break;
                /// проверка сторон треугольника
                case 3:
                    double perimeter = Perimeter(sides);
                    // TODO ? заменить поиск наибольшего элемента сортировкой массива и обращением к первому элементу
                    double maxSide = GetMaxSide(sides);
                    if (perimeter - maxSide - maxSide < Staff.Accuracy)
                    {
                        throw new Exception("Наибольшая сторона треугольника должна быть меньше суммы других сторон.");
                    }

                    IsRightTriangle = GetIsRightTriangle(sides, maxSide);
                    break;
                default:
                    break;
            }

            foreach (double side in sides)
            {
                FigureSides.Add(side);
            }
        }

        #region Внутренние методы для рассчета площади
        public double GetArea()
        {
            switch (FigureSides.Count)
            {
                case 1:
                    return GetCircleArea();
                case 3:
                    return GetTriangleArea();
                default:
                    throw new Exception($"Не описано метода для вчисления площади фигуры" +
                    $"с данным количеством сторон {FigureSides.Count}.");
            }
        }

        private double GetCircleArea()
        {
            double Area;
            Area = Math.PI * Math.Pow(FigureSides[0], 2);
            return Area;
        }

        private double GetTriangleArea()
        {
            double Area;

            if (IsRightTriangle)
            {
                Area = GetRightTriangleArea();
            }
            else
            {
                double SemiPerimeter = (Perimeter(FigureSides) / 2);
                Area = Math.Sqrt(SemiPerimeter * (SemiPerimeter - FigureSides[0])
                                                * (SemiPerimeter - FigureSides[1])
                                                * (SemiPerimeter - FigureSides[2]));
            }
            return Area;
        }

        private double GetRightTriangleArea()
        {
            double Area = 0;
            // TODO внутренняя функция площади прямоугольного треугольника (полупериметр в аргументы?)
            GetMaxSide(FigureSides);
            return Area;
        }
        #endregion

        #region Внешние методы для рассчета площади
        public double GetArea(double[] sides)
        {
            switch (sides.Length)
            {
                case 1:
                    return GetCircleArea(sides);
                case 3:
                    return GetTriangleArea(sides);
                default:
                    throw new Exception($"Не описано метода для вчисления площади фигуры " +
                                        $"с данным количеством сторон {FigureSides.Count}.");
            }
        }

        public double GetCircleArea(double[] sides)
        {
            // TODO вариант площали круга без создания фигуры
            double Area;
            Area = Math.PI * Math.Pow(FigureSides[0], 2);
            return Area;
        }

        public double GetTriangleArea(double[] sides)
        {
            // TODO вариант поиска площади треугольника без создания фигуры
            double Area;
            double SemiPerimeter = (Perimeter(FigureSides) / 2);
            
            GetIsRightTriangle(sides, GetMaxSide(sides));

            if (IsRightTriangle)
            {
                Area = GetRightTriangleArea(sides);
            }
            else Area = Math.Sqrt(SemiPerimeter * (SemiPerimeter - FigureSides[0])
                                                * (SemiPerimeter - FigureSides[1])
                                                * (SemiPerimeter - FigureSides[2]));
            return Area;
        }
        private double GetRightTriangleArea(double[] sides)
        {
            double Area = 0;
            // TODO внешняя функция площади прямоугольного треугольника
            return Area;
        }
        #endregion

        private void ValuesCheck(double[] sides)
        {
            for (int i = 0; i < sides.Length; i++)
            {
                if (sides[i] < Staff.Accuracy) throw new Exception($"Неверно указана сторона с индексом {i}.");
            }
        }

        private double GetMaxSide(double[] sides)
        {
            double[] partSides = new double[sides.Length - 1];
            
            for (int i = 1; i < sides.Length-1; i++)
            {
                partSides[i - 1] = sides[i];
            }

            return Math.Max(sides[0], GetMaxSide(partSides));
        }

        private double GetMaxSide(List<double> sides)
        {
            List<double> partSides = new List<double>();

            for (int i = 1; i < sides.Count - 1; i++)
            {
                partSides.Add(sides[i]);
            }

            return Math.Max(sides[0], GetMaxSide(partSides));
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

        # region perimeters
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
        #endregion
    }
}
