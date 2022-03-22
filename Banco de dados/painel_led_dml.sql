create database PainelSenai;
GO

create table Usuario(
idUsuario int primary key identity,
nomeUsuario varchar(500),
email varchar(500),
senha varchar(8)

);
GO

create table Campanha(
idCampanha int primary key identity,
idUsuario int foreign key references Usuario(idUsuario),
nomeCampanha varchar(500),
dataInicio datetime,
dataFim datetime,
imagem varchar(500),
);
GO
