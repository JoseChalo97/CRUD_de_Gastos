
CREATE DATABASE [GastosDB];
GO

USE [GastosDB];
GO

CREATE TABLE [dbo].[Gastos] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [Descripcion] NVARCHAR (100)  NOT NULL,
    [Monto]       DECIMAL (10, 2) NOT NULL,
    [Categoria]   NVARCHAR (50)   NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

INSERT INTO Gastos (Descripcion, Monto, Categoria) VALUES ('Almuerzo', 45.50, 'Comida');
INSERT INTO Gastos (Descripcion, Monto, Categoria) VALUES ('Gasolina', 250.00, 'Transporte');
INSERT INTO Gastos (Descripcion, Monto, Categoria) VALUES ('Cine', 75.00, 'Entretenimiento');
GO
