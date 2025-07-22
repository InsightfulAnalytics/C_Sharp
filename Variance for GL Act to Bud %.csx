/* 
	Create Variance to Budget % measure when the variance measure is selected
	[GL Var Act to Bud Agency Staff] is selected, creates
		DIVIDE(
		[GL Var Act to Bud Agency Staff],
		[GL Bud Agency Staff],
		BLANK()
	)
*/

// Iterate over all selected variance measures in the model
foreach (var varianceMeasure in Selected.Measures)
{
    // Variance measure name, e.g., "GL Var Act to Bud Depreciation"
    var varianceMeasureName = varianceMeasure.Name;

    // Initialize baseMeasureName
    string baseMeasureName = "";

    // Remove "GL Var Act to Bud " from the measure name to get the base measure name
    if (varianceMeasureName.StartsWith("GL Var Act to Bud "))
    {
        baseMeasureName = varianceMeasureName.Substring(18); // Skip "GL Var Act to Bud " (18 characters)
    }
    else
    {
        Error($"Measure '{varianceMeasureName}' does not start with 'GL Var Act to Bud '.");
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

    // Define the new percentage variance measure name
    var newMeasureName = varianceMeasureName + " %";

    // Define the DAX expression
    var daxExpression = $@"
DIVIDE(
    [{varianceMeasureName}],
    [{budgetMeasureName}],
    BLANK()
)";

    // Create the new measure in the same table as the variance measure
    var newMeasure = varianceMeasure.Table.AddMeasure(newMeasureName, daxExpression);

    // Set the Display Folder (place in "Percentage" subfolder under the variance measure's folder)
    if (!string.IsNullOrEmpty(varianceMeasure.DisplayFolder))
    {
        newMeasure.DisplayFolder = varianceMeasure.DisplayFolder + @"\Percentage";
    }
    else
    {
        newMeasure.DisplayFolder = @"Percentage";
    }

    // Set the format string to percentage
    newMeasure.FormatString = "0.00%";
}
