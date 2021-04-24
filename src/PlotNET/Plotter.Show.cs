using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ICSharpCore.Primitives;
using Newtonsoft.Json.Linq;

namespace PlotNET
{
    public partial class Plotter
    {
        public void Show()
        {
            Show(0, 0);
        }

        public void Show(int width, int height, string name = null)
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
            var fileName = (name ?? divClientID) + ".html";
            html += RenderBody(divClientID);
            html += RenderJS(divClientID, width, height);

            var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            File.WriteAllText(path, html, Encoding.UTF8);
            Console.WriteLine($"Plotly is saved to {path}.");

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

        private string GetDataByTraces()
        {
            return "[" + string.Join(",", _traces.Select(t =>
            {
                var xTexts = t.XValues != null ? string.Join(",", t.XValues) : string.Join(",", t.Labels.Select(l => "'" + l + "'"));
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
                    + @"'" + nameNode +
                    modeNode + @"
                }";
            })) + "]";
        }
    }
}
