/*** 
 * Filename: MySampleViewModel.cs
 * Description: Handles the download of json data and 
 * command handling from the page.
 * Utilities : 
 * Newtonsoft.Json used for handling and desrializing the json
 * FFImageLoading to lazy load the images.
 * Author : Vijay Ganeshkumar
 ***/



using FFImageLoading.Forms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MySampleApplication
{
    public class MySampleViewModel : INotifyPropertyChanged
    {
        private const string jsonUrl = "https://dl.dropboxusercontent.com/s/2iodh4vg0eortkl/facts.json";
        static int count = 0;
        private HttpClient _client = new HttpClient();
        public ICommand RefreshCommand { protected set; get; }
        public ICommand SortCommand { protected set; get; }
        public event PropertyChangedEventHandler PropertyChanged;
        private ItemsModel _myItems = new ItemsModel();

        public ItemsModel myItems
        {
            get
            {
                return _myItems;
            }
            set
            {
                _myItems = value;
                OnPropertyChanged("myItems");
            }
        }

        protected virtual void OnPropertyChanged(string propName)
        {
             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public MySampleViewModel()
        {
            DoWorkAsync();
            SortCommand = new Command(() =>
            {
                count++;
                Console.WriteLine("Sort Clicked");
                if(count%2 == 0)
                    myItems.rows = new ObservableCollection<rowdetails>(myItems.rows.OrderBy(x => x.title).ToList());
                else
                    myItems.rows = new ObservableCollection<rowdetails>(myItems.rows.OrderByDescending(x => x.title).ToList());
                OnPropertyChanged("myItems");
            });
            RefreshCommand = new Command(() =>
            {
                Console.WriteLine("Refresh Clicked");
                myItems.rows.Clear();
                DoWorkAsync();
            });
        }

        private void DoWorkAsync()
        {
            Task.Run(async () =>
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                };
                String content = await _client.GetStringAsync(jsonUrl);
                myItems = JsonConvert.DeserializeObject<ItemsModel>(content,settings);
                foreach(var yItems in myItems.rows)
                {
                    if (yItems.title == null && yItems.description == null && yItems.imageHref == null)
                        myItems.rows.Remove(yItems);
                }
            });
        }
    }
}
