-- Script Sweet Customer database

-- Step 1
CREATE DATABASE SweetCustomer;

-- Step 2
USE SweetCustomer;

CREATE TABLE Country (
    CountryId INT PRIMARY KEY NOT NULL,
    Name NVARCHAR(MAX) NOT NULL
);

CREATE TABLE Customer (
    CustomerId INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    CountryId INT NOT NULL,
    Name NVARCHAR(MAX) NOT NULL,
    FOREIGN KEY (CountryId) REFERENCES Country(CountryId)
);


-- Step 3. Seed country data

INSERT INTO Country (CountryId, Name) VALUES
(1, 'Sweden'),
(2, 'Norway'),
(3, 'Finland'),
(4, 'Denmark'),
(5, 'Iceland');
