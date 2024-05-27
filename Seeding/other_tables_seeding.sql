DBCC CHECKIDENT ('[Customers]', RESEED, 0);
DBCC CHECKIDENT ('[Restaurants]', RESEED, 0);
DBCC CHECKIDENT ('[Employees]', RESEED, 0);
DBCC CHECKIDENT ('[Tables]', RESEED, 0);
DBCC CHECKIDENT ('[Reservations]', RESEED, 0);
DBCC CHECKIDENT ('[Orders]', RESEED, 0);
DBCC CHECKIDENT ('[OrderItems]', RESEED, 0);
DBCC CHECKIDENT ('[MenuItems]', RESEED, 0);


-- Inserting records into the Customer table
INSERT INTO Customers (FirstName, LastName, Email, PhoneNumber) VALUES
('John', 'Doe', 'john@example.com', '123-456-7890'),
('Jane', 'Smith', 'jane@example.com', '987-654-3210'),
('Alice', 'Johnson', 'alice@example.com', '555-123-4567'),
('Bob', 'Williams', 'bob@example.com', '444-222-3333'),
('Emily', 'Brown', 'emily@example.com', '999-888-7777');

-- Inserting records into the Restaurant table
INSERT INTO Restaurants (Name, Address, PhoneNumber, OperatingHours) VALUES
('Tasty Bites', '123 Main St, City, Country', '123-456-7890', '9:00 AM - 10:00 PM'),
('Cafe Delicious', '456 Elm St, City, Country', '987-654-3210', '8:00 AM - 9:00 PM'),
('Pizza Palace', '789 Oak St, City, Country', '555-123-4567', '10:00 AM - 11:00 PM'),
('Sushi Heaven', '321 Pine St, City, Country', '444-222-3333', '11:30 AM - 10:30 PM'),
('Burger Joint', '555 Maple St, City, Country', '999-888-7777', '11:00 AM - 9:00 PM');


-- Inserting records into the Employee table without specifying EmployeeID
INSERT INTO Employees (RestaurantID, FirstName, LastName, Position) VALUES
(1, 'John', 'Doe', 'Manager'),
(1, 'Alice', 'Smith', 'Waiter'),
(2, 'Bob', 'Johnson', 'Chef'),
(2, 'Emily', 'Williams', 'Server'),
(3, 'Jane', 'Brown', 'Hostess'),
(3, 'Michael', 'Wilson', 'Bartender'),
(4, 'Samantha', 'Jones', 'Server'),
(4, 'David', 'Martinez', 'Chef'),
(5, 'Olivia', 'Garcia', 'Waiter'),
(5, 'Daniel', 'Lee', 'Manager');


-- Inserting records into the Table table with distribution among different restaurants
INSERT INTO Tables (RestaurantID, Capacity) VALUES
(1, 4),  
(1, 2),  
(5, 6),  
(2, 3),   
(4, 5),  
(3, 8),   
(3, 10), 
(3, 12); 



-- Inserting records into the Reservation table with random future dates between 3 and 10 days from now
INSERT INTO Reservations (CustomerID, RestaurantID, TableID, ReservationDate, PartySize)
VALUES
(1, 1, 1, DATEADD(day, RAND()*(10-3)+3, GETDATE()), 4),
(2, 1, 2, DATEADD(day, RAND()*(10-3)+3, GETDATE()), 2),
(3, 2, 3, DATEADD(day, RAND()*(10-3)+3, GETDATE()), 6),
(4, 2, 4, DATEADD(day, RAND()*(10-3)+3, GETDATE()), 3),
(5, 3, 5, DATEADD(day, RAND()*(10-3)+3, GETDATE()), 5),
(2, 3, 6, DATEADD(day, RAND()*(10-3)+3, GETDATE()), 8),
(3, 4, 7, DATEADD(day, RAND()*(10-3)+3, GETDATE()), 10),
(1, 5, 8, DATEADD(day, RAND()*(10-3)+3, GETDATE()), 12);


-- Inserting records into the Order table
INSERT INTO Orders (ReservationID, EmployeeID, TotalAmount, OrderDate) VALUES
(1, 1, 50.00, GETDATE()),
(3, 2, 35.00, GETDATE()),
(5, 3, 60.00, GETDATE()),
(6, 4, 25.00, GETDATE()),
(3, 5, 70.00, GETDATE()),
(7, 6, 45.00, GETDATE()),
(8, 7, 55.00, GETDATE());


-- Inserting records into the Item table for Restaurant 
INSERT INTO MenuItems(RestaurantID, Name, Description, Price) VALUES
(1, 'Cheeseburger', 'Classic beef burger with cheddar cheese', 8.99),
(1, 'Caesar Salad', 'Romaine lettuce, croutons, parmesan cheese', 6.99),
(1, 'Chicken Sandwich', 'Grilled chicken breast with lettuce and mayo', 7.99),
(1, 'Margarita Pizza', 'Classic pizza with tomato sauce and mozzarella cheese', 10.99),
(1, 'Spaghetti Bolognese', 'Spaghetti pasta with meat sauce', 9.99);

INSERT INTO MenuItems (RestaurantID, Name, Description, Price) VALUES
(2, 'Sushi Combo', 'Assorted sushi rolls and sashimi', 18.99),
(2, 'Chicken Teriyaki', 'Grilled chicken with teriyaki sauce', 14.99),
(2, 'Tempura Roll', 'Deep-fried shrimp and vegetable sushi roll', 12.99),
(2, 'Beef Ramen', 'Japanese noodle soup with beef and vegetables', 11.99),
(2, 'Miso Soup', 'Traditional Japanese soup with tofu and seaweed', 4.99);

INSERT INTO MenuItems (RestaurantID, Name, Description, Price) VALUES
(3, 'Margherita Pizza', 'Tomato sauce, mozzarella cheese, basil', 10.99),
(3, 'Pepperoni Pizza', 'Tomato sauce, mozzarella cheese, pepperoni', 12.99),
(3, 'Garlic Breadsticks', 'Freshly baked breadsticks with garlic butter', 6.99),
(3, 'Caesar Salad', 'Romaine lettuce, croutons, parmesan cheese', 8.99),
(3, 'Chicken Alfredo Pasta', 'Pasta with grilled chicken and creamy Alfredo sauce', 13.99);

INSERT INTO MenuItems (RestaurantID, Name, Description, Price) VALUES
(4, 'Pho', 'Vietnamese noodle soup with beef or chicken', 9.99),
(4, 'Spring Rolls', 'Fresh vegetables and shrimp rolled in rice paper', 6.99),
(4, 'Pad Thai', 'Thai stir-fried rice noodles with shrimp and peanuts', 11.99),
(4, 'Green Curry', 'Thai curry with chicken, eggplant, and basil', 12.99),
(4, 'Tom Yum Soup', 'Spicy Thai soup with shrimp and lemongrass', 8.99);

INSERT INTO MenuItems (RestaurantID, Name, Description, Price) VALUES
(5, 'BBQ Ribs', 'Slow-cooked pork ribs with BBQ sauce', 15.99),
(5, 'Grilled Salmon', 'Fresh salmon fillet grilled to perfection', 17.99),
(5, 'Caesar Salad', 'Romaine lettuce, croutons, parmesan cheese', 8.99),
(5, 'New York Strip Steak', 'Grilled steak served with mashed potatoes and vegetables', 21.99),
(5, 'Vegetable Stir Fry', 'Assorted vegetables stir-fried in a savory sauce', 12.99);


-- Inserting records into the OrderItem table
INSERT INTO OrderItems (OrderID, ItemID, Quantity) VALUES
(2, 1, 2),  -- 2 items ordered for OrderID 1
(2, 2, 1),  -- 1 item ordered for OrderID 1
(3, 3, 3),  -- 3 items ordered for OrderID 2
(3, 4, 2),  -- 2 items ordered for OrderID 2
(4, 5, 1),  -- 1 item ordered for OrderID 3
(4, 6, 2);  -- 2 items ordered for OrderID 3