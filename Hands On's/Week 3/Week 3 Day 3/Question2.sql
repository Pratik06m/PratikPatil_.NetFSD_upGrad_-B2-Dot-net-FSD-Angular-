USE StoreDb;

CREATE TABLE categories (
    category_id INT PRIMARY KEY,
    category_name VARCHAR(100) NOT NULL
);

CREATE TABLE brands (
    brand_id INT PRIMARY KEY,
    brand_name VARCHAR(100) NOT NULL
);

CREATE TABLE products (
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100) NOT NULL,
    brand_id INT NOT NULL,
    category_id INT NOT NULL,
    model_year INT NOT NULL,
    list_price DECIMAL(10,2) NOT NULL,

    CONSTRAINT FK_Products_Brands
        FOREIGN KEY (brand_id)
        REFERENCES brands(brand_id),

    CONSTRAINT FK_Products_Categories
        FOREIGN KEY (category_id)
        REFERENCES categories(category_id)
);

INSERT INTO categories VALUES
(1,'Electronics'),
(2,'Appliances'),
(3,'Sports');

INSERT INTO brands VALUES
(1,'Samsung'),
(2,'LG'),
(3,'Nike'),
(4,'Apple');

INSERT INTO products VALUES
(101,'Galaxy S23',1,1,2025,75000.00),
(102,'LG Washing Machine',2,2,2024,42000.00),
(103,'Nike Running Shoes',3,3,2025,6000.00),
(104,'iPhone 15',4,1,2025,80000.00),
(105,'Air Conditioner',2,2,2023,55000.00),
(106,'Bluetooth Speaker',1,1,2024,4500.00);

SELECT 
    p.product_name,
    b.brand_name,
    c.category_name,
    p.model_year,
    p.list_price
FROM products p
INNER JOIN brands b
    ON p.brand_id = b.brand_id
INNER JOIN categories c
    ON p.category_id = c.category_id
WHERE p.list_price > 500
ORDER BY p.list_price ASC;