using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace tyoaikaseuranta
{
    public partial class formi : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Tyot;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
       
        private decimal summa = (decimal)0.0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //bttyhjenna.Enabled = false;
                taytaGridview();
              
            }

        }

        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        protected void bttyhjenna_Click(object sender, EventArgs e)
        {
            //bttyhjenna.Enabled = true;
            Clear();



        }
        public void Clear()
        {
            hftyot.Value = "";
            txbpaikka.Text = txbtyoaika.Text = txbylityo.Text = TextBox1.Text = "";
            txbmatkat.Text = "0";
            lbonnistui.Text = lbeionnistu.Text = "";
            bttallenna.Text = "Tallenna";
            bttyhjenna.Enabled = true;
        }

        protected void bttallenna_Click(object sender, EventArgs e)
        {


            if (con.State == ConnectionState.Closed)

                con.Open();
            SqlCommand Cmd = new SqlCommand("tyotLisaaPaivita", con);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@Id", (hftyot.Value == "" ? 0 : Convert.ToInt32(hftyot.Value)));
            Cmd.Parameters.AddWithValue("@Paikat", txbpaikka.Text.Trim());
            Cmd.Parameters.AddWithValue("@Pvm", Convert.ToDateTime(TextBox1.Text.Trim()));
            Cmd.Parameters.AddWithValue("@Tyoajat", txbtyoaika.Text.Trim());
            Cmd.Parameters.AddWithValue("@Ylityo", txbylityo.Text.Trim());
            Cmd.Parameters.AddWithValue("@Matkat", Convert.ToDecimal(txbmatkat.Text.Trim()));
            Cmd.ExecuteNonQuery();

            con.Close();
            Clear();
            if (hftyot.Value == "")
            { lbeionnistu.Text = "Tallennettu"; }
            else
                lbeionnistu.Text = "Ei tallennettu";


            taytaGridview();
        }
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            TextBox1.Text = Calendar1.SelectedDate.ToString("dd-MMM-yyyy");
            //TextBox1.Text = String.Format("{0:dd-MMM-yyyy}", Calendar1.SelectedDate);
        }
        void taytaGridview()
        {
            if (con.State == ConnectionState.Closed)

                con.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("kaikki", con);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable daTb = new DataTable();
            sqlDa.Fill(daTb);
            con.Close();          
            GridView1.DataSource = daTb;
            GridView1.DataBind();
            decimal kaikkimatk = daTb.AsEnumerable().Sum(row => row.Field<decimal>("Matkat"));
           

            //GridView1.FooterRow.Cells[5].Text = string.Format(kaikkimatk + " Km");
            TextBox2.Text=string.Format(kaikkimatk + " Km");

        }
        protected void lnk_OnClick(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32((sender as LinkButton).CommandArgument);
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("nakyma", con);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@Id", Id);
            DataTable daTb = new DataTable();
            sqlDa.Fill(daTb);
            con.Close();
            hftyot.Value = Id.ToString();
            txbpaikka.Text = daTb.Rows[0]["Paikat"].ToString();
            TextBox1.Text = daTb.Rows[0]["Pvm"].ToString();
            txbtyoaika.Text = daTb.Rows[0]["Tyoajat"].ToString();
            txbylityo.Text = daTb.Rows[0]["Ylityo"].ToString();
            txbmatkat.Text = daTb.Rows[0]["Matkat"].ToString();
            bttallenna.Text = "Päivitä";
            btpoista.Enabled = true;

        }

        

        
      

        protected void btpoista_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand Cmd = new SqlCommand("poista", con);

            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(hftyot.Value));
                Cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {

            }
            Clear();
            taytaGridview();
            lbonnistui.Text = "Poistettu";
        }




        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
            taytaGridview();
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {///lasketaan yhden sivun matkat yhteen
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                summa += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Matkat"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {

                e.Row.Cells[4].Text = string.Format(summa + " Km");

            }

        }

       
    }
}