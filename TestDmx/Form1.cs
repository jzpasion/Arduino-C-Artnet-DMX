using ArtNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestDmx
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ArtNet.ArtNet.Initialize();
        }

        byte[] dmxValues = new byte[512];

        string ArtLynxIPAdd = "192.168.1.100";

        Thread startReading;
        string Play = "Stop";

        private void Form1_Load(object sender, EventArgs e)
        {
            ArtNet.ArtNet.Initialize();
            ArtNet.ArtNet.SendArtPoll(ArtLynxIPAdd);
            ArtNet.ArtNet.SendArtPollReply(ArtLynxIPAdd);
            ArtNet.ArtNet.SendArtDmx(ArtLynxIPAdd, 0, 1, dmxValues);

            startReading = new Thread(startRead_InOut);
            startReading.Start();
           
        }

        void startRead_InOut()
        {
            while (true)
            {

                try
                {
                    ArtNet.ArtNet.SendArtDmx(ArtLynxIPAdd, 0, 1, dmxValues);
                    //ArtNet.ArtNet.SendArtDmx(ArtLynxIPAdd, 0, 2, dmxValues);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
      
            Play = "Start";
            for (int i = 0; i < 69; i+=2)
            {
                for (int x = 66; x > 0; x -= 3)
                {
                    if (x == 0)
                    {
                        x = 66;
                    }
                    

               
                  
               
                if (i == 68)
                {
                    i = 0;
                }
                dmxValues[i] = Convert.ToByte(10);
                    dmxValues[x] = Convert.ToByte(10);

                    Thread.Sleep(100);
                dmxValues[i] = Convert.ToByte(0);
                    dmxValues[x] = Convert.ToByte(0);
                }
            }





        }

        private void Button2_Click(object sender, EventArgs e)
        {
            for( int i = 0; i < 512; i++)
            {
                dmxValues[i] = Convert.ToByte(0);

            }
           
        }
    }
}
