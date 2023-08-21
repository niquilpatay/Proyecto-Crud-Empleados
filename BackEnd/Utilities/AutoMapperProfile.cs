using AutoMapper;
using BackEnd.DTOs;
using BackEnd.Models;
using System.Globalization;

namespace BackEnd.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            #region Departamento
            CreateMap<Departamento, DepartamentoDTO>().ReverseMap();
            #endregion

            #region Empleado
            CreateMap<Empleado, EmpleadoDTO>()
                .ForMember(destino => destino.NombreDepartamento,//destino => EmpleadoDTO
                opt => opt.MapFrom(origen => origen.IdDepartamentoNavigation.Nombre)) //origen => Empleado
                .ForMember(destino => destino.FechaContrato,//destino => EmpleadoDTO
                opt => opt.MapFrom(origen => origen.FechaContrato.Value.ToString("dd/MM/yyyy"))); //origen => Empleado

            CreateMap<EmpleadoDTO, Empleado>()
                .ForMember(destino => destino.IdDepartamentoNavigation,//destino => Empleado
                opt => opt.Ignore()) //origen => EmpleadoDTO
                .ForMember(destino => destino.FechaContrato,//destino => Empleado
                opt => opt.MapFrom(origen => DateTime.ParseExact(origen.FechaContrato,"dd/MM/yyyy",CultureInfo.InvariantCulture))); //origen => EmpleadoDTO
            #endregion
        }
    }
}
