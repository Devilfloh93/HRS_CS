use human_resource_app;

create table sick_day(
employee_id int unsigned,
sick_date datetime,
foreign key (employee_id) references employee(employee_id)
)