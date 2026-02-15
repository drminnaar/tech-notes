CREATE TABLE IF NOT EXISTS customers.customers_by_id (
    created_at TIMESTAMP,
    customer_id uuid,
    email text,
    region text,
    first_name text,
    last_name text,
    registered_date timestamp,
    PRIMARY KEY (customer_id)
) WITH comment = 'Stores customer details partitioned by customer ID. Enables primary lookup by unique Customer ID (The fastest, single-row lookup)';