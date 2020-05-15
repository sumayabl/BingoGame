namespace ConsoleApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.nombre = new System.Windows.Forms.TextBox();
            this.iniciarSesion = new System.Windows.Forms.Button();
            this.pass = new System.Windows.Forms.TextBox();
            this.registrar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.enviar = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.PlayerList = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.l1 = new System.Windows.Forms.Label();
            this.l2 = new System.Windows.Forms.Label();
            this.l3 = new System.Windows.Forms.Label();
            this.l4 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.UltimosNumeros = new System.Windows.Forms.ListBox();
            this.label12 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.l5 = new System.Windows.Forms.Label();
            this.l6 = new System.Windows.Forms.Label();
            this.l7 = new System.Windows.Forms.Label();
            this.l8 = new System.Windows.Forms.Label();
            this.l9 = new System.Windows.Forms.Label();
            this.l10 = new System.Windows.Forms.Label();
            this.l11 = new System.Windows.Forms.Label();
            this.l12 = new System.Windows.Forms.Label();
            this.l13 = new System.Windows.Forms.Label();
            this.l14 = new System.Windows.Forms.Label();
            this.l15 = new System.Windows.Forms.Label();
            this.bingo = new System.Windows.Forms.Button();
            this.linea = new System.Windows.Forms.Button();
            this.start = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.ListaInvitar = new System.Windows.Forms.ListBox();
            this.AddPlayer = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.text = new System.Windows.Forms.Button();
            this.TextMessage = new System.Windows.Forms.TextBox();
            this.ListText = new System.Windows.Forms.ListBox();
            this.empezar = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.mensajes = new System.Windows.Forms.ListBox();
            this.bola = new System.Windows.Forms.Label();
            this.Ayuda = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // nombre
            // 
            this.nombre.BackColor = System.Drawing.SystemColors.Window;
            this.nombre.Font = new System.Drawing.Font("MV Boli", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nombre.Location = new System.Drawing.Point(30, 104);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(138, 32);
            this.nombre.TabIndex = 0;
            // 
            // iniciarSesion
            // 
            this.iniciarSesion.BackColor = System.Drawing.Color.Red;
            this.iniciarSesion.Font = new System.Drawing.Font("MV Boli", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iniciarSesion.ForeColor = System.Drawing.Color.White;
            this.iniciarSesion.Location = new System.Drawing.Point(45, 207);
            this.iniciarSesion.Name = "iniciarSesion";
            this.iniciarSesion.Size = new System.Drawing.Size(109, 29);
            this.iniciarSesion.TabIndex = 1;
            this.iniciarSesion.Text = "Iniciar Sesión";
            this.iniciarSesion.UseVisualStyleBackColor = false;
            this.iniciarSesion.Click += new System.EventHandler(this.button1_Click);
            // 
            // pass
            // 
            this.pass.Font = new System.Drawing.Font("MV Boli", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pass.Location = new System.Drawing.Point(33, 163);
            this.pass.Name = "pass";
            this.pass.Size = new System.Drawing.Size(135, 32);
            this.pass.TabIndex = 2;
            // 
            // registrar
            // 
            this.registrar.BackColor = System.Drawing.Color.YellowGreen;
            this.registrar.Font = new System.Drawing.Font("MV Boli", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registrar.ForeColor = System.Drawing.Color.White;
            this.registrar.Location = new System.Drawing.Point(45, 242);
            this.registrar.Name = "registrar";
            this.registrar.Size = new System.Drawing.Size(109, 29);
            this.registrar.TabIndex = 3;
            this.registrar.Text = "Crear usuario";
            this.registrar.UseVisualStyleBackColor = false;
            this.registrar.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Gainsboro;
            this.label1.Font = new System.Drawing.Font("MV Boli", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "Nombre";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Gainsboro;
            this.label2.Font = new System.Drawing.Font("MV Boli", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(26, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 21);
            this.label2.TabIndex = 5;
            this.label2.Text = "Contraseña";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Gainsboro;
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(20, 68);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(160, 212);
            this.button3.TabIndex = 6;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("MV Boli", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(183, 46);
            this.label3.TabIndex = 7;
            this.label3.Text = "BINGO90";
            // 
            // enviar
            // 
            this.enviar.BackColor = System.Drawing.Color.White;
            this.enviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.enviar.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enviar.ForeColor = System.Drawing.Color.Maroon;
            this.enviar.Location = new System.Drawing.Point(791, 202);
            this.enviar.Name = "enviar";
            this.enviar.Size = new System.Drawing.Size(110, 28);
            this.enviar.TabIndex = 10;
            this.enviar.Text = "Abrir estadistica";
            this.enviar.UseVisualStyleBackColor = false;
            this.enviar.Click += new System.EventHandler(this.enviar_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.textBox1.Location = new System.Drawing.Point(835, 314);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(66, 22);
            this.textBox1.TabIndex = 14;
            // 
            // PlayerList
            // 
            this.PlayerList.BackColor = System.Drawing.SystemColors.Control;
            this.PlayerList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PlayerList.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.PlayerList.FormattingEnabled = true;
            this.PlayerList.ItemHeight = 17;
            this.PlayerList.Location = new System.Drawing.Point(20, 314);
            this.PlayerList.Margin = new System.Windows.Forms.Padding(2);
            this.PlayerList.Name = "PlayerList";
            this.PlayerList.Size = new System.Drawing.Size(160, 85);
            this.PlayerList.TabIndex = 17;
            this.PlayerList.SelectedIndexChanged += new System.EventHandler(this.PlayerList_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(17, 293);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 19);
            this.label5.TabIndex = 19;
            this.label5.Text = "Conectados";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(225, 293);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(548, 217);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // l1
            // 
            this.l1.AutoSize = true;
            this.l1.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.l1.Location = new System.Drawing.Point(362, 314);
            this.l1.Name = "l1";
            this.l1.Size = new System.Drawing.Size(33, 32);
            this.l1.TabIndex = 26;
            this.l1.Text = "B";
            // 
            // l2
            // 
            this.l2.AutoSize = true;
            this.l2.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.l2.Location = new System.Drawing.Point(418, 314);
            this.l2.Name = "l2";
            this.l2.Size = new System.Drawing.Size(23, 32);
            this.l2.TabIndex = 28;
            this.l2.Text = "I";
            // 
            // l3
            // 
            this.l3.AutoSize = true;
            this.l3.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.l3.Location = new System.Drawing.Point(531, 314);
            this.l3.Name = "l3";
            this.l3.Size = new System.Drawing.Size(34, 32);
            this.l3.TabIndex = 29;
            this.l3.Text = "N";
            // 
            // l4
            // 
            this.l4.AutoSize = true;
            this.l4.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.l4.Location = new System.Drawing.Point(585, 314);
            this.l4.Name = "l4";
            this.l4.Size = new System.Drawing.Size(36, 32);
            this.l4.TabIndex = 30;
            this.l4.Text = "G";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(369, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 18);
            this.label4.TabIndex = 31;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label10.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(823, 293);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 17);
            this.label10.TabIndex = 34;
            this.label10.Text = "gado";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(354, 135);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(0, 18);
            this.label11.TabIndex = 35;
            // 
            // UltimosNumeros
            // 
            this.UltimosNumeros.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UltimosNumeros.FormattingEnabled = true;
            this.UltimosNumeros.ItemHeight = 22;
            this.UltimosNumeros.Location = new System.Drawing.Point(791, 81);
            this.UltimosNumeros.Name = "UltimosNumeros";
            this.UltimosNumeros.Size = new System.Drawing.Size(110, 114);
            this.UltimosNumeros.TabIndex = 36;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(788, 46);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(75, 32);
            this.label12.TabIndex = 37;
            this.label12.Text = "ÚLTIMOS \r\nNÚMEROS";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(12, 538);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(33, 33);
            this.pictureBox2.TabIndex = 38;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.SystemColors.Control;
            this.label13.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(51, 538);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(95, 16);
            this.label13.TabIndex = 39;
            this.label13.Text = "No conectado";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(538, 39);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(224, 197);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 40;
            this.pictureBox3.TabStop = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(365, 352);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 41;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(241, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 16);
            this.label9.TabIndex = 42;
            this.label9.Text = "BOTE:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(357, 25);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 32);
            this.label14.TabIndex = 43;
            this.label14.Text = "05:";
            // 
            // l5
            // 
            this.l5.AutoSize = true;
            this.l5.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.l5.Location = new System.Drawing.Point(304, 375);
            this.l5.Name = "l5";
            this.l5.Size = new System.Drawing.Size(33, 32);
            this.l5.TabIndex = 53;
            this.l5.Text = "B";
            // 
            // l6
            // 
            this.l6.AutoSize = true;
            this.l6.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.l6.Location = new System.Drawing.Point(415, 375);
            this.l6.Name = "l6";
            this.l6.Size = new System.Drawing.Size(23, 32);
            this.l6.TabIndex = 54;
            this.l6.Text = "I";
            // 
            // l7
            // 
            this.l7.AutoSize = true;
            this.l7.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.l7.Location = new System.Drawing.Point(472, 375);
            this.l7.Name = "l7";
            this.l7.Size = new System.Drawing.Size(34, 32);
            this.l7.TabIndex = 55;
            this.l7.Text = "N";
            // 
            // l8
            // 
            this.l8.AutoSize = true;
            this.l8.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.l8.Location = new System.Drawing.Point(642, 375);
            this.l8.Name = "l8";
            this.l8.Size = new System.Drawing.Size(36, 32);
            this.l8.TabIndex = 56;
            this.l8.Text = "G";
            // 
            // l9
            // 
            this.l9.AutoSize = true;
            this.l9.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.l9.Location = new System.Drawing.Point(697, 314);
            this.l9.Name = "l9";
            this.l9.Size = new System.Drawing.Size(36, 32);
            this.l9.TabIndex = 57;
            this.l9.Text = "O";
            // 
            // l10
            // 
            this.l10.AutoSize = true;
            this.l10.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.l10.Location = new System.Drawing.Point(304, 434);
            this.l10.Name = "l10";
            this.l10.Size = new System.Drawing.Size(23, 32);
            this.l10.TabIndex = 58;
            this.l10.Text = "I";
            // 
            // l11
            // 
            this.l11.AutoSize = true;
            this.l11.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.l11.Location = new System.Drawing.Point(249, 434);
            this.l11.Name = "l11";
            this.l11.Size = new System.Drawing.Size(33, 32);
            this.l11.TabIndex = 59;
            this.l11.Text = "B";
            // 
            // l12
            // 
            this.l12.AutoSize = true;
            this.l12.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.l12.Location = new System.Drawing.Point(472, 434);
            this.l12.Name = "l12";
            this.l12.Size = new System.Drawing.Size(34, 32);
            this.l12.TabIndex = 60;
            this.l12.Text = "N";
            // 
            // l13
            // 
            this.l13.AutoSize = true;
            this.l13.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.l13.Location = new System.Drawing.Point(585, 434);
            this.l13.Name = "l13";
            this.l13.Size = new System.Drawing.Size(36, 32);
            this.l13.TabIndex = 61;
            this.l13.Text = "G";
            // 
            // l14
            // 
            this.l14.AutoSize = true;
            this.l14.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.l14.Location = new System.Drawing.Point(642, 434);
            this.l14.Name = "l14";
            this.l14.Size = new System.Drawing.Size(36, 32);
            this.l14.TabIndex = 62;
            this.l14.Text = "O";
            // 
            // l15
            // 
            this.l15.AutoSize = true;
            this.l15.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.l15.Location = new System.Drawing.Point(697, 375);
            this.l15.Name = "l15";
            this.l15.Size = new System.Drawing.Size(36, 32);
            this.l15.TabIndex = 63;
            this.l15.Text = "O";
            // 
            // bingo
            // 
            this.bingo.BackColor = System.Drawing.Color.Red;
            this.bingo.Font = new System.Drawing.Font("MV Boli", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bingo.ForeColor = System.Drawing.Color.White;
            this.bingo.Location = new System.Drawing.Point(790, 444);
            this.bingo.Name = "bingo";
            this.bingo.Size = new System.Drawing.Size(109, 66);
            this.bingo.TabIndex = 64;
            this.bingo.Text = "Bingo!";
            this.bingo.UseVisualStyleBackColor = false;
            // 
            // linea
            // 
            this.linea.BackColor = System.Drawing.Color.YellowGreen;
            this.linea.Font = new System.Drawing.Font("MV Boli", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linea.ForeColor = System.Drawing.Color.White;
            this.linea.Location = new System.Drawing.Point(790, 400);
            this.linea.Name = "linea";
            this.linea.Size = new System.Drawing.Size(109, 38);
            this.linea.TabIndex = 65;
            this.linea.Text = "Linea";
            this.linea.UseVisualStyleBackColor = false;
            // 
            // start
            // 
            this.start.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.start.Location = new System.Drawing.Point(806, 342);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(102, 24);
            this.start.TabIndex = 66;
            this.start.Text = "timer (dis)";
            this.start.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(398, 25);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(45, 32);
            this.label15.TabIndex = 67;
            this.label15.Text = "00";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(247, 25);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(60, 32);
            this.label21.TabIndex = 68;
            this.label21.Text = "200";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Red;
            this.label22.Location = new System.Drawing.Point(348, 9);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(62, 16);
            this.label22.TabIndex = 69;
            this.label22.Text = "TIEMPO:";
            // 
            // ListaInvitar
            // 
            this.ListaInvitar.BackColor = System.Drawing.SystemColors.Control;
            this.ListaInvitar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListaInvitar.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListaInvitar.ForeColor = System.Drawing.Color.YellowGreen;
            this.ListaInvitar.FormattingEnabled = true;
            this.ListaInvitar.ItemHeight = 17;
            this.ListaInvitar.Location = new System.Drawing.Point(20, 424);
            this.ListaInvitar.Margin = new System.Windows.Forms.Padding(2);
            this.ListaInvitar.Name = "ListaInvitar";
            this.ListaInvitar.Size = new System.Drawing.Size(160, 51);
            this.ListaInvitar.TabIndex = 72;
            this.ListaInvitar.Tag = "";
            // 
            // AddPlayer
            // 
            this.AddPlayer.BackColor = System.Drawing.Color.YellowGreen;
            this.AddPlayer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddPlayer.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddPlayer.ForeColor = System.Drawing.Color.White;
            this.AddPlayer.Location = new System.Drawing.Point(185, 329);
            this.AddPlayer.Name = "AddPlayer";
            this.AddPlayer.Size = new System.Drawing.Size(27, 55);
            this.AddPlayer.TabIndex = 73;
            this.AddPlayer.Text = "+";
            this.AddPlayer.UseVisualStyleBackColor = false;
            this.AddPlayer.Click += new System.EventHandler(this.AddPlayer_Click);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.YellowGreen;
            this.label7.Location = new System.Drawing.Point(17, 403);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(137, 19);
            this.label7.TabIndex = 75;
            this.label7.Text = "Ivitados";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // text
            // 
            this.text.BackColor = System.Drawing.Color.Red;
            this.text.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.text.Font = new System.Drawing.Font("MV Boli", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text.ForeColor = System.Drawing.Color.White;
            this.text.Location = new System.Drawing.Point(433, 196);
            this.text.Name = "text";
            this.text.Size = new System.Drawing.Size(75, 34);
            this.text.TabIndex = 77;
            this.text.Text = "Enviar";
            this.text.UseVisualStyleBackColor = false;
            this.text.Click += new System.EventHandler(this.text_Click);
            // 
            // TextMessage
            // 
            this.TextMessage.Location = new System.Drawing.Point(225, 196);
            this.TextMessage.Multiline = true;
            this.TextMessage.Name = "TextMessage";
            this.TextMessage.Size = new System.Drawing.Size(202, 34);
            this.TextMessage.TabIndex = 78;
            // 
            // ListText
            // 
            this.ListText.BackColor = System.Drawing.SystemColors.Control;
            this.ListText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListText.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListText.ForeColor = System.Drawing.Color.Red;
            this.ListText.FormattingEnabled = true;
            this.ListText.ItemHeight = 17;
            this.ListText.Location = new System.Drawing.Point(225, 85);
            this.ListText.Margin = new System.Windows.Forms.Padding(2);
            this.ListText.Name = "ListText";
            this.ListText.Size = new System.Drawing.Size(283, 102);
            this.ListText.TabIndex = 79;
            // 
            // empezar
            // 
            this.empezar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.empezar.Location = new System.Drawing.Point(225, 247);
            this.empezar.Name = "empezar";
            this.empezar.Size = new System.Drawing.Size(94, 40);
            this.empezar.TabIndex = 80;
            this.empezar.Text = "Empezar!";
            this.empezar.UseVisualStyleBackColor = true;
            this.empezar.Click += new System.EventHandler(this.empezar_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(222, 68);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 16);
            this.label8.TabIndex = 81;
            this.label8.Text = "Chat";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Enabled = false;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(0, 525);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(926, 51);
            this.button1.TabIndex = 82;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(51, 555);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 16);
            this.label6.TabIndex = 83;
            this.label6.Text = "No has iniciado sesión";
            // 
            // mensajes
            // 
            this.mensajes.BackColor = System.Drawing.SystemColors.Control;
            this.mensajes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mensajes.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mensajes.ForeColor = System.Drawing.Color.Gray;
            this.mensajes.FormattingEnabled = true;
            this.mensajes.ItemHeight = 16;
            this.mensajes.Location = new System.Drawing.Point(310, 541);
            this.mensajes.Margin = new System.Windows.Forms.Padding(2);
            this.mensajes.Name = "mensajes";
            this.mensajes.Size = new System.Drawing.Size(311, 16);
            this.mensajes.TabIndex = 84;
            this.mensajes.Tag = "";
            // 
            // bola
            // 
            this.bola.AutoSize = true;
            this.bola.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.bola.Font = new System.Drawing.Font("Arial", 62.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bola.ForeColor = System.Drawing.Color.Transparent;
            this.bola.Location = new System.Drawing.Point(587, 95);
            this.bola.Name = "bola";
            this.bola.Size = new System.Drawing.Size(132, 96);
            this.bola.TabIndex = 85;
            this.bola.Text = "90";
            this.bola.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Ayuda
            // 
            this.Ayuda.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ayuda.Location = new System.Drawing.Point(806, 548);
            this.Ayuda.Name = "Ayuda";
            this.Ayuda.Size = new System.Drawing.Size(94, 23);
            this.Ayuda.TabIndex = 86;
            this.Ayuda.Text = "¿Cómo jugar?";
            this.Ayuda.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(920, 578);
            this.Controls.Add(this.Ayuda);
            this.Controls.Add(this.bola);
            this.Controls.Add(this.mensajes);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.empezar);
            this.Controls.Add(this.ListText);
            this.Controls.Add(this.TextMessage);
            this.Controls.Add(this.text);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.AddPlayer);
            this.Controls.Add(this.ListaInvitar);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.start);
            this.Controls.Add(this.linea);
            this.Controls.Add(this.bingo);
            this.Controls.Add(this.l15);
            this.Controls.Add(this.l14);
            this.Controls.Add(this.l13);
            this.Controls.Add(this.l12);
            this.Controls.Add(this.l11);
            this.Controls.Add(this.l10);
            this.Controls.Add(this.l9);
            this.Controls.Add(this.l8);
            this.Controls.Add(this.l7);
            this.Controls.Add(this.l6);
            this.Controls.Add(this.l5);
            this.Controls.Add(this.enviar);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.UltimosNumeros);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.l4);
            this.Controls.Add(this.l3);
            this.Controls.Add(this.l2);
            this.Controls.Add(this.l1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.PlayerList);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.registrar);
            this.Controls.Add(this.pass);
            this.Controls.Add(this.iniciarSesion);
            this.Controls.Add(this.nombre);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nombre;
        private System.Windows.Forms.Button iniciarSesion;
        private System.Windows.Forms.TextBox pass;
        private System.Windows.Forms.Button registrar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button enviar;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListBox PlayerList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label l1;
        private System.Windows.Forms.Label l2;
        private System.Windows.Forms.Label l3;
        private System.Windows.Forms.Label l4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListBox UltimosNumeros;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label l5;
        private System.Windows.Forms.Label l6;
        private System.Windows.Forms.Label l7;
        private System.Windows.Forms.Label l8;
        private System.Windows.Forms.Label l9;
        private System.Windows.Forms.Label l10;
        private System.Windows.Forms.Label l11;
        private System.Windows.Forms.Label l12;
        private System.Windows.Forms.Label l13;
        private System.Windows.Forms.Label l14;
        private System.Windows.Forms.Label l15;
        private System.Windows.Forms.Button bingo;
        private System.Windows.Forms.Button linea;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ListBox ListaInvitar;
        private System.Windows.Forms.Button AddPlayer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button text;
        private System.Windows.Forms.TextBox TextMessage;
        private System.Windows.Forms.ListBox ListText;
        private System.Windows.Forms.Button empezar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox mensajes;
        private System.Windows.Forms.Label bola;
        private System.Windows.Forms.Button Ayuda;
    }
}