# API CRUD de Usuarios con Inicio de Sesión

Esta API proporciona operaciones CRUD (Crear, Leer, Actualizar y Eliminar)
 para gestionar usuarios, junto con funcionalidad de inicio de sesión y token de verificacion.

## Requisitos

- [SQL Server] instalado y en funcionamiento.
- [SQL Server Management Studio] u otra herramienta similar.
- [.NET 6 SDK]

## Pasos de Configuración

1. Descarga el archivo `db_ilana.bacpac` y restaura la base de datos siguiendo estos pasos:

   - Abre SQL Server Management Studio.
   - Haz clic derecho en "Databases" en el Explorador de Objetos y selecciona "Import Data-tier Application".
   - Sigue las instrucciones para seleccionar el archivo `db_ilana.bacpac` y completar el proceso de restauración.

2. Abre el archivo de configuración `appsettings.json` en el proyecto y encuentra 
la cadena de conexión a la base de datos. Actualiza la cadena de conexión con los detalles correctos:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "aquí-debes-poner-tu-cadena-de-conexion"
     }
   }
