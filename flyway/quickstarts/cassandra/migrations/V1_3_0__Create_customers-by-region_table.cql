CREATE TABLE IF NOT EXISTS customers.customers_by_region (
    region text,
    customer_id uuid,
    email text,
    first_name text,
    last_name text,
    registered_date timestamp,
    PRIMARY KEY (region, customer_id)
)
WITH CLUSTERING ORDER BY (customer_id ASC)
AND  comment = 'Stores customer details partitioned by region and customer ID. Lookup for management/shipping by Region (Groups customers in a geographical area)';