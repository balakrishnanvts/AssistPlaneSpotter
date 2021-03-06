USE [master]
GO
/****** Object:  Database [AssistPlaneSpotter]    Script Date: 7/19/2021 12:04:34 PM ******/
CREATE DATABASE [AssistPlaneSpotter]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AssistPlaneSpotter', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\AssistPlaneSpotter.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'AssistPlaneSpotter_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\AssistPlaneSpotter_log.ldf' , SIZE = 58368KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [AssistPlaneSpotter] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AssistPlaneSpotter].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AssistPlaneSpotter] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AssistPlaneSpotter] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AssistPlaneSpotter] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AssistPlaneSpotter] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AssistPlaneSpotter] SET ARITHABORT OFF 
GO
ALTER DATABASE [AssistPlaneSpotter] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AssistPlaneSpotter] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AssistPlaneSpotter] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AssistPlaneSpotter] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AssistPlaneSpotter] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AssistPlaneSpotter] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AssistPlaneSpotter] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AssistPlaneSpotter] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AssistPlaneSpotter] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AssistPlaneSpotter] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AssistPlaneSpotter] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AssistPlaneSpotter] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AssistPlaneSpotter] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AssistPlaneSpotter] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AssistPlaneSpotter] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AssistPlaneSpotter] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AssistPlaneSpotter] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AssistPlaneSpotter] SET RECOVERY FULL 
GO
ALTER DATABASE [AssistPlaneSpotter] SET  MULTI_USER 
GO
ALTER DATABASE [AssistPlaneSpotter] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AssistPlaneSpotter] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AssistPlaneSpotter] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AssistPlaneSpotter] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [AssistPlaneSpotter] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'AssistPlaneSpotter', N'ON'
GO
USE [AssistPlaneSpotter]
GO
/****** Object:  Table [dbo].[Plane]    Script Date: 7/19/2021 12:04:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Plane](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Make] [varchar](128) NULL,
	[Model] [varchar](128) NULL,
	[Registration] [varchar](8) NULL,
	[Location] [varchar](255) NULL,
	[EntryDateTime] [datetime] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Plane] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[USP_DELETEPLANE]    Script Date: 7/19/2021 12:04:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Balakrishnan Thirumoorthy
-- Create date: 18/07/2021
-- Description:	To do Get the data from Plane table
-- =============================================
CREATE PROCEDURE [dbo].[USP_DELETEPLANE]
(@ID INT)
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE PLANE SET STATUS = 0 WHERE ID = @ID;
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GETPLANE]    Script Date: 7/19/2021 12:04:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Balakrishnan Thirumoorthy
-- Create date: 18/07/2021
-- Description:	To do Get the data from Plane table
-- =============================================
CREATE PROCEDURE [dbo].[USP_GETPLANE]
(@ID INT)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [ID]
			,[MAKE]
			,[MODEL]
			,[REGISTRATION]
			,[LOCATION]
			,[ENTRYDATETIME]
	FROM [PLANE]
	WHERE ID = CASE WHEN (@ID IS NOT NULL AND @ID>0) THEN @ID ELSE ID END
	AND STATUS=1
	ORDER BY [ID] DESC;
END
GO
/****** Object:  StoredProcedure [dbo].[USP_MANAGEPLANE]    Script Date: 7/19/2021 12:04:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Balakrishnan Thirumoorthy
-- Create date: 18/07/2021
-- Description:	To do Insert/Update the data in Plane table
-- =============================================
CREATE PROCEDURE [dbo].[USP_MANAGEPLANE]
(@ID INT,
@MAKE VARCHAR(128),
@MODEL VARCHAR(128),
@REGISTRATION VARCHAR(8),
@LOCATION VARCHAR(255),
@ENTRYDATETIME DATETIME)
AS
BEGIN
	SET NOCOUNT ON;
	IF(@ID IS NULL OR @ID=0)
		BEGIN
			INSERT INTO [dbo].[Plane]
					([Make]
					,[Model]
					,[Registration]
					,[Location]
					,[EntryDateTime]
					,[Status])
			VALUES
			(@MAKE,
			@MODEL,
			@REGISTRATION,
			@LOCATION,
			@ENTRYDATETIME,
			1)
		END
	ELSE
		BEGIN
			UPDATE [dbo].[Plane]
			SET [Make] = @MAKE
			,[Model] = @MODEL
			,[Registration] = @REGISTRATION
			,[Location] = @LOCATION
			,[EntryDateTime] = @ENTRYDATETIME
			,[Status]=1
			WHERE Id=@ID
		END
END
GO
USE [master]
GO
ALTER DATABASE [AssistPlaneSpotter] SET  READ_WRITE 
GO
