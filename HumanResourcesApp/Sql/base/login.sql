use human_resource_app;

create table login(
employee_id int unsigned,
login_name varchar(64),
login_password varchar(64),
unique key (login_name),
primary key (employee_id),
foreign key (employee_id) references employee(employee_id)
)