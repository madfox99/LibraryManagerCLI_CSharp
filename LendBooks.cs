// DON-CODE
using Terminal.Gui;
using System;
using System.Text.RegularExpressions;

public class LendBooksWindow {
	private bool IsNumeric(string input) {
		return int.TryParse(input, out _);
	}
	private bool IsValidDateFormat(string date) {
		DateTime parsedDate;
		return DateTime.TryParseExact(date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out parsedDate);
	}
    public Window CreateLendBooksWindow(Window leftWindow, String databasePath) {
		string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
        var createLendBooksWindow = new Window() {
            X = Pos.Right(leftWindow), // Position it to the right of the leftWindow
            Y = 1, // Leave one row for the top-level menu
            Width = Dim.Fill(), // Fill the remaining space
            Height = Dim.Fill()
        };
        var mainTitleLabel = new Label("  _                _   ___         _      \n | |   ___ _ _  __| | | _ )___ ___| |_____\n | |__/ -_| ' \\/ _` | | _ / _ / _ | / (_-<\n |____\\___|_||_\\__,_| |___\\___\\___|_\\_/__/") {
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
        var memberIDLabel = new Label("Member ID") {
            X = Pos.Left(bookIDLabel),
            Y = Pos.Bottom(bookIDLabel) + 1,
            Width = 17
        };
        var memberIDInput = new TextField ("") {
            X = Pos.Right(memberIDLabel) + 1,
            Y = Pos.Top(memberIDLabel),
            Width = 30,
            Height = 1
        };
        var lendDateLabel = new Label("Lend Date") {
            X = Pos.Left(bookIDLabel),
            Y = Pos.Bottom(memberIDLabel) + 1,
            Width = 17
        };
        var lendDateInput = new TextField (currentDate) {
            X = Pos.Right(lendDateLabel) + 1,
            Y = Pos.Top(lendDateLabel),
            Width = 30,
            Height = 1
        };
        var dueDateLabel = new Label("Due Date") {
            X = Pos.Left(bookIDLabel),
            Y = Pos.Bottom(lendDateLabel) + 1,
            Width = 17
        };
        var dueDateInput = new TextField ("") {
            X = Pos.Right(dueDateLabel) + 1,
            Y = Pos.Top(dueDateLabel),
            Width = 30,
            Height = 1
        };
        var explainLabel = new Label("*[Date format: YYYY-MM-DD]") {
            X = Pos.Left(bookIDLabel) + 10,
            Y = Pos.Bottom(dueDateLabel) + 1,            
        };
        var clearButton = new Button("Clear") {
            X = Pos.Left(bookIDLabel) + 29,
            Y = Pos.Bottom(dueDateLabel) + 3,            
        };
        clearButton.Clicked += () => {
            // Clear the text fields when the Clear button is clicked
            bookIDInput.Text = "";
            memberIDInput.Text = "";
            lendDateInput.Text = "";
            dueDateInput.Text = "";
        };
        var okButton = new Button("  Ok ") {
            X = Pos.Right(clearButton) + 1,
            Y = Pos.Bottom(dueDateLabel) + 3,
        };
		okButton.Clicked += () => {
    string bookID = bookIDInput.Text.ToString();
    string memberID = memberIDInput.Text.ToString();
    string lendDate = lendDateInput.Text.ToString();
    string dueDate = dueDateInput.Text.ToString();

    // Check if any of the required fields are empty
    if (string.IsNullOrWhiteSpace(bookID) || string.IsNullOrWhiteSpace(memberID) || string.IsNullOrWhiteSpace(lendDate) || string.IsNullOrWhiteSpace(dueDate)) {
        MessageBox.ErrorQuery("Error", "All required fields must be filled in.", "OK");
        return; // Exit the event handler
    }

    // Check if Book ID and Member ID contain only numbers
    if (!IsNumeric(bookID) || !IsNumeric(memberID)) {
        MessageBox.ErrorQuery("Error", "Book ID and Member ID must contain only numbers.", "OK");
        return; // Exit the event handler
    }

    // Check if lendDate and dueDate are in the format "YYYY-MM-DD"
    if (!IsValidDateFormat(lendDate) || !IsValidDateFormat(dueDate)) {
        MessageBox.ErrorQuery("Error", "Lend Date and Due Date must be in the format 'YYYY-MM-DD'.", "OK");
        return; // Exit the event handler
    }

    // Convert strings to integers and parse dates
    int parsedBookID, parsedMemberID;
    if (!int.TryParse(bookID, out parsedBookID) || !int.TryParse(memberID, out parsedMemberID)) {
        MessageBox.ErrorQuery("Error", "Failed to parse Book ID or Member ID.", "OK");
        return; // Exit the event handler
    }

    DateTime parsedLendDate, parsedDueDate;
    if (!DateTime.TryParse(lendDate, out parsedLendDate) || !DateTime.TryParse(dueDate, out parsedDueDate)) {
        MessageBox.ErrorQuery("Error", "Failed to parse Lend Date or Due Date.", "OK");
        return; // Exit the event handler
    }

    // Check if the due date is before today's date
    if (parsedDueDate < DateTime.Today) {
        MessageBox.ErrorQuery("Error", "Due Date cannot be before today's date.", "OK");
        return; // Exit the event handler
    }

    // Call the LendBook method and check the result
    var (success, message) = new LibraryFunctions(databasePath).LendBook(parsedBookID, parsedMemberID, parsedLendDate, parsedDueDate);

    if (success) {
        MessageBox.Query("Success", message, "OK");
        // Clear the text fields when the operation is complete
        bookIDInput.Text = "";
        memberIDInput.Text = "";
        lendDateInput.Text = "";
        dueDateInput.Text = "";
    } else {
        MessageBox.ErrorQuery("Error", message, "OK");
    }
};


        bookIDInput.FocusFirst();
		createLendBooksWindow.Add(
			mainTitleLabel,
			bookIDLabel, bookIDInput,
			memberIDLabel, memberIDInput,
			lendDateLabel, lendDateInput,
			dueDateLabel, dueDateInput,
			explainLabel,
			clearButton, okButton
		);        
        return createLendBooksWindow;
    }
}