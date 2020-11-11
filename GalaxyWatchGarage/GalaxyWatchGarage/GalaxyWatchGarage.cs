using GalaxyWatchGarage.Pages;
using NetworkApp.Models;
using SamsungWatchGarage.Integration;
using SamsungWatchGarage.Integration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace GalaxyWatchGarage
{
    public class App : Application
    {
        MyQApi api;
        private ContentPage noInternetPage = new ContentPage
        {
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children =
                    {
                        new Label
                        {
                            HorizontalTextAlignment = TextAlignment.Center,
                            Text = "No Internet Connectivity..."
                        }

                    }
            }
        };
        private ContentPage loggedInFail = new ContentPage
        {
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children =
                    {
                        new Label
                        {
                            HorizontalTextAlignment = TextAlignment.Center,
                            Text = "Login credentials are invalid..."
                        }

                    }
            }
        };
        private readonly WiFiApiManager wifi;

        public App()
        {
            api = new MyQApi();
            wifi = new WiFiApiManager();
        }



        protected async override void OnStart()
        {
            if (!wifi.IsActive())
            {
                await wifi.Activate();
            }
            await wifi.Scan();

            var net = wifi.ScanResult();

            var savedNetowrk = net.Where(x => wifi.IsAPInfoStored(x.Name)).FirstOrDefault();
            if (savedNetowrk != null)
            {
                if (savedNetowrk.State.ToLower() != "connected")
                {
                    await wifi.Connect(savedNetowrk.Name, string.Empty);
                }
                var result = await api.Login(Constants.Email, Constants.Password);
                if (result)
                {
                    MainPage = new Garage(api);
                }
                else
                {
                    MainPage = loggedInFail; 
                }
            } else
            {
                MainPage = noInternetPage;
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
