using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient; // [cite: 266]
using Microsoft.Extensions.Configuration;
using System.IO;

[cite_start]// 1. Model / Entities [cite: 275]
public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
}

[cite_start]// 2. Data Access Class [cite: 276]
public class ProductDataAccess
{
    private readonly string _connectionString;

    public ProductDataAccess(string connectionString)
    {
        _connectionString = connectionString;
    }

    [cite_start]// Insert Product using stored procedure and SqlParameter [cite: 241, 243, 258]
    public void InsertProduct(Product product)
    {
        try
        {
            [cite_start] using (SqlConnection conn = new SqlConnection(_connectionString)) // using statement [cite: 263, 267]
            {
                [cite_start] using (SqlCommand cmd = new SqlCommand("sp_InsertProduct", conn)) // [cite: 268]
                {
                    cmd.CommandType = CommandType.StoredProcedure; // [cite: 270]
                    cmd.Parameters.AddWithValue("@ProductName", product.ProductName); // Parameterized query [cite: 272]
                    cmd.Parameters.AddWithValue("@Category", product.Category);
                    cmd.Parameters.AddWithValue("@Price", product.Price);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Product inserted successfully.");
                }
            }
        }
        [cite_start]catch (Exception ex) // Proper exception handling [cite: 273]
        {
            Console.WriteLine($"Error inserting product: {ex.Message}");
        }
    }

    [cite_start]// View All Products [cite: 244]
    public List<Product> GetAllProducts()
    {
        List<Product> products = new List<Product>();
        try
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetAllProducts", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    [cite_start] using (SqlDataReader reader = cmd.ExecuteReader()) // [cite: 269]
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                ProductId = Convert.ToInt32(reader["ProductId"]),
                                ProductName = reader["ProductName"].ToString(),
                                Category = reader["Category"].ToString(),
                                Price = Convert.ToDecimal(reader["Price"])
                            });
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving products: {ex.Message}");
        }
        return products;
    }

    [cite_start]// Update Product [cite: 246]
    public void UpdateProduct(Product product)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateProduct", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                    cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                    cmd.Parameters.AddWithValue("@Category", product.Category);
                    cmd.Parameters.AddWithValue("@Price", product.Price);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Product updated successfully.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating product: {ex.Message}");
        }
    }

    [cite_start]// Delete Product [cite: 251]
    public void DeleteProduct(int productId)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_DeleteProduct", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductId", productId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Product deleted successfully.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting product: {ex.Message}");
        }
    }
}

[cite_start]// 3. Program (Main Entry) [cite: 277]
class Program
{
    static void Main(string[] args)
    {
        [cite_start]// Store connection string in appsettings.json [cite: 260]
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = config.GetConnectionString("DefaultConnection");
        ProductDataAccess dal = new ProductDataAccess(connectionString);

        [cite_start]// Meaningful console output for operations [cite: 290]
        Console.WriteLine("--- ADO.NET CRUD Operations ---");

        [cite_start]// 1. Insert [cite: 241]
        dal.InsertProduct(new Product { ProductName = "Gaming Laptop", Category = "Electronics", Price = 1200.50m });

        [cite_start]// 2. View All [cite: 244]
        Console.WriteLine("\n--- All Products ---");
        var products = dal.GetAllProducts();
        foreach (var p in products)
        {
            Console.WriteLine($"{p.ProductId}: {p.ProductName} - {p.Category} - ${p.Price}");
        }

        [cite_start]// 3. Update [cite: 246]
        if (products.Count > 0)
        {
            int idToUpdate = products[0].ProductId;
            Console.WriteLine($"\n--- Updating Product ID: {idToUpdate} ---");
            dal.UpdateProduct(new Product { ProductId = idToUpdate, ProductName = "Updated Laptop", Category = "Electronics", Price = 1000.00m });
        }

        [cite_start]// 4. Delete [cite: 251]
        if (products.Count > 0)
        {
            int idToDelete = products[0].ProductId;
            Console.WriteLine($"\n--- Deleting Product ID: {idToDelete} ---");
            dal.DeleteProduct(idToDelete);
        }
    }
}