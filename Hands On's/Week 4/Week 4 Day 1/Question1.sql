CREATE DATABASE EcommDb;
GO

USE EcommDb;
GO

CREATE TABLE stores (
    store_id INT PRIMARY KEY,
    store_name VARCHAR(100),
    city VARCHAR(50)
);


CREATE TABLE customers(
    customer_id INT PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    city VARCHAR(100)
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
(1,'AutoHub','Mumbai'),
(2,'Speed Motors','Pune'),
(3,'City Wheels','Nagpur');

INSERT INTO customers VALUES
(1,'Rahul','Sharma','Mumbai'),
(2,'Priya','Patil','Pune'),
(3,'Amit','Verma','Delhi'),
(4,'Sneha','Joshi','Nagpur'),
(5,'Rohit','Kulkarni','Pune');

INSERT INTO orders VALUES
(101,1,1,'2024-01-10'),
(102,2,2,'2024-02-15'),
(103,3,1,'2024-03-05'),
(104,4,3,'2024-04-01'),
(105,5,2,'2024-04-20');

INSERT INTO products VALUES
(1,'Yamaha R15',200000),
(2,'Honda Shine',90000),
(3,'Hyundai Verna',1200000),
(4,'Tata Nexon',1500000),
(5,'Suzuki Access',85000);

INSERT INTO order_items VALUES
(1,101,1,1,200000,0.10),
(2,102,2,2,90000,0.05),
(3,103,3,1,1200000,0.15),
(4,104,4,1,1500000,0.10),
(5,105,5,3,85000,0.05);

CREATE PROCEDURE sp_total_sales_per_store
AS
BEGIN

SELECT 
s.store_name,
SUM(oi.quantity * oi.list_price * (1 - ISNULL(oi.discount,0))) AS total_sales
FROM stores s
JOIN orders o
ON s.store_id = o.store_id
JOIN order_items oi
ON o.order_id = oi.order_id
GROUP BY s.store_name;

END;

EXEC sp_total_sales_per_store;

CREATE FUNCTION fn_calculate_discount_price
(
@price DECIMAL(10,2),
@discount DECIMAL(5,2)
)

RETURNS DECIMAL(10,2)
AS
BEGIN

RETURN @price * (1 - ISNULL(@discount,0));

END;

SELECT 
product_id,
list_price,
discount,
dbo.fn_calculate_discount_price(list_price,discount) AS final_price
FROM order_items;

CREATE FUNCTION fn_top_selling_products()
RETURNS TABLE
AS
RETURN
(
SELECT TOP 5
p.product_name,
SUM(oi.quantity) AS total_sold
FROM products p
JOIN order_items oi
ON p.product_id = oi.product_id
GROUP BY p.product_name
ORDER BY total_sold DESC
);

SELECT * FROM dbo.fn_top_selling_products();