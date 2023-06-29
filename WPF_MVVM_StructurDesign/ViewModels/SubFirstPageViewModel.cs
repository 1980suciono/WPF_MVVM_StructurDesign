using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using Microsoft.Reporting.WinForms;
using System.Windows.Forms.Integration;
using WPF_MVVM_StructurDesign.Command;
using WPF_MVVM_StructurDesign.Models;
using System.Data;
using System.Windows;
using System.Collections.ObjectModel;

namespace WPF_MVVM_StructurDesign.ViewModels
{
    class SubFirstPageViewModel : Items
    {
        object[] obj;
        DBServices objDBService;
        ReportViewer reportViewer;
        ReportDataSource rds;
        DataSet ds;
        DataTable dt;
       
        public SubFirstPageViewModel()
        {
           
        }
        public SubFirstPageViewModel(params object[] obj) : this()
        {
            this.obj = obj;
            objDBService = ((MainViewModel)obj[0]).objDBService;

            ShowFirstPageCommand = new RelayCommand(()=>((MainViewModel)obj[0]).CurrentViewModel=((FirstPageViewModel)obj[1]));
            ShortDateCommand = new RelayCommand(SearchDate);

            ds = new DataSet();
            ds.Tables.Add("DataSet1");
            dt = new DataTable();

            dt = ds.Tables[0];

            dt.Columns.Add(new DataColumn("Id", typeof(Int32)));
            dt.Columns.Add(new DataColumn("NamaBarang", typeof(string)));
            dt.Columns.Add(new DataColumn("JumlahBarang", typeof(string)));
            dt.Columns.Add(new DataColumn("HargaBarang", typeof(string)));
            dt.Columns.Add(new DataColumn("TanggalMasuk", typeof(DateTime)));


            WindowsFormsHost windowsFormsHost = new WindowsFormsHost();
            reportViewer = new ReportViewer();
            windowsFormsHost.Child = reportViewer;
            Viewer = windowsFormsHost;
            reportViewer.LocalReport.ReportEmbeddedResource = "WPF_MVVM_StructurDesign.Report1.rdlc";
            rds = new ReportDataSource();
            rds.Name = "DataSet1";
            RefreshReportDataSource();
        }

        private WindowsFormsHost viewer;
        public WindowsFormsHost Viewer
        {
            get { return viewer; }
            set
            {
                viewer = value;
                OnPropertyChanged("Viewer");
            }
        }

        public void RefreshReportDataSource()
        {
            dt.Rows.Clear();
            int j = 0;
            foreach (Items i in ((MainViewModel)obj[0]).ItemList)
            {
                dt.Rows.Add(i.Id, i.NamaBarang, i.JumlahBarang, i.HargaBarang, i.TanggalMasuk);
                j++;
            }

            rds.Value = dt;
        

            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(rds);
            reportViewer.RefreshReport();
        }

        private RelayCommand shortDateCommand;
        public RelayCommand ShortDateCommand
        {
            get { return shortDateCommand; }
            set
            {

                shortDateCommand = value;
                OnPropertyChanged("ShortDateCommand");
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

        public void SearchDate()
        {

            try
            {
                DateTimeItem1 = DateTimeItem1 != null ? DateTimeItem1 : DateTime.Now;
                DateTimeItem2 = DateTimeItem2 != null ? DateTimeItem2 : DateTime.Now;
                
                 ((MainViewModel)obj[0]).ItemList = new ObservableCollection<Items>(objDBService.Sort(DateTimeItem1, DateTimeItem2));
                
                if (((MainViewModel)obj[0]).ItemList == null || !((MainViewModel)obj[0]).ItemList.Any())
                {
                    MessageBox.Show("Item not found");
                }
                RefreshReportDataSource();
            }
            catch (Exception ex)
            {
                base.Message = ex.ToString();
            }

        }

        
        public DateTime DateTimeItem1
        {
            get { return ((MainViewModel)obj[0]).DateTimeItem1; }
            set
            {
                ((MainViewModel)obj[0]).DateTimeItem1 = value;
                OnPropertyChanged("DateTimeItem1");
            }
        }

       
        public DateTime DateTimeItem2
        {
            get { return ((MainViewModel)obj[0]).DateTimeItem2; }
            set
            {
                ((MainViewModel)obj[0]).DateTimeItem2 = value;
                OnPropertyChanged("DateTimeItem2");
            }
        }

        public new string Message => base.Message;

        private RelayCommand showFirstPageCommand;
        public RelayCommand ShowFirstPageCommand
        {
            get { return showFirstPageCommand; }
            set
            {

                showFirstPageCommand = value;
                OnPropertyChanged("ShowFirstPageCommand");
            }

        }

    }
}
