using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace TravelRecordApp.ViewModel
{
    class HomeViewModel
    {
        public NewTravelCommand NewTravelCommand { get; set; }
        public HomeViewModel()
        {
            NewTravelCommand = new NewTravelCommand(this);
        }
        public void NewTravelNavigation()
        {
            App.Current.MainPage.Navigation.PushAsync(new NewTravelPage());
        }
    }
    class NewTravelCommand : ICommand
    {
        private HomeViewModel vM;
        public NewTravelCommand(HomeViewModel vm)
        {
            vM = vm;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            vM.NewTravelNavigation();
        }
    }
}
