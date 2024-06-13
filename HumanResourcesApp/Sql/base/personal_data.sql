use human_resource_app;

Create Table if not exists personal_data (
person_id int unsigned auto_increment,
firstname varchar(255),
middlename varchar(255),
lastname varchar(255),
gender varchar(120),
birth_date datetime,
street_adress varchar(255),
postal_code varchar(20),
city varchar(255),
state varchar(255),
country varchar(255),
email varchar(255),
phone varchar(255),
primary key (person_id)
)