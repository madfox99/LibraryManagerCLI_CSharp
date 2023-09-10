// DON-CODE
using System;
using System.Data.SQLite;
using System.IO;

public class LibraryDatabase {
    private string connectionString;

    public LibraryDatabase(string databasePath) {
        if (string.IsNullOrWhiteSpace(databasePath)) {
            throw new ArgumentNullException(nameof(databasePath), "Database path cannot be null or empty.");
        }
        connectionString = $"Data Source={databasePath};Version=3;";
        InitializeDatabase();
    }

    private void InitializeDatabase() {
        using (SQLiteConnection connection = new SQLiteConnection(connectionString)) {
            connection.Open(); // Open the DB connection

            // Create tables
            string createBooksTableSql = @"
                CREATE TABLE IF NOT EXISTS Books (
                BookID INTEGER PRIMARY KEY AUTOINCREMENT,
                Title TEXT NOT NULL,
                Author TEXT,
                ISBN TEXT UNIQUE,
                Quantity INTEGER,
                AvailableQuantity INTEGER,
                Genre TEXT,
                PublicationYear TEXT,
                Publisher TEXT,
                Language TEXT,
                Description TEXT,
                Location TEXT
            );
            ";
            // ISBN - International Standard Book Number

            string createMembersTableSql = @"
                CREATE TABLE IF NOT EXISTS Members (
                    MemberID INTEGER PRIMARY KEY AUTOINCREMENT,
                    FirstName TEXT NOT NULL,
                    LastName TEXT NOT NULL,
                    Email TEXT UNIQUE,
                    PhoneNumber TEXT
                );
            ";

            string createLendingTableSql = @"
                CREATE TABLE IF NOT EXISTS Lending (
                    TransactionID INTEGER PRIMARY KEY AUTOINCREMENT,
                    BookID INTEGER NOT NULL,
                    MemberID INTEGER NOT NULL,
                    LendDate TEXT NOT NULL,
                    DueDate TEXT NOT NULL,
                    ReturnDate TEXT DEFAULT 'N/A',
                    FOREIGN KEY (BookID) REFERENCES Books (BookID),
                    FOREIGN KEY (MemberID) REFERENCES Members (MemberID)
                );
            ";


            string createFinesTableSql = @"
                CREATE TABLE IF NOT EXISTS Fines (
                    FineID INTEGER PRIMARY KEY AUTOINCREMENT,
                    TransactionID INTEGER NOT NULL,
                    Amount DECIMAL(5, 2) NOT NULL,
                    FOREIGN KEY (TransactionID) REFERENCES Lending (TransactionID)
                );
            ";

            using (SQLiteCommand command = new SQLiteCommand(createBooksTableSql, connection)) {
                command.ExecuteNonQuery();
            }

            using (SQLiteCommand command = new SQLiteCommand(createMembersTableSql, connection)) {
                command.ExecuteNonQuery();
            }

            using (SQLiteCommand command = new SQLiteCommand(createLendingTableSql, connection)) {
                command.ExecuteNonQuery();
            }

            using (SQLiteCommand command = new SQLiteCommand(createFinesTableSql, connection)) {
                command.ExecuteNonQuery();
            }

            connection.Close(); //Close the DB connection
        }
    }
}
