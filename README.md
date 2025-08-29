# Sistema de Gestión de Eventos del Colegio de Profesionales

## Descripción

Este sistema permite planificar, administrar y promocionar eventos organizados por el Colegio de Profesionales, como cursos, conferencias, charlas, partidos deportivos y actividades recreativas.  
Los usuarios pueden inscribirse en línea, realizar pagos, recibir recordatorios automáticos, y acceder a material de apoyo. Además, el sistema genera reportes y estadísticas para mejorar la organización futura.

## Funcionalidades

### Para usuarios

- Registrarse y crear una cuenta → **F**
- Iniciar sesión → **C**
- Consultar listado de eventos → **F**
- Inscribirse en un evento → **F**
- Realizar pago en línea → **F**
- Recibir confirmación de pago → **C**
- Responder encuestas de satisfacción → **F**

### Para organizadores

- Crear nuevo evento → **F**
- Modificar información del evento → **F**
- Subir material de apoyo → **F**

### Funcionalidades automáticas del sistema

- Enviar recordatorio previo al evento → **T**
- Habilitar encuestas de satisfacción → **T**
- Generar reportes y estadísticas → **T**

### Para administradores

- Consultar reportes → **F**

## Cómo usarlo

1. Clonar el repositorio:

````bash
git clone https://github.com/Juan7139nf/Evento_colegio.git

```bash
{
  "Usuario": {
    "Nombre": "Benji",
    "Apellido": "Torres",
    "Correo": "benji@example.com",
    "Contrasenia": "Segura123!",
    "Rol": "Usuario"
  },
  "Evento": {
    "Nombre_Evento": "ComicFest Tarija",
    "Tipo": "Convención",
    "Fecha": "2025-09-15T14:00:00",
    "Lugar": "Centro Cultural",
    "Capacidad_Max": 300,
    "Estado": "Activo",
    "Content": [
      {
        "Orden": 1,
        "Title": "Bienvenida",
        "Content": "Introducción al evento y reglas generales"
      },
      {
        "Orden": 2,
        "Title": "Panel Creativo",
        "Content": "Charla con artistas locales"
      }
    ],
    "Archivos": [
      {
        "Orden": 1,
        "Url": "https://drive.google.com/comicfest-programa.pdf",
        "Tipo": "PDF"
      }
    ]
  },
  "Inscripcion": {
    "Fecha_Inscripcion": "2025-08-28T23:30:00",
    "Estado": "Confirmado",
    "Id_Usuario": "9a07a59c-05d8-417a-aa13-5d8b28d74ffc",
    "Id_Evento": "746825ea-2677-4654-aaba-3fc98f9c36b3"
  },
  "Encuesta": {
    "Titulo": "Satisfacción del Evento",
    "Fecha_Creacion": "2025-08-28T23:35:00",
    "Id_Evento": "746825ea-2677-4654-aaba-3fc98f9c36b3",
    "Id_Inscripcion": "7f0b1723-3517-4cdc-9974-fac97357bff0"
  },
  "Notificacion": {
    "Tipo": "Recordatorio",
    "Fecha_Envio": "2025-08-29T08:00:00",
    "Estado": "Enviado",
    "Id_Usuario": "9a07a59c-05d8-417a-aa13-5d8b28d74ffc",
    "Id_Evento": "746825ea-2677-4654-aaba-3fc98f9c36b3"
  },
  "Pago": {
    "Monto": 50.0,
    "Fecha_Pago": "2025-08-28T23:40:00",
    "Metodo": "Transferencia",
    "Estado": "Completado",
    "Id_Inscripcion": "7f0b1723-3517-4cdc-9974-fac97357bff0"
  },
  "Reporte": {
    "Tipo": "Estadísticas de Participación",
    "Fecha_Generacion": "2025-08-29T10:00:00",
    "Archivo": "reporte_participacion_comicfest.pdf",
    "Id_Evento": "746825ea-2677-4654-aaba-3fc98f9c36b3"
  }
}
````
