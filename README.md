# WPF_MVVM_StructurDesign



CREATE TABLE [dbo].[Table_Items] (
    [Id]           INT          IDENTITY (1, 1) NOT NULL,
    [NamaBarang]   VARCHAR (30) NOT NULL,
    [JumlahBarang] VARCHAR (30) NOT NULL,
    [HargaBarang]  VARCHAR (30) NOT NULL,
    [TanggalMasuk] DATETIME     NOT NULL,
    CONSTRAINT [pk_Items] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Table_Login2] (
    [IdLogin]  INT          IDENTITY (1, 1) NOT NULL,
    [Username] VARCHAR (30) NOT NULL,
    [Pass]     VARCHAR (40) NOT NULL,
    CONSTRAINT [PK_Table_Login] PRIMARY KEY CLUSTERED ([IdLogin] ASC, [Username] ASC)
);


create procedure [DeleteItemById]
(
	@Id int
)
as delete from Table_Items where Id=@Id

CREATE procedure InsertItemById
(
	@Id int,
	@NamaBarang varchar(30),
	@JumlahBarang varchar(30),
	@HargaBarang varchar(30),
	@TanggalMasuk datetime
)
as insert into Table_Items(Id,NamaBarang,JumlahBarang,HargaBarang,TanggalMasuk) values(@Id,@NamaBarang,@JumlahBarang,@HargaBarang,@TanggalMasuk)

CREATE procedure InsertItemPair
(
	@Username varchar(50),
	@Pass varchar(50)
)
as insert into Table_Login2 values(@Username,@pass)

create procedure InsertItems
(
	@NamaBarang varchar(30),
	@JumlahBarang varchar(30),
	@HargaBarang varchar(30),
	@TanggalMasuk datetime
)
as insert into Table_Items(NamaBarang,JumlahBarang,HargaBarang,TanggalMasuk) values(@NamaBarang,@JumlahBarang,@HargaBarang,@TanggalMasuk)

create procedure SelectAllItems
as select * from Table_Items

create procedure SelectItemById
(
	@Id int
)
as select * from Table_Items where Id=@Id

create procedure SelectItemByInputDate
(
	@TanggalMasuk1 varchar(30),
	@TanggalMasuk2 varchar(30)
)
as select * from Table_Items where TanggalMasuk >= @TanggalMasuk1 and  TanggalMasuk <= @TanggalMasuk2

create procedure SelectItemByNama
(
	@NamaBarang varchar(30)
)
as select * from Table_Items where NamaBarang like @NamaBarang

CREATE procedure SelectItemByParams
(
	@NamaBarang varchar(30),
	@JumlahBarang varchar(30),
	@HargaBarang varchar(30)
)
as select * from Table_Items where NamaBarang like @NamaBarang or  JumlahBarang = @JumlahBarang or  HargaBarang = @HargaBarang

CREATE procedure SelectItemByUser
(
	@Username varchar(50)
)
as select * from Table_Login2 where Username=@Username

create procedure UpdateItemById
(
	@Id int,
	@NamaBarang varchar(30),
	@JumlahBarang varchar(30),
	@HargaBarang varchar(30),
	@TanggalMasuk datetime
)
as update Table_Items set NamaBarang=@NamaBarang,JumlahBarang=@JumlahBarang,HargaBarang=@HargaBarang,TanggalMasuk=@TanggalMasuk where Id=@Id
