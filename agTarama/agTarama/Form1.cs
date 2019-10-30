using System;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.IO;

namespace agTarama
{
    public partial class Form1 : Form
    {
        String[] ipAdresleri;
        public Form1()
        {
            InitializeComponent();
            
            bilgisayarlariBul();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        public void DirSearch(string dir)
        {
            listBox2.Items.Add(dir);
            try
            {
                foreach (string f in Directory.GetFiles(dir))
                    listBox3.Items.Add(f);
                foreach (string d in Directory.GetDirectories(dir))
                {
                    listBox3.Items.Add(d);
                    DirSearch(d);
                }

            }
            catch (System.Exception ex)
            {
                listBox3.Items.Add(ex.ToString());
            }
        }

        public void tara (String subnet,Int16 sayac)
        {
            Ping paket;
            PingReply cevap;
            IPAddress adresler;
            IPHostEntry host;

            for(int i=1;i<255;i++)
            {
                String sonSayi = "." + i.ToString();
                paket = new Ping();
                string data = " ";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                cevap = paket.Send(subnet + sonSayi,100,buffer);
                if(cevap.Status==IPStatus.Success)
                {
                    try
                    {
                        adresler = IPAddress.Parse(subnet + sonSayi);
                        host = Dns.GetHostEntry(adresler);
                        String yol = subnet + sonSayi ;
                        DirSearch(@"\\LENOVO");
                        listBox1.Items.Add(yol);
                        ipAdresleri[sayac] = subnet + sonSayi;


                    }
                    catch
                    {

                    }
                }
            }
        }

        public void bilgisayarlariBul()
        {
            IPHostEntry host;
            Int16 sayac = 0;
            string yerelIP = " ? ";
            host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ipAd in host.AddressList)
            {
                if (ipAd.AddressFamily.ToString() == "InterNetwork")
                {
                    yerelIP = ipAd.ToString();
                    //listBox1.Items.Add(yerelIP);
                    string[] adresler = yerelIP.Split('.');
                    yerelIP = adresler[0] + "." + adresler[1] + "." + adresler[2];
                    tara(yerelIP,sayac);
                    sayac++;
                }
            }
        }


    }
}
