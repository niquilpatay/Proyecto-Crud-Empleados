using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using BackEnd.Services;
using BackEnd.Services.Implementation;
using AutoMapper;
using BackEnd.DTOs;
using BackEnd.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AGREGAR DBCONTEXT
builder.Services.AddDbContext<DbempleadoAngularContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("connectionString"));
});

// INYECCION SERVICIOS
builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();

// AGREGAR AUTOMAPPER
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// AGREGAR CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

  #region Peticiones API Rest

//GET DEPARTAMENTOS
app.MapGet("/departamento/lista", async (IDepartamentoService _departamentoService, IMapper _mapper) =>
{
    var listaDepartamento = await _departamentoService.GetListDepartamento();
    var listaDepartamentoDTO = _mapper.Map<List<DepartamentoDTO>>(listaDepartamento);

    if(listaDepartamentoDTO.Count > 0)
    {
        return Results.Ok(listaDepartamentoDTO);
    }
    else
    {
        return Results.NotFound();
    }
});

//GET EMPLEADOS
app.MapGet("/empleado/lista", async (IEmpleadoService _empleadoService, IMapper _mapper) =>
{
    var listaEmpleado = await _empleadoService.GetListEmpleado();
    var listaEmpleadoDTO = _mapper.Map<List<EmpleadoDTO>>(listaEmpleado);

    if (listaEmpleadoDTO.Count > 0)
    {
        return Results.Ok(listaEmpleadoDTO);
    }
    else
    {
        return Results.NotFound();
    }
});

//POST EMPLEADO
app.MapPost("/empleado/guardar", async (EmpleadoDTO modelo, IEmpleadoService _empleadoService, IMapper _mapper) =>
{
    var _empleado = _mapper.Map<Empleado>(modelo);
    var _empleadoCreado = await _empleadoService.AddEmpleado(_empleado);

    if (_empleadoCreado.IdEmpleado != 0)
    {
        return Results.Ok(_mapper.Map<EmpleadoDTO>(_empleadoCreado));
    }
    else
    {
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
    }
});

//PUT EMPLEADO
app.MapPut("/empleado/actualizar/{idEmpleado}", async (int idEmpleado, EmpleadoDTO modelo, IEmpleadoService _empleadoService, IMapper _mapper) =>
{
    var _encontrado = await _empleadoService.GetEmpleado(idEmpleado);
    if (_encontrado is null)
    {
        return Results.NotFound();    
    }

    var _empleado = _mapper.Map<Empleado>(modelo);

    _encontrado.NombreCompleto = _empleado.NombreCompleto;
    _encontrado.IdDepartamento = _empleado.IdDepartamento;
    _encontrado.Sueldo = _empleado.Sueldo;
    _encontrado.FechaContrato = _empleado.FechaContrato;

    var respuesta = await _empleadoService.UpdateEmpleado(_encontrado);
    if (respuesta)
    {
        return Results.Ok(_mapper.Map<EmpleadoDTO>(_encontrado));
    }
    else
    {
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
    }
});

//DELETE EMPLEADO
app.MapDelete("/empleado/eliminar/{idEmpleado}", async (int idEmpleado, IEmpleadoService _empleadoService, IMapper _mapper) =>
{
    var _encontrado = await _empleadoService.GetEmpleado(idEmpleado);
    if (_encontrado is null)
    {
        return Results.NotFound();
    }

    var respuesta = await _empleadoService.DeleteEmpleado(_encontrado);
    if (respuesta)
    {
        return Results.Ok(_mapper.Map<EmpleadoDTO>(_encontrado));
    }
    else
    {
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
    }
});
#endregion

app.UseCors("NuevaPolitica");

app.Run();

//NuGets:
//Microsoft.EntityFrameworkCore.Tools
//Microsoft.EntityFrameworkCore.SqlServer
//AutoMapper
//AutoMapper.Extensions.Microsoft.DependencyInjection

//Comandos
//Scaffold-DbContext "Server=DESKTOP-M0IN372\\SQLEXPRESS; DataBase=DBEmpleadoAngular; Trusted_Connection=True; TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models