// Minus the selected measrue to the corresponding LY measrue, for example:
// [GL Act Agency Staff] is selected, creates
// [GL Act Agency Staff] - [GL LY Agency Staff] 

// Iterate over all selected measures in the model
foreach (var measure in Selected.Measures)
{
    // Original measure name, e.g., "GL Act Depreciation" or "GL Depreciation"
    var actualMeasureName = measure.Name;

    // Initialize baseMeasureName
    string baseMeasureName = "";

    // Remove "GL Act " or "GL " from the measure name to get the base measure name
    if (actualMeasureName.StartsWith("GL Act "))
    {
        baseMeasureName = actualMeasureName.Substring(7); // Skip "GL Act " (7 characters)
    }
    else if (actualMeasureName.StartsWith("GL "))
    {
        baseMeasureName = actualMeasureName.Substring(3); // Skip "GL " (3 characters)
    }
    else
    {
        Error($"Measure '{actualMeasureName}' does not start with 'GL ' or 'GL Act '.");
        continue;
    }

    // Construct the LY measure name
    var lyMeasureName = "GL LY " + baseMeasureName;

    // Check if the LY measure exists
    var lyMeasure = Model.AllMeasures.FirstOrDefault(m => m.Name == lyMeasureName);
    if (lyMeasure == null)
    {
        Error($"LY measure '{lyMeasureName}' not found.");
        continue;
    }

    // Construct the new variance measure name
    var newMeasureName = "GL Var Act to LY " + baseMeasureName;

    // Define the DAX expression
    var daxExpression = $"[{actualMeasureName}] - [{lyMeasureName}]";

    // Create the new measure in the same table as the original measure
    var newMeasure = measure.Table.AddMeasure(newMeasureName, daxExpression);

    // Set the Display Folder to "\Finance\Var LY"
    newMeasure.DisplayFolder = @"Finance\Var LY";

    // Set the format string (copy from the original measure)
    newMeasure.FormatString = measure.FormatString;
}
