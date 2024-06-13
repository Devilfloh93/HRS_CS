use human_resource_app;

create table holiday(
employee_id int unsigned,
holiday_date datetime,
foreign key (employee_id) references employee(employee_id)
)