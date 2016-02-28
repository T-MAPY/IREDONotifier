using System;
using System.ComponentModel;

namespace IREDONotifier.Model
{
    public class User : INotifyPropertyChanged
    {
        private string _regId;
        public string RegId
        {
            get { return _regId; }
            set
            {
                _regId = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        private DateTime _upDateTime;
        public DateTime UpdateTime
        {
            get { return _upDateTime; }
            set
            {
                _upDateTime = value;
                OnPropertyChanged();
            }
        }

        #region PropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
