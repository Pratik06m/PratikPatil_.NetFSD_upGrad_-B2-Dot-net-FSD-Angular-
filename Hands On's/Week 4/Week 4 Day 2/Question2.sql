CREATE DATABASE W4D2Q2;
GO

USE W4D2Q2;
GO

CREATE TABLE stores(
    store_id INT PRIMARY KEY,
    store_name VARCHAR(100)
);

CREATE TABLE products(
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100),
    list_price DECIMAL(10,2)
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
    order_status INT,

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


INSERT INTO orders VALUES
(101,1,'2024-05-01',4),
(102,1,'2024-05-02',2);

INSERT INTO order_items VALUES
(1,101,1,2,200000),
(2,101,2,3,90000),
(3,102,3,1,85000);

BEGIN TRY

BEGIN TRANSACTION

DECLARE @order_id INT = 101

-- Savepoint before restoring stock
SAVE TRANSACTION restore_point

-- Restore stock quantities
UPDATE s
SET s.quantity = s.quantity + oi.quantity
FROM stocks s
JOIN order_items oi 
ON s.product_id = oi.product_id
JOIN orders o
ON o.order_id = oi.order_id
WHERE oi.order_id = @order_id
AND s.store_id = o.store_id

-- Check if update affected rows
IF @@ROWCOUNT = 0
BEGIN
    RAISERROR('Stock restoration failed.',16,1)
END

-- Update order status to Rejected (3)
UPDATE orders
SET order_status = 3
WHERE order_id = @order_id

COMMIT TRANSACTION

PRINT 'Order cancelled successfully and stock restored'

END TRY

BEGIN CATCH

PRINT 'Error occurred during cancellation'

ROLLBACK TRANSACTION restore_point

ROLLBACK TRANSACTION

END CATCH


SELECT * FROM stocks;


SELECT * FROM orders;





