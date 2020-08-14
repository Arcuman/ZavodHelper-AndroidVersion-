using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Info
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoPage : ContentPage
    {
        public InfoPage()
        {
            InitializeComponent();
            BindingContext = new InfoVM(Navigation, this);
        }
        public InfoPage(string state)
        {
            InitializeComponent();
            BindingContext = new InfoVM(Navigation, this,state);
        }
    }
}