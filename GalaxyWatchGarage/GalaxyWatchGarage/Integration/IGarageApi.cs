using SamsungWatchGarage.Integration.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SamsungWatchGarage.Integration
{
    public interface IGarageApi
    {
        Task<bool> Login(string email, string password);
        Task<List<Device>> GetDevices();
        Task<Device> GetDevice(string serialNumber);
        Task<DoorState> GetDoorState(string serialNumber);
        Task<bool> SetDoorState(string serialNumber, string action);
    }
}
