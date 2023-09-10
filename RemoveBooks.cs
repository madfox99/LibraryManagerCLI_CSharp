// DON-CODE
using Terminal.Gui;
using System;
using System.Text.RegularExpressions;

public class RemoveBooksWindow {
	private bool IsNumeric(string input) {
		return int.TryParse(input, out _);
	}
    public Window CreateRemoveBooksWindow(Window leftWindow, String databasePath) {
        var createRemoveBooksWindow = new Window() {
            X = Pos.Right(leftWindow), // Position it to the right of the leftWindow
            Y = 1, // Leave one row for the top-level menu
            Width = Dim.Fill(), // Fill the remaining space
            Height = Dim.Fill()
        };
        var mainTitleLabel = new Label("  ___         _         \n | _ )___ ___| |__  ___ \n | _ / _ / _ | / / |___|\n |___\\___\\___|_\\_\\      ") {
            X = 6,
            Y = 1,
            Width = Dim.Fill(),
            Height = 1,
            LayoutStyle = LayoutStyle.Computed, // Use computed layout
        };
        var bookIDLabel = new Label("Book ID") {
            X = 2,
            Y = Pos.Bottom(mainTitleLabel) + 2,
            Width = 17
        };
        var bookIDInput = new TextField ("") {
            X = Pos.Right(bookIDLabel) + 1,
            Y = Pos.Top(bookIDLabel),
            Width = 30,
            Height = 1
        };
        var isbnLabel = new Label("ISBN") {
            X = Pos.Left(bookIDLabel),
            Y = Pos.Bottom(bookIDLabel) + 1,
            Width = 17
        };
        var isbnInput = new TextField ("") {
            X = Pos.Right(isbnLabel) + 1,
            Y = Pos.Top(isbnLabel),
            Width = 30,
            Height = 1
        };
        var clearButton = new Button("Clear") {
            X = Pos.Left(bookIDLabel) + 29,
            Y = Pos.Bottom(isbnLabel) + 3,            
        };
        clearButton.Clicked += () => {
            // Clear the text fields when the Clear button is clicked
            bookIDInput.Text = "";
            isbnInput.Text = "";
        };
        var okButton = new Button("  Ok ") {
            X = Pos.Right(clearButton) + 1,
            Y = Pos.Bottom(isbnLabel) + 3,
        };
        okButton.Clicked += () => {
            string bookID = bookIDInput.Text.ToString();
            string isbn = isbnInput.Text.ToString();

            // Check if any of the required fields are empty
            if (string.IsNullOrWhiteSpace(bookID) || string.IsNullOrWhiteSpace(isbn)) {
                MessageBox.ErrorQuery("Error", "All required fields must be filled in.", "OK");
                return; // Exit the event handler
            }

            // Check if Book ID & ISBN contains only numbers
            if (!IsNumeric(bookID)) {
                MessageBox.ErrorQuery("Error", "Book ID must contain only numbers.", "OK");
                return; // Exit the event handler
            }

            // Convert strings to integers and parse dates
            int parsedbookID;
            if (!int.TryParse(bookID, out parsedbookID)) {
                MessageBox.ErrorQuery("Error", "Failed to parse Book ID.", "OK");
                return; // Exit the event handler
            }

            // Ask for confirmation
            int confirmAction = MessageBox.Query("Confirm Action", "Are you sure you want to remove this book?", "Yes", "No");

            if (confirmAction == 0) {
                // Call the RemoveBook function
                var (success, message) = new LibraryFunctions(databasePath).RemoveBook(parsedbookID, isbn);

                if (success) {
                    MessageBox.Query("Remove Book Result", message, "OK");
                    bookIDInput.Text = "";
                    isbnInput.Text = "";
                } else {
                    MessageBox.ErrorQuery("Error", message, "OK");
                }
            }
        };
       
        bookIDInput.FocusFirst();
		createRemoveBooksWindow.Add(
			mainTitleLabel,
			bookIDLabel, bookIDInput,
            isbnLabel, isbnInput, 
			clearButton, okButton
		);        
        return createRemoveBooksWindow;
    }
}