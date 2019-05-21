using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace PlotNET
{
    public class Trace
    {
        public Trace(float[] xValues, float[] yValues)
        {
            XValues = xValues;
            YValues = yValues;
            Type = "scatter";
        }

        public Trace(int[] xValues, int[] yValues)
        {
            XValues = xValues.Select(v => (float)v).ToArray();
            YValues = yValues.Select(v => (float)v).ToArray();
            Type = "scatter";
        }

        public float[] XValues { get; set; }

        public float[] YValues { get; set; }

        public string Type { get; set; }
    }
}
