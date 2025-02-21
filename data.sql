-- Insert fake suppliers
INSERT INTO Supplier (name, address) VALUES
('Supplier A', '123 Main St'),
('Supplier B', '456 Elm St'),
('Supplier C', '789 Oak St');

-- Insert fake articles
INSERT INTO Article (name, idFamille, idSupplier, priceincents, year) VALUES
('Merlot Rouge', 1, 1, 1500, 2020),
('Chardonnay Blanc', 3, 1, 2000, 2019),
('Pinot Noir Rouge', 1, 2, 1800, 2021),
('Sparkling Rosé', 2, 3, 2500, 2020),
('Digestive Spirit', 5, 3, 3000, 2018);

-- Insert fake clients
INSERT INTO Client (name) VALUES
('Alice'),
('Bob'),
('Charlie');

-- Insert fake supplier orders (with grouped orders)
INSERT INTO SupplierOrder (idSupplier, idArticle, quantity, status, group_id) VALUES
(1, 1, 50, 'pending', NULL),
(1, 2, 30, 'delivered', NULL),
(2, 3, 40, 'canceled', NULL),
(3, 4, 20, 'pending', 4), -- group starts here
(3, 5, 15, 'pending', 4); -- part of group 4

-- Insert fake sales (with grouped sales)
INSERT INTO Sale (idArticle, idClient, quantity, date, group_id) VALUES
(1, 1, 2, '2024-12-01 12:00:00', NULL),
(2, 2, 1, '2024-12-02 13:00:00', NULL),
(3, 3, 3, '2024-12-03 14:00:00', NULL),
(4, 1, 1, '2024-12-04 15:00:00', 4), -- group starts here
(5, 2, 2, '2024-12-04 15:30:00', 4); -- part of group 4

-- Insert fake articles in carts
INSERT INTO ArticlesInCart (idArticle, idClient, quantity) VALUES
(1, 1, 1),
(2, 2, 2),
(3, 3, 1),
(4, 1, 3),
(5, 2, 1);
