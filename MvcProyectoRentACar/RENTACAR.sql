SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[COCHE](
	[ID] [int] NOT NULL,
	[MARCA] [nvarchar](50) NULL,
	[MODELO] [nvarchar](50) NULL,
	[MATRICULA] [nvarchar](50) NULL,
	[IMAGEN] [nvarchar](50) NULL,
	[ASIENTOS] [int] NULL,
	[IDMARCHAS] [int] NULL,
	[IDGAMA] [int] NULL,
	[IDESTADO] [int] NULL,
	[KILOMETRAJE] [int] NULL,
	[PUERTAS] [int] NULL,
	[IDCOMBUSTIBLE] [int] NULL,
	[IDVENDEDOR] [int] NULL,
	[PRECIOKILOMETROS] [decimal](10, 2) NULL,
	[PRECIOILIMITADO] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[COMBUSTIBLE]    Script Date: 14/03/2025 12:53:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[COMBUSTIBLE](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TIPO] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ESTADO]    Script Date: 14/03/2025 12:53:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ESTADO](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ESTADO] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GAMA]    Script Date: 14/03/2025 12:53:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GAMA](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NIVEL] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MARCHAS]    Script Date: 14/03/2025 12:53:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MARCHAS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TIPO] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VISTA_COCHES]    Script Date: 14/03/2025 12:53:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VISTA_COCHES] AS 

SELECT  

    C.ID, 

    C.MARCA, 

    C.MODELO, 

    C.IMAGEN, 

    C.ASIENTOS, 

    M.TIPO AS MARCHA,                  -- SUSTITUYE EL ID DE MARCHAS POR EL TIPO DE MARCHA 

    G.NIVEL AS GAMA,                   -- SUSTITUYE EL ID DE GAMA POR EL NOMBRE DE LA GAMA 

    E.ESTADO AS ESTADO,                -- SUSTITUYE EL ID DE ESTADO POR EL NOMBRE DEL ESTADO 

    C.KILOMETRAJE, 

    C.PUERTAS, 

    CO.TIPO AS COMBUSTIBLE,           -- SUSTITUYE EL ID DE COMBUSTIBLE POR EL TIPO DE COMBUSTIBLE 

    C.PRECIOKILOMETROS, 

    C.PRECIOILIMITADO 

FROM  

    COCHE C 

JOIN  

    MARCHAS M ON C.IDMARCHAS = M.ID   -- UNE LA TABLA MARCHAS PARA OBTENER EL TIPO DE MARCHA 

JOIN  

    GAMA G ON C.IDGAMA = G.ID         -- UNE LA TABLA GAMA PARA OBTENER EL NIVEL DE GAMA 

JOIN  

    ESTADO E ON C.IDESTADO = E.ID     -- UNE LA TABLA ESTADO PARA OBTENER EL ESTADO DEL COCHE 

JOIN  

    COMBUSTIBLE CO ON C.IDCOMBUSTIBLE = CO.ID  -- UNE LA TABLA COMBUSTIBLE PARA OBTENER EL TIPO DE COMBUSTIBLE 
GO
/****** Object:  Table [dbo].[COMPRADOR]    Script Date: 14/03/2025 12:53:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[COMPRADOR](
	[ID] [int] NOT NULL,
	[IDUSUARIO] [int] NULL,
	[NOMBRE] [nvarchar](50) NOT NULL,
	[APELLIDOS] [nvarchar](50) NOT NULL,
	[DNI] [nvarchar](50) NOT NULL,
	[CARNET] [nvarchar](50) NOT NULL,
	[TELEFONO] [nvarchar](50) NOT NULL,
	[FECHANACIMIENTO] [date] NOT NULL,
	[MONEDERO] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ESTADORESERVA]    Script Date: 14/03/2025 12:53:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ESTADORESERVA](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ESTADO] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RESERVA]    Script Date: 14/03/2025 12:53:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RESERVA](
	[ID] [int] NOT NULL,
	[IDCOCHE] [int] NULL,
	[IDUSUARIO] [int] NULL,
	[FECHAINICIO] [date] NOT NULL,
	[FECHAFIN] [date] NOT NULL,
	[IDESTADORESERVA] [int] NULL,
	[PRECIO] [decimal](10, 2) NOT NULL,
	[KILOMETRAJE] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ROL]    Script Date: 14/03/2025 12:53:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ROL](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ROL] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USUARIO]    Script Date: 14/03/2025 12:53:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USUARIO](
	[ID] [int] NOT NULL,
	[NOMBRE] [nvarchar](50) NOT NULL,
	[EMAIL] [nvarchar](50) NOT NULL,
	[PASSWORD] [nvarchar](50) NOT NULL,
	[IDROL] [int] NULL,
	[SALT] [nvarchar](50) NOT NULL,
	[PASS] [varbinary](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VENDEDOR]    Script Date: 14/03/2025 12:53:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VENDEDOR](
	[ID] [int] NOT NULL,
	[IDUSUARIO] [int] NULL,
	[NOMBREEMPRESA] [nvarchar](50) NOT NULL,
	[DIRECCION] [nvarchar](50) NULL,
	[TELEFONO] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[COCHE] ([ID], [MARCA], [MODELO], [MATRICULA], [IMAGEN], [ASIENTOS], [IDMARCHAS], [IDGAMA], [IDESTADO], [KILOMETRAJE], [PUERTAS], [IDCOMBUSTIBLE], [IDVENDEDOR], [PRECIOKILOMETROS], [PRECIOILIMITADO]) VALUES (1, N'Audi', N'A1 Sportback', N'2345tkp', N'1audi-a1-sportback.png', 5, 2, 2, 1, 0, 5, 3, 1, CAST(23.54 AS Decimal(10, 2)), CAST(27.54 AS Decimal(10, 2)))
INSERT [dbo].[COCHE] ([ID], [MARCA], [MODELO], [MATRICULA], [IMAGEN], [ASIENTOS], [IDMARCHAS], [IDGAMA], [IDESTADO], [KILOMETRAJE], [PUERTAS], [IDCOMBUSTIBLE], [IDVENDEDOR], [PRECIOKILOMETROS], [PRECIOILIMITADO]) VALUES (2, N'Audi ', N'A6 Avant', N'1234rsz', N'2audi-a6-avant.png', 5, 2, 2, 1, 0, 5, 2, 1, CAST(33.52 AS Decimal(10, 2)), CAST(36.76 AS Decimal(10, 2)))
INSERT [dbo].[COCHE] ([ID], [MARCA], [MODELO], [MATRICULA], [IMAGEN], [ASIENTOS], [IDMARCHAS], [IDGAMA], [IDESTADO], [KILOMETRAJE], [PUERTAS], [IDCOMBUSTIBLE], [IDVENDEDOR], [PRECIOKILOMETROS], [PRECIOILIMITADO]) VALUES (3, N'Peugeot', N'208', N'8754RBM', N'3peugeot-208.png', 5, 1, 1, 1, 0, 5, 3, 1, CAST(12.00 AS Decimal(10, 2)), CAST(14.66 AS Decimal(10, 2)))
INSERT [dbo].[COCHE] ([ID], [MARCA], [MODELO], [MATRICULA], [IMAGEN], [ASIENTOS], [IDMARCHAS], [IDGAMA], [IDESTADO], [KILOMETRAJE], [PUERTAS], [IDCOMBUSTIBLE], [IDVENDEDOR], [PRECIOKILOMETROS], [PRECIOILIMITADO]) VALUES (4, N'Mini ', N'Cooper Cabrio', N'1234cdt', N'4mini-cooper-cabrio.png', 4, 2, 3, 1, 2652, 3, 3, 1, CAST(89.54 AS Decimal(10, 2)), CAST(93.45 AS Decimal(10, 2)))
INSERT [dbo].[COCHE] ([ID], [MARCA], [MODELO], [MATRICULA], [IMAGEN], [ASIENTOS], [IDMARCHAS], [IDGAMA], [IDESTADO], [KILOMETRAJE], [PUERTAS], [IDCOMBUSTIBLE], [IDVENDEDOR], [PRECIOKILOMETROS], [PRECIOILIMITADO]) VALUES (5, N'Mini ', N'Countryman', N'5678plj', N'5mini-countryman.png', 5, 2, 3, 1, 0, 5, 1, 1, CAST(111.43 AS Decimal(10, 2)), CAST(136.76 AS Decimal(10, 2)))
INSERT [dbo].[COCHE] ([ID], [MARCA], [MODELO], [MATRICULA], [IMAGEN], [ASIENTOS], [IDMARCHAS], [IDGAMA], [IDESTADO], [KILOMETRAJE], [PUERTAS], [IDCOMBUSTIBLE], [IDVENDEDOR], [PRECIOKILOMETROS], [PRECIOILIMITADO]) VALUES (6, N'Citroen', N'c3-e', N'5646KJH', N'6citroen-c3-e.png', 5, 1, 2, 1, 0, 5, 1, 1, CAST(21.43 AS Decimal(10, 2)), CAST(26.43 AS Decimal(10, 2)))
GO
SET IDENTITY_INSERT [dbo].[COMBUSTIBLE] ON 

INSERT [dbo].[COMBUSTIBLE] ([ID], [TIPO]) VALUES (2, N'Diesel')
INSERT [dbo].[COMBUSTIBLE] ([ID], [TIPO]) VALUES (1, N'Eléctrico')
INSERT [dbo].[COMBUSTIBLE] ([ID], [TIPO]) VALUES (3, N'Gasolina')
SET IDENTITY_INSERT [dbo].[COMBUSTIBLE] OFF
GO
INSERT [dbo].[COMPRADOR] ([ID], [IDUSUARIO], [NOMBRE], [APELLIDOS], [DNI], [CARNET], [TELEFONO], [FECHANACIMIENTO], [MONEDERO]) VALUES (3, 4, N'Pedro', N'Jimenez Parra', N'51026145R', N'51026145R', N'612345678', CAST(N'2000-11-11' AS Date), CAST(0.00 AS Decimal(10, 2)))
GO
SET IDENTITY_INSERT [dbo].[ESTADO] ON 

INSERT [dbo].[ESTADO] ([ID], [ESTADO]) VALUES (2, N'Alquilado')
INSERT [dbo].[ESTADO] ([ID], [ESTADO]) VALUES (1, N'Libre')
SET IDENTITY_INSERT [dbo].[ESTADO] OFF
GO
SET IDENTITY_INSERT [dbo].[ESTADORESERVA] ON 

INSERT [dbo].[ESTADORESERVA] ([ID], [ESTADO]) VALUES (1, N'Activo')
INSERT [dbo].[ESTADORESERVA] ([ID], [ESTADO]) VALUES (3, N'Espera')
INSERT [dbo].[ESTADORESERVA] ([ID], [ESTADO]) VALUES (2, N'Finalizado')
SET IDENTITY_INSERT [dbo].[ESTADORESERVA] OFF
GO
SET IDENTITY_INSERT [dbo].[GAMA] ON 

INSERT [dbo].[GAMA] ([ID], [NIVEL]) VALUES (3, N'Alta')
INSERT [dbo].[GAMA] ([ID], [NIVEL]) VALUES (1, N'Baja')
INSERT [dbo].[GAMA] ([ID], [NIVEL]) VALUES (2, N'Media')
SET IDENTITY_INSERT [dbo].[GAMA] OFF
GO
SET IDENTITY_INSERT [dbo].[MARCHAS] ON 

INSERT [dbo].[MARCHAS] ([ID], [TIPO]) VALUES (2, N'Automático')
INSERT [dbo].[MARCHAS] ([ID], [TIPO]) VALUES (1, N'Manual')
SET IDENTITY_INSERT [dbo].[MARCHAS] OFF
GO
INSERT [dbo].[RESERVA] ([ID], [IDCOCHE], [IDUSUARIO], [FECHAINICIO], [FECHAFIN], [IDESTADORESERVA], [PRECIO], [KILOMETRAJE]) VALUES (1, 3, 4, CAST(N'2025-03-14' AS Date), CAST(N'2025-03-18' AS Date), 1, CAST(60.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[RESERVA] ([ID], [IDCOCHE], [IDUSUARIO], [FECHAINICIO], [FECHAFIN], [IDESTADORESERVA], [PRECIO], [KILOMETRAJE]) VALUES (2, 5, 4, CAST(N'2025-03-16' AS Date), CAST(N'2025-03-17' AS Date), 3, CAST(222.86 AS Decimal(10, 2)), 1)
INSERT [dbo].[RESERVA] ([ID], [IDCOCHE], [IDUSUARIO], [FECHAINICIO], [FECHAFIN], [IDESTADORESERVA], [PRECIO], [KILOMETRAJE]) VALUES (3, 4, 4, CAST(N'2025-03-14' AS Date), CAST(N'2025-03-14' AS Date), 2, CAST(89.54 AS Decimal(10, 2)), 1)
INSERT [dbo].[RESERVA] ([ID], [IDCOCHE], [IDUSUARIO], [FECHAINICIO], [FECHAFIN], [IDESTADORESERVA], [PRECIO], [KILOMETRAJE]) VALUES (4, 2, 4, CAST(N'2025-03-24' AS Date), CAST(N'2025-03-27' AS Date), 3, CAST(134.08 AS Decimal(10, 2)), 1)
GO
SET IDENTITY_INSERT [dbo].[ROL] ON 

INSERT [dbo].[ROL] ([ID], [ROL]) VALUES (2, N'Comprador')
INSERT [dbo].[ROL] ([ID], [ROL]) VALUES (1, N'Vendedor')
SET IDENTITY_INSERT [dbo].[ROL] OFF
GO
INSERT [dbo].[USUARIO] ([ID], [NOMBRE], [EMAIL], [PASSWORD], [IDROL], [SALT], [PASS]) VALUES (1, N'Rentking', N'rentking@gmail.com', N'123', 1, N'VvàØqU"Ù 0ÖDõÁ)Oå»ÔWkÙ~=ILøÊour8uûÝl¶"L', 0x18BA94474EA1BB1B6D5CD283011AB5E2A2C584ECBE11D9F9B528EF6373649B99524E73026ADEB8BB165B686D17875C8DA53EE07383FF6321BBC1A8E3F71193BB)
INSERT [dbo].[USUARIO] ([ID], [NOMBRE], [EMAIL], [PASSWORD], [IDROL], [SALT], [PASS]) VALUES (4, N'Pedro', N'pedro@gmail.com', N'1234', 2, N'ljXgmifsNWajbnlEL6hjVSuqR+GsgGqb9eh98gN+ow4=', 0xA027872F76CCE92C1ADBF01EB2829EF3ADA6744BE943A3C002589A76935CC027368DBA73FA0B7B5B08368FE9DF2AADA6C4C2DB179529B88567F0F9AB84751EF7)
GO
INSERT [dbo].[VENDEDOR] ([ID], [IDUSUARIO], [NOMBREEMPRESA], [DIRECCION], [TELEFONO]) VALUES (1, 1, N'Rentking', N'Calle Copa Cabana', N'613525477')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__COCHE__46A2F688788F8BF5]    Script Date: 14/03/2025 12:53:15 ******/
ALTER TABLE [dbo].[COCHE] ADD UNIQUE NONCLUSTERED 
(
	[MATRICULA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__COMBUSTI__B6FCAAA2B86447CF]    Script Date: 14/03/2025 12:53:15 ******/
ALTER TABLE [dbo].[COMBUSTIBLE] ADD UNIQUE NONCLUSTERED 
(
	[TIPO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_CARNET]    Script Date: 14/03/2025 12:53:15 ******/
ALTER TABLE [dbo].[COMPRADOR] ADD  CONSTRAINT [UQ_CARNET] UNIQUE NONCLUSTERED 
(
	[CARNET] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_DNI]    Script Date: 14/03/2025 12:53:15 ******/
ALTER TABLE [dbo].[COMPRADOR] ADD  CONSTRAINT [UQ_DNI] UNIQUE NONCLUSTERED 
(
	[DNI] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_TELEFONO]    Script Date: 14/03/2025 12:53:15 ******/
ALTER TABLE [dbo].[COMPRADOR] ADD  CONSTRAINT [UQ_TELEFONO] UNIQUE NONCLUSTERED 
(
	[TELEFONO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__ESTADO__541A11CCCB8EF8A3]    Script Date: 14/03/2025 12:53:15 ******/
ALTER TABLE [dbo].[ESTADO] ADD UNIQUE NONCLUSTERED 
(
	[ESTADO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__ESTADORE__541A11CC24992B55]    Script Date: 14/03/2025 12:53:15 ******/
ALTER TABLE [dbo].[ESTADORESERVA] ADD UNIQUE NONCLUSTERED 
(
	[ESTADO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__GAMA__A2B1AB5C46BC3F51]    Script Date: 14/03/2025 12:53:15 ******/
ALTER TABLE [dbo].[GAMA] ADD UNIQUE NONCLUSTERED 
(
	[NIVEL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__MARCHAS__B6FCAAA250A05E67]    Script Date: 14/03/2025 12:53:15 ******/
ALTER TABLE [dbo].[MARCHAS] ADD UNIQUE NONCLUSTERED 
(
	[TIPO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__ROL__CAFF71FC848D78F9]    Script Date: 14/03/2025 12:53:15 ******/
ALTER TABLE [dbo].[ROL] ADD UNIQUE NONCLUSTERED 
(
	[ROL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__USUARIO__161CF7244965DE5C]    Script Date: 14/03/2025 12:53:15 ******/
ALTER TABLE [dbo].[USUARIO] ADD UNIQUE NONCLUSTERED 
(
	[EMAIL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[COCHE]  WITH CHECK ADD FOREIGN KEY([IDCOMBUSTIBLE])
REFERENCES [dbo].[COMBUSTIBLE] ([ID])
GO
ALTER TABLE [dbo].[COCHE]  WITH CHECK ADD FOREIGN KEY([IDESTADO])
REFERENCES [dbo].[ESTADO] ([ID])
GO
ALTER TABLE [dbo].[COCHE]  WITH CHECK ADD FOREIGN KEY([IDGAMA])
REFERENCES [dbo].[GAMA] ([ID])
GO
ALTER TABLE [dbo].[COCHE]  WITH CHECK ADD FOREIGN KEY([IDMARCHAS])
REFERENCES [dbo].[MARCHAS] ([ID])
GO
ALTER TABLE [dbo].[COCHE]  WITH CHECK ADD FOREIGN KEY([IDVENDEDOR])
REFERENCES [dbo].[VENDEDOR] ([ID])
GO
ALTER TABLE [dbo].[COMPRADOR]  WITH CHECK ADD FOREIGN KEY([IDUSUARIO])
REFERENCES [dbo].[USUARIO] ([ID])
GO
ALTER TABLE [dbo].[USUARIO]  WITH CHECK ADD FOREIGN KEY([IDROL])
REFERENCES [dbo].[ROL] ([ID])
GO
ALTER TABLE [dbo].[VENDEDOR]  WITH CHECK ADD FOREIGN KEY([IDUSUARIO])
REFERENCES [dbo].[USUARIO] ([ID])
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERT_COCHE]    Script Date: 14/03/2025 12:53:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE [dbo].[SP_INSERT_COCHE]
(@MARCA NVARCHAR(50),@MODELO NVARCHAR(50),@MATRICULA NVARCHAR(50),@IMAGEN NVARCHAR(50),
@ASIENTOS INT,@IDMARCHAS INT,@IDGAMA INT,@KILOMETRAJE INT,@PUERTAS INT,@IDCOMBUSTIBLE INT,
@IDVENDEDOR INT,@PRECIOKILOMETROS DECIMAL(10,2),@PRECIOILIMITADO DECIMAL(10,2))
AS
BEGIN
    DECLARE @NEW_ID INT;

    -- Obtener el máximo ID de la tabla COCHE y sumarle 1
    SELECT @NEW_ID = ISNULL(MAX(ID), 0) + 1 FROM COCHE;

    -- Insertar el nuevo coche con el ID calculado
    INSERT INTO COCHE (ID, MARCA, MODELO, MATRICULA, IMAGEN, ASIENTOS, IDMARCHAS, IDGAMA, IDESTADO,
                       KILOMETRAJE, PUERTAS, IDCOMBUSTIBLE, IDVENDEDOR, PRECIOKILOMETROS, PRECIOILIMITADO)
    VALUES (@NEW_ID, @MARCA, @MODELO, @MATRICULA, @IMAGEN, @ASIENTOS, @IDMARCHAS, @IDGAMA, 1,
            @KILOMETRAJE, @PUERTAS, @IDCOMBUSTIBLE, @IDVENDEDOR, @PRECIOKILOMETROS, @PRECIOILIMITADO);
END;
GO

