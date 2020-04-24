using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;  //Se ha añadido para los threads

namespace ConsoleApplication1
{
    public partial class Form1 : Form
    {
        Socket server;
        Thread atender;
        int i;
        double timeLeft = 300.00;
        int Sec = 60;

        public Form1()
        {
            InitializeComponent();

            //Necesario para que los elementos de los formularios puedan ser accedidos desde threads diferentes a los que los crearon
            CheckForIllegalCrossThreadCalls = false;

            //Deshabilitar botones al abrir formulario
            iniciarSesion.Enabled = false;
            registrar.Enabled = false;
            enviar.Enabled = false;
        }

        private void Conexion()
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("147.83.117.22");
            IPEndPoint ipep = new IPEndPoint(direc, 50060);


            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                //MessageBox.Show("Conectado.");
                label13.Text = "Conectado";
                i = 0;

            }
            catch (SocketException ex)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No se ha podido conectar con el servidor");
                label13.Text = "No conectado";
                i = 1;
                return;
            }

            //pongo en marcha el thread que atenderá los mensajes del servidor
            ThreadStart ts = delegate { atender_mensaje_servidor(); };
            atender = new Thread(ts);
            atender.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Conexion();
        }

        int K = 0;
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (K % 2 == 0)  //Nos conectamos
            {
                Conexion();
                //Habilitar botones al conectarse
                iniciarSesion.Enabled = true;
                registrar.Enabled = true;
                enviar.Enabled = true;
                estadistica();
                K++;
            }
            else  //Nos desconectamos
            {
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes("0/");
                server.Send(msg);

                server.Shutdown(SocketShutdown.Both);
                server.Close();

                // Nos desconectamos
                atender.Abort();

                //Vaciar lista
                PlayerList.Items.Clear();

                //Deshabilitar botones al desconectarse
                iniciarSesion.Enabled = false;
                registrar.Enabled = false;
                enviar.Enabled = false;
                label13.Text = "No conectado";
                K++;
            }
        }
        

        private void button1_Click(object sender, EventArgs e)  //iniciar sesion
        {
            if (nombre.Text == "" || pass.Text == "")
            {
                MessageBox.Show("Debes Introducir un Nombre y/o contraseña");
            }
            else
            {
                if (i == 0)
                {
                    string mensaje = "2/" + nombre.Text + "/" + pass.Text;
                    //Enviamos al servidor el nombre tecleado
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
            
                }
                else
                {
                    MessageBox.Show("Intentanto Reconectar");
                    Conexion();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)  //crear usuario
        {
            if (nombre.Text == "" || pass.Text == "")
            {
                MessageBox.Show("Debes Introducir un Nombre y/o contraseña");
            }
            else
            {
                if (i == 0)
                {
                    string mensaje = "1/" + nombre.Text + "/" + pass.Text;
                    // Enviamos al servidor el nombre tecleado
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                   
                }
                else
                {
                    MessageBox.Show("Intentanto Reconectar");
                    Conexion();
                }
            }
        }

        private void estadistica()
        {
            string mensaje_1 = "3/";
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje_1);
            server.Send(msg);

            string mensaje_2 = "4/";
            // Enviamos al servidor el nombre tecleado
            byte[] msg3 = System.Text.Encoding.ASCII.GetBytes(mensaje_2);
            server.Send(msg3);
        }

        private void enviar_Click(object sender, EventArgs e)
        {

                // Quiere saber 
                string mensaje = "5/" + textBox1.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

        }


        private void nueva_Click(object sender, EventArgs e)
        {
            Random rdn = new Random();
            int a = rdn.Next(0, 90);
            int b = rdn.Next(0, 90);
            int c = rdn.Next(0, 90);
            int d = rdn.Next(0, 90);
            int z = rdn.Next(0, 90);
            int f = rdn.Next(0, 90);
            int g = rdn.Next(0, 90);
            int h = rdn.Next(0, 90);
            int w = rdn.Next(0, 90);
            int j = rdn.Next(0, 90);
            int k = rdn.Next(0, 90);
            int l = rdn.Next(0, 90);
            int m = rdn.Next(0, 90);
            int n = rdn.Next(0, 90);
            int o = rdn.Next(0, 90);
            
            l1.Text = a.ToString();
            l2.Text = b.ToString();           
            l3.Text = c.ToString();
            l4.Text = d.ToString();
            l5.Text = z.ToString();
            l6.Text = f.ToString();
            l7.Text = g.ToString();
            l8.Text = h.ToString();
            l9.Text = w.ToString();
            l10.Text = j.ToString();
            l11.Text = k.ToString();
            l12.Text = l.ToString();
            l13.Text = m.ToString();
            l14.Text = n.ToString();
            l15.Text = o.ToString();
        }

        private void start_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
                if (timeLeft > 0)
                {
                    // Display the new time left
                    // by updating the Time Left label.
                    timeLeft = timeLeft - 1;
                    double timeMin;                   
                    timeMin = timeLeft / 60;
                    timeMin = Math.Truncate(timeMin);
                    if (Sec > 0)
                        Sec = Sec - 1;
                    else
                        Sec = 60;

                    label14.Text = "0" + timeMin + ":";
                    label15.Text = Sec + "";
                }
                else
                {
                    // If the user ran out of time, stop the timer, show
                    // a MessageBox, and fill in the answers.
                    timer1.Stop();
                    label14.Text = "Time's up!";
                    MessageBox.Show("You didn't finish in time.", "Sorry!");
                }
        }

         private void atender_mensaje_servidor()
         {
             while (true)
             {
                 //Recibimos mensaje del servidor
                 byte[] msg2 = new byte[80];
                 server.Receive(msg2);
                 string[] trozos = Encoding.ASCII.GetString(msg2).Split('/');
                 int codigo = Convert.ToInt32(trozos[0]);
                 string mensaje = trozos[1].Split('\0')[0];

                 switch (codigo)
                 {
                     case 1:  //crear usuario

                         if (trozos[1].TrimEnd('\0') == "SI")
                         {
                             MessageBox.Show("Registrado.");
                         }
                         else
                         {
                             MessageBox.Show("No registrado.\nInténtelo más tarde.");
                         }

                         break;

                     case 2:  //iniciar sesion

                         if (trozos[1].TrimEnd('\0') == "SI")
                         {
                             MessageBox.Show("Acceso Permitido.");
                             //string nombre = words[2];

                         }
                         else
                         {
                             MessageBox.Show("Acceso Denegado, no está registrado.\nPruebe con otros credenciales o inténtelo más tarde.");
                         }

                         break;

                     case 3:  //servicio1

                         if (trozos[1].TrimEnd('\0') == "SI")
                         {

                             label17.Text = trozos[2];
                            //MessageBox.Show("El jugador que ha ganado más partidas es: " + words[2]);

                         }
                         else
                         {
                             MessageBox.Show("No hay resultado.\nInténtelo más tarde.");
                         }

                         break;

                     case 4:  //servicio1

                         if (trozos[1].TrimEnd('\0') == "SI")
                         {

                             label18.Text = trozos[2];
                            //MessageBox.Show("El jugador que tiene más puntuación es: " + words_2[2]);

                         }
                         else
                         {
                             MessageBox.Show("No hay resultado.\nInténtelo más tarde.");
                         }

                         break;

                     case 5:  //servicio1

                         MessageBox.Show("Los jugadores que han jugado con " + textBox1.Text + " son:" + mensaje + ".");
                         
                         break;

                    case 6:
                        //Lista de conectados 
                        PlayerList.Items.Clear();

                        int i = 0;
                        int result = Int32.Parse(trozos[1]);

                        while (i < result)
                        {
                            PlayerList.Items.Add(trozos[i + 2]);
                            i++;
                        }

                        break;

                 }
             }

         }

    }
}