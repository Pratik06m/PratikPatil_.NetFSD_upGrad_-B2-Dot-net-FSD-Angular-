USE StoreDb;

CREATE TABLE stocks (
    store_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL,

    CONSTRAINT PK_Stocks 
        PRIMARY KEY (store_id, product_id),

    CONSTRAINT FK_Stocks_Stores
        FOREIGN KEY (store_id)
        REFERENCES stores(store_id),

    CONSTRAINT FK_Stocks_Products
        FOREIGN KEY (product_id)
        REFERENCES products(product_id)
);

INSERT INTO stocks VALUES
(1,101,20),
(1,103,50),
(2,104,15),
(2,105,30),
(3,102,25),
(3,106,40);

SELECT 
    p.product_name,
    s.store_name,
    st.quantity AS available_stock,
    IFNULL(SUM(oi.quantity), 0) AS total_quantity_sold
FROM stocks st
INNER JOIN products p
    ON st.product_id = p.product_id
INNER JOIN stores s
    ON st.store_id = s.store_id
LEFT JOIN orders o
    ON st.store_id = o.store_id
    AND o.order_status = 4
LEFT JOIN order_items oi
    ON o.order_id = oi.order_id
    AND st.product_id = oi.product_id
GROUP BY 
    p.product_name,
    s.store_name,
    st.quantity
ORDER BY 
    p.product_name;