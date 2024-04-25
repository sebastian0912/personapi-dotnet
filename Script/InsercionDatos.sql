use persona_db
go

INSERT INTO persona (cc, nombre, apellido, genero, edad) VALUES 
(12345678, 'Ana', 'Pérez', 'F', 28),
(87654321, 'Luis', 'Gómez', 'M', 35),
(11223344, 'Carlos', 'Martín', 'M', 42),
(44332211, 'Laura', 'López', 'F', 30),
(55667788, 'Sofía', 'Díaz', 'F', 25),
(23456789, 'Julia', 'Sánchez', 'F', 26),
(98765432, 'Marco', 'Jiménez', 'M', 40),
(22334455, 'Elena', 'Fernández', 'F', 37),
(55443322, 'Javier', 'Ruiz', 'M', 29),
(66778899, 'Marta', 'Rodríguez', 'F', 32);


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
('Ingeniería de Software', 'Desarrollo y mantenimiento de software.'),
('Medicina', 'Atención médica y salud.'),
('Derecho', 'Defensa legal y asesoramiento.'),
('Arquitectura', 'Diseño y construcción de edificios.'),
('Educación', 'Enseñanza y formación académica.'),
('Contabilidad', 'Gestión financiera y auditoría.'),
('Ingeniería Civil', 'Diseño y construcción de infraestructuras.'),
('Psicología', 'Estudio del comportamiento y procesos mentales.'),
('Diseño Gráfico', 'Creación de contenido visual y branding.'),
('Periodismo', 'Investigación y difusión de noticias.');


INSERT INTO estudios (id_prof, cc_per, fecha, univer) VALUES 
(1, 12345678, '2022-05-10', 'Universidad Nacional'),
(2, 87654321, '2018-08-15', 'Universidad de Salamanca'),
(3, 11223344, '2020-02-20', 'Universidad Complutense'),
(4, 44332211, '2019-09-10', 'Universidad de Barcelona'),
(5, 55667788, '2021-03-30', 'Universidad de Granada'),
(6, 23456789, '2023-04-15', 'Universidad de Sevilla'),
(7, 98765432, '2017-11-22', 'Universidad Politécnica de Madrid'),
(8, 22334455, '2021-01-09', 'Universidad Autónoma de Barcelona'),
(9, 55443322, '2024-02-18', 'Universidad de Valencia'),
(10, 66778899, '2022-07-30', 'Universidad de Málaga');
