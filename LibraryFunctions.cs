// DON-CODE
using System;
using System.Collections.Generic;
using System.Data.SQLite;

public class LibraryFunctions {
    private string connectionString;

    public LibraryFunctions(string databasePath) {
        if (string.IsNullOrWhiteSpace(databasePath)) {
            throw new ArgumentNullException(nameof(databasePath), "Database path cannot be null or empty.");
        }
        connectionString = $"Data Source={databasePath};Version=3;";
    }

	// Function 1: Add Books with feedback message
	public (bool success, string message) AddBook(string title, string author, string isbn, int quantity, string genre, string publicationYear, string publisher, string language, string description, string location) {
		using (SQLiteConnection connection = new SQLiteConnection(connectionString)) {
			connection.Open();

			string insertBookSql = @"
				INSERT INTO Books (Title, Author, ISBN, Quantity, AvailableQuantity, Genre, PublicationYear, Publisher, Language, Description, Location)
				VALUES (@Title, @Author, @ISBN, @Quantity, @Quantity, @Genre, @PublicationYear, @Publisher, @Language, @Description, @Location);
			";
			try {
				using (SQLiteCommand command = new SQLiteCommand(insertBookSql, connection)) {
					command.Parameters.AddWithValue("@Title", title);
					command.Parameters.AddWithValue("@Author", author);
					command.Parameters.AddWithValue("@ISBN", isbn);
					command.Parameters.AddWithValue("@Quantity", quantity);
					command.Parameters.AddWithValue("@Genre", genre);
					command.Parameters.AddWithValue("@PublicationYear", publicationYear);
					command.Parameters.AddWithValue("@Publisher", publisher);
					command.Parameters.AddWithValue("@Language", language);
					command.Parameters.AddWithValue("@Description", description);
					command.Parameters.AddWithValue("@Location", location);
					command.ExecuteNonQuery();
				}
				connection.Close();
				return (true, "Book added successfully.");
			} catch (Exception ex) {
				// Handle any exceptions that may occur (e.g., unique constraint violation)
				connection.Close();
				return (false, "Failed to add the book. " + ex.Message);
			}
		}
	}


	// Function 2: Register Members with feedback message
	public (bool success, string message) RegisterMember(string firstName, string lastName, string email, string phoneNumber) {
		using (SQLiteConnection connection = new SQLiteConnection(connectionString)) {
			connection.Open();
			string insertMemberSql = @"
				INSERT INTO Members (FirstName, LastName, Email, PhoneNumber)
				VALUES (@FirstName, @LastName, @Email, @PhoneNumber);
			";
			try {
				using (SQLiteCommand command = new SQLiteCommand(insertMemberSql, connection)) {
					command.Parameters.AddWithValue("@FirstName", firstName);
					command.Parameters.AddWithValue("@LastName", lastName);
					command.Parameters.AddWithValue("@Email", email);
					command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
					command.ExecuteNonQuery();
				}
				connection.Close();
				return (true, "Member registered successfully.");
			} catch (Exception ex) {
				// Handle any exceptions that may occur (e.g., unique constraint violation)
				connection.Close();
				return (false, "Failed to register the member. " + ex.Message);
			}
		}
	}

    // Function 3: Remove Books
	public (bool Success, string Message) RemoveBook(int bookID, string isbn) {
		try {
			using (SQLiteConnection connection = new SQLiteConnection(connectionString)) {
				connection.Open();
				string deleteBookSql = @"
					DELETE FROM Books
					WHERE BookID = @BookID
					AND ISBN = @ISBN;
				";
				using (SQLiteCommand command = new SQLiteCommand(deleteBookSql, connection)) {
					command.Parameters.AddWithValue("@BookID", bookID);
					command.Parameters.AddWithValue("@ISBN", isbn);
					int rowsAffected = command.ExecuteNonQuery();

					if (rowsAffected > 0) {
						connection.Close();
						return (true, "Book removed successfully.");
					} else {
						connection.Close();
						return (false, "Book not found or could not be removed.");
					}
				}
			}
		} catch (Exception ex) {
			return (false, $"An error occurred: {ex.Message}");
		}
	}


    // Function 4: Remove Members
	public (bool Success, string Message) RemoveMember(int memberID, string email) {
		try {
			using (SQLiteConnection connection = new SQLiteConnection(connectionString)) {
				connection.Open();
				// Check if the member ID and email match for confirmation
				string checkMemberSql = @"
				SELECT 1
				FROM Members
				WHERE MemberID = @MemberID
				AND Email = @Email;
				";
				using (SQLiteCommand checkCommand = new SQLiteCommand(checkMemberSql, connection)) {
					checkCommand.Parameters.AddWithValue("@MemberID", memberID);
					checkCommand.Parameters.AddWithValue("@Email", email);
					bool memberExists = checkCommand.ExecuteScalar() != null;

					if (!memberExists) {
						connection.Close();
						return (false, "Member ID or email does not exist or does not match.");
					}
				}
				// If member exists, proceed with deletion
				string deleteMemberSql = @"
				DELETE FROM Members WHERE MemberID = @MemberID;
				";
				using (SQLiteCommand deleteCommand = new SQLiteCommand(deleteMemberSql, connection)) {
					deleteCommand.Parameters.AddWithValue("@MemberID", memberID);
					int rowsAffected = deleteCommand.ExecuteNonQuery();

					if (rowsAffected > 0) {
						connection.Close();
						return (true, "Member removed successfully.");
					} else {
						connection.Close();
						return (false, "Member not found or could not be removed.");
					}
				}
			}
		} catch (Exception ex) {
		return (false, $"An error occurred: {ex.Message}");
		}
	}


	// Function 5: Search Book Information
	public (List<(string Column, object Value)> bookInfo, bool valueFound) SearchBookInformation(string[] searchCriteria) {
		List<(string Column, object Value)> bookInfo = new List<(string Column, object Value)>();
		bool valueFound = false;
		if (searchCriteria.Length != 2) {
			bookInfo.Add(("Error", "Invalid input format. Please provide two elements in the array: column name and value."));
			return (bookInfo, valueFound);
		}
		using (SQLiteConnection connection = new SQLiteConnection(connectionString)) {
			try {
				connection.Open();
				var columnName = searchCriteria[0];
				var columnValue = searchCriteria[1];
				string searchBookSql = @"
					SELECT BookID, Title, Author, ISBN, Quantity, AvailableQuantity, Genre, PublicationYear, Publisher, Language, Description, Location
					FROM Books
					WHERE
				";
				switch (columnName.ToLower()) {
					case "bookid":
						searchBookSql += "BookID = @SearchValue";
						break;
					case "title":
						searchBookSql += "Title = @SearchValue";
						break;
					case "author":
						searchBookSql += "Author = @SearchValue";
						break;
					case "isbn":
						searchBookSql += "ISBN = @SearchValue";
						break;
					case "quantity":
						searchBookSql += "Quantity = @SearchValue";
						break;
					case "availableQuantity":
						searchBookSql += "AvailableQuantity = @SearchValue";
						break;
					case "genre":
						searchBookSql += "Genre = @SearchValue";
						break;
					case "publicationYear":
						searchBookSql += "PublicationYear = @SearchValue";
						break;
					case "publisher":
						searchBookSql += "Publisher = @SearchValue";
						break;
					case "language":
						searchBookSql += "Language = @SearchValue";
						break;
					case "description":
						searchBookSql += "Description = @SearchValue";
						break;
					case "location":
						searchBookSql += "Location = @SearchValue";
						break;
					default:
						// Invalid column name
						bookInfo.Add(("Error", "Invalid column name."));
						return (bookInfo, valueFound);
				}
				using (SQLiteCommand command = new SQLiteCommand(searchBookSql, connection)) {
					command.Parameters.AddWithValue("@SearchValue", columnValue);

					using (SQLiteDataReader reader = command.ExecuteReader()) {
						while (reader.Read()) {
							valueFound = true;
							for (int i = 0; i < reader.FieldCount; i++) {
								string colName = reader.GetName(i);
								object colValue = reader[i];
								bookInfo.Add((colName, colValue));
							}
						}
					}
				}
			}catch (SQLiteException ex) {
				bookInfo.Add(("Error", $"Database error: {ex.Message}"));
			}finally {
				connection.Close();
			}
		}
		return (bookInfo, valueFound);
	}

	
	// Function 6: Search Member Information
	public (List<(string Column, object Value)> memberInfo, bool valueFound) SearchMemberInformation(string[] searchCriteria) {
		List<(string Column, object Value)> memberInfo = new List<(string Column, object Value)>();
		bool valueFound = false;
		if (searchCriteria.Length != 2) {
			memberInfo.Add(("Error", "Invalid input format. Please provide two elements in the array: column name and value."));
			return (memberInfo, valueFound);
		}
		using (SQLiteConnection connection = new SQLiteConnection(connectionString)) {
			try {
				connection.Open();
				var columnName = searchCriteria[0];
				var columnValue = searchCriteria[1];
				string searchMemberSql = $@"SELECT MemberID, FirstName, LastName, Email, PhoneNumber FROM Members WHERE ";
				switch (columnName.ToLower()) {
					case "memberid":
						searchMemberSql += "MemberID = @SearchValue";
						break;
					case "firstname":
						searchMemberSql += "FirstName = @SearchValue";
						break;
					case "lastname":
						searchMemberSql += "LastName = @SearchValue";
						break;
					case "email":
						searchMemberSql += "Email = @SearchValue";
						break;
					case "phonenumber":
						searchMemberSql += "PhoneNumber = @SearchValue";
						break;
					default:
						// Invalid column name
						memberInfo.Add(("Error", "Invalid column name."));
						return (memberInfo, valueFound);
				}
				using (SQLiteCommand command = new SQLiteCommand(searchMemberSql, connection)) {
					command.Parameters.AddWithValue("@SearchValue", columnValue);

					using (SQLiteDataReader reader = command.ExecuteReader()) {
						while (reader.Read()) {
							valueFound = true;
							for (int i = 0; i < reader.FieldCount; i++) {
								string colName = reader.GetName(i);
								object colValue = reader[i];
								memberInfo.Add((colName, colValue));
							}
						}
					}
				}
			}catch (SQLiteException ex){
				memberInfo.Add(("Error", $"Database error: {ex.Message}"));
			}finally{
				connection.Close();
			}
		}
		return (memberInfo, valueFound);
	}

	// Function 7: Display Book Names
	public List<string[]> GetAllBooksAsArrays() {
		List<string[]> bookArrays = new List<string[]>();
		using (SQLiteConnection connection = new SQLiteConnection(connectionString)) {
			connection.Open();
			string displayBooksSql = @"
				SELECT *
				FROM Books;
			";
			using (SQLiteCommand command = new SQLiteCommand(displayBooksSql, connection)) {
				using (SQLiteDataReader reader = command.ExecuteReader()) {
					while (reader.Read()) {
						string[] bookData = new string[12];
						for (int col = 0; col < 12; col++) {
							object columnValue = reader.GetValue(col);
							bookData[col] = columnValue.ToString();
						}
						bookArrays.Add(bookData);
					}
				}
			}
			connection.Close();
		}
		return bookArrays;
	}
	
	// Function 8: Display Member Names
	public List<string[]> GetAllMembersAsArrays() {
		List<string[]> memberArrays = new List<string[]>();
		using (SQLiteConnection connection = new SQLiteConnection(connectionString)) {
			connection.Open();
			string displayMembersSql = @"
				SELECT *
				FROM Members;
			";
			using (SQLiteCommand command = new SQLiteCommand(displayMembersSql, connection)) {
				using (SQLiteDataReader reader = command.ExecuteReader()) {
					while (reader.Read()) {
						string[] memberData = new string[5];
						for (int col = 0; col < 5; col++) {
							object columnValue = reader.GetValue(col);
							memberData[col] = columnValue.ToString();
						}
						memberArrays.Add(memberData);
					}
				}
			}
			connection.Close();
		}
		return memberArrays;
	}


	// Function 9: Lend Books
	public (bool Success, string Message) LendBook(int bookID, int memberID, DateTime lendDate, DateTime dueDate) {
		try {
			using (SQLiteConnection connection = new SQLiteConnection(connectionString)) {
			connection.Open();
			// Check if bookID and memberID are valid
			if (!IsBookValid(connection, bookID) || !IsMemberValid(connection, memberID)) {
				return (false, "Book ID or Member ID is not valid.");
			}
			string insertLendingSql = @"
			INSERT INTO Lending (BookID, MemberID, LendDate, DueDate)
			VALUES (@BookID, @MemberID, @LendDate, @DueDate);
			";
			using (SQLiteCommand command = new SQLiteCommand(insertLendingSql, connection)) {
				command.Parameters.AddWithValue("@BookID", bookID);
				command.Parameters.AddWithValue("@MemberID", memberID);
				command.Parameters.AddWithValue("@LendDate", lendDate);
				command.Parameters.AddWithValue("@DueDate", dueDate);
				command.ExecuteNonQuery();
			}
			// Update the AvailableQuantity in the Books table
			string updateBookQuantitySql = @"
			UPDATE Books
			SET AvailableQuantity = AvailableQuantity - 1
			WHERE BookID = @BookID;
			";
			using (SQLiteCommand command = new SQLiteCommand(updateBookQuantitySql, connection)) {
				command.Parameters.AddWithValue("@BookID", bookID);
				command.ExecuteNonQuery();
			}
			connection.Close();
			return (true, "Book successfully lent.");
			}
		} catch (Exception ex) {
			return (false, $"Error lending book: {ex.Message}");
		}
	}

	private bool IsBookValid(SQLiteConnection connection, int bookID) {
		try {
			string query = "SELECT COUNT(*) FROM Books WHERE BookID = @BookID;";
			using (SQLiteCommand command = new SQLiteCommand(query, connection)) {
				command.Parameters.AddWithValue("@BookID", bookID);
				int count = Convert.ToInt32(command.ExecuteScalar());
				return count > 0;
			}
		} catch (Exception ex) {
			// Handle any exceptions, e.g., log the error
			return false; // Return false on error
		}
	}

	private bool IsMemberValid(SQLiteConnection connection, int memberID) {
		try {
			string query = "SELECT COUNT(*) FROM Members WHERE MemberID = @MemberID;";
			using (SQLiteCommand command = new SQLiteCommand(query, connection)) {
				command.Parameters.AddWithValue("@MemberID", memberID);
				int count = Convert.ToInt32(command.ExecuteScalar());
				return count > 0;
			}
		} catch (Exception ex) {
			// Handle any exceptions, e.g., log the error
			return false; // Return false on error
		}
	}



	// Function 10: Return Books
public (bool Success, string Message) ReturnBook(int transactionID, DateTime returnDate) {
    try {
        using (SQLiteConnection connection = new SQLiteConnection(connectionString)) {
            connection.Open();

            // Check if the transactionID exists in the Lending table
            string checkTransactionSql = "SELECT 1 FROM Lending WHERE TransactionID = @TransactionID;";
            using (SQLiteCommand checkCommand = new SQLiteCommand(checkTransactionSql, connection)) {
                checkCommand.Parameters.AddWithValue("@TransactionID", transactionID);
                bool transactionExists = checkCommand.ExecuteScalar() != null;
                if (!transactionExists) {
                    return (false, "Transaction ID does not exist.");
                }
            }

            // Update the ReturnDate in the Lending table
            string updateReturnDateSql = @"
                UPDATE Lending
                SET ReturnDate = @ReturnDate
                WHERE TransactionID = @TransactionID;
            ";

            using (SQLiteCommand updateReturnDateCommand = new SQLiteCommand(updateReturnDateSql, connection)) {
                updateReturnDateCommand.Parameters.AddWithValue("@TransactionID", transactionID);
                updateReturnDateCommand.Parameters.AddWithValue("@ReturnDate", returnDate);

                updateReturnDateCommand.ExecuteNonQuery();
            }

            // Update the AvailableQuantity in the Books table
            string updateBookQuantitySql = @"
                UPDATE Books
                SET AvailableQuantity = AvailableQuantity + 1
                WHERE BookID = (
                    SELECT BookID
                    FROM Lending
                    WHERE TransactionID = @TransactionID
                );
            ";

            using (SQLiteCommand updateBookQuantityCommand = new SQLiteCommand(updateBookQuantitySql, connection)) {
                updateBookQuantityCommand.Parameters.AddWithValue("@TransactionID", transactionID);

                updateBookQuantityCommand.ExecuteNonQuery();
            }

            connection.Close();
        }

        return (true, "");
    } catch (Exception ex) {
        return (false, $"Error returning book: {ex.Message}");
    }
}


	// Function 11: View Lending Information
	public List<string[]> GetAllLendingInfoAsArrays() {
		List<string[]> lendingInfoArrays = new List<string[]>();
		using (SQLiteConnection connection = new SQLiteConnection(connectionString)) {
			connection.Open();
			string viewLendingSql = @"
				SELECT L.TransactionID, B.Title AS BookTitle, M.FirstName || ' ' || M.LastName AS MemberFullName, L.LendDate, L.DueDate, L.ReturnDate
				FROM Lending AS L
				JOIN Books AS B ON L.BookID = B.BookID
				JOIN Members AS M ON L.MemberID = M.MemberID;
			";
			using (SQLiteCommand command = new SQLiteCommand(viewLendingSql, connection)) {
				using (SQLiteDataReader reader = command.ExecuteReader()) {
					while (reader.Read()) {
						string[] lendingData = new string[6]; // Reduced to 6 columns due to concatenation
						for (int col = 0; col < 6; col++) {
							object columnValue = reader.GetValue(col);
							lendingData[col] = columnValue.ToString();
						}
						lendingInfoArrays.Add(lendingData);
					}
				}
			}
			connection.Close();
		}
		return lendingInfoArrays;
	}


	// Function 12: Display Overdue Books
	public List<string[]> DisplayOverdueBooks() {
		List<string[]> overdueBooksArrays = new List<string[]>();
		using (SQLiteConnection connection = new SQLiteConnection(connectionString)) {
			connection.Open();
			string overdueBooksSql = @"
				SELECT L.TransactionID, L.MemberID, B.Title AS BookTitle, M.FirstName || ' ' || M.LastName AS MemberFullName, L.DueDate
				FROM Lending AS L
				JOIN Books AS B ON L.BookID = B.BookID
				JOIN Members AS M ON L.MemberID = M.MemberID
				WHERE (L.ReturnDate IS NULL AND L.DueDate < DATE('now')) OR L.ReturnDate = 'N/A';
			";
			using (SQLiteCommand command = new SQLiteCommand(overdueBooksSql, connection)) {
				using (SQLiteDataReader reader = command.ExecuteReader()) {
					while (reader.Read()) {
						string[] overdueData = new string[5]; // Include TransactionID and MemberID
						overdueData[0] = reader["TransactionID"].ToString();
						overdueData[1] = reader["MemberID"].ToString();
						overdueData[2] = reader["BookTitle"].ToString();
						overdueData[3] = reader["MemberFullName"].ToString();
						overdueData[4] = reader["DueDate"].ToString();
						overdueBooksArrays.Add(overdueData);
					}
				}
			}
			connection.Close();
		}
		return overdueBooksArrays;
	}

	// Function 13: Fine Calculation
	public (bool Success, string Message, decimal Fine) CalculateFine(int transactionID, DateTime returnDate) {
		decimal fine = 0;
		try {
			using (SQLiteConnection connection = new SQLiteConnection(connectionString)) {
				connection.Open();
				// Check if the transactionID exists in the Lending table
				string checkTransactionSql = "SELECT 1 FROM Lending WHERE TransactionID = @TransactionID;";
				using (SQLiteCommand checkCommand = new SQLiteCommand(checkTransactionSql, connection)) {
					checkCommand.Parameters.AddWithValue("@TransactionID", transactionID);
					bool transactionExists = checkCommand.ExecuteScalar() != null;
					if (!transactionExists) {
						return (false, "Transaction ID does not exist.", fine);
					}
				}
				// If the transactionID exists, proceed with fine calculation
				string calculateFineSql = @"
				SELECT L.LendDate, L.DueDate
				FROM Lending AS L
				WHERE L.TransactionID = @TransactionID;
				";
				using (SQLiteCommand command = new SQLiteCommand(calculateFineSql, connection)) {
					command.Parameters.AddWithValue("@TransactionID", transactionID);
					using (SQLiteDataReader reader = command.ExecuteReader()) {
						if (reader.Read()) {
							DateTime lendDate = Convert.ToDateTime(reader["LendDate"]);
							DateTime dueDate = Convert.ToDateTime(reader["DueDate"]);
							// Check if the return date is before or equal to the lend date
							if (returnDate <= lendDate) {
								return (false, "Return Date must be after the Lend Date.", fine);
							}
							// Calculate the number of days overdue
							int daysOverdue = (int)(returnDate - dueDate).TotalDays;
							// Calculate the fine based on the specified criteria
							if (daysOverdue <= 7) {
								fine = daysOverdue * 50; // Rs. 50 per additional day for up to 7 days
							} else {
								fine = 7 * 50 + (daysOverdue - 7) * 100; // Rs. 50 per additional day up to 7 days, then Rs. 100 per additional day
							}
						}
					}
				}
				connection.Close();
			}
			return (true, "", fine);
		} catch (Exception ex) {
			return (false, $"Error calculating fine: {ex.Message}", fine);
		}
	}
}
