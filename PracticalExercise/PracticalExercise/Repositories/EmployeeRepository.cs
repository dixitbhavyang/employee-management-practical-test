using Microsoft.EntityFrameworkCore;
using PracticalExercise.Data;
using PracticalExercise.Entities;
using PracticalExercise.Interfaces;

namespace PracticalExercise.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> AddAsync(Employee employee)
        {   
            await _context.Employees.AddAsync(employee);
            return employee;
        }

        public async Task<(List<Employee> Items, int TotalCount)> GetAllAsync(int page, int pageSize, string? department, decimal? minSalary)
        {
            var query = _context.Employees.Where(e => e.IsActive);

            if (!string.IsNullOrWhiteSpace(department))
            {
                query = query.Where(e => e.Department.ToLower().Contains(department.ToLower()));
            }
            
            if (minSalary.HasValue)
            {
                query = query.Where(e => e.Salary >= minSalary);
            }

            var totalCount = await query.CountAsync();

            var items = await query.OrderBy(e => e.Id).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return (items, totalCount);
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            return employee;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return false;

            employee.IsActive = false;
            employee.ModifiedDate = DateTime.UtcNow;

            return true;
        }

        public async Task<List<Employee>> GetBySearchAsync(string? name)
        {
            var query = _context.Employees.Where(e => e.IsActive);

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(e => e.Name.ToLower().Contains(name.ToLower()));
            }

            return await query.ToListAsync();
        }

        public async Task<bool> AddBulkAsync(List<Employee> employees)
        {
            await _context.Employees.AddRangeAsync(employees);
            return true;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
