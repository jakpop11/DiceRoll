using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiceRoll
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        // Constructor of AboutPage which shares ViewModel from another page
        public AboutPage(object bindableObject)
        {
            InitializeComponent();
            BindingContext = bindableObject;
        }
    }
}