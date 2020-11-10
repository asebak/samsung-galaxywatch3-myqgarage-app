using System;
using System.Collections.Generic;
using System.Text;

namespace SamsungWatchGarage.Integration
{
    public static class Constants
    {
        public static string On = "turnOn";
        public static string Off = "turnOff";
        public static string ActionOpen = "open";
        public static string ActionClose = "close";
        public static string AuthVersion = "v5";
        public static string DeviceVersion = "v5.1";
        public static string AuthUrl = $"https://api.myqdevice.com/api/{AuthVersion}/";
        public static string DeviceUrl = $"https://api.myqdevice.com/api/{DeviceVersion}/";
        public static string MyQApplicationId = "JVM/G9Nwih5BwKgNCjLxiFUQxQijAebyyg8QUHr7JOrP+tuPb8iHfRHKwTmDzHOu";
        public static string DoorState = "door_state";
        public static string LightState = "light_state";

        public static string Email = "";
        public static string Password = "";
    }
}
