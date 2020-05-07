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

        string conectado;  //nombre del usuario conectado en este cliente
        string curItem;  //item seleccionado de la lista de conectados
        int K = 0;

        string servicio1;
        string servicio2;

        public Form1()
        {
            InitializeComponent();

            //Necesario para que los elementos de los formularios puedan ser accedidos desde threads diferentes a los que los crearon
            CheckForIllegalCrossThreadCalls = false;

            //Deshabilitar botones al abrir formulario
            iniciarSesion.Enabled = false;
            registrar.Enabled = false;
            enviar.Enabled = false;
            text.Enabled = false;
            empezar.Enabled = false;
            AddPlayer.Enabled = false;
            TextMessage.Enabled = false;

            //Mensaje bienvenida
            //string estadistica = "Histórico de usuario\n\n" + "Partidas jugadas: " + "5\n" + "Bingo: " + "0\n" + "Linea: " + "4\n" + "Mayor bote ganado: " + "20\n\n" + "Global\n\n" + "Bote más alto ganado: " + "500";
            //MessageBox.Show(estadistica,"Estadisticas" ,MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Conexion()
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.1.134");  //147.83.117.22
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (K % 2 == 0)  //Nos conectamos
            {
                Conexion();
                //Habilitar botones al conectarse
                iniciarSesion.Enabled = true;
                registrar.Enabled = true;
                enviar.Enabled = true;
                //estadistica();
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

        private void enviar_Click(object sender, EventArgs e)
        {

            string mensaje_1 = "3/";  //Mas partidas ganadas
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje_1);
            server.Send(msg);

            string mensaje_2 = "4/";  //Mas puntos
            // Enviamos al servidor el nombre tecleado
            byte[] msg3 = System.Text.Encoding.ASCII.GetBytes(mensaje_2);
            server.Send(msg3);

            MessageBox.Show("El usuario que ha ganado más partidas es: " + servicio1 + "\nEl usuario que ha ganado más puntos es: " + servicio2, "Estadisticas",MessageBoxButtons.OK, MessageBoxIcon.Information);

            /* // Quiere saber 
            string mensaje = "5/" + textBox1.Text;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);*/

        }

         private void empezar_Click(object sender, EventArgs e)
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
       

        private void EnableLogIn()  //Habilita botones cuando usuario inicia sesion
        {
            text.Enabled = true;
            empezar.Enabled = true;
            AddPlayer.Enabled = true;
            TextMessage.Enabled = true;
            label6.Text = "Has iniciado sesión como: " + conectado;
            mensajes.Items.Add("Has iniciado sesión como : " + conectado);
            nombre.Clear();
            pass.Clear();
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
                             conectado = trozos[2].TrimEnd('\0');
                             EnableLogIn();
                             //MessageBox.Show("Has iniciado sesión como " + conectado);          
                         }
                         else
                         {
                             MessageBox.Show("Acceso Denegado, no está registrado.\nPruebe con otros credenciales o inténtelo más tarde.");
                         }

                         break;

                     case 3:  //Mas partidas ganadas

                         if (trozos[1].TrimEnd('\0') == "SI")
                         {
                             servicio1 = trozos[2];
                             //MessageBox.Show("El jugador que ha ganado más partidas es: " + words[2]);
                         }
                         else
                         {
                             servicio1 = "Algo ha ido mal.";
                             //MessageBox.Show("No hay resultado.\nInténtelo más tarde.");
                         }

                         break;

                     case 4:  //Mas puntos

                         if (trozos[1].TrimEnd('\0') == "SI")
                         {
                             servicio2 = trozos[2];
                             //MessageBox.Show("El jugador que tiene más puntuación es: " + words_2[2]);
                         }
                         else
                         {
                             servicio2 = "Algo ha ido mal.";
                             //MessageBox.Show("No hay resultado.\nInténtelo más tarde.");
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

                    case 7:

                        if (trozos[1].TrimEnd('\0') == "SI")
                        {                           
                            string anfitrion = trozos[3].TrimEnd('\0');
                            DialogResult resp = MessageBox.Show("¿Aceptas jugar?", anfitrion + " te esta invitando a jugar", MessageBoxButtons.OKCancel);
                            if (resp == DialogResult.OK)
                            {
                                //Acepta
                                ListaInvitar.Items.Add(trozos[2]);
                                string acepta = "7/SI/" + trozos[2] + "/" + trozos[3];
                                byte[] msg = System.Text.Encoding.ASCII.GetBytes(acepta);
                                server.Send(msg);
                            }
                            else
                            {
                                //No acepta
                                string acepta = "7/NO" + trozos[2] + "/" + trozos[3];
                                byte[] msg = System.Text.Encoding.ASCII.GetBytes(acepta);
                                server.Send(msg);
                            }
                        }

                        break;

                    case 8:

                        string invitado = trozos[2].TrimEnd('\0');
                        if (trozos[1].TrimEnd('\0') == "SI")
                        {
                            mensajes.Items.Add(invitado + " ha aceptado jugar.");
                            //MessageBox.Show(invitado + " ha aceptado jugar.");
                        }
                        else
                        {
                            mensajes.Items.Add(invitado + " no ha aceptado jugar.");
                            //MessageBox.Show(invitado + " no ha aceptado jugar.");
                        }

                        break;

                    case 9:

                        string usuario = trozos[1].TrimEnd('\0');
                        mensaje = trozos[2].TrimEnd('\0');
                        string text = usuario + ": " + mensaje;
                        ListText.Items.Add(text);

                        break;

                 }
             }

         }

         private void PlayerList_SelectedIndexChanged(object sender, EventArgs e)
         {
              curItem = PlayerList.SelectedItem.ToString(); 
         }

         private void AddPlayer_Click(object sender, EventArgs e)
         {        
             //MessageBox.Show("current: " + curItem + "\nconectado:" + conectado);

           if (curItem == null)
           {
               MessageBox.Show("No has seleccionado a nadie.");
           }

           else if (curItem == conectado)
           {
               MessageBox.Show("No puedes invitarte a ti mismo.");
           }

           else
           {
               string mensaje = "6/" + curItem + "/" + conectado;
               byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
               server.Send(msg);
           }
         }

         private void text_Click(object sender, EventArgs e)
         {
             if (TextMessage.Text == "")
             {
                 MessageBox.Show("Escribe algo en el cuadro de texto.");
             }
             else
             {
                 string mensaje = "8/" + conectado + "/" + TextMessage.Text;
                 byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                 server.Send(msg);
             }
         }
    }
}