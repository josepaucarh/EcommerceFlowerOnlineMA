USE FlowerOnlineMA;
GO

-- 1. Agregar columnas a la tabla Productos (Detalle de Cuidado)
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Productos' AND COLUMN_NAME = 'TipoLuz')
BEGIN
    ALTER TABLE Productos 
    ADD TipoLuz VARCHAR(100) NULL,        -- Ej: "Luz Indirecta", "Sol Directo"
        FrecuenciaRiego VARCHAR(100) NULL, -- Ej: "Riego Semanal", "Poca Agua"
        NivelCuidado VARCHAR(50) NULL;     -- Ej: "Fácil cuidado", "Principiantes"
END
GO

-- 2. Agregar columnas a la tabla Ventas (Opciones de Entrega y Dedicatoria)
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Ventas' AND COLUMN_NAME = 'TipoEntrega')
BEGIN
    ALTER TABLE Ventas 
    ADD TipoEntrega VARCHAR(50) NOT NULL DEFAULT 'EnvioDomicilio', -- 'EnvioDomicilio' o 'RecojoTienda'
        CostoEnvio DECIMAL(10,2) NOT NULL DEFAULT 15.00,
        DireccionEnvio VARCHAR(250) NULL,
        MensajeDedicatoria VARCHAR(500) NULL;
END
GO

-- 3. Agregar columna de Avatar a la tabla Usuarios (Ajustes de Perfil)
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Usuarios' AND COLUMN_NAME = 'RutaAvatar')
BEGIN
    ALTER TABLE Usuarios 
    ADD RutaAvatar VARCHAR(250) NULL;
END
GO

-- 4. Crear tabla de Favoritos (Vista de Favoritos)
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Favoritos')
BEGIN
    CREATE TABLE Favoritos (
        IdFavorito INT PRIMARY KEY IDENTITY(1,1),
        IdUsuario INT NOT NULL FOREIGN KEY REFERENCES Usuarios(IdUsuario),
        IdProducto INT NOT NULL FOREIGN KEY REFERENCES Productos(IdProducto),
        FechaAgregado DATETIME DEFAULT GETDATE(),
        CONSTRAINT UQ_Usuario_Producto UNIQUE (IdUsuario, IdProducto)
    );
END
GO

-- 1. Actualizar datos existentes con los nuevos campos de cuidado
UPDATE Productos SET 
    TipoLuz = 'Luz Indirecta', 
    FrecuenciaRiego = 'Semanal', 
    NivelCuidado = 'Fácil cuidado' 
WHERE IdCategoria = 1;

UPDATE Productos SET 
    TipoLuz = 'Sol Directo', 
    FrecuenciaRiego = 'Cada 3 días', 
    NivelCuidado = 'Moderado' 
WHERE IdCategoria = 2;
GO

-- 2. Modificar sp_ListarProductos para incluir los campos de cuidado
ALTER PROCEDURE sp_ListarProductos
    @Nombre VARCHAR(100) = ''
AS
BEGIN
    SELECT
        p.IdProducto,
        p.Nombre,
        p.Descripcion,
        p.Precio,
        p.Stock,
        p.RutaImagen,
        p.IdCategoria,
        p.TipoLuz,
        p.FrecuenciaRiego,
        p.NivelCuidado,
        c.Nombre as nombreCategoria
    FROM Productos p
    INNER JOIN Categorias c ON p.IdCategoria = c.IdCategoria
    WHERE p.Estado = 1 AND p.Nombre LIKE '%' + @Nombre + '%';
END
GO

-- 3. Modificar sp_ObtenerProductoID para incluir los campos de cuidado
ALTER PROCEDURE sp_ObtenerProductoID
    @IdProducto INT
AS
BEGIN
    SELECT
        p.IdProducto,
        p.Nombre,
        p.Descripcion,
        p.Precio,
        p.Stock,
        p.RutaImagen,
        p.IdCategoria,
        p.TipoLuz,
        p.FrecuenciaRiego,
        p.NivelCuidado,
        c.Nombre as nombreCategoria
    FROM Productos p
    INNER JOIN Categorias c ON p.IdCategoria = c.IdCategoria
    WHERE p.IdProducto = @IdProducto AND p.Estado = 1;
END
GO

-- 4. Modificar sp_InsertarProducto
ALTER PROCEDURE sp_InsertarProducto
    @Nombre VARCHAR(100),
    @Descripcion VARCHAR(500),
    @Precio DECIMAL(10,2),
    @Stock INT,
    @RutaImagen VARCHAR(250),
    @IdCategoria INT,
    @TipoLuz VARCHAR(100) = NULL,
    @FrecuenciaRiego VARCHAR(100) = NULL,
    @NivelCuidado VARCHAR(50) = NULL,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado = 1;
    SET @Mensaje = 'Producto registrado correctamente';
    
    BEGIN TRY
        INSERT INTO Productos(Nombre, Descripcion, Precio, Stock, RutaImagen, IdCategoria, TipoLuz, FrecuenciaRiego, NivelCuidado)
        VALUES (@Nombre, @Descripcion, @Precio, @Stock, @RutaImagen, @IdCategoria, @TipoLuz, @FrecuenciaRiego, @NivelCuidado);
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO

-- 5. Modificar sp_EditarProducto
ALTER PROCEDURE sp_EditarProducto
    @IdProducto INT,
    @Nombre VARCHAR(100),
    @Descripcion VARCHAR(500),
    @Precio DECIMAL(10,2),
    @Stock INT,
    @RutaImagen VARCHAR(250),
    @IdCategoria INT,
    @TipoLuz VARCHAR(100) = NULL,
    @FrecuenciaRiego VARCHAR(100) = NULL,
    @NivelCuidado VARCHAR(50) = NULL,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado = 1;
    SET @Mensaje = 'Producto actualizado correctamente';

    BEGIN TRY
        UPDATE Productos SET
            Nombre = @Nombre,
            Descripcion = @Descripcion,
            Precio = @Precio,
            Stock = @Stock,
            RutaImagen = ISNULL(@RutaImagen, RutaImagen),
            IdCategoria = @IdCategoria,
            TipoLuz = @TipoLuz,
            FrecuenciaRiego = @FrecuenciaRiego,
            NivelCuidado = @NivelCuidado
        WHERE IdProducto = @IdProducto;
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO