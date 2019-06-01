using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVVM.Models
{
    public class Todo : INotifyPropertyChanged
    {
        //propf tab tab=> skrót do propfull
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; PropertyChange(nameof(Name)); }
        }

        private DateTime beginning;

        public DateTime Beginning
        {
            get { return beginning; }
            set { beginning = value; PropertyChange(nameof(Beginning)); }
        }

        private int numberOfParticipants;

        public int NumberOfParticipants
        {
            get { return numberOfParticipants; }
            set { numberOfParticipants = value; PropertyChange(nameof(NumberOfParticipants)); }
        }

        private int wind_kph;

        public int Wind_kph
        {
            get { return wind_kph; }
            set { wind_kph = value; PropertyChange(nameof(Wind_kph)); }
        }

        public Todo(string name, int number, DateTime beg, int wind_kph)
        {
            Name = name;
            Beginning = beg;
            NumberOfParticipants = number;
            Wind_kph = wind_kph;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        private void PropertyChange(string propertyName)
        {
            if (PropertyChanged!=null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
