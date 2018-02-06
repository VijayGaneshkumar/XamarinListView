/*** 
 * Filename: MySampleViewModel.cs
 * Description: Handles the drawing of the main page and sets the binding context. 
 * Author : Vijay Ganeshkumar
 ***/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*** 
 * Filename: FormsListView.xaml.cs
 * Description: Listview for application
 * Author : Vijay Ganeshkumar
 ***/
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace MySampleApplication
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FormsListView : ContentPage
	{
        public FormsListView ()
		{
			InitializeComponent();
        }
    }
}