using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "eventos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre_Evento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lugar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capacidad_Max = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArchivosJson = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contrasenia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "reportes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha_Generacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Archivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_Evento = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reportes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_reportes_eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "eventos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "inscripciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fecha_Inscripcion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_Usuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Id_Evento = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inscripciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_inscripciones_eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "eventos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_inscripciones_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "notificaciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha_Envio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_Usuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Id_Evento = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_notificaciones_eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "eventos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_notificaciones_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "encuestas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha_Creacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_Evento = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Id_Inscripcion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InscripcionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_encuestas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_encuestas_eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "eventos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_encuestas_inscripciones_InscripcionId",
                        column: x => x.InscripcionId,
                        principalTable: "inscripciones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "pagos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Monto = table.Column<double>(type: "float", nullable: true),
                    Fecha_Pago = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Metodo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_Inscripcion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InscripcionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pagos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pagos_inscripciones_InscripcionId",
                        column: x => x.InscripcionId,
                        principalTable: "inscripciones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_encuestas_EventoId",
                table: "encuestas",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_encuestas_InscripcionId",
                table: "encuestas",
                column: "InscripcionId");

            migrationBuilder.CreateIndex(
                name: "IX_inscripciones_EventoId",
                table: "inscripciones",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_inscripciones_UsuarioId",
                table: "inscripciones",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_notificaciones_EventoId",
                table: "notificaciones",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_notificaciones_UsuarioId",
                table: "notificaciones",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_pagos_InscripcionId",
                table: "pagos",
                column: "InscripcionId");

            migrationBuilder.CreateIndex(
                name: "IX_reportes_EventoId",
                table: "reportes",
                column: "EventoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "encuestas");

            migrationBuilder.DropTable(
                name: "notificaciones");

            migrationBuilder.DropTable(
                name: "pagos");

            migrationBuilder.DropTable(
                name: "reportes");

            migrationBuilder.DropTable(
                name: "inscripciones");

            migrationBuilder.DropTable(
                name: "eventos");

            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}
