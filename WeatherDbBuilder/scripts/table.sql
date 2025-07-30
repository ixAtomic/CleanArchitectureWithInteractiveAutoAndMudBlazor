﻿
CREATE TABLE Weather (
    Id INT PRIMARY KEY IDENTITY(1,1),
    City NVARCHAR(100) NOT NULL,
    Temperature DECIMAL(5, 2) NOT NULL,
    Humidity INT NOT NULL,
    Condition NVARCHAR(50) NOT NULL,
    Date DATE NOT NULL
);
