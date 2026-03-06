CREATE DATABASE Week3Day4;

USE Week3Day4;

CREATE TABLE categories (
    category_id INT PRIMARY KEY,
    category_name VARCHAR(100) NOT NULL
);

CREATE TABLE products (
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100) NOT NULL,
    category_id INT NOT NULL,
    model_year INT NOT NULL,
    list_price DECIMAL(10,2) NOT NULL,

    CONSTRAINT FK_Products_Categories
        FOREIGN KEY (category_id)
        REFERENCES categories(category_id)
);

INSERT INTO categories VALUES
(1,'Sedan'),
(2,'SUV'),
(3,'Hatchback');

SELECT * FROM categories;

INSERT INTO products VALUES
(101,'Honda City',1,2023,900000),
(102,'Hyundai Verna',1,2023,800000),
(103,'Toyota Corolla',1,2022,700000),

(104,'Toyota Fortuner',2,2024,3500000),
(105,'Mahindra XUV700',2,2023,2500000),
(106,'Tata Safari',2,2023,2400000),

(107,'Maruti Swift',3,2023,600000),
(108,'Hyundai i20',3,2022,750000),
(109,'Tata Altroz',3,2023,650000);

SELECT * FROM products;

SELECT 
    CONCAT(p.product_name, ' (', p.model_year, ')') AS product_info,
    p.product_name,
    p.model_year,
    p.list_price,

    -- Category average price
    (SELECT AVG(p2.list_price)
     FROM products p2
     WHERE p2.category_id = p.category_id) AS category_avg_price,

    -- Difference
    p.list_price -
    (SELECT AVG(p3.list_price)
     FROM products p3
     WHERE p3.category_id = p.category_id) 
    AS price_difference

FROM products p
WHERE p.list_price >
    (SELECT AVG(p4.list_price)
     FROM products p4
     WHERE p4.category_id = p.category_id)
ORDER BY p.category_id;