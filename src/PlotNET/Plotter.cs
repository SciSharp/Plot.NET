using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PlotNET
{
    public class Plotter
    {
        private string jsUrl = "https://cdn.plot.ly/plotly-latest.min.js";

        public string Plot(string fileName = "")
        {
            var html = RenderHeader();
            html += RenderBody();
            html += RenderJS();

            if (!string.IsNullOrEmpty(fileName))
                File.WriteAllText(fileName + ".html", html);

            return html;
        }

        private string RenderHeader()
        {
            return $"<head>" +
                $"<script src=\"{jsUrl}\"></script>" +
                $"</head>";
        }

        private string RenderBody()
        {
            return $"<div id=\"tester\" style=\"width: 90 %; height: 250px;\"></div>";
        }

        private string RenderJS()
        {
            return @"<script>

                       TESTER = document.getElementById('tester');

                        Plotly.plot(TESTER, [{
                        x: [1, 2, 3, 4, 5],
                        y: [1, 2, 4, 8, 16] }], {
                        margin: { t: 0 }
                        }, { showSendToCloud: true} );

                    </script>";
        }
    }
}
