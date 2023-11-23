CREATE DATABASE Project_63135741;

USE Project_63135741;

CREATE TABLE KindOfMenu (
    KindID NVARCHAR(10) PRIMARY KEY,
    NameOfKind NVARCHAR(50) NOT NULL
);

CREATE TABLE Foods (
    FoodsID VARCHAR(10) PRIMARY KEY,
	PicFood NVARCHAR(MAX), 
    FoodsName NVARCHAR(50) NOT NULL,
	Descrip NVARCHAR(200) NOT NULL,
    Price DECIMAL(10,2), 
	KindID NVARCHAR(10) NOT NULL FOREIGN KEY REFERENCES KindOfMenu(KindID)
	ON UPDATE CASCADE
	ON DELETE CASCADE
);

CREATE TABLE BookTable (
    BookTableID NVARCHAR(10) PRIMARY KEY,
	NameCus NVARCHAR(MAX), 
    PhoneCus NVARCHAR(50) NOT NULL,
	QuantityPP NVARCHAR(50) NOT NULL,
    TimeBook Time NOT NULL, 
	DayBook Date NOT NULL, 
	Note NVARCHAR(200) NOT NULL
);

INSERT INTO BookTable (BookTableID, NameCus, PhoneCus, QuantityPP, TimeBook, DayBook, Note)
VALUES ('TB001', 'John Doe', '123-456-7890', '2', '10:00:00', '2023-11-22', 'Window seat, please.');

INSERT INTO KindOfMenu (KindID, NameOfKind) 
VALUES 
    ('Su', N'Sushi'),
	('So', N'Soup'),
	('No', N'Noodles'),
	('Ra', N'Raw Dish'),
	('Co', N'Cooked Dish'),
	('Be', N'Bean'),
    ('Wi', N'Wine'),
	('Te', N'Tea'),
	('Wa', N'Water'),
	('Ma', N'Matcha'),
	('Sof', N'Soft Drink')

INSERT INTO Foods 
VALUES 
    ('F01', N'Sushi.jpg', N'Sushi', N'Delicious bite-sized rolls', 15.99, 'Su'),
    ('F02', N'Tempura.jpg', N'Tempura', N'Crispy battered seafood or vegetables', 12.50, 'No'),
    ('F03', N'MisoSoup.jpg', N'Miso Soup', N'Savory soy-based soup with tofu and seaweed', 7.25, 'So'),
    ('F04', N'Gyoza.jpg', N'Gyoza', N'Pan-fried dumplings with a savory filling', 9.75, 'Co'),
    ('F05', N'Takoyaki.jpg', N'Takoyaki', N'Octopus-filled savory balls', 10.99, 'Co'),
    ('F06', N'Sashimi.jpg', N'Sashimi', N'Fresh slices of raw fish', 18.50, 'Ra'),
    ('F07', N'Okonomiyaki.jpg', N'Okonomiyaki', N'Savory Japanese pancake with various fillings', 13.75, 'Co'),
    ('F08', N'Edamame.jpg', N'Edamame', N'Boiled young soybeans served with salt', 6.99, 'Be'),
	('F09', N'Ramen.jpg', N'Ramen', N'Delicious noodle soup', 11.99, 'No'),
    ('F10', N'TeriyakiChicken.jpg', N'Teriyaki Chicken', N'Grilled chicken with teriyaki sauce', 14.50, 'Co'),
    ('F11', N'Tonkatsu.jpg', N'Tonkatsu', N'Pork cutlet with special sauce', 13.25, 'Co'),
    ('F12', N'Yakitori.jpg', N'Yakitori', N'Grilled skewered chicken', 10.75, 'Co'),
    ('F13', N'Sukiyaki.jpg', N'Sukiyaki', N'Japanese hot pot', 19.99, 'No'),
    ('F14', N'Udon.jpg', N'Udon', N'Japanese thick noodle soup', 12.50, 'No'),
    ('F15', N'Soba.jpg', N'Soba', N'Buckwheat noodles', 9.75, 'No'),
    ('F16', N'TonkotsuRamen.jpg', N'Tonkotsu Ramen', N'Ramen with rich pork bone broth', 15.99, 'No'),
	('F17', N'GreenTea.jpg', N'Green Tea', N'Japanese green tea, refreshing and aromatic', 3.99, 'Te'),
    ('F18', N'Sake.jpg', N'Sake', N'Japanese rice wine, traditional and rich in flavor', 8.50, 'Wi'),
    ('F19', N'MatchaLatte.jpg', N'Matcha Latte', N'Japanese green tea latte, creamy and indulgent', 5.25, 'Ma'),
    ('F20', N'YuzuCitrusSoda.jpg', N'Yuzu Citrus Soda', N'Refreshing soda with a hint of yuzu citrus', 4.75, 'Sof'),
    ('F21', N'PlumWine.jpg', N'Plum Wine', N'Sweet and tart Japanese plum wine', 9.99, 'Wi'),
    ('F22', N'RamenBrothMartini.jpg', N'Ramen Broth Martini', N'A unique cocktail inspired by ramen flavors', 12.50, 'So'),
    ('F23', N'Calpico.jpg', N'Calpico', N'Japanese uncarbonated soft drink, mild and creamy', 3.75, 'Wa'),
    ('F24', N'Shiboritate.jpg', N'Shiboritate', N'Freshly pressed sake, known for its fruity taste', 11.99, 'Wi'),
	('F25', N'Hojicha.jpg', N'Hojicha', N'Roasted Japanese green tea, aromatic and nutty', 4.25, 'Te'),
    ('F26', N'Shochu.jpg', N'Shochu', N'Japanese distilled beverage, similar to vodka', 7.99, 'Wi'),
    ('F27', N'Amazake.jpg', N'Amazake', N'Sweet, low-alcohol Japanese drink made from fermented rice', 6.50, 'So'),
    ('F28', N'Chuhai.jpg', N'Chuhai', N'Japanese highball cocktail with shochu or vodka', 8.75, 'Wi'),
    ('F29', N'Umeshu.jpg', N'Umeshu', N'Sweet and sour plum liqueur, often served on the rocks', 10.50, 'Wi'),
	('F30', N'Cocacola.jpg', N'Cocacola', N'carbonated soft drinks', 20.50, 'Sof');

---------------------------------------------------------------------------
CREATE PROCEDURE Find_Menu
    @MenuName NVARCHAR(50) = NULL,
    @KindID NVARCHAR(10) = NULL
AS
BEGIN
    DECLARE @SqlStr NVARCHAR(4000),
			@ParamList nvarchar(2000)
    SELECT @SqlStr = '
       SELECT * 
       FROM Foods
       WHERE 1=1'

    IF @MenuName IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (FoodsName LIKE ''%' + @MenuName + '%'')'

    IF @KindID IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (KindID LIKE ''%' + @KindID + '%'')'

    EXEC sp_executesql @SqlStr
END;

EXEC Find_Menu @MenuName = 'Sushi', @KindID = 'F'
