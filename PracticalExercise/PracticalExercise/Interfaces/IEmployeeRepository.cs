using PracticalExercise.Entities;

namespace PracticalExercise.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> AddAsync(Employee employee);
        Task<(List<Employee> Items, int TotalCount)> GetAllAsync(int page, int pageSize, string? department, decimal? minSalary);
        Task<Employee?> GetByIdAsync(int id);
        Task<Employee> UpdateAsync(Employee employee);
        Task<bool> DeleteAsync(int id);
        Task<List<Employee>> GetBySearchAsync(string? name);
        Task<bool> AddBulkAsync(List<Employee> employees);
        Task<int> SaveChangesAsync();
    }
}
