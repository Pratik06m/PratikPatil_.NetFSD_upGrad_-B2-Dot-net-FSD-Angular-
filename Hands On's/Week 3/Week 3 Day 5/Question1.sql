CREATE DATABASE Week3Day5;

USE Week3Day5;

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
    product_name VARCHAR(150) NOT NULL,
    brand_id INT,
    category_id INT,
    model_year INT,
    list_price DECIMAL(10,2),

    FOREIGN KEY (brand_id) REFERENCES brands(brand_id),
    FOREIGN KEY (category_id) REFERENCES categories(category_id)
);

CREATE TABLE customers (
    customer_id INT PRIMARY KEY,
    first_name VARCHAR(100),
    last_name VARCHAR(100),
    city VARCHAR(100),
    phone VARCHAR(15),
    email VARCHAR(150)
);

CREATE TABLE stores (
    store_id INT PRIMARY KEY,
    store_name VARCHAR(150),
    city VARCHAR(100),
    phone VARCHAR(15)
);

INSERT INTO categories VALUES
(1,'Motorcycles'),
(2,'Cars'),
(3,'Scooters'),
(4,'Electric Vehicles'),
(5,'Trucks');


INSERT INTO brands VALUES
(1,'Yamaha'),
(2,'Honda'),
(3,'Hyundai'),
(4,'Tata'),
(5,'Suzuki');

INSERT INTO products VALUES
(101,'Yamaha R15',1,1,2020,200000),
(102,'Honda Shine',2,1,2019,95000),
(103,'Hyundai Verna',3,2,2021,1200000),
(104,'Tata Nexon EV',4,4,2022,1500000),
(105,'Suzuki Access 125',5,3,2020,90000);


INSERT INTO customers VALUES
(1,'Rahul','Sharma','Mumbai','9876543210','rahul@gmail.com'),
(2,'Priya','Patil','Pune','9876543211','priya@gmail.com'),
(3,'Amit','Verma','Mumbai','9876543212','amit@gmail.com'),
(4,'Sneha','Joshi','Nagpur','9876543213','sneha@gmail.com'),
(5,'Rohit','Kulkarni','Pune','9876543214','rohit@gmail.com');


INSERT INTO stores VALUES
(1,'Auto World','Mumbai','9871111111'),
(2,'City Motors','Pune','9872222222'),
(3,'Prime Vehicles','Nagpur','9873333333'),
(4,'Super Auto','Delhi','9874444444'),
(5,'Urban Motors','Bangalore','9875555555');


SELECT 
p.product_name,
b.brand_name,
c.category_name,
p.model_year,
p.list_price
FROM products p
JOIN brands b
ON p.brand_id = b.brand_id
JOIN categories c
ON p.category_id = c.category_id;


SELECT *
FROM customers
WHERE city = 'Mumbai';


SELECT 
c.category_name,
COUNT(p.product_id) AS total_products
FROM categories c
LEFT JOIN products p
ON c.category_id = p.category_id
GROUP BY c.category_name;