using System;

namespace AreaLib
{
    public class Figure : IFigure
    {
        readonly double[] FigureSides;
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
                    break;
                /// проверка сторон треугольника
                case 3:
                    double perimeter = Perimeter(sides);
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

            FigureSides = sides;
        }

        #region Внутренние методы для рассчета площади
        public double GetArea()
        {
            return FigureSidesCount switch
            {
                1 => GetCircleArea(),
                3 => GetTriangleArea(),
                _ => throw new Exception($"Не описано метода для вчисления площади фигуры" +
                                         $"с данным количеством сторон {FigureSidesCount}."),
            };
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
            double Area;
            double maxSide = GetMaxSide(FigureSides);
            
            if (FigureSides[0] == maxSide) Area = FigureSides[1] * FigureSides[2] / 2 ;
            else if (FigureSides[1] == maxSide) Area = FigureSides[0] * FigureSides[2] / 2;
            else Area = FigureSides[0] * FigureSides[1] / 2;
            
            return Area;
        }
        #endregion

        #region Методы для рассчета площади без создания объекта фигуры
        public double GetArea(double[] sides)
        {
            return sides.Length switch
            {
                1 => GetCircleArea(sides),
                3 => GetTriangleArea(sides),
                _ => throw new Exception($"Не описано метода для вчисления площади фигуры " +
                                         $"с данным количеством сторон {sides.Length}."),
            };
        }

        public double GetCircleArea(double[] sides)
        {
            double Area;
            Area = Math.PI * Math.Pow(sides[0], 2);
            return Area;
        }

        public double GetTriangleArea(double[] sides)
        {
            double Area;
            double SemiPerimeter = (Perimeter(FigureSides) / 2);
            
            if (GetIsRightTriangle(sides, GetMaxSide(sides)))
            {
                Area = GetRightTriangleArea(sides);
            }
            else Area = Math.Sqrt(SemiPerimeter * (SemiPerimeter - sides[0])
                                                * (SemiPerimeter - sides[1])
                                                * (SemiPerimeter - sides[2]));
            return Area;
        }
        private double GetRightTriangleArea(double[] sides)
        {
            double Area;
            double maxSide = GetMaxSide(sides);

            if (sides[0] == maxSide) Area = sides[1] * sides[2] / 2;
            else if (sides[1] == maxSide) Area = sides[0] * sides[2] / 2;
            else Area = sides[0] * sides[1] / 2;

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
        #endregion
    }
}
