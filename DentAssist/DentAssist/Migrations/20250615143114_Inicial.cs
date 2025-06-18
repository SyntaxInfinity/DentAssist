using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentAssist.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "especialidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEspecialidad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_especialidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pacientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RUT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pacientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tratamientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrecioEstimado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tratamientos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "odontologos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Matricula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EspecialidadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_odontologos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_odontologos_especialidades_EspecialidadId",
                        column: x => x.EspecialidadId,
                        principalTable: "especialidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "historialTratamientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    TratamientoId = table.Column<int>(type: "int", nullable: false),
                    OdontologoId = table.Column<int>(type: "int", nullable: false),
                    FechaRealizacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_historialTratamientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_historialTratamientos_odontologos_OdontologoId",
                        column: x => x.OdontologoId,
                        principalTable: "odontologos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_historialTratamientos_pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_historialTratamientos_tratamientos_TratamientoId",
                        column: x => x.TratamientoId,
                        principalTable: "tratamientos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "planTratamientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    OdontologoId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ObservacionesGenerales = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_planTratamientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_planTratamientos_odontologos_OdontologoId",
                        column: x => x.OdontologoId,
                        principalTable: "odontologos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_planTratamientos_pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "turnos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Duracion = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    OdontologoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_turnos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_turnos_odontologos_OdontologoId",
                        column: x => x.OdontologoId,
                        principalTable: "odontologos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_turnos_pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tratamientosPropuestos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    TratamientoId = table.Column<int>(type: "int", nullable: false),
                    FechaEstimada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoPaso = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrdenSecuencia = table.Column<int>(type: "int", nullable: false),
                    ObservacionesPaso = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tratamientosPropuestos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tratamientosPropuestos_planTratamientos_PlanId",
                        column: x => x.PlanId,
                        principalTable: "planTratamientos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tratamientosPropuestos_tratamientos_TratamientoId",
                        column: x => x.TratamientoId,
                        principalTable: "tratamientos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_historialTratamientos_OdontologoId",
                table: "historialTratamientos",
                column: "OdontologoId");

            migrationBuilder.CreateIndex(
                name: "IX_historialTratamientos_PacienteId",
                table: "historialTratamientos",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_historialTratamientos_TratamientoId",
                table: "historialTratamientos",
                column: "TratamientoId");

            migrationBuilder.CreateIndex(
                name: "IX_odontologos_EspecialidadId",
                table: "odontologos",
                column: "EspecialidadId");

            migrationBuilder.CreateIndex(
                name: "IX_planTratamientos_OdontologoId",
                table: "planTratamientos",
                column: "OdontologoId");

            migrationBuilder.CreateIndex(
                name: "IX_planTratamientos_PacienteId",
                table: "planTratamientos",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_tratamientosPropuestos_PlanId",
                table: "tratamientosPropuestos",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_tratamientosPropuestos_TratamientoId",
                table: "tratamientosPropuestos",
                column: "TratamientoId");

            migrationBuilder.CreateIndex(
                name: "IX_turnos_OdontologoId",
                table: "turnos",
                column: "OdontologoId");

            migrationBuilder.CreateIndex(
                name: "IX_turnos_PacienteId",
                table: "turnos",
                column: "PacienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "historialTratamientos");

            migrationBuilder.DropTable(
                name: "tratamientosPropuestos");

            migrationBuilder.DropTable(
                name: "turnos");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "planTratamientos");

            migrationBuilder.DropTable(
                name: "tratamientos");

            migrationBuilder.DropTable(
                name: "odontologos");

            migrationBuilder.DropTable(
                name: "pacientes");

            migrationBuilder.DropTable(
                name: "especialidades");
        }
    }
}
