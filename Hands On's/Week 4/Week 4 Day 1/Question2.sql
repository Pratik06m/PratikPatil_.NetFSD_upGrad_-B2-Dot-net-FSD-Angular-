CREATE DATABASE W4D1Q2;
GO

USE W4D1Q2;
GO

CREATE TABLE products (
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100),
    list_price DECIMAL(10,2)
);


CREATE TABLE stores (
    store_id INT PRIMARY KEY,
    store_name VARCHAR(100)
);

CREATE TABLE stocks (
    store_id INT,
    product_id INT,
    quantity INT,

    PRIMARY KEY (store_id, product_id),

    FOREIGN KEY (store_id) REFERENCES stores(store_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);

CREATE TABLE orders (
    order_id INT PRIMARY KEY,
    store_id INT,
    order_date DATE,

    FOREIGN KEY (store_id) REFERENCES stores(store_id)
);

CREATE TABLE order_items (
    order_item_id INT PRIMARY KEY,
    order_id INT,
    product_id INT,
    quantity INT,
    list_price DECIMAL(10,2),

    FOREIGN KEY (order_id) REFERENCES orders(order_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);

INSERT INTO products VALUES
(1,'Yamaha R15',200000),
(2,'Honda Shine',90000),
(3,'Suzuki Access',85000),
(4,'Hyundai Verna',1200000),
(5,'Tata Nexon',1500000);

INSERT INTO stores VALUES
(1,'Auto Hub'),
(2,'City Motors');

INSERT INTO stocks VALUES
(1,1,10),
(1,2,15),
(1,3,20),
(2,4,8),
(2,5,5);

INSERT INTO orders VALUES
(101,1,'2024-02-01'),
(102,2,'2024-02-10');

CREATE TRIGGER trg_update_stock
ON order_items
AFTER INSERT
AS
BEGIN
    BEGIN TRY

        -- Check if stock is sufficient
        IF EXISTS (
            SELECT 1
            FROM inserted i
            JOIN orders o ON i.order_id = o.order_id
            JOIN stocks s 
            ON s.product_id = i.product_id 
            AND s.store_id = o.store_id
            WHERE s.quantity < i.quantity
        )
        BEGIN
            RAISERROR('Stock is insufficient for this product.',16,1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Update stock quantity
        UPDATE s
        SET s.quantity = s.quantity - i.quantity
        FROM stocks s
        JOIN inserted i 
        ON s.product_id = i.product_id
        JOIN orders o
        ON o.order_id = i.order_id
        WHERE s.store_id = o.store_id;

    END TRY

    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;

SELECT * FROM stocks;

INSERT INTO order_items VALUES
(1,101,1,2,200000);

INSERT INTO order_items VALUES
(2,102,5,20,1500000);

SELECT * FROM stocks;