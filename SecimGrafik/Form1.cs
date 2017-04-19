using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecimGrafik
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        // Girilen değeri sayıya çevirir.
        private int sayiyaCevir(String text)
        {
            try
            {
                return Int32.Parse(text);               
            }
            catch (FormatException)
            {
                return -1;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Grafikleri temizle.
            chart1.Series["Yuzdeler"].Points.Clear();
            chart2.Series["Yuzdeler"].Points.Clear();
            // Pie charttan etiketleri sil.
            chart2.Series["Yuzdeler"]["PieLabelStyle"] = "Disabled";

            // Textbox değerlerini hashtableda tut.
            Hashtable degerler = new Hashtable() {
                {'A', textBoxA.Text},
                {'B', textBoxB.Text},
                {'C', textBoxC.Text},
                {'D', textBoxD.Text},
                {'E', textBoxE.Text},
            };

            // Yüzde değerleri anahtar/deger şeklinde sıralı bir sözlükte tut.
            SortedDictionary<string, int> yuzdeler = new SortedDictionary<string, int>();            

            bool dogru = true;
            int toplam = 0;

            foreach(DictionaryEntry deger in degerler) 
            {
                int yuzde = sayiyaCevir(deger.Value.ToString());

                if (yuzde == -1)
                {
                    dogru = false;                    
                }
                else
                {                   
                    yuzdeler.Add(deger.Key.ToString(), yuzde);
                    toplam += yuzde;
                }
            }

            int eleman = yuzdeler.Count;

            if (!dogru || eleman != 5)
            {
                message.Text = "Lütfen tüm adayların yüzde değerlerini sayı olarak girin!";
            }
            else if (toplam != 100)
            {
                message.Text = "Fazla ya da eksik değer girdiniz. Değerlerin toplamı 100 olmalı! Girdiğiniz değerler toplamı:" + toplam.ToString();
            }
            else
            {
                message.Text = "";                

                foreach (var yuzde in yuzdeler)
                {
                    chart1.Series["Yuzdeler"].Points.AddXY(yuzde.Key, yuzde.Value);
                    chart2.Series["Yuzdeler"].Points.AddXY(yuzde.Key, yuzde.Value); 
                }               

            }
           

        }         
        

    }
}
