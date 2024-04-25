use persona_db;
go

-- Creating 'profesion' table
CREATE TABLE profesion (
    id INT PRIMARY KEY IDENTITY,
    nom VARCHAR(90),
    des TEXT
);
GO

-- Creating 'persona' table
CREATE TABLE persona (
    cc INT PRIMARY KEY,
    nombre VARCHAR(45),
    apellido VARCHAR(45),
    genero CHAR(1) CHECK (genero IN ('M', 'F')),
    edad INT
);
GO

-- Creating 'estudios' table
CREATE TABLE estudios (
    id_prof INT,
    cc_per INT,
    fecha DATE,
    univer VARCHAR(50),
    CONSTRAINT PK_estudios PRIMARY KEY (id_prof, cc_per),
    CONSTRAINT FK_estudios_profesion FOREIGN KEY (id_prof) REFERENCES profesion(id),
    CONSTRAINT FK_estudios_persona FOREIGN KEY (cc_per) REFERENCES persona(cc)
);
GO

-- Creating 'telefono' table
CREATE TABLE telefono (
    num VARCHAR(15) PRIMARY KEY,
    oper VARCHAR(45),
    duenio INT,
    CONSTRAINT FK_telefono_persona FOREIGN KEY (duenio) REFERENCES persona(cc)
);
GO
