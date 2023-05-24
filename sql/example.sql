INSERT INTO Usuarios
    (nome, cpf, email, senha, tipoUsuario, username)
VALUES
    ('Isa', '12345678901', 'isa@email.com', '123123', 'Cliente', 'isa.couto')

INSERT INTO Usuarios
    (nome, cpf, email, senha, tipoUsuario, username)
VALUES
    ('Vóvó', '23546598714', 'vovo@ig.com', '123123', 'Administrador', 'Vovo')

INSERT INTO Produtos
    (nome, info_nutricional, alergia, preco, unidade, id_cat, imagem)
VALUES
    ('Pão francês', 'O pão francês é feito com farinha de trigo, água, fermento e sal. É uma excelente fonte de carboidratos, mas deve ser consumido com moderação devido ao seu teor calórico.', 'Glúten', 2.49, 'unidade', 1, ''),
    ('Pão de queijo', 'O pão de queijo é uma delícia típica brasileira feita com polvilho, queijo, ovos e óleo. É uma excelente fonte de carboidratos, proteínas e gorduras saudáveis.', 'Lactose', 5.99, 'unidade', 1, NULL),
    ('Croissant', 'O croissant é um pão de massa folhada que contém farinha, manteiga e açúcar. É rico em calorias, gorduras e carboidratos.', 'Glúten, lactose', 4.49, 'unidade', 1, NULL),
    ('Bolo de cenoura', 'O bolo de cenoura é uma sobremesa macia e saborosa feita com cenoura ralada, farinha de trigo, açúcar, ovos e óleo vegetal. É uma boa fonte de carboidratos e fibras.', 'Glúten', 7.99, 'unidade', 2, NULL),
    ('Baguete', 'A baguete é um pão tradicional francês com uma casca crocante e miolo macio. É um ótimo acompanhamento para sanduíches e refeições.', 'Glúten', 3.99, 'unidade', 1, NULL);

INSERT INTO Categorias
    (nome)
VALUES
    ('Salgado'),
    ('Doce')