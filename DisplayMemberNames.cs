// DON-CODE
using Terminal.Gui;
using NStack;
using System;
using System.Runtime.InteropServices;
using System.Data;
using System.Collections.Generic;

public class DisplayMemberNamesWindow {	
	DataTable dt;	
    public Window CreateDisplayMemberNamesWindow(Window leftWindow, String databasePath){
        var createDisplayMemberNamesWindow = new Window() {
            X = Pos.Right(leftWindow), // Position it to the right of the leftWindow
            Y = 1, // Leave one row for the top-level menu
            Width = Dim.Fill(), // Fill the remaining space
            Height = Dim.Fill()
        };
        var mainTitleLabel = new Label("  __  __           _                    \n |  \\/  |___ _ __ | |__ ___ _ _ ___ _/\\_\n | |\\/| / -_| '  \\| '_ / -_| '_(_-< >  <\n |_|  |_\\___|_|_|_|_.__\\___|_| /__/  \\/") {
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
			Width = 80,
			Height = 17,
		};		
		dt = new DataTable();
		dt.Columns.Add("MemberID");
		dt.Columns.Add("First Name");
		dt.Columns.Add("Last Name");
		dt.Columns.Add("Email");
		dt.Columns.Add("Phone No.");
		tableView.Table=dt;
		List<string[]> memberArrays = new LibraryFunctions(databasePath).GetAllMembersAsArrays();
		foreach (string[] memberData in memberArrays) {
			string combinedData = string.Join("¥", memberData);
			dt.Rows.Add(combinedData.Split('¥'));
		}
		
		okButton.Clicked += () => {			
			dt = new DataTable();
			dt.Columns.Add("MemberID");
			dt.Columns.Add("First Name");
			dt.Columns.Add("Last Name");
			dt.Columns.Add("Email");
			dt.Columns.Add("Phone No.");
			tableView.Table=dt;
			List<string[]> memberArrays = new LibraryFunctions(databasePath).GetAllMembersAsArrays();
			foreach (string[] memberData in memberArrays) {
				string combinedData = string.Join("¥", memberData);
				dt.Rows.Add(combinedData.Split('¥'));
			}
		};
        createDisplayMemberNamesWindow.Add(
            mainTitleLabel,
			okButton,
			tableView
        );		
        return createDisplayMemberNamesWindow;
    }
}
