namespace Curiosity
{
    /// <summary>
    /// Main class for the Curiosity robot
    /// @author: Emre Altan <emre.altan2@gmail.com>
    /// @date: 2023-04-04
    /// </summary>
    public class CuriosityRobot
    {
        private string grid;
        private int[] gridBoundaries;
        private string commands;

        private Dictionary<FacingEnum, List<int>> commandIdentifiersMap;

        /// <summary>
        /// Constructor method for the Curiosity robot
        /// </summary>
        /// <param name="grid">input for the Mars plateau grid. i.e 5x5</param>
        /// <param name="commands">commands for the robot. i.e LFLRFLFF</param>
        public CuriosityRobot(string grid, string commands)
        {
            this.grid = grid;
            this.commands = commands;
            gridBoundaries = new int[] { 1, 1 };

            //populate the command identifier map. List<int> => { axis, step }
            commandIdentifiersMap = new Dictionary<FacingEnum, List<int>>
            {
                { FacingEnum.West, new List<int>() { 0, -1 } },
                { FacingEnum.East, new List<int>() { 0, 1 } },
                { FacingEnum.South, new List<int>() { 1, -1 } },
                { FacingEnum.North, new List<int>() { 1, 1 } }
            };
        }

        /// <summary>
        /// Executes the commands and returns the final position of the robot
        /// </summary>
        /// <returns>final position of the robot as a string with the format like: 1,4,West</returns>
        public string Run() {
            //starting position 1x1,North
            Position position = new Position(new int[] { 1, 1 }, FacingEnum.North);
            if(string.IsNullOrEmpty(grid) || string.IsNullOrEmpty(commands))
            {
                return GenerateResult(position);
            }

            SetGridBoundaries();

            position = ExecuteCommands(position);

            return GenerateResult(position);
        }

        /// <summary>
        /// Sets gridBoundaries by using the grid input 
        /// </summary>
        private void SetGridBoundaries()
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

        /// <summary>
        /// Executes the commands and calculates the final position of the robot
        /// </summary>
        /// <param name="position">initial position</param>
        /// <returns>final position of the robot</returns>
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

        /// <summary>
        /// Converts the position object to the result string with the format like: 1,4,West
        /// </summary>
        /// <param name="position">position of the robot</param>
        /// <returns>formatted string version of the position</returns>
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

        /// <summary>
        /// Inner class for storing the current position info
        /// </summary>
        class Position
        {
            /// <summary>
            /// Constructor for the Position class
            /// </summary>
            /// <param name="coordinates">coordinates (x,y) of the current position</param>
            /// <param name="direction">facing direction of the robot</param>
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