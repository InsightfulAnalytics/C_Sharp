// Iterate over all selected measures in the model
foreach (var measure in Selected.Measures)
{
    // Example measure name: "Act Drugs For Episodes"
    var measureName = measure.Name;

    // Ensure the measure starts with "Act "
    if (!measureName.StartsWith("Act "))
    {
        Error($"Measure '{measureName}' does not start with 'Act '. Skipping...");
        continue;
    }

    // Extract the base name by removing "Act "
    var baseName = measureName.Substring(4);

    // The corresponding Budget measure
    var budMeasureName = "Bud " + baseName;
    var budMeasure = Model.AllMeasures.FirstOrDefault(m => m.Name == budMeasureName);

    if (budMeasure == null)
    {
        Error($"Budget measure '{budMeasureName}' not found. Skipping...");
        continue;
    }

    // Capture the display folder from the selected measure
    string displayFolder = measure.DisplayFolder ?? string.Empty;

    // 1) Create the Variance measure
    var varMeasureName = "Var Act To Bud " + baseName;
    var varMeasureExpression = $"[{budMeasureName}] - [{measureName}]";

    var varMeasure = measure.Table.AddMeasure(varMeasureName, varMeasureExpression);
    varMeasure.DisplayFolder = displayFolder;
    varMeasure.FormatString = measure.FormatString;

    // 2) Create the Variance % measure
    var varPctMeasureName = varMeasureName + " %";
    var varPctExpression = 
$@"DIVIDE(
    [{varMeasureName}],
    [{budMeasureName}]
)";

    var varPctMeasure = measure.Table.AddMeasure(varPctMeasureName, varPctExpression);
    varPctMeasure.DisplayFolder = displayFolder;
    varPctMeasure.FormatString = "0.00%";
}
