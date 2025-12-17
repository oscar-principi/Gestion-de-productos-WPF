CREATE DATABASE ABMLWPFDB;
GO

USE ABMLWPFDB;
GO


--Entidad
CREATE TABLE Productos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    Stock INT NOT NULL,
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FechaModificacion DATETIME NULL
);
GO

-------------------Producto--------------------
--Alta
CREATE PROCEDURE sp_InsertarProducto
    @Nombre NVARCHAR(100),
    @Precio DECIMAL(18,2),
    @Stock INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Productos (Nombre, Precio, Stock, FechaCreacion)
    VALUES (@Nombre, @Precio, @Stock, GETDATE());
END
GO


--Baja
CREATE PROCEDURE sp_EliminarProducto
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Productos
    WHERE Id = @Id;
END
GO



--Modificacion
CREATE PROCEDURE sp_ActualizarProducto
    @Id INT,
    @Nombre NVARCHAR(100),
    @Precio DECIMAL(18,2),
    @Stock INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Productos
    SET Nombre = @Nombre,
        Precio = @Precio,
        Stock = @Stock,
        FechaModificacion = GETDATE()
    WHERE Id = @Id;
END
GO



--Listado
CREATE PROCEDURE sp_ListarProductos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, Nombre, Precio, Stock, FechaCreacion, FechaModificacion
    FROM Productos;
END
GO


--Busqueda 
CREATE OR ALTER PROCEDURE sp_BuscarProductos
    @Nombre NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Busqueda NVARCHAR(110) = '%' + ISNULL(@Nombre, '') + '%';

    SELECT Id, Nombre, Precio, Stock, FechaCreacion, FechaModificacion
    FROM Productos
    WHERE Nombre LIKE @Busqueda
    ORDER BY Nombre ASC;
END
GO







-------------------Usuarios--------------------
USE ABMLWPFDB;
GO

-- Tabla de Usuarios
CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(256) NOT NULL, -- Guardaremos un Hash, no texto plano
    Email NVARCHAR(100)
);
GO

-- Procedimiento para Registrar Usuario
CREATE PROCEDURE sp_RegistrarUsuario
    @Name NVARCHAR(50),
    @Password NVARCHAR(256),
    @Email NVARCHAR(100)
AS
BEGIN
    INSERT INTO Usuarios (Name, Password, Email)
    VALUES (@Name, @Password, @Email);
END;
GO

-- Procedimiento para Login
CREATE PROCEDURE sp_Login
    @Name NVARCHAR(50),
    @Password NVARCHAR(256)
AS
BEGIN
    SELECT * FROM Usuarios 
    WHERE Name = @Name AND Password = @Password;
END;
GO