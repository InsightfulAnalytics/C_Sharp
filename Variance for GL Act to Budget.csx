// Minus the selected measrue to the corresponding Budget measrue, for example:
// [GL Act Agency Staff] is selected, creates
// [GL Act Agency Staff] - [GL Bud Agency Staff] 

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

    // Construct the Budget measure name
    var budgetMeasureName = "GL Bud " + baseMeasureName;

    // Check if the Budget measure exists
    var budgetMeasure = Model.AllMeasures.FirstOrDefault(m => m.Name == budgetMeasureName);
    if (budgetMeasure == null)
    {
        Error($"Budget measure '{budgetMeasureName}' not found.");
        continue;
    }

    // Construct the new variance measure name
    var newMeasureName = "GL Var Act to Bud " + baseMeasureName;

    // Define the DAX expression
    var daxExpression = $"[{actualMeasureName}] - [{budgetMeasureName}]";

    // Create the new measure in the same table as the original measure
    var newMeasure = measure.Table.AddMeasure(newMeasureName, daxExpression);

    // Set the Display Folder to "\Finance\Var Bud"
    newMeasure.DisplayFolder = @"Finance\Var Bud";

    // Set the format string (copy from the original measure)
    newMeasure.FormatString = measure.FormatString;
}
