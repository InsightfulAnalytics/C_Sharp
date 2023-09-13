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

/* Then run time intel script on the created measures */

    var dateColumn = "'Dates'[Date]";

    // Creates time intelligence measures for every selected measure:
    foreach(var m in Selected.Measures) {
        // Year-to-date:
        m.Table.AddMeasure(
            m.Name + " YTD",                                       // Name
            "TOTALYTD(" + m.DaxObjectName + ", " + dateColumn + ")",     // DAX expression
            m.DisplayFolder                                        // Display Folder
        );
        
        // Previous year:
        m.Table.AddMeasure(
            m.Name + " PY",                                       // Name
            "CALCULATE(" + m.DaxObjectName + ", SAMEPERIODLASTYEAR(" + dateColumn + "))",     // DAX expression
            m.DisplayFolder                                        // Display Folder
        );    
        
        // Year-over-year
        m.Table.AddMeasure(
            m.Name + " YoY",                                       // Name
            m.DaxObjectName + " - [" + m.Name + " PY]",            // DAX expression
            m.DisplayFolder                                        // Display Folder
        );
        
        // Year-over-year %:
        m.Table.AddMeasure(
            m.Name + " YoY%",                                       // Name
            "DIVIDE([" + m.Name + " YoY], [" + m.Name + " PY])",    // DAX expression
            m.DisplayFolder                                         // Display Folder
        ).FormatString = "0.0 %";                                   // Set format string as percentage
        
        // Quarter-to-date:
        m.Table.AddMeasure(
            m.Name + " QTD",                                            // Name
            "TOTALQTD(" + m.DaxObjectName + ", " + dateColumn + ")",    // DAX expression
            m.DisplayFolder                                             // Display Folder
        );
        
        // Month-to-date:
        m.Table.AddMeasure(
            m.Name + " MTD",                                       // Name
            "TOTALMTD(" + m.DaxObjectName + ", " + dateColumn + ")",     // DAX expression
            m.DisplayFolder                                        // Display Folder
        );
    }

//