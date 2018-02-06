/*** 
 * Filename: BaseNotify.cs
 * Description: Extension class for INotifyPropertyChanged
 * Author : Vijay Ganeshkumar
 ***/
using System.ComponentModel;


namespace MySampleApplication.ViewModel
{
    public class BaseNotify: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
