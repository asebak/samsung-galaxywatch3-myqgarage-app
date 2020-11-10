using SamsungWatchGarage.Integration;
using System;
using System.Linq;

namespace ConsoleApiTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = new MyQApi();
            var result =  api.Login(Constants.Email, Constants.Password).Result; 
            var garageDoor = api.GetDevices().Result.Where(x => x.DeviceFamily == "garagedoor").FirstOrDefault() ;
            var doorstate = api.GetDoorState(garageDoor.SerialNumber).Result;
            var setting = api.SetDoorState(garageDoor.SerialNumber, Constants.ActionClose).Result;
        }
    }
}
