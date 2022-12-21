using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net;



namespace NavigationUICSHARP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }


        int tab = 0;
        int selection = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.TransparencyKey = Color.Crimson;
            Thread backgroundThread = new Thread(new ThreadStart(recieve_input));

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void tabchecker()
        {
            switch (tab)
            {
                case 1:
                    Home.Visible = true;
                    button2.BackColor = Color.DimGray;
                    button1.BackColor = Color.FromArgb(255, 30, 30, 30);
                    break;
                case 2:
                    button1.BackColor = Color.DimGray;
                    button2.BackColor = Color.FromArgb(255, 30, 30, 30);
                    Home.Visible = false;
                    Networking.Visible = true;
                    break;
                case 3:
                    button1.BackColor = Color.FromArgb(255, 30, 30, 30);
                    button2.BackColor = Color.FromArgb(255, 30, 30, 30);
                    Home.Visible = false;
                    Networking.Visible = false;
                    break;
            }
            if (tab == 1)// Home
            {
                switch (selection)
                {
                    case 1: //button 1
                        HomeButton1.Focus();
                        break;
                    case 2:
                        HomeButton2.Focus();
                        break;
                    case 3:
                        HomeButton3.Focus();
                        break;
                    case 4:
                        HomeButton4.Focus();
                        break;

                }
            }
        }

        void commandcheck(string command)
        {
            if (command.Contains("Q"))
            {
                MessageBox.Show("a");
                tabchecker();

                tab = tab + 1;
                if (tab > 3)
                {
                    tab = 0;
                }
            }
        }

        bool pipethread = true;
        bool recieved_input = false;
        string intputted;
        void recieve_input()
        {

            while (pipethread)
            {
                var namedPipeServer = new NamedPipeServerStream("nhsupspipe", PipeDirection.InOut, 1, PipeTransmissionMode.Byte);
                var streamReader = new StreamReader(namedPipeServer);
                namedPipeServer.WaitForConnection();


                //Console.WriteLine($"read from pipe client: {streamReader.ReadLine()}");
                intputted = streamReader.ReadLine();
                if (intputted.Length > 0)
                {
                    recieved_input = true;

                }
                else
                {
                    recieved_input = false;
                }
                commandcheck(intputted);
                Console.WriteLine(intputted);
                namedPipeServer.Dispose();
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Q)
            {
                MessageBox.Show("a");
                tabchecker();

                tab = tab + 1;
                if (tab > 3)
                {
                    tab = 0;
                }
            }

            if (e.KeyCode == Keys.W)
            {
                selection = selection + 1;
                if (selection > 9)
                {
                    selection = 0;
                }
            }



        }
    }
}
