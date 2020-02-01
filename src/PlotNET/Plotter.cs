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

        private string RenderJS(string divClientID, int width, int height)
        {
            var data = GetDataByTraces();
            
            var strWidth = @",
                    width: " + (width - 20) + "," + @"
                    height: " + (height - 20);

            return @"<script>
                        var data = " + data + @";
                        Plotly.newPlot('" + divClientID + @"', data,
                            {
                                margin: { t: 5, l: 30, r: 5, b: 30 }" + strWidth + @"
                            });
                    </script>";
        }

        public void Show()
        {
            Show(0, 0);
        }

        public void Show(int width, int height)
        {
            if (width <= 0 && height <= 0)
            {
                width = 800;
                height = 500;
            }
            else if (width > 0 && height <= 0)
            {
                height = (width / 8) * 5;
            }
            else if (height > 0 && width <= 0)
            {
                width = (height / 5) * 8;
            }

            var html = RenderHeader();
            var divClientID = "plot-" + Math.Abs(Guid.NewGuid().ToString().GetHashCode());
            var fileName = divClientID + ".html";
            html += RenderBody(divClientID);
            html += RenderJS(divClientID, width, height);

            File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName), html, Encoding.UTF8);

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
