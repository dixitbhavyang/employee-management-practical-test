# Employee Management API (Practical Test)

## APIs

### 1. Add Employee

**POST** `/api/employees`

------------------------------------------------------------------------

### 2. Get All Employees (with Pagination & Filters)

**GET** `/api/employees`

**Query Parameters (Optional):** 
- `page` (default: 1)\
- `pageSize` (default: 10)\
- `department`\
- `minSalary`

**Example:**\
GET /api/employees?department=HR&minSalary=20000&page=1&pageSize=10

------------------------------------------------------------------------

### 3. Get Employee by Id

**GET** `/api/employees/{id}`

------------------------------------------------------------------------

### 4. Update Employee

**PUT** `/api/employees/{id}`

------------------------------------------------------------------------

### 5. Delete Employee (Soft Delete)

**DELETE** `/api/employees/{id}`

This will set `IsActive = false` instead of permanently deleting the
record.

------------------------------------------------------------------------

### 6. Search Employees (Partial Match)

**GET** `/api/employees/search?name=mah`

------------------------------------------------------------------------

### 7. Bulk Insert Employees

**POST** `/api/employees/bulk-insert`
