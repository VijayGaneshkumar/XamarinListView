﻿/*** 
 * Filename: App.xaml.cs
 * Description: App Page to handle the states of application life cycle
 * Author : Vijay Ganeshkumar
 ***/

using Plugin.Connectivity;
using Xamarin.Forms;

namespace MySampleApplication
{
	public partial class App : Application
	{
		public App ()
		{
            InitializeComponent();
            MainPage = new NavigationPage(new FormsListView());
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
