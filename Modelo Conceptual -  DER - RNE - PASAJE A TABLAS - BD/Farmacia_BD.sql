CREATE DATABASE Farmacia_BD;
GO

USE Farmacia_BD;
GO

CREATE TABLE Empleado (
    Usuario VARCHAR(50) PRIMARY KEY,
    Contrasena VARCHAR(255) NOT NULL,
    Nombre VARCHAR(100) NOT NULL
);
GO

--drop table
CREATE TABLE Cliente (
    Cedula VARCHAR(20) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    NumeroTarjeta VARCHAR(16) NOT NULL, 
    Telefono VARCHAR(9)
);
GO

--drop table
CREATE TABLE Articulo (
    Codigo VARCHAR(10) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Tamaño VARCHAR(50) NOT NULL,
    CodigoCategoria VARCHAR(6) NOT NULL,
    TipoPresentacion VARCHAR(20) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL,
    CONSTRAINT CK_TipoPresentacion CHECK (TipoPresentacion IN ('Unidad', 'Blister', 'Sobre', 'Frasco')),
    CONSTRAINT CK_Codigo_Articulo CHECK (LEN(Codigo) = 10),
    CONSTRAINT CK_Precio_Positive CHECK (Precio > 0) 
);
GO


-- drop table
CREATE TABLE Venta (
    NumeroVenta INT IDENTITY(1,1) PRIMARY KEY,
    Fecha DATE NOT NULL DEFAULT GETDATE(),
    Estado VARCHAR(20) NOT NULL DEFAULT 'Armado',
    Direccion VARCHAR(200) NOT NULL,
    cantidadNumero INT NOT NULL,
    Usuario VARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Empleado(Usuario), 
    Cedula VARCHAR(20) NOT NULL FOREIGN KEY REFERENCES Cliente(Cedula),
    Codigo VARCHAR(10) NOT NULL FOREIGN KEY REFERENCES Articulo(Codigo),
    CONSTRAINT CK_EstadoVenta CHECK (Estado IN ('Armado', 'Envío', 'Entregado', 'Devuelto')),
    CONSTRAINT CK_Cantidad_Positive CHECK (cantidadNumero > 0) 
);
GO

CREATE TABLE Categoria (
    Codigo VARCHAR(6) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    CONSTRAINT CK_Codigo_Categoria CHECK (LEN(Codigo) = 6)
);
GO


--- Procedimientos Almacenados ---

-- DROP PROC
CREATE PROCEDURE BuscarEmpleados
    @usuario VARCHAR(50)
AS
BEGIN
    SELECT *
    FROM Empleado
    WHERE Usuario = @usuario
END
GO

CREATE PROCEDURE AgregarEmpleado
    @Usuario NVARCHAR(50),
    @Contrasena NVARCHAR(255),
    @Nombre NVARCHAR(100)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Empleado WHERE Usuario = @Usuario)
        RETURN -1;

    BEGIN TRY
        INSERT INTO Empleado (Usuario, Contrasena, Nombre)
        VALUES (@Usuario, @Contrasena, @Nombre);
        RETURN 1;
    END TRY
    BEGIN CATCH
        RETURN -2;
    END CATCH
END;
GO

GO

---Procedimientos Almacenados (Venta)----

-- DROP PROC
CREATE PROCEDURE ObtenerVentasPendientes
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        v.NumeroVenta, 
        v.Fecha, 
        v.Estado, 
        v.Direccion, 
        v.cantidadNumero,
        v.Usuario AS Usuario, 
        v.Cedula AS CedulaCliente,
        v.Codigo AS CodigoArticulo,
        e.Nombre AS nombreCompletoEmpleado,  
        e.Contrasena AS contrasenaEmpleado  
    FROM Venta v
    INNER JOIN Empleado e ON v.Usuario = e.Usuario  
    INNER JOIN Articulo a ON v.Codigo = a.Codigo    
    WHERE v.Estado = 'Armado'  
    ORDER BY v.Fecha DESC;
END;
GO

CREATE PROCEDURE BuscarVenta
    @numeroVenta INT
AS
BEGIN
    SELECT *
    FROM Venta
    WHERE NumeroVenta = @numeroVenta
END
GO

-- drop proc puede cambiar los estados depende que me pida
CREATE PROCEDURE ActualizarEstado
    @numeroVenta INT,
    @nuevoEstado VARCHAR(50)
AS
BEGIN
    UPDATE Venta
    SET Estado = @nuevoEstado
    WHERE NumeroVenta = @numeroVenta;
END
GO


--drop proc 
CREATE PROCEDURE ObtenerVentasPorCliente
    @clienteCedula VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        v.NumeroVenta, 
        v.Fecha, 
        v.Estado, 
        v.Direccion, 
        v.cantidadNumero,
        v.Usuario,
        v.Cedula,
        v.Codigo AS CodigoArticulo  
    FROM Venta v
    WHERE v.Cedula = @clienteCedula
    ORDER BY v.Fecha DESC;
END;
GO

CREATE PROCEDURE RegistrarVenta 
    @Direccion NVARCHAR(200), 
    @Usuario NVARCHAR(50), 
    @Cedula VARCHAR(20), 
    @CodigoArticulo VARCHAR(10), 
    @Cantidad INT,
    @NumeroVenta INT OUTPUT
AS
BEGIN
    INSERT INTO Venta (Fecha, Estado, Direccion, cantidadNumero, Usuario, Cedula, Codigo)
    VALUES (GETDATE(), 'Armado', @Direccion, @Cantidad, @Usuario, @Cedula, @CodigoArticulo);

    SET @NumeroVenta = SCOPE_IDENTITY(); 
END;
GO

--- Procedimientos Almacenados (Articulos)---
CREATE PROCEDURE ObtenerArticulosComprados
    @CedulaCliente VARCHAR(20)
AS
BEGIN
    SELECT 
        a.Codigo AS CodigoArticulo,
        a.Nombre AS NombreArticulo,
        a.Tamaño,
        a.TipoPresentacion,
        a.Precio,
        a.CodigoCategoria,
        cat.Nombre AS NombreCategoria
    FROM 
        Venta v
        JOIN Articulo a ON v.Codigo = a.Codigo
        JOIN Cliente c ON v.Cedula = c.Cedula
        JOIN Categoria cat ON a.CodigoCategoria = cat.Codigo
    WHERE 
        c.Cedula = @CedulaCliente
END
GO

CREATE PROCEDURE BuscarArticulosPorCategoria
    @CodigoCategoria VARCHAR(6)
AS
BEGIN
    SELECT Codigo, Nombre, Tamaño, CodigoCategoria, TipoPresentacion, Precio
    FROM Articulo
    WHERE CodigoCategoria = @CodigoCategoria;
END
GO

CREATE PROCEDURE BuscarArticulo
    @Codigo VARCHAR(10)
AS
BEGIN
    SELECT * 
    FROM Articulo
    WHERE Codigo = @Codigo;
END
GO

CREATE PROCEDURE RegistrarArticulo
    @Codigo VARCHAR(10),
    @Nombre NVARCHAR(100),
    @Tamaño NVARCHAR(50),
    @CodigoCategoria VARCHAR(6),
    @TipoPresentacion NVARCHAR(20),
    @Precio DECIMAL(10, 2)
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Categoria WHERE Codigo = @CodigoCategoria)
        RETURN -1;

    IF EXISTS (SELECT 1 FROM Articulo WHERE Codigo = @Codigo)
        RETURN -2;

    BEGIN TRY
        INSERT INTO Articulo (Codigo, Nombre, Tamaño, CodigoCategoria, TipoPresentacion, Precio)
        VALUES (@Codigo, @Nombre, @Tamaño, @CodigoCategoria, @TipoPresentacion, @Precio);
        RETURN 1;
    END TRY
    BEGIN CATCH
        RETURN -3;
    END CATCH
END;
GO

CREATE PROCEDURE EliminarArticulo
    @codigo VARCHAR(10)
AS
BEGIN
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Articulo WHERE Codigo = @codigo)
            RETURN -1;
        IF EXISTS (SELECT 1 FROM Venta WHERE Codigo = @codigo)
        DELETE FROM Articulo WHERE Codigo = @codigo;
        RETURN 1;
    END TRY
    BEGIN CATCH
        RETURN -3;
    END CATCH
END
GO

CREATE PROCEDURE ModificarArticulo
    @codigo VARCHAR(10),
    @nombre VARCHAR(100),
    @tamaño VARCHAR(50),
    @codigoCategoria VARCHAR(6),
    @tipoPresentacion VARCHAR(20),
    @precio DECIMAL(10, 2)
AS
BEGIN

    IF NOT EXISTS (SELECT 1 FROM Articulo WHERE Codigo = @codigo)
        RETURN -1;

    IF NOT EXISTS (SELECT 1 FROM Categoria WHERE Codigo = @codigoCategoria)
        RETURN -2;

    UPDATE Articulo
    SET Nombre = @nombre,
        Tamaño = @tamaño,
        CodigoCategoria = @codigoCategoria,
        TipoPresentacion = @tipoPresentacion,
        Precio = @precio
    WHERE Codigo = @codigo;

    RETURN 1;
END
GO

CREATE PROCEDURE ListarArticulos
AS
BEGIN
    SELECT Codigo, Nombre, Tamaño, CodigoCategoria, TipoPresentacion, Precio
    FROM Articulo
    ORDER BY Nombre;
END
GO


--- Procedimientos Almacenados (Categoria)---

CREATE PROCEDURE BuscarCategoria
    @codigo VARCHAR(6)
AS
BEGIN
    SELECT *
    FROM Categoria
    WHERE Codigo = @codigo;
END
GO

CREATE PROCEDURE BuscarTodasCategorias
AS
BEGIN
    SELECT Codigo, Nombre FROM Categoria
END
GO

CREATE PROCEDURE AgregarCategoria
    @Codigo VARCHAR(6),
    @Nombre NVARCHAR(50)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Categoria WHERE Codigo = @Codigo)
        RETURN -1;

    BEGIN TRY
        INSERT INTO Categoria (Codigo, Nombre)
        VALUES (@Codigo, @Nombre);
        RETURN 1;
    END TRY
    BEGIN CATCH
        RETURN -2;
    END CATCH
END;
GO

--drop proc
CREATE PROCEDURE EliminarCategoria
    @codigo VARCHAR(20)
AS
BEGIN
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Categoria WHERE Codigo = @codigo)
            RETURN -1;
        IF EXISTS (SELECT 1 FROM Articulo WHERE CodigoCategoria = @codigo)
            RETURN -2; 
        DELETE FROM Categoria WHERE Codigo = @codigo;
        RETURN 1;
    END TRY
    BEGIN CATCH
        RETURN -3;
    END CATCH
END
GO

CREATE PROCEDURE ModificarCategoria
    @codigo VARCHAR(6),
    @nombre VARCHAR(50)
AS
BEGIN
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Categoria WHERE Codigo = @codigo)
            RETURN -1; 
        UPDATE Categoria
        SET Nombre = @nombre
        WHERE Codigo = @codigo;

        RETURN 1;
    END TRY
    BEGIN CATCH
        RETURN -2;
    END CATCH
END
GO

CREATE PROCEDURE ListarCategoria
AS
BEGIN
    SELECT Codigo, Nombre
    FROM Categoria
    ORDER BY Nombre;
END
GO


--- Procedimientos Almacenados (Cliente)---

CREATE PROCEDURE AgregarCliente
    @Cedula VARCHAR(20),
    @Nombre NVARCHAR(100),
    @NumeroTarjeta VARCHAR(16),
    @Telefono VARCHAR(9)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Cliente WHERE Cedula = @Cedula)
        RETURN -1; 
    BEGIN TRY
        INSERT INTO Cliente (Cedula, Nombre, NumeroTarjeta, Telefono)
        VALUES (@Cedula, @Nombre, @NumeroTarjeta, @Telefono);     
        RETURN 1; 
    END TRY
    BEGIN CATCH
        RETURN -2;
    END CATCH
END
GO

CREATE PROCEDURE BuscarCI
@cedula INT 
AS
BEGIN
	SELECT *
	FROM Cliente
	WHERE cedula = @cedula
END
GO

CREATE PROCEDURE ModificarCliente
    @cedula VARCHAR(20),
    @nombre NVARCHAR(100),
    @NumeroTarjeta VARCHAR(255),
    @Telefono VARCHAR(20)
AS
BEGIN
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Cliente WHERE Cedula = @cedula)
            RETURN -1; 

        UPDATE Cliente
        SET Nombre = @nombre, 
            NumeroTarjeta = @NumeroTarjeta, 
            Telefono = @Telefono
        WHERE Cedula = @cedula;

        RETURN 1;
    END TRY
    BEGIN CATCH
        RETURN -2;
    END CATCH
END
GO

CREATE PROCEDURE EliminarCliente
    @cedula VARCHAR(20)
AS
BEGIN
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Cliente WHERE Cedula = @cedula)
            RETURN -1;
        IF EXISTS (SELECT 1 FROM Venta WHERE Cedula = @cedula)
            RETURN -2; 
        DELETE FROM Cliente WHERE Cedula = @cedula;

        RETURN 1; 
    END TRY
    BEGIN CATCH
        RETURN -3;
    END CATCH
END
GO

CREATE PROCEDURE ListarClientes
AS
BEGIN
    SELECT Cedula, Nombre, NumeroTarjeta, Telefono
    FROM Cliente
    ORDER BY Nombre; 
END
GO

--- LOGUEO ---

CREATE PROCEDURE LogueoEmpleado
    @Usuario VARCHAR(50),
    @Contrasena VARCHAR(255)
AS
BEGIN 
    SELECT Usuario, Nombre, Contrasena FROM Empleado WHERE Usuario = @Usuario and Contrasena = @Contrasena;
END
GO


--- Datos de Prueba --- 

EXEC AgregarEmpleado 'maria', '222222', 'Maria López';
EXEC AgregarEmpleado 'Juan', '111111', 'JUAN GOMEZ';

EXEC AgregarCliente '12345678', 'Pedro Gomez', '4556123456709012', '0987654321';
EXEC AgregarCliente '34567890', 'Carlos Pérez', '4556123412341234', '0998765432';
EXEC AgregarCliente '00000000', 'Ana Martínez', '4556999911112222', '0988123456';

EXEC AgregarCategoria 'MED001', 'Medicamentos';
EXEC AgregarCategoria 'CUR002', 'Curaciones';

EXEC RegistrarArticulo 'ART0010001', 'Ibuprofeno 400mg', '20', 'MED001', 'Unidad', 50;
EXEC RegistrarArticulo 'ART0010002', 'Perifar Migra', '8', 'MED001', 'Unidad', 280;

DECLARE @NumeroVenta INT;

-- Primera venta
EXEC RegistrarVenta 'Patagones 688', 'maria', '34567890', 'ART0010001', 5, @NumeroVenta OUTPUT;
SELECT @NumeroVenta AS NumeroVentaGenerado;

-- Reutilizar la misma variable para la segunda venta
EXEC RegistrarVenta 'burdeos', 'Juan', '00000000', 'ART0010001', 2, @NumeroVenta OUTPUT;
SELECT @NumeroVenta AS NumeroVentaGenerado;

EXEC ObtenerVentasPendientes;

SELECT * FROM Empleado;

SELECT * FROM Cliente;

SELECT * FROM Categoria;

SELECT * FROM Articulo;

SELECT * FROM Venta;

