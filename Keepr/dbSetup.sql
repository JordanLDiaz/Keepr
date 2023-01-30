CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture',
        coverImg VARCHAR(255) DEFAULT 'https://images.unsplash.com/photo-1500964757637-c85e8a162699?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8OHx8Y292ZXIlMjBpbWFnZXxlbnwwfHwwfHw%3D&auto=format&fit=crop&w=500&q=60' COMMENT 'User Cover Image'
    ) default charset utf8 COMMENT '';

DROP TABLE accounts;

CREATE TABLE
    IF NOT EXISTS vaults(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        creatorId VARCHAR(255) NOT NULL,
        name VARCHAR(255) NOT NULL COMMENT 'Vault Name',
        description TEXT NOT NULL COMMENT 'Vault description',
        img VARCHAR(255) NOT NULL DEFAULT 'https://images.unsplash.com/photo-1582139329536-e7284fece509?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8M3x8dmF1bHR8ZW58MHx8MHx8&auto=format&fit=crop&w=500&q=60',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        isPrivate BOOLEAN NOT NULL DEFAULT false,
        FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

DROP TABLE vaults;

CREATE TABLE
    IF NOT EXISTS keeps(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        creatorId VARCHAR(255) NOT NULL,
        name VARCHAR(255) NOT NULL COMMENT 'Keep Name',
        description TEXT NOT NULL COMMENT 'Keep Description',
        img VARCHAR(255) NOT NULL DEFAULT 'https://images.unsplash.com/photo-1496262967815-132206202600?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8MTZ8fGtlZXAlMjBvYmplY3R8ZW58MHx8MHx8&auto=format&fit=crop&w=500&q=60',
        views INT NOT NULL DEFAULT 0 COMMENT 'Keep Views',
        kept INT NOT NULL DEFAULT 0 COMMENT 'Keep keeps',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

DROP TABLE keeps;

CREATE TABLE
    IF NOT EXISTS vaultKeeps(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        creatorId VARCHAR(255) NOT NULL,
        vaultId INT NOT NULL,
        keepId INT NOT NULL,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE,
        FOREIGN KEY (vaultId) REFERENCES vaults (id) ON DELETE CASCADE,
        FOREIGN KEY (keepId) REFERENCES keeps (id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

DROP TABLE vaultKeeps;

SELECT
    keeps.*,
    COUNT(vaultKeeps.id) AS kept,
    accounts.*
FROM keeps
    JOIN accounts ON accounts.id = keeps.creatorId
    LEFT JOIN vaultKeeps ON vaultKeeps.keepId = keeps.id
WHERE keeps.id = 1;