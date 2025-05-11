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
('Tecnologia', 'Produtos relacionados � tecnologia, como gadgets e acess�rios', 'https://example.com/images/tecnologia.jpg', @paramDateNow, @paramDateNow, NULL, 1, 1),
('Moda', 'Roupas, acess�rios e tend�ncias de moda', 'https://example.com/images/moda.jpg', @paramDateNow, @paramDateNow, NULL, 1, 1),
('Livros', 'Livros de fic��o, n�o-fic��o, acad�micos e mais', 'https://example.com/images/livros.jpg', @paramDateNow, @paramDateNow, NULL, 1, 1),
('Esportes', 'Equipamentos e acess�rios esportivos', 'https://example.com/images/esportes.jpg', @paramDateNow, @paramDateNow, NULL, 1, 1),
('Alimenta��o', 'Produtos de alimenta��o e bebidas', 'https://example.com/images/alimentacao.jpg', @paramDateNow, @paramDateNow, NULL, 1, 1);