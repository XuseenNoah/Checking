using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1.ViewModels;

namespace WebApplication1.Models
{
    public class _repository
    {
        public string connection = System.Configuration.ConfigurationManager.ConnectionStrings["db"].ConnectionString;

        internal void CreatePerson(Customers cus)
        {
            using (var conn = new SqlConnection(connection))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Insert into Customers Values (@cn,@cp,@cc,getdate())";
                cmd.Parameters.AddWithValue("cn", cus.CustomerName);
                cmd.Parameters.AddWithValue("@cp", cus.CustomerPhone);
                cmd.Parameters.AddWithValue("@cc", cus.CustomerCity);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        internal List<Customers> ListPerson()
        {
            using (var conn = new SqlConnection(connection))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Select *from Customers";
                conn.Open();
                var reader = cmd.ExecuteReader();
                var list = new List<Customers>();
                while (reader.Read())
                {
                    Customers cus = new Customers();
                    cus.Id = (int)reader["Id"];
                    cus.CustomerName = reader["CustomerName"] as string;
                    cus.CustomerPhone = reader["CustomerPhone"] as string;
                    cus.CustomerCity = reader["CustomerCity"] as string;
                    cus.Date = (DateTime)reader["Date"];
                    list.Add(cus);
                }
                return list;
            }
        }

        internal void DeletePerson(string id)
        {
            using (var conn = new SqlConnection(connection))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Delete from Customers Where Id=@id";
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();

            }
        }

        internal void UpdatePerson(Customers cus)
        {
            using (var conn = new SqlConnection(connection))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Update Customers Set CustomerName=@cn,CustomerPhone=@cp,CustomerCity=@cc Where Id=@id";
                cmd.Parameters.AddWithValue("@id", cus.Id);
                cmd.Parameters.AddWithValue("@cn", cus.CustomerName);
                cmd.Parameters.AddWithValue("@cp", cus.CustomerPhone);
                cmd.Parameters.AddWithValue("@cc", cus.CustomerCity);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        internal Customers GetUpdate(string id)
        {
            using (var conn = new SqlConnection(connection))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Select *from Customers Where Id=@id";
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                var reader = cmd.ExecuteReader();
                Customers cus = null;
                if (reader.Read())
                {
                    cus = new Customers();
                    cus.Id = (int)reader["Id"];
                    cus.CustomerName = reader["CustomerName"] as string;
                    cus.CustomerPhone = reader["CustomerPhone"] as string;
                    cus.CustomerCity = reader["CustomerCity"] as string;
                    
                }
                return cus;
            }
        }
    }
}