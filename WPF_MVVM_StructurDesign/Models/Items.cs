using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WPF_MVVM_StructurDesign.Models
{
    class Items : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string message;
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }

        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        private string namaBarang;
        public string NamaBarang
        {
            get { return namaBarang; }
            set
            {
                namaBarang = value;
                OnPropertyChanged("NamaBarang");
            }
        }
        private string jumlahBarang;
        public string JumlahBarang
        {
            get { return jumlahBarang; }
            set
            {
                jumlahBarang = value;
                OnPropertyChanged("JumlahBarang");
            }
        }
        private string hargaBarang;
        public string HargaBarang
        {
            get { return hargaBarang; }
            set
            {
                hargaBarang = value;
                OnPropertyChanged("HargaBarang");
            }
        }
        private DateTime tanggalMasuk;
        public DateTime TanggalMasuk
        {
            get { return tanggalMasuk; }
            set
            {
                tanggalMasuk = value;
                OnPropertyChanged("TanggalMasuk");
            }
        }

        private int idLogin;
        public int IdLogin
        {
            get { return idLogin; }
            set
            {
                idLogin = value;
                OnPropertyChanged("IdLogin");
            }
        }

        private string user;
        public string User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

    }
}
