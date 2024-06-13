use human_resource_app;

create table employee(
employee_id int unsigned auto_increment,
salary decimal(10,2),
max_holidays int unsigned,
person_id int unsigned,
primary key (employee_id),
unique key (person_id),
foreign key (person_id) references personal_data(person_id)
)