// For Tabular editor 3 - PBI Measures

/* Creates an AVERAGE measure for every currently selected column and hide the column. */
    foreach(var c in Selected.Columns)
    {
        var newMeasure = c.Table.AddMeasure(
            c.Name + " Average" ,                    // Name
            "Average(" + c.DaxObjectFullName + ")",    // DAX expression
            c.DisplayFolder                        // Display Folder
        );
        
        // Set the format string on the new measure:
        newMeasure.FormatString = "#,##0;(#,##0)";

        // Provide some documentation:
        newMeasure.Description = "Average of column " + c.DaxObjectFullName;

        // Hide the base column:
        c.IsHidden = true;
    }

//
