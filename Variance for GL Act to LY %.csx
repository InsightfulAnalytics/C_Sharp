/* 
	Create Variance to LY % measure when the variance measure is selected
	[GL Var Act to LY Agency Staff] is selected, creates
		DIVIDE(
		[GL Var Act to LY Agency Staff],
		[GL LY Agency Staff],
		BLANK()
	)

*/

// Iterate over all selected variance measures in the model
foreach (var varianceMeasure in Selected.Measures)
{
    // Variance measure name, e.g., "GL Var Act to LY Depreciation"
    var varianceMeasureName = varianceMeasure.Name;

    // Initialize baseMeasureName
    string baseMeasureName = "";

    // Remove "GL Var Act to LY " from the measure name to get the base measure name
    if (varianceMeasureName.StartsWith("GL Var Act to LY "))
    {
        baseMeasureName = varianceMeasureName.Substring(17); // Skip "GL Var Act to LY " (17 characters)
    }
    else
    {
        Error($"Measure '{varianceMeasureName}' does not start with 'GL Var Act to LY '.");
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

    // Define the new percentage variance measure name
    var newMeasureName = varianceMeasureName + " %";

    // Define the DAX expression
    var daxExpression = $@"
DIVIDE(
    [{varianceMeasureName}],
    [{lyMeasureName}],
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
