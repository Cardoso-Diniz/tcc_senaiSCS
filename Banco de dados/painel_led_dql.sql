USE PainelSenai;
GO

SELECT * FROM TipoUsuario;
SELECT * FROM Usuario;
SELECT * FROM CadastrarCampanha;

UPDATE CadastrarCampanha
SET campanhaAtiva = 1
WHERE idCampanha =1 ;

UPDATE CadastrarCampanha
SET campanhaAtiva = 0
WHERE idCampanha =2 ;