CREATE DATABASE StoreDb;

USE StoreDb;

CREATE TABLE customers (
    customer_id INT PRIMARY KEY,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    email VARCHAR(100) UNIQUE
);

CREATE TABLE orders (
    order_id INT PRIMARY KEY,
    customer_id INT NOT NULL,
    order_date DATETIME NOT NULL,
    order_status INT NOT NULL,
    total_amount DECIMAL(10,2),

    CONSTRAINT FK_Orders_Customers
        FOREIGN KEY (customer_id)
        REFERENCES customers(customer_id)
);

INSERT INTO customers VALUES
(1,'Rahul','Sharma','rahul@gmail.com'),
(2,'Priya','Patil','priya@gmail.com'),
(3,'Amit','Verma','amit@gmail.com'),
(4,'Sneha','Joshi','sneha@gmail.com');

INSERT INTO orders VALUES
(101,1,'2026-03-01',1,1500.00),   -- Pending
(102,2,'2026-03-02',4,2500.00),   -- Completed
(103,3,'2026-03-03',2,1800.00),   -- Processing
(104,1,'2026-03-04',4,3000.00),   -- Completed
(105,4,'2026-03-05',5,1200.00),   -- Cancelled
(106,2,'2026-03-06',1,2200.00);   -- Pending

SELECT 
    c.first_name,
    c.last_name,
    o.order_id,
    o.order_date,
    o.order_status
FROM customers c
INNER JOIN orders o
    ON c.customer_id = o.customer_id
WHERE o.order_status = 1
   OR o.order_status = 4
ORDER BY o.order_date DESC;