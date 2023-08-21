using Microsoft.EntityFrameworkCore;
using BackEnd.Models;
using BackEnd.Services;
using Microsoft.JSInterop.Infrastructure;

namespace BackEnd.Services.Implementation
{
    public class EmpleadoService : IEmpleadoService
    {
        private DbempleadoAngularContext _dbContext;

        public EmpleadoService(DbempleadoAngularContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Empleado>> GetListEmpleado()
        {
            try
            {
                List<Empleado> lista = new List<Empleado>();
                lista = await _dbContext.Empleados.Include(dpt => dpt.IdDepartamentoNavigation).ToListAsync();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Empleado> GetEmpleado(int idEmpleado)
        {
            try
            {
                Empleado? encontrado = new Empleado();
                encontrado = await _dbContext.Empleados.Include(dpt => dpt.IdDepartamentoNavigation).Where(e => e.IdEmpleado == idEmpleado).FirstOrDefaultAsync();
                return encontrado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Empleado> AddEmpleado(Empleado e)
        {
            try
            {
                _dbContext.Empleados.Add(e);
                await _dbContext.SaveChangesAsync();
                return e;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateEmpleado(Empleado e)
        {
            try
            {
                _dbContext.Empleados.Update(e);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteEmpleado(Empleado e)
        {
            try
            {
                _dbContext.Empleados.Remove(e);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
