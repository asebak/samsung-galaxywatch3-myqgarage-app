using SamsungWatchGarage.Integration;
using SamsungWatchGarage.Integration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tizen.Applications;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Device = SamsungWatchGarage.Integration.Models.Device;
namespace GalaxyWatchGarage.Pages
{
    public class Garage : ContentPage
    {
        Button button;
        MyQApi api;
        DoorState doorState;
        Device garageDoor;

        public Garage(MyQApi api)
        {
            this.api = api;



            Xamarin.Forms.Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            {
                Task.Run(() => GetGarageDoor()).Wait();
                return true;
            });


            Task.Run(() => GetGarageDoor());

            button = new Button
             {
                 HorizontalOptions = LayoutOptions.Center,
                 Text = "Loading...",
                 IsEnabled = false
             };


            button.Clicked += OnButtonClicked;

                Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children = {
                        button
                    }

            };
        }

        public async void GetGarageDoor()
        {
            var devices = await api.GetDevices();
            garageDoor = devices.Where(x => x.DeviceFamily == "garagedoor").FirstOrDefault();
            GetDoorState(garageDoor);
        }

        public async void GetDoorState(Device garageDoor)
        {
            doorState = await api.GetDoorState(garageDoor.SerialNumber);
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                UpdateUi(doorState);
            });
        }


        private void UpdateUi(DoorState doorState)
        {
            switch (doorState)
            {
                case SamsungWatchGarage.Integration.Models.DoorState.opening:
                case SamsungWatchGarage.Integration.Models.DoorState.closing:
                    button.BackgroundColor = Color.Brown;
                    button.Text = doorState.ToString();
                    button.IsEnabled = false;
                    break;
                case SamsungWatchGarage.Integration.Models.DoorState.open:
                    button.BackgroundColor = Color.Red;
                    button.IsEnabled = true;
                    button.Text = "Close";
                    break;
                case SamsungWatchGarage.Integration.Models.DoorState.closed:
                    button.BackgroundColor = Color.Green;
                    button.IsEnabled = true;
                    button.Text = "Open";
                    break;
            }
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            var action = string.Empty;
            var newState = DoorState.opening;
            if(doorState == DoorState.open)
            {
                action = Constants.ActionClose;
                newState = DoorState.closing;
            } else if(doorState == DoorState.closed)
            {
                action = Constants.ActionOpen;
                newState = DoorState.opening;
            }
            else
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    Toast.DisplayText("Couldn't set");
                });
                return;
            }
            var set = await api.SetDoorState(garageDoor.SerialNumber, action);
            if(set)
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    UpdateUi(newState);
                });
            }

        }

    }
}