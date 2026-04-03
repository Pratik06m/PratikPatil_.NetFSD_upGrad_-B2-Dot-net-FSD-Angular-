CREATE TABLE Products (
    ProductId INT PRIMARY KEY IDENTITY(1,1), -- [cite: 238]
    ProductName VARCHAR(100), -- [cite: 238]
    Category VARCHAR(50), -- [cite: 238]
    Price DECIMAL(10,2) -- [cite: 238]
);
GO

-- Insert Stored Procedure [cite: 280]
CREATE PROCEDURE sp_InsertProduct
    @ProductName VARCHAR(100),
    @Category VARCHAR(50),
    @Price DECIMAL(10,2)
AS
BEGIN
    INSERT INTO Products (ProductName, Category, Price)
    VALUES (@ProductName, @Category, @Price);
END;
GO

-- Get All Stored Procedure [cite: 281]
CREATE PROCEDURE sp_GetAllProducts
AS
BEGIN
    SELECT ProductId, ProductName, Category, Price FROM Products;
END;
GO

-- Update Stored Procedure [cite: 282]
CREATE PROCEDURE sp_UpdateProduct
    @ProductId INT,
    @ProductName VARCHAR(100),
    @Category VARCHAR(50),
    @Price DECIMAL(10,2)
AS
BEGIN
    UPDATE Products
    SET ProductName = @ProductName, Category = @Category, Price = @Price
    WHERE ProductId = @ProductId;
END;
GO

-- Delete Stored Procedure [cite: 283]
CREATE PROCEDURE sp_DeleteProduct
    @ProductId INT
AS
BEGIN
    DELETE FROM Products WHERE ProductId = @ProductId;
END;
GO