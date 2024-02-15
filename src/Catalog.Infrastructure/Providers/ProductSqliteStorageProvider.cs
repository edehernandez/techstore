using Catalog.Business.Model;
using Catalog.Business.Providers;
using Microsoft.Data.Sqlite;

namespace Catalog.Data.Providers
{
    public class ProductSqliteStorageProvider : IProductStorageProvider
    {
        const string tableName = "Products";

        private readonly SqliteConnection connection;

        public ProductSqliteStorageProvider(string connectionString)
        {
            connection = new SqliteConnection(connectionString);
            connection.Open();

            CreateTableIfNotExists();
        }
        void CreateTableIfNotExists()
        {
            string createTableSql = $@"
                CREATE TABLE IF NOT EXISTS {tableName} (
                    Id TEXT PRIMARY KEY,
                    Brand TEXT NOT NULL,
                    Name TEXT NOT NULL,
                    Description TEXT,
                    Price REAL NOT NULL,
                    QuantityInStock INTEGER NOT NULL
                )";

            using (var command = connection.CreateCommand())
            {
                command.CommandText = createTableSql;
                command.ExecuteNonQuery();
            }
        }

        void IProductStorageProvider.AddProduct(Product product)
        {
            string sql = $"INSERT INTO {tableName} (Id, Brand, Name, Description, Price, QuantityInStock) VALUES (@id, @brand, @name, @description, @price, @quantityInStock)";

            var command = connection.CreateCommand();

            command.CommandText = sql;

            command.Parameters.AddWithValue("@id", product.Id);
            command.Parameters.AddWithValue("@brand", product.Brand);
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@description", product.Description);
            command.Parameters.AddWithValue("@price", product.Price);
            command.Parameters.AddWithValue("@quantityInStock", product.QuantityInStock);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.WriteLine("Product added successfully!");
            }
            else
            {
                Console.WriteLine("Error adding product.");
            }
        }

        void IProductStorageProvider.DeleteProduct(Guid id)
        {
            string sql = $"DELETE FROM {tableName} WHERE Id = @productId";
            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.Parameters.AddWithValue("@productId", id);

                //connection.Open();
                command.ExecuteNonQuery();
            }
        }

        Product IProductStorageProvider.GetProductById(Guid id)
        {
            string sql = $"SELECT * FROM {tableName} WHERE Id = @id";

            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.Parameters.AddWithValue("@id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Product
                        {
                            Id = new Guid(reader["Id"].ToString()),
                            Brand = reader["Brand"].ToString(),
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            QuantityInStock = Convert.ToInt32(reader["QuantityInStock"])
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        List<Product> IProductStorageProvider.GetAllProducts()
        {
            List<Product> products = new List<Product>();

            string sql = $"SELECT * FROM {tableName}";
            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;

                //connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            Id = new Guid(reader["Id"].ToString()),
                            Brand = reader["Brand"].ToString(),
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            QuantityInStock = Convert.ToInt32(reader["QuantityInStock"])
                        };
                        products.Add(product);
                    }
                }
            }

            return products;
        }

        void IProductStorageProvider.UpdateProduct(Product product)
        {
            string sql = $@"UPDATE {tableName} SET
                            Brand = @brand,
                            Name = @name,
                            Description = @description,
                            Price = @price,
                            QuantityInStock = @quantityInStock
                            WHERE Id = @productId";

            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.Parameters.AddWithValue("@productId", product.Id);
                command.Parameters.AddWithValue("@brand", product.Brand);
                command.Parameters.AddWithValue("@name", product.Name);
                command.Parameters.AddWithValue("@description", product.Description);
                command.Parameters.AddWithValue("@price", product.Price);
                command.Parameters.AddWithValue("@quantityInStock", product.QuantityInStock);

                command.ExecuteNonQuery();
            }

        }

        public void Dispose()
        {
            connection.Dispose();
        }

    }
}
