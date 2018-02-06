/*** 
 * Filename: MySampleViewModel.cs
 * Description: Handles the download of json data and 
 * command handling from the page.
 * Utilities : 
 * Newtonsoft.Json used for handling and desrializing the json
 * FFImageLoading to lazy load the images.
 * Author : Vijay Ganeshkumar
 ***/

using Newtonsoft.Json;
using Acr.UserDialogs;
using Plugin.Connectivity;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MySampleApplication
{
    public class MySampleViewModel : INotifyPropertyChanged
    {
        private const string jsonUrl = "https://dl.dropboxusercontent.com/s/2iodh4vg0eortkl/facts.json";
        static int count = 0;
        public ICommand RefreshCommand { protected set; get; }
        public ICommand SortCommand { protected set; get; }
        private HttpClient _client = new HttpClient();

        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        private bool _isEnabled = false;
        public bool isEnabled
        { 
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                OnPropertyChanged("isEnabled");
            }
        }
                
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
            //check and probhibit the json data fetch when no data connectivity is present.
            if (CrossConnectivity.Current.IsConnected)
            {
                isEnabled = true;
                try
                {
                    // Background thread to download and deserialize the json data
                   Task.Run(async () =>
                   {
                        IsBusy = true;
                        var settings = new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                        };
                        String content = await _client.GetStringAsync(jsonUrl);
                        myItems = JsonConvert.DeserializeObject<ItemsModel>(content, settings);
                        IsBusy = false;
                         //ToDo: null value rjection setting is not handling as expected.
                         // workaround to remove when all 3 fields are null.
                        foreach (var yItems in myItems.rows)
                        {
                            if (yItems.title == null && yItems.description == null && yItems.imageHref == null)
                                myItems.rows.Remove(yItems);
                        }
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine("UnExpected Response {0}", ex.Message);
                }
            }
            else
            {
                isEnabled = false;
                UserDialogs.Instance.Alert("Alert", "Please enable data conenctivity to begin", "OK");
            }
        }
    }
}

