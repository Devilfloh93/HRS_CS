use human_resource_app;

create table department(
department_id int unsigned auto_increment,
department_name varchar(255),
employee_id int unsigned,
primary key (department_id),
unique key (employee_id),
foreign key (employee_id) references employee(employee_id)
)