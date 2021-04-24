using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlotNET.Models
{
    public class Layout
    {
        [JsonProperty("margin")]
        public LayoutMargin Margin { get; set; } = new LayoutMargin();
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("xaxis")]
        public AxisElement XAxis { get; set; }
        [JsonProperty("yaxis")]
        public AxisElement YAxis { get; set; }
    }
}
