namespace LittleTetris
{
    
    public static class GameModel
    { 
        public static readonly bool[,] field = new bool[Constants.CELL_COUNT_X, Constants.CELL_COUNT_Y];
        public static Figure figure = new Figure();
        public static int gameScore = 0;
        public static int destroyedLines = 0;
    }
}
