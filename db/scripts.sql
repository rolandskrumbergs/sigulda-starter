CREATE DATABASE AUTO_SLUDINAJUMI;

CREATE TABLE dbo.Sludinajumi (
    ID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Marka varchar(30) NOT NULL,
    Gads int NOT NULL,
    Ipasnieks varchar(100) NOT NULL
);

INSERT INTO dbo.Sludinajumi (Marka, Gads, Ipasnieks)
VALUES ('AUDI', 2023, 'Jānis Bērziņš');

SELECT * FROM dbo.Sludinajumi;