USE Week3Day4;

CREATE TABLE customers (
    customer_id INT PRIMARY KEY,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    email VARCHAR(100)
);

CREATE TABLE orders (
    order_id INT PRIMARY KEY,
    customer_id INT NOT NULL,
    order_date DATE NOT NULL,
    order_status INT,
    order_total DECIMAL(10,2) NOT NULL,

    CONSTRAINT FK_Orders_Customers
        FOREIGN KEY (customer_id)
        REFERENCES customers(customer_id)
);

INSERT INTO customers VALUES
(1,'Rahul','Sharma','rahul@gmail.com'),
(2,'Priya','Patil','priya@gmail.com'),
(3,'Amit','Verma','amit@gmail.com'),
(4,'Sneha','Joshi','sneha@gmail.com'),
(5,'Rohit','Kulkarni','rohit@gmail.com');

SELECT * FROM customers;


INSERT INTO orders VALUES
(101,1,'2024-01-10',4,12000),
(102,1,'2024-02-12',4,3000),
(103,2,'2024-01-15',4,7000),
(104,3,'2024-03-01',4,4000);

SELECT * FROM orders;


-- Customers WITH orders
SELECT 
    CONCAT(c.first_name, ' ', c.last_name) AS full_name,
    
    -- Total order value using subquery
    (SELECT SUM(o2.order_total)
     FROM orders o2
     WHERE o2.customer_id = c.customer_id) AS total_order_value,

    -- Classification
    CASE
        WHEN (SELECT SUM(o3.order_total)
              FROM orders o3
              WHERE o3.customer_id = c.customer_id) > 10000 
            THEN 'Premium'

        WHEN (SELECT SUM(o3.order_total)
              FROM orders o3
              WHERE o3.customer_id = c.customer_id) BETWEEN 5000 AND 10000
            THEN 'Regular'

        ELSE 'Basic'
    END AS customer_category

FROM customers c
INNER JOIN orders o
    ON c.customer_id = o.customer_id
GROUP BY c.customer_id, c.first_name, c.last_name

UNION

-- Customers WITHOUT orders
SELECT
    CONCAT(c.first_name, ' ', c.last_name) AS full_name,
    0 AS total_order_value,
    'No Orders' AS customer_category
FROM customers c
WHERE c.customer_id NOT IN 
    (SELECT customer_id FROM orders)

ORDER BY full_name;