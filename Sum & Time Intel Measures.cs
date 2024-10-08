// For Tabular editor 3 - PBI Measures


/* Then run time intel script on the created measures */

    var dateColumn = "'DimDate'[Date]";

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
            "CALCULATE(" + m.DaxObjectName + ", DATEADD(" + dateColumn + ", -1 , YEAR))",     // DAX expression
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
            "DIVIDE([" + m.Name + " YoY], [" + m.Name + " PY], 0)",    // DAX expression
            m.DisplayFolder                                         // Display Folder
        ).FormatString = "0.0 %";                                   // Set format string as percentage
        
        // Quarter-to-date:
        m.Table.AddMeasure(
            m.Name + " QTD",                                            // Name
            "TOTALQTD(" + m.DaxObjectName + ", " + dateColumn + ")",    // DAX expression
            m.DisplayFolder                                             // Display Folder
        );
        
        // Quarter-to-date PY:
        m.Table.AddMeasure(
            m.Name + " QoQ",                                       // Name
            "CALCULATE(" +  m.Name + " QTD" + ", DATEADD(" + dateColumn + ", -1 , YEAR))",     // DAX expression
            m.DisplayFolder                                        // Display Folder
        );
        
        // QoQ %:
        m.Table.AddMeasure(
            m.Name + " QoQ%",
            "DIVIDE([" + m.Name + " QoQ], [" + m.Name + " PY], 0)",
            m.DisplayFolder
        ).FormatString = "0.0 %";


        // Month-to-date:
        m.Table.AddMeasure(
            m.Name + " MTD",                                       // Name
            "TOTALMTD(" + m.DaxObjectName + ", " + dateColumn + ")",     // DAX expression
            m.DisplayFolder                                        // Display Folder
        );
    }

//