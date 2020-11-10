using Newtonsoft.Json;
using SamsungWatchGarage.Integration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Action = SamsungWatchGarage.Integration.Models.Action;

namespace SamsungWatchGarage.Integration
{
    public class MyQApi : IGarageApi
    {
        private string accountId;
        private string token;
        private List<Device> devices;
        public MyQApi()
        {
            devices = new List<Device>();
        }

        public async Task<bool> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }
            else if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(email));
            }
            else
            {

                var json = JsonConvert.SerializeObject(new UserCredentials
                {
                    Username = email,
                    Password = password
                });
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var url = Constants.AuthUrl + "Login";
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("MyQApplicationId", Constants.MyQApplicationId);

                try
                {
                    var response = await client.PostAsync(url, data);
                    response.EnsureSuccessStatusCode();
                    var result = JsonConvert.DeserializeObject<UserToken>(response.Content.ReadAsStringAsync().Result);
                    token = result.SecurityToken;
                    if (string.IsNullOrEmpty(token))
                    {
                        return false;
                    }
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }

            }
        }

        public async Task<Device> GetDevice(string serialNumber)
        {
            if (devices.Count == 0)
            {
                GetDevices();
            }
            return devices.Where(x => x.SerialNumber == serialNumber).FirstOrDefault();
        }

        public async Task<List<Device>> GetDevices()
        {
            if (string.IsNullOrEmpty(accountId))
            {
                accountId = await GetAccountId();
            }

            var url = Constants.DeviceUrl + $"Accounts/{accountId}/Devices";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("MyQApplicationId", Constants.MyQApplicationId);
            client.DefaultRequestHeaders.Add("SecurityToken", token);
            try
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var result = JsonConvert.DeserializeObject<DeviceResponse>(response.Content.ReadAsStringAsync().Result);
                devices = result.Items;
                return devices;

            }
            catch (Exception e)
            {
                return new List<Device>();
            }
        }

        public async Task<DoorState> GetDoorState(string serialNumber)
        {
            //door_state: opening, closed, open, closing

            var state = await GetState(serialNumber, nameof(DoorState));

            return (DoorState)Enum.Parse(typeof(DoorState), state);
        }


        public async Task<bool> SetDoorState(string serialNumber, string action)
        {
            return await SetDeviceState(serialNumber, action, Constants.DoorState);
        }


        private async Task<string> GetAccountId()
        {
            var url = Constants.AuthUrl + "My?expand=account";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("MyQApplicationId", Constants.MyQApplicationId);
            client.DefaultRequestHeaders.Add("SecurityToken", token);
            try
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var result = JsonConvert.DeserializeObject<AccountData>(response.Content.ReadAsStringAsync().Result);
                return result.Account.Id;

            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        private async Task<bool> SetDeviceState(string serialNo, string action, string stateProp)
        {


            var url = Constants.DeviceUrl + $"Accounts/{accountId}/Devices/{serialNo}/Actions";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("MyQApplicationId", Constants.MyQApplicationId);
            client.DefaultRequestHeaders.Add("SecurityToken", token);
            var json = JsonConvert.SerializeObject(new Action
            {
                ActionType = action
            });
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PutAsync(url, data);
                response.EnsureSuccessStatusCode();
                devices = await GetDevices();
                return true;

            }
            catch (Exception e)
            {
                return false;
            }

        }

        private async Task<string> GetState(string serialNo, string stateProp)
        {
            var source = await GetDevice(serialNo);
            var value = GetPropValue(source.State, stateProp);
            return value.ToString();
        }

        public static object GetPropValue(object source, string propertyName)
        {
            var property = source.GetType().GetRuntimeProperties().FirstOrDefault(p => string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase));
            return property?.GetValue(source);
        }
    }
}
