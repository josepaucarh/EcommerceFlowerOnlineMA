create database FlowerOnlineMA;
go

use FlowerOnlineMA;
go

create table Categorias
(
IdCategoria int identity(1,1) primary key,
Nombre varchar(50) not null,
Estado bit default 1 not null
);
go

 create table Productos
 (
 IdProducto int identity(1,1) primary key,
 Nombre varchar(100) not null,
 Descripcion varchar(500) null,
 Precio decimal(10,2) not null,
 Stock int not null default 0,
 RutaImagen varchar(250) null,
 IdCategoria int not null,
 Estado bit default 1 not null,
 constraint FK_Productos_Categorias foreign key (IdCategoria) references Categorias(IdCategoria)
 );
 go

 create table Usuarios
 (
 IdUsuario int identity(1,1) primary key,
 Nombre varchar(50) not null,
 Apellido varchar(50) not null,
 Correo varchar(100) unique not null,
 Clave varchar(100) not null,
 EsAdmin bit default 0 not null,
 FechaRegistro datetime default getdate() not null,
 Estado bit default 1 not null
 );
 go

 create table Ventas
 (
 IdVenta int identity(1,1) primary key,
 IdUsuario int not null,
 Total decimal(10,2) not null,
 MensajeDedicatorio varchar(500) null,
 FechaVenta datetime default getdate() not null,
 constraint fk_Ventas_Usuarios foreign key (IdUsuario) references Usuarios(IdUsuario)
 );
 go

 create table DetalleVentas
 (
 IdDetalleVenta int identity(1,1) primary key,
 IdVenta int not null,
 IdProducto int not null,
 Cantidad int not null,
 PrecioUnitario decimal(10,2) not null,
 constraint fk_DetalleVentas_Ventas foreign key (IdVenta) references Ventas(IdVenta),
 constraint fk_DetalleVentas_Productos foreign key(IdProducto) references Productos(IdProducto)
 );
 go

 insert into Categorias (Nombre) values
 ('Ramos de Rosas'), 
 ('Arreglos en Caja'), 
 ('Plantas y Orquídeas'), 
 ('Packs Especiales');

 insert into Usuarios (Nombre, Apellido, Correo, Clave, EsAdmin) values
 ('Admin', 'Floreria', 'admin@floreria.com', '123456', 1),
 ('Jose', 'Paucar', 'josepaucar.hi@gmail.com', '123456', 0);

 insert into Productos (Nombre, Descripcion, Precio, Stock, RutaImagen, IdCategoria) values
 ('Ramo Bouquet 12 Rosas Rojas', 'Hermoso bouquet de 12 rosas rojas seleccionadas con envoltorio fino.', 89.90, 20, '/Uploads/Productos/ramo-rosas.jpg', 1),
 ('Caja Premium Tulipanes Primavera', 'Caja cilíndrica de lujo con 10 tulipanes de colores variados.', 120.00, 15, '/Uploads/Productos/caja-tulipanes.jpg', 2),
 ('Orquídea Phalaenopsis Blanca', 'Planta de orquídea blanca de dos varas en maceta de cerámica.', 145.00, 10, '/Uploads/Productos/orquidea.jpg', 3),
 ('Pack Amor Infinito', 'Ramo de 6 rosas rojas + Peluche de oso mediano + Globo Feliz Día.', 110.00, 12, '/Uploads/Productos/pack-amor.jpg', 4);
 go

 --Procedimientos Almacenados

 --Listar Productos Activos
 CREATE PROCEDURE sp_ListarProductos
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
        c.Nombre as nombreCategoria
    FROM Productos p
    INNER JOIN Categorias c ON p.IdCategoria = c.IdCategoria
    WHERE p.Estado = 1 AND p.Nombre LIKE '%' + @Nombre + '%';
 END
 GO

 --Obtener Producto por ID
CREATE PROCEDURE sp_ObtenerProductoID
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
        c.Nombre as nombreCategoria
    FROM Productos p
    INNER JOIN Categorias c ON p.IdCategoria = c.IdCategoria
    WHERE p.IdProducto = @IdProducto AND p.Estado =1;
END
GO

--Insertar Productos
CREATE PROCEDURE sp_InsertarProducto
    @Nombre VARCHAR(100),
    @Descripcion VARCHAR(500),
    @Precio DECIMAL(10,2),
    @Stock INT,
    @RutaImagen VARCHAR(250),
    @IdCategoria INT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado = 1;
    SET @Mensaje = 'Producto registrado correctamente';
    
    BEGIN TRY
        INSERT INTO Productos(Nombre, Descripcion, Precio, Stock, RutaImagen, IdCategoria)
        VALUES (@Nombre, @Descripcion, @Precio, @Stock, @RutaImagen, @IdCategoria);
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO

--Editar Producto
CREATE PROCEDURE sp_EditarProducto
    @IdProducto INT,
    @Nombre VARCHAR(100),
    @Descripcion VARCHAR(500),
    @Precio DECIMAL(10,2),
    @Stock INT,
    @RutaImagen VARCHAR(250),
    @IdCategoria INT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado=1;
    SET @Mensaje='Producto actualizado correctamente';

    BEGIN TRY
        UPDATE Productos SET
            Nombre=@Nombre,
            Descripcion=@Descripcion,
            Precio= @Precio,
            Stock= @Stock,
            RutaImagen= ISNULL(@RutaImagen, RutaImagen),
            IdCategoria=@IdCategoria
        WHERE IdProducto=@IdProducto;
    END TRY
    BEGIN CATCH
        SET @Resultado=0;
        SET @Mensaje=ERROR_MESSAGE();
    END CATCH
END
GO

--Eliminar Producto
CREATE PROCEDURE sp_EliminarProducto
    @IdProducto INT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado=1;
    SET @Mensaje='Producto eliminado correctamente';
    
    BEGIN TRY
        UPDATE Productos
        SET Estado=0
        WHERE IdProducto = @IdProducto;
    END TRY
    BEGIN CATCH
        SET @Resultado=0;
        SET @Mensaje= ERROR_MESSAGE();
    END CATCH
END
GO

--Validar Login del Cliente o Administrador
CREATE PROCEDURE sp_ValidarUsuario
    @Correo VARCHAR(100),
    @Clave VARCHAR(100)
AS
BEGIN
    SELECT IdUsuario, Nombre, Apellido, Correo,EsAdmin
    FROM Usuarios
    WHERE Correo = @Correo AND Clave = @Clave AND Estado=1;
END
GO

--Validar compras
CREATE PROCEDURE sp_RegistrarVenta
    @IdUsuario INT,
    @Total DECIMAL(10,2),
    @MensajeDedicatorio VARCHAR(500),
    @IdVentaGenerado INT OUTPUT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado=1;
    SET @Mensaje='Venta iniciada correctamente';

    BEGIN TRY
        INSERT INTO Ventas(IdUsuario, Total,MensajeDedicatorio)
        VALUES(@IdUsuario,@Total,@MensajeDedicatorio);

        SET @IdVentaGenerado= SCOPE_IDENTITY();
    END TRY
    BEGIN CATCH
        SET @Resultado=0;
        SET @Mensaje= ERROR_MESSAGE();
    END CATCH
END
GO

--Registrar Detalle y Descontar stock
CREATE PROCEDURE sp_RegistrarDetalleVenta
    @IdVenta INT,
    @IdProducto INT,
    @Cantidad INT,
    @PrecioUnitario DECIMAL(10,2),
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET @Resultado=1;
    SET @mensaje='Producto agregado al detalle';

    --Validar Stock
    IF NOT EXISTS(SELECT 1 FROM Productos WHERE IdProducto= @IdProducto AND Stock >= @Cantidad)
    BEGIN
        SET @Resultado=0;
        SET @Mensaje='No hay suficiente stock disponible para este producto';
    END

    --Transaccion 
    BEGIN TRY
        BEGIN TRANSACTION;
        INSERT INTO DetalleVentas(IdVenta, IdProducto,Cantidad,PrecioUnitario)
        VALUES(@IdVenta,@IdProducto,@Cantidad,@PrecioUnitario);

    UPDATE Productos
    SET Stock= Stock - @Cantidad
    WHERE IdProducto = @IdProducto;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    SET @Resultado=0;
    SET @Mensaje=ERROR_MESSAGE();
END CATCH
END
GO