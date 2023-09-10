// DON-CODE
using Terminal.Gui;
using NStack;
using System;
using System.Runtime.InteropServices;
using System.Data;
using System.Collections.Generic;

public class DisplayOverdueBooksWindow {	
	DataTable dt;	
    public Window CreateDisplayOverdueBooksWindow(Window leftWindow, String databasePath){
        var createDisplayOverdueBooksWindow = new Window() {
            X = Pos.Right(leftWindow), // Position it to the right of the leftWindow
            Y = 1, // Leave one row for the top-level menu
            Width = Dim.Fill(), // Fill the remaining space
            Height = Dim.Fill()
        };
        var mainTitleLabel = new Label("   ___                 _            ___   \n  / _ \\__ _____ _ _ __| |_  _ ___  | _ )  \n | (_) \\ V / -_| '_/ _` | || / -_) | _ \\_ \n  \\___/ \\_/\\___|_| \\__,_|\\_,_\\___| |___(_)") {
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
		dt.Columns.Add("Transaction ID");
		dt.Columns.Add("Member ID");
		dt.Columns.Add("Book Title");
		dt.Columns.Add("Member Name");
		dt.Columns.Add("Due Date");
		tableView.Table=dt;
		List<string[]> lendingInfo  = new LibraryFunctions(databasePath).DisplayOverdueBooks();
		foreach (string[] bookData in lendingInfo) {
			string combinedData = string.Join("¥", bookData);
			dt.Rows.Add(combinedData.Split('¥'));
		}		
		okButton.Clicked += () => {			
			dt = new DataTable();
			dt.Columns.Add("Transaction ID");
			dt.Columns.Add("Member ID");
			dt.Columns.Add("Book Title");
			dt.Columns.Add("Member Name");
			dt.Columns.Add("Due Date");
			tableView.Table=dt;
			List<string[]> lendingInfo  = new LibraryFunctions(databasePath).DisplayOverdueBooks();
			foreach (string[] bookData in lendingInfo) {
				string combinedData = string.Join("¥", bookData);
				dt.Rows.Add(combinedData.Split('¥'));
			}
		};
        createDisplayOverdueBooksWindow.Add(
            mainTitleLabel,
			okButton,
			tableView
        );		
        return createDisplayOverdueBooksWindow;
    }
}
