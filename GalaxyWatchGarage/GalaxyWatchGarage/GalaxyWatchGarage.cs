using GalaxyWatchGarage.Pages;
using SamsungWatchGarage.Integration;
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
        public App()
        {
            api = new MyQApi();
        }



        protected async override void OnStart()
        {
            var result = await api.Login(Constants.Email, Constants.Password);
            if (result)
            {
                MainPage = new Garage(api);
            }
            else
            {
                MainPage = new ContentPage
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
