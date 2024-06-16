use human_resource_app;

create table work_schedule(
employee_id int unsigned,
schedule_start datetime,
schedule_end datetime,
foreign key (employee_id) references employee(employee_id)
)