using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace PlotNET
{
    public class Plotter
    {
        private string _jsUrl = "https://cdn.plot.ly/plotly-latest.min.js";

        private string _html;


        public Plotter Plot(string fileName)
        {
            var html = RenderHeader();
            var divClientID = "plot-" + Math.Abs(Guid.NewGuid().ToString().GetHashCode());
            html += RenderBody(divClientID);
            html += RenderJS(divClientID);
            _html = $"<iframe border=\"0\" style=\"border:0px;\" src=\"{fileName}\"></iframe>";
            File.WriteAllText(Path.Combine(AppContext.BaseDirectory, fileName), html, Encoding.UTF8);
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

        private string RenderJS(string divClientID)
        {
            return @"<script>
                        var data = [
                            {
                                x: ['giraffes', 'orangutans', 'monkeys'],
                                y: [20, 14, 23],
                                type: 'bar'
                            }
                        ];

                        Plotly.newPlot('" + divClientID + "', data);</script>";
        }

        public JObject Show()
        {
            return new JObject {                
                {
                    "data", new JObject {
                        { "text/html", _html }
                    }
                }
            };
        }
    }
}
