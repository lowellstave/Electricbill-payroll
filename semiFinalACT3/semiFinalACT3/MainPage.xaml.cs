using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace semiFinalACT3
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        /*
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var tableList = await App.db.GetAllAsync();
            if (tableList != null)
            {
                print.ItemsSource = tableList;
            }
        }
        */
        public async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(meterNo.Text) && !string.IsNullOrWhiteSpace(previousR.Text) && !string.IsNullOrWhiteSpace(presentR.Text) && h.IsChecked == true || b.IsChecked == true)
            {
                try
                {
                    int meter = int.Parse(meterNo.Text);
                    double pres = double.Parse(presentR.Text);
                    double prev = double.Parse(previousR.Text);
                    double consumption, charge, chargePerKW, principal, amountP, demand, service, vat;

                    try
                    {
                        bool h1 = h.IsChecked, b1 = b.IsChecked;
                        string type;
                        if (h1)
                        {
                            demand = 300;
                            service = 200;
                            type = "H";
                        }
                        else if (b1)
                        {
                            demand = 600;
                            service = 400;
                            type = "B";
                        }
                        else
                        {
                            demand = 0;
                            service = 0;
                            type = "";
                            await DisplayAlert("Admin", "Invalid type of registration", "OK");
                        }

                        consumption = pres - prev;

                        if (consumption < 72)
                        {
                            chargePerKW = 6.50;
                        }
                        else if (consumption <= 150)
                        {
                            chargePerKW = 9.50;
                        }
                        else if (consumption <= 300)
                        {
                            chargePerKW = 10.50;
                        }
                        else if (consumption <= 400)
                        {
                            chargePerKW = 12.50;
                        }
                        else if (consumption <= 500)
                        {
                            chargePerKW = 14.00;
                        }
                        else
                        {
                            chargePerKW = 16.50;
                        }

                        charge = consumption * chargePerKW;
                        principal = charge + demand + service;
                        vat = principal * 0.05;
                        amountP = principal + vat;

                        table table = new table()
                        {
                            meterNo = meter,
                            presR = pres,
                            prevR = prev,
                            typeOfR = type,
                            principalAmount = principal,
                            amountPayable = amountP,
                        };
                        await App.db.SaveAsync(table);

                        h.IsChecked = b.IsChecked = false;
                        meterNo.Text = previousR.Text = presentR.Text = string.Empty;

                        string status = "add";

                        Application.Current.Properties["status"] = status;
                        Application.Current.Properties["data"] = table.meterNo.ToString();
                        await Navigation.PushAsync(new print());

                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Admin", "Invalid type of registration", "OK");
                    }

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Admin", "Meter, Present, and Previous must be a Number!", "OK");
                }
            }
            else
            {
                await DisplayAlert("Admin", "Meter, Present, Previous, and Type of Registration must not be Empty!", "OK");
            }
        }
        public async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(meterNo.Text) && !string.IsNullOrWhiteSpace(previousR.Text) && !string.IsNullOrWhiteSpace(presentR.Text) && h.IsChecked == true || b.IsChecked == true)
            {
                try
                {
                    int meter = int.Parse(meterNo.Text);
                    double pres = double.Parse(presentR.Text);
                    double prev = double.Parse(previousR.Text);
                    double consumption, charge, chargePerKW, principal, amountP, demand, service, vat;

                    try
                    {
                        bool h1 = h.IsChecked, b1 = b.IsChecked;
                        string type;
                        if (h1)
                        {
                            demand = 300;
                            service = 200;
                            type = "H";
                        }
                        else if (b1)
                        {
                            demand = 600;
                            service = 400;
                            type = "B";
                        }
                        else
                        {
                            demand = 0;
                            service = 0;
                            type = "";
                            await DisplayAlert("Admin", "Invalid type of registration", "OK");
                        }

                        consumption = pres - prev;

                        if (consumption < 72)
                        {
                            chargePerKW = 6.50;
                        }
                        else if (consumption <= 150)
                        {
                            chargePerKW = 9.50;
                        }
                        else if (consumption <= 300)
                        {
                            chargePerKW = 10.50;
                        }
                        else if (consumption <= 400)
                        {
                            chargePerKW = 12.50;
                        }
                        else if (consumption <= 500)
                        {
                            chargePerKW = 14.00;
                        }
                        else
                        {
                            chargePerKW = 16.50;
                        }

                        charge = consumption * chargePerKW;
                        principal = charge + demand + service;
                        vat = principal * 0.05;
                        amountP = principal + vat;

                        table table = new table()
                        {
                            meterNo = meter,
                            presR = pres,
                            prevR = prev,
                            typeOfR = type,
                            principalAmount = principal,
                            amountPayable = amountP,
                        };
                        await App.db.UpdateAsync(table);

                        h.IsChecked = b.IsChecked = false;
                        meterNo.Text = previousR.Text = presentR.Text = string.Empty;

                        string status = "update";

                        var electricity = await App.db.SearchAsync(meter);
                        if (electricity != null)
                        {
                            Application.Current.Properties["status"] = status;
                            Application.Current.Properties["data"] = table.meterNo.ToString();

                            await Navigation.PushAsync(new print());
                        }
                        else
                        {
                            await DisplayAlert("Admin", "Data doesn't exist", "OK");
                        }

                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Admin", "Invalid type of registration", "OK");
                    }

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Admin", "Meter, Present, and Previous must be a Number!", "OK");
                }
            }
            else
            {
                await DisplayAlert("Admin", "Meter, Present, Previous, and Type of Registration must not be Empty!", "OK");
            }
        }
        public async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(meterNo.Text))
            {
                var table = await App.db.SearchAsync(int.Parse(meterNo.Text));
                if (table != null)
                {
                    int meter = int.Parse(meterNo.Text);
                    var electricity = await App.db.SearchAsync(meter);
                    meterNo.Text = previousR.Text = presentR.Text = string.Empty;
                    h.IsChecked = b.IsChecked = false;

                    string status = "delete";

                    if (electricity != null)
                    {
                        Application.Current.Properties["status"] = status;
                        Application.Current.Properties["data"] = electricity.meterNo.ToString();

                        await Navigation.PushAsync(new print());
                    }
                    else
                    {
                        await DisplayAlert("Admin", "Data doesn't exist", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Admin", "Data doesn't exist", "OK");
                }
            }
            else
            {
                await DisplayAlert("Required", "Please Enter Meter Number", "OK");
            }

        }
    }
}
