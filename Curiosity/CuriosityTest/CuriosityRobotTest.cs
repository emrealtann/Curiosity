using Curiosity;
using System.Text;

namespace CuriosityTest
{
    public class CuriosityRobotTest
    {
        [Fact]
        public void Empty_Grid_Returns_Initial_Location()
        {
            CuriosityRobot robot = new CuriosityRobot("", "FFRL");
            string result = robot.Run();
            Assert.Equal("1,1,North", result);
        }

        [Fact]
        public void Empty_Command_Returns_Initial_Location()
        {
            CuriosityRobot robot = new CuriosityRobot("1x1", "");
            string result = robot.Run();
            Assert.Equal("1,1,North", result);
        }

        [Fact]
        public void One_Command_Returns_Correct_Location()
        {
            CuriosityRobot robot = new CuriosityRobot("2x2", "F");
            string result = robot.Run();
            Assert.Equal("1,2,North", result);
        }

        [Fact]
        public void Sample_Command_Returns_Correct_Location()
        {
            CuriosityRobot robot = new CuriosityRobot("5x5", "FFRFLFLF");
            string result = robot.Run();
            Assert.Equal("1,4,West", result);
        }

        [Fact]
        public void Sample_Command2_Returns_Correct_Location()
        {
            CuriosityRobot robot = new CuriosityRobot("3x6", "FFLFRRRFLFF");
            string result = robot.Run();
            Assert.Equal("3,2,East", result);
        }

        [Fact]
        public void Sample_Command3_Returns_Correct_Location()
        {
            CuriosityRobot robot = new CuriosityRobot("1x1", "LFLRLLFLRR");
            string result = robot.Run();
            Assert.Equal("1,1,South", result);
        }

        [Fact]
        public void Sample_Command4_Returns_Correct_Location()
        {
            CuriosityRobot robot = new CuriosityRobot("4x3", "FFFFRFFFFLL");
            string result = robot.Run();
            Assert.Equal("4,3,West", result);
        }

        [Fact]
        public void Sample_Command5_Returns_Correct_Location()
        {
            CuriosityRobot robot = new CuriosityRobot("4x4", "FLFRFFFRFRFFFFLFRFL");
            string result = robot.Run();
            Assert.Equal("3,1,East", result);
        }

        [Fact]
        public void Sample_Command6_Returns_Correct_Location()
        {
            CuriosityRobot robot = new CuriosityRobot("4x4", "LLLL");
            string result = robot.Run();
            Assert.Equal("1,1,North", result);
        }

        [Fact]
        public void Sample_Command7_Returns_Correct_Location()
        {
            StringBuilder commands = new StringBuilder();
            for(int i = 0; i < 500_000_000; i++)
            {
                commands.Append('F');
            }

            CuriosityRobot robot = new CuriosityRobot("10000x10000", commands.ToString());
            string result = robot.Run();
            Assert.Equal("1,10000,North", result);
        }
    }
}