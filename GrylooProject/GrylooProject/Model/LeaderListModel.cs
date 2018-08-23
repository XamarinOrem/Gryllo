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
    public class LeaderListModel:INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public string leaderID { get; set; }

        public string fontSize { get; set; }

        public ImageSource leaderImg { get; set; }

        public string leaderName { get; set; }

        public bool labelStatus { get; set; }

        public bool starButtonStatus { get; set; }
      //  public string rateImg { get; set; }

        private ImageSource _rateImg;

        public ImageSource rateImg
        {
            get { return _rateImg; }
            set
            {
                _rateImg = value;

                OnPropertyChanged(); // Notify, that Title has been changed
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
