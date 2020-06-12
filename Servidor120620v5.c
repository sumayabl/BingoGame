#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>
#include <pthread.h>
#include <time.h>
//#include <my_global.h>


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

//CREAR ESTRUCTURAS PARA LA LISTA DE INVITADOS
typedef struct {
	int id_partida;
	char user [20];	
}Invitado;


typedef struct {	
	int num;	
	Invitado invitados[100];			
}ListaInvitados;


ListaInvitados listainv;

//CREAR TABLA PARTIDAS
typedef struct{
	int id; //-1 libre y 0 ocupado
	int id_partida;
	int bote;
	char jugador[20];
	int sock;
}TPartida;

typedef TPartida Ttabla [100];

Ttabla t;
TPartida e;

//CREAR TABLA BOLAS
typedef struct{
	int id;
	int bola;
	int id_partida;	
}TBola;

typedef TBola TtablaB[100];			

TtablaB b;
TBola o;

int numPartida = 0;
//int res2;
int idP;
int numP;
int bote;
//char jugadores[512];
char invitadoslist[300];

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
	conn = mysql_real_connect (conn, "shiva2.upc.es","root", "mysql", NULL, 0, NULL, 0);  //shiva2.upc.es
	if (conn==NULL)
	{
		printf ("Error al inicializar la conexion: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	err=mysql_query(conn,"CREATE DATABASE IF NOT EXISTS TG4");
	if (err!=0)
	{
		printf ("Error al crear la base de datos %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	err=mysql_query(conn,"USE TG4;");
	if (err!=0)
	{
		printf ("Error al entrar en la base de datos %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	err=mysql_query(conn,"CREATE TABLE IF NOT EXISTS jugadores (id INTEGER PRIMARY KEY AUTO_INCREMENT, nombre VARCHAR(25) NOT NULL, pass VARCHAR(25) NOT NULL, bingos INTEGER, lineas INTEGER);");
	if (err!=0)
	{
		printf ("Error al definir la tabla %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	err=mysql_query(conn,"CREATE TABLE IF NOT EXISTS partidas (id INTEGER PRIMARY KEY, jugadores VARCHAR(512) NOT NULL, bote INTEGER, diahora DATETIME);");
	if (err!=0)
	{
		printf ("Error al definir la tabla %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	err=mysql_query(conn,"CREATE TABLE IF NOT EXISTS relacion (id_j INTEGER, id_p INTEGER, puntos INTEGER, FOREIGN KEY (id_j) REFERENCES jugadores(id) ON DELETE CASCADE, FOREIGN KEY (id_p) REFERENCES partidas(id));");
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
	if (lista->num == 100) //no se puede a￯﾿ﾯ￯ﾾ﾿￯ﾾﾯ￯﾿ﾯ￯ﾾﾾ￯ﾾ﾿￯﾿ﾯ￯ﾾﾾ￯ﾾﾃ￯﾿ﾯ￯ﾾ﾿￯ﾾﾯ￯﾿ﾯ￯ﾾﾾ￯ﾾﾾ￯﾿ﾯ￯ﾾﾾ￯ﾾﾱadir m￯﾿ﾯ￯ﾾ﾿￯ﾾﾯ￯﾿ﾯ￯ﾾﾾ￯ﾾ﾿￯﾿ﾯ￯ﾾﾾ￯ﾾﾃ￯﾿ﾯ￯ﾾ﾿￯ﾾﾯ￯﾿ﾯ￯ﾾﾾ￯ﾾﾾ￯﾿ﾯ￯ﾾﾾ￯ﾾﾡs usuarios, devuelve -1		
		return -1;	
	else
	{		
		lista->conectados[lista->num].socket = socket;
		lista->num++;		
		return 0;
	}	
}

int PonInvitado (ListaInvitados *listainv, int id_partida, char nombre[25])
{	
	if (listainv->num == 100) //no se puede a￯﾿ﾯ￯ﾾ﾿￯ﾾﾯ￯﾿ﾯ￯ﾾﾾ￯ﾾ﾿￯﾿ﾯ￯ﾾﾾ￯ﾾﾃ￯﾿ﾯ￯ﾾ﾿￯ﾾﾯ￯﾿ﾯ￯ﾾﾾ￯ﾾﾾ￯﾿ﾯ￯ﾾﾾ￯ﾾﾱadir m￯﾿ﾯ￯ﾾ﾿￯ﾾﾯ￯﾿ﾯ￯ﾾﾾ￯ﾾ﾿￯﾿ﾯ￯ﾾﾾ￯ﾾﾃ￯﾿ﾯ￯ﾾ﾿￯ﾾﾯ￯﾿ﾯ￯ﾾﾾ￯ﾾﾾ￯﾿ﾯ￯ﾾﾾ￯ﾾﾡs usuarios, devuelve -1		
		return -1;	
	else
	{		
		listainv->invitados[listainv->num].id_partida = id_partida;
		strcpy (listainv->invitados[listainv->num].user, nombre);
		listainv->num++;		
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

int DamePosicionPartida (Ttabla t, char nombre[20])	
{	
	int i=0;	
	int encontrado = 0;
	while ((i < 100) && !encontrado)		
	{	
		//printf("Jugador: %s\n",t[i].jugador);
		if (strcmp(t[i].jugador,nombre) == 0)			
			encontrado = 1;
		
		if (!encontrado)			
			i=i+1;		
	}	
	if (encontrado)		
		return i; //Cuando encuentra la i(posicion), la devuelve	
	else		
		return -1;	
}

//Funcion eliminar partida de la tabla
int EliminarPartida (Ttabla t, char nombre[20])	
{	
	int pos = DamePosicionPartida(t, nombre); //Obtenemos pos para eliminar usuario de la lista		
	if (pos == -1)		
		return -1;	
	else		
	{		
		t[pos].id=-1;
		t[pos].id_partida=-1;
		t[pos].bote = 100;
		printf("Partida eliminada: %d\n",t[pos].id_partida);
		//printf("2222 nombre eliminado de la partida: %s\n",t[pos].jugador);
		return 0;		
	}	
}

void DameInvitados (ListaInvitados *listainv, int idP, char invitadoslist[300])	
{	
	int cont=0;
	for (int i=0; i< listainv->num; i++)
		if(listainv->invitados[i].id_partida==idP)
		cont++;
	
	sprintf (invitadoslist, "%d", cont);
	
	int i;	
	for (i=0; i< listainv->num; i++)
		if(listainv->invitados[i].id_partida==idP)
		sprintf (invitadoslist, "%s/%s", invitadoslist, listainv->invitados[i].user);	
}

//Se puede usar la posicion para obtener el socket(lista.conectados[pos].socket)
int DamePosicionInvitado (ListaInvitados *listainv, char nombre[20])	
{	
	int i=0;	
	int encontrado = 0;
	
	while ((i < listainv->num) && !encontrado)		
	{		
		if (strcmp(listainv->invitados[i].user,nombre) == 0)			
			encontrado = 1;
		
		if (!encontrado)			
			i=i+1;		
	}	
	if (encontrado)		
		return i; //Cuando encuentra la i(posicion), la devuelve	
	else		
		return -1;	
}

//Funcion eliminar conectado a la lista (DESCONECTAR)
int EliminaInvitado (ListaInvitados *listainv, char nombre[20])	
{	
	int pos = DamePosicionInvitado (listainv, nombre); //Obtenemos pos para eliminar usuario de la lista	
	
	if (pos == -1)		
		return -1;	
	else		
	{		
		int i;		
		for (i=pos; i < listainv->num-1; i++)			
		{
			strcpy(listainv->invitados[i].user, listainv->invitados[i+1].user);			
			listainv->invitados[i].id_partida = listainv->invitados[i+1].id_partida;			
		}		
		listainv->num--;		
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
	sprintf(consulta,"SELECT DISTINCT jugadores.nombre FROM jugadores, partidas, relacion WHERE partidas.id IN (SELECT relacion.id_p FROM relacion WHERE id_j = (SELECT jugadores.id FROM jugadores WHERE jugadores.nombre = '%s')) AND jugadores.id = relacion.id_j AND relacion.id_p = partidas.id AND NOT jugadores.nombre = '%s';", nombre, nombre);
	
	int err;	
	err=mysql_query (conn, consulta); 	
	if (err!=0)
	{		
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
		respondido[0]='\0';
		while (row !=NULL)
		{
			//La columna 0 contiene el nombre del jugador
			sprintf(respondido,"%s%s,", respondido, row[0]);
			//Obtenemos la siguiente fila
			row = mysql_fetch_row (resultado);
		}		
		respondido[strlen(respondido)-1]='\0';
		respondido[strlen(respondido)-1]='/';
		return 1;
	}	
}

int MasPuntuacion(char respondido[512])	
{
	char consulta[256];
	sprintf(consulta,"SELECT jugadores.nombre, relacion.id_p FROM jugadores, relacion WHERE relacion.puntos = (SELECT MAX(relacion.puntos) FROM relacion) AND relacion.id_j = jugadores.id;");
	
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

int MasGanadas(char respondido[512], char nombre[25])	
{	
	char consulta[256];	
	sprintf(consulta,"SELECT DISTINCT id_p FROM relacion WHERE id_j = (SELECT id FROM jugadores WHERE nombre='%s');",nombre);	
	
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
		respondido[0]='\0';
		while (row !=NULL)
		{
			//La columna 0 contiene el nombre del jugador
			sprintf(respondido,"%s%s,", respondido, row[0]);
			printf("2222 Respondido: %s\n",respondido);
			//Obtenemos la siguiente fila
			row = mysql_fetch_row (resultado);
		}		
		respondido[strlen(respondido)-1]='\0';
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
	
	sprintf(consulta,"SELECT nombre FROM jugadores WHERE nombre='%s'",nombre);
	int err;	
	err = mysql_query(conn, consulta);		
	if (err!=0)		
	{		
		printf ("Error al introducir los datos %u %s\n", mysql_errno(conn), mysql_error(conn));
		return -1;		
	}
	else  //Comprueba si el nombre est￯﾿ﾡ usado 	
	{
		MYSQL_RES *resultado;  //Guardamos el resultado de la consulta
		MYSQL_ROW row;	
		resultado = mysql_store_result (conn); 	
		row = mysql_fetch_row (resultado);
		
		if (row == NULL)  //No hay ningun nombre igual
		{
			sprintf(consulta,"INSERT INTO jugadores (nombre, pass, bingos, lineas) VALUES ('%s','%s',NULL,NULL);", nombre, pass);
			err;	
			err = mysql_query(conn, consulta);		
			if (err!=0)		
			{		
				printf ("Error al introducir los datos %u %s\n", mysql_errno(conn), mysql_error(conn));
				return -1;		
			}	
			else 		
				return 1;
		}	
		else  //Hay otro nombre igual
		{
			return 0;
		}	
	}
	
	sprintf(consulta,"INSERT INTO jugadores (nombre, pass, bingos, lineas) VALUES ('%s','%s',NULL,NULL);", nombre, pass);
	err;	
	err = mysql_query(conn, consulta);		
	if (err!=0)		
	{		
		printf ("Error al introducir los datos %u %s\n", mysql_errno(conn), mysql_error(conn));
		return -1;		
	}	
	else 		
		return 1;	
}

//Inicializar tabla partidas
void inicializa (Ttabla t)
{
	int i;
	for ( i=0; i<100; i++)
	{
		t[i].id = -1;
		t[i].bote = 100;
	}
}

//Funcion anadir jugador a la tabla partidas (ACEPTAR INVITACION)
int PonElemento (Ttabla t, char nombre[20], int numP, int socket)	
{	
	int i=0;	
	int encontrado = 0;
	
	while ((i < 100) && !encontrado)		
	{		
		if (t[i].id == -1)
			encontrado = 1;
		
		if (!encontrado)
			i=i+1;
	}	
	if (encontrado)
	{
		t[i].sock = socket;
		strcpy(t[i].jugador, nombre);
		t[i].id = 0;
		t[i].id_partida = numP;
		for(int j=0; j<100; j++)
		{
			if (t[j].id_partida == numP)
				t[j].bote = t[j].bote + 10;
		}
		return 0;
	}	
	else
		return -1;
}

int UltimaPartida()
{
	char consulta[256];
	sprintf(consulta,"SELECT MAX(id) FROM partidas;");
	int err;	
	err = mysql_query(conn, consulta);		
	if (err!=0)		
	{		
		printf ("Error al introducir los datos %u %s\n", mysql_errno(conn), mysql_error(conn));
		return -1;		
	}
	else  //Comprueba si el nombre est￯﾿ﾡ usado 	
	{
		MYSQL_RES *resultado;  //Guardamos el resultado de la consulta
		MYSQL_ROW row;	
		resultado = mysql_store_result (conn); 	
		row = mysql_fetch_row (resultado);

		int ultima = atoi(row[0]);
		return ultima;
	}
}

int NuevaPartida ()
{
	numPartida = UltimaPartida();
	numPartida = numPartida+1;
	return numPartida;
}

//Dame identificador partida
int DameidP (Ttabla t, char nombre[20])	
{	
	int i=0;	
	int encontrado = 0;
	
	while ((i < 100) && !encontrado)		
	{		
		if (strcmp(t[i].jugador,nombre) == 0)			
			encontrado = 1;
		
		if (!encontrado)			
			i=i+1;		
	}	
	if (encontrado)		
		return t[i].id_partida; //Cuando encuentra la i(posicion), la devuelve	
	else		
		return -1;	
}

int CompruebaNumero (TtablaB b, int id_partida, int num)	
{	
	int i=0;	
	int encontrado = 0;
	
	while ((i < 100) && (encontrado==0))		
	{	
		if((b[i].bola==num)&&(b[i].id_partida=id_partida))
		{
			encontrado=1;
			//printf("Posicion: %d\n",i);
		}
		
		else	
		{
			i=i+1;
			//printf("Suma y sigue\n");
		}
	}	
	if (encontrado==1)		
		return 0;	
	else		
		return -1;	
}

//Inicializar tabla bolas
void inicializaBola (TtablaB b)
{
	int i;
	for ( i=0; i<100; i++)
		b[i].id = -1;
}

//Dame bola aleatoria
int DameBola()
{
	int bola =  rand()%31;
	int c = 0;
	
	int i;
	for (i=0; i<100; i++)
		if (b[i].id != -1) 
			if (bola == b[i].bola)
				c ++;
	
	if (c ==0)
		return bola;
	else
		return -1;
}

//Funcion anadir jugador a la tabla partidas (ACEPTAR INVITACION)

int PonBola (TtablaB b, int bola, int numP)	
{	
	int i=0;	
	int encontrado = 0;
	
	while ((i < 100) && !encontrado)		
	{		
		if (b[i].id == -1)
			encontrado = 1;
		
		if (!encontrado)
			i=i+1;
	}	
	if (encontrado)
	{
		b[i].bola =  bola;
		b[i].id_partida = numP;
		b[i].id = 0;
		return 0;
	}	
	else
		return -1;
}


void DameFechaHora(char fechayhora[100])
{
	time_t c;
	struct tm *tm;
	
	c=time(NULL);
	tm=localtime(&c);
	strftime(fechayhora, 100, "%y/%m/%d %H:%M:%S", tm);
}

int DameBote(Ttabla t, char nombre[25])
{
	int pos = DamePosicionPartida (t, nombre);
	printf("Bote: %d Posicion tabla: %d\n",t[pos].bote,pos);
	return t[pos].bote;
}

int GuardarPartida(Ttabla t, int numP, char nombre[20])	
{	
	char consulta[256];	
	char nombreJ[512];
	char nombreJ2[512];
	nombreJ[0] = '\0';
	int err;
	int id_JugadorD;
	char id_JugadorS;
	//printf("3333 numP: %d\n",numP);
	//printf("5555 %s\n",nombreJ);
	//Busca jugadores
	int i;
	for(i=0;i<100;i++)
	{
		if(t[i].id_partida==numP)
			sprintf(nombreJ,"%s%s/",nombreJ,t[i].jugador);
		//printf("4444 %s\n",t[i].jugador);}
	}	
	//Quito coma final
	strcpy(nombreJ2,nombreJ);
	nombreJ2[strlen(nombreJ2)-1]='\0';
	//printf("Jugadores: %s\n",nombreJ2);
	
	//Obtiene fecha y hora
	char fechayhora[100];
	DameFechaHora(fechayhora);
	int bote = DameBote(t, nombre);
	printf ("Datetime server: %s\n", fechayhora);
	//Inserto en tabla partidas
	sprintf(consulta,"INSERT INTO partidas VALUES (%d,'%s',%d,'%s');", numP, nombreJ2, bote, fechayhora);
	printf("%s\n",consulta);
	
	err = mysql_query(conn, consulta);		
	if (err!=0)		
	{		
		printf ("Error al introducir los datos %u %s\n", mysql_errno(conn), mysql_error(conn));	
	}
	else
		printf("Insert into partidas succes\n");
}

int GuardarRelacion(Ttabla t, int numP, char nombre[20], int puntos)
{
	
	char consulta[256];	
	int err;
	int id_JugadorD;
	
	sprintf(consulta,"SELECT id FROM jugadores WHERE nombre='%s';", nombre);
	printf("%s\n",consulta);
	err = mysql_query(conn, consulta);
	//Guardamos el resultado de la consulta	
	MYSQL_RES *resultado;	
	MYSQL_ROW row;	
	resultado = mysql_store_result (conn); 	
	row = mysql_fetch_row (resultado);
	if (err!=0)  //Si la consulta da error, err es diferente de 0		
	{		
		printf ("Error al introducir los datos %u %s\n", mysql_errno(conn), mysql_error(conn));			
	}		
	if (row == NULL)		
	{		
		printf ("No se han obtenido datos en la consulta.\n");
	}
	else		
	{	
		//printf("row: %s\n",row[0]);
		//strcpy (id_JugadorS, row[0]);
		id_JugadorD = atoi(row[0]);
		printf("idJ: %d\n",id_JugadorD);
	}	
	sprintf(consulta,"INSERT INTO relacion VALUES (%d,%d,%d);", id_JugadorD, numP, puntos);
	printf("%s\n",consulta);
	err = mysql_query(conn, consulta);		
	if (err!=0)		
	{		
		printf ("Error al introducir los datos %u %s.\n", mysql_errno(conn), mysql_error(conn));	
	}
}

int EliminarUsuario(char nombre[25])
{
	char consulta[256];
	sprintf (consulta,"DELETE FROM jugadores WHERE nombre='%s';",nombre);
	int err = mysql_query(conn,consulta);
	if (err!=0)
	{
		printf ("Error al borrar los datos %u %s\n",mysql_errno(conn), mysql_error(conn));
		exit (1);
		return 0;
	}
	else  //Se ha borrado con exito
		return -1;
}

//Funcion para forzar un bingo
int fuerza;
int ForzarBingo ()	
{	
	fuerza = fuerza + 1;
	return fuerza;
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
	char notificacion[500];
	
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
		char anfitrion[20];
		char invitado[20];
		char mensaje[120];
		char consulta[256];
		
		//Sacamos el codigo		
		char *p = strtok( peticion, "/");		
		int codigo =  atoi (p);		
		printf("Codigo: %d\n", codigo);
		
		if (codigo == 0 || codigo == 1 || codigo == 2 || codigo == 3 || codigo == 5 || codigo == 6 || codigo == 7 || codigo == 8 || codigo == 9 || codigo == 11 || codigo == 10 || codigo == 12 || codigo == 13 || codigo == 14 || codigo == 15)  //Saco el nombre			
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
			
			if (codigo == 8)
			{
				p = strtok( NULL, "/");				
				strcpy (mensaje, p);				
			}
		}
		
		if (codigo ==0) //Acabamos el bucle y desconectamos el servidor			
		{			
			terminar=1;
			//Llamamos a la funcion eliminar partida
			int i;
			for (i=0;i<5;i++)
			{
				EliminarPartida(t,nombre);
			}
			//Llamamos a la funcion eliminar conectado lista
			int res = Eliminar (&lista, nombre);			
			if (res == -1)				
				printf ("No esta.\n");			
			else
				printf ("Eliminado de la lista: %s\n", nombre);
			
			//Lista conectados			
			char notificacion[300];
			char misConectados [300];			
			DameConectados (&lista, misConectados);			
			printf ("misConectados: %s\n", misConectados);;
			if((strcmp(misConectados,"0"))!=0)
			{
				sprintf (notificacion, "6/%s", misConectados);
				printf ("Notificacion: %s\n", notificacion);
				for(int j=0; j<lista.num; j++)
				{
					write (lista.conectados[j].socket, notificacion, strlen(notificacion));
				}
			}
		}
		
		else if (codigo == 1)  //Crear usuario			
		{			
			pthread_mutex_lock( &mutex ); //No me interrumpas ahora			
			int ok = CrearUsuario(nombre, pass);  //Llamo a la funcion			
			pthread_mutex_unlock( &mutex); //ya puedes interrumpirme
			
			if(ok==1)  //Se ha registrado bien				
			{				
				printf("Se ha registrado correctamente\n");				
				strcpy (respuesta,"1/SI");				
				printf ("%s\n", respuesta);				
			}			
			else if (ok==0)  //Hay otro nombre igual	   
				strcpy(respuesta,"1/1NO");
			else  //No se ha podido registrar
				strcpy(respuesta,"1/2NO");
			
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
					//strcpy(respuesta,"2/SI");
					sprintf (respuesta, "2/SI/%s", nombre);
					//Llamar funcion anadir conectado								
					int res = Asigna (&lista, nombre, sock_conn);					
					if (res == -1)						
						printf ("Lista llena. No se puede anadir.\n");					
					else						
						printf ("Se ha anadido a la lista correctamente.\n");
				}								
				else
				{					
					strcpy(respuesta,"2/NO");					
					printf ("%s\n", respuesta);
				}				
			}			
			else			   
			   strcpy(respuesta,"2/NO");
			printf ("%s\n", respuesta);
			
			//Envio respuesta al cliente			
			write (sock_conn, respuesta, strlen(respuesta));
			
			//Lista conectados			
			char notificacion[300];			
			char misConectados [300];			
			DameConectados (&lista, misConectados);			
			printf ("misConectados: %s\n", misConectados);
			
			sprintf (notificacion, "6/%s", misConectados);			
			printf ("Notificacion: %s\n", notificacion);
			//Vacio misConectados
			misConectados[0]='\0';
			//Envio la respuesta al cliente			
			int j;			
			for(j=0; j<lista.num; j++)
			{
				write (lista.conectados[j].socket, notificacion, strlen(notificacion));				
			}
		}				
		
		else if (codigo == 3) //Dame jugador que ha ganado mas partidas
		{
			int ok = MasGanadas(respondido, nombre);  //Llamo a la funcion			
			if (ok==1)				
				sprintf(respuesta,"3/SI/%s/",respondido);
			else				
				strcpy (respuesta,"3/NO");
			
			ok = MasPuntuacion(respondido);  //Llamo a la funcion
			if (ok==1)
				sprintf(respuesta,"%s4/SI/%s/",respuesta,respondido);			
			else
				strcpy(respuesta,"4/NO");			
			
			JugarConOtro(nombre, respondido);  //Llamo a la funcion
			sprintf(respuesta,"%s5/SI/%s",respuesta,respondido);
			
			printf("Respuesta: %s\n",respuesta);
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
			sprintf(respuesta,"5/SI/%s",respondido);
			
			//Envio respuesta al cliente
			write (sock_conn, respuesta, strlen(respuesta));
			printf ("Enviado al cliente: %s\n", respuesta);
		}
		
		else if (codigo == 6)  //manda una invitacion
		{
			p = strtok( NULL, "/");				
			strcpy (anfitrion, p);				
			printf ("Anfitrion: %s\n", anfitrion);
			
			sprintf (notificacion, "7/SI/%s/%s", nombre, anfitrion);
			int num = DamePosicion(&lista, nombre);
			write (lista.conectados[num].socket, notificacion, strlen(notificacion));
			
			/*			res2 = NuevaPartida ();*/
			/*			printf("Nueva partida: %d\n",res2);*/
			//Llamar funcion anadir jugador a la tabla partida								
			/*			int res = PonElemento (t, anfitrion, res2, sock_conn);					*/
			/*			if (res == -1)						*/
			/*				printf ("Tabla llena. No se puede anadir.\n");					*/
			/*			else						*/
			/*				printf ("Se ha anadido a la tabla correctamente.\n");*/
			
		}
		
		else if (codigo == 7)  //manda la aceptacion
		{
			p = strtok( NULL, "/");				
			strcpy (invitado, p);				
			printf ("Invitado: %s\n", invitado);
			
			p = strtok( NULL, "/");				
			strcpy (anfitrion, p);				
			printf ("Anfitrion: %s\n", anfitrion);
			
			if (strcmp (nombre, "SI")==0)  //El invitado manda un SI
			{				
				numP = DameidP (t, anfitrion);
				printf("Nﾺ part. anf.: %d\n",numP);
				
				//Poner al jugador invitado en la tabla partida
				int res = PonElemento (t, invitado, numP, sock_conn);					
				if (res == -1)						
					printf ("Tabla llena. No se puede anadir a la tabla partida.\n");					
				else						
					printf ("Se ha anadido a la tabla partida correctamente.\n");
				
				//Poner al invitado en la lista de invitados
				res = PonInvitado(&listainv, numP, invitado);
				
				//Pedimos la lista de invitados
				DameInvitados(&listainv, numP, invitadoslist);
				
				sprintf(notificacion,"8/1/%s",invitadoslist);
				printf("Notificacion invitados: %s\n",notificacion);
				//Enviamos lista de invitados a todos los que estan en la partida
				int j;
				for(j=0; j<100; j++)
				{
					if(t[j].id == 0 && t[j].id_partida == numP)
					{
						write (t[j].sock, notificacion, strlen(notificacion));
					}
				}
				//Creamos la respuesta para el anfitiron
				sprintf (notificacion, "8/SI/%s", invitado);
			}
			else
				sprintf (notificacion, "8/NO/%s", invitado); 
			
			printf("Notificacion: %s\n", notificacion);
			int num = DamePosicion(&lista, anfitrion);
			write (lista.conectados[num].socket, notificacion, strlen(notificacion));
		}
		
		else if (codigo == 8)  //manda mensaje de chat
		{
			sprintf (notificacion, "9/%s/%s", nombre, mensaje);
			printf("Notificaci￯﾿ﾳn: %s\n",notificacion);
			
			int numP = DameidP (t, nombre);
			printf("Numero de partida que encuentra: %d\n",numP);
			if (numP == -1)
				printf ("No esta en ninguna partida.\n");
			else
			{
				int j;
				for(j=0; j<100; j++)
				{
					if(t[j].id == 0 && t[j].id_partida == numP)
					{
						printf("Socket al que se envia el mensaje: %d\n",t[j].sock);
						write (t[j].sock, notificacion, strlen(notificacion));
					}
				}
			}
		}
		
		else if (codigo == 9)  //Servicio peticion de bola
		{
			int bola;
			int resultado;
			int numP = DameidP (t, nombre);
			if (numP == -1)
				printf ("No esta en ninguna partida.");
			else
			{
				/*bola= DameBola();
				//Que no se repita la bola
				if(bola == -1)
				while(bola == -1)
				bola= DameBola();*/
				//ESTOY FORZANDO UN BINGO
				bola = ForzarBingo();
				
				//Llamar funcion anadir bola a la tabla
				int res = PonBola (b, bola, numP);					
				if (res == -1)
					printf ("Tabla llena. No se puede anadir.\n");
				
				else
					printf ("Se ha anadido a la tabla correctamente.\n");
				//printf("Bola: %d\n",bola);					}
			}
			sprintf (notificacion, "10/%d", bola);
			printf ("Notificacion: %s\n", notificacion);
			
			int j;
			for(j=0; j<100; j++)
			{
				if(t[j].id == 0 && t[j].id_partida == numP)
					write (t[j].sock, notificacion, strlen(notificacion));
			}
		}
		else if (codigo == 10)  //LINEA!!
		{
			int numP = DameidP (t, nombre);
			int trampa=0;
			int f=0;
			while ((f<5) && (trampa==0))
			{
				p = strtok( NULL, "/");				
				int num = atoi(p);			
				printf ("Numero: %d\n", num);
				//Comprobar que ese numero ha salido en esa partida y enviar respuesta ok o no ok.
				int res = CompruebaNumero(b, numP, num);
				if (res == 0)  //Numero si est￯﾿ﾡ
					trampa=0;
				else  //Numero no est￯﾿ﾡ, sale del bucle
					trampa=1;
				f++;
			}
			if(trampa==0)  //Fin de la partida, tenemos ganador.
			{
				sprintf(notificacion,"12/SI/%s",nombre);
				printf ("Notificacion: %s\n", notificacion);
				int j;
				for(j=0; j<100; j++)
				{
					if(t[j].id == 0 && t[j].id_partida == numP)
					{
						write (t[j].sock, notificacion, strlen(notificacion));
						printf("sock: %d, user: %s",t[j].sock,t[j].jugador);
					}
				}
			}
			else  //Tramposo
			{
				strcpy(respuesta,"12/NO");
				printf ("Respuesta: %s\n", respuesta);
				write (sock_conn, respuesta, strlen(respuesta));
			}
		}
		else if (codigo==11)  //BINGO!!
		{
			int numP = DameidP (t, nombre);
			
			int trampa=0;
			int f=0;
			while ((f<15) && (trampa==0))
			{
				p = strtok( NULL, "/");				
				int num = atoi(p);			
				printf ("Numero: %d\n", num);
				//Comprobar que ese numero ha salido en esa partida y enviar respuesta ok o no ok.
				int res = CompruebaNumero(b, numP, num);
				if (res == 0)  //Numero si est￯﾿ﾡ
					trampa=0;
				else  //Numero no est￯﾿ﾡ, sale del bucle
					trampa=1;
				f++;
			}
			if(trampa==0)  //Fin de la partida, tenemos ganador.
			{
				sprintf(notificacion,"11/SI/%s",nombre);
				printf ("Notificacion: %s\n", notificacion);
				int j;
				for(j=0; j<100; j++)
				{
					if(t[j].id == 0 && t[j].id_partida == numP)
						write (t[j].sock, notificacion, strlen(notificacion));
				}
			}
			else  //Tramposo
			{
				strcpy(respuesta,"11/NO");
				printf ("Respuesta: %s\n", respuesta);
				write (sock_conn, respuesta, strlen(respuesta));
			}
		}
		else if(codigo == 12)  //Dejar partida
		{
			char anfitrion[25];
			p=strtok(NULL,"/");
			strcpy(anfitrion,p);
			
			int puntos;
			p=strtok(NULL,"/");
			puntos = atoi(p);
			
			if(strcmp(nombre,anfitrion)==0)  //Si el que deja partida es anfitrion
			{
				int numP = DameidP (t, nombre);
				strcpy(notificacion,"13/A");
				printf ("Notificacion: %s\n", notificacion);
				//Lo enviamos a todos los que estan en la partida de anfitrion
				int j;
				for(j=0; j<100; j++)
				{
					if(t[j].id == 0 && t[j].id_partida == numP)
						write (t[j].sock, notificacion, strlen(notificacion));
				}
				//Aqui guardamos la partida con todos sus jugadores dentro
				pthread_mutex_lock( &mutex ); //No me interrumpas ahora	
				GuardarPartida(t,numP, nombre);
				pthread_mutex_unlock( &mutex ); //No me interrumpas ahora	
				
				//Aqui guardamos la relacion con todos sus jugadores dentro
				pthread_mutex_lock( &mutex ); //No me interrumpas ahora	
				GuardarRelacion(t,numP, nombre, puntos);
				pthread_mutex_unlock( &mutex ); //No me interrumpas ahora
			}
			else
			{
				int res = EliminaInvitado (&listainv, nombre);
				if (res == -1)						
					printf ("No se ha eliminado correctamente de la lista de invitados\n");					
				else						
					printf ("Se ha eliminado correctamente de la lista de invitados\n");
				
				int numP = DameidP (t, nombre);
				
				//Pedimos la lista de invitados
				DameInvitados(&listainv, numP, invitadoslist);
				
				sprintf(notificacion,"13/I/%s/%s",nombre, invitadoslist);
				printf("Notificacion invitados al dejar partida: %s\n",notificacion);
				//Enviamos lista de invitados a todos los que estan en la partida
				int j;
				for(j=0; j<100; j++)
				{
					if(t[j].id == 0 && t[j].id_partida == numP)
					{
						write (t[j].sock, notificacion, strlen(notificacion));
					}
				}
				//Aqui guardamos la relacion con todos sus jugadores dentro
				pthread_mutex_lock( &mutex ); //No me interrumpas ahora	
				GuardarRelacion(t,numP, nombre, puntos);
				pthread_mutex_unlock( &mutex ); //No me interrumpas ahora
				
				/*				strcpy(respuesta,"13/I");*/
				/*				printf ("Respuesta: %s\n", respuesta);*/
				/*				write (sock_conn, respuesta, strlen(respuesta));*/
			}
			pthread_mutex_lock( &mutex ); //No me interrumpas ahora	
			EliminarPartida(t,nombre);
			pthread_mutex_unlock( &mutex ); //No me interrumpas ahora	
		}
		else if (codigo == 13)
		{
			pthread_mutex_lock( &mutex ); //No me interrumpas ahora	
			int res = EliminarUsuario(nombre);
			pthread_mutex_unlock( &mutex ); //No me interrumpas ahora
			if (res == -1)
			{
				strcpy(respuesta,"14/SI");
				write (sock_conn, respuesta, strlen(respuesta));
				printf("Se ha eliminado: %s\n", nombre);
			}
			else
			{
				strcpy(respuesta,"14/NO");
				write (sock_conn, respuesta, strlen(respuesta));
				printf("No se ha eliminado: %s\n", nombre);
			}
		}
		else if (codigo == 14)
		{
			//Crea nueva partida
			idP = NuevaPartida ();	
			//Llamar funcion insertar jugador a la tabla partida
			
			int res = PonElemento (t, nombre, idP, sock_conn);					
			if (res == -1)						
				printf ("Tabla llena. No se puede anadir.\n");					
			else						
				printf ("Se ha anadido a la tabla partidas correctamente. idP: %d\n",idP);
		}
		else if (codigo == 15)
		{
			int numP = DameidP (t, nombre);
			bote = DameBote(t, nombre);
			
			sprintf(notificacion, "15/%d", bote);
			printf("Notificacion bote: %s\n",notificacion);
			for(int j=0; j<100; j++)
			{
				if(t[j].id == 0 && t[j].id_partida == numP)
					write (t[j].sock, notificacion, strlen(notificacion));
			}
		}
	}
	// Se acabo el servicio para este cliente
	close(sock_conn); 	
}

int main(int argc, char *argv[])	
{	
	srand (time(NULL));
	
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
	serv_adr.sin_port = htons(50071);
	
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");
	
	pthread_t thread;
	//Como no tenemos que usar el indentificador del trhead para nada
	//quitamos el vector y lo dejamos como una variable
	//cada vez que se conecte un cliente machacara el thread anterior.
	
	inicializa(t);
	inicializaBola(b);
	
	//Bucle infinito
	for (;;)
	{
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
