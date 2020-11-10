using System;
using System.Collections.Generic;
using System.Threading;

namespace LittleTetris
{
    public static class LineChecker
    {
        public static void IsTooHigh()
        {
            for (int i = 0; i < Constants.CELL_COUNT_X; i++)
                if (GameModel.field[i, 3])
                    Environment.Exit(0);
        }

        public static void FindFilledLines()
        {
            List<int> filledLines = new List<int>(4);
            bool lineIsFilled;
            for (int i = 0; i < Constants.CELL_COUNT_Y; i++)
            {
                lineIsFilled = true;
                for (int j = 1; j < Constants.CELL_COUNT_X; j++)
                {
                    if (!GameModel.field[j, i])
                    {
                        lineIsFilled = false;
                        break;
                    }
                }
                if (lineIsFilled)
                    filledLines.Add(i);
            }
            DestroyFilledLines(filledLines);
        }

        private static void DestroyFilledLines(List<int> filledLines)
        {
            if (filledLines.Count == 0)
                return;
            int multiple = 1;
            foreach (int line in filledLines)
            {
                for (int i = line; i > 0; i--)
                    for (int j = 1; j < Constants.CELL_COUNT_X; j++)
                        GameModel.field[j, i] = GameModel.field[j, i - 1];
                GameModel.gameScore += Constants.SCORE_FOR_LINE * multiple;
                GameModel.destroyedLines++;
                multiple++;
            }

        }
    }
}
