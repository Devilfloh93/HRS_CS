use human_resource_app;

create table work_time(
employee_id int unsigned,
work_time_in_sec int unsigned default 0,
work_date datetime,
foreign key (employee_id) references employee(employee_id)
)