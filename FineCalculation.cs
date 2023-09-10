// DON-CODE
using Terminal.Gui;
using System;
using System.Text.RegularExpressions;

public class FineCalculationWindow {
	private bool IsNumeric(string input) {
		return int.TryParse(input, out _);
	}
	private bool IsValidDateFormat(string date) {
		DateTime parsedDate;
		return DateTime.TryParseExact(date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out parsedDate);
	}
    public Window CreateFineCalculationWindow(Window leftWindow, String databasePath) {
		string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
        var createFineCalculationWindow = new Window() {
            X = Pos.Right(leftWindow), // Position it to the right of the leftWindow
            Y = 1, // Leave one row for the top-level menu
            Width = Dim.Fill(), // Fill the remaining space
            Height = Dim.Fill()
        };
        var mainTitleLabel = new Label("  ___ _             ___      _      \n | __(_)_ _  ___   / __|__ _| |__   \n | _|| | ' \\/ -_) | (__/ _` | / _|_ \n |_| |_|_||_\\___|  \\___\\__,_|_\\__(_)") {
            X = 6,
            Y = 1,
            Width = Dim.Fill(),
            Height = 1,
            LayoutStyle = LayoutStyle.Computed, // Use computed layout
        };
        var transactionIDLabel = new Label("Transaction ID") {
            X = 2,
            Y = Pos.Bottom(mainTitleLabel) + 2,
            Width = 17
        };
        var transactionIDInput = new TextField ("") {
            X = Pos.Right(transactionIDLabel) + 1,
            Y = Pos.Top(transactionIDLabel),
            Width = 30,
            Height = 1
        };
        var returnDateLabel = new Label("Return Date") {
            X = Pos.Left(transactionIDLabel),
            Y = Pos.Bottom(transactionIDLabel) + 1,
            Width = 17
        };
        var returnDateInput = new TextField ("") {
            X = Pos.Right(returnDateLabel) + 1,
            Y = Pos.Top(returnDateLabel),
            Width = 30,
            Height = 1
        };
        var explainLabel = new Label("*[Date format: YYYY-MM-DD]") {
            X = Pos.Left(returnDateLabel) + 10,
            Y = Pos.Bottom(returnDateLabel) + 1,            
        };
        var clearButton = new Button("Clear") {
            X = Pos.Left(transactionIDLabel) + 29,
            Y = Pos.Bottom(returnDateLabel) + 3,            
        };
        clearButton.Clicked += () => {
            // Clear the text fields when the Clear button is clicked
            transactionIDInput.Text = "";
            returnDateInput.Text = "";
        };
        var okButton = new Button("  Ok ") {
            X = Pos.Right(clearButton) + 1,
            Y = Pos.Bottom(returnDateLabel) + 3,
        };
        okButton.Clicked += () => {
            string transactionID = transactionIDInput.Text.ToString();
            string returnDate = returnDateInput.Text.ToString();
            // Check if any of the required fields are empty
            if (string.IsNullOrWhiteSpace(transactionID) || string.IsNullOrWhiteSpace(returnDate)) {
                MessageBox.ErrorQuery("Error", "All required fields must be filled in.", "OK");
                return; // Exit the event handler
            }
            // Check if Transaction ID contains only numbers
            if (!IsNumeric(transactionID)) {
                MessageBox.ErrorQuery("Error", "Transaction ID must contain only numbers.", "OK");
                return; // Exit the event handler
            }
            // Check if Return Date is in the format "YYYY-MM-DD"
            if (!IsValidDateFormat(returnDate)) {
                MessageBox.ErrorQuery("Error", "Return Date must be in the format 'YYYY-MM-DD'.", "OK");
                return; // Exit the event handler
            }
            // Convert strings to integers and parse dates
            int parsedTransactionID;
            if (!int.TryParse(transactionID, out parsedTransactionID)) {
                MessageBox.ErrorQuery("Error", "Failed to parse Transaction ID.", "OK");
                return; // Exit the event handler
            }
            DateTime parsedReturnDate;
            if (!DateTime.TryParse(returnDate, out parsedReturnDate)) {
                MessageBox.ErrorQuery("Error", "Failed to parse Return Date.", "OK");
                return; // Exit the event handler
            }
            // Call the CalculateFine function
            var (success, message, fine) = new LibraryFunctions(databasePath).CalculateFine(parsedTransactionID, parsedReturnDate);
            if (success) {
                string formattedFine;
                if (fine >= 0) {
                    formattedFine = $"Fine amount: {fine:#,##0.00} රු"; // Format fine with LKR symbol
                } else {
                    formattedFine = "No fine.";
                }
                MessageBox.Query("Fine Calculation Result", formattedFine, "OK");
            } else {
                MessageBox.ErrorQuery("Error", message, "OK");
            }
        };
        
        transactionIDLabel.FocusFirst();
		createFineCalculationWindow.Add(
			mainTitleLabel,
			transactionIDLabel, transactionIDInput,
            returnDateLabel, returnDateInput,
			explainLabel,
			clearButton, okButton
		);        
        return createFineCalculationWindow;
    }
}