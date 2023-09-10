// DON-CODE
using Terminal.Gui;

public class RightWindow {
	public Window CreateRightWindow(Window leftWindow){
		// Create a main window for the right section
        var rightWindow = new Window() {
            X = Pos.Right(leftWindow), // Position it to the right of the leftWindow
            Y = 1, // Leave one row for the top-level menu
            Width = Dim.Fill(), // Fill the remaining space
            Height = Dim.Fill()
        };
		var panelLabelRight = new Label("Library Management System\n\n        ,___,\n        (O,O)\n        /)_)\n       ═╦''╦═") {
			X = 3, // Set X to 0 to center the label horizontally
			Y = 2,
			Width = Dim.Fill(),
			Height = 1,
			LayoutStyle = LayoutStyle.Computed, // Use computed layout
		};
		rightWindow.Add(panelLabelRight);
		return rightWindow;
	}
}