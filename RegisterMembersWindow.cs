// DON-CODE
using Terminal.Gui;
using System.Text.RegularExpressions;

public class RegisterMembersWindow {
    public Window CreateRegisterMembersWindow(Window leftWindow, String databasePath){
        var registerMembersWindow = new Window() {
            X = Pos.Right(leftWindow), // Position it to the right of the leftWindow
            Y = 1, // Leave one row for the top-level menu
            Width = Dim.Fill(), // Fill the remaining space
            Height = Dim.Fill()
        };
		var mainTitleLabel = new Label("  __  __           _                   _   \n |  \\/  |___ _ __ | |__ ___ _ _ ___  _| |_ \n | |\\/| / -_| '  \\| '_ / -_| '_(_-< |_   _|\n |_|  |_\\___|_|_|_|_.__\\___|_| /__/   |_|") {
			X = 6,
			Y = 1,
			Width = Dim.Fill(),
			Height = 1,
			LayoutStyle = LayoutStyle.Computed, // Use computed layout
		};
		var firstNameLabel = new Label("First Name") {
			X = 2,
			Y = Pos.Bottom(mainTitleLabel) + 2,
			Width = 17
		};
		var firstNameInput = new TextField ("") {
			X = Pos.Right(firstNameLabel) + 1,
			Y = Pos.Top(firstNameLabel),
			Width = 30,
			Height = 1
		};
		var lastNameLabel = new Label("Last Name") {
			X = Pos.Left(firstNameLabel),
			Y = Pos.Bottom(firstNameLabel) + 1,
			Width = 17
		};
		var lastNameInput = new TextField ("") {
			X = Pos.Right(lastNameLabel) + 1,
			Y = Pos.Top(lastNameLabel),
			Width = 30,
			Height = 1
		};
		var emailLabel = new Label("Email") {
			X = Pos.Left(firstNameLabel),
			Y = Pos.Bottom(lastNameLabel) + 1,
			Width = 17
		};
		var emailInput = new TextField ("") {
			X = Pos.Right(emailLabel) + 1,
			Y = Pos.Top(emailLabel),
			Width = 30,
			Height = 1
		};
		var phoneNumberLabel = new Label("Phone Number") {
			X = Pos.Left(firstNameLabel),
			Y = Pos.Bottom(emailLabel) + 1,
			Width = 17
		};
		var phoneNumberInput = new TextField ("") {
			X = Pos.Right(phoneNumberLabel) + 1,
			Y = Pos.Top(phoneNumberLabel),
			Width = 30,
			Height = 1
		};
		var clearButton = new Button("Clear") {
			X = Pos.Left(firstNameLabel) + 29,
			Y = Pos.Bottom(phoneNumberLabel) + 3,			
		};
		clearButton.Clicked += () => {
            // Clear the text fields when the Clear button is clicked
            firstNameInput.Text = "";
            lastNameInput.Text = "";
            emailInput.Text = "";
            phoneNumberInput.Text = "";
        };
		var okButton = new Button("  Ok ") {
			X = Pos.Right(clearButton) + 1,
			Y = Pos.Bottom(phoneNumberLabel) + 3,
		};
		okButton.Clicked += () => {
			string firstName = firstNameInput.Text.ToString();
			string lastName = lastNameInput.Text.ToString();
			string email = emailInput.Text.ToString();
			string phoneNumber = phoneNumberInput.Text.ToString();

			// Check if any of the required fields are empty
			if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(email)) {
				MessageBox.ErrorQuery("Error", "All required fields must be filled in.", "OK");
				return; // Exit the event handler
			}

			// Check if the phone number contains only digits or starts with "+"
			if (!Regex.IsMatch(phoneNumber, @"^(\+)?\d+$")) {
				MessageBox.ErrorQuery("Error", "Invalid phone number format. Please enter a valid phone number.", "OK");
				return; // Exit the event handler
			}
			
			// Regular expression for basic email format validation
			string emailPattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";

			if (!Regex.IsMatch(email, emailPattern)) {
				MessageBox.ErrorQuery("Error", "Invalid email format. Please enter a valid email address.", "OK");
				return; // Exit the event handler
			}

			// Call the RegisterMember function and check the result
			var (success, message) = new LibraryFunctions(databasePath).RegisterMember(firstName, lastName, email, phoneNumber);

			if (success) {
				MessageBox.Query("Success", message, "OK");
			} else {
				MessageBox.ErrorQuery("Error", message, "OK");
			}

			// Clear the text fields when the Clear button is clicked
			firstNameInput.Text = "";
			lastNameInput.Text = "";
			emailInput.Text = "";
			phoneNumberInput.Text = "";
		};
		firstNameInput.FocusFirst();
		registerMembersWindow.Add(
			mainTitleLabel,
			firstNameLabel, firstNameInput,
			lastNameLabel, lastNameInput,
			emailLabel, emailInput,
			phoneNumberLabel, phoneNumberInput,
			clearButton, okButton
		);        
        return registerMembersWindow;
    }
    
    
}