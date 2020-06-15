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
using System.Threading; //Se ha añadido para los threads
using System.Timers;
using System.Media;
using System.IO;

namespace ConsoleApplication1
{
    public partial class Form1 : Form
    {
        Socket server;
        Thread atender;
        int i;
        int turno = 0;
        int dejar;
        int Tbolas = 0;
        int Tcarton = 0;
        int speaker = 1;
        int puntos;
        string bote;

        Thread bolas;
        Thread CartonNum;
        Thread aviso;

        //Timer
        int origTime = 240;
        int tiempo = 0;

        int a,b,c,d,f,g,h,n,j,k,l,m,o,w,z;

        int linea1;
        int linea2;
        int linea3;

        //private int quick = 14400;
  
        string conectado;  //nombre del usuario conectado en este cliente
        string anfitrion;  //guardamos el anfitrion de la partida actual
        string curItem;  //item seleccionado de la lista de conectados
        int K = 0;
        int C = 0;
        int Q = 0;
        int nueva = 0;

        string servicio1;
        string servicio2;
        string servicio3;

        //Declaro una clase delegada
        delegate void DelegadoParaEscribir(string nombre);
        delegate void DelegadoParaMensajes(string conectado);
        delegate void DelegadoParaMensajesInvitados(string invitado, string texto);
        delegate void DelegadoParaMensajesGeneral(string texto);
        delegate void DelegadoListaInvitados(string invitado);
        delegate void DelegadoListaConectados(string conectado);
        delegate void DelegadoChat(string text);
        delegate void DelegadoLimpiarLista();
        delegate void DelegadoLimpiarInvitados();
        delegate void DelegadoParaLogIn();
        delegate void DelegadoBola(string bola);
        delegate void DelegadoParaLinea();
        delegate void DelegadoParaBingo();
        delegate void DelegadoParaCarton();
        delegate void DelegadoRestablecer();
        delegate void DelegadoDarCarton();
        delegate void DelegadoParaAviso();
        delegate void DelegadoAnfitrion(string conectado);
        delegate void DelegadoAnfitrionLimpiar();
        delegate void DelegadoParaBote(string bote);
        delegate void DelegadoParaTimer();
        delegate void DelegadoParaPuntos(string puntos);

        public Form1()
        {
            InitializeComponent();

            //Necesario para que los elementos de los formularios puedan ser accedidos desde threads diferentes a los que los crearon
            //CheckForIllegalCrossThreadCalls = false;

            //Deshabilitar botones al abrir formulario
            iniciarSesion.Enabled = false;
            registrar.Enabled = false;
            enviar.Enabled = false;
            text.Enabled = false;
            empezar.Enabled = false;
            AddPlayer.Enabled = false;
            TextMessage.Enabled = false;
            bingo.Enabled = false;
            linea.Enabled = false;
            enviar.Visible = false;
            dejarButton.Visible = false;
        }

        public void PonBote(string bote)  //Funcion nueva que creo por el tema del excepcion cross thread
        {
            botelbl.Text = bote;
        }

        public void PonNombre(string texto)  //Funcion nueva que creo por el tema del excepcion cross thread
        {
            label6.Text = texto;
        }

        public void PonMensaje(string conectado)
        {
            mensajes.Items.Add("Has iniciado sesión como: " + conectado);
            mensajes.TopIndex = mensajes.Items.Count - 1;
        }

        public void PonMensajeInvitado(string invitado, string texto)
        {
            mensajes.Items.Add(invitado + "" + texto);
            mensajes.TopIndex = mensajes.Items.Count - 1;
        }

        public void PonMensajeGeneral(string texto)
        {
            mensajes.Items.Add(texto);
            mensajes.TopIndex = mensajes.Items.Count - 1;
        }

        public void PonAnfitrion(string conectado)
        {
            label10.Text = conectado;
        }

        public void LimpiaAnfitrion()
        {
            label10.Text = null;
        }

        public void PonListaInvitados(string invitado)
        {
            ListaInvitar.Items.Add(invitado);
        }

        public void LimpiarListaInvitados()
        {
            ListaInvitar.Items.Clear();
        }

        public void LimpiarLista()
        {
            PlayerList.Items.Clear();
        }

        public void PonListaConectados(string conectado)
        {
            PlayerList.Items.Add(conectado);
        }

        public void PonerText(string text)
        {
            ListText.Items.Add(text);
            ListText.TopIndex = ListText.Items.Count - 1;
            //if ((speaker - 1) % 2 == 0)  //Sonido activado
            //{
            //    playSimpleSoundText();
            //}        
        }

        public void PonAviso()
        {
            cuadro.Text = "Terminado";
        }

        public void PonTimer()
        {
            if (tiempo == 0)
            {
                timer1.Interval = 1000;
                timer1.Tick += new EventHandler(timer1_Tick);
                timer1.Start();
                tiempo = -1;

                cuadro.Text = "En juego!";
                ThreadStart ta = delegate { atender_aviso(); };
                aviso = new Thread(ta);
                aviso.Start();
            }
        }

        public void PararTimer()
        {
            if (tiempo == -1)
            {
                timer1.Stop();
                tiempo = 0;
                origTime = 240;
                timer.Text = "00:00";
            }
        }
                
        public void Restablecer()
        {
            UltimosNumeros.Items.Clear(); 
            labelbola.Text = "90";
            l1.Text = "B";
            l2.Text = "I";
            l3.Text = "N";
            l4.Text = "G";
            l5.Text = "B";
            l6.Text = "I";
            l7.Text = "N";
            l8.Text = "G";
            l9.Text = "O";
            l10.Text = "I";
            l11.Text = "B";
            l12.Text = "N";
            l13.Text = "G";
            l14.Text = "O";
            l15.Text = "O";
        }

        public void BotonLinea()
        {
            linea.Enabled = true;
        }

        public void BotonLineaOff()
        {
            linea.Enabled = false;
        }

        public void BotonBingo()
        {
            bingo.Enabled = true;
        }

        public void LogIn()
        {
            text.Enabled = true;
            empezar.Enabled = true;
            TextMessage.Enabled = true;
            enviar.Visible = true;
            dejarButton.Visible = true;
            nombre.Clear();
            pass.Clear();

        }

        public void PonerBola(string bola)
        {
            labelbola.Text = bola;
            UltimosNumeros.Items.Add(bola);
            UltimosNumeros.TopIndex = UltimosNumeros.Items.Count - 1;
        }

        public void PonerPuntos(string puntos)
        {
            puntuacion.Text = puntos;
        }

        private void DarCarton()
        {
            Random rdn = new Random();
            //a = rdn.Next(0, 30);
            //b = rdn.Next(0, 30);
            //c = rdn.Next(0, 30);
            //d = rdn.Next(0, 30);
            //z = rdn.Next(0, 30);
            //f = rdn.Next(0, 30);
            //g = rdn.Next(0, 30);
            //h = rdn.Next(0, 30);
            //w = rdn.Next(0, 30);
            //j = rdn.Next(0, 30);
            //k = rdn.Next(0, 30);
            //l = rdn.Next(0, 30);
            //m = rdn.Next(0, 30);
            //n = rdn.Next(0, 30);
            //o = rdn.Next(0, 30);

            //LE DOY VALORES PARA FORZAR EL BINGO
            a = 1;
            b = 2;
            c = 3;
            d = 4;
            z = 5;
            f = 6;
            g = 7;
            h = 8;
            w = 9;
            j = 10;
            k = 11;
            l = 12;
            m = 13;
            n = 14;
            o = 15;

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

        //private void playSimpleSound()
        //{
        //    SoundPlayer simpleSound = new SoundPlayer(@"\\sounds\\bola.wav");
        //    simpleSound.Play();
        //    //string path = Directory.GetCurrentDirectory();
        //    //MessageBox.Show("The current directory is {0}" + path);
        //}

        ////string pathToFiles = Server.MapPath("../Sonidos/correct.wav");
        ////    System.Media.SoundPlayer player = new System.Media.SoundPlayer(pathToFiles);
        ////    player.Play();

        //private void playSimpleSoundText()
        //{
        //    SoundPlayer simpleSound = new SoundPlayer(@"ClienteBingo\sounds\text.wav");
        //    simpleSound.Play();
        //}

        private void vaciarCarton()
        {
            num1.Checked = false;
            num2.Checked = false;
            num3.Checked = false;
            num4.Checked = false;
            num5.Checked = false;
            num6.Checked = false;
            num7.Checked = false;
            num8.Checked = false;
            num9.Checked = false;
            num10.Checked = false;
            num11.Checked = false;
            num12.Checked = false;
            num13.Checked = false;
            num14.Checked = false;
            num15.Checked = false;
        }

        private void Conexion()
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("147.83.117.22");  //147.83.117.22
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
                DelegadoParaMensajesGeneral delegadoMensajesGeneral = new DelegadoParaMensajesGeneral(PonMensajeGeneral);
                mensajes.Invoke(delegadoMensajesGeneral, new object[] { "No se ha podido conectar con el servidor." });

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
            //Conexion();
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
                K++;
            }
            else  //Nos desconectamos
            {
                if (conectado != null)  //Si hay alguien conectado
                {
                    // Enviamos al servidor el nombre tecleado
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes("0/" + conectado);
                    server.Send(msg);

                    server.Shutdown(SocketShutdown.Both);
                    server.Close();

                    // Nos desconectamos
                    atender.Abort();
                    if (Tbolas == 1)  //Lo paramo solo si está activo
                    {
                        bolas.Abort();
                    }

                    //Vaciar lista
                    PlayerList.Items.Clear();

                    //Deshabilitar botones al desconectarse
                    iniciarSesion.Enabled = false;
                    registrar.Enabled = false;
                    enviar.Enabled = false;
                    label13.Text = "No conectado";
                    conectado = "";
                    K++;
                }
                else  //Si no hay nadie conectado
                {
                    iniciarSesion.Enabled = false;
                    registrar.Enabled = false;
                    enviar.Enabled = false;
                    label13.Text = "No conectado";
                    K++;
                }
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
                    DelegadoParaMensajesGeneral delegadoMensajesGeneral = new DelegadoParaMensajesGeneral(PonMensajeGeneral);
                    mensajes.Invoke(delegadoMensajesGeneral, new object[] { "Intentando reconectar." });
                    //MessageBox.Show("Intentanto Reconectar");
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
            string mensaje_1 = "3/" + conectado;  //Mas partidas ganadas
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje_1);
            server.Send(msg);

            //string mensaje_3 = "5/" + conectado;
            //// Enviamos al servidor el nombre tecleado
            //byte[] msg2 = System.Text.Encoding.ASCII.GetBytes(mensaje_3);
            //server.Send(msg2);

            //string mensaje_2 = "4/";  //Mas puntos
            //// Enviamos al servidor el nombre tecleado
            //byte[] msg1 = System.Text.Encoding.ASCII.GetBytes(mensaje_2);
            //server.Send(msg1);
        }

        private void empezar_Click(object sender, EventArgs e)  //Nueva y empezar
        {
            if (nueva == 0)  //Si no esta en ninguna partida
            {
                string mensaje = "14/" + conectado;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                anfitrion = conectado;
                empezar.Text = "Empezar!";
                AddPlayer.Enabled = true;
                nueva = 1;
 
                //Solicitar bote
                string mensaje1 = "15/" + conectado;
                // Enviamos al servidor el nombre tecleado
                byte[] msg1 = System.Text.Encoding.ASCII.GetBytes(mensaje1);
                server.Send(msg1);
            }
            else  //Si ya esta en una partida, partida en curso o pendiente de empezar
            {
                if (turno == 0)  //Si la partida aun no ha empezado
                {
                    turno = 1;
                    dejar = 0;
                    AddPlayer.Enabled = false;
                    string mensaje = "9/" + conectado;
                    // Enviamos al servidor el nombre tecleado
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                   
                    //empezar.Enabled = false;
                    DelegadoDarCarton delegadoDarCarton = new DelegadoDarCarton(DarCarton);
                    l1.Invoke(delegadoDarCarton, new object[] { });

                    //pongo en marcha el thread que dará bolas cada 5 segundos
                    ThreadStart tb = delegate { atender_bolas(); };
                    bolas = new Thread(tb);
                    bolas.Start();
                    //Para saber si el thread está activo
                    Tbolas = 1;

                    //pongo en marcha el thread que comprobara el carton a cada momento
                    ThreadStart tc = delegate { AtenderCarton(); };
                    CartonNum = new Thread(tc);
                    CartonNum.Start();
                    Tcarton = 1;

                    cuadro.Text = "En juego!";
                    ThreadStart ta = delegate { atender_aviso(); };
                    aviso = new Thread(ta);
                    aviso.Start();

                    //Timer
                    if (tiempo == 0)
                    {
                        timer1.Interval = 1000;
                        timer1.Tick += new EventHandler(timer1_Tick);
                        timer1.Start();
                        tiempo = -1;
                    }
                }
                else  //Si la partida esta en marcha
                {
                    DelegadoParaMensajesGeneral delegadoMensajesGeneral = new DelegadoParaMensajesGeneral(PonMensajeGeneral);
                    mensajes.Invoke(delegadoMensajesGeneral, new object[] { "Deja la partida en curso para unirte a una nueva." });
                }
                
            }
        }

        //Timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            origTime--;
            timer.Text = origTime / 60 + ":" + ((origTime % 60) >= 10 ? (origTime % 60).ToString() : "0" + (origTime % 60));
        }
       
        private void EnableLogIn()  //Habilita botones cuando usuario inicia sesion
        {
            DelegadoParaLogIn delegadoLogIn = new DelegadoParaLogIn(LogIn);
            text.Invoke(delegadoLogIn);
            empezar.Invoke(delegadoLogIn);
            AddPlayer.Invoke(delegadoLogIn);
            TextMessage.Invoke(delegadoLogIn);
            nombre.Invoke(delegadoLogIn);
            pass.Invoke(delegadoLogIn);

            //label6.Text = "Has iniciado sesión como: " + conectado;  //Lo quito de aqui por el tema del excepcion cross thread
            DelegadoParaEscribir delegado = new DelegadoParaEscribir(PonNombre);
            label6.Invoke(delegado, new object[] { "Usuario: " + conectado });

            //mensajes.Items.Add("Has iniciado sesión como : " + conectado);
            DelegadoParaMensajes delegadoMensajes = new DelegadoParaMensajes(PonMensaje);
            mensajes.Invoke(delegadoMensajes, new object[] {conectado});
        }

        private void atender_aviso()
        {
            while (true)
            {
                Thread.Sleep(600);
                if (C % 2 == 0)
                {
                    cuadro.BackColor = Color.Tomato;
                    C++;
                }
                else
                {
                    cuadro.BackColor = Color.Red;
                    C++;
                }
            }
        }

        private void AtenderCarton()
        {
            while (true)
            {
                //Si hago linea 1 enable boton linea
                if ((num1.Checked == true) && (num2.Checked == true) && (num3.Checked == true) && (num4.Checked == true) && (num5.Checked == true))
                {
                    DelegadoParaLinea delegadoLinea = new DelegadoParaLinea(BotonLinea);
                    num1.Invoke(delegadoLinea);
                }
                //Si hago linea 2 enable boton linea
                if ((num6.Checked == true) && (num7.Checked == true) && (num8.Checked == true) && (num9.Checked == true) && (num10.Checked == true))
                {
                    DelegadoParaLinea delegadoLinea = new DelegadoParaLinea(BotonLinea);
                    num6.Invoke(delegadoLinea);
                }
                //Si hago linea 3 enable boton linea
                if ((num11.Checked == true) && (num12.Checked == true) && (num13.Checked == true) && (num14.Checked == true) && (num15.Checked == true))
                {
                    DelegadoParaLinea delegadoLinea = new DelegadoParaLinea(BotonLinea);
                    num11.Invoke(delegadoLinea);
                }
                //Si hago bingo enable boton bingo
                if ((num1.Checked == true) && (num2.Checked == true) && (num3.Checked == true) && (num4.Checked == true) && (num5.Checked == true) && (num6.Checked == true) && (num7.Checked == true) && (num8.Checked == true) && (num9.Checked == true) && (num10.Checked == true) && (num11.Checked == true) && (num12.Checked == true) && (num13.Checked == true) && (num14.Checked == true) && (num15.Checked == true))
                {
                    DelegadoParaBingo delegadoBingo = new DelegadoParaBingo(BotonBingo);
                    num1.Invoke(delegadoBingo);
                }
            }
        }

        private void atender_bolas()
        {
            while (true)
            {
                Thread.Sleep(3000);

                string mensaje = "9/" + conectado;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
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
                             conectado = trozos[2].TrimEnd('\0');
                             EnableLogIn();         
                         }
                         else
                         {
                             MessageBox.Show("Acceso Denegado, no está registrado.\nPruebe con otros credenciales o inténtelo más tarde.");
                         }

                         break;

                     case 3:  //Mas partidas ganadas

                         if (trozos[1].TrimEnd('\0') == "SI")
                         {
                             servicio1 = trozos[2].TrimEnd('\0');
                             //MessageBox.Show("El jugador que ha ganado más partidas es: " + words[2]);
                         }
                         if (trozos[4].TrimEnd('\0') == "SI")
                         {
                             servicio2 = trozos[5].TrimEnd('\0');
                             //MessageBox.Show("No hay resultado.\nInténtelo más tarde.");
                         }
                         if (trozos[7].TrimEnd('\0') == "SI")
                         {
                             servicio3 = trozos[8].TrimEnd('\0');
                         }

                         MessageBox.Show("Id de partidas en las que ha jugado " + conectado + ":\n\n" + servicio1 +
                            "\n\nEl usuario que ha obtenido más puntos es:\n\n " + servicio2 +
                            "\n\nLos jugadores que han jugado con " + conectado + " son:\n\n" + servicio3
                            , "Estadisticas", MessageBoxButtons.OK, MessageBoxIcon.Information);

                         break;

                     case 4:  //Mas puntos

                         if (trozos[1].TrimEnd('\0') == "SI")
                         {
                             servicio2 = trozos[2].TrimEnd('\0');
                             //MessageBox.Show("El jugador que tiene más puntuación es: " + servicio2);

                             MessageBox.Show("El usuario que ha ganado más partidas es: " + servicio1 +
                             "\n\nEl usuario que ha ganado más puntos es: " + servicio2 +
                             "\n\nLos jugadores que han jugado con " + conectado + " son: " + servicio3 + "."
                             , "Estadisticas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         }
                         else
                         {
                             servicio2 = "No se han obtenido datos.";
                             //MessageBox.Show("No hay resultado.\nInténtelo más tarde.");
                         }

                         break;

                     case 5:  //Quien ha jugado con conectado

                         if (trozos[1].TrimEnd('\0') == "SI")
                         {
                             servicio3 = trozos[2];
                         }
                         else
                         {
                             servicio3 = "No se han obtenido datos.";
                             //MessageBox.Show("No hay resultado.\nInténtelo más tarde.");
                         }                         
                         break;

                    case 6:
                        //Lista de conectados 

                        DelegadoLimpiarLista delegadoLimpiarLista = new DelegadoLimpiarLista(LimpiarLista);
                        PlayerList.Invoke(delegadoLimpiarLista);

                        int i = 0;
                        int result = Int32.Parse(trozos[1]);

                        while (i < result)
                        {
                            DelegadoListaConectados delegadoListaConectados = new DelegadoListaConectados(PonListaConectados);
                            PlayerList.Invoke(delegadoListaConectados, new object[] {trozos[i + 2]});
                            i++;
                        }

                        break;

                    case 7:  //Peticion invitacion

                        if (trozos[1].TrimEnd('\0') == "SI")
                        {                           
                            anfitrion = trozos[3].TrimEnd('\0');
                            DialogResult resp = MessageBox.Show("¿Aceptas jugar?", anfitrion + " te esta invitando a jugar", MessageBoxButtons.OKCancel);
                            if (resp == DialogResult.OK)
                            {
                                //Acepta                            
                                //DelegadoListaInvitados delegadoListaInvitados = new DelegadoListaInvitados(PonListaInvitados);
                                //ListaInvitar.Invoke(delegadoListaInvitados, new object[] {trozos[2]});

                                DelegadoAnfitrion delegadoListaAnfitrion = new DelegadoAnfitrion(PonAnfitrion);
                                label10.Invoke(delegadoListaAnfitrion, new object[] { anfitrion });

                                string acepta = "7/SI/" + trozos[2] + "/" + trozos[3];
                                byte[] msg = System.Text.Encoding.ASCII.GetBytes(acepta);
                                server.Send(msg);

                                anfitrion = trozos[3].TrimEnd('\0');

                                DelegadoDarCarton delegadoDarCarton = new DelegadoDarCarton(DarCarton);
                                l1.Invoke(delegadoDarCarton, new object[] { });

                                linea1 = 0;
                                linea2 = 0;
                                linea3 = 0;
                                dejar = 0;

                                //pongo en marcha el thread que comprobara el carton a cada momento
                                ThreadStart tc = delegate { AtenderCarton(); };
                                CartonNum = new Thread(tc);
                                CartonNum.Start();
                                Tcarton = 1;
                            }
                            else
                            {
                                //No acepta
                                string acepta = "7/NO" + "/" + trozos[2] + "/" + trozos[3];
                                byte[] msg = System.Text.Encoding.ASCII.GetBytes(acepta);
                                server.Send(msg);
                            }
                        }

                        break;

                    case 8:  //Respuesta invitacion

                        string invitado = trozos[2].TrimEnd('\0');
                        if (trozos[1].TrimEnd('\0') == "SI")
                        {
                            string texto = " ha aceptado jugar.";
                            DelegadoParaMensajesInvitados delegadoMensajesInvitados = new DelegadoParaMensajesInvitados(PonMensajeInvitado);
                            mensajes.Invoke(delegadoMensajesInvitados, new object[] {invitado, texto});                            

                            if (Q == 0)  //Cuando alguien acepta jugar solo una vez ponemos anfitrion
                            {
                                DelegadoAnfitrion delegadoListaAnfitrion = new DelegadoAnfitrion(PonAnfitrion);
                                label10.Invoke(delegadoListaAnfitrion, new object[] { conectado });
                                Q = 1;
                            }

                            string mensaje1 = "15/" + conectado;
                            // Enviamos al servidor el nombre tecleado
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje1);
                            server.Send(msg);
                        }
                        else if (trozos[1].TrimEnd('\0') == "1")
                        {
                            DelegadoLimpiarInvitados delegadoLimpiarListaI = new DelegadoLimpiarInvitados(LimpiarListaInvitados);
                            ListaInvitar.Invoke(delegadoLimpiarListaI);

                            int u = 3;
                            int numero = Convert.ToInt32(trozos[2]);
                            while (u < numero+3)
                            {
                                string nombre_invitado = trozos[u].TrimEnd('\0');
                                DelegadoListaInvitados delegadoListaInvitados = new DelegadoListaInvitados(PonListaInvitados);
                                ListaInvitar.Invoke(delegadoListaInvitados, new object[] { nombre_invitado });
                                u++;
                            }
                        }
                        else
                        {
                            string texto = " no ha aceptado jugar.";
                            DelegadoParaMensajesInvitados delegadoMensajesInvitados = new DelegadoParaMensajesInvitados(PonMensajeInvitado);
                            mensajes.Invoke(delegadoMensajesInvitados, new object[] { invitado, texto });
                        }

                        break;

                    case 9:  //Insertar mensaje en chat

                        string usuario = trozos[1].TrimEnd('\0');
                        mensaje = trozos[2].TrimEnd('\0');
                        string text = usuario + ": " + mensaje;

                        DelegadoChat delegadoChat = new DelegadoChat(PonerText);
                        mensajes.Invoke(delegadoChat, new object[] {text});

                        break;

                    case 10:  //Recibe bola nueva

                        string bola = trozos[1].TrimEnd('\0');

                        DelegadoBola delegadobola = new DelegadoBola(PonerBola);
                        labelbola.Invoke(delegadobola, new object[] { bola });
                        //if ((speaker-1) % 2 == 0)  //Sonido activado
                        //{
                        //    playSimpleSound();
                        //}
                        DelegadoParaTimer delegadotimer = new DelegadoParaTimer(PonTimer);
                        timer.Invoke(delegadotimer);
                                                 
                        break;

                    case 11:  //Recibe ganador para fin de partida

                        string okBingo = trozos[1].TrimEnd('\0');
                        if (okBingo == "SI")
                        {
                            //Paramos de enviar bolas
                            if (Tbolas == 1)  //Lo paramo solo si está activo
                            {
                                bolas.Abort();
                            }
                            aviso.Abort();
                            DelegadoParaAviso delegadoAviso = new DelegadoParaAviso(PonAviso);
                            cuadro.Invoke(delegadoAviso);
                            cuadro.BackColor = Color.Green;

                            if (trozos[2].TrimEnd('\0') == conectado)
                            {
                                DelegadoParaMensajesGeneral delegadoMensajesGeneral = new DelegadoParaMensajesGeneral(PonMensajeGeneral);
                                mensajes.Invoke(delegadoMensajesGeneral, new object[] { "Enhorabuena, has hecho BINGO. Has ganado!" });

                                DelegadoParaTimer delegadotimer1 = new DelegadoParaTimer(PararTimer);
                                timer.Invoke(delegadotimer1);

                                puntos = Convert.ToInt32(bote);

                                DelegadoParaPuntos delegadopuntos = new DelegadoParaPuntos(PonerPuntos);
                                puntuacion.Invoke(delegadopuntos, new object[] { bote });
                            }
                            else 
                            {
                                DelegadoParaMensajesGeneral delegadoMensajesGeneral = new DelegadoParaMensajesGeneral(PonMensajeGeneral);
                                mensajes.Invoke(delegadoMensajesGeneral, new object[] { "Otro hizo bingo. Se acabó la partida." });

                                DelegadoParaTimer delegadotimer1 = new DelegadoParaTimer(PararTimer);
                                timer.Invoke(delegadotimer1);
                            }                                              
                        }
                        else
                        {
                            //La casilla que has seleccionado no es correcta
                            DelegadoParaCarton delegadocarton = new DelegadoParaCarton(vaciarCarton);
                            num1.Invoke(delegadocarton);

                            DelegadoParaMensajesGeneral delegadoMensajesGeneral = new DelegadoParaMensajesGeneral(PonMensajeGeneral);
                            mensajes.Invoke(delegadoMensajesGeneral, new object[] {"No has hecho BINGO, revisa las bolas que han salido."});
                        }
                        break;

                    case 12:  //Recibe ganador para fin de partida

                        string okLinea = trozos[1].TrimEnd('\0');
                        if (okLinea == "SI")
                        {
                            if ((trozos[2].TrimEnd('\0') == conectado) || (trozos[2].TrimEnd('\0') == "SI"))
                            {
                                DelegadoParaMensajesGeneral delegadoMensajesGeneral = new DelegadoParaMensajesGeneral(PonMensajeGeneral);
                                mensajes.Invoke(delegadoMensajesGeneral, new object[] { "Enhorabuena, has hecho Linea." });
                                puntos = puntos + 20;
                                string puntos1 = puntos.ToString();

                                DelegadoParaPuntos delegadopuntos = new DelegadoParaPuntos(PonerPuntos);
                                puntuacion.Invoke(delegadopuntos, new object[] { puntos1 });
                            }
                            else
                            {
                                DelegadoParaMensajesGeneral delegadoMensajesGeneral = new DelegadoParaMensajesGeneral(PonMensajeGeneral);
                                mensajes.Invoke(delegadoMensajesGeneral, new object[] { "Otro hizo linea." });
                            }
                        }
                        else
                        {
                            DelegadoParaMensajesGeneral delegadoMensajesGeneral = new DelegadoParaMensajesGeneral(PonMensajeGeneral);
                            mensajes.Invoke(delegadoMensajesGeneral, new object[] { "No has hecho linea, revisa las bolas que han salido." });
                        }
                        break;

                    case 13:  //Respuesta dejar partida
                        {
                            //DelegadoParaCarton delegadocarton = new DelegadoParaCarton(vaciarCarton);
                            //num1.Invoke(delegadocarton, new object[] { });

                            //DelegadoRestablecer delegadorestablecer = new DelegadoRestablecer(Restablecer);
                            //UltimosNumeros.Invoke(delegadorestablecer, new object[] { });

                            DelegadoLimpiarInvitados delegadoLimpiarListaI = new DelegadoLimpiarInvitados(LimpiarListaInvitados);
                            ListaInvitar.Invoke(delegadoLimpiarListaI);
                                                        
                            //DelegadoParaTimer delegadotimer1 = new DelegadoParaTimer(PararTimer);
                            //timer.Invoke(delegadotimer1);

                            string tipo = trozos[1].TrimEnd('\0');

                            if (tipo == "A")  //Si quien deja la partida ha sido el anfitrion
                            {
                                DelegadoParaCarton delegadocarton = new DelegadoParaCarton(vaciarCarton);
                                num1.Invoke(delegadocarton, new object[] { });

                                DelegadoRestablecer delegadorestablecer = new DelegadoRestablecer(Restablecer);
                                UltimosNumeros.Invoke(delegadorestablecer, new object[] { });

                                DelegadoParaMensajesGeneral delegadoMensajesGeneral = new DelegadoParaMensajesGeneral(PonMensajeGeneral);
                                mensajes.Invoke(delegadoMensajesGeneral, new object[] { "El anfitrion ha salido de la partida, se acabó." });

                                DelegadoAnfitrionLimpiar delegadoLimpiarAnfitrion = new DelegadoAnfitrionLimpiar(LimpiaAnfitrion);
                                label10.Invoke(delegadoLimpiarAnfitrion);

                                DelegadoParaTimer delegadotimer1 = new DelegadoParaTimer(PararTimer);
                                timer.Invoke(delegadotimer1);

                                linea1 = 0;
                                linea2 = 0;
                                linea3 = 0;

                                DelegadoParaLinea delegadoLinea = new DelegadoParaLinea(BotonLineaOff);
                                linea.Invoke(delegadoLinea);

                                if (Tcarton == 1)  //Lo paramo solo si está activo
                                {
                                    CartonNum.Abort();
                                    Tcarton = 0;
                                }

                                puntos = 0;
                            }
                            else
                            {
                                string nombre = trozos[2].TrimEnd('\0');
                                if (nombre == conectado)
                                {
                                    DelegadoParaCarton delegadocarton = new DelegadoParaCarton(vaciarCarton);
                                    num1.Invoke(delegadocarton, new object[] { });

                                    DelegadoRestablecer delegadorestablecer = new DelegadoRestablecer(Restablecer);
                                    UltimosNumeros.Invoke(delegadorestablecer, new object[] { });

                                    DelegadoParaMensajesGeneral delegadoMensajesGeneral = new DelegadoParaMensajesGeneral(PonMensajeGeneral);
                                    mensajes.Invoke(delegadoMensajesGeneral, new object[] { "Has salido de la partida." });

                                    DelegadoAnfitrionLimpiar delegadoLimpiarAnfitrion = new DelegadoAnfitrionLimpiar(LimpiaAnfitrion);
                                    label10.Invoke(delegadoLimpiarAnfitrion);

                                    DelegadoParaTimer delegadotimer1 = new DelegadoParaTimer(PararTimer);
                                    timer.Invoke(delegadotimer1);

                                    linea1 = 0;
                                    linea2 = 0;
                                    linea3 = 0;

                                    DelegadoParaLinea delegadoLinea = new DelegadoParaLinea(BotonLineaOff);
                                    linea.Invoke(delegadoLinea);

                                    if (Tcarton == 1)  //Lo paramo solo si está activo
                                    {
                                        CartonNum.Abort();
                                        Tcarton = 0;
                                    }

                                    puntos = 0;
                                }
                                else
                                {
                                    int u = 4;
                                    int numero = Convert.ToInt32(trozos[3]);
                                    while (u < numero + 4)
                                    {
                                        string nombre_invitado = trozos[u].TrimEnd('\0');
                                        DelegadoListaInvitados delegadoListaInvitados = new DelegadoListaInvitados(PonListaInvitados);
                                        ListaInvitar.Invoke(delegadoListaInvitados, new object[] { nombre_invitado });
                                        u++;
                                    }
                                }
                                                              
                            }
                        }
                        break;

                    case 14:  //Respuesta eliminar usuario
                        {
                            string okBorrar = trozos[1].TrimEnd('\0');
                            if (okBorrar == "SI")
                            {
                                DelegadoParaMensajesGeneral delegadoMensajesGeneral = new DelegadoParaMensajesGeneral(PonMensajeGeneral);
                                mensajes.Invoke(delegadoMensajesGeneral, new object[] { conectado + " ha sido borrado del sistema." });
                                delete();
                            }
                            else 
                            {
                                DelegadoParaMensajesGeneral delegadoMensajesGeneral = new DelegadoParaMensajesGeneral(PonMensajeGeneral);
                                mensajes.Invoke(delegadoMensajesGeneral, new object[] { "No se ha podido borrar al usuario." });
                            }
                        }
                        break;

                    case 15:
                        {
                            bote = trozos[1].TrimEnd('\0');
                            DelegadoParaBote delegadoBote = new DelegadoParaBote(PonBote);
                            botelbl.Invoke(delegadoBote, new object[] { bote }); 
                        }
                        break;
                 }
             }
         }

         private void PlayerList_SelectedIndexChanged(object sender, EventArgs e)
         {
              curItem = PlayerList.SelectedItem.ToString();
              curItem = curItem.TrimEnd('\0');
         }

         private void AddPlayer_Click(object sender, EventArgs e)  //Invitar jugador
         {        
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
               string mensaje = "6/" + curItem + "/" + conectado.TrimEnd('\0');
               byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
               server.Send(msg);
           }
         }

         private void text_Click(object sender, EventArgs e)  //Chat
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
                 TextMessage.Text="";
             }
         }

         private void bingo_Click(object sender, EventArgs e)
         {
             if ((num1.Checked == true) && (num2.Checked == true) && (num3.Checked == true) && (num4.Checked == true) && (num5.Checked == true) && (num6.Checked == true) && (num7.Checked == true) && (num8.Checked == true) && (num9.Checked == true) && (num10.Checked == true) && (num11.Checked == true) && (num12.Checked == true) && (num13.Checked == true) && (num14.Checked == true) && (num15.Checked == true))
             {
                 bingo.Enabled = false;
                 //Recogemos todas las bolas             
                 string mensaje = "11/" + conectado + "/" + a + "/" + b + "/" + c + "/" + d + "/" + z + "/" + f + "/" + g + "/" + h + "/" + w + "/" + j + "/" + k + "/" + l + "/" + m + "/" + n + "/" + o;
                 byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                 server.Send(msg);
             }
             else 
             {
                 DelegadoParaMensajesGeneral delegadoMensajesGeneral = new DelegadoParaMensajesGeneral(PonMensajeGeneral);
                 mensajes.Invoke(delegadoMensajesGeneral, new object[] { "No has marcado todos los números." });
             }
         }

         private void linea_Click(object sender, EventArgs e)
         {
             //Si hago linea 1 enable boton linea
             if ((linea1 == 0) && (num1.Checked == true) && (num2.Checked == true) && (num3.Checked == true) && (num4.Checked == true) && (num5.Checked == true))
             {
                 linea1 = 1;
                 string mensaje = "10/" + conectado + "/" + a + "/" + b + "/" + c + "/" + d + "/" + w;
                 byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                 server.Send(msg);
             }
             //Si hago linea 2 enable boton linea
             if ((linea2 == 0) && (num6.Checked == true) && (num7.Checked == true) && (num8.Checked == true) && (num9.Checked == true) && (num10.Checked == true))
             {
                 linea2 = 1;
                 string mensaje = "10/" + conectado + "/" + z + "/" + f + "/" + g + "/" + h + "/" + o;
                 byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                 server.Send(msg);
             }
             //Si hago linea 3 enable boton linea
             if ((linea3 == 0) && (num11.Checked == true) && (num12.Checked == true) && (num13.Checked == true) && (num14.Checked == true) && (num15.Checked == true))
             {
                 linea3 = 1;
                 string mensaje = "10/" + conectado + "/" + k + "/" + j + "/" + l + "/" + m + "/" + n;
                 byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                 server.Send(msg);
             }
         }

         private void dejarButton_Click(object sender, EventArgs e)
         {
             if (dejar == 0)  //Dejar no activo, Salir
             {
                 if (anfitrion == conectado) //Si es el anfitrion
                 {
                     string message = "Eres el anfitrión de esta partida, si sales finalizará el juego para todos.\n\n¿Quieres salir de la partida?";
                     string title = "Dejar partida";
                     MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                     DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Information);
                     if (result == DialogResult.Yes)
                     {
                         nueva = 0;
                         dejar = 1;
                         turno = 0;
                         Q = 0;
                         empezar.Text = "Nueva!";
                         if (Tbolas == 1)  //Lo paramo solo si está activo
                         {
                             bolas.Abort();
                         }
                         string mensaje = "12/" + conectado + "/" + anfitrion + "/" + puntos;
                         byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                         server.Send(msg);

                         aviso.Abort();
                         cuadro.Visible = false;

                         puntuacion.Text = "";
                         
                     }
                 }
                 else //Si no es el anfitrion
                 {
                     dejar = 1;
                     turno = 0;
                     string mensaje = "12/" + conectado + "/" + anfitrion + "/" + puntos;
                     byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                     server.Send(msg);

                     aviso.Abort();
                     cuadro.Visible = false;

                     puntuacion.Text = "";
                     
                 }
             }
             else  //Dejar activo, No sale no está en ninguna
             {
                 DelegadoParaMensajesGeneral delegadoMensajesGeneral = new DelegadoParaMensajesGeneral(PonMensajeGeneral);
                 mensajes.Invoke(delegadoMensajesGeneral, new object[] { "No estás en ninguna partida." });
             }
         }

         private void sound_Click(object sender, EventArgs e)
         {
             if (speaker % 2 == 0)  //activamos sonido
             {
                 speaker++;
                 DelegadoParaMensajesGeneral delegadoMensajesGeneral = new DelegadoParaMensajesGeneral(PonMensajeGeneral);
                 mensajes.Invoke(delegadoMensajesGeneral, new object[] { "Sonido activado." });
             }
             else  //desactivamos sonido
             {
                 speaker++;
                 DelegadoParaMensajesGeneral delegadoMensajesGeneral = new DelegadoParaMensajesGeneral(PonMensajeGeneral);
                 mensajes.Invoke(delegadoMensajesGeneral, new object[] { "Sonido desactivado." });
             }
         }

         private void Ayuda_Click(object sender, EventArgs e)
         {
             Form2 frm = new Form2();
             frm.Show();
         }

         private void Form1_FormClosing(object sender, FormClosingEventArgs e)
         {
             if (conectado != null)
             {
                 atender.Abort();
                 if (Tbolas == 1)  //Lo paramo solo si está activo
                 {
                     bolas.Abort();
                 }
                 // Enviamos al servidor el nombre tecleado
                 byte[] msg = System.Text.Encoding.ASCII.GetBytes("0/" + conectado);
                 server.Send(msg);

                 server.Shutdown(SocketShutdown.Both);
                 server.Close();

                 if (Tcarton == 1)  //Lo paramo solo si está activo
                 {
                     CartonNum.Abort();
                 }
             }
             else if (K % 2 != 0)  //Si estas conectado K es impar
                 atender.Abort();
         }

         private void delete()
         {
             byte[] msg = System.Text.Encoding.ASCII.GetBytes("0/" + conectado);
             server.Send(msg);
             conectado = "";

             DelegadoParaEscribir delegado = new DelegadoParaEscribir(PonNombre);
             label6.Invoke(delegado, new object[] { "No has iniciado sesión" });

             DelegadoLimpiarLista delegadoLimpiarLista = new DelegadoLimpiarLista(LimpiarLista);
             PlayerList.Invoke(delegadoLimpiarLista);
             atender.Abort();
         }

         private void eliminar_Click(object sender, EventArgs e)  //Elimina al usuario activo
         {
             if (conectado == null)  //Si no estas logeado
             {
                 DelegadoParaMensajesGeneral delegadoMensajesGeneral = new DelegadoParaMensajesGeneral(PonMensajeGeneral);
                 mensajes.Invoke(delegadoMensajesGeneral, new object[] { "Para eliminar tu cuenta debes iniciar sesión." });
             }
             else
             {     
                 byte[] msg3 = System.Text.Encoding.ASCII.GetBytes("13/" + conectado);
                 server.Send(msg3);
                 //byte[] msg = System.Text.Encoding.ASCII.GetBytes("0/" + conectado);
                 //server.Send(msg);
             }
         }  
    }
}