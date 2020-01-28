using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_NoteBook_Linear_Equations
{
    public class ManageDb
    {
        OleDbConnection Conn;
        OleDbCommand Cmd;
        public ManageDb()
        {
            string ExcelFilePath = @"C:\\Users\\EHSAN\\Desktop\\Notebook.xlsx";//آدرس فایل خودت بده
            string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ExcelFilePath + ";Extended Properties=Excel 12.0;Persist Security Info=True";
            Conn = new OleDbConnection(excelConnectionString);
        }

        //

        public List<Information> ReadData()
        {
            List<Information> Infos = new List<Information>();
            Conn.Open();
            Cmd = new OleDbCommand();
            Cmd.Connection = Conn;
            Cmd.CommandText = "Select * from [Sheet1$]";
            var Reader = Cmd.ExecuteReader();
            while (Reader.Read())
            {
                Infos.Add(new Information()
                {
                    ID = Convert.ToInt32(Reader["ID"]),
                    FName = Reader["FName"].ToString(),
                    Lname = Reader["Lname"].ToString(),
                    City = Reader["City"].ToString(),
                    Age = Convert.ToInt32(Reader["Age"]),
                    Equx1 = Reader["Equx1"].ToString(),
                    Equx2 = Reader["Equx2"].ToString(),
                    Answer = Reader["Answer"].ToString(),
                    Equx3 = Reader["Equx3"].ToString()
                });
            }
            Reader.Close();
            Conn.Close();
            return Infos;
        }

        //


        public bool SaveData(Information info)
        {
            bool IsSave = false;
            if (info.ID != 0)
            {
                Conn.Open();
                Cmd = new OleDbCommand();
                Cmd.Connection = Conn;
                Cmd.Parameters.AddWithValue("@ID", info.ID);
                Cmd.Parameters.AddWithValue("@FName", info.FName);
                Cmd.Parameters.AddWithValue("@Lname", info.Lname);
                Cmd.Parameters.AddWithValue("@City", info.City);
                Cmd.Parameters.AddWithValue("@Age", info.Age);
                Cmd.Parameters.AddWithValue("@Equx1", info.Equx1);
                Cmd.Parameters.AddWithValue("@Equx2", info.Equx2);
                Cmd.Parameters.AddWithValue("@Equx3", info.Equx3);
                Cmd.Parameters.AddWithValue("@Answer", info.Answer);

                if (!SearchData(info).Result)
                {
                    Cmd.CommandText = "Insert into [Sheet1$] values (@ID,@FName,@Lname,@City,@Age,@Equx1,@Equx2,@Equx3,@Answer)";
                }
                else
                {
                    Cmd.CommandText = "Update [Sheet1$] set ID=@ID,FName=@FName,Lname=@Lname,Age=@Age,Equx1=@Equx1,Equx2=@Equx2,Equx3=@Equx3 where ID=@ID";

                }
                int result = Cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    IsSave = true;
                }
                Conn.Close();
            }
            return IsSave;
        }

        //

        private async Task<bool> SearchData(Information info)
        {
            bool IsRecordExist = false;
            Cmd.CommandText = "Select * from [Sheet1$] where ID=@ID";
            var Reader = await Cmd.ExecuteReaderAsync();
            if (Reader.HasRows)
            {
                IsRecordExist = true;
            }

            Reader.Close();
            return IsRecordExist;
        }

        private async Task<int> LastId(Information info)
        {
            Cmd.CommandText = "Select Max(ID) from [Sheet1$]";
            var Reader = await Cmd.ExecuteReaderAsync();
            if (Reader.HasRows)
                return 0;
            Reader.Close();
            return 0;
        }

        public bool ExistData(Information info)
        {
            bool IsRecordExist = false;
            Cmd.CommandText = "Select * from [Sheet1$] where ID='"+info.ID+"' Or FName='"+info.FName+"' or Lname='"+info.Lname+"' or Equx1='"+info.Equx1+"' or Equx2='"+info.Equx2+"'";
            var Reader =  Cmd.ExecuteReader();
            if (Reader.HasRows)
            {
                IsRecordExist = true;
            }

            Reader.Close();
            return IsRecordExist;
        }


    }


    public class Information
    {
        public int ID { get; set; }
        public string FName { get; set; }
        public string Lname { get; set; }
        public string City { get; set; }
        public int Age { get; set; }
        public string Equx1 { get; set; }
        public string Equx2 { get; set; }
        public string Equx3 { get; set; }
        public string Answer { get; set; }
    }
}
