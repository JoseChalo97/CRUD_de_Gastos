
CREATE DATABASE [AcademiaDB];
GO

USE [AcademiaDB];
GO

CREATE TABLE [dbo].[Cursos] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [Nombre] NVARCHAR (100)  NOT NULL,
    [Instructor] NVARCHAR (50) NOT NULL,
    [Cuota]   DECIMAL (10,2)   NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

INSERT INTO Cursos (Nombre, Instructor, Cuota) VALUES ('Ingles Basico', 'Marta Lopez', 150.00);
INSERT INTO Cursos (Nombre, Instructor, Cuota) VALUES ('Frances', 'Luis Perez', 200.00);
GO
