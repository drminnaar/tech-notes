-- idempotent approach to create keyspace
-- 'IF NOT EXISTS' makes the script safe to re-run
CREATE KEYSPACE IF NOT EXISTS product_catalog WITH REPLICATION = {'class': 'SimpleStrategy', 'replication_factor': 1};

-- idempotent approach to create table
-- 'IF NOT EXISTS' makes the script safe to re-run
-- create table using fully-qualified name (keyspace_name.table_name)
CREATE TABLE IF NOT EXISTS product_catalog.products (
    created_at TIMESTAMP,
    product_id UUID PRIMARY KEY,
    product_code TEXT,
    name TEXT,
    description TEXT,
    price DECIMAL,
    brand TEXT,
    category TEXT,
    stock INT,
    image_url TEXT
) WITH comment = 'Stores product details, partitioned by product ID';

-- insert sample data using fully-qualified table name so we don't rely on 'USE product_catalog'
INSERT INTO product_catalog.products (created_at, product_id, product_code, name, description, price, brand, category, stock, image_url) VALUES (toTimestamp(now()), uuid(), 'BK-MT01-44', 'PeakPulse TrailBlaster', 'A blue mountain bike for rugged trails, size 44', 799.99, 'PeakPulse', 'Mountain Bike', 50, 'https://example.com/images/peakpulse_trailblaster.jpg');
INSERT INTO product_catalog.products (created_at, product_id, product_code, name, description, price, brand, category, stock, image_url) VALUES (toTimestamp(now()), uuid(), 'SB-OG01-8', 'OceanGlide Pro', 'A yellow surfboard for pro surfers, 8ft', 429.99, 'OceanGlide', 'Surfboard', 25, 'https://example.com/images/oceanglide_pro.jpg');
