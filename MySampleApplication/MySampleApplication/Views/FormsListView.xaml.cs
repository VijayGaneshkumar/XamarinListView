using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using FFImageLoading.Forms;

namespace MySampleApplication
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FormsListView : ContentPage
	{
        public ObservableCollection<ItemsViewModel> veggies { get; set; }
        private const string jsonUrl = "https://dl.dropboxusercontent.com/s/2iodh4vg0eortkl/facts.json";
        private HttpClient _client = new HttpClient();
        ItemsViewModel myItems;
        public FormsListView ()
		{
			InitializeComponent();
            OnGetListDetails();
        }
        private Task<ItemsViewModel> DoWorkAsync()
        {
            TaskCompletionSource<ItemsViewModel> tcs = new TaskCompletionSource<ItemsViewModel>();
            Task.Run(async () =>
            {
                var content = await _client.GetStringAsync(jsonUrl);
                myItems = JsonConvert.DeserializeObject<ItemsViewModel>(content);
                tcs.SetResult(myItems);
            });
            return tcs.Task;
        }

        protected async void OnGetListDetails()
        {

            CachedImage ffImage = this.FindByName<CachedImage>("ffImage");
            var tr = await DoWorkAsync();
            ObservableCollection<rowdetails> listItems = new ObservableCollection<rowdetails>();
            foreach (var item in myItems.rows)
            {
                if (item.imageHref == null)
                    item.imageHref = "error.png";
                if (item.title != null)
                    listItems.Add(item);

            }
            myList.ItemsSource = listItems;
            int itemCount = listItems.Count;

        }
    }
}