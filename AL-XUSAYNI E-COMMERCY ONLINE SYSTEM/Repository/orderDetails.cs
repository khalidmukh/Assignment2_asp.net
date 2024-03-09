using AL_XUSAYNI_E_COMMERCY_ONLINE_SYSTEM.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Data.SqlClient;

namespace AL_XUSAYNI_E_COMMERCY_ONLINE_SYSTEM.Repository
{
    public class orderDetails
    {
        SqlConnection con;
        SqlCommand cmd;

        public orderDetails()  //constructor
        {
            con = new SqlConnection("server=DESKTOP-K0HLOGO\\SQLEXPRESS;database=asp;integrated security=true; TrustServerCertificate=True");
        }

        public List<resource> list()
        {
            List<resource> data = new List<resource>();
            using (con)
            {
                con.Open();
                string _query = "select top 50 * from OrderDetails order by OrderID desc;";
                using (SqlCommand cmd = new SqlCommand(_query, con))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        data.Add(new resource() { OrderId = Convert.ToInt32(dr["OrderID"]), Id = Convert.ToInt32(dr["ProductID"]), Name = dr["Name"].ToString(), Quentity = Convert.ToInt32(dr["Quantity"]), Price = dr.GetDecimal(dr.GetOrdinal("Price")), Description = dr["Description"].ToString()});
                    }

                }
            }
            return data;

        }
        public bool Order(string Name, int productid, int Quentiy, decimal Price, string Description)
        {
            using (con)
            {
                con.Open();
                string _query = $"insert into OrderDetails values('{Name}',{productid},{Quentiy},{Price},'{Description}')";
                cmd = new SqlCommand(_query, con);

                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        //public bool Order(string Name, int productid, int Quentiy, decimal Price, string Description)
        //{
        //    using (con)
        //    {
        //        con.Open();

        //        string _query = $"insert into OrderDetails values('{Name}',{productid},{Quentiy},'{Price}','{Description}')";
        //        SqlCommand cmd = new SqlCommand(_query, con);
        //        cmd.Parameters.AddWithValue("@Name", Name);
        //        cmd.Parameters.AddWithValue("@ProductID", productid);
        //        cmd.Parameters.AddWithValue("@Quantity", Quentiy);
        //        cmd.Parameters.AddWithValue("@Price", Price);
        //        cmd.Parameters.AddWithValue("@Description", Description);
        //        int count = cmd.ExecuteNonQuery();
        //        if (count > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
    }
}
