CREATE DATABASE stive;

USE stive;

CREATE TABLE
    Famille (
        id INT AUTO_INCREMENT PRIMARY KEY,
        name VARCHAR(100) NOT NULL UNIQUE
    );

INSERT INTO
    Famille (name)
VALUES
    ('Rouge'),
    ('Rosé'),
    ('Blanc'),
    ('Pétillant'),
    ('Digestif');

CREATE TABLE
    Supplier (
        id INT AUTO_INCREMENT PRIMARY KEY,
        name VARCHAR(100) NOT NULL,
        address VARCHAR(100) NOT NULL
    );

CREATE TABLE
    Article (
        id INT AUTO_INCREMENT PRIMARY KEY,
        name VARCHAR(100) NOT NULL,
        idFamille INT NOT NULL,
        idSupplier INT NOT NULL,
        ref VARCHAR(50) NOT NULL,
        price DOUBLE NOT NULL,
        contenance INT NOT NULL,
        age DATE NOT NULL,
        artQuantity INT NOT NULL,
        FOREIGN KEY (idFamille) REFERENCES Famille (id),
        FOREIGN KEY (idSupplier) REFERENCES Supplier (id)
    );

CREATE TABLE Commande (
    id INT AUTO_INCREMENT PRIMARY KEY,
    idClient INT NOT NULL,
    totalPrice DOUBLE NOT NULL,
    status VARCHAR(50) DEFAULT 'Pending',
    createdAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (idClient) REFERENCES ClientSTIVE(id) ON DELETE CASCADE
);

CREATE TABLE ArticlesInCommande (
    id INT AUTO_INCREMENT PRIMARY KEY,
    idCommande INT NOT NULL,
    idArticle INT NOT NULL,
    quantity INT NOT NULL,
    price DOUBLE NOT NULL,
    FOREIGN KEY (idCommande) REFERENCES Commande(id) ON DELETE CASCADE,
    FOREIGN KEY (idArticle) REFERENCES Article(id) ON DELETE CASCADE
);

CREATE TABLE
    ClientSTIVE (
        id INT AUTO_INCREMENT PRIMARY KEY,
        name VARCHAR(100) NOT NULL
    );

CREATE TABLE
    ArticlesInCart (
        id INT AUTO_INCREMENT PRIMARY KEY,
        idArticle INT NOT NULL,
        idClient INT NOT NULL,
        quantity INT NOT NULL
    );

CREATE TABLE
    Admin (
        id INT AUTO_INCREMENT PRIMARY KEY,
        hashed_password VARCHAR(255) NOT NULL
    );

INSERT INTO
    Admin (hashed_password)
VALUES
    (MD5 ('password'));