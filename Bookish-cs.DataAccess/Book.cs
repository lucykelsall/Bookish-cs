using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;  
using System.Data.SqlClient;
using Dapper;
using Npgsql;

namespace Bookish_cs.DataAccess
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Author { get; set; }
        [Required]
        public string? Isbn { get; set; }

        public static async Task MapBooks()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["bookish"].ConnectionString;
            await using var dataSource = NpgsqlDataSource.Create(connectionString);


            await using (var connection = new NpgsqlConnection(connectionString))
            {
                var sql = "SELECT * FROM books";
                var books = await connection.QueryAsync<Book>(sql);
                books.ToList().ForEach(book => Console.WriteLine($"Book ID: {book.BookId}, Title: {book.Title}, Author: {book.Author}, ISBN: {book.Isbn}"));
            }

            //await using (var cmd = dataSource.CreateCommand("SELECT title FROM books"));
            /*await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    Console.WriteLine(reader.GetString(0));
                }
            }*/

            /*
            await using (var cmd = dataSource.CreateCommand("INSERT INTO books (title, author, isbn) VALUES ($1, $2, $3)"))
            {
                cmd.Parameters.AddWithValue("The Warden");
                cmd.Parameters.AddWithValue("Anthony Trollope");
                cmd.Parameters.AddWithValue("999999999");
                await cmd.ExecuteNonQueryAsync();
                Console.WriteLine("Hello");
            }
            */
        }
    }
}