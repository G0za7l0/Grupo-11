# Arquitectura y Estructura del Proyecto

El repositorio principal está compuesto por tres grandes bloques o submódulos, cada uno con una responsabilidad clara dentro del ecosistema de la aplicación.

A continuación, se presenta un análisis detallado de la estructura y los puntos más importantes de cada uno:

## 📁 Estructura General del Proyecto
El proyecto se divide en las siguientes carpetas principales:
1. **`landing-telecom/`**: Frontend (Página de aterrizaje o Landing Page).
2. **`ETLService/`**: Backend / Microservicio (Procesamiento de datos y Dashboard).
3. **`Grupo-11/`**: Backend Principal (API Web Core).

---

### 1. `landing-telecom` (Frontend - Landing Page)
Este directorio contiene la interfaz de usuario pública de la empresa **"Telecom S.A."**, orientada a captar clientes corporativos para servicios de infraestructura digital y redes.

*   **Tecnologías clave:** HTML5 puro, **Tailwind CSS** (inyectado vía CDN para estilos rápidos y modernos), **Lucide Icons** para la iconografía y **Google Fonts** (Outfit e Inter) para la tipografía moderna.
*   **Puntos importantes:**
    *   **Diseño Moderno (UI/UX):** Utiliza tendencias actuales como *Glassmorphism* (efectos translúcidos y borrosos en la barra de navegación y tarjetas), animaciones suaves (`reveal` al hacer scroll, elementos flotantes) y gradientes de color llamativos para dar un aspecto muy profesional y "premium".
    *   **Estructura de Ventas:** Está dividida en secciones estratégicas: *Hero* (impacto visual inicial), *Empresa* (Misión/Visión/Valores), *Servicios* (Redes, Mantenimiento, Soporte), *Diferenciadores* y finalmente un *Llamado a la Acción (CTA)* con un formulario de contacto.
    *   **Interactividad:** Cuenta con scripts nativos al final del `index.html` para manejar la opacidad de la barra de navegación al bajar, la aparición de elementos en pantalla (scroll reveal) y una simulación de envío exitoso en el formulario de contacto.

---

### 2. `ETLService` (Microservicio de Datos y Visualización)
Es un proyecto de **ASP.NET Core (C#)** que, por su nombre (Extract, Transform, Load), está enfocado en la manipulación, extracción y carga de datos.

*   **Tecnologías clave:** ASP.NET Core, Razor Pages, Swagger (OpenAPI) y JavaScript modular (ES6).
*   **Puntos importantes:**
    *   **Dashboard Dinámico:** A diferencia de una API pura, este servicio también sirve vistas web. Según su excelente documentación interna (`README.MD`), utiliza Razor Pages (`index.cshtml`) para cargar un script llamado `DashboardVentas.js`.
    *   **Librería Propia (`WDevCore`):** Emplea componentes creados por ustedes (como `WTableDynamicComp`) para leer un archivo estático (`data.json`) y generar dinámicamente un reporte o tabla de ventas interactiva en el navegador del cliente.
    *   **Configuración en `Program.cs`:** Tiene los CORS (Cross-Origin Resource Sharing) abiertos completamente (`AllowAnyOrigin`, `AllowAnyMethod`) lo cual es útil en desarrollo para que otros frontends puedan consumir sus datos sin bloqueos del navegador.

---

### 3. `Grupo-11` (Backend - API Principal)
Es el núcleo lógico principal de la aplicación, también construido sobre **ASP.NET Core (C#)**.

*   **Tecnologías clave:** ASP.NET Core Web API, Swagger, Sesiones en Memoria.
*   **Puntos importantes:**
    *   **Gestión de Sesiones:** En su `Program.cs` se observa que implementa un caché en memoria distribuida y configura sesiones (`AddSession`) con un tiempo de expiración por inactividad de 40 minutos (`IdleTimeout = TimeSpan.FromMinutes(40)`). Esto indica que la API guarda estado del usuario (posiblemente tokens de autenticación antiguos o carritos temporales).
    *   **Manejo de Base de Datos y Seguridad:** Hace uso de un componente llamado `AuthNetCore` para inicializar la conexión a la base de datos principal (`DefaultConnection`), delegando la seguridad y conexión a esa capa.
    *   **Serialización de Enums:** Tiene configurado `JsonStringEnumConverter`, lo que significa que cuando la API devuelve datos o los recibe, los valores Enum (como estados de "Pendiente", "Aprobado") se ven como texto legible y no como números, lo cual es una excelente práctica.

---

### 💡 Resumen de Arquitectura
El proyecto sigue una arquitectura distribuida donde:
1. Tienen un **escaparate público** muy bien diseñado visualmente (`landing-telecom`).
2. Un **servidor central** de lógica de negocio y seguridad (`Grupo-11`).
3. Un **servicio de apoyo** orientado a reportes, dashboards y manejo masivo de datos (`ETLService`).
