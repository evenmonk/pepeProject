using System;
using System.Collections.Generic;

namespace LittleTetris
{
    public class Figure
    {
        private enum Figures
        {
            O = 0, //Квадрат
            I = 1, //Палка
            J = 2, //Углы
            L = 3,
            Z = 4, //Зиг-заги
            S = 5,
            T = 6 // Т-образная
        }

        public List<Point> CellsCoordinates;
        private readonly Figures type;

        public Figure()
        {
            CellsCoordinates = new List<Point>(4);
            type = (Figures)new Random().Next(7);
            switch (type)
            {
                case Figures.O:
                    CellsCoordinates.Add(new Point(7, 2));
                    CellsCoordinates.Add(new Point(7, 3));
                    CellsCoordinates.Add(new Point(8, 2));
                    CellsCoordinates.Add(new Point(8, 3));
                    break;
                case Figures.I:
                    CellsCoordinates.Add(new Point(7, 2));
                    CellsCoordinates.Add(new Point(7, 3));
                    CellsCoordinates.Add(new Point(7, 4));
                    CellsCoordinates.Add(new Point(7, 5));
                    break;
                case Figures.J:
                    CellsCoordinates.Add(new Point(7, 2));
                    CellsCoordinates.Add(new Point(7, 3));
                    CellsCoordinates.Add(new Point(7, 4));
                    CellsCoordinates.Add(new Point(6, 4));
                    break;
                case Figures.L:
                    CellsCoordinates.Add(new Point(7, 2));
                    CellsCoordinates.Add(new Point(7, 3));
                    CellsCoordinates.Add(new Point(7, 4));
                    CellsCoordinates.Add(new Point(8, 4));
                    break;
                case Figures.Z:
                    CellsCoordinates.Add(new Point(6, 3));
                    CellsCoordinates.Add(new Point(7, 3));
                    CellsCoordinates.Add(new Point(7, 4));
                    CellsCoordinates.Add(new Point(8, 4));
                    break;
                case Figures.S:
                    CellsCoordinates.Add(new Point(8, 3));
                    CellsCoordinates.Add(new Point(7, 3));
                    CellsCoordinates.Add(new Point(7, 4));
                    CellsCoordinates.Add(new Point(6, 4));
                    break;
                case Figures.T:
                    CellsCoordinates.Add(new Point(7, 3));
                    CellsCoordinates.Add(new Point(7, 4));
                    CellsCoordinates.Add(new Point(6, 4));
                    CellsCoordinates.Add(new Point(8, 4));
                    break;
                default:
                    throw new Exception("Something goes wrong at figure creature");
            }
        }

        private void UpdateAfterFall()
        {
            for (int i = 0; i < 4; i++)
            {
                Point cell = CellsCoordinates[i];
                GameModel.field[cell.X, cell.Y] = true;
                GameModel.figure = new Figure();
            }
        }

        public void Rotate()
        {
            if (this.type != Figures.O)
            {
                Point center = CellsCoordinates[1];
                Point cell;
                for (int i = 0; i < 4; i++)
                {
                    cell = CellsCoordinates[i];
                    int dx = center.X - cell.Y + center.Y;
                    int dy = center.Y + cell.X - center.X;
                    if (dx < 0 || dx >= Constants.CELL_COUNT_X || dy >= Constants.CELL_COUNT_Y)
                        return;
                    else if (GameModel.field[dx, dy])
                        return;
                    cell.X = dx;
                    cell.Y = dy;
                }
            }
        }

        public void MoveDown()
        {
            if (IsFigureFall())
                UpdateAfterFall();
            else
            {
                Point cell;
                for (int i = 0; i < 4; i++)
                {
                    cell = CellsCoordinates[i];
                    CellsCoordinates[i] = new Point(cell.X, cell.Y + 1);
                }
            }
        }

        public void MoveSide(int dx)
        {
            if (!AbleToSide(dx))
                return;
            for (int i = 0; i < 4; i++)
                CellsCoordinates[i].X += dx;
        }

        private bool AbleToSide(int dx)
        {
            Point cell;
            for (int i = 0; i < 4; i++)
            {
                cell = CellsCoordinates[i];
                if (cell.X + dx < 0
                    || cell.X + dx == Constants.CELL_COUNT_X
                    || GameModel.field[cell.X + dx, cell.Y])
                    return false;
            }
            return true;
        }

        private bool IsFigureFall()
        {
            Point cell;
            for (int i = 0; i < 4; i++)
            {
                cell = CellsCoordinates[i];
                if (cell.Y == Constants.CELL_COUNT_Y - 1 || GameModel.field[cell.X, cell.Y + 1])
                    return true;
            }
            return false;
        }
    }
}
