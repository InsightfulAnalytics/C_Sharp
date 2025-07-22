
// Loop through all selected measures in the model
foreach (var measure in Selected.Measures)
{
    // Define the name for the measure
    var newMeasureName = measure.Name + " LY";

    // Construct the DAX expression for the calculation
    var daxExpression = $@"
    CALCULATE(
        [{measure.Name}],
        DATEADD(DimDate[Date], -1, YEAR)
    )";

    // Create the new measure in the same table as the original measure
    var newMeasure = measure.Table.AddMeasure(newMeasureName, daxExpression);

    // Place the new measure in the "Finance\LY" folder
    newMeasure.DisplayFolder = @"Finance\LY";

    // Set the format string for the new measure
    newMeasure.FormatString = "#,##0;(#,##0);0";
}
