using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace PlotNET
{
    public class Plotter
    {
        private string _jsUrl = "https://cdn.plot.ly/plotly-latest.min.js";

        private string[] _labels;

        private float[] _xValues;

        private float[] _yValues;

        private List<Trace> _traces = new List<Trace>();

        public Plotter Plot(float[] xValues, float[] yValues)
        {
            _xValues = xValues;
            _yValues = yValues;
            return this;
        }

        public Plotter Plot(int[] xValues, int[] yValues)
        {
            return Plot(xValues.Select(v => (float)v).ToArray(),
                yValues.Select(v => (float)v).ToArray());
        }

        public Plotter Plot(string[] labels, float[] yValues)
        {
            _labels = labels;
            _yValues = yValues;
            return this;
        }

        public Plotter Plot(string[] labels, int[] yValues)
        {
            return Plot(labels, yValues.Select(v => (float)v).ToArray());
        }

        public Plotter Trace(int[] xValues, int[] yValues)
        {
            _traces.Add(new Trace(xValues, yValues));
            return this;
        }

        public Plotter Labels(string[] labels)
        {
            _labels = labels;
            return this;
        }

        public Plotter X(float[] values)
        {
            _xValues = values;
            return this;
        }

        public Plotter X(int[] values)
        {
            return X(values.Select(v => (float)v).ToArray());
        }

        public Plotter Y(float[] values)
        {
            _yValues = values;
            return this;
        }

        public Plotter Y(int[] values)
        {
            return Y(values.Select(v => (float)v).ToArray());
        }

        public Plotter Values(float[] xValues, float[] yValues)
        {
            _xValues = xValues;
            _yValues = yValues;
            return this;
        }

        public Plotter Values(string[] labels, float[] yValues)
        {
            _labels = labels;
            _yValues = yValues;
            return this;
        }

        private string RenderHeader()
        {
            return $"<script src=\"{_jsUrl}\"></script>";
        }

        private string RenderBody(string divClientID)
        {
            return $"<div id=\"{divClientID}\"></div>";
        }

        private string GetDataByXY()
        {
            var x = _labels != null ?
                string.Join(",", _labels.Select(l => "'" + l  + "'"))
                : string.Join(",", _xValues);

            var y = string.Join(",", _yValues);

            return @"[
                            {
                                x: [" + x + @"],
                                y: [" + y +  @"],
                                type: 'bar'
                            }
                        ];";
        }

        private string GetDataByTraces()
        {
            return "[" + string.Join(",", _traces.Select(t =>
            {
                return @"{
                    x: [" + string.Join(",", t.XValues) + @"],
                    y: [" + string.Join(",", t.YValues) + @"],
                    type: '" + t.Type +  @"'
                }";
            })) + "]";
        }

        private string RenderJS(string divClientID)
        {
            var data = _traces.Any() ? GetDataByTraces() : GetDataByXY();

            return @"<script>
                        var data = " + data + @";
                        Plotly.newPlot('" + divClientID + @"', data);
                    </script>";
        }

        public JObject Show()
        {
            return Show(0, 0);
        }

        public JObject Show(int width, int height)
        {
            var html = RenderHeader();
            var divClientID = "plot-" + Math.Abs(Guid.NewGuid().ToString().GetHashCode());
            var fileName = divClientID + ".html";
            html += RenderBody(divClientID);
            html += RenderJS(divClientID);

            File.WriteAllText(Path.Combine(AppContext.BaseDirectory, fileName), html, Encoding.UTF8);

            var strWidth = width == 0 ? "100%" : width.ToString() + "px";
            var strHeight = height == 0 ? "100%" : height.ToString() + "px";
            
            return new JObject {                
                {
                    "data", new JObject {
                        { "text/html", $"<iframe border=\"0\" style=\"border:0px;width:{strWidth};height:{strHeight};\" src=\"{fileName}\"></iframe>" }
                    }
                }
            };
        }
    }
}
