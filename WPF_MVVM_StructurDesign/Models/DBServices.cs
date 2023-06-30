using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.Globalization;


using WPF_MVVM_StructurDesign.Utilities;

namespace WPF_MVVM_StructurDesign.Models
{
    class DBServices
    {
        private List<Items> objItemsList;

        Items item;
        private OleDbConnection objSqlConnection;
        private OleDbCommand objSqlCommand;

        public DBServices()
        {
            objItemsList = new List<Items>();
            item = new Items();
            #region instatiate koneksi tergantung library yg dipake
            DoOleDbConnection();
            #endregion

        }

     
        public void DoOleDbConnection()
        {
          
            objSqlConnection = new OleDbConnection(LoginHelper.ConnectionString);

            objSqlCommand = new OleDbCommand
            {
                Connection = objSqlConnection,
                CommandType = CommandType.StoredProcedure
            };

        }
      
        public List<Items> GetAll()
        {
            try
            {
                objItemsList = new List<Items>();
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "SelectAllItems";

                objSqlConnection.Open();

                var objSqlDataReader = objSqlCommand.ExecuteReader();
                if (objSqlDataReader.HasRows)
                {
                    while (objSqlDataReader.Read())
                    {
                        item = new Items();
                        item.Id = objSqlDataReader.GetInt32(0);
                        item.NamaBarang = objSqlDataReader.GetString(1);
                        item.JumlahBarang = objSqlDataReader.GetString(2);
                        item.HargaBarang= objSqlDataReader.GetString(3);
                        item.TanggalMasuk = objSqlDataReader.GetDateTime(4);
                        objItemsList.Add(item);
                    }

                }
                
                objSqlDataReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSqlConnection.Close();
            }
            return objItemsList;
        }
        public bool Add(Items objItems)
        {
            bool isAdded = false;
            if (objItems != null && !objItems.Id.Equals(string.Empty))
            {
                try
                {

                    objSqlCommand.Parameters.Clear();
                    objSqlCommand.CommandText = "InsertItems";
                    objSqlCommand.Parameters.AddWithValue("@NamaBarang", objItems.NamaBarang);
                    objSqlCommand.Parameters.AddWithValue("@JumlahBarang", new CurrencyUtil().RectifiedFormat(objItems.JumlahBarang));
                    objSqlCommand.Parameters.AddWithValue("@HargaBarang", new CurrencyUtil().RectifiedFormat(objItems.HargaBarang, false));
                    objSqlCommand.Parameters.AddWithValue("@TanggalMasuk",DateTime.Now.Date);

                    objSqlConnection.Open();
                    var numberOfRowsAffected = objSqlCommand.ExecuteNonQuery();
                    if (numberOfRowsAffected > 0)
                    {

                        isAdded = true;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objSqlConnection.Close();
                }
            }
            return isAdded;

        }

        public bool Update(Items objItems)
        {
            bool isUpdate = false;
            if (objItems != null && !objItems.Id.Equals(string.Empty))
            {
                try
                {
                    objSqlCommand.Parameters.Clear();
                    objSqlCommand.CommandText = "UpdateItemById";
                    objSqlCommand.Parameters.AddWithValue("@Id", objItems.Id);
                    objSqlCommand.Parameters.AddWithValue("@NamaBarang", objItems.NamaBarang);
                    objSqlCommand.Parameters.AddWithValue("@JumlahBarang", new CurrencyUtil().RectifiedFormat(objItems.JumlahBarang));
                    objSqlCommand.Parameters.AddWithValue("@HargaBarang", new CurrencyUtil().RectifiedFormat(objItems.HargaBarang, false));
                    objSqlCommand.Parameters.AddWithValue("@TanggalMasuk", objItems.TanggalMasuk);
                    objSqlConnection.Open();

                    var numberOfRowsAffected = objSqlCommand.ExecuteNonQuery();
                    if (numberOfRowsAffected > 0)
                    {
                        MessageBox.Show("Id " + objItems.Id + " Updated");
                        isUpdate = true;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objSqlConnection.Close();
                }
            }

            return isUpdate;

        }

        public bool Delete(int id)
        {
            bool isDeleted = false;
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "DeleteItemById";
                objSqlCommand.Parameters.AddWithValue("@Id", id);

                objSqlConnection.Open();

                var numberOfRowsAffected = objSqlCommand.ExecuteNonQuery();
                if (numberOfRowsAffected > 0)
                {
                    isDeleted = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSqlConnection.Close();
            }

            return isDeleted;

        }
        public Items Find(int id)
        {
            item = new Items();
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "SelectItemById";
                objSqlCommand.Parameters.AddWithValue("@Id", id);
                objSqlConnection.Open();
                
                var objSqlDataReader = objSqlCommand.ExecuteReader();

                if (objSqlDataReader.HasRows)
                {
                    objSqlDataReader.Read();
                    item = new Items();
                    item.Id = objSqlDataReader.GetInt32(0);
                    item.NamaBarang = objSqlDataReader.GetString(1);
                    item.JumlahBarang = objSqlDataReader.GetString(2);
                    item.HargaBarang= objSqlDataReader.GetString(3);
                    item.TanggalMasuk = objSqlDataReader.GetDateTime(4);
                    objItemsList.Add(item);
                }
                objSqlDataReader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSqlConnection.Close();
            }
            return item;

        }

        public List<Items> Sort(DateTime date1,DateTime date2)
        {
            item = new Items();

            DataTable dt = new DataTable();
            OleDbDataAdapter da;
            try
            {
                
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "SelectItemByInputDate";
                objSqlCommand.Parameters.AddWithValue("@TanggalMasuk1", date1);
                objSqlCommand.Parameters.AddWithValue("@TanggalMasuk2", date2);
                objSqlConnection.Open();
                
                da =new OleDbDataAdapter(objSqlCommand);

                da.Fill(dt);

                objItemsList = new List<Items>();
                for (int y = 0; y< dt.Rows.Count; y++)
                {
                    item = new Items();
                    item.Id = (int)dt.Rows[y]["Id"];
                    item.NamaBarang = dt.Rows[y]["NamaBarang"].ToString();
                    item.JumlahBarang =dt.Rows[y]["JumlahBarang"].ToString();
                    item.HargaBarang = dt.Rows[y]["HargaBarang"].ToString();
                    item.TanggalMasuk = (DateTime)dt.Rows[y]["TanggalMasuk"];
                   
                    objItemsList.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSqlConnection.Close();
            }
            return objItemsList;
        }

        public List<Items> Search(string param)
        {
            try
            {
                
                objItemsList = new List<Items>();

                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "SelectItemByParams";
                objSqlCommand.Parameters.AddWithValue("@NamaBarang", '%' + param + '%');
                objSqlCommand.Parameters.AddWithValue("@HargaBarang", param);
                objSqlCommand.Parameters.AddWithValue("@JumlahBarang",  param );                          

                objSqlConnection.Open();

                var objSqlDataReader = objSqlCommand.ExecuteReader();

                if (objSqlDataReader.HasRows)
                {
                    Items item = null;
                    while (objSqlDataReader.Read())
                    {
                        item = new Items();
                        item.Id = objSqlDataReader.GetInt32(0);
                        item.NamaBarang = objSqlDataReader.GetString(1);
                        item.JumlahBarang = objSqlDataReader.GetString(2);
                        item.HargaBarang= objSqlDataReader.GetString(3);
                        item.TanggalMasuk = objSqlDataReader.GetDateTime(4);
                        objItemsList.Add(item);
                    }

                }
                objSqlDataReader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSqlConnection.Close();
            }
            return objItemsList;
        }


        public Items FindPair(string username)
        {
            item = new Items();
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "SelectItemByUser";
                objSqlCommand.Parameters.AddWithValue("@Username", username);
                objSqlConnection.Open();

                var objSqlDataReader2 = objSqlCommand.ExecuteReader();

                if (objSqlDataReader2.HasRows)
                {
                    objSqlDataReader2.Read();
                    item = new Items();
                    item.IdLogin = objSqlDataReader2.GetInt32(0);
                    item.User = objSqlDataReader2.GetString(1);
                    item.Password = objSqlDataReader2.GetString(2);

                    objItemsList.Add(item);
                }
                objSqlDataReader2.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSqlConnection.Close();
            }
            return item;

        }

        public bool AddPair(Items objItems)
        {
            bool isAdded = false;
            if (objItems != null && !objItems.User.Equals(string.Empty))
            {
                try
                {
                    objSqlCommand.Parameters.Clear();
                    objSqlCommand.CommandText = "InsertItemPair";
                    objSqlCommand.Parameters.AddWithValue("@Username", objItems.User);
                    objSqlCommand.Parameters.AddWithValue("@Pass", objItems.Password);

                    objSqlConnection.Open();
                    var numberOfRowsAffected = objSqlCommand.ExecuteNonQuery();
                    if (numberOfRowsAffected > 0)
                    {

                        isAdded = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objSqlConnection.Close();
                }
            }
            return isAdded;

        }

        public bool LoginService(string key, string value)
        {
            item = FindPair(key);
            if (item != null && item.User != null && !item.User.Equals(string.Empty))
            {
                if (item.User.Equals(key) && item.Password.Equals(new LoginUtil().Sha1Hash(value))) return true;
            }
            return false;
        }

        public Dictionary<string, string> RegistrationService(string key, string value)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            item = FindPair(key);
            if (item == null || item.User == null || item.User.Equals(string.Empty))
            {
                new LoginUtil(dict).Registrasi(new[] { key, value });//problem
                item = new Items();
                item.User = key;
                item.Password = dict.ElementAt(0).Value;
                if (!AddPair(item))
                {
                    dict.Clear();
                    LoginHelper.Message = "Registrasi gagal, silahkan coba lagi";
                }

            }
            else LoginHelper.Message = "username sudah terdaftar";

            MessageBox.Show(LoginHelper.Message);

            return dict;
        }
    }
}
