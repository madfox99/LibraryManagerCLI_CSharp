// DON-CODE
using Terminal.Gui;

public class AddBooksWindow {
    public Window CreateAddBooksWindow(Window leftWindow, String databasePath){
        var addBooksWindow = new Window() {
            X = Pos.Right(leftWindow), // Position it to the right of the leftWindow
            Y = 1, // Leave one row for the top-level menu
            Width = Dim.Fill(), // Fill the remaining space
            Height = Dim.Fill()
        };
		var mainTitleLabel = new Label("  ___         _          _   \n | _ )___ ___| |_____  _| |_ \n | _ / _ / _ | / (_-< |_   _|\n |___\\___\\___|_\\_/__/   |_|  ") {
			X = 12,
			Y = 1,
			Width = Dim.Fill(),
			Height = 1,
			LayoutStyle = LayoutStyle.Computed, // Use computed layout
		};
		var titleLabel = new Label("Title") {
			X = 2,
			Y = Pos.Bottom(mainTitleLabel) + 2,
			Width = 17
		};		
		var titleInput = new TextField ("") {
			X = Pos.Right(titleLabel) + 1,
			Y = Pos.Top(titleLabel),
			Width = 30,
			Height = 1
		};
		var authorLabel = new Label("Author") {
			X = Pos.Left(titleLabel),
			Y = Pos.Bottom(titleLabel) + 1,
			Width = 17
		};
		var authorInput = new TextField ("") {
			X = Pos.Right(authorLabel) + 1,
			Y = Pos.Top(authorLabel),
			Width = 30,
			Height = 1
		};
		var isbnLabel = new Label("ISBN") {
			X = Pos.Left(titleLabel),
			Y = Pos.Bottom(authorLabel) + 1,
			Width = 17
		};
		var isbnInput = new TextField ("") {
			X = Pos.Right(isbnLabel) + 1,
			Y = Pos.Top(isbnLabel),
			Width = 30,
			Height = 1
		};
		var quantityLabel = new Label("Quantity") {
			X = Pos.Left(titleLabel),
			Y = Pos.Bottom(isbnLabel) + 1,
			Width = 17
		};
		var quantityInput = new TextField ("") {
			X = Pos.Right(quantityLabel) + 1,
			Y = Pos.Top(quantityLabel),
			Width = 30,
			Height = 1
		};
		var genreLabel = new Label("Genre") {
			X = Pos.Left(titleLabel),
			Y = Pos.Bottom(quantityLabel) + 1,
			Width = 17
		};
		var genreInput = new TextField ("") {
			X = Pos.Right(genreLabel) + 1,
			Y = Pos.Top(genreLabel),
			Width = 30,
			Height = 1
		};
		var publicationDateLabel = new Label("Publication Year") {
			X = Pos.Left(titleLabel),
			Y = Pos.Bottom(genreLabel) + 1,
			Width = 17
		};
		var publicationDateInput = new TextField ("") {
			X = Pos.Right(publicationDateLabel) + 1,
			Y = Pos.Top(publicationDateLabel),
			Width = 30,
			Height = 1
		};
		var publisherLabel = new Label("Publisher") {
			X = Pos.Left(titleLabel),
			Y = Pos.Bottom(publicationDateLabel) + 1,
			Width = 17
		};
		var publisherInput = new TextField ("") {
			X = Pos.Right(publisherLabel) + 1,
			Y = Pos.Top(publisherLabel),
			Width = 30,
			Height = 1
		};
		var languageLabel = new Label("Language") {
			X = Pos.Left(titleLabel),
			Y = Pos.Bottom(publisherLabel) + 1,
			Width = 17
		};
		var languageInput = new TextField ("") {
			X = Pos.Right(languageLabel) + 1,
			Y = Pos.Top(languageLabel),
			Width = 30,
			Height = 1
		};
		var descriptionLabel = new Label("Description") {
			X = Pos.Left(titleLabel),
			Y = Pos.Bottom(languageLabel) + 1,
			Width = 17
		};
		var descriptionInput = new TextField ("") {
			X = Pos.Right(descriptionLabel) + 1,
			Y = Pos.Top(descriptionLabel),
			Width = 30,
			Height = 1
		};
		var locationLabel = new Label("Location") {
			X = Pos.Left(titleLabel),
			Y = Pos.Bottom(descriptionLabel) + 1,
			Width = 17
		};
		var locationInput = new TextField ("") {
			X = Pos.Right(locationLabel) + 1,
			Y = Pos.Top(locationLabel),
			Width = 30,
			Height = 1
		};
		var clearButton = new Button("Clear") {
			X = Pos.Left(titleLabel) + 29,
			Y = Pos.Bottom(locationLabel) + 3,			
		};
		clearButton.Clicked += () => {
            // Clear the text fields when the Clear button is clicked
            titleInput.Text = "";
            authorInput.Text = "";
            isbnInput.Text = "";
            quantityInput.Text = "";
            genreInput.Text = "";
            publicationDateInput.Text = "";
            publisherInput.Text = "";
            languageInput.Text = "";
            descriptionInput.Text = "";
            locationInput.Text = "";
        };
		var okButton = new Button("  Ok ") {
			X = Pos.Right(clearButton) + 1,
			Y = Pos.Bottom(locationLabel) + 3,
		};
		okButton.Clicked += () => {
			string title = titleInput.Text.ToString();
			string author = authorInput.Text.ToString();
			string isbn = isbnInput.Text.ToString();
			string quantityText = quantityInput.Text.ToString();
			string genre = genreInput.Text.ToString();
			string publicationYear = publicationDateInput.Text.ToString();
			string publisher = publisherInput.Text.ToString();
			string language = languageInput.Text.ToString();
			string description = descriptionInput.Text.ToString();
			string location = locationInput.Text.ToString();
			
			// Check if any of the required fields are empty
			if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author) || string.IsNullOrWhiteSpace(isbn) || string.IsNullOrWhiteSpace(quantityText) || string.IsNullOrWhiteSpace(publicationYear) || string.IsNullOrWhiteSpace(language) || string.IsNullOrWhiteSpace(location)) {
				MessageBox.ErrorQuery("Error", "All required fields must be filled in.", "OK");
				return; // Exit the event handler
			}

			// Check if the "Quantity" field is a valid integer
			if (!int.TryParse(quantityText, out int quantity)) {
				MessageBox.ErrorQuery("Error", "Quantity must be a valid number.", "OK");
				return; // Exit the event handler
			}
			
			// Call the AddBook function and check the result
			var (success, message) = new LibraryFunctions(databasePath).AddBook(title, author, isbn, quantity, genre, publicationYear, publisher, language, description, location);

			if (success) {
				MessageBox.Query("Success", message, "OK");
			} else {
				MessageBox.ErrorQuery("Error", message, "OK");
			}
			
			// Clear the text fields when the Clear button is clicked
			titleInput.Text = "";
			authorInput.Text = "";
			isbnInput.Text = "";
			quantityInput.Text = "";
			genreInput.Text = "";
			publicationDateInput.Text = "";
			publisherInput.Text = "";
			languageInput.Text = "";
			descriptionInput.Text = "";
			locationInput.Text = "";
		};
		titleInput.FocusFirst();
		addBooksWindow.Add(
			mainTitleLabel,
			titleLabel, titleInput,
			authorLabel, authorInput,
			isbnLabel, isbnInput,
			quantityLabel, quantityInput,
			quantityLabel, quantityInput,
			genreLabel, genreInput,
			publicationDateLabel, publicationDateInput,
			publisherLabel, publisherInput,
			languageLabel, languageInput,
			descriptionLabel, descriptionInput,
			locationLabel, locationInput,
			clearButton, okButton
		);        
        return addBooksWindow;
    }    
}