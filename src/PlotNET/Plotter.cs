using System;
using System.Collections.Generic;
using System.Text;
using Tensorflow.NumPy;
using PlotNET.Models;

namespace PlotNET
{
    public partial class Plotter
    {
        private string _jsUrl = "https://cdn.plot.ly/plotly-latest.min.js";

        private List<Trace> _traces = new List<Trace>();
        Layout _layout = new Layout();

        public Plotter Plot(NDArray xValues, NDArray yValues, string name, ChartType type = ChartType.Bar, string mode = null)
        {
            _traces.Add(new Trace(xValues, yValues, type)
            {
                Name = name,
                Mode = mode
            });
            return this;
        }

        public Plotter Plot(float[] xValues, float[] yValues, string name, ChartType type = ChartType.Bar, string mode = null)
        {
            _traces.Add(new Trace(xValues, yValues, type)
            {
                Name = name,
                Mode = mode
            });
            return this;
        }

        public Plotter Plot(int[] xValues, int[] yValues, string name, ChartType type = ChartType.Bar, string mode = null)
        {
            _traces.Add(new Trace(xValues, yValues, type)
            {
                Name = name,
                Mode = mode
            });
            return this;
        }

        public Plotter Plot(string[] labels, NDArray yValues, string name, ChartType type = ChartType.Bar, string mode = null)
        {
            _traces.Add(new Trace(labels, yValues, type)
            {
                Name = name,
                Mode = mode
            });
            return this;
        }

        public Plotter Plot(string[] labels, float[] yValues, string name, ChartType type = ChartType.Bar, string mode = null)
        {
            _traces.Add(new Trace(labels, yValues, type)
            {
                Name = name,
                Mode = mode
            });
            return this;
        }

        public Plotter Plot(string[] labels, int[] yValues, string name, ChartType type = ChartType.Bar, string mode = null)
        {
            _traces.Add(new Trace(labels, yValues, type)
            {
                Name = name,
                Mode = mode
            });
            return this;
        }

        public Plotter Plot(Trace trace)
        {
            _traces.Add(trace);
            return this;
        }
    }
}
