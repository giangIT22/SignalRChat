﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SignalRChat
{
    public class ConnClass
    {
        //public SqlCommand cmd = new SqlCommand();
        //public SqlDataAdapter sda;
        //public SqlDataReader sdr;
        //public DataSet ds = new DataSet();
        //public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ToString());

        //public bool IsExist(string Query)
        //{
        //    bool check = false;
        //    using (cmd = new SqlCommand(Query, con))
        //    {
        //        con.Open();
        //        sdr = cmd.ExecuteReader();
        //        if (sdr.HasRows)
        //            check = true;
        //    }
        //    sdr.Close();
        //    con.Close();
        //    return check;

        //}

        //public bool ExecuteQuery(string Query)
        //{
        //    int j = 0;
        //    using (cmd = new SqlCommand(Query, con))
        //    {
        //        con.Open();
        //        j = cmd.ExecuteNonQuery();
        //        con.Close();
        //    }

        //    if (j > 0)
        //        return true;
        //    else
        //        return false;

        //}

        //public string GetColumnVal(string Query, string ColumnName)
        //{
        //    string RetVal = "";
        //    using (cmd = new SqlCommand(Query, con))
        //    {
        //        con.Open();
        //        sdr = cmd.ExecuteReader();
        //        while (sdr.Read())
        //        {
        //            RetVal = sdr[ColumnName].ToString();
        //            break;
        //        }
        //        sdr.Close();
        //        con.Close();
        //    }

        //    return RetVal;


        //}

        public DataTable ExecuteQuery(string query, object[] parameter = null)//excutequery trả về các dòng dữ liệu , dòng kết quả
        {
            DataTable data = new DataTable();
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ToString()))//using dung để giải phóng bộ nhớ
            {
                cnn.Open();//mở kết nối

                SqlCommand cmd = new SqlCommand(query, cnn);//đây sẽ là câu truy vẫn để lấy dữ liệu
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains("@"))
                        {
                            cmd.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);//đi thực thi câu lệnh truy vấn để lấy dữ liệu ra

                adapter.Fill(data); //đổ cái dữ liệu mình dấy ra vào data(bảng dữ liệu)

                cnn.Close();
            }

            return data;
        }


        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ToString()))//using dung để giải phóng bộ nhớ
            {
                cnn.Open();//mở kết nối

                SqlCommand cmd = new SqlCommand(query, cnn);//đây sẽ là câu truy vẫn để lấy dữ liệu
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains("@"))
                        {
                            cmd.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                data = cmd.ExecuteNonQuery();//ExecuteNonQuery() trả về số dòng thành công, chỉ dùng cho insert ,delete,update 
                cnn.Close();
            }

            return data;
        }

    }
}