#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <pthread.h>
#include <mysql.h>

MYSQL *conn;
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;
int err;

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

void *AtenderCliente (void *socket)
{
	int sock_conn;
	int *s;
	s= (int *) socket;
	sock_conn= *s;
	
	//int socket_conn = * (int *) socket;
	
	char peticion[512];
	char respuesta[512];
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
		
		// vamos a ver que quieren
		char *p = strtok( peticion, "/");
		int codigo =  atoi (p);
		
		printf("Codigo: %d\n", codigo);
		
		if (codigo == 1 || codigo == 2)
		{
		p = strtok( NULL, "/");
		strcpy (nombre, p);
		
		p = strtok( NULL, "/");
		strcpy (pass, p);
		
		printf ("Codigo: %d, Nombre: %s, Password: %s\n", codigo, nombre, pass);
		}
		
		if (codigo ==0) //piden la longitd del nombre
		{
			terminar=1;
		}
		else if (codigo == 1)  //Crear usuario
		{
			sprintf(consulta,"INSERT INTO jugadores (nombre, pass, ganadas, edad) VALUES ('%s','%s',NULL,NULL);", nombre, pass);
			
			err = mysql_query(conn, consulta);
			if (err!=0)
			{
				printf ("Error al introducir los datos %u %s\n", mysql_errno(conn), mysql_error(conn));
				strcpy(respuesta,"1/NO");
				printf ("%s\n", respuesta);
				//Y lo enviamos
				write (sock_conn, respuesta, strlen(respuesta));
			}
			else 
			{
				printf("Se ha registrado correctamente\n");
				strcpy (respuesta,"1/SI");
				printf ("%s\n", respuesta);
				//Y lo enviamos
				write (sock_conn,respuesta, strlen(respuesta));
			}
		}
		else if (codigo == 2)  //Iniciar sesion
		{
			sprintf(consulta,"SELECT pass FROM jugadores WHERE nombre='%s';", nombre);
			
			err = mysql_query(conn, consulta);
			if (err!=0)  //Si la consulta da error, err es diferente de 0
			{
				printf ("Error al introducir los datos %u %s\n", mysql_errno(conn), mysql_error(conn));
				strcpy(respuesta,"2/NO");
				printf ("%s\n", respuesta);
			}
			//Guardamos el resultado de la consulta
			MYSQL_RES *resultado;
			MYSQL_ROW row;
			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			
			if (row == NULL)
			{
				printf ("No se han obtenido datos en la consulta\n");
				strcpy (respuesta,"2/NO");
				
				printf ("%s\n",respuesta);
			}
			else 
			{
				printf("%s\n",row[0]);
				
				int validar = strcmp(pass, row[0]);
				
				if(validar == 0)
				{
					strcpy(respuesta,"2/SI/");
					
					//LISTA CONECTADOS
					strcpy(lista.conectados[lista.num].user,nombre);
					lista.conectados[lista.num].socket = sock_conn;
					lista.num++;
					
				}
				
				else
					strcpy(respuesta,"2/NO");
				
				printf ("%s\n", respuesta);
			}
			write (sock_conn, respuesta, strlen(respuesta));
		}
		
		else if (codigo == 3) //Dame jugador que ha ganado mas partidas
		{
			sprintf(consulta,"SELECT jugadores.nombre FROM jugadores WHERE ganadas = (SELECT MAX(ganadas) FROM jugadores);");
			
			err = mysql_query(conn, consulta);
			if (err!=0)  //Si la consulta da error, err es diferente de 0
			{
				printf ("Error al introducir los datos %u %s\n", mysql_errno(conn), mysql_error(conn));
				strcpy(respuesta,"3/NO");
				printf ("%s\n", respuesta);
			}
			//Guardamos el resultado de la consulta
			MYSQL_RES *resultado;
			MYSQL_ROW row;
			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			
			if (row == NULL)
			{
				printf ("No se han obtenido datos en la consulta\n");
				strcpy (respuesta,"3/NO");
				
				printf ("%s\n",respuesta);
			}
			else 
			{  
				sprintf(respuesta,"3/SI/%s",row[0]);
				write (sock_conn, respuesta, strlen(respuesta));
				printf ("%s\n", respuesta);
			}
			
		}

		else if (codigo == 4)  //Dame jugador con m￡s puntos
		{
			sprintf(consulta,"SELECT jugadores.nombre, relacion.id_p FROM jugadores, relacion WHERE relacion.puntuacion = (SELECT MAX(relacion.puntuacion) FROM relacion) AND relacion.id_j = jugadores.id;");
			
			err = mysql_query(conn, consulta);
			if (err!=0)  //Si la consulta da error, err es diferente de 0
			{
				printf ("Error al introducir los datos %u %s\n", mysql_errno(conn), mysql_error(conn));
				strcpy(respuesta,"4/NO");
				printf ("%s\n", respuesta);
			}
			//Guardamos el resultado de la consulta
			MYSQL_RES *resultado;
			MYSQL_ROW row;
			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			
			if (row == NULL)
			{
				printf ("No se han obtenido datos en la consulta\n");
				strcpy (respuesta,"4/NO");
				
				printf ("%s\n",respuesta);
			}
			else 
			{   
				sprintf(respuesta,"4/SI/%s",row[0]);
				write (sock_conn, respuesta, strlen(respuesta));
				printf ("%s\n", respuesta);
			}
		}
		else if (codigo == 5) //Codigo 5 Dame jugadores que han jugado con otro jugador
		{
			p = strtok( NULL, "/");
			strcpy (nombre, p);
			sprintf(consulta,"SELECT jugadores.nombre FROM jugadores, partidas, relacion WHERE partidas.id IN (SELECT relacion.id_p FROM relacion WHERE id_j = (SELECT jugadores.id FROM jugadores WHERE jugadores.nombre = '%s')) AND jugadores.id = relacion.id_j AND relacion.id_p = partidas.id AND NOT jugadores.nombre = '%s';", nombre, nombre);
			
			err=mysql_query (conn, consulta); 
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			//Guardamos el resultado de la consulta
			MYSQL_RES *resultado;
			MYSQL_ROW row;
			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			
			if (row == NULL)
				printf ("No se han obtenido datos en la consulta\n");
			
			else
			{	//El resultado debe ser una matriz con una o varias filas
				//y una columna que contiene el nombre del jugador.
				//PARA CREAR UNA MATRIZ DE VARIAS FILAS USAMOS EL BUCLE WHILE
				//strcpy (respuesta, row[0]);
				strcpy (respuesta,"");
				while (row !=NULL)
				{
					//La columna 0 contiene el nombre del jugador
					sprintf(respuesta,"%s %s,", respuesta, row[0]);
					//Obtenemos la siguiente fila
					row = mysql_fetch_row (resultado);
				}
				//strcpy (respuesta,"5");
				respuesta[strlen(respuesta)-1]='\0';
				write (sock_conn, respuesta, strlen(respuesta));
				printf ("Enviado al cliente: %s\n", respuesta);
			}
		}
		else if (codigo == 6)  //Lista conectados
		{
			char num[12];
			sprintf(num, "%d",lista.num);
			
			strcpy(respuesta,num);
			strcat(respuesta,"/");
			
			int d=0;
			while (d<lista.num)
			{
				strcat(respuesta,lista.conectados[d].user);
				strcat(respuesta,"/");
				d++;
			}
			write (sock_conn, respuesta, strlen(respuesta));
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
	
	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	// establecemos el puerto de escucha
	serv_adr.sin_port = htons(9080);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");
	
	int i;
	int sockets[100];
	pthread_t thread[100];
	i=0;
	// Bucle para atender a 5 clientes
	for (;;){
		printf ("Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		
		sockets[i] =sock_conn;
		//sock_conn es el socket que usaremos para este cliente
		
		// Crear thead y decirle lo que tiene que hacer
		pthread_create (&thread[i], NULL, AtenderCliente,&sockets[i]);
		i=i+1;
		
	}
	
	//for (i=0; i<5; i++)
	//pthread_join (thread[i], NULL);
}
