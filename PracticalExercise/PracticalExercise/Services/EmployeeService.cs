using PracticalExercise.DTOs;
using PracticalExercise.Entities;
using PracticalExercise.Exceptions;
using PracticalExercise.Interfaces;

namespace PracticalExercise.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeDTO> CreateEmployeeAsync(CreateEmployeeRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                throw new ValidationException("Name should not be empty");
            }

            if (request.Salary <= 0)
            {
                throw new ValidationException("Salary should be > 0");
            }

            var employee = new Employee
            {
                Name = request.Name,
                Salary = request.Salary,
                Department = request.Department,
                CreatedDate = DateTime.UtcNow,
                IsActive = true
            };

            var createdEmployee = await _employeeRepository.AddAsync(employee);
            await _employeeRepository.SaveChangesAsync();

            var result = await _employeeRepository.GetByIdAsync(createdEmployee.Id);

            return MapToDTO(result!);
        }

        public async Task<PagedResult<EmployeeDTO>> GetAllEmployeesAsync(int page, int pageSize, string? department, decimal? minSalary)
        {
            if (page < 1)
                throw new ValidationException("Page number must be greater than 0");

            if (pageSize < 1 || pageSize > 100)
                throw new ValidationException("Page size must be between 1 and 100");

            var (employees, totalCount) = await _employeeRepository.GetAllAsync(page, pageSize, department, minSalary);

            var employeeDTOs = employees.Select(e => MapToDTO(e)).ToList();

            return new PagedResult<EmployeeDTO>
            {
                Items = employeeDTOs,
                TotalRecords = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<EmployeeDTO> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
            {
                throw new NotFoundException("Employee", id);
            }

            return MapToDTO(employee);
        }
        public async Task<EmployeeDTO> UpdateEmployeeAsync(int id, UpdateEmployeeRequest request)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                throw new NotFoundException("Employee", id);
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                throw new ValidationException("Name should not be empty");
            }

            if (request.Salary <= 0)
            {
                throw new ValidationException("Salary should be > 0");
            }

            employee.Name = request.Name;
            employee.Department = request.Department;
            employee.Salary = request.Salary;
            employee.ModifiedDate = DateTime.UtcNow;

            await _employeeRepository.UpdateAsync(employee);
            await _employeeRepository.SaveChangesAsync();

            var result = await _employeeRepository.GetByIdAsync(employee.Id);

            return MapToDTO(result!);
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                throw new NotFoundException("Employee", id);
            }

            var isDeleted = await _employeeRepository.DeleteAsync(id);
            if (isDeleted)
            {
                await _employeeRepository.SaveChangesAsync();
            }

            return isDeleted;
        }

        public async Task<List<EmployeeDTO>> GetEmployeesBySearchAsync(string? name)
        {
            var employees = await _employeeRepository.GetBySearchAsync(name);
            var employeeDTOs = employees.Select(e => MapToDTO(e)).ToList();

            return employeeDTOs;
        }

        public async Task<bool> CreateEmployeesAsync(List<CreateEmployeeRequest> requests)
        {
            if (requests.Any(r => string.IsNullOrEmpty(r.Name)))
            {
                throw new ValidationException("Name should not be empty");
            }

            if (requests.Any(r => r.Salary <= 0))
            {
                throw new ValidationException("Salary should be > 0");
            }

            var employees = requests.Select(r => new Employee
            {
                Name = r.Name,
                Salary = r.Salary,
                Department = r.Department,
                CreatedDate = DateTime.UtcNow,
                IsActive = true
            }).ToList();

            var createdEmployee = await _employeeRepository.AddBulkAsync(employees);
            await _employeeRepository.SaveChangesAsync();

            return true;
        }

        private EmployeeDTO MapToDTO(Employee employee)
        {
            return new EmployeeDTO
            {
                Id = employee.Id,
                Name = employee.Name,
                Department = employee.Department,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                CreatedDate = employee.CreatedDate
            };
        }
    }
}
