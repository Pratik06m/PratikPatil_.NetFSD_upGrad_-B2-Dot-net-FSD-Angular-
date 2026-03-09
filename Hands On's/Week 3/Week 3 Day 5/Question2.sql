USE Week3Day5;

CREATE VIEW vw_product_details
AS
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

SELECT * FROM vw_product_details;

CREATE VIEW vw_customer_store_info
AS
SELECT 
    c.customer_id,
    c.first_name + ' ' + c.last_name AS customer_name,
    c.city AS customer_city,
    s.store_name,
    s.city AS store_city
FROM customers c
CROSS JOIN stores s;

SELECT * FROM vw_customer_store_info;

CREATE INDEX idx_products_brand
ON products(brand_id);

CREATE INDEX idx_products_category
ON products(category_id);

SELECT 
p.product_name,
b.brand_name,
c.category_name
FROM products p
JOIN brands b
ON p.brand_id = b.brand_id
JOIN categories c
ON p.category_id = c.category_id;