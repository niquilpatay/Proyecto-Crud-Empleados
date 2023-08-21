using Microsoft.EntityFrameworkCore;
using BackEnd.Models;
using BackEnd.Services;

namespace BackEnd.Services.Implementation
{
    public class DepartamentoService : IDepartamentoService
    {
        private DbempleadoAngularContext _dbContext;

        public DepartamentoService(DbempleadoAngularContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Departamento>> GetListDepartamento()
        {
            try
            {
                List<Departamento> lista = new List<Departamento>();
                lista = await _dbContext.Departamentos.ToListAsync();   
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
