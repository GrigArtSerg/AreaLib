using System;

namespace AreaLib
{
    public class Figure : IFigure
    {
        readonly double[] FigureSides;
        readonly int FigureSidesCount;
        readonly bool IsRightTriangle;
        readonly double MaxSide;

        public double[] GetFigureSides { get { return FigureSides; } }
        public int GetfigureSidesCount { get { return FigureSidesCount; } }
        public bool GetIsRightTriangle { get { return IsRightTriangle; } }
        public double GetMaxSide { get { return MaxSide; } }

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
                /// сумма сторон треугольника больше третьей
                case 3:
                    MaxSide = SetMaxSide(sides);
                    TriangleIsTriangle(sides);
                    IsRightTriangle = SetIsRightTriangle(sides);
                    break;
                default:
                    throw new Exception($"Не описано метода для вычисления площади фигуры" +
                                         $"с данным количеством сторон {FigureSidesCount}.");
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

            if (FigureSides[0] == MaxSide) Area = FigureSides[1] * FigureSides[2] / 2;
            else if (FigureSides[1] == MaxSide) Area = FigureSides[0] * FigureSides[2] / 2;
            else Area = FigureSides[0] * FigureSides[1] / 2;

            return Area;
        }
        #endregion

        /*
        #region Методы для рассчета площади с прямой передачей параметров
        public double GetArea(double[] sides)
        {
            // TODO проверять существует ли уже 
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

            if (SetIsRightTriangle(sides, SetMaxSide(sides)))
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
            double maxSide = SetMaxSide(sides);

            if (sides[0] == maxSide) Area = sides[1] * sides[2] / 2;
            else if (sides[1] == maxSide) Area = sides[0] * sides[2] / 2;
            else Area = sides[0] * sides[1] / 2;

            return Area;
        }
        #endregion
        */
        private void ValuesCheck(double[] sides)
        {
            for (int i = 0; i < sides.Length; i++)
            {
                if (sides[i] < Staff.Accuracy) throw new Exception($"Неверно указана сторона с индексом {i}.");
            }
        }

        private double SetMaxSide(double[] sides)
        {
            double maxSide = 0;
            for (int i = 0; i < sides.Length; i++)
            {
                if (maxSide < sides[i]) maxSide = sides[i];
            }
            return maxSide;
        }

        private void TriangleIsTriangle(double[] sides)
        {
            double perimeter = Perimeter(sides);
            if (perimeter - MaxSide - MaxSide < Staff.Accuracy)
            {
                throw new Exception("Наибольшая сторона треугольника должна быть меньше суммы других сторон.");
            }
        }

        private bool SetIsRightTriangle(double[] sides)
        {
            double a, b;

            if (sides[0] == MaxSide)
            {
                a = sides[1];
                b = sides[2];
            }
            else if (sides[1] == MaxSide)
            {
                a = sides[0];
                b = sides[2];
            }
            else
            {
                a = sides[0];
                b = sides[1];
            }

            return Math.Abs(Math.Pow(MaxSide, 2) - Math.Pow(a, 2) - Math.Pow(b, 2)) < Staff.Accuracy;
        }

        public double Perimeter(double[] sides)
        {
            double perimeter = 0;
            foreach (double side in sides) perimeter += side;
            return perimeter;
        }
    }
}
