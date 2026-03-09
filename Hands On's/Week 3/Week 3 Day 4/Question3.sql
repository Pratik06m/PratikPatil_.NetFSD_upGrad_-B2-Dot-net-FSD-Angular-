CREATE DATABASE StoreDB2;

USE StoreDB2;

CREATE TABLE stores (
    store_id INT PRIMARY KEY,
    store_name VARCHAR(50)
);

CREATE TABLE products (
    product_id INT PRIMARY KEY,
    product_name VARCHAR(50),
    list_price DECIMAL(10,2)
);

CREATE TABLE orders (
    order_id INT PRIMARY KEY,
    store_id INT,
    order_date DATE,
    FOREIGN KEY (store_id) REFERENCES stores(store_id)
);

CREATE TABLE order_items (
    order_id INT,
    product_id INT,
    quantity INT,
    list_price DECIMAL(10,2),
    discount DECIMAL(10,2),
    FOREIGN KEY (order_id) REFERENCES orders(order_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);

CREATE TABLE stocks (
    store_id INT,
    product_id INT,
    quantity INT,
    FOREIGN KEY (store_id) REFERENCES stores(store_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);

INSERT INTO stores VALUES
(1,'Pune Store'),
(2,'Mumbai Store'),
(3,'Delhi Store');


INSERT INTO products VALUES
(101,'Laptop',60000),
(102,'Mobile',30000),
(103,'Headphones',5000),
(104,'Keyboard',2000);

INSERT INTO orders VALUES
(1,1,'2026-03-01'),
(2,2,'2026-03-02'),
(3,1,'2026-03-03');

INSERT INTO order_items VALUES
(1,101,2,60000,2000),
(1,103,3,5000,500),
(2,102,1,30000,1000),
(3,104,4,2000,200);


INSERT INTO stocks VALUES
(1,101,0),
(1,103,10),
(1,104,0),
(2,102,0),
(3,101,5);

SELECT *
FROM (
    SELECT s.store_name, p.product_name, oi.quantity
    FROM order_items oi
    JOIN orders o ON oi.order_id = o.order_id
    JOIN stores s ON o.store_id = s.store_id
    JOIN products p ON oi.product_id = p.product_id
) AS sold_products;

SELECT 
    s.store_name,
    p.product_name,
    SUM(oi.quantity) AS total_quantity_sold,
    SUM(oi.quantity * oi.list_price - oi.discount) AS total_revenue
FROM order_items oi
JOIN orders o ON oi.order_id = o.order_id
JOIN stores s ON o.store_id = s.store_id
JOIN products p ON oi.product_id = p.product_id
JOIN stocks st ON st.product_id = oi.product_id 
              AND st.store_id = s.store_id
WHERE st.quantity = 0
GROUP BY s.store_name, p.product_name;

UPDATE stocks
SET quantity = 0
WHERE product_id IN (
    SELECT product_id
    FROM products
    WHERE product_name = 'Headphones'
);

SELECT 
    s.store_name,
    p.product_name,
    st.quantity
FROM stocks st
JOIN stores s ON st.store_id = s.store_id
JOIN products p ON st.product_id = p.product_id;