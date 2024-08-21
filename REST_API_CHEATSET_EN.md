
# REST API Cheat Sheet

## Core Principles
1. **Client-Server**: Separation of concerns for modularity.
2. **Statelessness**: Each request is self-contained, with no server-side session data.
3. **Cacheability**: Responses can be cached to improve performance.
4. **Layered System**: Components are independent and replaceable.
5. **Code on Demand** (optional): Servers can extend client functionality by sending executable code.
6. **Uniform Interface**: Standardized interaction with resources.

## HTTP Methods
1. **GET**: Retrieve resources.
2. **POST**: Create new resources or submit data.
3. **PUT**: Update or replace existing resources.
4. **PATCH**: Partially modify existing resources.
5. **DELETE**: Remove resources.
6. **HEAD**: Retrieve headers only.
7. **OPTIONS**: Get information about available communication options for a resource.

## Status Codes
1. **2xx (Success)**:
    - **200 OK**: The request was successful.
    - **201 Created**: The resource was successfully created.
2. **3xx (Redirection)**:
    - **301 Moved Permanently**: The resource was moved to a new URI.
3. **4xx (Client Error)**:
    - **401 Unauthorized**: Authentication is required or has failed.
    - **403 Forbidden**: The server understands the request but refuses to authorize it.
    - **404 Not Found**: The requested resource could not be found.
4. **5xx (Server Error)**:
    - **500 Internal Server Error**: A generic error occurred on the server side.

## Security Best Practices
1. **Authentication**: Use OAuth 2.0, JWT (JSON Web Tokens).
2. **Authorization**: Implement RBAC (Role-Based Access Control) or ABAC (Attribute-Based Access Control).
3. **HTTPS**: Use TLS/SSL for encrypting communications.
4. **Input Validation**: Always validate and sanitize input data to prevent injection attacks.
5. **Rate Limiting and Throttling**: Implement limits to avoid abuse.
6. **CORS (Cross-Origin Resource Sharing)**: Control access from different origins.
7. **Security Headers**: Use headers like Content-Security-Policy and X-Frame-Options for mitigating web vulnerabilities.

## Resource Naming Conventions
1. **Nouns**: Use nouns for resource names (e.g., `/users`, `/products`).
2. **Pluralization**: Use plural nouns for collections (e.g., `/users`).
3. **Hyphens**: Use hyphens for readability (e.g., `/product-categories`).
4. **Lowercase**: Use lowercase letters consistently.

## Best Practices
1. **Versioning**: Use version numbers in the URI (e.g., `/v1/users`).
2. **Filtering and Sorting**: Apply query parameters for filtering and sorting (e.g., `/users?status=active&sort=name,asc`).
3. **Pagination**: Use limit and offset parameters for large datasets.
4. **Error Handling**: Return clear and consistent error codes and messages.
5. **Documentation**: Use tools like OpenAPI (Swagger) for comprehensive API documentation.
6. **Caching**: Implement server-side and client-side caching for better performance.
