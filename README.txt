Dear User 

Here are some instructions before running the program

you should create the database, table and procedure before on Microsoft SQL server which is provided below in this test file

Connect also visual studio to the database through server explorer

Before running the code you should also modify the connectionString found in Web.config

I did my best to fullfill the assignment given and i wish you Good Luck!!!

Laurent


<connectionStrings>
    <add name="dbConnection" connectionString="Data Source=your laptop/PC;Initial Catalog=your database;Integrated Security=True" providerName="System.Data.SqlClient"/>



--DATABASE CREATED


USE [master]
GO

/****** Object:  Database [customer_shares]    Script Date: 15/02/2021 23:05:46 ******/
CREATE DATABASE [customer_shares]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'customer_shares', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\customer_shares.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'customer_shares_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\customer_shares_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [customer_shares].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [customer_shares] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [customer_shares] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [customer_shares] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [customer_shares] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [customer_shares] SET ARITHABORT OFF 
GO

ALTER DATABASE [customer_shares] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [customer_shares] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [customer_shares] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [customer_shares] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [customer_shares] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [customer_shares] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [customer_shares] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [customer_shares] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [customer_shares] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [customer_shares] SET  DISABLE_BROKER 
GO

ALTER DATABASE [customer_shares] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [customer_shares] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [customer_shares] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [customer_shares] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [customer_shares] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [customer_shares] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [customer_shares] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [customer_shares] SET RECOVERY FULL 
GO

ALTER DATABASE [customer_shares] SET  MULTI_USER 
GO

ALTER DATABASE [customer_shares] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [customer_shares] SET DB_CHAINING OFF 
GO

ALTER DATABASE [customer_shares] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [customer_shares] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [customer_shares] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [customer_shares] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [customer_shares] SET QUERY_STORE = OFF
GO

ALTER DATABASE [customer_shares] SET  READ_WRITE 
GO



--TABLE CUSTOMER CREATED 

USE [customer_shares]
GO

/****** Object:  Table [dbo].[tblCustomer]    Script Date: 15/02/2021 23:05:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblCustomer](
	[RecordNo] [int] IDENTITY(1,1) NOT NULL,
	[DocRef] [nvarchar](50) NULL,
	[CustomerId] [nvarchar](50) NULL,
	[Type] [nvarchar](50) NULL,
	[DOB] [nvarchar](50) NULL,
	[DateIncorp] [nvarchar](50) NULL,
	[RegNo] [nvarchar](50) NULL,
	[Address1] [nvarchar](50) NULL,
	[Address2] [nvarchar](50) NULL,
	[Town_City] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[Name] [nvarchar](100) NULL,
	[ContactNo] [nvarchar](50) NULL,
	[NumShares] [bigint] NULL,
	[SharePrice] [decimal](18, 2) NULL,
	[Balance] [decimal](18, 2) NULL,
 CONSTRAINT [PK_table_customer] PRIMARY KEY CLUSTERED 
(
	[RecordNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblCustomer] SET (LOCK_ESCALATION = DISABLE)
GO






--PROCEDURE IN SQL DATABASE


USE [customer_shares]
GO

/****** Object:  StoredProcedure [dbo].[insertCustomer]    Script Date: 15/02/2021 23:06:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[insertCustomer] 
(
	@docRef nvarchar(50), 
	@customerId nvarchar(50), 
	@type nvarchar(50), 
	@dob nvarchar(50), 
	@dateIncorp nvarchar(50), 
	@regNo nvarchar(50), 
	@address1 nvarchar(50), 
	@address2 nvarchar(50), 
	@town_city nvarchar(50), 
	@country nvarchar(50), 
	@name nvarchar(50), 
	@contactNo nvarchar(50), 
	@numShares int, 
	@sharePrice decimal(18, 2), 
	@balance decimal(18, 2)
	
)
AS
IF EXISTS(SELECT 'True' FROM [dbo].[tblCustomer] WHERE CustomerId = @customerId AND DocRef=@docRef)
BEGIN
	RETURN
END
ELSE
BEGIN
	INSERT INTO dbo.tblCustomer (DocRef, CustomerId, Type, DOB, DateIncorp, RegNo, Address1, Address2, Town_City, Country, Name, ContactNo, NumShares, 
		SharePrice, Balance) 
     VALUES (@docRef, @customerId, @type, @dob, @dateIncorp, @regNo, @address1, @address2, @town_city, @country, @name, @contactNo, @numShares, 
	 @sharePrice, @balance)
END
GO




