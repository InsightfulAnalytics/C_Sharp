// For Tabular editor 3 - PBI Measures

/* Creates a SUM measure for every currently selected column and hide the column. */
    foreach(var c in Selected.Columns)
    {
        var newMeasure = c.Table.AddMeasure(
            c.Name + " Total" ,                    // Name
            "SUM(" + c.DaxObjectFullName + ")",    // DAX expression
            c.DisplayFolder                        // Display Folder
        );
        
        // Set the format string on the new measure:
        newMeasure.FormatString = "#,##0;(#,##0)";

        // Provide some documentation:
        newMeasure.Description = "sum of column " + c.DaxObjectFullName;

        // Hide the base column:
        c.IsHidden = true;
    }

//
