using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;



namespace tyoaikaseuranta
{
    public partial class formi : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Server=tcp:Oma sivu tähän,dbserver.database.windows.net,1433;Initial Catalog=tyoaikaseuranta20200305091157_db;Persist Security Info=False;User ID=timo;Password=**********;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
       // SqlConnection con = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Tyot; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        private decimal summa = (decimal)0.0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //bttyhjenna.Enabled = false;
                taytaGridview();
                Calendar1.SelectedDate = DateTime.Today;
                TextBox1.Text = Calendar1.SelectedDate.ToString("dd.MM.yyyy");
               
            }

        }

        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        protected void bttyhjenna_Click(object sender, EventArgs e)
        {
          
            Clear();
        }
        public void Clear()
        {
            hftyot.Value = "";
            txbpaikka.Text = "";
            txbtyoaika.Text = "0:00";
            txbylityo.Text = "0:00";
            txbmatkat.Text = "0";
            lbonnistui.Text = lbeionnistu.Text = "";
            bttallenna.Text = "Tallenna";
            bttyhjenna.Enabled = true;
            
        }

        protected void bttallenna_Click(object sender, EventArgs e )
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
            { lbeionnistu.Text = "Ei tallennettu"; }
           
          
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
            //decimal sum = 0;
            //foreach (DataRow dr in daTb.Rows)
            //{
            //    dynamic value = dr["Ylityo"].ToString();
            //    if (!string.IsNullOrEmpty(value))
            //    {
            //        sum += Convert.ToDecimal(value);
            //    }
            //}
            //Texylityo.Text = Convert.ToDecimal(sum).ToString();
            //GridView1.FooterRow.Cells[5].Text = string.Format(kaikkimatk + " Km");
            TextBox2.Text = string.Format(kaikkimatk + " Km");
            //decimal summa =0;
            //foreach (DataRow row in daTb.Rows)
            //{
            //    dynamic value = row["Tyoajat"].ToString();
            //    if (!string.IsNullOrEmpty(value))
            //    {
            //        summa += Convert.ToDecimal(value);

            //    }
            //}

            //Textunnit.Text = Convert.ToDecimal(summa).ToString(DateTime.MinValue.ToString("HH.mm"));
          

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




        string totalTime;
        string ylityo;
        TimeSpan ts = new TimeSpan();
        TimeSpan yt = new TimeSpan();


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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ts = ts.Add(TimeSpan.Parse(DataBinder.Eval(e.Row.DataItem, "Tyoajat").ToString()));
                var hourPart = ((int)(Math.Truncate(ts.TotalMinutes / 60))).ToString();
                var minutePart = (ts.TotalMinutes % 60).ToString()/*.PadLeft(2, '0')*/;
                totalTime = hourPart + ":" + minutePart;
            }

            Textunnit.Text = totalTime;//lasketaan aika tunnit ja minuutit
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                yt = yt.Add(TimeSpan.Parse(DataBinder.Eval(e.Row.DataItem, "Ylityo").ToString()));
                var hourPart = ((int)(Math.Truncate(yt.TotalMinutes / 60))).ToString();
                var minutePart = (yt.TotalMinutes % 60).ToString()/*.PadLeft(2, '0')*/;
                ylityo = hourPart + ":" + minutePart;
            }

            Texylityo.Text = ylityo;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "50px");
            }
        }
        

    }
}
