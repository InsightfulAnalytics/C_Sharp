// Loop through all selected measures in the model
foreach (var measure in Selected.Measures)
{
    // Define the name for the measure
    var newMeasureName = measure.Name + " LY";

    // Construct the DAX expression for the cumulative calculation with comments formatted above the relevant lines
    var daxExpression = $@"

    CALCULATE(
        [{measure.Name}],
        DATEADD(Dates[Date],-1,YEAR)
    )";


    // Create the new measure in the same table as the original measure
    var newMeasure = measure.Table.AddMeasure(newMeasureName, daxExpression);

    // If the measure has a display folder, place the new measure in a subfolder under the parent folder
    if (!string.IsNullOrEmpty(measure.DisplayFolder))
    {
        // Split the folder path by the backslash and get the first part (the parent folder)
        var parentFolder = measure.DisplayFolder.Split('\\')[0];
        // Place the new measure in "ParentFolder\Cumulative"
        newMeasure.DisplayFolder = parentFolder + @"\Date Intel";
    }
    else
    {
        // If no folder exists, just place the new measure in a "Cumulative" folder
        newMeasure.DisplayFolder = "Date Intel";
    }

    // Set the format string for the new measure to "#,##0;(#,##0);0"
    newMeasure.FormatString = "#,##0;(#,##0);0";
}


