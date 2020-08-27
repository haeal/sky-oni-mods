using System;
using Newtonsoft.Json;
using PeterHan.PLib;
using PeterHan.PLib.Options;

namespace Drains
{
    [Serializable][RestartRequired]
    public class DrainOptions: POptions.SingletonOptions<DrainOptions>
    {
        [JsonProperty]
        [Option("Solid Drains", "Drains will be solid and absorb water on the cell above them.")]
        public bool UseSolidDrain { get; set; }
        [JsonProperty]
        [Option("Drain Size", "The amount of fluid the drain will pick up in one go.")]
        public float DrainSize { get; set; }
    }
}
