use human_resource_app;

create table break_time(
employee_id int unsigned,
break_time_in_sec int unsigned default 0,
created_at datetime,
foreign key (employee_id) references employee(employee_id)
)