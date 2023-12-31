USE [master]
GO
/****** Object:  Database [demoFirebase]    Script Date: 25/07/2023 11:50:15 a. m. ******/
CREATE DATABASE [demoFirebase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'demoFirebase', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\demoFirebase.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'demoFirebase_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\demoFirebase_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [demoFirebase] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [demoFirebase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [demoFirebase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [demoFirebase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [demoFirebase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [demoFirebase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [demoFirebase] SET ARITHABORT OFF 
GO
ALTER DATABASE [demoFirebase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [demoFirebase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [demoFirebase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [demoFirebase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [demoFirebase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [demoFirebase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [demoFirebase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [demoFirebase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [demoFirebase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [demoFirebase] SET  ENABLE_BROKER 
GO
ALTER DATABASE [demoFirebase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [demoFirebase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [demoFirebase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [demoFirebase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [demoFirebase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [demoFirebase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [demoFirebase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [demoFirebase] SET RECOVERY FULL 
GO
ALTER DATABASE [demoFirebase] SET  MULTI_USER 
GO
ALTER DATABASE [demoFirebase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [demoFirebase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [demoFirebase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [demoFirebase] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [demoFirebase] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [demoFirebase] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'demoFirebase', N'ON'
GO
ALTER DATABASE [demoFirebase] SET QUERY_STORE = OFF
GO
USE [demoFirebase]
GO
/****** Object:  Table [dbo].[EMPLEADO]    Script Date: 25/07/2023 11:50:15 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EMPLEADO](
	[IdEmpleado] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Telefono] [varchar](50) NULL,
	[URLimagen] [varchar](500) NULL,
	[NombreImagen] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdEmpleado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[EMPLEADO] ON 

INSERT [dbo].[EMPLEADO] ([IdEmpleado], [Nombre], [Telefono], [URLimagen], [NombreImagen]) VALUES (1, N'estefano45', N'31221666666', N'https://firebasestorage.googleapis.com/v0/b/demofirebase-6ce30.appspot.com/o/Fotos_Perfil%2FNew%20Ford%20-%20Hacienda%20Ford.jfif?alt=media&token=cf4a791a-6d7f-4c79-ae0e-298eb6f721b7', NULL)
INSERT [dbo].[EMPLEADO] ([IdEmpleado], [Nombre], [Telefono], [URLimagen], [NombreImagen]) VALUES (4, N'carro2332', N'3122121212', N'https://firebasestorage.googleapis.com/v0/b/demofirebase-6ce30.appspot.com/o/Fotos_Perfil%2Ff1000.jfif?alt=media&token=65f967ed-edbe-4c07-b47d-562f1a8ef2d9', N'f1000.jfif')
INSERT [dbo].[EMPLEADO] ([IdEmpleado], [Nombre], [Telefono], [URLimagen], [NombreImagen]) VALUES (7, N'juan', N'3114854330', N'https://firebasestorage.googleapis.com/v0/b/demofirebase-6ce30.appspot.com/o/Fotos_Perfil%2Fkent-tupas-WaUcTYPfiCU-unsplash.jpg?alt=media&token=ada567b3-22e1-4545-9a7e-685496805b71', N'')
SET IDENTITY_INSERT [dbo].[EMPLEADO] OFF
GO
ALTER TABLE [dbo].[EMPLEADO] ADD  DEFAULT ('') FOR [NombreImagen]
GO
/****** Object:  StoredProcedure [dbo].[Actualizar]    Script Date: 25/07/2023 11:50:15 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Actualizar](
@Id int,
@Nombre varchar(50),
@Telefono varchar(50),
@URLimagen varchar(500),
@NombreImagen varchar(500)
)
as
begin 
update EMPLEADO set Nombre = @Nombre, Telefono = @Telefono, URLimagen = @URLimagen, NombreImagen = @NombreImagen WHERE IdEmpleado = @Id
end


GO
/****** Object:  StoredProcedure [dbo].[Guardar]    Script Date: 25/07/2023 11:50:15 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Guardar](
@Nombre varchar(50),
@Telefono varchar(50),
@URLimagen varchar(500),
@NombreImagen varchar(500)

)
as
begin 
insert into EMPLEADO(Nombre, Telefono, URLimagen, NombreImagen) 
values(@Nombre, @Telefono, @URLimagen, @NombreImagen)
end


GO
/****** Object:  StoredProcedure [dbo].[Listar]    Script Date: 25/07/2023 11:50:15 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[Listar] as
begin 
	select * from EMPLEADO
end

GO
USE [master]
GO
ALTER DATABASE [demoFirebase] SET  READ_WRITE 
GO
