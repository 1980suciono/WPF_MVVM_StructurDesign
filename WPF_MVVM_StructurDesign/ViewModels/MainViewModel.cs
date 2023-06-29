using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WPF_MVVM_StructurDesign.Models;
using System.Windows;
using WPF_MVVM_StructurDesign.Utilities;

namespace WPF_MVVM_StructurDesign.ViewModels
{
    class MainViewModel:Items
    {
        public FirstPageViewModel objFirstPageViewModel;
        public SecondPageViewModel objSecondPageViewModel;
        public LoginPageViewModel objLoginPageViewModel;
        public DBServices objDBService;
        public MainViewModel()
        {
            
            // MessageBox.Show(new CurrencyUtil().RectifiedFormat("7987.675,,657,89", false), "testing");
            //MessageBox.Show(new CurrencyUtil().CountRupiah(Convert.ToDouble(new CurrencyUtil().RectifiedFormat("7987.675,,657,89", false))), "testing");
            DateTimeItem1 = DateTime.Now.Date;
            DateTimeItem2 = DateTime.Now.Date;
            ObservableCollection<Items> ItemList = new ObservableCollection<Items>();
            CurrentLoginPair = new Items() { IdLogin = 0, User = "", Password = "" };
            DictionaryLoginPair = new Dictionary<string, string>();
            objDBService = new DBServices();
            CurrentItem = new Items() { Id = 0, NamaBarang="",JumlahBarang="",HargaBarang="",TanggalMasuk=DateTime.Now };
            if (objFirstPageViewModel == null)
            {
                objLoginPageViewModel = new LoginPageViewModel(this);
                objFirstPageViewModel = new FirstPageViewModel(this);
                objSecondPageViewModel = new SecondPageViewModel(this);
            }
            if (LoginHelper.LoginStatus)
            {
                            
                CurrentViewModel = objFirstPageViewModel;
            }
            else 
            {
                                
                CurrentViewModel = objLoginPageViewModel;
            }
           
        }

        private Dictionary<string, string> dictionaryLoginPair;
        public Dictionary<string, string> DictionaryLoginPair
        {
            get { return dictionaryLoginPair; }
            set
            {
                dictionaryLoginPair = value;
                OnPropertyChanged("DictionaryLoginPair");
            }
        }

        private Items currentLoginPair;
        public Items CurrentLoginPair
        {
            get { return currentLoginPair; }
            set
            {
                currentLoginPair = value;
                OnPropertyChanged("CurrentLoginPair");
            }
        }

        private DateTime dateTimeItem1;
        public DateTime DateTimeItem1
        {
            get { return dateTimeItem1; }
            set
            {
                dateTimeItem1 = value;
                OnPropertyChanged("DateTimeItem1");
            }
        }

        private DateTime dateTimeItem2;
        public DateTime DateTimeItem2
        {
            get { return dateTimeItem2; }
            set
            {
                dateTimeItem2 = value;
                OnPropertyChanged("DateTimeItem2");
            }
        }

        private Items currentItem;
        public Items CurrentItem
        {
            get{return currentItem; }
            set
            {
                currentItem = value;
                OnPropertyChanged("CurrentItem");
            }
        }

        private ObservableCollection<Items> itemList;
        public ObservableCollection<Items> ItemList
        {
            get{ return itemList; }
            set
            {
                if (value == itemList) return;
                foreach (Items i in value)
                {
                    double dblVal = 0;
                    Double.TryParse(new CurrencyUtil().RectifiedFormat(i.HargaBarang), out dblVal);
                    i.HargaBarang = dblVal.ToString("N2");
                }

                itemList = value;
                OnPropertyChanged("ItemList");
            }
        }


        private object currentViewModel;
        public object CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                currentViewModel = value;
                OnPropertyChanged("CurrentViewModel");
            }
        }
    }
}
