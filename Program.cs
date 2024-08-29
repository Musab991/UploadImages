using System;
using System.Data.SqlClient;
using System.IO;

class Program
{
    static void Main()
    {
        // Define the folder path and connection string
        string folderPath = @"C:\Users\XIGMATEK\Downloads\LapShop (2)\LapShop\LapShop\wwwroot\Uploads\Items";

        string connectionString = "Server = DARK-HORSE-ATIE; Database=LapShop ; Integrated Security= SSPI;  TrustServerCertificate = True;";

        // Get image file names
        string[] imageFiles = Directory.GetFiles(folderPath);

        // Insert image file names into TempImageUrls table
        InsertImageUrlsToDatabase(imageFiles, connectionString);

        Console.WriteLine("Image URLs have been uploaded to the database.");
    }

    private static void InsertImageUrlsToDatabase(string[] imageFiles, string connectionString)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            foreach (string filePath in imageFiles)
            {
                // Extract the file name from the full path
                string fileName = Path.GetFileName(filePath);

                // Prepare the SQL command to insert image URL
                string sql = "INSERT INTO TempImageUrls (ImageUrl) VALUES (@ImageUrl)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ImageUrl", fileName);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
