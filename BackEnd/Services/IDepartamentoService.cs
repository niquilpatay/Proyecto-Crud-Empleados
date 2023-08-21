using BackEnd.Models;

namespace BackEnd.Services
{
    public interface IDepartamentoService
    {
        Task<List<Departamento>> GetListDepartamento();
    }
}
