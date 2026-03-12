CREATE DATABASE W4D1Q4;
GO

USE W4D1Q4;
GO

CREATE TABLE stores(
    store_id INT PRIMARY KEY,
    store_name VARCHAR(100)
);


CREATE TABLE customers(
    customer_id INT PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50)
);

CREATE TABLE products(
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100),
    list_price DECIMAL(10,2)
);

CREATE TABLE orders(
    order_id INT PRIMARY KEY,
    customer_id INT,
    store_id INT,
    order_date DATE,
    order_status INT,

    FOREIGN KEY (customer_id) REFERENCES customers(customer_id),
    FOREIGN KEY (store_id) REFERENCES stores(store_id)
);

CREATE TABLE order_items(
    order_item_id INT PRIMARY KEY,
    order_id INT,
    product_id INT,
    quantity INT,
    list_price DECIMAL(10,2),
    discount DECIMAL(5,2),

    FOREIGN KEY (order_id) REFERENCES orders(order_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);

INSERT INTO stores VALUES
(1,'Auto Hub'),
(2,'City Motors'),
(3,'Prime Vehicles'),
(4,'Urban Motors'),
(5,'Super Wheels');



INSERT INTO customers VALUES
(1,'Rahul','Sharma'),
(2,'Priya','Patil'),
(3,'Amit','Verma'),
(4,'Sneha','Joshi'),
(5,'Rohit','Kulkarni');

INSERT INTO orders VALUES
(101,1,1,'2024-01-10',4),
(102,2,2,'2024-02-12',4),
(103,3,1,'2024-03-01',2),
(104,4,3,'2024-03-20',4),
(105,5,2,'2024-04-01',1);


INSERT INTO order_items VALUES
(1,101,1,1,200000,0.10),
(2,101,2,2,90000,0.05),
(3,102,3,3,85000,0.05),
(4,104,4,1,1200000,0.10),
(5,104,5,1,1500000,0.15);

BEGIN TRY

BEGIN TRANSACTION

-- Temporary table to store order revenue
CREATE TABLE #order_revenue(
    order_id INT,
    store_id INT,
    revenue DECIMAL(12,2)
)

DECLARE @order_id INT
DECLARE @store_id INT
DECLARE @revenue DECIMAL(12,2)

-- Cursor to fetch completed orders
DECLARE order_cursor CURSOR FOR
SELECT order_id, store_id
FROM orders
WHERE order_status = 4

OPEN order_cursor

FETCH NEXT FROM order_cursor INTO @order_id, @store_id

WHILE @@FETCH_STATUS = 0
BEGIN

    -- Calculate revenue per order
    SELECT @revenue =
    SUM(quantity * list_price * (1 - ISNULL(discount,0)))
    FROM order_items
    WHERE order_id = @order_id

    -- Insert into temporary table
    INSERT INTO #order_revenue
    VALUES(@order_id, @store_id, @revenue)

    FETCH NEXT FROM order_cursor INTO @order_id, @store_id
END

CLOSE order_cursor
DEALLOCATE order_cursor

-- Store-wise revenue summary
SELECT 
s.store_name,
SUM(r.revenue) AS total_store_revenue
FROM #order_revenue r
JOIN stores s
ON r.store_id = s.store_id
GROUP BY s.store_name

COMMIT TRANSACTION

END TRY

BEGIN CATCH
ROLLBACK TRANSACTION
PRINT 'Error occurred during revenue calculation'
END CATCH





