CREATE TABLE Cardapio (
idItem MEDIUMINT UNSIGNED PRIMARY KEY NOT NULL AUTO_INCREMENT,
Nome VARCHAR(45) NOT NULL,
Categoria TINYINT UNSIGNED NOT NULL,
IngredientesDesc VARCHAR(255),
Valor DECIMAL(15,2) UNSIGNED NOT NULL);

CREATE TABLE Setores (
idSetor TINYINT UNSIGNED PRIMARY KEY NOT NULL AUTO_INCREMENT,
Nome VARCHAR(45) NOT NULL);

CREATE TABLE Colaboradores (
idColaborador MEDIUMINT UNSIGNED PRIMARY KEY NOT NULL AUTO_INCREMENT,
idSetor TINYINT UNSIGNED NOT NULL REFERENCES Setores(idSetor),
Nome VARCHAR(45) NOT NULL,
Senha VARCHAR(128) NOT NULL);

CREATE TABLE Clientes (
idCliente MEDIUMINT UNSIGNED PRIMARY KEY NOT NULL AUTO_INCREMENT,
Nome VARCHAR(45) NOT NULL,
Mesa SMALLINT UNSIGNED,
Data DATETIME NOT NULL,
Fechado BOOL NOT NULL DEFAULT false);

CREATE TABLE Pedidos (
idPedido INT UNSIGNED PRIMARY KEY NOT NULL AUTO_INCREMENT,
idItem MEDIUMINT UNSIGNED NOT NULL REFERENCES CardapioPratos(idPrato),
idCliente MEDIUMINT UNSIGNED NOT NULL REFERENCES Clientes(idCliente),
Quantidade SMALLINT UNSIGNED NOT NULL,
Status TINYINT UNSIGNED NOT NULL DEFAULT 1);


-- entradas
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Pão australiano', 1, 'Pão australiano em fatias', 2.59);
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Onion rings', 1, 'Rodelas de cebola empanadas', 5.00);
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Fritas', 1, 'Batatas fritas rústicas ou laminadas', 8.00);
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Amendoim e castanhas', 1, 'Amendoim e castanhas com sal', 10.00);
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Fondue', 1, 'Queijo fondue derretido', 10.00);
-- executivos
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Almoço executivo', 2, 'Delicioso bife acebolado', 20.00);
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Tropeiro', 2, 'Mineiro e caprichado', 20.00);
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Mexidão', 2, 'Mineiro e caprichado', 20.00);
-- burgers
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Zé Burger', 3, 'Pão, hamburger bovino 120g, picles, alface e tomate', 25.00);
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Chicken Burger', 3, 'Pão, hamburger de frango 120g, batata palha, maionese', 22.00);
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Fish Burger', 3, 'Pão, hambuger de tilápia 120g, molho de camarão', 32.00);
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Ultra Burger', 3, 'Pão, 2 hambugeres bovinos de 80g cada, 4 fatias de cheddar, bacon, ovo', 39.00);
-- asiaticos
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Combinado 30 peças', 4, 'sushis salmão, atum, truta salmonada', 32.00);
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Box executivo', 4, 'Frango xadrez e arroz', 35.00);
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Temaki 4 peças', 4, 'Salmão, cream cheese, cebolinha', 44.00);
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Hot philadelphia 20 peças', 4, 'sushis empanados e marinados', 64.00);
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Ramen', 4, 'Delicioso e autêntico ramen', 32.00);
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Onigiri', 4, 'Arroz com variados recheios', 32.00);
-- massas
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Lasanha bolonhesa', 5, 'Lasanha bolonhesa, molho ao sugo', 22.00);
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Risoto', 5, 'Arroz refogado no queijo', 23.00);
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Macarronada', 5, 'Espaguete ao molho bolonhesa ou branco', 23.00);
-- sobremesas
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Frutas da época', 6, 'Cesta de frutos', 10.00);
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Petit gateau', 6, 'Sorvete frio com brownie quente', 12.00);
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Açaí', 6, 'Com leite ninho, granola, calda', 15.00);
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Pudim', 6, 'De leite condensado, chocolate, nata', 15.00);
-- bebidas, sem alcool
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Refrigerante lata', 7, 'Coca, fanta, pepsi', 2.00);
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Água mineral sem gás 500ml', 7, 'Água de fontes naturais', 3.00);
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Água mineral com gás 500ml', 7, 'Água de fontes naturais', 4.00);
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Suco de caixinha', 7, 'Laranja, pêssego, uva, caju', 4.00);
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Limonada', 7, 'Limão expremido', 6.00);
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Suco natural de laranja', 7, 'Laranja expremida na hora', 8.00);
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Suco integral de uva', 7, '100% suco de uva', 8.00);
-- bebidas, com alcool
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Long neck nacional', 8, 'Brahma, Skol', 5.00);
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Long neck importada', 8, 'Stella Artois, Heineken, Eisenbahn', 10.00);
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Vinho tinto seco', 8, 'Cabernet sauvignon', 20.00);
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Champagne', 8, 'Chateau blanc', 44.00);
INSERT INTO Cardapio (Nome, Categoria, IngredientesDesc, Valor) VALUES ('Coqueteis', 8, 'Sex in the beach, piña colada, bloody mary', 60.00);

-- setores
INSERT INTO Setores (Nome) VALUES ('Admin');
INSERT INTO Setores (Nome) VALUES ('Cozinha');
INSERT INTO Setores (Nome) VALUES ('Copa');
INSERT INTO Setores (Nome) VALUES ('Garçom');

-- colaboradores
-- senha admin
INSERT INTO Colaboradores (idSetor, Nome, Senha) VALUES (1, 'Jose da Silva Admin', 'c7ad44cbad762a5da0a452f9e854fdc1e0e7a52a38015f23f3eab1d80b931dd472634dfac71cd34ebc35d16ab7fb8a90c81f975113d6c7538dc69dd8de9077ec');
-- senha panela
INSERT INTO Colaboradores (idSetor, Nome, Senha) VALUES (2, 'Joao o Cozinheiro', 'df219b8be0db1dd7e4c784bbd26c7f52b54e3ccc51862d483288260f71ed85c51376eabaab2e8d9960e273f10dd0ac4b2836d983d2a707cd9e3eced15d21cd7a');
-- senha drink
INSERT INTO Colaboradores (idSetor, Nome, Senha) VALUES (3, 'Maria a Copeira', '9ef36591b6b288aa92f55edb9d519d7e9da3141a7159ecef87841817b0b4290aa6e46241b8888c53d3dfeb587b517a8699627c68335d530daf6b6d5576e28db9');
-- senha servir
INSERT INTO Colaboradores (idSetor, Nome, Senha) VALUES (4, 'Pedro o Garçom', '40f42aee01df5cd2683fcf9eb139a7dc69ae3c5a7a44e7b947668600f874482e2490e524d8a47fc1e31ba268b2915dc8207c6fd8238af51796c78ea4754a3f59');