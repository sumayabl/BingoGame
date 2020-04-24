#include <string.h>

#include <unistd.h>

#include <stdlib.h>

#include <sys/types.h>

#include <sys/socket.h>

#include <netinet/in.h>

#include <stdio.h>

#include <mysql.h>

#include <pthread.h>



MYSQL *conn;

//Estrucutra necesaria para acceso excluyente

pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;



//CREAR ESTRUCTURAS PARA LA LISTA DE CONECTADOS

typedef struct {

	

	char user [20];

	int socket;

}Conectado;



typedef struct {

	

	int num;

	Conectado conectados[100];

	

}ListaConectados;



ListaConectados lista;



//FUNCION CREAR BBDD

int ConectarMysql()

{

	int err;

	//Creamos una conexion al servidor MYSQL 

	conn = mysql_init(NULL);

	if (conn==NULL) {

		printf ("Error al crear la conexion: %u %s\n", 

				mysql_errno(conn), mysql_error(conn));

		exit (1);

	}

	

	//inicializar la conexion, indicando nuestras claves de acceso

	// al servidor de bases de datos (user,pass)

	conn = mysql_real_connect (conn, "localhost","root", "mysql", NULL, 0, NULL, 0);

	if (conn==NULL)

	{

		printf ("Error al inicializar la conexion: %u %s\n", 

				mysql_errno(conn), mysql_error(conn));

		exit (1);

	}

	

	err=mysql_query(conn,"CREATE DATABASE IF NOT EXISTS juego");

	if (err!=0)

	{

		printf ("Error al crear la base de datos %u %s\n", 

				mysql_errno(conn), mysql_error(conn));

		exit (1);

	}

	

	err=mysql_query(conn,"USE juego;");

	if (err!=0)

	{

		printf ("Error al entrar en la base de datos %u %s\n", 

				mysql_errno(conn), mysql_error(conn));

		exit (1);

	}

	

	err=mysql_query(conn,"CREATE TABLE IF NOT EXISTS jugadores (id INTEGER PRIMARY KEY AUTO_INCREMENT, nombre VARCHAR(25) NOT NULL, pass VARCHAR(25) NOT NULL, ganadas INTEGER, edad INTEGER);");

	if (err!=0)

	{

		printf ("Error al definir la tabla %u %s\n",

				mysql_errno(conn), mysql_error(conn));

		exit (1);

	}

}



//Funcion anadir conectado a la lista (INICIAR SESION)

int PonSocket (ListaConectados *lista, int socket)

{

	if (lista->num == 100) //no se puede aￃﾱadir mￃﾡs usuarios, devuelve -1

		return -1;

	else{

		lista->conectados[lista->num].socket = socket;

		lista->num++;

		return 0;

	}

}



int Asigna (ListaConectados *lista, char nombre[20], int socket)

{

	int i=0;

	int encontrado = 0;

	while ((i < lista->num) && !encontrado)

	{

		if (lista->conectados[i].socket == socket)

			encontrado = 1;

		if (!encontrado)

			i=i+1;

	}

	if (encontrado)

	{

		//Asignamos nombre al num que pertenece

		strcpy (lista->conectados[i].user, nombre);

		return i; //Cuando encuentra la i(posicion), la devuelve

	}

	else

		return -1;

}



//Funcion obtener posicion del usuario en la lista (LOCALIZAR USUARIO)

//Se puede usar la posicion para obtener el socket(lista.conectados[pos].socket)

int DamePosicion (ListaConectados *lista, char nombre[20])

{

	int i=0;

	int encontrado = 0;

	while ((i < lista->num) && !encontrado)

	{

		if (strcmp(lista->conectados[i].user,nombre) == 0)

			encontrado = 1;

		if (!encontrado)

			i=i+1;

	}

	if (encontrado)

		return i; //Cuando encuentra la i(posicion), la devuelve

	else

		return -1;

}



//Funcion eliminar socket

int EliminarSocket (ListaConectados *lista, char nombre[20], int socket)

{

	int pos = DamePosicion (lista, nombre); //Obtenemos pos para eliminar usuario de la lista

	if (pos == -1)

		return -1;

	else

	{

		int i;

		for (i=pos; i < lista->num-1; i++)

		{

			lista->conectados[i].socket = lista->conectados[i+1].socket;

		}

		lista->num--;

		return 0;

	}

}



//Funcion eliminar conectado a la lista (DESCONECTAR)

int Eliminar (ListaConectados *lista, char nombre[20])

{

	int pos = DamePosicion (lista, nombre); //Obtenemos pos para eliminar usuario de la lista

	if (pos == -1)

		return -1;

	else

	{

		int i;

		for (i=pos; i < lista->num-1; i++)

		{

			strcpy(lista->conectados[i].user, lista->conectados[i+1].user);

			lista->conectados[i].socket = lista->conectados[i+1].socket;

		}

		lista->num--;

		return 0;

	}

}



//Funcion para obtener los nombre de todos los conectados separados por /,

//Pero primero pone el numero de conectados(ACTUALIZAR)

void DameConectados (ListaConectados *lista, char conectados[300])

{

	sprintf (conectados, "%d", lista->num);

	int i;

	for (i=0; i< lista->num; i++)

		sprintf (conectados, "%s/%s", conectados, lista->conectados[i].user);

}



int JugarConOtro(char nombre[25], char respondido[512])

{

	char consulta[512];

	sprintf(consulta,"SELECT jugadores.nombre FROM jugadores, partidas, relacion WHERE partidas.id IN (SELECT relacion.id_p FROM relacion WHERE id_j = (SELECT jugadores.id FROM jugadores WHERE jugadores.nombre = '%s')) AND jugadores.id = relacion.id_j AND relacion.id_p = partidas.id AND NOT jugadores.nombre = '%s';", nombre, nombre);

	

	int err;

	err=mysql_query (conn, consulta); 

	if (err!=0) {

		printf ("Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));

		exit (1);

		return -1;

	}

	//Guardamos el resultado de la consulta

	MYSQL_RES *resultado;

	MYSQL_ROW row;

	resultado = mysql_store_result (conn); 

	row = mysql_fetch_row (resultado);

	

	if (row == NULL)

	{

		printf ("No se han obtenido datos en la consulta\n");

		return 0;

	}

	

	else

	{	//El resultado debe ser una matriz con una o varias filas

		//y una columna que contiene el nombre del jugador.

		//PARA CREAR UNA MATRIZ DE VARIAS FILAS USAMOS EL BUCLE WHILE

		while (row !=NULL)

		{

			//La columna 0 contiene el nombre del jugador

			sprintf(respondido,"%s %s,", respondido, row[0]);

			//Obtenemos la siguiente fila

			row = mysql_fetch_row (resultado);

		}

		respondido[strlen(respondido)-1]='\0';

		return 1;

	}

}



int MasPuntuacion(char respondido[512])

{

	char consulta[256];

	sprintf(consulta,"SELECT jugadores.nombre, relacion.id_p FROM jugadores, relacion WHERE relacion.puntuacion = (SELECT MAX(relacion.puntuacion) FROM relacion) AND relacion.id_j = jugadores.id;");

	

	int err;

	err = mysql_query(conn, consulta);

	if (err!=0)  //Si la consulta da error, err es diferente de 0

	{

		printf ("Error al introducir los datos %u %s\n", mysql_errno(conn), mysql_error(conn));

		return -1;

	}

	//Guardamos el resultado de la consulta

	MYSQL_RES *resultado;

	MYSQL_ROW row;

	resultado = mysql_store_result (conn); 

	row = mysql_fetch_row (resultado);

	

	if (row == NULL)

	{

		printf ("No se han obtenido datos en la consulta\n");

		return 0;

	}

	else 

	{   

		strcpy (respondido, row[0]);

		return 1;

	}

}



int MasGanadas(char respondido[512])

{

	char consulta[256];

	sprintf(consulta,"SELECT jugadores.nombre FROM jugadores WHERE ganadas = (SELECT MAX(ganadas) FROM jugadores);");

	

	int err;

	err = mysql_query(conn, consulta);

	if (err!=0)  //Si la consulta da error, err es diferente de 0

	{

		printf ("Error al introducir los datos %u %s\n", mysql_errno(conn), mysql_error(conn));

		return -1;

	}

	//Guardamos el resultado de la consulta

	MYSQL_RES *resultado;

	MYSQL_ROW row;

	resultado = mysql_store_result (conn); 

	row = mysql_fetch_row (resultado);

	

	if (row == NULL)

	{

		printf ("No se han obtenido datos en la consulta\n");

		return 0;

	}

	else 

	{  

		strcpy (respondido, row[0]);

		return 1;

	}

}



int IniciarSesion(char nombre[25], char pass[25], int sock_conn, char respondido[512])

{

	char consulta[256];

	sprintf(consulta,"SELECT pass FROM jugadores WHERE nombre='%s';", nombre);

	

	int err;

	err = mysql_query(conn, consulta);

	

	//Guardamos el resultado de la consulta

	MYSQL_RES *resultado;

	MYSQL_ROW row;

	resultado = mysql_store_result (conn); 

	row = mysql_fetch_row (resultado);

	

	if (err!=0)  //Si la consulta da error, err es diferente de 0

	{

		printf ("Error al introducir los datos %u %s\n", mysql_errno(conn), mysql_error(conn));

		return -1;

	}

	if (row == NULL)

	{

		printf ("No se han obtenido datos en la consulta\n");

		return 0;

	}

	else

	{

		strcpy (respondido, row[0]);

		return 1;

	}

}



int CrearUsuario(char nombre[25], char pass[25])

{

	char consulta[256];

	sprintf(consulta,"INSERT INTO jugadores (nombre, pass, ganadas, edad) VALUES ('%s','%s',NULL,NULL);", nombre, pass);

	

	int err;

	err = mysql_query(conn, consulta);	

	if (err!=0)

	{

		printf ("Error al introducir los datos %u %s\n", mysql_errno(conn), mysql_error(conn));

		return -1;

	}

	else 

		return 1;

}



void *AtenderCliente (void *socket)

{

	int sock_conn;

	int *s;

	s= (int *) socket;

	sock_conn= *s;

	

	//int socket_conn = * (int *) socket;   //QUE ES ESTO, PORQUE ESA COMENTADO

	

	char respondido[512];  //Es la respuesta que envian las funciones

	char peticion[512];

	char respuesta[512];  //Es la respuesta que preparamos para el servidor

	int ret;

	

	//Para lista conectados

	char usuario[20];

	

	ConectarMysql();	

	

	int terminar = 0;

	// Entramos en un bucle para atender todas las peticiones de este cliente

	//hasta que se desconecte

	while (terminar == 0)

	{

		// Ahora recibimos la petici?n

		ret=read(sock_conn,peticion, sizeof(peticion));

		printf ("Recibido\n");

		

		// Tenemos que a?adirle la marca de fin de string 

		// para que no escriba lo que hay despues en el buffer

		peticion[ret]='\0';

		

		printf ("Peticion: %s\n",peticion);

		

		char nombre[20];

		char pass[20];

		char consulta[256];

		

		//Sacamos el codigo

		char *p = strtok( peticion, "/");

		int codigo =  atoi (p);

		printf("Codigo: %d\n", codigo);

		

		if (codigo == 1 || codigo == 2 || codigo == 5)  //Saco el nombre

		{

			p = strtok( NULL, "/");

			strcpy (nombre, p);

			printf ("Nombre: %s\n", nombre);

			

			if (codigo == 1 || codigo == 2)  //Saco la password

			{

				p = strtok( NULL, "/");

				strcpy (pass, p);

				printf ("Password: %s\n", pass);

			}

			

		}

		

		if (codigo ==0) //Acabamos el bucle y desconectamos el servidor

		{

			terminar=1;

			//Llamamos a la funcion eliminar conectado lista

			int res = Eliminar (&lista, nombre);

			if (res == -1)

				printf ("No esta.\n");

			else

				printf ("Eliminado de la lista: %s\n", nombre);

			//return 0;

			int res1 = EliminarSocket (&lista, nombre, socket);

			if (res1 == -1)

				printf ("No esta.\n");

			else

				printf ("Socket eliminado: %d\n", socket);

			

			//Lista conectados

			char notificacion[300];

			char misConectados [300];

			DameConectados (&lista, misConectados);

			printf ("Resultado: %s\n", misConectados);

			

			sprintf (notificacion, "6/%s/", misConectados);

			printf ("Notificacion: %s\n", notificacion);

			//Envio la respuesta al cliente

			int j;

			for(j=0; j<lista.num; j++){

				write (lista.conectados[j].socket, notificacion, strlen(notificacion));

			}

			printf ("Socket eli: %d\n", lista.conectados[0].socket);

			printf ("Socket eli: %d\n", lista.conectados[1].socket);

		}

		else if (codigo == 1)  //Crear usuario

		{

			pthread_mutex_lock( &mutex ); //No me interrumpas ahora

			int ok = CrearUsuario(nombre, pass);  //Llamo a la funcion

			pthread_mutex_unlock( &mutex); //ya puedes interrumpirme

			

			if(ok==1)

			{

				printf("Se ha registrado correctamente\n");

				strcpy (respuesta,"1/SI");

				printf ("%s\n", respuesta);

			}

			else

			   strcpy(respuesta,"1/NO");

			

			//Envio respuesta al cliente

			write (sock_conn,respuesta, strlen(respuesta));

			

			

		}

		else if (codigo == 2)  //Iniciar sesion

		{

			pthread_mutex_lock( &mutex ); //No me interrumpas ahora

			int ok = IniciarSesion(nombre, pass, sock_conn, respondido);  //Llamo a la funcion

			pthread_mutex_unlock( &mutex); //ya puedes interrumpirme

			

			if(ok==1)

			{

				int validar = strcmp(pass, respondido);	

				if(validar == 0)

				{

					strcpy(respuesta,"2/SI");

					//Llamar funcion anadir conectado			

					int res = Asigna (&lista, nombre, sock_conn);

					if (res == -1)

						printf ("Lista llena. No se puede a￱adir.\n");

					else

						printf ("Se ha a￱adido a la lista correctamente.\n");

				}

				

				else

				{

					strcpy(respuesta,"2/NO");

					printf ("%s\n", respuesta);

				}

			}

			else

			   strcpy(respuesta,"2/NO");

			

			//Envio respuesta al cliente

			write (sock_conn, respuesta, strlen(respuesta));

			

			//Lista conectados

			char notificacion[300];

			char misConectados [300];

			DameConectados (&lista, misConectados);

			printf ("Resultado: %s\n", misConectados);

			

			sprintf (notificacion, "6/%s/", misConectados);

			printf ("Notificacion: %s\n", notificacion);

			//Envio la respuesta al cliente

			int j;

			for(j=0; j<lista.num; j++){

				write (lista.conectados[j].socket, notificacion, strlen(notificacion));

			}

			printf ("Socket eli: %d\n", lista.conectados[0].socket);

			printf ("Socket eli: %d\n", lista.conectados[1].socket);



		}

		

		else if (codigo == 3) //Dame jugador que ha ganado mas partidas

		{

			int ok = MasGanadas(respondido);  //Llamo a la funcion

			if (ok==1)

				sprintf(respuesta,"3/SI/%s",respondido);

			else

				strcpy (respuesta,"3/NO");

			//Envio respuesta al cliente

			write (sock_conn, respuesta, strlen(respuesta));

		}

		

		else if (codigo == 4)  //Dame jugador con mas puntos

		{

			int ok = MasPuntuacion(respondido);  //Llamo a la funcion

			if (ok==1)

				sprintf(respuesta,"4/SI/%s",respondido);

			else

				strcpy(respuesta,"4/NO");

			//Envio respuesta al cliente

			write (sock_conn, respuesta, strlen(respuesta));

		}

		else if (codigo == 5) //Codigo 5 Dame jugadores que han jugado con otro jugador

		{

			JugarConOtro(nombre, respondido);  //Llamo a la funcion

			strcpy (respuesta, respondido);

			//Envio respuesta al cliente

			write (sock_conn, respuesta, strlen(respuesta));

			printf ("Enviado al cliente: %s\n", respuesta);

		}

	}

	// Se acabo el servicio para este cliente

	close(sock_conn); 

}



int main(int argc, char *argv[])

{

	

	int sock_conn, sock_listen;

	struct sockaddr_in serv_adr;

	

	// INICIALITZACIONS

	// Obrim el socket

	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)

		printf("Error creant socket");

	

	// Fem el bind al port

	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr

	serv_adr.sin_family = AF_INET;

	

	// asocia el socket a cualquiera de las IP de la maquina. 

	//htonl formatea el numero que recibe al formato necesario

	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);

	// establecemos el puerto de escucha

	serv_adr.sin_port = htons(9060);

	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)

		printf ("Error al bind");

	

	if (listen(sock_listen, 3) < 0)

		printf("Error en el Listen");

	

	pthread_t thread;

	//Como no tenemos que usar el indentificador del trhead para nada

	//quitamos el vector y lo dejamos como una variable

	//cada vez que se conecte un cliente machacara el thread anterior.

	

	//Bucle infinito

	for (;;){

		printf ("Escuchando\n");

		

		sock_conn = accept(sock_listen, NULL, NULL);

		printf ("He recibido conexion\n");

		//sock_conn es el socket que usaremos para este cliente.



		PonSocket (&lista, sock_conn);

		//Llamamos a la funcion PonSocket a la cual le pasamos

		//la lista de conectados y sock_conn.

		

		// Crear thead y decirle lo que tiene que hacer

		pthread_create (&thread, NULL, AtenderCliente,&lista.conectados[lista.num-1].socket);

		

	}

}