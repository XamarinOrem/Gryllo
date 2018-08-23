using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GrylooProject.Model
{
    public class rateLeaderListItem
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public string id { get; set; }
        public UriImageSource image { get; set; }

        public String Image { get; set; }
        public string leaderName { get; set; }
        public string rateCount { get; set; }

        public string associatedParty { get; set; }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
