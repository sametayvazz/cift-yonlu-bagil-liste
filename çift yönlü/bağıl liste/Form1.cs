using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bağıl_liste
{
    public partial class Form1 : Form
    {
        public class dugum
        {
            public string ad;
            public string soyad;
            public int no;
            public dugum sonraki;
            public dugum onceki;
        }
        dugum ilk = null;
        dugum son = null;
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)  //sona ekleme
        {
            dugum yeni = new dugum();     //yeni adında değişken ürettik.
            yeni.ad = textBox1.Text;      //yenin "ad" ismindeki parametresi
            yeni.soyad = textBox2.Text;   //yenin "soyad" ismindeki parametresi
            yeni.no = Convert.ToInt32(textBox3.Text);  //stringi int'e çevirdik.
            if (ilk == null)    //ilk null ise daha önce veri tanımlanmamış demektir.
            {
                ilk = yeni;       // ilk yeni veri
                son = ilk;            // son aynı zamanda ilk oluyor
                ilk.onceki = null;        //ilkin öncekisi null
                son.sonraki = null;       //sondan sonraki null

            }
            else
            {
                son.sonraki = yeni;     //son eklenen yenidir
                yeni.onceki = son;      //yeni eklenenden önceki sondur
                son = yeni;             //yeni düğüm oluştu sonuncu yeni olmuş oldu
                son.sonraki = null;   //sondaki null
            }
        }
        private void listeyiYazdir(dugum ilk)   //listeye veriyi yazdırma
        {
            textBox4.Text = null;

            while (ilk != null)    // ilk sayı null a denk gelmiyorsa
            {
                textBox4.Text += ilk.ad + "  " + ilk.soyad + " : " + ilk.no.ToString(); //listeye ilki yazırdık.
                textBox4.Text += "  <-->  ";
                ilk = ilk.sonraki;    // ilk veri ilkin sonrakisi oluyor.
            }
            textBox4.Text += "null";   // döngüden çıkınca bunu yazdır
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listeyiYazdir(ilk);          //listeye yazdir diye bir fonksiyon yazıp,listenin ilk
                                         //düğümünü gönderdi.
        }

        private void button2_Click(object sender, EventArgs e)   //araya ekleme
        {
            dugum yeni = new dugum();      //yeni adında değişken ürettik.
            dugum gecici = new dugum();     //gecici adında değişken ürettik        
            yeni.ad = textBox1.Text;       //yenin "ad" ismindeki parametresi 
            yeni.soyad = textBox2.Text;      //yenin "soyad" ismindeki parametresi
            yeni.no = Convert.ToInt32(textBox3.Text);     //no parametresi,stringi int'e çevirdik.            
            gecici = ilk;      //geçiciyi ilke atadık
            if (ilk != null)    //eğer ilk ,null 'a eşit değilse
            {
                while (gecici.no < yeni.no)   //yeni numara , geçici numaradan büyükse döngüye gir
                {
                    if (gecici.sonraki.no > yeni.no)   //geçiciden sonraki numara, yeni numaradan büyükse gir 
                    {
                        break;  //çık
                    }
                    gecici = gecici.sonraki;  //geçici yani bir önceki numaradan sonraki numaraya geç
                }
                //yeni.sonraki = aranan.sonraki;  // yeni numara sonraki aranan numara yerine geçer
                //aranan.sonraki = yeni;  //aranan numara yeni olur
                gecici.sonraki.onceki = yeni;   //geçiçiden sonraki veriyle, geçici arasındaki veri yeni veridir
                yeni.sonraki = gecici.sonraki;  //yeniden sonraki veri, geciciden sonraki veridir
                gecici.sonraki = yeni;  //geçiciden sonraki veri yeni veri olur
                yeni.onceki = gecici;  //yeni veriden önceki geçici veri olur.
            }
        }

        private void button4_Click(object sender, EventArgs e)  //listeden silme
        {
            int no = Convert.ToInt32(textBox3.Text);   //stringi int'e çevirdik
            dugum silinecek = new dugum();   //silinecek adında değişken ürettik
            dugum gecici = new dugum();    //gecici adında değişken ürettik
            silinecek = ilk;  //silineceği ilk'e atadık.

            if (ilk.no == no)    //ilk no , no'ya eşitse  ( ilk düğümü silme)
            {
                ilk = ilk.sonraki;  //ilk veri ilkin sonrakisindaki olur
                silinecek.onceki = null;  //listenin başındaki null
            }
            else if (son.no == no)  //son no , sileceğimiz no'ya eşitse  (son düğümü silme)
            {
                son = son.onceki;    //sondan önceki son olur
                son.sonraki = null;  //sondan sonraki listenin sonundaki null
            }
            else
            {
                while (silinecek.no != no)    //silinecek no, girdiğimiz no'ya eşit değilse içeriye gir. (ortadan düğüm silme)
                {
                    gecici = silinecek;        //geçiciyi silinecek olarak atıyor
                    silinecek = silinecek.sonraki;     //bir sonraki veriyi geçiçi olarak atıyor.
                }
                silinecek.onceki.sonraki = silinecek.sonraki;  //silinecek veriden sonraki, geçiciden sonraki veri olur
                silinecek.sonraki.onceki = silinecek.onceki; //sileceğimiz veriden önceki veriyi, sileceğimiz veriden sonraki verinin öncekisi olması kodu
            }
        }

        private void button5_Click(object sender, EventArgs e)   //başa ekleme
        {
            dugum yeni = new dugum();      //yeni adında değişken ürettik.                  
            yeni.ad = textBox1.Text;       //yenin "ad" ismindeki parametresi 
            yeni.soyad = textBox2.Text;      //yenin "soyad" ismindeki parametresi
            yeni.no = Convert.ToInt32(textBox3.Text);  //no parametresi,stringi int'e çevirdik.
            if (ilk == null)    //ilk null'a eşitse
            {
                ilk = yeni;       // ilk yeni veri
                son = ilk;            // son aynı zamanda ilk oluyor
                ilk.onceki = null;        //ilkin öncekisi null
                son.sonraki = null;       //sondan sonraki null

            }
            else    //eğer ilk ,null 'a eşit değilse
            {
                while (ilk.no > yeni.no)   //ilk numara , yeni numaradan büyükse döngüye gir
                {
                    ilk.onceki = yeni;  // ilk veriden önceki yeni olur
                    yeni.sonraki = ilk;  //yeniden sonraki veri ilktir
                    ilk = yeni;  //ilk yenidir
                    yeni.onceki = null;  //ilkten önceki null                                     

                }               

            }
        }
    }
}
