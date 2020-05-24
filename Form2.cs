using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Pizza_Siparis_Stok_Otomasyonu
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.; Initial Catalog=pizza;Integrated Security=SSPI");
        SqlCommand komut;
        int pizzafiyat = 0;
        int pizzatutar = 0;
        int menufiyat = 0;
        int menututar = 0;
        int icecekfiyat = 0;
        int icecektutar = 0;
        int ekurunfiyat = 0;
        int ekuruntutar = 0;
        int toplamtutar = 0;
        string pizza;
        string menu;
        string icecek;
        string ekurun;
        private void button1_Click(object sender, EventArgs e)
        {
          
            try
            {
                baglanti.Open();


                komut = new SqlCommand("insert into siparis_liste(k_adi,musteri_adi,musteri_soyadi,siparis_tarihi,tutar,sip_pizza,sip_menu,sip_icecek,sip_ekgida,durum) values('"+Form1.giris+"','"+textBox1.Text+"','"+textBox2.Text+"','"+DateTime.Now.ToShortDateString()+"','"+toplamtutar.ToString()+"','"+pizza+"','"+menu+"','"+icecek+"','"+ekurun+"','"+"Teslim Edilmedi"+"')", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Siparişiniz Alınmıştır!");


            }
            catch (Exception hata)
            {

                MessageBox.Show(hata.ToString());
            }
            try
            {
                baglanti.Open();



                SqlCommand komut2 = new SqlCommand("insert into log(kullanici_adi,tarih,islem,aciklama) values('" + Form1.giris + "','" + DateTime.Now.ToString() + "','" + "ekleme" + "','" + "Self Sipariş Yapıldı" + "')", baglanti);
                komut2.ExecuteNonQuery();
                baglanti.Close();
            }
            catch
            {

                ;
            }
            try
            {
                baglanti.Open();
                komut = new SqlCommand("insert into musteriler(adi,soyadi,tel,adres,aciklama) values('" + textBox1.Text + "','" + textBox2.Text + "','" + maskedTextBox1.Text + "','" + textBox4.Text + "','" + textBox3.Text + "')", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
               
            }
            catch 
            {
                ;
               
            }
            sifirla();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {

                baglanti.Open();
                SqlCommand com = new SqlCommand("Select * from boyutlar", baglanti);

                SqlDataReader oku = com.ExecuteReader();
                while (oku.Read())
                {

                    comboBox5.Items.Add(oku["boyutadi"].ToString());

                }

                baglanti.Close();

            }
            catch (Exception hata)
            {

                MessageBox.Show("" + hata.Message);
            }
            try
            {

                baglanti.Open();
                SqlCommand com = new SqlCommand("Select * from pizzalar", baglanti);

                SqlDataReader oku = com.ExecuteReader();
                while (oku.Read())
                {

                    comboBox4.Items.Add(oku["pizzaadi"].ToString());

                }

                baglanti.Close();

            }
            catch (Exception hata)
            {

                MessageBox.Show("" + hata.Message);
            }
            try
            {
                
                baglanti.Open();
                SqlCommand com = new SqlCommand("Select * from menuler", baglanti);

                SqlDataReader oku = com.ExecuteReader();
                while (oku.Read())
                {

                    comboBox2.Items.Add(oku["menu_adi"].ToString());

                }

                baglanti.Close();

            }
            catch (Exception hata)
            {

                MessageBox.Show(""+hata.Message);
            }

            try
            {

                baglanti.Open();
                SqlCommand com = new SqlCommand("Select * from icecekler", baglanti);

                SqlDataReader oku = com.ExecuteReader();
                while (oku.Read())
                {

                    comboBox1.Items.Add(oku["icecek_adi"].ToString());

                }

                baglanti.Close();

            }
            catch (Exception hata)
            {

                MessageBox.Show("" + hata.Message);
            }
            try
            {

                baglanti.Open();
                SqlCommand com = new SqlCommand("Select * from ekurunler", baglanti);

                SqlDataReader oku = com.ExecuteReader();
                while (oku.Read())
                {

                    comboBox3.Items.Add(oku["ekurunler_adi"].ToString());

                }

                baglanti.Close();

            }
            catch (Exception hata)
            {

                MessageBox.Show("" + hata.Message);
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {


            try
            {

                baglanti.Open();
                SqlCommand com = new SqlCommand("Select * from menuler where menu_adi='" + comboBox2.Text + "'", baglanti);

                SqlDataReader oku = com.ExecuteReader();
                while (oku.Read())
                {
                    menufiyat = int.Parse(oku["fiyat"].ToString());


                }

                baglanti.Close();



            }
            catch (Exception hata)
            {

                MessageBox.Show("" + hata.Message);
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pizzaid;
            string yol = "";

            try
            {

                baglanti.Open();
                SqlCommand com = new SqlCommand("Select * from pizzalar where pizzaadi='"+comboBox4.Text+"'", baglanti);

                SqlDataReader oku = com.ExecuteReader();
                while (oku.Read())
                {

                   yol=oku["resimyol"].ToString();
                   pizzaid = int.Parse(oku["id"].ToString());
                }

                baglanti.Close();
                pictureBox6.ImageLocation = yol;


            }
            catch (Exception hata)
            {

                MessageBox.Show("" + hata.Message);
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {


            try
            {

                baglanti.Open();
                SqlCommand com = new SqlCommand("Select * from boyut_fiyat where boyut_adi='" + comboBox5.Text + "'", baglanti);

                SqlDataReader oku = com.ExecuteReader();
                while (oku.Read())
                {
                    pizzafiyat= int.Parse(oku["fiyat"].ToString());

                  
                }

                baglanti.Close();
                


            }
            catch (Exception hata)
            {

                MessageBox.Show("" + hata.Message);
            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            pizzatutar = pizzafiyat * int.Parse(comboBox6.Text);
            label21.Text = pizzatutar.ToString();
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {

            menututar = (menufiyat * int.Parse(comboBox7.Text));
            label24.Text = menututar.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {

                baglanti.Open();
                SqlCommand com = new SqlCommand("Select * from icecekler where icecek_adi='" + comboBox1.Text + "'", baglanti);

                SqlDataReader oku = com.ExecuteReader();
                while (oku.Read())
                {
                    icecekfiyat = int.Parse(oku["fiyat"].ToString());


                }

                baglanti.Close();



            }
            catch (Exception hata)
            {

                MessageBox.Show("" + hata.Message);
            }
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {

            icecektutar = (icecekfiyat * int.Parse(comboBox8.Text));
            label27.Text = icecektutar.ToString();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                baglanti.Open();
                SqlCommand com = new SqlCommand("Select * from ekurunler where ekurunler_adi='" + comboBox3.Text + "'", baglanti);

                SqlDataReader oku = com.ExecuteReader();
                while (oku.Read())
                {
                    ekurunfiyat = int.Parse(oku["fiyat"].ToString());


                }

                baglanti.Close();



            }
            catch (Exception hata)
            {

                MessageBox.Show("" + hata.Message);
            }
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {

            ekuruntutar = (ekurunfiyat * int.Parse(comboBox9.Text));
            label30.Text = ekuruntutar.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox4.Text!="")
            {
                listBox1.Items.Add("Pizza Adı: " + comboBox4.Text + " Boyut: " + comboBox5.Text + " Miktar: " + comboBox6.Text + " Tutar: " + pizzatutar.ToString() + "TL");
                pizza +=comboBox4.Text+" ";
            }
           
              

            if (comboBox2.Text != "")
            {
                listBox1.Items.Add("Menü Adı: "+comboBox2.Text+" Miktar: "+comboBox7.Text+" Tutar: "+menututar.ToString()+"TL");
                menu += comboBox2.Text+" ";
            }
          


            if (comboBox1.Text != "")
            {
                listBox1.Items.Add("içecek Adı: " + comboBox1.Text + " Miktar: " + comboBox8.Text + " Tutar: " + icecektutar.ToString() + "TL");
                icecek += comboBox1.Text + " ";
            }
          
            

            if (comboBox3.Text != "")
            {
                listBox1.Items.Add("Ek Ürün Adı: " + comboBox3.Text + " Miktar: " + comboBox9.Text + " Tutar: " + ekuruntutar.ToString() + "TL");
                ekurun += comboBox3.Text + " ";
            }

            comboBox1.Text = ""; comboBox4.Text = ""; comboBox2.Text = ""; comboBox3.Text = "";

            toplamtutar += pizzatutar + menututar + icecektutar + ekuruntutar;
            label14.Text = toplamtutar.ToString();
            pizzatutar = 0;
            menututar = 0;
            icecektutar = 0;
            ekuruntutar = 0;
            label14.Text = "0";
            label21.Text = "0";
            label24.Text = "0";
            label27.Text = "0";
            label30.Text = "0";
            comboBox6.Text = "0";
            comboBox7.Text = "0";
            comboBox8.Text = "0";
            comboBox9.Text = "0";
            comboBox5.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sifirla();
        }
        void sifirla()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            maskedTextBox1.Text = "";
            listBox1.Items.Clear();
            label14.Text = "0";
            label21.Text = "0";
            label24.Text = "0";
            label27.Text = "0";
            label30.Text = "0";
            pizza = "";
            menu = "";
            icecek = "";
            ekurun = "";
            pizzafiyat = 0;
             pizzatutar = 0;
             menufiyat = 0;
             menututar = 0;
             icecekfiyat = 0;
             icecektutar = 0;
             ekurunfiyat = 0;
             ekuruntutar = 0;
             toplamtutar = 0;
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            comboBox5.Text = "";
            comboBox6.Text = "0";
            comboBox7.Text = "0";
            comboBox8.Text = "0";
            comboBox9.Text = "0";


        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
