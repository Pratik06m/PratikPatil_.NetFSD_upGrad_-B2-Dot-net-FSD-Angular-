USE StoreDb;

CREATE TABLE stores (
    store_id INT PRIMARY KEY,
    store_name VARCHAR(100) NOT NULL,
    city VARCHAR(100)
);

ALTER TABLE orders
ADD store_id INT;

ALTER TABLE orders
ADD CONSTRAINT FK_Orders_Stores
FOREIGN KEY (store_id)
REFERENCES stores(store_id);

CREATE TABLE order_items (
    order_item_id INT PRIMARY KEY,
    order_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    list_price DECIMAL(10,2) NOT NULL,
    discount DECIMAL(4,2) NOT NULL,  -- Example: 0.10 = 10%

    CONSTRAINT FK_OrderItems_Orders
        FOREIGN KEY (order_id)
        REFERENCES orders(order_id)
);

INSERT INTO stores VALUES
(1,'Pune Store','Pune'),
(2,'Mumbai Store','Mumbai'),
(3,'Nagpur Store','Nagpur');

UPDATE orders SET store_id = 1 WHERE order_id IN (101,104);
UPDATE orders SET store_id = 2 WHERE order_id IN (102,106);
UPDATE orders SET store_id = 3 WHERE order_id IN (103,105);

INSERT INTO order_items VALUES
(1,101,101,2,75000,0.10),
(2,102,103,1,6000,0.05),
(3,104,104,1,80000,0.15),
(4,106,105,3,55000,0.08),
(5,103,102,1,42000,0.00);

SELECT 
    s.store_name,
    SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_sales
FROM stores s
INNER JOIN orders o
    ON s.store_id = o.store_id
INNER JOIN order_items oi
    ON o.order_id = oi.order_id
WHERE o.order_status = 4
GROUP BY s.store_name
ORDER BY total_sales DESC;