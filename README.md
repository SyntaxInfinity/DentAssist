# DentAssist - Sistema de Gestión Dental

## Descripción
Aplicación web para gestión de clínicas dentales desarrollada en ASP.NET Core MVC. Permite administrar pacientes, turnos, odontólogos y tratamientos de manera eficiente.

## Tecnologías Utilizadas
- **Backend**: ASP.NET Core 6.0+, Entity Framework Core
- **Frontend**: Razor Pages, Bootstrap 5
- **Base de Datos**: SQL Server (o SQLite para desarrollo)
- **Autenticación**: ASP.NET Identity (opcional)

## Requisitos
- .NET 8.0
- SQL Server (recomendado) o SQLite
- Visual Studio 2022 (recomendado) o VS Code con extensión C#
- Git (opcional)

## Instalación

### 1. Clonar el repositorio
```bash
git clone https://github.com/tu-usuario/DentAssist.git
cd DentAssist

Configurar la base de datos
Editar el archivo appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=TU_SERVIDOR;Database=DentAssist;User=tu_usuario;Password=tu_contraseña;"

Aplicar migraciones
dotnet ef database update

Ejecutar la aplicación
dotnet run

Uso Básico
Credenciales de prueba
Administrador: admin@dentassist.cl / Admin123!

Recepcionista: recepcion@dentassist.cl / Recepcion123!

Odontólogo: odontologo@dentassist.cl / Odontologo123!

Inserts base de datos credenciales de prueba
los siguientes inserts son para poder ejecutarlos en la base de datos y almacenar las credenciales de prueba

este insert va en la tabla de usuarios
USE [DentAssist]
GO

INSERT INTO [dbo].[usuarios]
           ([Nombre]
           ,[Correo]
           ,[Password]
           ,[Rol])
     VALUES
           ('Administrador', 'admin@dentassist.cl', 'Admin123', 'Administrador'),
		   ('Recepcionista', 'recepcion@dentassist.cl', 'Recepcion123', 'Recepcionista')
GO

el siguiente insert va en la tabla de odontologos
USE [DentAssist]
GO

INSERT INTO [dbo].[odontologos]
           ([Nombre]
           ,[Matricula]
           ,[Email]
           ,[Password]
           ,[EspecialidadId])
     VALUES
           ('Odontologo', 'CD-45678', 'odontologo@dentassist.cl', 'Odontologo123', 1)
GO

Funcionalidades por rol
Administrador	Gestión de odontólogos y tratamientos
Recepcionista	Agenda de turnos y administración de pacientes
Odontólogo	Consulta de agenda y creación de planes de tratamiento

Primeros Pasos
Iniciar sesión con las credenciales de prueba
Explorar el menú según tu rol asignado
Para administradores:
Registrar nuevos odontólogos en "Odontólogos > Nuevo"
Agregar tratamientos disponibles en "Tratamientos"

Estructura del proyecto
DentAssist/
├── Controllers/       
├── Models/           
├── Views/           
├── Migrations/        
├── wwwroot/           
├── appsettings.json   
└── Program.cs         
