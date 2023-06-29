using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


using WPF_MVVM_StructurDesign.Command;
using WPF_MVVM_StructurDesign.Models;
using WPF_MVVM_StructurDesign.Utilities;


namespace WPF_MVVM_StructurDesign.ViewModels
{
    class SecondPageViewModel:Items
    {
        object[] obj;
        public SecondPageViewModel()
        {
        }
        public SecondPageViewModel(params object[] obj) : this()
        {
            this.obj = obj;
            ShowFirstPageCommand = new RelayCommand(() => ((MainViewModel)obj[0]).CurrentViewModel = ((MainViewModel)obj[0]).objFirstPageViewModel);
            ShowPrintDialogCommand = new RelayCommand(DoPrint);
        }

        private RelayCommand showFirstPageCommand;
        public RelayCommand ShowFirstPageCommand
        {
            get
            {
                return showFirstPageCommand;
            }
            set
            {
                showFirstPageCommand = value;
                OnPropertyChanged("ShowFirstPageCommand");
            }

        }

        private RelayCommand showPrintDialogCommand;
        public RelayCommand ShowPrintDialogCommand
        {
            get
            {
                return showPrintDialogCommand;
            }
            set
            {
                showPrintDialogCommand = value;
                OnPropertyChanged("showPrintDialogCommand");
            }

        }

        public void DoPrint()
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {

                PrintDocument printDocument = new PrintDocument();
                printDocument.PrintPage += new PrintPageEventHandler(PrintPageSource);            
                printDocument.Print();
            }
        }

        public void PrintPageSource(object sender, PrintPageEventArgs e)
        {
            Font printFont = new Font(FontFamily.GenericMonospace, 10);
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            String line = "";
            if (((MainViewModel)obj[0]).ItemList != null)
            {
                foreach (Items i in ((MainViewModel)obj[0]).ItemList)
                {

                    line += new StringUtil().SpaceRight(i.Id.ToString(), 4, 2) +
                      new StringUtil().SpaceRight(i.NamaBarang, 15, 2) + 
                      new StringUtil().SpaceLeft(i.JumlahBarang, 10, 2) + 
                      new StringUtil().SpaceLeft(i.HargaBarang, 15, 2)+"\n";

                }
            }
            else
            {
                MessageBox.Show("Printing Failed!");
                return;
            }
           

            linesPerPage = e.MarginBounds.Height/printFont.GetHeight(e.Graphics);

            
            yPos = topMargin + (count * printFont.GetHeight(e.Graphics));
            e.Graphics.DrawString(line, printFont, System.Drawing.Brushes.Black,leftMargin, yPos, new StringFormat());
          
            e.HasMorePages = false;
        }

        public Items CurrentItem
        {
            get { return ((MainViewModel)obj[0]).CurrentItem; } 
            set
            {
                ((MainViewModel)obj[0]).CurrentItem = value;
                OnPropertyChanged("CurrentItem");
            }
        }

        public ObservableCollection<Items> ItemList
        {
            get { return ((MainViewModel)obj[0]).ItemList; }
            set
            {
                ((MainViewModel)obj[0]).ItemList = value;
                OnPropertyChanged("ItemList");
            }
        }
    }
}
