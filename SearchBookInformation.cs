// DON-CODE
using Terminal.Gui;
using NStack;
using System;
using System.Runtime.InteropServices;
using System.Data;
using System.Collections.Generic;

public class SearchBookInformationWindow {	
	DataTable dt;	
    public Window CreateSearchBookInformationWindow(Window leftWindow, String databasePath){
        var searchBookInformationWindow = new Window() {
            X = Pos.Right(leftWindow), // Position it to the right of the leftWindow
            Y = 1, // Leave one row for the top-level menu
            Width = Dim.Fill(), // Fill the remaining space
            Height = Dim.Fill()
        };
        var mainTitleLabel = new Label("  ___                 _      ___ ___ \n / __|___ __ _ _ _ __| |_   | _ |__ \\\n \\__ / -_/ _` | '_/ _| ' \\  | _ \\ /_/\n |___\\___\\__,_|_| \\__|_||_| |___/(_)") {
            X = 4,
            Y = 1,
            Width = Dim.Fill(),
            Height = 1,
            LayoutStyle = LayoutStyle.Computed, // Use computed layout
        };
        var idCheckBox = new CheckBox("ID") {
            X = 2,
            Y = Pos.Bottom(mainTitleLabel) + 2,
            Width = 18
        };
		var titleCheckBox = new CheckBox("Title") {
            X = Pos.Left(idCheckBox),
            Y = Pos.Bottom(idCheckBox) + 1,
            Width = 18
        };
		var authorCheckBox = new CheckBox("Author") {
            X = Pos.Left(idCheckBox),
            Y = Pos.Bottom(titleCheckBox) + 1,
            Width = 18
        };
		var isbnCheckBox = new CheckBox("ISBN") {
            X = Pos.Left(idCheckBox),
            Y = Pos.Bottom(authorCheckBox) + 1,
            Width = 18
        };
		var publicationYearCheckBox = new CheckBox("Publication Year") {
            X = Pos.Left(idCheckBox),
            Y = Pos.Bottom(isbnCheckBox) + 1,
            Width = 18
        };
		var publisherCheckBox = new CheckBox("Publisher") {
            X = Pos.Left(idCheckBox),
            Y = Pos.Bottom(publicationYearCheckBox) + 1,
            Width = 18
        };
		var languageCheckBox = new CheckBox("Language") {
            X = Pos.Left(idCheckBox),
            Y = Pos.Bottom(publisherCheckBox) + 1,
            Width = 18
        };
		var locationCheckBox = new CheckBox("Location") {
            X = Pos.Left(idCheckBox),
            Y = Pos.Bottom(languageCheckBox) + 1,
            Width = 18
        };
        var idInput = new TextField () {
            X = Pos.Right(idCheckBox) + 1,
            Y = Pos.Top(idCheckBox),
            Width = 30,
            Height = 1
        };
		var titleInput = new TextField () {
            X = Pos.Right(titleCheckBox) + 1,
            Y = Pos.Top(titleCheckBox),
            Width = 30,
            Height = 1
        };
		var authorInput = new TextField () {
            X = Pos.Right(authorCheckBox) + 1,
            Y = Pos.Top(authorCheckBox),
            Width = 30,
            Height = 1
        };
		var isbnInput = new TextField () {
            X = Pos.Right(isbnCheckBox) + 1,
            Y = Pos.Top(isbnCheckBox),
            Width = 30,
            Height = 1
        };	
		var publicationYearInput = new TextField () {
            X = Pos.Right(publicationYearCheckBox) + 1,
            Y = Pos.Top(publicationYearCheckBox),
            Width = 30,
            Height = 1
        };
		var publisherInput = new TextField () {
            X = Pos.Right(publisherCheckBox) + 1,
            Y = Pos.Top(publisherCheckBox),
            Width = 30,
            Height = 1
        };
		var languageInput = new TextField () {
            X = Pos.Right(languageCheckBox) + 1,
            Y = Pos.Top(languageCheckBox),
            Width = 30,
            Height = 1
        };
		var locationInput = new TextField () {
            X = Pos.Right(locationCheckBox) + 1,
            Y = Pos.Top(locationCheckBox),
            Width = 30,
            Height = 1
        };
		var clearButton = new Button("Clear") {
			X = Pos.Left(locationCheckBox) + 30,
			Y = Pos.Bottom(locationCheckBox) + 2,			
		};
		var tableView = new TableView() {
			X = Pos.Left(idCheckBox),
			Y = Pos.Bottom(locationCheckBox) + 5,
			Width = 120,
			Height = 17,
		};
		dt = new DataTable();
		dt.Columns.Add("BookID");
		dt.Columns.Add("Title");
		dt.Columns.Add("Author");
		dt.Columns.Add("ISBN");
		dt.Columns.Add("Quantity");
		dt.Columns.Add("Available Quantity");
		dt.Columns.Add("Genre");
		dt.Columns.Add("Publication Year");
		dt.Columns.Add("Publisher");
		dt.Columns.Add("Language");
		dt.Columns.Add("Description");
		dt.Columns.Add("Location");
		tableView.Table=dt;
		clearButton.Clicked += () => {
			// Clear the text fields when the Clear button is clicked
			idInput.Text = "";
			titleInput.Text = "";
			authorInput.Text = "";
			isbnInput.Text = "";
			publicationYearInput.Text = "";
			publisherInput.Text = "";
			languageInput.Text = "";
			locationInput.Text = "";
			dt = new DataTable();
			dt.Columns.Add("BookID");
			dt.Columns.Add("Title");
			dt.Columns.Add("Author");
			dt.Columns.Add("ISBN");
			dt.Columns.Add("Quantity");
			dt.Columns.Add("Available Quantity");
			dt.Columns.Add("Genre");
			dt.Columns.Add("Publication Year");
			dt.Columns.Add("Publisher");
			dt.Columns.Add("Language");
			dt.Columns.Add("Description");
			dt.Columns.Add("Location");
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
				selectedColumn = "BookID";
				searchValue = idInput.Text.ToString();
				if (!int.TryParse(searchValue, out int intValue)) {
					MessageBox.ErrorQuery("Error", "ID must be a valid number.", "OK");
					return; // Exit the event handler
				}
			} else if (titleCheckBox.Checked) {
				selectedColumn = "Title";
				searchValue = titleInput.Text.ToString();
			} else if (authorCheckBox.Checked) {
				selectedColumn = "Author";
				searchValue = authorInput.Text.ToString();
			} else if (isbnCheckBox.Checked) {
				selectedColumn = "ISBN";
				searchValue = isbnInput.Text.ToString();
			} else if (publicationYearCheckBox.Checked) {
				selectedColumn = "Publication Year";
				searchValue = publicationYearInput.Text.ToString();
				if (!int.TryParse(searchValue, out int intValue)) {
					MessageBox.ErrorQuery("Error", "ID must be a valid number.", "OK");
					return; // Exit the event handler
				}
			} else if (publisherCheckBox.Checked) {
				selectedColumn = "Publisher";
				searchValue = publisherInput.Text.ToString();
			} else if (languageCheckBox.Checked) {
				selectedColumn = "Language";
				searchValue = languageInput.Text.ToString();
			} else if (locationCheckBox.Checked) {
				selectedColumn = "Location";
				searchValue = locationInput.Text.ToString();
			}
			
			// // Perform the search
			string[] searchCriteria = new string[] { selectedColumn, searchValue };
			var (memberInfo, valueFound) = new LibraryFunctions(databasePath).SearchBookInformation(searchCriteria);
			
			dt = new DataTable();
			dt.Columns.Add("BookID");
			dt.Columns.Add("Title");
			dt.Columns.Add("Author");
			dt.Columns.Add("ISBN");
			dt.Columns.Add("Quantity");
			dt.Columns.Add("Available Quantity");
			dt.Columns.Add("Genre");
			dt.Columns.Add("Publication Year");
			dt.Columns.Add("Publisher");
			dt.Columns.Add("Language");
			dt.Columns.Add("Description");
			dt.Columns.Add("Location");
			tableView.Table=dt;
			
			if (valueFound) {
				List<string> valueGroups = new List<string>();
				List<string> currentGroup = new List<string>();

				foreach (var item in memberInfo) {
					currentGroup.Add(item.Value.ToString());
					if (currentGroup.Count == 12) {
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
				titleCheckBox.Checked = false;
				authorCheckBox.Checked = false;
				isbnCheckBox.Checked = false;
				publicationYearCheckBox.Checked = false;
				publisherCheckBox.Checked = false;
				languageCheckBox.Checked = false;
				locationCheckBox.Checked = false;
				titleInput.Text = "";
				authorInput.Text = "";
				isbnInput.Text = "";
				publicationYearInput.Text = "";
				publisherInput.Text = "";
				languageInput.Text = "";
				locationInput.Text = "";
			} else {
				idCheckBox.Checked = false;
			}
		};
		titleInput.TextChanged += (args) => {
			if (!string.IsNullOrWhiteSpace(titleInput.Text.ToString())) {
				idCheckBox.Checked = false;
				titleCheckBox.Checked = true;
				authorCheckBox.Checked = false;
				isbnCheckBox.Checked = false;
				publicationYearCheckBox.Checked = false;
				publisherCheckBox.Checked = false;
				languageCheckBox.Checked = false;
				locationCheckBox.Checked = false;
				idInput.Text = "";
				authorInput.Text = "";
				isbnInput.Text = "";
				publicationYearInput.Text = "";
				publisherInput.Text = "";
				languageInput.Text = "";
				locationInput.Text = "";
			} else {
				titleCheckBox.Checked = false;
			}
		};
		authorInput.TextChanged += (args) => {
			if (!string.IsNullOrWhiteSpace(authorInput.Text.ToString())) {
				idCheckBox.Checked = false;
				titleCheckBox.Checked = false;
				authorCheckBox.Checked = true;
				isbnCheckBox.Checked = false;
				publicationYearCheckBox.Checked = false;
				publisherCheckBox.Checked = false;
				languageCheckBox.Checked = false;
				locationCheckBox.Checked = false;
				idInput.Text = "";
				titleInput.Text = "";
				isbnInput.Text = "";
				publicationYearInput.Text = "";
				publisherInput.Text = "";
				languageInput.Text = "";
				locationInput.Text = "";
			} else {
				authorCheckBox.Checked = false;
			}
		};
		isbnInput.TextChanged += (args) => {
			if (!string.IsNullOrWhiteSpace(isbnInput.Text.ToString())) {
				idCheckBox.Checked = false;
				titleCheckBox.Checked = false;
				authorCheckBox.Checked = false;
				isbnCheckBox.Checked = true;
				publicationYearCheckBox.Checked = false;
				publisherCheckBox.Checked = false;
				languageCheckBox.Checked = false;
				locationCheckBox.Checked = false;
				idInput.Text = "";
				titleInput.Text = "";
				authorInput.Text = "";
				publicationYearInput.Text = "";
				publisherInput.Text = "";
				languageInput.Text = "";
				locationInput.Text = "";
			} else {
				isbnCheckBox.Checked = false;
			}
		};
		publicationYearInput.TextChanged += (args) => {
			if (!string.IsNullOrWhiteSpace(publicationYearInput.Text.ToString())) {
				idCheckBox.Checked = false;
				titleCheckBox.Checked = false;
				authorCheckBox.Checked = false;
				isbnCheckBox.Checked = false;
				publicationYearCheckBox.Checked = true;
				publisherCheckBox.Checked = false;
				languageCheckBox.Checked = false;
				locationCheckBox.Checked = false;
				idInput.Text = "";
				titleInput.Text = "";
				authorInput.Text = "";
				isbnInput.Text = "";
				publisherInput.Text = "";
				languageInput.Text = "";
				locationInput.Text = "";
			} else {
				publicationYearCheckBox.Checked = false;
			}
		};
		publisherInput.TextChanged += (args) => {
			if (!string.IsNullOrWhiteSpace(publisherInput.Text.ToString())) {
				idCheckBox.Checked = false;
				titleCheckBox.Checked = false;
				authorCheckBox.Checked = false;
				isbnCheckBox.Checked = false;
				publicationYearCheckBox.Checked = false;
				publisherCheckBox.Checked = true;
				languageCheckBox.Checked = false;
				locationCheckBox.Checked = false;
				idInput.Text = "";
				titleInput.Text = "";
				authorInput.Text = "";
				isbnInput.Text = "";
				publicationYearInput.Text = "";
				languageInput.Text = "";
				locationInput.Text = "";
			} else {
				publisherCheckBox.Checked = false;
			}
		};
		languageInput.TextChanged += (args) => {
			if (!string.IsNullOrWhiteSpace(languageInput.Text.ToString())) {
				idCheckBox.Checked = false;
				titleCheckBox.Checked = false;
				authorCheckBox.Checked = false;
				isbnCheckBox.Checked = false;
				publicationYearCheckBox.Checked = false;
				publisherCheckBox.Checked = false;
				languageCheckBox.Checked = true;
				locationCheckBox.Checked = false;
				idInput.Text = "";
				titleInput.Text = "";
				authorInput.Text = "";
				isbnInput.Text = "";
				publicationYearInput.Text = "";
				publisherInput.Text = "";
				locationInput.Text = "";
			} else {
				languageCheckBox.Checked = false;
			}
		};
		locationInput.TextChanged += (args) => {
			if (!string.IsNullOrWhiteSpace(locationInput.Text.ToString())) {
				idCheckBox.Checked = false;
				titleCheckBox.Checked = false;
				authorCheckBox.Checked = false;
				isbnCheckBox.Checked = false;
				publicationYearCheckBox.Checked = false;
				publisherCheckBox.Checked = false;
				languageCheckBox.Checked = false;
				locationCheckBox.Checked = true;
				idInput.Text = "";
				titleInput.Text = "";
				authorInput.Text = "";
				isbnInput.Text = "";
				publicationYearInput.Text = "";
				publisherInput.Text = "";
				languageInput.Text = "";
			} else {
				locationCheckBox.Checked = false;
			}
		};
		idInput.FocusFirst();
        searchBookInformationWindow.Add(
            mainTitleLabel,
			idCheckBox, idInput,
			titleCheckBox, titleInput,
			authorCheckBox, authorInput,
			isbnCheckBox, isbnInput,
			publicationYearCheckBox, publicationYearInput,
			publisherCheckBox, publisherInput,
			languageCheckBox, languageInput,
			locationCheckBox, locationInput,
			clearButton, okButton,
			tableView
        );		
        return searchBookInformationWindow;
    }
}
