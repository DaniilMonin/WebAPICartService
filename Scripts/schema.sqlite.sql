create table CartLinks
(
    CartId INTEGER not null,
    ProductId INTEGER not null,
    Total INTEGER not null
);

create unique index CartLinks_CartId_ProductId_uindex
    on CartLinks (CartId, ProductId);

create table Carts
(
    Id INTEGER not null
        constraint Carts_pk
            primary key autoincrement,
    LastUpdateStamp TEXT
);

create unique index Carts_Id_uindex
    on Carts (Id);

create table DailyCartReports
(
    id integer not null
        constraint DailyCartReports_pk
            primary key autoincrement,
    body TEXT,
    CreationDate TEXT
);

create unique index DailyCartReports_id_uindex
    on DailyCartReports (id);

create table Hooks
(
    ServiceId TEXT not null
        constraint Hooks_pk
            primary key,
    ServiceUri TEXT not null
);

create unique index Hooks_ServiceId_uindex
    on Hooks (ServiceId);

create table Jobs
(
    Id INTEGER not null
        constraint Jobs_pk
            primary key autoincrement,
    JobId TEXT not null,
    Cron TEXT not null,
    Comment TEXT
);

create unique index Jobs_Id_uindex
    on Jobs (Id);

create table Products
(
    Id INTEGER not null
        constraint Products_pk
            primary key autoincrement,
    Name TEXT not null,
    Cost REAL not null,
    ForBonusPoints INTEGER not null
);

create unique index Products_Id_uindex
    on Products (Id);

create table TemplateReports
(
    Id INTEGER not null
        constraint TemplateReports_pk
            primary key autoincrement,
    Body TEXT not null
);

create unique index TemplateReports_Id_uindex
    on TemplateReports (Id);

CREATE VIEW DailyReporting as
select cr.LastUpdateStamp, cl.CartId, cl.ProductId, cl.Total, p.Name as ProductName, (p.ForBonusPoints * cl.Total) as TotalBonus, p.ForBonusPoints as Bonus, p.Cost as Price from CartLinks cl
                                                                                                                                                                                      left join Products p on cl.ProductId = p.Id
                                                                                                                                                                                      join Carts cr on cl.CartId = cr.Id;



INSERT INTO TemplateReports (Id, Body) VALUES (1, '<ul id=''products''>
  {{ for cart in result }}
    <li>
      <h2>{{ cart.key }}</h2>
	  {{ total = 0 }}
	  {{ cost = 0 }}
	  {{ for product in cart.value }}
		{{ fullprice = product.total | math.times product.price }}
		Name {{ product.product_name }}        	   
		Price: {{ product.price }}
		Total: {{ product.total }}	           
		Total price: {{ fullprice }}	  
		Bonus: {{ product.bonus }}	           
		Total Bonus: {{ product.total_bonus }}	       
		{{ total = total | math.plus product.total }}    
		{{ cost = cost | math.plus fullprice  }}    
	  {{ end }}
		Total in cart {{ total }}
		Total in cost {{ cost }}
		Medium {{ cost | math.divided_by total }}
    </li>
  {{ end }}
</ul>');


INSERT INTO Products (Id, Name, Cost, ForBonusPoints) VALUES (1, 'Продукт 1', 10.55, 5);
INSERT INTO Products (Id, Name, Cost, ForBonusPoints) VALUES (2, 'Продукт 2', 89.74, 1);
INSERT INTO Products (Id, Name, Cost, ForBonusPoints) VALUES (3, 'Продукт 3', 23, 0);

INSERT INTO Jobs (Id, JobId, Cron, Comment) VALUES (1, '1B75FA85-136B-46AF-A48F-579D0565D3AD', '0/10 * * * * ?', 'Simple ping job');
INSERT INTO Jobs (Id, JobId, Cron, Comment) VALUES (2, '8CEDC172-90F5-42F4-B567-EFAE91A46E89', '0/20 * * * * ?', 'Reporting');
INSERT INTO Jobs (Id, JobId, Cron, Comment) VALUES (3, '336C632E-67E2-41EF-90E5-13704B4B29AA', '0/35 * * * * ?', 'Cleaning job');

INSERT INTO Carts (Id, LastUpdateStamp) VALUES (1, null);
INSERT INTO Carts (Id, LastUpdateStamp) VALUES (2, null);

INSERT INTO Hooks (ServiceId, ServiceUri) VALUES ('E40C3DD5-AC0C-4CB5-AA42-9C663A901F03', 'http://foo.bar');
INSERT INTO Hooks (ServiceId, ServiceUri) VALUES ('ADB1C993-EE80-4CC9-BCF1-CA2A18C8F6A4', 'http://sample.org/ru');