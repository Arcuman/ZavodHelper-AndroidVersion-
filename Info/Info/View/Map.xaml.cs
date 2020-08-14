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
    public partial class Map : ContentPage
    {
        public Map(Instruments SelInstrument,string state)
        {
            InitializeComponent();
            BindingContext = new MapVM(Navigation, this, SelInstrument,state);
        }
    }
}