using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlotNET.UnitTest
{
    [TestClass]
    public class BarChartTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var plotter = new Plotter();
            plotter.Plot("TestMethod1");
        }
    }
}
