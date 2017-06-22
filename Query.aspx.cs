using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace Rosteras
{
    public partial class Query : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        /*protected void ExecuteButton_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection("Data Source=(local);Initial Catalog=Ρόστερ;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter();
            SqlCommand command;

            String[] q = Regex.Split(query.Text," ");
            //System.Diagnostics.Debug.WriteLine("\nfuck you");
            String tbl = null;
            con.Open();

            if (q[0].Equals("select"))
            {
                sda.SelectCommand = new SqlCommand(query.Text, con);
                ds.Tables.Clear();
                sda.Fill(ds);

                results.DataSource = ds.Tables[0];
                results.DataBind();
            }

            else if (q[0].Equals("declare") || q[0].Equals("update") || q[0].Equals("delete") || q[0].Equals("insert"))
            {
                if (q[0].Equals("insert"))
                    tbl = getTable(q, 1);
                else if (q[0].Equals("delete"))
                    tbl = getTable(q);
                else
                    tbl = getTable(q,"1");

                command = new SqlCommand(query.Text, con);
                command.ExecuteNonQuery();
                sda.SelectCommand = new SqlCommand("select * from " + tbl, con);
                ds.Tables.Clear();
                sda.Fill(ds);

                results.DataSource = ds.Tables[0];
                results.DataBind();

            }

            else
            {
                sda.SelectCommand = new SqlCommand("select Όνομα,Ηλικία from Παίκτες", con);
                ds.Tables.Clear();
                sda.Fill(ds);
                results.DataSource = ds.Tables[0];
                results.DataBind();
            }


           

                con.Close();
        }*/

        
    
                

        private String getTable(String[] q)
        {
            for (int i = 1; i < q.Length; i++)
            {
                if (q[i].Equals("from") || q[i].Equals("FROM"))
                {
                    return q[i + 1];
                }
            }
            return null;
        }

        private String getTable(String[] q, int z)
        {
            for (int i = 1; i < q.Length; i++)
            {
                if (q[i].Equals("into") || q[i].Equals("INTO"))
                {
                    return q[i + 1];
                }
            }
            return null;
        }

        private String getTable(String[] q, String z)
        {
            for (int i = 0; i < q.Length; i++)
            {
                if (q[i].Equals("update") || q[i].Equals("UPDATE"))
                {
                    return q[i + 1];
                }
            }
            return null;
        }

    }
}