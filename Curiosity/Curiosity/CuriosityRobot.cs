namespace Curiosity
{
    public class CuriosityRobot
    {
        private string grid;
        private string commands;
        public CuriosityRobot(string grid, string commands)
        {
            this.grid = grid;
            this.commands = commands;
        }

        public string Run() {
            string result = "1,1,North";
            return result;
        }
    }
}