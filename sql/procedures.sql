USE Proj_Interdisciplinar

--PROCEDURES

--USUARIOS

-- INCLUI
CREATE PROCEDURE spIncluiUsuario(
    @nome varchar(max),
    @cpf varchar(15),
    @email varchar(max),
    @senha varchar(30),
    @tipoUsuario varchar(13),
    @username varchar(20)
)
AS
BEGIN
    INSERT INTO Usuarios
        (nome, cpf, email, senha, tipoUsuario, username)
    VALUES
        (@nome, @cpf, @email, @senha, @tipoUsuario, @username)
END
GO

-- ALTERA
CREATE PROCEDURE spAlteraUsuario(
    @nome varchar(max),
    @cpf varchar(15),
    @email varchar(max),
    @senha varchar(30),
    @tipoUsuario varchar(13),
    @username varchar(20)
)
AS
BEGIN
    UPDATE Usuarios SET
    nome = @nome,
    cpf = @cpf,
    email = @email,
    senha = @senha,
    tipoUsuario = @tipoUsuario,
    username = @username
    where cpf = @cpf
END
GO

-- EXCLUIR
CREATE PROCEDURE spExcluiUsuario(
    @id int
)
AS
BEGIN
    DELETE FROM Usuarios 
    WHERE id = @id
END
GO

--CONSULTA PARA VALIDACAO DE LOGIN
Create PROCEDURE spConsultaUsuario(
    @username varchar(20),
    @senha varchar(30)
)
AS
BEGIN
    SELECT *
    FROM Usuarios
    WHERE username LIKE @username
    AND senha LIKE @senha
END
GO

-- LISTAGEM DE TODOS OS USUARIOS

CREATE PROCEDURE spListaTodosUsuarios
AS
BEGIN
    SELECT *
    FROM Usuarios
END
GO

--LISTAGEM DE TODOS OS CLIENTES

CREATE PROCEDURE spListaClientes
AS
BEGIN
    SELECT *
    FROM Usuarios
    WHERE tipoUsuario LIKE 'Cliente'
    ORDER BY nome
END
GO

--LISTAGEM DE TODOS OS ADMINISTRADORES

CREATE PROCEDURE spListaAdministradores
AS
BEGIN
    SELECT *
    FROM Usuarios
    WHERE tipoUsuario LIKE 'Administrador'
    ORDER BY nome
END
GO

--CONSULTA PELO ID

CREATE PROCEDURE spConsultaId(
    @id int
)
AS
BEGIN
    SELECT *
    FROM Usuarios
    WHERE id = @id
END
GO

--PRODUTOS-------------------------

-- INCLUI
CREATE PROCEDURE spIncluiProduto(
    @nome VARCHAR (MAX),
    @info_nutricional VARCHAR (MAX),
    @alergia VARCHAR (MAX),
    @preco REAL,
    @unidade VARCHAR (MAX),
    @id_cat INT,
    @imagem VARBINARY (MAX)
)
AS
BEGIN
    INSERT INTO Produtos
        (nome, info_nutricional, alergia, preco, unidade, id_cat, imagem)
    VALUES
        (@nome, @info_nutricional, @alergia, @preco, @unidade, @id_cat, @imagem)
END
GO

-- ALTERA
CREATE PROCEDURE spAlteraProduto(
    @id int,
    @nome VARCHAR (MAX),
    @info_nutricional VARCHAR (MAX),
    @alergia VARCHAR (MAX),
    @preco REAL,
    @unidade VARCHAR (MAX),
    @id_cat INT,
    @imagem VARBINARY (MAX)
)
AS
BEGIN
    UPDATE Produtos SET
    nome = @nome,
    info_nutricional = @info_nutricional,
    alergia = @alergia,
    preco = @preco,
    unidade = @unidade,
    id_cat = @id_cat,
    imagem = @imagem
    where id = @id
END
GO

-- EXCLUI
CREATE PROCEDURE spExcluiProduto(
    @id int
)
AS
BEGIN
    DELETE FROM Produtos 
    WHERE id = @id
END
GO

--CONSULTA PELO ID
CREATE PROCEDURE spConsultaProduto(
    @id int
)
AS
BEGIN
    SELECT *
    FROM Produtos
    WHERE id = @id
END
GO

-- LISTAGEM DE PRODUTOS
CREATE PROCEDURE spListaProduto
AS
BEGIN
    SELECT p.id, p.nome, p.info_nutricional, p.alergia, p.preco, p.unidade, c.nome as 'categoria', p.imagem
    FROM Produtos p, Categorias c
	WHERE p.id_cat = c.id_cat;
END
GO

--CONSULTA ID PRODUTO
CREATE PROCEDURE spConsultaIdProduto(
    @id int
)
AS
BEGIN
    SELECT p.id, p.nome, p.info_nutricional, p.alergia, p.preco, p.unidade, c.nome as 'categoria', p.imagem
    FROM Produtos p, Categorias c
    WHERE p.id = @id AND c.id_cat = p.id_cat
END
GO
--LISTAGEM AVAN�ADA DE PRODUTOS
CREATE PROCEDURE spConsultaAvancadaProdutos(
		@nome VARCHAR(MAX),
		@id_cat int,
		@unidade VARCHAR(MAX),
		@alergia VARCHAR(MAX)
)
AS
BEGIN
	SELECT p.id, p.nome, p.info_nutricional, p.alergia, p.preco, c.nome as 'categoria', p.unidade, p.imagem
	FROM Produtos p, Categorias c
	WHERE 
	c.id_cat = @id_cat AND
	c.id_cat = p.id_cat AND
	p.nome LIKE @nome AND
	p.alergia LIKE @alergia AND
	p.unidade LIKE @unidade;
END
GO
--CATEGORIAS----------------------
-- INCLUI
CREATE PROCEDURE spIncluiCategoria
    (
    @nome varchar(50)
)
AS
BEGIN
    INSERT INTO Categorias
        (nome)
    VALUES
        (@nome)
END
GO

-- ALTERA
CREATE PROCEDURE spAlteraCategoria(
    @id_cat int,
    @nome varchar(max)
)
AS
BEGIN
    UPDATE Categorias SET
    nome = @nome
    where id_cat = @id_cat
END
GO

-- EXCLUI
CREATE PROCEDURE spExcluiCategoria(
    @id_cat int
)
AS
BEGIN
    DELETE FROM Categorias 
    WHERE id_cat = @id_cat
END
GO

-- CONSULTA NOME CATEGORIAS > CONSULTA AVAN�ADA
CREATE PROCEDURE spConsultaCategoriaNome(
    @id_cat INT
)
AS
BEGIN
    SELECT nome
    FROM Categorias
    WHERE id_cat = @id_cat
END
GO
-- CONSULTA CATEGORIAS
CREATE PROCEDURE spConsultaCategoria(
    @id_cat INT
)
AS
BEGIN
    SELECT *
    FROM Categorias
    WHERE id_cat = @id_cat
END
GO

-- LISTAGEM DE CATEGORIAS
CREATE PROCEDURE spListaCategoria
AS
BEGIN
    SELECT *
    FROM Categorias
END
GO
