create database PainelSenai;
GO

USE PainelSenai;

create table TipoUsuario(
idTipoUsuario int primary key identity,
nomeTipoUsuario varchar(20) not null
);
GO

create table Usuario(
idUsuario int primary key identity,
idTipoUsuario int foreign key references TipoUsuario(idTipoUsuario),
nomeUsuario varchar(500),
email varchar(500),
senha varchar(100)
);
GO

create table CadastrarCampanha(
idCampanha int primary key identity,
idUsuario int foreign key references Usuario(idUsuario),
nomeCampanha varchar(500),
dataInicio datetime,
dataFim datetime,
arquivo varchar(500),
descricao varchar(500),
campanhaAtiva bit
);
GO
