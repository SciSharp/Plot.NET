﻿using System;
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

        private List<Trace> _traces = new List<Trace>();

        public Plotter Plot(float[] xValues, float[] yValues, ChartType type = ChartType.Bar, string name = null)
        {
            _traces.Add(new Trace(xValues, yValues, type) { Name = name });
            return this;
        }

        public Plotter Plot(int[] xValues, int[] yValues, ChartType type = ChartType.Bar, string name = null)
        {
            _traces.Add(new Trace(xValues, yValues, type) { Name = name });
            return this;
        }

        public Plotter Plot(string[] labels, float[] yValues, ChartType type = ChartType.Bar, string name = null)
        {
            _traces.Add(new Trace(labels, yValues, type) { Name = name });
            return this;
        }

        public Plotter Plot(string[] labels, int[] yValues, ChartType type = ChartType.Bar, string name = null)
        {
            _traces.Add(new Trace(labels, yValues, type) { Name = name });
            return this;
        }

        public Plotter Trace(int[] xValues, int[] yValues, ChartType type = ChartType.Bar, string name = null)
        {
            _traces.Add(new Trace(xValues, yValues, type) { Name = name });
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

                return @"{
                    x: [" + xTexts + @"],
                    y: [" + string.Join(",", t.YValues) + @"],
                    type: '" + t.Type.ToString().ToLower() +  @"'" + nameNode + @"
                }";
            })) + "]";
        }

        private string RenderJS(string divClientID)
        {
            var data = GetDataByTraces();

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
