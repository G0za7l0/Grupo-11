# Análisis y Estructura del Proyecto "Grupo-11"

El proyecto **Grupo-11** presenta una arquitectura distribuida sólida, diseñada bajo un enfoque modular que separa claramente las responsabilidades. Esto no solo facilita el mantenimiento, sino que también permite escalar cada componente de forma independiente.

A continuación, se detalla la estructura, las especificaciones técnicas y los puntos clave de cada bloque del ecosistema.

---

## 📁 Estructura General del Proyecto

El repositorio principal está compuesto por tres grandes submódulos:

1. **`landing-telecom/`**: Frontend público (Página de aterrizaje o Landing Page).
2. **`Grupo-11/`**: Backend Principal (API Web Core).
3. **`ETLService/`**: Microservicio Backend enfocado en el procesamiento de datos (ETL) y visualización.

---

## 1. 🌐 `landing-telecom` (Frontend - Landing Page)

Este directorio contiene la interfaz de usuario de presentación para la empresa **"Telecom S.A."**. Su objetivo principal es captar clientes corporativos interesados en servicios de infraestructura digital y redes.

### Especificaciones Técnicas:
* **Lenguajes:** HTML5, CSS3, JavaScript (Vanilla).
* **Estilos:** **Tailwind CSS** (inyectado vía CDN para estilos rápidos y modernos).
* **Recursos:** **Lucide Icons** para iconografía minimalista y **Google Fonts** (Outfit e Inter) para tipografía moderna.

### Puntos Claves:
* **Diseño Premium (UI/UX):** Emplea tendencias actuales de diseño como el *Glassmorphism* (efectos translúcidos en barras de navegación y tarjetas), animaciones suaves (`reveal` al hacer scroll) y gradientes de color llamativos para dar un aspecto profesional y tecnológico.
* **Embudo de Ventas (Estructura Estratégica):** La página está diseñada para convertir visitantes en leads. Se divide en secciones claras: *Hero* (impacto visual), *Empresa* (misión/valores), *Servicios*, *Diferenciadores* y culmina con un *Llamado a la Acción (CTA)* mediante un formulario de contacto.
* **Dinamismo Nativo:** Utiliza scripts nativos en el `index.html` para la interactividad (manejo de opacidad del navbar, animaciones al hacer scroll y simulaciones del formulario), evitando la dependencia de frameworks pesados y garantizando tiempos de carga óptimos.

---

## 2. ⚙️ `Grupo-11` (Backend - API Principal)

Es el núcleo lógico principal (core) de la plataforma. Se encarga de procesar las transacciones principales, la seguridad, la gestión de usuarios y el estado central del negocio.

### Especificaciones Técnicas:
* **Framework:** ASP.NET Core Web API (C#).
* **Documentación:** Swagger (OpenAPI) integrado.
* **Estado:** Sesiones en Memoria Distribuida.

### Puntos Claves:
* **Seguridad y Conexión Centralizada:** Delega la seguridad y la inicialización de la conexión a la base de datos principal (`DefaultConnection`) a través de una capa o componente denominado `AuthNetCore`.
* **Manejo de Sesiones Avanzado:** Configura caché en memoria y sesiones (`AddSession`) con un tiempo de expiración por inactividad de 40 minutos (`IdleTimeout = TimeSpan.FromMinutes(40)`). Esto permite almacenar el estado temporal del usuario de forma segura.
* **Buenas Prácticas de Serialización:** Tiene configurado `JsonStringEnumConverter`, asegurando que los valores de tipo *Enum* (ej. "Pendiente", "Aprobado") se devuelvan y reciban como texto legible en lugar de números, mejorando la integración con los clientes o el frontend.

---

## 3. 📊 `ETLService` (Microservicio de Datos y Visualización)

Es un servicio independiente orientado a tareas ETL (Extract, Transform, Load - Extracción, Transformación y Carga) y a la generación de reportes y dashboards administrativos.

### Especificaciones Técnicas:
* **Framework:** ASP.NET Core (C#).
* **Vistas:** Razor Pages (`index.cshtml`).
* **Frontend Interno:** JavaScript modular (ES6).

### Puntos Claves:
* **Dashboards Dinámicos Integrados:** A diferencia de la API principal, este servicio renderiza vistas web mediante Razor Pages, cargando scripts específicos (`DashboardVentas.js`) para mostrar información visual sin necesidad de un frontend externo separado.
* **Reutilización de Librerías Propias:** Hace uso de una librería propia (`WDevCore` / `WTableDynamicComp`) para leer archivos de datos (ej. `data.json`) y generar dinámicamente reportes interactivos o tablas de ventas en el navegador del cliente.
* **Configuración Abierta para Integración:** El archivo `Program.cs` configura políticas de CORS completamente abiertas (`AllowAnyOrigin`, `AllowAnyMethod`). Esto es muy útil en entornos de desarrollo y facilita que futuros frontends o paneles de control consuman sus datos sin bloqueos.

---

## 💡 Conclusión Estratégica

La arquitectura del proyecto está pensada para la escalabilidad:
1. **El Escaparate (`landing-telecom`):** Un frontend ligero e impactante dedicado exclusivamente al marketing y captura de prospectos.
2. **El Cerebro (`Grupo-11`):** Un servidor centralizado y seguro encargado de la lógica de negocio core.
3. **El Analista (`ETLService`):** Un servicio de apoyo orientado a procesar y visualizar datos masivos, evitando saturar o sobrecargar el servidor principal.
