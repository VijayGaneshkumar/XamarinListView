/*** 
 * Filename: ItemsModel.cs
 * Description: ViewModel for the main page
 * Author : Vijay Ganeshkumar
 ***/

using MySampleApplication.ViewModel;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace MySampleApplication
{
    public class rowdetails 
    {
        public string title { get; set; }

        public string description { get; set; }

        public string imageHref { get; set; }
    }
    public class ItemsModel : BaseNotify
    {
        private string _title;

        public string title { get { return _title; } set { _title = value;
                OnPropertyChanged("title");
            } }

        public ObservableCollection<rowdetails> rows { get; set; }

    }
}
