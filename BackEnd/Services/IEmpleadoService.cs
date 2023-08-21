using BackEnd.Models;

namespace BackEnd.Services
{
    public interface IEmpleadoService
    {
        Task<List<Empleado>> GetListEmpleado();
        Task<Empleado> GetEmpleado(int idEmpleado);
        Task<Empleado> AddEmpleado(Empleado e);
        Task<bool> UpdateEmpleado(Empleado e);
        Task<bool> DeleteEmpleado(Empleado e);
    }
}
