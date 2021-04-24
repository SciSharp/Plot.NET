using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlotNET.Models
{
    public class LayoutMargin
    {
        [JsonProperty("t")]
        public int Top { get; set; } = 5;
        [JsonProperty("l")]
        public int Left { get; set; } = 30;
        [JsonProperty("r")]
        public int Right { get; set; } = 5;
        [JsonProperty("b")]
        public int Bottom { get; set; } = 30;
    }
}
