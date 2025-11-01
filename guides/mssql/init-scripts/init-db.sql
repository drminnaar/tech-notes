IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'sample_db')
BEGIN
    CREATE DATABASE sample_db;
    PRINT 'Database ''sample_db'' created.';
END
ELSE
BEGIN
    PRINT 'Database ''sample_db'' already exists.';
END
GO