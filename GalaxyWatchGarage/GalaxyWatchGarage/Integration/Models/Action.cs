using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SamsungWatchGarage.Integration.Models
{
    public class Action
    {
        [JsonProperty("action_type")]
        public string ActionType { get; set; }
    }
}
