declare @paramDateNow datetime; set @paramDateNow = getdate();

INSERT INTO [Products]
(
	[ProductName],
	[Description],
	[ImageUrl],
	[Price],
	[Stock],
	[CreationDate],
	[UpdateDate],
	[CreationUserId],
	[UpdateUserId],
	[CategoryId]
)
VALUES
-- Categoria 1: Tecnologia
('Fone Bluetooth', 'Fone de ouvido sem fio de alta qualidade', 'https://example.com/product1.jpg', 199.99, 50, @paramDateNow, @paramDateNow, 1, 1, 1),
('Teclado Mec�nico', 'Teclado para gamers com ilumina��o RGB', 'https://example.com/product2.jpg', 349.90, 30, @paramDateNow, @paramDateNow, 1, 1, 1),

-- Categoria 2: Moda
('Jaqueta de Couro', 'Jaqueta estilosa para o inverno', 'https://example.com/product3.jpg', 499.00, 15, @paramDateNow, @paramDateNow, 1, 1, 2),
('T�nis Esportivo', 'T�nis confort�vel para caminhada e corrida', 'https://example.com/product4.jpg', 299.90, 40, @paramDateNow, @paramDateNow, 1, 1, 2),

-- Categoria 3: Livros
('O Poder do H�bito', 'Livro sobre desenvolvimento de h�bitos saud�veis', 'https://example.com/product5.jpg', 89.90, 100, @paramDateNow, @paramDateNow, 1, 1, 3),
('Clean Code', 'Livro sobre boas pr�ticas de programa��o', 'https://example.com/product6.jpg', 129.50, 80, @paramDateNow, @paramDateNow, 1, 1, 3),

-- Categoria 4: Esportes
('Bola de Futebol', 'Bola oficial tamanho 5 para partidas profissionais', 'https://example.com/product7.jpg', 150.00, 60, @paramDateNow, @paramDateNow, 1, 1, 4),
('Luvas de Boxe', 'Luvas acolchoadas para treinos e competi��es', 'https://example.com/product8.jpg', 230.00, 25, @paramDateNow, @paramDateNow, 1, 1, 4),

-- Categoria 5: Alimenta��o
('Caf� Gourmet 500g', 'Caf� premium torrado em gr�os', 'https://example.com/product9.jpg', 45.00, 70, @paramDateNow, @paramDateNow, 1, 1, 5),
('Kit de Temperos', 'Kit com 10 tipos de temperos naturais', 'https://example.com/product10.jpg', 79.90, 55, @paramDateNow, @paramDateNow, 1, 1, 5);
