using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;

namespace PlotNET
{
    public partial class Plotter
    {
        private string RenderHeader()
        {
            return $"<script src=\"{_jsUrl}\"></script>";
        }

        private string RenderBody(string divClientID)
        {
            return $"<div id=\"{divClientID}\"></div>";
        }

        private string RenderJS(string divClientID, int width, int height)
        {
            var data = GetDataByTraces();
            _layout.Width = width;
            _layout.Height = height;
            var layout = JsonConvert.SerializeObject(_layout, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return "\r\n" + 
@"<script>
    var data = " + data + @";
    var layout = " + layout + @";
    Plotly.newPlot('" + divClientID + @"', data, layout);
</script>";
        }
    }
}
