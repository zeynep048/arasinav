using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
namespace Pizza_Siparis_Stok_Otomasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.;  Initial Catalog=pizza;Integrated Security=SSPI");
        public static string giris = "";
            public static string sifre="";
            public static string yetki="";
        public static string yetki_log = "0";
        public static string yetki_user = "0";
        public static string yetki_buy = "0";
        public static string yetki_stok = "0";
        public static string yetki_cari = "0";
        public static string yetki_toptan = "0";

        private string password = "1";



        private byte[] Sifrele(byte[] SifresizVeri, byte[] Key, byte[] IV)

        {

            MemoryStream ms = new MemoryStream();

            Rijndael alg = Rijndael.Create();



            alg.Key = Key;

            alg.IV = IV;



            CryptoStream cs = new CryptoStream(ms,



            alg.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(SifresizVeri, 0, SifresizVeri.Length);

            cs.Close();



            byte[] sifrelenmisVeri = ms.ToArray();

            return sifrelenmisVeri;

        }



        private byte[] SifreCoz(byte[] SifreliVeri, byte[] Key, byte[] IV)

        {

            MemoryStream ms = new MemoryStream();

            Rijndael alg = Rijndael.Create();



            alg.Key = Key;

            alg.IV = IV;



            CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);



            cs.Write(SifreliVeri, 0, SifreliVeri.Length);

            cs.Close();



            byte[] SifresiCozulmusVeri = ms.ToArray();

            return SifresiCozulmusVeri;

        }



        public string TextSifrele(string sifrelenecekMetin)

        {

            byte[] sifrelenecekByteDizisi = System.Text.Encoding.Unicode.GetBytes(sifrelenecekMetin);



            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,



            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});



            byte[] SifrelenmisVeri = Sifrele(sifrelenecekByteDizisi,



                 pdb.GetBytes(32), pdb.GetBytes(16));



            return Convert.ToBase64String(SifrelenmisVeri);

        }



        public string TextSifreCoz(string text)

        {

            byte[] SifrelenmisByteDizisi = Convert.FromBase64String(text);



            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password,



                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65,



            0x64, 0x76, 0x65, 0x64, 0x65, 0x76});



            byte[] SifresiCozulmusVeri = SifreCoz(SifrelenmisByteDizisi,



                pdb.GetBytes(32), pdb.GetBytes(16));



            return System.Text.Encoding.Unicode.GetString(SifresiCozulmusVeri);

        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult x = MessageBox.Show("Programdan Çıkmak İstediğinizden Emin Misiniz?", "Çıkış Mesajı!", MessageBoxButtons.YesNo);
            if (x == DialogResult.Yes)
            {
                //Evet tıklandığında Yapılacak İşlemler
                Application.Exit(); // Evet tıklandığında uygulama kapanacak

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {


                if (baglanti.State == ConnectionState.Closed) //eğer bağlantı kapalıysa
                {
                    baglanti.Open(); //bağlantıyı aç
                }
                string cözücü = TextSifrele(textBox2.Text.ToString());//şifreyi md5e çeviririz 
                SqlCommand com = new SqlCommand("Select * from kullanici where k_adi='" + textBox1.Text.ToString() +
                    "'and parola='" +  cözücü + "'", baglanti);
                //burada veritabanina kodlayarak yazdımız şifrelerin kodlarını karşılaştırdık
                SqlDataReader oku = com.ExecuteReader();
                if (oku.Read())
                {
                    //
                    giris = textBox1.Text;
                    sifre = textBox2.Text;
                    yetki = oku["rol"].ToString();
                    //kullanıcının yetkisi admin ise onu admin paneline yönlendiriyoruz
                    //formlar arası kullanmak amaçlı public değişkenlere girilen değerleri aldık
                   
                    if (yetki == "Admin")
                    {
                        baglanti.Close();
                        baglanti.Open();
                        SqlCommand comt = new SqlCommand("Select * from yetki where k_adi='" + textBox1.Text.ToString() + "'", baglanti);
                        oku = comt.ExecuteReader();
                        if (oku.Read())
                        {
                            yetki_log = oku["yetki_log"].ToString(); ;
                            yetki_user = oku["yetki_user"].ToString();
                            yetki_buy = oku["yetki_buy"].ToString();
                            yetki_stok = oku["yetki_stok"].ToString();
                            yetki_cari = oku["yetki_cari"].ToString();
                            yetki_toptan = oku["yetki_toptan"].ToString();
                        }//yetkileri program içi değişkenlere aldık
                        Form3 frm3 = new Form3();
                        frm3.Show();
                        this.Hide();//admin paneline geçiş yapdık 

                    }
                    else if (yetki == "Müşteri")//eğer kullanıcı rolü müşteri ise o zaman müşteri paneline yönlendiriyoruz
                    {
                        Form2 frm2 = new Form2();
                        frm2.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Bu Tür yetkili Kullanıcı Yok");
                    }

                   

                   

                }
                baglanti.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);//hata mesajı
             
            }
        }


            
        }
    }

