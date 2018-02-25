using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AnimationDemo
{
	public partial class MainPage : ContentPage
	{
        Button qr;
        Button directory;
        bool isOpen;
        

        public MainPage()
		{
			InitializeComponent();

            OpenOptions.Clicked += OpenOptions_Clicked;
		}

        private void CustomButton_Clicked(object sender, EventArgs e)
        {
            ePay.Animate("Custom Animation", x =>
            {
                ePay.BackgroundColor = Color.FromRgb(x, 0, 1 - x);
                ePay.Scale = 1 + 1.1 * x;
            }, length: 500);
        }

        private async void OpenOptions_Clicked(object sender, EventArgs e)
        {
            if (isOpen)
            {
                var c2 = stOptions.Bounds;
                var b2 = stOptions.Bounds;
                await directory.ScaleTo(0, 150, Easing.CubicOut);
                await qr.ScaleTo(0, 150, Easing.CubicOut);
                stOptions.Children.Remove(directory);
                stOptions.Children.Remove(qr);
                 
                c2.Width = OpenOptions.Width;
                c2.X = c2.X + c2.Width;
                await OpenOptions.LayoutTo(c2, 150, Easing.Linear);
                ePay.IsVisible = true;
                b2.Width = ePay.Bounds.Width;
                await ePay.LayoutTo(b2, 150, Easing.Linear);

                isOpen = false;
                return;
            }

            var b = stOptions.Bounds;
            b.X = b.X - ePay.Bounds.Width*2;
            b.Width = ePay.Bounds.Width;
            await ePay.LayoutTo(b, 250, Easing.Linear);
            
            var c = stOptions.Bounds;
            c.Width = OpenOptions.Width;
            await OpenOptions.LayoutTo(c, 250, Easing.Linear);
            ePay.IsVisible = false;
            BuildButtons();

            stOptions.Children.Add(directory);
            stOptions.Children.Add(qr);
            await directory.ScaleTo(1, 500, Easing.CubicOut);
            await qr.ScaleTo(1, 500, Easing.CubicOut);
            isOpen = true;
        }

        void BuildButtons()
        {
            directory = new Button()
            {
                Text = "Directory",
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Scale = 0
            };
            qr = new Button()
            {
                Text = "QR",
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Scale = 0
            };
        }
    }
}
