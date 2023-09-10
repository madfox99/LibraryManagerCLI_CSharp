// DON-CODE
using Terminal.Gui;
using NStack;
using System;
using System.IO;
using System.Runtime.InteropServices;

public class Program {
	
	// Variables
    private static string libraryDirectory = null;
    private static string databasePath = null;
    private static string oldConsoleTitle = null;
	private static LibraryDatabase libraryDatabase = null;
	private static Button[] libraryButtons = null; // Store all library action buttons
	private static Window[] libraryWindows = null; // Store all library action windows
	private static Toplevel top = null; // Declare top frame as a global variable
	
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

    public static void Main(string[] args) {
        
		SetupLibraryPaths(); // initialize the system paths
        MaximizeConsoleWindow(); // Maximize the consolw window
        ChangeConsoleTitle(); // Change the console title
        CreateLibraryDirectory(); // Create the application folder
		SetupDatabase(); // Create the database
        TerminalGUI(); // GUI application

    }
	
    private static void TerminalGUI() {
        Application.Init(); // Initialize the application
        top = Application.Top; // Top frame

        // Creates the top-level menu
        var menu = new MenuBar(new MenuBarItem[] {
            new MenuBarItem ("_File", new MenuItem [] {
                new MenuItem ("_Quit", "", () => { if (Quit ()) top.Running = false; })
            })
        });
        top.Add(menu); // Add menu to the top level
		
		// Left Window
		var leftWindowObj = new LeftWindow();
		
		libraryButtons = leftWindowObj.WindowButtons();

        // Set event handlers for button clicks
        foreach (var button in libraryButtons) {
            button.Clicked += () => OnLibraryButtonClick(button);
        }		

		var leftWindow = leftWindowObj.CreateLeftWindow();		
        var rightWindow = new RightWindow().CreateRightWindow(leftWindow);
        var addBooksWindow = new AddBooksWindow().CreateAddBooksWindow(leftWindow, databasePath);
		var registerMembersWindow = new RegisterMembersWindow().CreateRegisterMembersWindow(leftWindow, databasePath);
		var searchMemberInformationWindow = new SearchMemberInformationWindow().CreateSearchMemberInformationWindow(leftWindow, databasePath);
		var searchBookInformationWindow = new SearchBookInformationWindow().CreateSearchBookInformationWindow(leftWindow, databasePath);
		var displayBookNamesWindow = new DisplayBookNamesWindow().CreateDisplayBookNamesWindow(leftWindow, databasePath);
		var displayMemberNamesWindow = new DisplayMemberNamesWindow().CreateDisplayMemberNamesWindow(leftWindow, databasePath);
		var lendBooksWindow = new LendBooksWindow().CreateLendBooksWindow(leftWindow, databasePath);
		var viewLendingInformationWindow = new ViewLendingInformationWindow().CreateViewLendingInformationWindow(leftWindow, databasePath);
		var displayOverdueBooksWindow = new DisplayOverdueBooksWindow().CreateDisplayOverdueBooksWindow(leftWindow, databasePath);
		var fineCalculationWindow = new FineCalculationWindow().CreateFineCalculationWindow(leftWindow, databasePath);
		var returnBooksWindow = new ReturnBooksWindow().CreateReturnBooksWindow(leftWindow, databasePath);
		var removeBooksWindow = new RemoveBooksWindow().CreateRemoveBooksWindow(leftWindow, databasePath);
		var removeMembersWindow = new RemoveMembersWindow().CreateRemoveMembersWindow(leftWindow, databasePath);
		
		// Create an array to store all library action buttons
        libraryWindows = new Window[] {
            rightWindow,
			addBooksWindow, registerMembersWindow,
			searchMemberInformationWindow, searchBookInformationWindow,
			displayBookNamesWindow, displayMemberNamesWindow,
			lendBooksWindow,
			viewLendingInformationWindow, displayOverdueBooksWindow,
			fineCalculationWindow, returnBooksWindow,
			removeBooksWindow, removeMembersWindow
        };

        // Add the windows to the top-level window
        top.Add(leftWindow);
        top.Add(rightWindow);

        static bool Quit() {
            var n = MessageBox.Query(50, 5, "Quit", "Are you sure you want to quit?", "Yes", "No");
            return n == 0;
        }

        Application.Run();
    }
	
	// Remove windows in right sector and refresh the application
	private static void RemoveRightWindows(){
		foreach (var window in libraryWindows) {
            top.Remove(window);				
        }
		Application.Refresh();
	}
	
    // Event handler for library action button clicks
private static void OnLibraryButtonClick(Button clickedButton) {
    foreach (var button in libraryButtons) {
        if (button == clickedButton) {
            // Set the clicked button's color scheme to errorColorScheme
            button.ColorScheme = Colors.Error;
        } else {
            // Reset the color scheme of other buttons to baseColorScheme
            button.ColorScheme = Colors.Base;
        }
    }

    RemoveRightWindows();

    switch (clickedButton.Text.ToString()) {
        case addButtonText:
            top.Add(libraryWindows[1]);
            Application.Refresh();
            libraryWindows[1].FocusFirst();
            break;
        case registerButtonText:
            top.Add(libraryWindows[2]);
            Application.Refresh();
            libraryWindows[2].FocusFirst();
            break;
        case removeBookButtonText:
            top.Add(libraryWindows[12]);
            Application.Refresh();
            libraryWindows[12].FocusFirst();
            break;
        case removeMemberButtonText:
            top.Add(libraryWindows[13]);
            Application.Refresh();
            libraryWindows[13].FocusFirst();
            break;
        case searchBookButtonText:
			top.Add(libraryWindows[4]);
            Application.Refresh();
            libraryWindows[4].FocusFirst();
            break;
        case searchMemberButtonText:
			top.Add(libraryWindows[3]);
            Application.Refresh();
            libraryWindows[3].FocusFirst();
            break;
        case displayBookNamesButtonText:
            top.Add(libraryWindows[5]);
            Application.Refresh();
            libraryWindows[5].FocusFirst();
            break;
        case displayMemberNamesButtonText:
            top.Add(libraryWindows[6]);
            Application.Refresh();
            libraryWindows[6].FocusFirst();
            break;
        case lendBooksButtonText:
            top.Add(libraryWindows[7]);
            Application.Refresh();
            libraryWindows[7].FocusFirst();
            break;
        case returnBooksButtonText:
            top.Add(libraryWindows[11]);
            Application.Refresh();
            libraryWindows[11].FocusFirst();
            break;
        case viewLendingInfoButtonText:
            top.Add(libraryWindows[8]);
            Application.Refresh();
            libraryWindows[8].FocusFirst();
            break;
        case displayOverdueBooksButtonText:
            top.Add(libraryWindows[9]);
            Application.Refresh();
            libraryWindows[9].FocusFirst();
            break;
        case fineCalculationButtonText:
            top.Add(libraryWindows[10]);
            Application.Refresh();
            libraryWindows[10].FocusFirst();
            break;
        case homeButtonText:
            top.Add(libraryWindows[0]);
            Application.Refresh();
            break;
        default:
            new Dialog("Error");
            break;
    }
}


	private static void SetupDatabase(){
		try{
			libraryDatabase = new LibraryDatabase(databasePath);
		} catch (Exception ex) {
            HandleException(ex, "Creating the Database");
        }
	}

    private static void SetupLibraryPaths() {
        libraryDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LibraryManagementSystem");
        databasePath = Path.Combine(libraryDirectory, "mylibrary.db");
    }

    private static void MaximizeConsoleWindow() {
        try {
            // Maximize console window (Windows only)
            var hwnd = GetConsoleWindow();
            ShowWindow(hwnd, MAXIMIZE);
        } catch (Exception ex) {
            HandleException(ex, "Maximizing Console Window");
        }
    }

    private static void ChangeConsoleTitle() {
        try {
            // Change console title (Windows only)
            oldConsoleTitle = Console.Title;
            Console.Title = "Library Management System | v0.0.1";
        } catch (Exception ex) {
            HandleException(ex, "Changing Console Title");
        }
    }

    private static void CreateLibraryDirectory() {
        try {
            // Create the LibraryManagementSystem directory if it doesn't exist (Windows only)
            if (!Directory.Exists(libraryDirectory)) {
                Directory.CreateDirectory(libraryDirectory);
            }
        } catch (Exception ex) {
            HandleException(ex, "Creating Library Directory");
        }
    }

    private static void HandleException(Exception ex, string operationName) {
        // Handle exceptions here
        // Log or report the error along with the operationName
        Console.WriteLine($"Error during {operationName}: {ex.Message}");
    }

    // P/Invoke declarations for Windows API functions
    [DllImport("kernel32.dll", ExactSpelling = true)]
    private static extern IntPtr GetConsoleWindow();
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    private const int MAXIMIZE = 3;
	
}
