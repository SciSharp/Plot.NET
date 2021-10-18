using PandasNet;
using System;
using System.Collections.Generic;
using System.Text;
using Tensorflow.NumPy;
using System.Linq;

namespace PlotNET.Extensions
{
    public static class DataFrameExt
    {
        public static void plot(this DataFrame df)
        {
            var plotter = new Plotter();

            foreach(var col in df.data)
            {
                plotter.Plot(
                    (string[])df.index.data,
                    np.array(col.data),
                    "DataFrame", ChartType.Scatter);
                
            }
            plotter.Show();
        }
    }
}
