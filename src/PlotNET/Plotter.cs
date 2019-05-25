using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using NumSharp;
using ICSharpCore.Primitives;

namespace PlotNET
{
    public class Plotter
    {
        private string _jsUrl = "https://cdn.plot.ly/plotly-latest.min.js";

        private List<Trace> _traces = new List<Trace>();

        public Plotter Plot(NDArray xValues, NDArray yValues, ChartType type = ChartType.Bar, string name = null, string mode = null)
        {
            _traces.Add(new Trace(xValues, yValues, type)
            {
                Name = name,
                Mode = mode
            });
            return this;
        }

        public Plotter Plot(float[] xValues, float[] yValues, ChartType type = ChartType.Bar, string name = null, string mode = null)
        {
            _traces.Add(new Trace(xValues, yValues, type)
            {
                Name = name,
                Mode = mode
            });
            return this;
        }

        public Plotter Plot(int[] xValues, int[] yValues, ChartType type = ChartType.Bar, string name = null, string mode = null)
        {
            _traces.Add(new Trace(xValues, yValues, type)
            {
                Name = name,
                Mode = mode
            });
            return this;
        }

        public Plotter Plot(string[] labels, NDArray yValues, ChartType type = ChartType.Bar, string name = null, string mode = null)
        {
            _traces.Add(new Trace(labels, yValues, type)
            {
                Name = name,
                Mode = mode
            });
            return this;
        }

        public Plotter Plot(string[] labels, float[] yValues, ChartType type = ChartType.Bar, string name = null, string mode = null)
        {
            _traces.Add(new Trace(labels, yValues, type)
            {
                Name = name,
                Mode = mode
            });
            return this;
        }

        public Plotter Plot(string[] labels, int[] yValues, ChartType type = ChartType.Bar, string name = null, string mode = null)
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

        private string RenderHeader()
        {
            return $"<script src=\"{_jsUrl}\"></script>";
        }

        private string RenderBody(string divClientID)
        {
            return $"<div id=\"{divClientID}\"></div>";
        }

        private string GetDataByTraces()
        {
            return "[" + string.Join(",", _traces.Select(t =>
            {
                var xTexts = t.XValues != null ? string.Join(",", t.XValues) : string.Join(",", t.Labels.Select(l => "'" + l  + "'"));
                var nameNode = string.Empty;
                
                if (!string.IsNullOrEmpty(t.Name))
                {
                    nameNode = @",
                        name: '" + t.Name + "'";
                }

                var modeNode = string.Empty;

                if (!string.IsNullOrEmpty(t.Mode))
                {
                    modeNode = @",
                        mode: '" + t.Mode + "'";
                }


                return @"{
                    x: [" + xTexts + @"],
                    y: [" + string.Join(",", t.YValues) + @"],
                    type: '" + t.Type.ToString().ToLower()
                    +  @"'" + nameNode + 
                    modeNode + @"
                }";
            })) + "]";
        }

        private string RenderJS(string divClientID)
        {
            var data = GetDataByTraces();

            return @"<script>
                        var data = " + data + @";
                        Plotly.newPlot('" + divClientID + @"', data, { margin: { t: 0, l: 0, r: 0, b: 0 } });
                    </script>";
        }

        public void Show()
        {
            Show(0, 0);
        }

        public void Show(int width, int height)
        {
            var html = RenderHeader();
            var divClientID = "plot-" + Math.Abs(Guid.NewGuid().ToString().GetHashCode());
            var fileName = divClientID + ".html";
            html += RenderBody(divClientID);
            html += RenderJS(divClientID);

            File.WriteAllText(Path.Combine(AppContext.BaseDirectory, fileName), html, Encoding.UTF8);

            var strWidth = width == 0 ? "100%" : width.ToString() + "px";
            var strHeight = height == 0 ? "100%" : height.ToString() + "px";

            DisplayDataEmitter.Emit(new DisplayData
            {
                Data =  new JObject
                {
                    { "text/html", $"<iframe border=\"0\" style=\"border:0px;width:{strWidth};height:{strHeight};\" src=\"{fileName}\"></iframe>" }
                }
            });

            _traces.Clear();
        }
    }
}
