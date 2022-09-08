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
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
        }


        // Constructor of HistoryPage which shares ViewModel from another page
        public HistoryPage(object bindableObject)
        {
            InitializeComponent();
            BindingContext = bindableObject;
        }
    }
}