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
    class LoginPageViewModel : Items
    {
        object[] obj;
        public LoginPageViewModel()
        {
        }
        public LoginPageViewModel(params object[] obj) : this()
        {
            this.obj = obj;
            LoginCommand = new RelayCommand(Login);
            ConnectionCommand = new RelayCommand(CreateConnection);
        }

        private RelayCommand showSubLoginPageViewModelCommand;
        public RelayCommand ShowSubLoginPageViewModelCommand
        {
            get { return new RelayCommand(() => ((MainViewModel)obj[0]).CurrentViewModel = new SubLoginPageViewModel(new[] { obj[0], this })); }
            set
            {
                showSubLoginPageViewModelCommand = value;
                OnPropertyChanged("ShowSubLoginPageViewModelCommand");
            }
        }

        private RelayCommand connectionCommand;
        public RelayCommand ConnectionCommand
        {
            get { return connectionCommand; }
            set
            {
                connectionCommand = value;
                OnPropertyChanged("ConnectionCommand");
            }
        }


        public async void CreateConnection()
        {
            string str = (await new ConnectionUtil().CreateStringConnectionAsync()).StringConnection;
            if (str!=null&&!str.Equals(string.Empty))
            {
                LoginHelper.ConnectionString = str;
                LoginHelper.ConnectionStatus = true;
                MessageBox.Show("Connection Created");
            }
            else MessageBox.Show("Create Connection please");


        }


        private RelayCommand loginCommand;
        public RelayCommand LoginCommand
        {
            get { return loginCommand; }
            set
            {
                loginCommand = value;
                OnPropertyChanged("LoginCommand");
            }
        }

        public void Login()
        {
            if (LoginHelper.ConnectionStatus)
            {
                if (CurrentLoginPair.User != null && CurrentLoginPair.Password != null && !CurrentLoginPair.User.Equals(string.Empty) && !CurrentLoginPair.Password.Equals(string.Empty))
                {
                    ((MainViewModel)obj[0]).objDBService = new DBServices();
                    LoginHelper.LoginStatus = ((MainViewModel)obj[0]).objDBService.LoginService(CurrentLoginPair.User, CurrentLoginPair.Password);
                    ((MainViewModel)obj[0]).objFirstPageViewModel = new FirstPageViewModel(obj[0]);
                    if (LoginHelper.LoginStatus) ((MainViewModel)obj[0]).CurrentViewModel = ((MainViewModel)obj[0]).objFirstPageViewModel;
                    else MessageBox.Show("Maaf username dan password tidak sesuai");
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
