DROP DATABASE IF EXISTS juego;
CREATE DATABASE juego;

USE juego;

CREATE TABLE jugadores (
	id INT PRIMARY KEY AUTO_INCREMENT,
	nombre VARCHAR(25) NOT NULL,
	pass VARCHAR(25) NOT NULL,
	ganadas INT,
	edad INT)ENGINE=InnoDB;

CREATE TABLE partidas (
	id INT PRIMARY KEY AUTO_INCREMENT,
	jugador1 VARCHAR(25),
	jugador2 VARCHAR(25),
	ganador VARCHAR(25),
	tiempo INT,
	diahora DATETIME)ENGINE=InnoDB;

CREATE TABLE relacion (
	id_j INT,
	id_p INT,
	puntuacion INT,
	FOREIGN KEY (id_j) REFERENCES jugadores(id),
	FOREIGN KEY (id_p) REFERENCES partidas(id))ENGINE=InnoDB;
	

INSERT INTO jugadores VALUES (NULL,'Juan','juan1',2,20);
INSERT INTO jugadores VALUES (NULL,'Maria','maria2',1,21);
INSERT INTO jugadores VALUES (NULL,'Pepe','pepe3',1,22);
INSERT INTO jugadores VALUES (NULL,'Sole','sole4',0,23);

INSERT INTO partidas VALUES (NULL,'Juan','Maria','Juan',12,'2020/12/12 12:12:25');
INSERT INTO partidas VALUES (NULL,'Pepe','Maria','Maria',34,'2070/12/12 12:12:25');
INSERT INTO partidas VALUES (NULL,'Juan','Pepe','Juan',6,'3020/12/12 12:12:25');
INSERT INTO partidas VALUES (NULL,'Sole','Pepe','Pepe',69,'2020/02/12 02:12:25');

INSERT INTO relacion VALUES (1,1,300);
INSERT INTO relacion VALUES (2,1,200);
INSERT INTO relacion VALUES (3,2,100);
INSERT INTO relacion VALUES (2,2,300);
INSERT INTO relacion VALUES (3,3,900);
INSERT INTO relacion VALUES (3,3,350);
INSERT INTO relacion VALUES (4,4,100);
INSERT INTO relacion VALUES (3,4,400);











