using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace semiFinalACT3
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class print : ContentPage
    {
        public print()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            string status = $"{Application.Current.Properties["status"]}";
            int meter = int.Parse($"{Application.Current.Properties["data"]}");

            var data = await App.db.GetAllAsync();

            if (data != null)
            {
                if(status == "add")
                {
                    await DisplayAlert("Success", "Meter Number: " + meter + "'s Data Passed and Added Successfully", "OK");
                    printData.ItemsSource = data;
                }
                else if(status == "update")
                {
                    await DisplayAlert("Success", "Meter Number: " + meter + "'s Data Passed and Updated Successfully", "OK");
                    printData.ItemsSource = data;
                }
                else if(status == "delete")
                {
                    var delete = await App.db.SearchAsync(meter);
                    
                    await DisplayAlert("Success", "Meter Number: " + meter + "'s Data Deleted Successfully", "OK");
                    await App.db.DeleteAsync(delete);

                    var refresh = await App.db.GetAllAsync();
                    printData.ItemsSource = refresh;
                }
            }
            else
            {
                await DisplayAlert("Notice!", "No Data Found!", "OK");
            }
        }
        private void Back(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
    }
}