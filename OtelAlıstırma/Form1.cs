using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtelAlıstırma
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            textBox1.Tag = row.Cells["OtelNo"].Value.ToString();
            textBox1.Text = row.Cells["OtelAdi"].Value.ToString();
            textBox2.Text = row.Cells["OdaSayisi"].Value.ToString();
            textBox3.Text = row.Cells["YıldızSayisi"].Value.ToString();
            textBox4.Text = row.Cells["Konsept"].Value.ToString();
            textBox5.Text = row.Cells["Sehir"].Value.ToString();

        }
        OtelAlıstırmaEntities1 db = new OtelAlıstırmaEntities1();
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.OtelBilgis.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OtelBilgi save = new OtelBilgi();
            save.OtelAdi = textBox1.Text;
            save.OdaSayisi=Convert.ToInt32(textBox2.Text);
            save.YıldızSayisi=Convert.ToInt32(textBox3.Text);
            save.Konsept=textBox4.Text;
            save.Sehir=textBox5.Text;
            db.OtelBilgis.Add(save);// veritabanımda otelbilgi tablosuna ekle
            db.SaveChanges();//değişiklikleri kaydet
            dataGridView1.DataSource=db.OtelBilgis.ToList() ; //listele

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //öncelikle varolan değeri alman lazım otelnoyu
            int id =Convert.ToInt32(textBox1.Tag);// textbox1i görüp idnin içine attı
            //veritabanı işlemlerinde kullanılır "var" her seyde kullanılır yani sayi kelime vs
            var yenile=db.OtelBilgis.Where(x=>x.OtelNo==id).FirstOrDefault();//x=> id değerim var mı bak x değeri ile dısardan alınan id eslesiyo mu bak ve buldugun ilk değeri getir getir
            yenile.OtelAdi = textBox1.Text;
            yenile.OdaSayisi = Convert.ToInt32(textBox2.Text);
            yenile.YıldızSayisi = Convert.ToInt32(textBox3.Text);
            yenile.Konsept = textBox4.Text;
            yenile.Sehir = textBox5.Text;
            db.SaveChanges();//değişiklikleri kaydet
            dataGridView1.DataSource = db.OtelBilgis.ToList(); //listele
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int id =Convert.ToInt32(textBox1.Tag);//güncelle ve sil idye göre oldugu için tanımlıyosun
            var sil=db.OtelBilgis.Where(a=>a.OtelNo==id).FirstOrDefault();//hangi idyi aldıysan otelnosu o id olan değeri bul ve buldugun ilk değeri getir
            //o değeri al ve silin içine at a==>a atama yapmanı saglıyo bde olur cde olur lambda kuralı gibi bişiy
            db.OtelBilgis.Remove(sil);
            db.SaveChanges();
            dataGridView1.DataSource = db.OtelBilgis.ToList();//listele komutu

        }
    }
}
