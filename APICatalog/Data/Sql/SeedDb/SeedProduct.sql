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
('Teclado Mecânico', 'Teclado para gamers com iluminação RGB', 'https://example.com/product2.jpg', 349.90, 30, @paramDateNow, @paramDateNow, 1, 1, 1),

-- Categoria 2: Moda
('Jaqueta de Couro', 'Jaqueta estilosa para o inverno', 'https://example.com/product3.jpg', 499.00, 15, @paramDateNow, @paramDateNow, 1, 1, 2),
('Tênis Esportivo', 'Tênis confortável para caminhada e corrida', 'https://example.com/product4.jpg', 299.90, 40, @paramDateNow, @paramDateNow, 1, 1, 2),

-- Categoria 3: Livros
('O Poder do Hábito', 'Livro sobre desenvolvimento de hábitos saudáveis', 'https://example.com/product5.jpg', 89.90, 100, @paramDateNow, @paramDateNow, 1, 1, 3),
('Clean Code', 'Livro sobre boas práticas de programação', 'https://example.com/product6.jpg', 129.50, 80, @paramDateNow, @paramDateNow, 1, 1, 3),

-- Categoria 4: Esportes
('Bola de Futebol', 'Bola oficial tamanho 5 para partidas profissionais', 'https://example.com/product7.jpg', 150.00, 60, @paramDateNow, @paramDateNow, 1, 1, 4),
('Luvas de Boxe', 'Luvas acolchoadas para treinos e competições', 'https://example.com/product8.jpg', 230.00, 25, @paramDateNow, @paramDateNow, 1, 1, 4),

-- Categoria 5: Alimentação
('Café Gourmet 500g', 'Café premium torrado em grãos', 'https://example.com/product9.jpg', 45.00, 70, @paramDateNow, @paramDateNow, 1, 1, 5),
('Kit de Temperos', 'Kit com 10 tipos de temperos naturais', 'https://example.com/product10.jpg', 79.90, 55, @paramDateNow, @paramDateNow, 1, 1, 5);
