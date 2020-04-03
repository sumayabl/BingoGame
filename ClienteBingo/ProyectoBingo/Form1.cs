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
using System.Threading;

namespace ConsoleApplication1
{
    public partial class Form1 : Form
    {
        Socket server;
        int i;

        private void Conexion()
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9080);


            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                MessageBox.Show("Conectado.");
                i = 0;

            }
            catch (SocketException ex)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No se ha podido conectar con el servidor");
                i = 1;
                return;
            }
        }

        private void conectar_Click(object sender, EventArgs e)
        {
            Conexion();
        }

        private void desconectar_Click(object sender, EventArgs e)
        {
            byte[] msg = System.Text.Encoding.ASCII.GetBytes("0/");
            server.Send(msg);

            server.Shutdown(SocketShutdown.Both);
            server.Close();

            MessageBox.Show("Desconectado.");
        }



        public Form1()
        {
            InitializeComponent();
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

                    byte[] msg2 = new byte[80];
                    server.Receive(msg2);
                    mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                    string[] words = mensaje.Split('/');
                    mensaje = words[1];

                    if (words[1].TrimEnd('\0') == "SI")
                    {
                        MessageBox.Show("Acceso Permitido.");
   
                    }
                    else
                    {
                        MessageBox.Show("Acceso Denegado, no está registrado.\nPruebe con otros credenciales o inténtelo más tarde.");
                    }               
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

                    byte[] msg2 = new byte[80];
                    server.Receive(msg2);
                    mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                    string[] words = mensaje.Split('/');
                    mensaje = words[1];

                    if (words[1].TrimEnd('\0') == "SI")
                    {
                        MessageBox.Show("Registrado.");
                        
                    }
                    else
                    {
                        MessageBox.Show("No registrado.\nInténtelo más tarde.");
                    }
                   
                }
                else
                {
                    MessageBox.Show("Intentanto Reconectar");
                    Conexion();
                }
            }
        }

        private void enviar_Click(object sender, EventArgs e)
        {
            if (servicio1.Checked)  //Quien ha ganado mas partidas
            {
                string mensaje = "3/";
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                string[] words = mensaje.Split('/');
                mensaje = words[1];

                if (words[1].TrimEnd('\0') == "SI")
                {
                    MessageBox.Show("El jugador que ha ganado más partidas es: " + words[2]);
                }
                else
                {
                    MessageBox.Show("No hay resultado.\nIntentelo más tarde.");
                }
                
            }

            else if (servicio2.Checked)  //Quien tiene mas puntuacion
            {
                // Quiere saber 
                string mensaje = "4/";
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                string[] words = mensaje.Split('/');
                mensaje = words[1];

                if (words[1].TrimEnd('\0') == "SI")
                {
                    MessageBox.Show("El jugador que tiene más puntuación es: " + words[2]);
                }
                else
                {
                    MessageBox.Show("No hay resultado.\nIntentelo más tarde.");
                }
            }

            else if (servicio3.Checked)  //Quien ha jugado con otro jugador
            {
                // Quiere saber 
                string mensaje = "5/" + textBox1.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);


                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                //string[] words = mensaje.Split('/');

                MessageBox.Show("Los jugadores que han jugado con " + textBox1.Text + " son:" + mensaje + ".");
            }
           
        }


      //Actualizar la lista de conectados
        private void button5_Click(object sender, EventArgs e)
        {
            string mensaje = "6/";
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split(',')[0];

            MessageBox.Show(mensaje);

            string[] words = mensaje.Split('/');
            PlayerList.Items.Clear();

            int i = 0;
            int result = Int32.Parse(words[0]);

            while (i < result)
            {
                PlayerList.Items.Add(words[i + 1]);
                i++;
            }
        }


       /* 
        private void atender_mensaje_servidor()
        {
            while (true)
            {

                int op;
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                string mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];  //corta donde esta el final de linea
            
              //  mensaje = mensaje.TrimEnd('\0');      //todo lo que no sirve lo borra
        
                string[] words = mensaje.Split('/');  //guardas las palabras en words y separa el mensaje en barras
 
                op = Convert.ToInt32(words[0]);
             



                switch (op)
                {
                    case 1:  //crear usuario
                        mensaje = words[1];

                        //MessageBox.Show(words[1]);

                        if (words[1].TrimEnd('\0') == "SI")
                        {
                            MessageBox.Show("Registrado.");
                        }
                        else
                        {
                            MessageBox.Show("No registrado.\nInténtelo más tarde.");
                        }

                        break;

                    case 2:  //iniciar sesion

                        mensaje = words[1];

                        if (words[1].TrimEnd('\0') == "SI")
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

                        mensaje = words[2];

                        if (words[1].TrimEnd('\0') == "SI")
                        {

                            MessageBox.Show("El jugador que más partidas ha ganado es: " + mensaje);
                            //string nombre = words[2];

                        }
                        else
                        {
                            MessageBox.Show("No hay resultado.\nSe ha producido un error o inténtelo más tarde.");
                        }

                        break;

                    case 4:  //servicio1

                        mensaje = words[2];

                        if (words[1].TrimEnd('\0') == "SI")
                        {

                            MessageBox.Show("El jugador que mayor puntuación ha conseguido es: " + mensaje);
                            //string nombre = words[2];

                        }
                        else
                        {
                            MessageBox.Show("No hay resultado.\nSe ha producido un error o inténtelo más tarde.");
                        }

                        break;

                    case 5:  //servicio1

                        mensaje = words[2];

                        if (words[1].TrimEnd('\0') == "SI")
                        {

                            MessageBox.Show("El jugador que mayor puntuación ha conseguido es: " + mensaje);
                            //string nombre = words[2];

                        }
                        else
                        {
                            MessageBox.Show("No hay resultado.\nSe ha producido un error o inténtelo más tarde.");
                        }

                        break;
                }
            }

        }*/

    }
}