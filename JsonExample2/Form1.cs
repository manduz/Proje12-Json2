using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace JsonExample2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<Category> GetEmojiList()
        {
            JavaScriptSerializer tercuman = new JavaScriptSerializer();//json dosyasını ceviriyor
            string jsonContent = File.ReadAllText("emojiler.json");


            return tercuman.Deserialize<List<Category>>(jsonContent);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var list = GetEmojiList();
            DisplapEmojiList(list);//bu kısım form ekranında göstermek için
        }

        private void DisplapEmojiList(List<Category> list)
        {
            foreach (Category item in list)
            {
                Label emek = new Label() { Text = item.category };
                emek.AutoSize = false;
                emek.Width = this.ClientSize.Width;
                emek.Font = new Font(FontFamily.GenericMonospace, 20);
                emek.TextAlign = ContentAlignment.MiddleCenter;
                emek.Margin = new Padding(0,20,0,20);
                flowLayoutPanel1.SetFlowBreak(emek,true);
                flowLayoutPanel1.Controls.Add(emek);

                DisplayItems(item);

            }
        }

        private void DisplayItems(Category item)
        {
            foreach (Item emoji in item.items)
            {
                //10 tane items varsa 10 tane buton oluşturur
                Button button = new Button();
                button.Text = emoji.art + Environment.NewLine + emoji.name;//Environment ile önce alt metot sonra üst metodu çalıştırıyruz
                button.Font = new Font(FontFamily.GenericMonospace, 14);
                button.Padding = new Padding(5);
                button.Width = flowLayoutPanel1.ClientSize.Width / 2 - 10;
                button.Height = 80;
                button.Click += buttonClick;
                flowLayoutPanel1.Controls.Add(button);
            }
          
        }

        private void buttonClick(object sender, EventArgs e)
        {
            Button clickButton = (Button) sender;
            string[] infos = clickButton.Text.Split('\n');
            Clipboard.SetText(infos[0]);//ilk değer ataması
            MessageBox.Show(clickButton.Text + "has copled to clickBoard");



        }
    }
}
