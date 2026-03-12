CREATE DATABASE W4D2Q1;
GO

USE W4D2Q1;
GO

CREATE TABLE products(
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100),
    list_price DECIMAL(10,2)
);

CREATE TABLE stores(
    store_id INT PRIMARY KEY,
    store_name VARCHAR(100)
);

CREATE TABLE stocks(
    store_id INT,
    product_id INT,
    quantity INT,

    PRIMARY KEY(store_id, product_id),

    FOREIGN KEY(store_id) REFERENCES stores(store_id),
    FOREIGN KEY(product_id) REFERENCES products(product_id)
);

CREATE TABLE orders(
    order_id INT PRIMARY KEY,
    store_id INT,
    order_date DATE,

    FOREIGN KEY(store_id) REFERENCES stores(store_id)
);

CREATE TABLE order_items(
    order_item_id INT PRIMARY KEY,
    order_id INT,
    product_id INT,
    quantity INT,
    list_price DECIMAL(10,2),

    FOREIGN KEY(order_id) REFERENCES orders(order_id),
    FOREIGN KEY(product_id) REFERENCES products(product_id)
);

INSERT INTO stores VALUES
(1,'Auto Hub'),
(2,'City Motors');

INSERT INTO products VALUES
(1,'Yamaha R15',200000),
(2,'Honda Shine',90000),
(3,'Suzuki Access',85000);

INSERT INTO stocks VALUES
(1,1,10),
(1,2,15),
(1,3,20),
(2,1,5),
(2,2,10);

CREATE TRIGGER trg_reduce_stock
ON order_items
AFTER INSERT
AS
BEGIN
    IF EXISTS(
        SELECT 1
        FROM inserted i
        JOIN orders o ON i.order_id = o.order_id
        JOIN stocks s 
        ON s.product_id = i.product_id
        AND s.store_id = o.store_id
        WHERE s.quantity < i.quantity
    )
    BEGIN
        RAISERROR('Stock insufficient for this order.',16,1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    UPDATE s
    SET s.quantity = s.quantity - i.quantity
    FROM stocks s
    JOIN inserted i 
    ON s.product_id = i.product_id
    JOIN orders o
    ON o.order_id = i.order_id
    WHERE s.store_id = o.store_id;
END;

BEGIN TRY

BEGIN TRANSACTION

-- Insert order
INSERT INTO orders VALUES
(101,1,'2024-05-01');

-- Insert order items
INSERT INTO order_items VALUES
(1,101,1,2,200000),
(2,101,2,3,90000);

COMMIT TRANSACTION

PRINT 'Order placed successfully'

END TRY

BEGIN CATCH
ROLLBACK TRANSACTION
PRINT 'Order failed due to stock issue'
END CATCH

BEGIN TRY

BEGIN TRANSACTION

INSERT INTO orders VALUES
(102,1,'2024-05-02');

INSERT INTO order_items VALUES
(3,102,1,50,200000);

COMMIT TRANSACTION

END TRY

BEGIN CATCH
ROLLBACK TRANSACTION
PRINT 'Transaction rolled back due to insufficient stock'
END CATCH

SELECT * FROM stocks;