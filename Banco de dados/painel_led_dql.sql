USE PainelSenai;
GO

INSERT INTO Usuario(nomeUsuario,email,senha)
VALUES  ('Juscelino','juscelino@teste.com','12345678'),
       ('Marketing','mkt1@teste.com','87654321');
GO

INSERT INTO Campanha(idUsuario,nomeCampanha,dataInicio,dataFim,imagem)
VALUES ('1','Abertura do curso','05/02/2023','19/05/2023','teste/img/teste.png'),
       ('2','Outubro rosa','02/10/2023','30/10/2023','teste/img/teste.png');
GO
