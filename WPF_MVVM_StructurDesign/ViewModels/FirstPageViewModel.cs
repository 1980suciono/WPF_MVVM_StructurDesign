using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WPF_MVVM_StructurDesign.Command;
using WPF_MVVM_StructurDesign.Models;
using System.Windows;
using System.Windows.Controls;

using WPF_MVVM_StructurDesign.Utilities;
namespace WPF_MVVM_StructurDesign.ViewModels
{
    class FirstPageViewModel:Items
    {
        object[] obj;
        DBServices objDBService;
        public FirstPageViewModel()
        { 
        }
        public FirstPageViewModel(params object[] obj):this()
        {
            this.obj = obj;

            objDBService = ((MainViewModel)obj[0]).objDBService;

            SaveCommand = new RelayCommand(SaveData);
            FindCommand = new RelayCommand(FindData);
            SearchCommand = new RelayCommand(SearchData);
            UpdateCommand = new RelayCommand(UpdateData);
            DeleteCommand = new RelayCommand(DeleteData);
            RefreshCommand = new RelayCommand(LoadData);
            LogoutCommand = new RelayCommand(Logout);
            if(LoginHelper.LoginStatus)LoadData();
        }
        #region Show sub first page 
        private RelayCommand showSubFirstPageCommand;
        public RelayCommand ShowSubFirstPageCommand
        {
            get
            {
                return new RelayCommand(() => ((MainViewModel)obj[0]).CurrentViewModel = new SubFirstPageViewModel(new[] { obj[0], this }));
            }
            set
            {
                showSubFirstPageCommand = value;
                OnPropertyChanged("ShowSubFirstPageCommand");
            }

        }
        #endregion
        #region Show second Page
        private RelayCommand showSecondPageCommand;
        public RelayCommand ShowSecondPageCommand
        {
            get
            {
                return new RelayCommand(() => ((MainViewModel)obj[0]).CurrentViewModel = new SecondPageViewModel(((MainViewModel)obj[0])));
            }
            set
            {
                showSecondPageCommand = value;
                OnPropertyChanged("ShowSecondPageCommand");
            }

        }
        #endregion
        #region loaddata

        public ObservableCollection<Items> ItemList
        {
            get { return ((MainViewModel)obj[0]).ItemList; }
            set
            {
                ((MainViewModel)obj[0]).ItemList = value;
                OnPropertyChanged("ItemList");
            }
        }


        private RelayCommand refreshCommand;
        public RelayCommand RefreshCommand
        {
            get { return refreshCommand; }
            set
            {
                refreshCommand = value;
                OnPropertyChanged("RefreshCommand");
            }

        }
        private void LoadData()
        {
            ItemList = new ObservableCollection<Items>(objDBService.GetAll());
        }

        #endregion

        #region Logout

        private RelayCommand logoutCommand;
        public RelayCommand LogoutCommand
        {
            get { return logoutCommand; }
            set
            {
                logoutCommand = value;
                OnPropertyChanged("LogoutCommand");
            }

        }

        public void Logout()
        {
            new LoginUtil().LogOut();
            ((MainViewModel)obj[0]).CurrentViewModel = ((MainViewModel)obj[0]).objLoginPageViewModel;
        }
        #endregion

        public new string Message => base.Message;

        #region savedata

        
        public Items CurrentItem
        {
            get 
            {
                if (((MainViewModel)obj[0]).CurrentItem == null)
                    ((MainViewModel)obj[0]).CurrentItem= new Items();
                
                    return ((MainViewModel)obj[0]).CurrentItem; 
            }
            set
            {
                ((MainViewModel)obj[0]).CurrentItem= value;
                OnPropertyChanged("CurrentItem");
            }
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
            set
            {
                saveCommand = value;
                OnPropertyChanged("SaveCommand");
            }

        }
         public void SaveData()
        {
            try
            {
                bool isSaved = false,isUpdate=false;
                SearchItem = (CurrentItem.NamaBarang != null) ? CurrentItem.NamaBarang : "";
                ObservableCollection<Items> localItemList = new ObservableCollection<Items>(objDBService.Search(SearchItem));

                if (localItemList == null || !localItemList.Any())
                {
                    isSaved = objDBService.Add(CurrentItem);
                    
                }
                else
                {
                    var result = MessageBox.Show("ditemukan barang yang sama, terus rekam maka dilakukan proses update","Peringatan",MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        isUpdate=objDBService.Update(CurrentItem);
                    }
                }
                 
                if (isSaved||isUpdate)
                {
                    if (isSaved) 
                    {
                        MessageBox.Show("saved");
                    }
                    LoadData();
                }
                else MessageBox.Show("save failled"); 
               
            }
            catch (Exception ex)
            {
                base.Message = ex.ToString();
            }
        }
	#endregion

        #region Finddata
        private RelayCommand findCommand;
        public RelayCommand FindCommand
        {
            get { return findCommand; }
            set
            {
                findCommand = value;
                OnPropertyChanged("FindCommand");
            }
        }

        public void FindData()
        {
            try
            {
                
                var objItems = objDBService.Find(CurrentItem.Id);
                if (objItems != null)
                {
                    CurrentItem.NamaBarang = objItems.NamaBarang;
                    CurrentItem.JumlahBarang = objItems.JumlahBarang;
                    CurrentItem.HargaBarang = objItems.HargaBarang;
                    CurrentItem.TanggalMasuk = objItems.TanggalMasuk;
                }
                else
                {
                    MessageBox.Show( "Item not found");
                }

            }
            catch (Exception ex)
            {
                base.Message = ex.ToString();
            }

        }
        #endregion

        #region searchdata
        private RelayCommand searchCommand;
        public RelayCommand SearchCommand
        {
            get { return searchCommand; }
            set
            {
                searchCommand = value;
                OnPropertyChanged("SearchCommand");
            }
        }

        private string searchItem;
        public string SearchItem
        {
            get { return searchItem; }
            set
            {
                searchItem = value;
                OnPropertyChanged("SearchItem");
            }
        }

        public void SearchData()
        {

            try
            {
                SearchItem = (SearchItem!=null) ? SearchItem : "";
                ItemList = new ObservableCollection<Items>(objDBService.Search(SearchItem));

                if (ItemList == null || !ItemList.Any())
                {
                    MessageBox.Show("Item not found");
                }

            }
            catch (Exception ex)
            {
                base.Message = ex.ToString();
            }

        }
        #endregion

        #region updatedata
        private RelayCommand updateCommand;
        public RelayCommand UpdateCommand
        {
            get { return updateCommand; }
            set
            {
                updateCommand = value;
                OnPropertyChanged("UpdateCommand");
            }
        }
        public void UpdateData()
        {
            try
            {
               
                var isUpdate = objDBService.Update(CurrentItem);

                if (isUpdate)
                {
                    MessageBox.Show("Data Updated");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Update failled");
                }
            }
            catch (Exception ex)
            {
                base.Message = ex.ToString();
            }
        }
        #endregion

        #region deletedata
        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
            set
            {
                deleteCommand = value;
                OnPropertyChanged("DeleteCommand");
            }
        }
        public void DeleteData()
        {
            try
            {
                var isDelete = objDBService.Delete(CurrentItem.Id);

                if (isDelete)
                {
                    MessageBox.Show("Data Deleted");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Deleted failled");
                }
            }
            catch (Exception ex)
            {
                base.Message = ex.ToString();
            }
        }
        #endregion

    }
}
