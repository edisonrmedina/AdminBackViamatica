-- Crear la base de datos ADMIN
CREATE DATABASE ADMIN;
GO

-- Usar la base de datos ADMIN
USE ADMIN;
GO

-- Crear tabla Rol
CREATE TABLE Rol (
    IdRol INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Activo BIT DEFAULT 1,
    IdTipoRol INT,
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FechaModificacion DATETIME
);
GO

-- Crear tabla ModuloAdmin
CREATE TABLE ModuloAdmin (
    IdModuloAdmin INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Activo BIT DEFAULT 1,
    Icono VARCHAR(255),
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FechaModificacion DATETIME
);
GO

-- Crear tabla SubModuloAdmin
CREATE TABLE SubModuloAdmin (
    IdSubModulo INT IDENTITY(1,1) PRIMARY KEY,
    IdModulo INT FOREIGN KEY REFERENCES ModuloAdmin(IdModuloAdmin) ON DELETE CASCADE ON UPDATE CASCADE,
    Nombre VARCHAR(100) NOT NULL,
    Activo BIT DEFAULT 1,
    Icono VARCHAR(255),
    Ruta VARCHAR(255),
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FechaModificacion DATETIME
);
GO

-- Crear tabla Usuario
CREATE TABLE Usuario (
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    Correo VARCHAR(100) NOT NULL UNIQUE,
    Usuario VARCHAR(100) NOT NULL,
    Activo BIT DEFAULT 1,
    IdRol INT FOREIGN KEY REFERENCES Rol(IdRol) ON DELETE SET NULL ON UPDATE CASCADE,
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FechaModificacion DATETIME
);
GO

-- Crear tabla AccesoUsuario
CREATE TABLE AccesoUsuario (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdSubModulo INT FOREIGN KEY REFERENCES SubModuloAdmin(IdSubModulo) ON DELETE CASCADE ON UPDATE CASCADE,
    IdRol INT FOREIGN KEY REFERENCES Rol(IdRol) ON DELETE CASCADE ON UPDATE CASCADE,
    Activo BIT DEFAULT 1,
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FechaModificacion DATETIME
);
GO

-- Crear tabla Sesion
CREATE TABLE Sesion (
    IdSesion INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT FOREIGN KEY REFERENCES Usuario(IdUsuario) UNIQUE,
    Token UNIQUEIDENTIFIER DEFAULT NEWID(),
    FechaInicio DATETIME NOT NULL,
    FechaExpiracion DATETIME NOT NULL,
    Activo BIT DEFAULT 1
);
GO

-- Crear tabla Opcion
CREATE TABLE Opcion (
    IdOpcion INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(255),
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FechaModificacion DATETIME
);
GO

-- Crear tabla PermisoRol
CREATE TABLE PermisoRol (
    IdPermisoRol INT IDENTITY(1,1) PRIMARY KEY,
    IdRol INT FOREIGN KEY REFERENCES Rol(IdRol) ON DELETE CASCADE ON UPDATE CASCADE,
    IdOpcion INT FOREIGN KEY REFERENCES Opcion(IdOpcion) ON DELETE CASCADE ON UPDATE CASCADE,
    Activo BIT DEFAULT 1,
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FechaModificacion DATETIME
);
GO

-- Trigger para actualizar la fecha de modificación en la tabla Rol
CREATE TRIGGER trg_UpdateRol
ON Rol
AFTER UPDATE
AS
BEGIN
    UPDATE Rol
    SET FechaModificacion = GETDATE()
    FROM Rol
    INNER JOIN inserted ON Rol.IdRol = inserted.IdRol;
END;
GO

-- Trigger para actualizar la fecha de modificación en la tabla ModuloAdmin
CREATE TRIGGER trg_UpdateModuloAdmin
ON ModuloAdmin
AFTER UPDATE
AS
BEGIN
    UPDATE ModuloAdmin
    SET FechaModificacion = GETDATE()
    FROM ModuloAdmin
    INNER JOIN inserted ON ModuloAdmin.IdModuloAdmin = inserted.IdModuloAdmin;
END;
GO

-- Trigger para actualizar la fecha de modificación en la tabla SubModuloAdmin
CREATE TRIGGER trg_UpdateSubModuloAdmin
ON SubModuloAdmin
AFTER UPDATE
AS
BEGIN
    UPDATE SubModuloAdmin
    SET FechaModificacion = GETDATE()
    FROM SubModuloAdmin
    INNER JOIN inserted ON SubModuloAdmin.IdSubModulo = inserted.IdSubModulo;
END;
GO

-- Trigger para actualizar la fecha de modificación en la tabla Usuario
CREATE TRIGGER trg_UpdateUsuario
ON Usuario
AFTER UPDATE
AS
BEGIN
    UPDATE Usuario
    SET FechaModificacion = GETDATE()
    FROM Usuario
    INNER JOIN inserted ON Usuario.IdUsuario = inserted.IdUsuario;
END;
GO

-- Trigger para actualizar la fecha de modificación en la tabla AccesoUsuario
CREATE TRIGGER trg_UpdateAccesoUsuario
ON AccesoUsuario
AFTER UPDATE
AS
BEGIN
    UPDATE AccesoUsuario
    SET FechaModificacion = GETDATE()
    FROM AccesoUsuario
    INNER JOIN inserted ON AccesoUsuario.Id = inserted.Id;
END;
GO

-- Trigger para actualizar la fecha de modificación en la tabla Opcion
CREATE TRIGGER trg_UpdateOpcion
ON Opcion
AFTER UPDATE
AS
BEGIN
    UPDATE Opcion
    SET FechaModificacion = GETDATE()
    FROM Opcion
    INNER JOIN inserted ON Opcion.IdOpcion = inserted.IdOpcion;
END;
GO

-- Trigger para manejar eliminación en la tabla Rol
CREATE TRIGGER trg_DeleteRol
ON Rol
AFTER DELETE
AS
BEGIN
    DELETE FROM AccesoUsuario
    WHERE IdRol IN (SELECT IdRol FROM deleted);

    DELETE FROM PermisoRol
    WHERE IdRol IN (SELECT IdRol FROM deleted);
END;
GO

-- Trigger para manejar eliminación en la tabla ModuloAdmin
CREATE TRIGGER trg_DeleteModuloAdmin
ON ModuloAdmin
AFTER DELETE
AS
BEGIN
    DELETE FROM SubModuloAdmin
    WHERE IdModulo IN (SELECT IdModuloAdmin FROM deleted);
END;
GO

-- Trigger para manejar eliminación en la tabla SubModuloAdmin
CREATE TRIGGER trg_DeleteSubModuloAdmin
ON SubModuloAdmin
AFTER DELETE
AS
BEGIN
    DELETE FROM AccesoUsuario
    WHERE IdSubModulo IN (SELECT IdSubModulo FROM deleted);
END;
GO

-- Trigger para manejar eliminación en la tabla Usuario
CREATE TRIGGER trg_DeleteUsuario
ON Usuario
AFTER DELETE
AS
BEGIN
    DELETE FROM Sesion
    WHERE IdUsuario IN (SELECT IdUsuario FROM deleted);
END;
GO
