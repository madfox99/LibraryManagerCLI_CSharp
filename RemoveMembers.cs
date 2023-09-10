// DON-CODE
using Terminal.Gui;
using System;
using System.Text.RegularExpressions;

public class RemoveMembersWindow {
	private bool IsNumeric(string input) {
		return int.TryParse(input, out _);
	}
    // Add a method to validate email format
    private bool IsValidEmail(string email) {
        string pattern = @"^\S+@\S+\.\S+$";
        return Regex.IsMatch(email, pattern);
    }
    public Window CreateRemoveMembersWindow(Window leftWindow, String databasePath) {
        var createRemoveMembersWindow = new Window() {
            X = Pos.Right(leftWindow), // Position it to the right of the leftWindow
            Y = 1, // Leave one row for the top-level menu
            Width = Dim.Fill(), // Fill the remaining space
            Height = Dim.Fill()
        };
        var mainTitleLabel = new Label("  __  __           _                  \n |  \\/  |___ _ __ | |__ ___ _ _   ___ \n | |\\/| / -_| '  \\| '_ / -_| '_| |___|\n |_|  |_\\___|_|_|_|_.__\\___|_|        ") {
            X = 6,
            Y = 1,
            Width = Dim.Fill(),
            Height = 1,
            LayoutStyle = LayoutStyle.Computed, // Use computed layout
        };
        var memberIDLabel = new Label("Member ID") {
            X = 2,
            Y = Pos.Bottom(mainTitleLabel) + 2,
            Width = 17
        };
        var memberIDInput = new TextField ("") {
            X = Pos.Right(memberIDLabel) + 1,
            Y = Pos.Top(memberIDLabel),
            Width = 30,
            Height = 1
        };
        var emailLabel = new Label("Email") {
            X = Pos.Left(memberIDLabel),
            Y = Pos.Bottom(memberIDLabel) + 1,
            Width = 17
        };
        var emailInput = new TextField ("") {
            X = Pos.Right(emailLabel) + 1,
            Y = Pos.Top(emailLabel),
            Width = 30,
            Height = 1
        };
        var clearButton = new Button("Clear") {
            X = Pos.Left(memberIDLabel) + 29,
            Y = Pos.Bottom(emailLabel) + 3,            
        };
        clearButton.Clicked += () => {
            // Clear the text fields when the Clear button is clicked
            memberIDInput.Text = "";
            emailInput.Text = "";
        };
        var okButton = new Button("  Ok ") {
            X = Pos.Right(clearButton) + 1,
            Y = Pos.Bottom(emailLabel) + 3,
        };
okButton.Clicked += () => {
    string memberID = memberIDInput.Text.ToString();
    string email = emailInput.Text.ToString();

    // Check if any of the required fields are empty
    if (string.IsNullOrWhiteSpace(memberID) || string.IsNullOrWhiteSpace(email)) {
        MessageBox.ErrorQuery("Error", "All required fields must be filled in.", "OK");
        return; // Exit the event handler
    }

    // Check if Member ID contains only numbers
    if (!IsNumeric(memberID)) {
        MessageBox.ErrorQuery("Error", "Member ID must contain only numbers.", "OK");
        return; // Exit the event handler
    }

    // Check if email is in a valid format using a regular expression
    if (!IsValidEmail(email)) {
        MessageBox.ErrorQuery("Error", "Invalid email format.", "OK");
        return; // Exit the event handler
    }

    // Convert strings to integers and call the RemoveMember function
    int parsedMemberID;
    if (!int.TryParse(memberID, out parsedMemberID)) {
        MessageBox.ErrorQuery("Error", "Failed to parse Member ID.", "OK");
        return; // Exit the event handler
    }

    // Ask for confirmation
    int confirmAction = MessageBox.Query("Confirm Action", "Are you sure you want to remove this member?", "Yes", "No");

    if (confirmAction == 0) {
        // User clicked "Yes"
        // Call the RemoveMember function
        var (success, message) = new LibraryFunctions(databasePath).RemoveMember(parsedMemberID, email);

        if (success) {
            MessageBox.Query("Remove Member Result", message, "OK");
            memberIDInput.Text = "";
            emailInput.Text = "";
        } else {
            MessageBox.ErrorQuery("Error", message, "OK");
        }
    }
};

       
        memberIDInput.FocusFirst();
		createRemoveMembersWindow.Add(
			mainTitleLabel,
			memberIDLabel, memberIDInput,
            emailLabel, emailInput, 
			clearButton, okButton
		);        
        return createRemoveMembersWindow;
    }
}