using Curiosity;

namespace CuriosityTest
{
    public class UnitTest1
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
    }
}