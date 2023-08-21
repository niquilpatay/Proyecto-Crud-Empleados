create database DBEmpleadoAngular

use DBEmpleadoAngular

create table Departamento(
IdDepartamento int primary key identity,
Nombre varchar(50),
FechaCreacion datetime default getdate()
)

create table Empleado(
IdEmpleado int primary key identity,
NombreCompleto varchar(50),
IdDepartamento int references Departamento(IdDepartamento),
Sueldo int,
FechaContrato datetime default getdate()
)

insert into Departamento(nombre) values
('Administración'),
('Marketing'),
('Ventas'),
('Comercio')

insert into Empleado(NombreCompleto,IdDepartamento,Sueldo,FechaContrato) values
('Qacho Nilpa',1,1400,getdate())