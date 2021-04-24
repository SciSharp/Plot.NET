using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlotNET.Models
{
    public class AxisElement
    {
        [JsonProperty("range")]
        public int[] Range { get; set; }
    }
}
