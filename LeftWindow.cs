// DON-CODE
using Terminal.Gui;

public class LeftWindow {
	// Button text
	private const String addButtonText = "         Add Books       ";
	private const String registerButtonText = "     Register Members    ";
	private const String removeBookButtonText = "       Remove Books      ";
	private const String removeMemberButtonText = "      Remove Members     ";
	private const String searchBookButtonText = " Search Book Information ";
	private const String searchMemberButtonText = "Search Member Information";
	private const String displayBookNamesButtonText = "   Display Book Names    ";
	private const String displayMemberNamesButtonText = "   Display Member Names  ";
	private const String lendBooksButtonText = "        Lend Books       ";
	private const String returnBooksButtonText = "       Return Books      ";
	private const String viewLendingInfoButtonText = " View Lending Information";
	private const String displayOverdueBooksButtonText = "  Display Overdue Books  ";
	private const String fineCalculationButtonText = "     Fine Calculation    ";
	private const String homeButtonText = "Home";
	private const String logoutButtonText = "Logout";
	// Buttons
	private Button addButton = new Button("");
	private Button registerButton = new Button("");
	private Button removeBookButton = new Button("");
	private Button removeMemberButton = new Button("");
	private Button searchBookButton = new Button("");
	private Button searchMemberButton = new Button("");
	private Button displayBookNamesButton = new Button("");
	private Button displayMemberNamesButton = new Button("");
	private Button lendBooksButton = new Button("");
	private Button returnBooksButton = new Button("");
	private Button viewLendingInfoButton = new Button("");
	private Button displayOverdueBooksButton = new Button("");
	private Button fineCalculationButton = new Button("");
	private Button homeButton = new Button("");
	private Button logoutButton = new Button("");
	
	
	public LeftWindow(){
		ButtonAssign();
	}
	
    public Button[] WindowButtons() {
        var buttons = new List<Button> {
            addButton,
            registerButton,
            removeBookButton,
            removeMemberButton,
            searchBookButton,
            searchMemberButton,
            displayBookNamesButton,
            displayMemberNamesButton,
            lendBooksButton,
            returnBooksButton,
            viewLendingInfoButton,
            displayOverdueBooksButton,
            fineCalculationButton,
            homeButton,
            logoutButton
        };

        return buttons.ToArray();
    }
	
	public void ButtonAssign(){
		addButton = new Button(addButtonText) {
			X = 2,
			Y = 1,
		};
		registerButton = new Button(registerButtonText) {
			X = Pos.Left(addButton),
			Y = Pos.Bottom(addButton) + 1,
		};
        removeBookButton = new Button(removeBookButtonText) {
			X = Pos.Left(registerButton),
			Y = Pos.Bottom(registerButton) + 1,
		};
        removeMemberButton = new Button(removeMemberButtonText) {
			X = Pos.Left(removeBookButton),
			Y = Pos.Bottom(removeBookButton) + 1,
		};
        searchBookButton = new Button(searchBookButtonText) {
			X = Pos.Left(removeMemberButton),
			Y = Pos.Bottom(removeMemberButton) + 1,
		};
        searchMemberButton = new Button(searchMemberButtonText) {
			X = Pos.Left(searchBookButton),
			Y = Pos.Bottom(searchBookButton) + 1,
		};
        displayBookNamesButton = new Button(displayBookNamesButtonText) {
			X = Pos.Left(searchMemberButton),
			Y = Pos.Bottom(searchMemberButton) + 1,
		};
        displayMemberNamesButton = new Button(displayMemberNamesButtonText) {
			X = Pos.Left(displayBookNamesButton),
			Y = Pos.Bottom(displayBookNamesButton) + 1,
		};
        lendBooksButton = new Button(lendBooksButtonText) {
			X = Pos.Left(displayMemberNamesButton),
			Y = Pos.Bottom(displayMemberNamesButton) + 1,
		};
        returnBooksButton = new Button(returnBooksButtonText) {
			X = Pos.Left(lendBooksButton),
			Y = Pos.Bottom(lendBooksButton) + 1,
		};
        viewLendingInfoButton = new Button(viewLendingInfoButtonText) {
			X = Pos.Left(returnBooksButton),
			Y = Pos.Bottom(returnBooksButton) + 1,
		};
        displayOverdueBooksButton = new Button(displayOverdueBooksButtonText) {
			X = Pos.Left(viewLendingInfoButton),
			Y = Pos.Bottom(viewLendingInfoButton) + 1,
		};
        fineCalculationButton = new Button(fineCalculationButtonText) {
			X = Pos.Left(displayOverdueBooksButton),
			Y = Pos.Bottom(displayOverdueBooksButton) + 1,
		};
		homeButton = new Button(homeButtonText) {
			X = Pos.Left(fineCalculationButton),
			Y = Pos.Bottom(fineCalculationButton) + 3,
		};
	}

	
	public Window CreateLeftWindow(){
		// Create a window for the left section
        var leftWindow = new Window("Library Actions") {
            X = 0,
            Y = 1, // Leave one row for the top-level menu
            Width = 35, // Set the width to 35
            Height = Dim.Fill()
        };

        // Add buttons for library functions to the leftWindow
        
		var panelLabelLeft = new Label ("┌─────────────────────────────┐\n|                             |\n├─────────────────────────────┤\n|                             |\n├─────────────────────────────┤\n|                             |\n├─────────────────────────────┤\n|                             |\n├─────────────────────────────┤\n|                             |\n├─────────────────────────────┤\n|                             |\n├─────────────────────────────┤\n|                             |\n├─────────────────────────────┤\n|                             |\n├─────────────────────────────┤\n|                             |\n├─────────────────────────────┤\n|                             |\n├─────────────────────────────┤\n|                             |\n├─────────────────────────────┤\n|                             |\n├─────────────────────────────┤\n|                             |\n└─────────────────────────────┘\n\n┌────────┐\n|        |\n└────────┘") {
			X = 1,
			Y = 0,
			Width = Dim.Fill (),
			Height = 1
		};		

        // Add the buttons to the leftWindow
        leftWindow.Add(
            panelLabelLeft, addButton, registerButton, removeBookButton, removeMemberButton,
            searchBookButton, searchMemberButton, displayBookNamesButton,
            displayMemberNamesButton, lendBooksButton, returnBooksButton,
            viewLendingInfoButton, displayOverdueBooksButton, fineCalculationButton,
			homeButton
        );
		homeButton.FocusFirst();		
		return leftWindow;
	}
}