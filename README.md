# TechStore API

## User Story

As a client of TechStore, I want to be able to interact with an API to manage my tech products efficiently. Here's a breakdown of the user story:

### 1. Authentication

As a client, I want to be able to securely log in to the TechStore API to access my account and manage my products.

- Endpoint: `/api/authentication`
- Method: `POST`
- Parameters:
  - `email` - The email(user@user.com) of the client
  - `password` - The password(123456) of the client
- Response: 
  - `200 OK` - Successful authentication, returns a token for subsequent requests.
  - `401 Unauthorized` - Invalid credentials.

### 2. Create a Product

As a client, I want to be able to add new tech products to my store via the API.

- Endpoint: `/api/Catalog`
- Method: `POST`
- Parameters:
  - `id` - The id of the product
  - `brand` - The brand of the product
  - `name` - The name of the product
  - `description` - Description of the product
  - `price` - Price of the product
  - `quantityInStock` - Quantity of the product available in the store
- Headers:
  - `Authorization: Bearer <token>` - Access token obtained after authentication
- Response:
  - `200 Created` - Product successfully added, returns the details of the created product.
  - `401 Unauthorized` - Invalid or missing token.
  - `400 Bad Request` - Invalid parameters.

### 3. Update a Product

As a client, I want to be able to update the details of existing products in my store.

- Endpoint: `api/Catalog/{productId}`
- Method: `PUT`
- Parameters (at least one required):
  - `id` - The id of the product
  - `brand` - The brand of the product
  - `name` - The name of the product
  - `description` - Description of the product
  - `price` - Price of the product
  - `quantityInStock` - Quantity of the product available in the store
- Headers:
  - `Authorization: Bearer <token>` - Access token obtained after authentication
- Response:
  - `200 OK` - Product details successfully updated.
  - `401 Unauthorized` - Invalid or missing token.
  - `404 Not Found` - Product with given ID not found.

### 4. Delete a Product

As a client, I want to be able to remove products from my store.

- Endpoint: `api/Catalog/{productId}`
- Method: `DELETE`
- Headers:
  - `Authorization: Bearer <token>` - Access token obtained after authentication
- Response:
  - `204 No Content` - Product successfully deleted.
  - `401 Unauthorized` - Invalid or missing token.
  - `404 Not Found` - Product with given ID not found.

### 5. Retrieve All Products

As a client, I want to be able to retrieve a list of all products in my store.

- Endpoint: `api/Catalog`
- Method: `GET`
- Headers:
  - `Authorization: Bearer <token>` - Access token obtained after authentication
- Response:
  - `200 OK` - List of all products successfully retrieved.
  - `401 Unauthorized` - Invalid or missing token.
