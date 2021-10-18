using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using PlotNET.Models;
using Tensorflow;

namespace PlotNET
{
    public partial class Plotter
    {
        public void Ylim(Shape shape)
        {
            _layout.YAxis = new AxisElement
            {
                Range = shape.as_int_list()
            };
        }
    }
}
