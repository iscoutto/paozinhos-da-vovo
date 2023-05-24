CREATE DATABASE Proj_Interdisciplinar

USE Proj_Interdisciplinar

CREATE TABLE Usuarios(
	id int primary key identity not null,
	nome varchar(50),
	email varchar(50),
	cpf varchar(15),
	tipoUsuario varchar(13),
	senha varchar(30),
	username varchar(30)
);

CREATE TABLE Categorias
(
    id_cat INTEGER PRIMARY KEY IDENTITY NOT NULL,
    nome VARCHAR(MAX)
);

CREATE TABLE Produtos (
  id INTEGER PRIMARY KEY identity not null,
  nome VARCHAR(40),
  info_nutricional VARCHAR(MAX),
  alergia VARCHAR(80),
  preco REAL,
  unidade VARCHAR(10),
  id_cat int,
  CONSTRAINT fk_categoria FOREIGN KEY (id_cat) REFERENCES Categorias (id_cat),
  imagem VARBINARY(MAX)
);

CREATE TABLE Pedidos (
  id INT PRIMARY KEY,
  id_usuario INT,
  data_hora DATETIME,
  status VARCHAR(MAX),
  FOREIGN KEY (id_usuario) REFERENCES Usuarios(id)
);

CREATE TABLE Carrinho (
  id_pedido INT,
  id_produto INT,
  quantidade INT,
  FOREIGN KEY (id_pedido) REFERENCES Pedidos(id),
  FOREIGN KEY (id_produto) REFERENCES Produtos(id)
);