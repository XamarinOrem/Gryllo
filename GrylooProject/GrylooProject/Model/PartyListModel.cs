using FFImageLoading.Work;
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
    public class PartyListModel:INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;

        public string partyID { get; set; }

        public Xamarin.Forms.ImageSource partyImg { get; set; }

        public string partyName { get; set; }

        public bool voteButtonVisiblity { get; set; }

        public string partySortName { get; set; }

        public string partyVotingStatus { get; set; }

        //public string voteImag { get; set; }

        private Xamarin.Forms.ImageSource _voteImag;

        public Xamarin.Forms.ImageSource voteImag
        {
            get { return _voteImag; }
            set
            {
                _voteImag = value;

                OnPropertyChanged(); // Notify, that Title has been changed
            }
        }



        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
   
}
