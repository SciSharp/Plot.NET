using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using NumSharp;
using PlotNET.Models;

namespace PlotNET
{
    public partial class Plotter
    {
        public void Ylim(Shape shape)
        {
            _layout.YAxis = new AxisElement
            {
                Range = shape.Dimensions
            };
        }
    }
}
