// Loop through all selected measures in the model
foreach (var measure in Selected.Measures)
{
    // Define the name for the new cumulative measure by prefixing "Cm" to the original measure name
    var newMeasureName = "Cm " + measure.Name;

    // Construct the DAX expression for the cumulative calculation with comments formatted above the relevant lines
    var daxExpression = $@"
    VAR __tbl = 
        FILTER( 
            // Using ALLSELECTED to respect current filter context
            ALLSELECTED( 'Dates' ), 
            // Filter dates less than or equal to the current max date
            'Dates'[Date] <= MAX( 'Dates'[Date]) 
        )
    VAR __sumx = 
        // Calculate the sum of the selected measure over the filtered dates
        SUMX(
            __tbl,
            [{measure.Name}] 
        )
    RETURN
        // Return the cumulative sum
        __sumx"; 

    // Create the new measure in the same table as the original measure
    var newMeasure = measure.Table.AddMeasure(newMeasureName, daxExpression);

    // If the measure has a display folder, place the new measure in a "Cumulative" subfolder under the parent folder
    if (!string.IsNullOrEmpty(measure.DisplayFolder))
    {
        // Split the folder path by the backslash and get the first part (the parent folder)
        var parentFolder = measure.DisplayFolder.Split('\\')[0];
        // Place the new measure in "ParentFolder\Cumulative"
        newMeasure.DisplayFolder = parentFolder + @"\Cumulative";
    }
    else
    {
        // If no folder exists, just place the new measure in a "Cumulative" folder
        newMeasure.DisplayFolder = "Cumulative";
    }

    // Set the format string for the new measure to "#,##0;(#,##0);0"
    newMeasure.FormatString = "#,##0;(#,##0);0";
}


