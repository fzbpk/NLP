using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GoogleNLP;
namespace tESTui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            AmazonNLP.AmazonNLP cp = new AmazonNLP.AmazonNLP();
            this.dataGridView1.DataSource = (cp.Entities(this.richTextBox1.Text));
            this.dataGridView2.DataSource = (cp.KeyPhrases(this.richTextBox1.Text));
            this.dataGridView3.DataSource = (cp.Sentiment(this.richTextBox1.Text)); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.comboBox1.Items.Add("en");
            this.comboBox1.Items.Add("es");
            this.comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "";
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GoogleNLP.Google nlp = new GoogleNLP.Google();
            Dictionary<string, object> cfg = new Dictionary<string, object>();
            cfg["project_id"] = "";
            cfg["private_key_id"] = "";
            cfg["private_key"] = "";
            cfg["client_email"] = "";
            cfg["client_id"] = "";
            cfg["auth_uri"] = "https://accounts.google.com/o/oauth2/auth";
            cfg["token_uri"] = "https://accounts.google.com/o/oauth2/token";
            cfg["auth_provider_x509_cert_url"] = "https://www.googleapis.com/oauth2/v1/certs";
            cfg["client_x509_cert_url"] = "https://www.googleapis.com/robot/v1/metadata/x509/208020158384-compute%40developer.gserviceaccount.com";
            nlp.Config = cfg;
            nlp.test();
        }
    }
}
