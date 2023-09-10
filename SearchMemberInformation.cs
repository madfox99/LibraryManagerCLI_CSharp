// DON-CODE
using Terminal.Gui;
using NStack;
using System;
using System.Runtime.InteropServices;
using System.Data;
using System.Collections.Generic;

public class SearchMemberInformationWindow {
	
	DataTable dt;
	
    public Window CreateSearchMemberInformationWindow(Window leftWindow, String databasePath){
        var searchMemberInformationWindow = new Window() {
            X = Pos.Right(leftWindow), // Position it to the right of the leftWindow
            Y = 1, // Leave one row for the top-level menu
            Width = Dim.Fill(), // Fill the remaining space
            Height = Dim.Fill()
        };
        var mainTitleLabel = new Label("  ___                 _      __  __ ___ \n / __|___ __ _ _ _ __| |_   |  \\/  |__ \\\n \\__ / -_/ _` | '_/ _| ' \\  | |\\/| | /_/\n |___\\___\\__,_|_| \\__|_||_| |_|  |_|(_) ") {
            X = 4,
            Y = 1,
            Width = Dim.Fill(),
            Height = 1,
            LayoutStyle = LayoutStyle.Computed, // Use computed layout
        };

        var idCheckBox = new CheckBox("ID") {
            X = 2,
            Y = Pos.Bottom(mainTitleLabel) + 2,
            Width = 12
        };
		var firstNameCheckBox = new CheckBox("First Name") {
            X = Pos.Left(idCheckBox),
            Y = Pos.Bottom(idCheckBox) + 1,
            Width = 12
        };
		var lastNameCheckBox = new CheckBox("Last Name") {
            X = Pos.Left(idCheckBox),
            Y = Pos.Bottom(firstNameCheckBox) + 1,
            Width = 12
        };
		var emailCheckBox = new CheckBox("Email") {
            X = Pos.Left(idCheckBox),
            Y = Pos.Bottom(lastNameCheckBox) + 1,
            Width = 12
        };
		var phoneNoCheckBox = new CheckBox("Phone No.") {
            X = Pos.Left(idCheckBox),
            Y = Pos.Bottom(emailCheckBox) + 1,
            Width = 12
        };
        var idInput = new TextField () {
            X = Pos.Right(idCheckBox) + 1,
            Y = Pos.Top(idCheckBox),
            Width = 30,
            Height = 1
        };
		var firstNameInput = new TextField () {
            X = Pos.Right(firstNameCheckBox) + 1,
            Y = Pos.Top(firstNameCheckBox),
            Width = 30,
            Height = 1
        };
		var lastNameInput = new TextField () {
            X = Pos.Right(lastNameCheckBox) + 1,
            Y = Pos.Top(lastNameCheckBox),
            Width = 30,
            Height = 1
        };
		var emailInput = new TextField () {
            X = Pos.Right(emailCheckBox) + 1,
            Y = Pos.Top(emailCheckBox),
            Width = 30,
            Height = 1
        };
		var phoneNoInput = new TextField () {
            X = Pos.Right(phoneNoCheckBox) + 1,
            Y = Pos.Top(phoneNoCheckBox),
            Width = 30,
            Height = 1
        };		
		var clearButton = new Button("Clear") {
			X = Pos.Left(phoneNoCheckBox) + 24,
			Y = Pos.Bottom(phoneNoCheckBox) + 2,			
		};
		var tableView = new TableView() {
			X = Pos.Left(idCheckBox),
			Y = Pos.Bottom(phoneNoCheckBox) + 5,
			Width = 90,
			Height = 17,
		};
		dt = new DataTable();
		dt.Columns.Add("MemberID");
		dt.Columns.Add("FirstName");
		dt.Columns.Add("LastName");
		dt.Columns.Add("Email");
		dt.Columns.Add("PhoneNumber");
		tableView.Table=dt;
		clearButton.Clicked += () => {
			// Clear the text fields when the Clear button is clicked
			idInput.Text = "";
			firstNameInput.Text = "";
			lastNameInput.Text = "";
			emailInput.Text = "";
			phoneNoInput.Text = "";
			dt = new DataTable();
			dt.Columns.Add("MemberID");
			dt.Columns.Add("FirstName");
			dt.Columns.Add("LastName");
			dt.Columns.Add("Email");
			dt.Columns.Add("PhoneNumber");
			tableView.Table=dt;
		};
		var okButton = new Button("  Ok ") {
			X = Pos.Right(clearButton) + 1,
			Y = Pos.Top(clearButton),
		};		
		okButton.Clicked += () => {
			string selectedColumn = "";
			string searchValue = "";

			if (idCheckBox.Checked) {
				selectedColumn = "MemberID";
				searchValue = idInput.Text.ToString();
				if (!int.TryParse(searchValue, out int intValue)) {
					MessageBox.ErrorQuery("Error", "ID must be a valid number.", "OK");
					return; // Exit the event handler
				}
			}
			else if (firstNameCheckBox.Checked) {
				selectedColumn = "FirstName";
				searchValue = firstNameInput.Text.ToString();
			}
			else if (lastNameCheckBox.Checked) {
				selectedColumn = "LastName";
				searchValue = lastNameInput.Text.ToString();
			}
			else if (emailCheckBox.Checked) {
				selectedColumn = "Email";
				searchValue = emailInput.Text.ToString();
			}
			else if (phoneNoCheckBox.Checked) {
				selectedColumn = "PhoneNumber";
				searchValue = phoneNoInput.Text.ToString();
			}
			
			// Perform the search
			string[] searchCriteria = new string[] { selectedColumn, searchValue };
			var (memberInfo, valueFound) = new LibraryFunctions(databasePath).SearchMemberInformation(searchCriteria);
			
			dt = new DataTable();
			dt.Columns.Add("MemberID");
			dt.Columns.Add("FirstName");
			dt.Columns.Add("LastName");
			dt.Columns.Add("Email");
			dt.Columns.Add("PhoneNumber");
			tableView.Table=dt;
			
			if (valueFound) {
				List<string> valueGroups = new List<string>();
				List<string> currentGroup = new List<string>();

				foreach (var item in memberInfo) {
					currentGroup.Add(item.Value.ToString());
					if (currentGroup.Count == 5) {
						valueGroups.Add(string.Join("¥", currentGroup));
						currentGroup.Clear();
					}
				}
				if (currentGroup.Count > 0) {
					valueGroups.Add(string.Join("¥", currentGroup));
				}
				string[] valueGroupsArray = valueGroups.ToArray();
				if (valueGroupsArray.Length > 0) {
					foreach (string groupValue in valueGroupsArray) {
						dt.Rows.Add(groupValue.Split('¥'));
					}
				}else {
					MessageBox.ErrorQuery("Information", "No matching records found.", "OK");
				}
			}
			else {
				MessageBox.ErrorQuery("Information", "No matching records found.", "OK");
			}
		};
        idInput.TextChanged += (args) => {
            if (!string.IsNullOrWhiteSpace(idInput.Text.ToString())) {
                idCheckBox.Checked = true;
				firstNameCheckBox.Checked = false;
                lastNameCheckBox.Checked = false;
                emailCheckBox.Checked = false;
                phoneNoCheckBox.Checked = false;
				firstNameInput.Text = "";
				lastNameInput.Text = "";
				emailInput.Text = "";
				phoneNoInput.Text = "";
            } else {
                idCheckBox.Checked = false;
            }
        };
        firstNameInput.TextChanged += (args) => {
            if (!string.IsNullOrWhiteSpace(firstNameInput.Text.ToString())) {
                idCheckBox.Checked = false;
				firstNameCheckBox.Checked = true;
                lastNameCheckBox.Checked = false;
                emailCheckBox.Checked = false;
                phoneNoCheckBox.Checked = false;
				idInput.Text = "";
				lastNameInput.Text = "";
				emailInput.Text = "";
				phoneNoInput.Text = "";
            } else {
                firstNameCheckBox.Checked = false;
            }
        };
        lastNameInput.TextChanged += (args) => {
            if (!string.IsNullOrWhiteSpace(lastNameInput.Text.ToString())) {
                idCheckBox.Checked = false;
				firstNameCheckBox.Checked = false;
                lastNameCheckBox.Checked = true;
                emailCheckBox.Checked = false;
                phoneNoCheckBox.Checked = false;
				idInput.Text = "";
				firstNameInput.Text = "";
				emailInput.Text = "";
				phoneNoInput.Text = "";
            } else {
                lastNameCheckBox.Checked = false;
            }
        };
        emailInput.TextChanged += (args) => {
            if (!string.IsNullOrWhiteSpace(emailInput.Text.ToString())) {
                idCheckBox.Checked = false;
				firstNameCheckBox.Checked = false;
                lastNameCheckBox.Checked = false;
                emailCheckBox.Checked = true;
                phoneNoCheckBox.Checked = false;
				idInput.Text = "";
				firstNameInput.Text = "";
				lastNameInput.Text = "";
				phoneNoInput.Text = "";
            } else {
                emailCheckBox.Checked = false;
            }
        };
        phoneNoInput.TextChanged += (args) => {
            if (!string.IsNullOrWhiteSpace(phoneNoInput.Text.ToString())) {
                idCheckBox.Checked = false;
				firstNameCheckBox.Checked = false;
                lastNameCheckBox.Checked = false;
                emailCheckBox.Checked = false;
                phoneNoCheckBox.Checked = true;
				idInput.Text = "";
				firstNameInput.Text = "";
				lastNameInput.Text = "";
				emailInput.Text = "";
            } else {
                phoneNoCheckBox.Checked = false;
            }
        };
		idInput.FocusFirst();
        searchMemberInformationWindow.Add(
            mainTitleLabel,
            idCheckBox, idInput,
            firstNameCheckBox, firstNameInput,
            lastNameCheckBox, lastNameInput,
            emailCheckBox, emailInput,
            phoneNoCheckBox, phoneNoInput,
			clearButton, okButton,
			tableView
        );		
        return searchMemberInformationWindow;
    }
}
