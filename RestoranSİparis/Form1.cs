using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestoranSİparis
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server =localHost; port =5432; Database =Restorant; user ID = postgres ; Password =12345");
        private void Cuzdan(int mmusterino)
        {

            string sorgu = "select cuzdan from musteri where musterino =";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);


        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from siparis order by siparisno";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut1 = new NpgsqlCommand("insert into siparis (siparisno,menuno,musterino,verilistarihi,bahsis) " +
                "values ((select otosiparisno()),@p2,@p3,@p4,@p5)", baglanti);
            komut1.Parameters.AddWithValue("@p2", int.Parse(TxtMenuNo.Text));
            komut1.Parameters.AddWithValue("@p3", int.Parse(TxtMusteriNo.Text));
            komut1.Parameters.AddWithValue("@p4", DateTime.Now);
            komut1.Parameters.AddWithValue("@p5", int.Parse(TxtBahsis.Text));
            komut1.ExecuteNonQuery();
            if (CbIndirim.Checked)
            {
             NpgsqlCommand indirim = new NpgsqlCommand("call kuponkullan(otosiparisno()-1)", baglanti);
             indirim.ExecuteNonQuery();
            }
       
            baglanti.Close();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("Delete from siparis where siparisno=@p1", baglanti);
            komut2.Parameters.AddWithValue("@p1", int.Parse(TxtSiparisNo.Text));
            komut2.ExecuteNonQuery();
            baglanti.Close();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("update siparis set menuno=@p1,bahsis=@p2 where siparisno=@p3 ", baglanti);
            komut3.Parameters.AddWithValue("@p3", int.Parse(TxtSiparisNo.Text));
            komut3.Parameters.AddWithValue("@p1", int.Parse(TxtMenuNo.Text));
            komut3.Parameters.AddWithValue("@p2", int.Parse(TxtBahsis.Text));
            komut3.ExecuteNonQuery();
            baglanti.Close();
        }

        private void BtnAra_Click(object sender, EventArgs e)
        {

            string sorgu = "select * from siparis where siparisno = "+ int.Parse(TxtSiparisNo.Text);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void TxtMusteriNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnmenulistele_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from menu";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btniade_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("call iade(@p1)", baglanti);
            komut2.Parameters.AddWithValue("@p1", int.Parse(TxtSiparisNo.Text));
            komut2.ExecuteNonQuery();
            baglanti.Close();
        }
    }
}
