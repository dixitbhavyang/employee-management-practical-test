using PracticalExercise.DTOs;

namespace PracticalExercise.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeDTO> CreateEmployeeAsync(CreateEmployeeRequest request);
        Task<PagedResult<EmployeeDTO>> GetAllEmployeesAsync(int page, int pageSize, string? department, decimal? minSalary);
        Task<EmployeeDTO> GetEmployeeByIdAsync(int employeeId);
        Task<EmployeeDTO> UpdateEmployeeAsync(int id, UpdateEmployeeRequest request);
        Task<bool> DeleteEmployeeAsync(int employeeId);
        Task<List<EmployeeDTO>> GetEmployeesBySearchAsync(string? name);
        Task<bool> CreateEmployeesAsync(List<CreateEmployeeRequest> requests);
    }
}
