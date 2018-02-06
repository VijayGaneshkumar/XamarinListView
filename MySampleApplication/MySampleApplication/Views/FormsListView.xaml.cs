/*** 
 * Filename: MySampleViewModel.cs
 * Description: Handles the drawing of the main page and sets the binding context. 
 * Author : Vijay Ganeshkumar
 ***/

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
        public FormsListView ()
		{
			InitializeComponent();
            MySampleViewModel myViewmModel = new MySampleViewModel();
            this.BindingContext = myViewmModel;
        }
    }
}