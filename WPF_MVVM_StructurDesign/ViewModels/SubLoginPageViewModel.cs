using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using WPF_MVVM_StructurDesign.Command;
using WPF_MVVM_StructurDesign.Models;
using WPF_MVVM_StructurDesign.Utilities;

namespace WPF_MVVM_StructurDesign.ViewModels
{
    
    class SubLoginPageViewModel : Items
    {
        object[] obj;
        public SubLoginPageViewModel()
        {
        }
        public SubLoginPageViewModel(params object[] obj)
        {
            this.obj = obj;
            CurrentLoginPair = new Items();
            ConfirmCommand = new RelayCommand(Confirm);
        }

        private RelayCommand showLoginPageViewModel;
        public RelayCommand ShowLoginPageViewModel
        {
            get { return new RelayCommand(() => ((MainViewModel)obj[0]).CurrentViewModel = new LoginPageViewModel(obj[0])); }
            set
            {
                showLoginPageViewModel = value;
                OnPropertyChanged("ShowLoginPageViewModel");
            }
        }

        private RelayCommand confirmCommand;
        public RelayCommand ConfirmCommand
        {
            get { return confirmCommand; }
            set
            {
                confirmCommand = value;
                OnPropertyChanged("ConfirmCommand");
            }
        }
        public void Confirm()
        {
            if (LoginHelper.ConnectionStatus)
            {
                if (CurrentLoginPair.User != null && CurrentLoginPair.Password != null && !CurrentLoginPair.User.Equals(string.Empty) && !CurrentLoginPair.Password.Equals(string.Empty))
                {
                    ((MainViewModel)obj[0]).DictionaryLoginPair = new DBServices().RegistrationService(CurrentLoginPair.User, CurrentLoginPair.Password);
                }
                else MessageBox.Show("Input kosong");

            }
            else MessageBox.Show("Database Belum terhubung");

        }

        private Items currentLoginPair;
        public Items CurrentLoginPair
        {
            get { return ((MainViewModel)obj[0]).CurrentLoginPair; }
            set
            {
                ((MainViewModel)obj[0]).CurrentLoginPair = value;
                OnPropertyChanged("CurrentLoginPair");
            }
        }

    }
}
