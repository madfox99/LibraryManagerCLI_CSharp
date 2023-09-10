// DON-CODE
using Terminal.Gui;
using NStack;
using System;
using System.Runtime.InteropServices;
using System.Data;
using System.Collections.Generic;

public class DisplayBookNamesWindow {	
	DataTable dt;	
    public Window CreateDisplayBookNamesWindow(Window leftWindow, String databasePath){
        var createDisplayBookNamesWindow = new Window() {
            X = Pos.Right(leftWindow), // Position it to the right of the leftWindow
            Y = 1, // Leave one row for the top-level menu
            Width = Dim.Fill(), // Fill the remaining space
            Height = Dim.Fill()
        };
        var mainTitleLabel = new Label("  ___         _           \n | _ )___ ___| |_____ _/\\_\n | _ / _ / _ | / (_-< >  <\n |___\\___\\___|_\\_/__/  \\/ ") {
            X = 4,
            Y = 1,
            Width = Dim.Fill(),
            Height = 1,
            LayoutStyle = LayoutStyle.Computed, // Use computed layout
        };
		var okButton = new Button("Refresh") {
			X = Pos.Left(mainTitleLabel),
			Y = Pos.Bottom(mainTitleLabel) + 2,
		};
        var tableView = new TableView() {
			X = Pos.Left(mainTitleLabel),
            Y = Pos.Bottom(okButton) + 1,
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
		List<string[]> bookArrays = new LibraryFunctions(databasePath).GetAllBooksAsArrays();
		foreach (string[] bookData in bookArrays) {
			string combinedData = string.Join("¥", bookData);
			dt.Rows.Add(combinedData.Split('¥'));
		}
		
		okButton.Clicked += () => {			
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
			List<string[]> bookArrays = new LibraryFunctions(databasePath).GetAllBooksAsArrays();
			foreach (string[] bookData in bookArrays) {
				string combinedData = string.Join("¥", bookData);
				dt.Rows.Add(combinedData.Split('¥'));
			}
		};
        createDisplayBookNamesWindow.Add(
            mainTitleLabel,
			okButton,
			tableView
        );		
        return createDisplayBookNamesWindow;
    }
}
