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