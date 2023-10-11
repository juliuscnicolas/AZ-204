/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [ProductId]
      ,[ProductName]
      ,[Quantity]
  FROM [AZ204].[dbo].[Products]

INSERT INTO [AZ204].[dbo].[Products] ([ProductName], [Quantity])
VALUES('Shoes', 2)

INSERT INTO [AZ204].[dbo].[Products] ([ProductName], [Quantity])
VALUES('Socks', 15)

INSERT INTO [AZ204].[dbo].[Products] ([ProductName], [Quantity])
VALUES('Pants', 9)