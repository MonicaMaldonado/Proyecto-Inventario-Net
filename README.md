# Proyecto de Gestión de Inventario (Prueba Técnica)

Esta solución implementa un sistema de gestión de inventario usando una arquitectura de microservicios (.NET), comunicados síncronamente vía API REST. La solución está dividida en dos proyectos:

* **`ProductoApi`**: Gestiona el CRUD de productos y es el único responsable de actualizar el stock.
* **`TransaccionApi`**: Registra las compras y ventas, y orquesta la comunicación con `ProductoApi` para validar y ajustar el stock.

## Requisitos

Para ejecutar este proyecto en un entorno local, necesitarás:

* **IDE:** Visual Studio 2022
* **SDK:** .NET 9.0
* **Base de Datos:** SQL Server (2019 o superior)
* **Herramienta de BD:** SQL Server Management Studio (SSMS)

---
## Ejecución del backend

Sigue estos pasos para levantar el proyecto localmente.

### 1. Configuración de la Base de Datos

1.  Abre SQL Server Management Studio (SSMS).
2.  Abre el archivo `script_db.sql` (incluido en la raíz de este repositorio).
3.  Ejecuta el script. Esto creará la base de datos `InventarioDB` y las tablas `Productos` y `Transacciones` (con algunos datos de ejemplo si los incluiste).

### 2. Configuración de la Conexión

1.  Abre el archivo `appsettings.json` dentro del proyecto `ProductoApi`.
2.  Modifica la `DefaultConnection` en `ConnectionStrings` para que apunte a tu servidor SQL Server local. 
3.  **Repite el paso 2** para el archivo `appsettings.json` del proyecto `TransaccionApi`.

### 3. Configuración de Puertos (Importante)

La comunicación entre servicios depende de los puertos. El proyecto está configurado así:
* `ProductoApi` corre en: `https://localhost:7187`
* `TransaccionApi` corre en: `https://localhost:7008`

*(Nota: ... La URL de `ProductoApi` (`7187`) está configurada en `TransaccionService.cs`...)*


### 4. Ejecutar la Solución

1.  Abre el archivo de solución (`GestionInventario.sln`) con Visual Studio 2022.
2.  Haz clic derecho en la **Solución** (en el Explorador de soluciones).
3.  Selecciona **"Configurar proyectos de inicio..."** (Configure Startup Projects...).
4.  Selecciona la opción **"Proyectos de inicio múltiples"**.
5.  Establece la "Acción" como **"Iniciar" (Start)** para ambos proyectos: `ProductoApi` y `TransaccionApi`.
6.  Haz clic en "Aceptar".
7.  Presiona **F5 (Ejecutar)**.

### 5. Probar la API

Se abrirán dos ventanas de Swagger en tu navegador:

* **API de Productos:** `https://localhost:7187/swagger`
    * Usa `POST /api/productos` para crear nuevos productos.
* **API de Transacciones:** `https://localhost:7008/swagger`
    * Usa `POST /api/transacciones` para registrar ventas o compras.
    * Usa `GET /api/transacciones/producto/{id}` para ver el historial filtrado.