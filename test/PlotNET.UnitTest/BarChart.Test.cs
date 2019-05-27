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
            
            plotter.Plot(
                new [] { "giraffes", "orangutans", "monkeys" },
                new [] { 20f, 14f, 23f },
                "Test Chart", ChartType.Bar);
        }
    }
}
