--1. Insert 20-25 sample records
insert into Employees (Name, Salary, Department, CreatedDate, ModifiedDate, IsActive)
values
('test user 1', 45000.00, 'Engineering', '2026-01-15', NULL, 1),
('test user 2', 52000.00, 'Marketing', '2026-02-01', '2026-03-10', 1),
('test user 3', 61000.00, 'Engineering', '2026-02-20', NULL, 1),
('test user 4', 38000.00, 'HR', '2026-03-05', '2026-01-22', 1),
('test user 5', 73000.00, 'Finance', '2026-03-18', NULL, 1),
('test user 6', 47000.00, 'Marketing', '2026-04-02', '2026-11-15', 0),
('test user 7', 55000.00, 'Operations', '2026-04-25', NULL, 1),
('test user 8', 41000.00, 'HR', '2026-05-10', '2026-02-08', 1),
('test user 9', 68000.00, 'Engineering', '2026-05-30', NULL, 1),
('test user 10', 49000.00, 'Finance', '2026-06-14', '2026-12-20', 1),
('test user 11', 57000.00, 'Engineering', '2026-07-01', NULL, 1),
('test user 12', 43000.00, 'Marketing', '2026-07-19', '2026-04-01', 0),
('test user 13', 80000.00, 'Finance', '2026-08-05', NULL, 1),
('test user 14', 46000.00, 'Operations', '2026-08-22', '2026-01-05', 1),
('test user 15', 63000.00, 'Engineering', '2026-09-10', NULL, 1),
('test user 16', 39000.00, 'HR', '2026-09-28', '2026-10-30', 1),
('test user 17', 58000.00, 'Operations', '2026-10-15', NULL, 1),
('test user 18', 51000.00, 'Marketing', '2026-11-02', '2026-03-25', 1),
('test user 19', 70000.00, 'Engineering', '2026-11-20', '2026-03-25', 0),
('test user 20', 44000.00, 'Finance', '2026-12-07', '2026-02-14', 1),
('test user 21', 77000.00, 'Engineering', '2026-01-10', NULL, 1),
('test user 22', 36000.00, 'HR', '2026-01-28', '2026-04-10', 1),
('test user 23', 62000.00, 'Operations', '2026-02-14', NULL, 1),
('test user 24', 48000.00, 'Marketing', '2026-03-01', '2026-04-20', 1),
('test user 25', 85000.00, NULL, '2026-03-18', NULL, 1);

--2. Write query to: Get all active employees
select *from Employees where IsActive = 1 

--Max Salary
select max(Salary) from Employees 
--3. Find: 2nd highest salary
select top 1 *from Employees where Salary < (select MAX(Salary) from Employees) order by Salary desc

--6. Department-wise employee count
select Department, count(Id) from Employees group by Department

--AVG Salary
select Department, avg(Salary) from Employees group by Department
--7. Get Employees Above Department Average Salary
select *from Employees e1 where Salary > (select avg(Salary) from Employees e2 group by Department having e1.Department = e2.Department)