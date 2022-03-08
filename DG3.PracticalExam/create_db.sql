
USE master
GO

CREATE DATABASE customers
GO

USE customers
GO

CREATE TABLE customer
(
   customer_id       int IDENTITY PRIMARY KEY,
   first_name        nvarchar(100) NOT NULL,
   last_name         nvarchar(100) NOT NULL,
   address_1         nvarchar(100) NOT NULL,
   address_2         nvarchar(100) NULL,
   city              nvarchar(100) NOT NULL,
   province          nvarchar(100) NULL,
   postcode          nvarchar(20) NULL,
   country           nvarchar(100) NOT NULL,
   email             nvarchar(100) NOT NULL,
   registration_date datetime NULL
);
GO

CREATE PROCEDURE customer_insert
(
   @first_name        nvarchar(100),
   @last_name         nvarchar(100),
   @address_1         nvarchar(100),
   @address_2         nvarchar(100),
   @city              nvarchar(100),
   @province          nvarchar(100),
   @postcode          nvarchar(20),
   @country           nvarchar(100),
   @email             nvarchar(100),
   @registration_date datetime = NULL,
   @customer_id       int OUTPUT,
)
AS
BEGIN
   INSERT INTO customer
   (
      first_name,
      last_name,
      address_1,
      address_2,
      city,
      province,
      postcode,
      country,
      email,
      registration_date
   )
   VALUES
   (
      @first_name,
      @last_name,
      @address_1,
      @address_2,
      @city,
      @province,
      @postcode,
      @country,
      @email,
      @registration_date
   );

   SET @customer_id = IDENT_CURRENT('customer');
END
GO