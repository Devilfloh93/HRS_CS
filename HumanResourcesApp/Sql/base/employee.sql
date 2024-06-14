use human_resource_app;

create table employee(
employee_id int unsigned auto_increment,
salary decimal(10,2),
max_holidays int unsigned,
job_title varchar(255),
person_id int unsigned,
primary key (employee_id),
unique key (person_id),
foreign key (person_id) references personal_data(person_id)
)