namespace Curiosity
{
    public class CuriosityRobot
    {
        private string grid;
        private int[] gridBoundaries;
        private string commands;

        private Dictionary<FacingEnum, List<int>> commandIdentifiersMap;

        public CuriosityRobot(string grid, string commands)
        {
            this.grid = grid;
            this.commands = commands;
            gridBoundaries = new int[] { 1, 1 };

            commandIdentifiersMap = new Dictionary<FacingEnum, List<int>>();
            //List<int> => { axis, step }
            commandIdentifiersMap.Add(FacingEnum.West, new List<int>() { 0, -1 });
            commandIdentifiersMap.Add(FacingEnum.East, new List<int>() { 0, 1 });
            commandIdentifiersMap.Add(FacingEnum.South, new List<int>() { 1, -1 });
            commandIdentifiersMap.Add(FacingEnum.North, new List<int>() { 1, 1 });
        }

        public string Run() {
            //starting position 1x1,North
            Position position = new Position(new int[] { 1, 1 }, FacingEnum.North);
            if(string.IsNullOrEmpty(grid) || string.IsNullOrEmpty(commands))
            {
                return GenerateResult(position);
            }

            SetGrid();

            position = ExecuteCommands(position);

            return GenerateResult(position);
        }

        private void SetGrid()
        {
            try
            {
                grid = grid.ToLower();
                if (grid.Where(x => x.Equals('x')).Count() != 1)
                {
                    return;
                }

                String[] gridArray = grid.Split('x');
                int gridX = int.Parse(gridArray[0]);
                int gridY = int.Parse(gridArray[1]);
                gridBoundaries = new int[] { gridX, gridY };
            }
            catch (Exception)
            {
                gridBoundaries = new int[] { 1, 1 };
            }
        }

        private Position ExecuteCommands(Position position)
        {
            foreach(char c in commands.ToCharArray())
            {
                List<int> identifiers = commandIdentifiersMap[position.Direction];
                if(c == 'F')
                {
                    //step forward
                    int axisIndex = identifiers[0];
                    int step = identifiers[1];
                    int currentPos = position.Coordinates[axisIndex];
                    int intendedPos = currentPos + step;
                    if(intendedPos <= gridBoundaries[axisIndex] && intendedPos >= 1)
                    {
                        //legal move
                        position.Coordinates[axisIndex] = intendedPos;
                    }
                }
                else if(c == 'L')
                {
                    //turn left
                    int currentDirection = (int)position.Direction;
                    int direction = currentDirection == 0 ? 3 : currentDirection - 1;
                    position.Direction = (FacingEnum)direction;
                }
                else if (c == 'R')
                {
                    //turn right
                    int currentDirection = (int)position.Direction;
                    int direction = currentDirection == 3 ? 0 : currentDirection + 1;
                    position.Direction = (FacingEnum)direction;
                }
            }
            return position;
        }

        private string GenerateResult(Position position)
        {
            string result = position.Coordinates[0] + "," + position.Coordinates[1] + "," + position.Direction.ToString();
            return result;
        }

        enum FacingEnum
        {
            North = 0,
            East = 1,
            South = 2,
            West = 3
        }

        class Position
        {
            public Position(int[] coordinates, FacingEnum direction)
            {
                Coordinates = coordinates;
                Direction = direction;
            }

            public int[] Coordinates { get; set; }
            public FacingEnum Direction { get; set; }
        }
    }
}