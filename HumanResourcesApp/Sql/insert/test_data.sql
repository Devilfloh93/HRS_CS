use human_resource_app;

insert into personal_data (
firstname, middlename, lastname, gender, birth_date, street_adress, postal_code, city, state, country, email, phone
) values ("Florian", "", "Pötzsch", "männlich",  "1993-08-13 13:23:44", "Boxberger Str. 13", "01239", "Dresden", "Sachsen", "Deutschland", "poetzsch@gmx.de", "00191393189");

Insert into employee  (
salary, max_holidays, person_id
) values (2000.30, 20, 1);

Insert into department (
department_name, employee_id
) value ("Test", 1);

Insert into holiday (
employee_id, holiday_date
) values (1, "2023-11-13 13:23:44");

Insert into login (
employee_id, login_name, login_password
) values (1, "test", "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08");

Insert into sick_day (
employee_id, sick_date
) values (1, "2022-08-06 13:23:44");

