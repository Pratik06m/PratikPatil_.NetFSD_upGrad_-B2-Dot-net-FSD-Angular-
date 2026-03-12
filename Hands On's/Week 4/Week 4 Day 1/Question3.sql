CREATE DATABASE W4D1Q3;
GO

USE W4D1Q3;
GO

CREATE TABLE customers(
    customer_id INT PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50)
);

CREATE TABLE stores(
    store_id INT PRIMARY KEY,
    store_name VARCHAR(100)
);

CREATE TABLE orders(
    order_id INT PRIMARY KEY,
    customer_id INT,
    store_id INT,
    order_date DATE,
    shipped_date DATE NULL,
    order_status INT,

    FOREIGN KEY (customer_id) REFERENCES customers(customer_id),
    FOREIGN KEY (store_id) REFERENCES stores(store_id)
);

INSERT INTO customers VALUES
(1,'Rahul','Sharma'),
(2,'Priya','Patil'),
(3,'Amit','Verma'),
(4,'Sneha','Joshi'),
(5,'Rohit','Kulkarni');

INSERT INTO stores VALUES
(1,'Auto Hub'),
(2,'City Motors'),
(3,'Prime Vehicles'),
(4,'Urban Motors'),
(5,'Super Wheels');


INSERT INTO orders VALUES
(101,1,1,'2024-01-10',NULL,1),
(102,2,2,'2024-02-12',NULL,2),
(103,3,1,'2024-03-01','2024-03-05',4),
(104,4,3,'2024-03-20',NULL,1),
(105,5,2,'2024-04-01',NULL,2);

CREATE TRIGGER trg_validate_order_status
ON orders
AFTER UPDATE
AS
BEGIN
    BEGIN TRY

        IF EXISTS (
            SELECT 1
            FROM inserted
            WHERE order_status = 4
            AND shipped_date IS NULL
        )
        BEGIN
            RAISERROR ('Shipped date must not be NULL when order status is Completed.',16,1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

    END TRY

    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;


UPDATE orders
SET shipped_date = '2024-05-10',
order_status = 4
WHERE order_id = 101;


SELECT * FROM orders;



