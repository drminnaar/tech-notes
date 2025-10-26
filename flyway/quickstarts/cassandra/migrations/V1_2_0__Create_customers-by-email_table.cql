CREATE TABLE IF NOT EXISTS customers.customers_by_email (    
    email text,
    customer_id uuid,
    region text,
    first_name text,
    last_name text,
    registered_date timestamp,
    PRIMARY KEY (email)
) WITH comment = 'Stores customer details partitioned by email. Lookup for login/auth using Email (Unique email address)';