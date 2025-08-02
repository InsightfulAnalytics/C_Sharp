// Loop through each measure the user has selected in the model.
foreach (var measure in Selected.Measures)
{
    // Define the name for the new measure by adding "Colour " as a prefix.
    var newMeasureName = "Colour " + measure.Name;

    // Construct the DAX expression.
    // The [{measure.Name}] placeholder is dynamically replaced with the
    // name of the currently selected measure in the loop.
    // Note: The double-quotes within the DAX (e.g., for "black")
    // are escaped by doubling them up ("").
    var daxExpression = $@"
VAR __variance = [{measure.Name}]
VAR __if =
    SWITCH(
        TRUE(),
        __variance > 0, [Colour 3Positive],
        __variance < 0, [Colour 2Negative],
        ""black""
    )
VAR __format = FORMAT( __if, ""0"" )
RETURN
    __format
";

    // Create the new measure in the same table as the original measure.
    var newMeasure = measure.Table.AddMeasure(newMeasureName, daxExpression);

    // Assign the new measure to the specified display folder.
    newMeasure.DisplayFolder = @"Parameter\Visuals";
}

// Inform the user that the script has finished.
Info("Colour measures have been created successfully.");
