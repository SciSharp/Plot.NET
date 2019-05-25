using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using NumSharp;

namespace PlotNET
{
    public class Trace
    {
        public Trace(float[] xValues, float[] yValues)
            : this(xValues, yValues, ChartType.Scatter)
        {

        }

        public Trace(NDArray xValues, NDArray yValues)
            : this(xValues.Data<float>(), yValues.Data<float>(), ChartType.Scatter)
        {

        }

        public Trace(int[] xValues, int[] yValues)
            : this(xValues.Select(v => (float)v).ToArray(), yValues.Select(v => (float)v).ToArray(), ChartType.Scatter)
        {

        }

        public Trace(string[] labels, NDArray yValues, ChartType type)
            : this(labels, yValues.Data<float>(), type)
        {

        }

        public Trace(string[] labels, float[] yValues, ChartType type)
        {
            Labels = labels;
            YValues = yValues;
            Type = type;
        }

        public Trace(string[] labels, int[] yValues, ChartType type)
            : this(labels, yValues.Select(v => (float)v).ToArray(), type)
        {

        }

        public Trace(NDArray xValues, NDArray yValues, ChartType type)
            : this(xValues.Data<float>(), yValues.Data<float>(), type)
        {

        }

        public Trace(float[] xValues, float[] yValues, ChartType type)
        {
            XValues = xValues;
            YValues = yValues;
            Type = type;
        }

        public Trace(int[] xValues, int[] yValues, ChartType type)
            : this(xValues.Select(v => (float)v).ToArray(), yValues.Select(v => (float)v).ToArray(), type)
        {

        }

        public string Name { get; set; }

        public string Mode { get; set; }

        public float[] XValues { get; set; }

        public float[] YValues { get; set; }

        public string[] Labels { get; set; }

        public ChartType Type { get; set; }
    }
}
