CREATE DATABASE OrderManagementDB;

USE OrderManagementDB;

CREATE TABLE customers (
    customer_id INT PRIMARY KEY,
    customer_name VARCHAR(50)
);

INSERT INTO customers VALUES
(1,'Rahul'),
(2,'Sneha'),
(3,'Amit'),
(4,'Priya');

CREATE TABLE orders (
    order_id INT PRIMARY KEY,
    customer_id INT,
    order_date DATE,
    shipped_date DATE,
    required_date DATE,
    order_status INT, 
    -- 1 = Pending, 2 = Completed, 3 = Rejected
    FOREIGN KEY (customer_id) REFERENCES customers(customer_id)
);

INSERT INTO orders VALUES
(101,1,'2024-01-10','2024-01-12','2024-01-15',2),
(102,1,'2023-02-05','2023-02-06','2023-02-10',3),
(103,2,'2024-06-01','2024-06-04','2024-06-05',2),
(104,3,'2022-03-15','2022-03-20','2022-03-18',3),
(105,4,'2024-07-01','2024-07-02','2024-07-05',1);

CREATE TABLE archived_orders (
    order_id INT,
    customer_id INT,
    order_date DATE,
    shipped_date DATE,
    required_date DATE,
    order_status INT
);

INSERT INTO archived_orders
SELECT *
FROM orders
WHERE order_status = 3
AND order_date < DATEADD(YEAR,-1,GETDATE());

DELETE FROM orders
WHERE order_id IN (
    SELECT order_id
    FROM archived_orders
);

SELECT customer_id
FROM customers
WHERE customer_id IN (
    SELECT customer_id
    FROM orders
    GROUP BY customer_id
    HAVING COUNT(*) = SUM(CASE WHEN order_status = 2 THEN 1 ELSE 0 END)
);

SELECT 
order_id,
order_date,
shipped_date,
DATEDIFF(DAY, order_date, shipped_date) AS processing_delay
FROM orders;

SELECT 
order_id,
order_date,
shipped_date,
required_date,
CASE
    WHEN shipped_date > required_date THEN 'Delayed'
    ELSE 'On Time'
END AS delivery_status
FROM orders;

SELECT * FROM archived_orders;

SELECT * FROM orders;