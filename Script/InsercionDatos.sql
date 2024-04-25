use persona_db
go

INSERT INTO persona (cc, nombre, apellido, genero, edad) VALUES 
(12345678, 'Ana', 'P�rez', 'F', 28),
(87654321, 'Luis', 'G�mez', 'M', 35),
(11223344, 'Carlos', 'Mart�n', 'M', 42),
(44332211, 'Laura', 'L�pez', 'F', 30),
(55667788, 'Sof�a', 'D�az', 'F', 25),
(23456789, 'Julia', 'S�nchez', 'F', 26),
(98765432, 'Marco', 'Jim�nez', 'M', 40),
(22334455, 'Elena', 'Fern�ndez', 'F', 37),
(55443322, 'Javier', 'Ruiz', 'M', 29),
(66778899, 'Marta', 'Rodr�guez', 'F', 32);


INSERT INTO telefono (num, oper, duenio) VALUES 
('+34123456789', 'Movistar', 12345678),
('+34876543210', 'Vodafone', 87654321),
('+34112233445', 'Orange', 11223344),
('+34443322112', 'Yoigo', 44332211),
('+34556677889', 'Pepephone', 55667788),
('+34657890123', 'Movistar', 23456789),
('+34987654321', 'Vodafone', 98765432),
('+34223344556', 'Orange', 22334455),
('+34554433221', 'Yoigo', 55443322),
('+34667788990', 'Pepephone', 66778899);


INSERT INTO profesion (nom, des) VALUES 
('Ingenier�a de Software', 'Desarrollo y mantenimiento de software.'),
('Medicina', 'Atenci�n m�dica y salud.'),
('Derecho', 'Defensa legal y asesoramiento.'),
('Arquitectura', 'Dise�o y construcci�n de edificios.'),
('Educaci�n', 'Ense�anza y formaci�n acad�mica.'),
('Contabilidad', 'Gesti�n financiera y auditor�a.'),
('Ingenier�a Civil', 'Dise�o y construcci�n de infraestructuras.'),
('Psicolog�a', 'Estudio del comportamiento y procesos mentales.'),
('Dise�o Gr�fico', 'Creaci�n de contenido visual y branding.'),
('Periodismo', 'Investigaci�n y difusi�n de noticias.');


INSERT INTO estudios (id_prof, cc_per, fecha, univer) VALUES 
(1, 12345678, '2022-05-10', 'Universidad Nacional'),
(2, 87654321, '2018-08-15', 'Universidad de Salamanca'),
(3, 11223344, '2020-02-20', 'Universidad Complutense'),
(4, 44332211, '2019-09-10', 'Universidad de Barcelona'),
(5, 55667788, '2021-03-30', 'Universidad de Granada'),
(6, 23456789, '2023-04-15', 'Universidad de Sevilla'),
(7, 98765432, '2017-11-22', 'Universidad Polit�cnica de Madrid'),
(8, 22334455, '2021-01-09', 'Universidad Aut�noma de Barcelona'),
(9, 55443322, '2024-02-18', 'Universidad de Valencia'),
(10, 66778899, '2022-07-30', 'Universidad de M�laga');
