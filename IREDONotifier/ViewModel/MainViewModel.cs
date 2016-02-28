using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Input;
using IREDONotifier.Command;
using IREDONotifier.Database;
using IREDONotifier.Model;
using NLog;
using System.Configuration;

namespace IREDONotifier.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private ObservableCollection<User> _users = new ObservableCollection<User>();
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set { _users = value; OnPropertyChanged(); }
        }

        private IList _gridSelectedUsers = new ArrayList();
        public IList GridSelectedUsers
        {
            get { return _gridSelectedUsers; }
            set { _gridSelectedUsers = value; OnPropertyChanged(); }
        }

        private string _notificationText;
        public string NotificationText
        {
            get { return _notificationText; }
            set { _notificationText = value; OnPropertyChanged(); }
        }

        private string _topicText;
        public string TopicText
        {
            get { return _topicText; }
            set { _topicText = value; OnPropertyChanged(); }
        }

        public ICommand SendCmd { get; private set; }

        public MainViewModel()
        {
            SendCmd = new SendCmd(this);

            var loader = new MySqlLoader();
            Users = loader.GetAllUsers();
        }

        internal bool CanSend()
        {
            return (GridSelectedUsers.Count > 0 || !string.IsNullOrWhiteSpace(TopicText)) && !string.IsNullOrWhiteSpace(NotificationText);
        }

        internal void Send()
        {
            if (!string.IsNullOrWhiteSpace(TopicText))
            {
                var json = "{ \"data\": { \"message\": \"" + NotificationText + "\" }, \"to\" : \"/topics/" + TopicText + "\"}";

                var result = SendMessage(json);

                Logger.Debug($"Zpráva do tématu '{TopicText}' odeslána s výsledkem: " + result);
                MessageBox.Show(result, $"Zpráva do tématu '{TopicText}' odeslána s výsledkem:");
            }
            else
                foreach (var user in Users)
                {
                    var json = "{ \"data\": { \"message\": \"" + NotificationText + "\" }, \"to\" : \"" + user.RegId + "\"}";

                    var result = SendMessage(json);

                    Logger.Debug($"Zpráva pro '{user.Email}' odeslána s výsledkem: " + result);
                    MessageBox.Show(result, $"Zpráva pro '{user.Email}' odeslána s výsledkem:");
                }
            NotificationText = "";
            TopicText = "";
        }

        internal string SendMessage(string json)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Convert.ToString((ConfigurationManager.AppSettings["GCM_URL"])));
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers["Authorization"] = "key=" + Convert.ToString((ConfigurationManager.AppSettings["GOOGLE_API_KEY"]));

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                return streamReader.ReadToEnd();
            }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
