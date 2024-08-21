
# Hoja de Ayuda sobre Mejores Prácticas para APIs REST

## Principios Básicos
1. **Cliente-Servidor**: Separación de responsabilidades para modularidad.
2. **Sin Estado**: Cada petición contiene toda la información necesaria, sin almacenar datos de sesión en el servidor.
3. **Cacheabilidad**: Las respuestas pueden ser cacheadas para mejorar el rendimiento.
4. **Sistema en Capas**: Las capas funcionan de forma independiente y pueden cambiarse sin afectar a otras.
5. **Código Bajo Demanda** (opcional): Los servidores pueden extender la funcionalidad del cliente enviando código ejecutable.
6. **Interfaz Uniforme**: Interacción consistente entre diferentes componentes.

## Métodos HTTP
1. **GET**: Recupera recursos.
2. **POST**: Crea nuevos recursos o envía datos.
3. **PUT**: Actualiza o reemplaza recursos existentes.
4. **PATCH**: Modifica parcialmente recursos existentes.
5. **DELETE**: Elimina recursos.
6. **HEAD**: Similar a GET, pero solo recupera encabezados.
7. **OPTIONS**: Obtiene las opciones de comunicación disponibles para un recurso.

## Códigos de Estado
1. **2xx (Éxito)**:
    - **200 OK**: La petición fue exitosa.
    - **201 Creado**: El recurso se creó exitosamente.
2. **3xx (Redirección)**:
    - **301 Movido Permanentemente**: El recurso se movió a una nueva URI.
3. **4xx (Error del Cliente)**:
    - **401 No Autorizado**: Se requiere autenticación o la misma ha fallado.
    - **403 Prohibido**: La petición es entendida pero no autorizada.
    - **404 No Encontrado**: El recurso solicitado no se pudo encontrar.
4. **5xx (Error del Servidor)**:
    - **500 Error Interno del Servidor**: Ocurrió un error genérico en el servidor.

## Buenas Prácticas de Seguridad
1. **Autenticación**: Utiliza OAuth 2.0, JWT (Tokens JSON Web).
2. **Autorización**: Implementa RBAC (Control de Acceso Basado en Roles) o ABAC (Control de Acceso Basado en Atributos).
3. **HTTPS**: Usa TLS/SSL para encriptar las comunicaciones.
4. **Validación de Entradas**: Siempre valida y sanitiza los datos de entrada para prevenir ataques de inyección.
5. **Limitación de Tasa y Control de Flujo**: Implementa límites para evitar abusos.
6. **CORS (Intercambio de Recursos entre Orígenes)**: Controla el acceso desde diferentes orígenes.
7. **Encabezados de Seguridad**: Utiliza encabezados como Content-Security-Policy y X-Frame-Options para mitigar vulnerabilidades web.

## Convenciones para Nombres de Recursos
1. **Sustantivos**: Usa sustantivos para los nombres de recursos (ej., `/usuarios`, `/productos`).
2. **Pluralización**: Usa nombres plurales para colecciones (ej., `/usuarios`).
3. **Guiones**: Usa guiones para mejorar la legibilidad (ej., `/categorias-productos`).
4. **Minúsculas**: Usa letras minúsculas consistentemente.

## Mejores Prácticas
1. **Versionado**: Utiliza números de versión en la URI (ej., `/v1/usuarios`).
2. **Filtrado y Ordenación**: Aplica parámetros de consulta para filtrar y ordenar (ej., `/usuarios?estado=activo&orden=nombre,asc`).
3. **Paginación**: Usa parámetros de límite y desplazamiento para grandes conjuntos de datos.
4. **Manejo de Errores**: Devuelve códigos de error y mensajes claros y consistentes.
5. **Documentación**: Utiliza herramientas como OpenAPI (Swagger) para documentación comprensible.
6. **Caché**: Implementa caché en el servidor y en el cliente para un mejor rendimiento.
