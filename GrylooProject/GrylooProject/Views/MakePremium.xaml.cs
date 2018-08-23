using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GrylooProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MakePremium : ContentPage
    {
        public MakePremium()
        {
            InitializeComponent();
            Title = "Make Premium";
            
        }

       
        private void txt_Focused(object sender, System.EventArgs e)
        {
            PlanPicker1.Focus();
        }
    }
}