


CREATE DATABASE asp
use asp
  
CREATE TABLE Users (
id int primary key ,
fullname varchar(250),
username varchar(250),
password varchar(250)
);
 
insert into Users values(1,'khalid mukhtaar','khalid','123');

CREATE TABLE Product (
    ID INT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    Description TEXT
);

CREATE TABLE OrderDetails (
    OrderID INT PRIMARY KEY identity (1,1),
	Name VARCHAR(255) NOT NULL,
    ProductID INT FOREIGN KEY (ProductID) REFERENCES Product(ID),
    Quantity INT,
    Price DECIMAL(10, 2),
	Description TEXT,
    
);

drop table OrderDetails
insert into Product values(1,'keyboard',12.1,'waa keyboard wire less ah')
insert into Product values(2,'mouse',10.1,'waa mouse wire less ah');

select * from OrderDetails
insert into OrderDetails values ('keyboard',1,2,12.1,'keyboard wire less ah');
SET IDENTITY_INSERT OrderDetails ON;
DELETE  FROM OrderDetails where OrderID =2


CREATE TABLE OrderDetails (
    OrderID INT identity (1,1),
	Name VARCHAR(255) NOT NULL,
    ProductID INT,
    Quantity INT,
    Price DECIMAL(10, 2),
	Description TEXT,
    FOREIGN KEY (ProductID) REFERENCES Product(ID)
   
);