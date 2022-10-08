USE TestDb

insert into Product (Name) 
values ('xps'), ('dell latitude'), ('macbook pro'), ('macbook air')

insert into ProductVersion (ProductID, Name, CreatedDate, Width, Height, Length) 
values ((select ID from Product WHERE Name = 'xps'), '9500', getDate(), 199, 211, 100); 

insert into ProductVersion (ProductID, Name, CreatedDate, Width, Height, Length) 
values ((select ID from Product WHERE Name = 'xps'), '9900', getDate(), 220, 231, 150); 

insert into ProductVersion (ProductID, Name, CreatedDate, Width, Height, Length) 
values ((select ID from Product WHERE Name = 'dell latitude'), 'AA211C', getDate(), 244, 151, 200); 

insert into ProductVersion (ProductID, Name, CreatedDate, Width, Height, Length) 
values ((select ID from Product WHERE Name = 'macbook pro'), '99822', getDate(), 244, 151, 200); 

insert into ProductVersion (ProductID, Name, CreatedDate, Width, Height, Length) 
values ((select ID from Product WHERE Name = 'macbook pro'), '11221', getDate(), 244, 151, 200); 

insert into ProductVersion (ProductID, Name, CreatedDate, Width, Height, Length) 
values ((select ID from Product WHERE Name = 'macbook pro'), '88888', getDate(), 244, 151, 200); 

insert into ProductVersion (ProductID, Name, CreatedDate, Width, Height, Length) 
values ((select ID from Product WHERE Name = 'macbook air'), 'A7777', getDate(), 180, 140, 150); 
