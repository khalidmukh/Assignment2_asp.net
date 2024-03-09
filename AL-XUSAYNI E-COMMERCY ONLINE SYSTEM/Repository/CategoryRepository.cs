
using System.Data.SqlClient;
using AL_XUSAYNI_E_COMMERCY_ONLINE_SYSTEM.Models;
using AL_XUSAYNI_E_COMMERCY_ONLINE_SYSTEM.Repository;
using Microsoft.Data.SqlClient;

namespace AL_XUSAYNI_E_COMMERCY_ONLINE_SYSTEM.Repository
{
    public class CategoryRepository
    {
        SqlConnection con;
        SqlCommand cmd;

        public object List { get; internal set; }
        public object Products { get; internal set; }

        public CategoryRepository()  //constructor
        {
            con = new SqlConnection("server=DESKTOP-K0HLOGO\\SQLEXPRESS;database=asp;integrated security=true; TrustServerCertificate=True");
        }

        public List<Product> getAll()
        {
            List<Product> data = new List<Product>();
            using (con)
            {
                con.Open();
                string _query = "select * from Product order by name asc";
                using (SqlCommand cmd = new SqlCommand(_query, con))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        data.Add(new Product() { Id = Convert.ToInt32(dr["id"]), Name = dr["name"].ToString(), Price = dr.GetDecimal(dr.GetOrdinal("price")), Description = dr["Description"].ToString()});
                    }

                }
            }
            return data;

        }
        public Product get_by_id(int Id)
        {
            Product data = new Product();
            using (con)
            {
                con.Open();
                string _query = $"select * from Product where id={Id}";
                cmd = new SqlCommand(_query, con);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data = new Product() { Id = Convert.ToInt32(dr["id"]), Name = dr["name"].ToString(), Price = dr.GetDecimal(dr.GetOrdinal("price")), Description = dr["Description"].ToString()};
                }
            }
            return data;
        }

        public bool create(int id ,string name, decimal price, string description)
         {
            try {
                using (con)
                {
                    con.Open();
                    string _query = $"insert into Product values({id},'{name}','{price}','{description}')";
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
            }catch (Exception ex)
            {
                return false;
            }
        }

        public bool update(int id, string newname, decimal newprice, string newdescription)
        {

          
            using (con)
            {   con.Open();
                string _query = $"update Product set name='{newname}',Price='{newprice}',Description='{newdescription}' where id={id}";
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

        public bool delete(int id)
        {
            using (con)
            {
                con.Open();
                string _query = $"delete from Product where id={id}";
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


        public List<Product> GetProducts()
        {
            List<Product> data = new List<Product>();
            using (con)
            {
                con.Open();
                string _query = "select * from Product order by ID ;";
                using (SqlCommand cmd = new SqlCommand(_query, con))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        data.Add(new Product() { Id = Convert.ToInt32(dr["id"]), Name = dr["name"].ToString(), Price = dr.GetDecimal(dr.GetOrdinal("price")), Description = dr["Description"].ToString(), });
                    }
                }
            }
            return data;

        }
      
        public Product get_order(int Id)
        {
            Product items = new Product();
            using (con)
            {
                con.Open();
                string _query = $"select * from Product where ID={Id}";
                cmd = new SqlCommand(_query, con);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    items = new Product() { Id = Convert.ToInt32(dr["id"]), Name = dr["name"].ToString(), Price = dr.GetDecimal(dr.GetOrdinal("price")), Description = dr["Description"].ToString(),};
                }
            }
            return items;
        }
    }
    }
