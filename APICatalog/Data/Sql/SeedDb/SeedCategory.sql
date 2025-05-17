declare @paramDateNow datetime; set @paramDateNow = getdate();

  INSERT INTO [Categories]
(
	[CategoryName],
	[Description],
	[ImageUrl],
	[CreationDate],
	[UpdateDate],
	[DeletionDate],
	[CreationUserId],
	[UpdateUserId]
)
VALUES
('Tecnologia', 'Produtos relacionados à tecnologia, como gadgets e acessórios', 'https://example.com/images/tecnologia.jpg', @paramDateNow, @paramDateNow, NULL, 1, 1),
('Moda', 'Roupas, acessórios e tendências de moda', 'https://example.com/images/moda.jpg', @paramDateNow, @paramDateNow, NULL, 1, 1),
('Livros', 'Livros de ficção, não-ficção, acadêmicos e mais', 'https://example.com/images/livros.jpg', @paramDateNow, @paramDateNow, NULL, 1, 1),
('Esportes', 'Equipamentos e acessórios esportivos', 'https://example.com/images/esportes.jpg', @paramDateNow, @paramDateNow, NULL, 1, 1),
('Alimentação', 'Produtos de alimentação e bebidas', 'https://example.com/images/alimentacao.jpg', @paramDateNow, @paramDateNow, NULL, 1, 1);