USE [MICROSERVICIOSANTIAGOTRUJILLO]
GO
/****** Object:  Table [dbo].[MICROST_CLIENTE]    Script Date: 24/7/2022 17:23:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MICROST_CLIENTE](
	[ID_CLIENTE] [int] IDENTITY(1,1) NOT NULL,
	[CMP_CONTRASENA] [varchar](30) NOT NULL,
	[CMP_ESTADO] [bit] NOT NULL,
	[CMP_NOMBRE] [varchar](30) NOT NULL,
	[CMP_GENERO] [varchar](30) NOT NULL,
	[CMP_EDAD] [int] NOT NULL,
	[CMP_IDENTIFICACION] [varchar](30) NOT NULL,
	[CMP_DIRECCION] [varchar](50) NULL,
	[CMP_TELEFONO] [varchar](20) NULL,
 CONSTRAINT [PK_MICROST_CLIENTE] PRIMARY KEY CLUSTERED 
(
	[ID_CLIENTE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MICROST_CUENTAS]    Script Date: 24/7/2022 17:23:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MICROST_CUENTAS](
	[CMP_NUMERO_CUENTA] [varchar](30) NOT NULL,
	[CMP_ID_CLIENTE] [int] NOT NULL,
	[CMP_TIPO] [varchar](30) NOT NULL,
	[CMP_ESTADO] [bit] NOT NULL,
 CONSTRAINT [PK__cuentas__5138EEC71FAE4CF6] PRIMARY KEY CLUSTERED 
(
	[CMP_NUMERO_CUENTA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MICROST_MOVIMIENTOS]    Script Date: 24/7/2022 17:23:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MICROST_MOVIMIENTOS](
	[ID_MOVIMIENTO] [int] IDENTITY(1,1) NOT NULL,
	[CMP_NUMERO_CUENTA] [varchar](30) NOT NULL,
	[CMP_FECHA] [datetime] NOT NULL,
	[CMP_TIPO_MOVIMIENTO] [varchar](30) NOT NULL,
	[CMP_SALDO_INICIAL] [money] NOT NULL,
	[CMP_MOVIMIENTO] [money] NOT NULL,
	[CMP_SALDO_DISPONIBLE] [money] NOT NULL,
 CONSTRAINT [PK__movimien__2D96FD74C19E909F] PRIMARY KEY CLUSTERED 
(
	[ID_MOVIMIENTO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MICROST_CUENTAS]  WITH CHECK ADD  CONSTRAINT [FK_cuentas_cuentas] FOREIGN KEY([CMP_ID_CLIENTE])
REFERENCES [dbo].[MICROST_CLIENTE] ([ID_CLIENTE])
GO
ALTER TABLE [dbo].[MICROST_CUENTAS] CHECK CONSTRAINT [FK_cuentas_cuentas]
GO
ALTER TABLE [dbo].[MICROST_MOVIMIENTOS]  WITH CHECK ADD  CONSTRAINT [FK_movimientos_cuentas] FOREIGN KEY([CMP_NUMERO_CUENTA])
REFERENCES [dbo].[MICROST_CUENTAS] ([CMP_NUMERO_CUENTA])
GO
ALTER TABLE [dbo].[MICROST_MOVIMIENTOS] CHECK CONSTRAINT [FK_movimientos_cuentas]
GO
