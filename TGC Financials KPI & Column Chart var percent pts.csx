// Title: Create Expense Percentage Comparison Measures
// Description: Creates a set of standard variance and KPI measures for a selected expense-based percentage 'Act' measure (where lower values are better).
// To Use: Select one or more measures containing " Act " in the name and run this script.

// Check if any measure is selected
if (Selected.Measures.Count == 0)
{
    Error("Please select at least one 'Actual' measure to proceed.");
    return;
}

// Define the folder where the new measures will be created
string displayFolder = "Finance\\New";

// Process each selected measure
foreach (var measure in Selected.Measures)
{
    string selectedMeasureName = measure.Name;

    // Verify that the selected measure is an 'Actual' measure, otherwise skip it
    if (!selectedMeasureName.Contains(" Act "))
    {
        Info("Skipping measure '" + selectedMeasureName + "' as its name does not contain ' Act '.");
        continue;
    }
    
    // Identify the table where the new measures will be added
    var table = measure.Table;

    // --- Parse Measure Names ---
    // Get the DAX object name for use in formulas (e.g., '[GL Act Total Ops Revenue]')
    string actMeasureDaxName = measure.DaxObjectName; 
    // Dynamically create the names for the Budget and Last Year comparison measures
    string budMeasureDaxName = actMeasureDaxName.Replace(" Act ", " Bud ");
    string lyMeasureDaxName = actMeasureDaxName.Replace(" Act ", " LY ");

    // Extract the core name and prefix for constructing the new measure names
    string coreName = selectedMeasureName.Substring(selectedMeasureName.IndexOf(" Act ") + 5);
    string prefix = selectedMeasureName.Substring(0, selectedMeasureName.IndexOf(" Act "));
    
    // --- Create Variance to Budget Measures ---

    // 1. Var to Bud Colour
    string name_Bud_Colour = $"{prefix} Var to Bud {coreName} Colour";
    string dax_Bud_Colour = $@"
VAR __actual = {actMeasureDaxName}
VAR __budget = {budMeasureDaxName}
VAR __variance = __actual - __budget
VAR __sentiment =
    SWITCH(
        TRUE(),
        __variance < 0, [Colour 1Primary],
        __variance > 0, [Colour 2Negative],
        [Colour 1Primary]
    )
VAR __format = FORMAT(__sentiment, ""0"")
RETURN
    __format
";
    table.AddMeasure(name_Bud_Colour, dax_Bud_Colour, displayFolder);
    
    // 2. Var to Bud KPI Colour
    string name_Bud_KPI_Colour = $"{prefix} Var to Bud {coreName} KPI Colour";
    string dax_Bud_KPI_Colour = $@"
VAR __actual = {actMeasureDaxName}
VAR __budget = {budMeasureDaxName}
VAR __variance = __actual - __budget
VAR __sentiment =
    SWITCH(
        TRUE(),
        __variance < 0, [Colour 3Positive],
        __variance > 0, [Colour 2Negative],
        [Colour 1Primary]
    )
VAR __format = FORMAT(__sentiment, ""0"")
RETURN
    __format
";
    table.AddMeasure(name_Bud_KPI_Colour, dax_Bud_KPI_Colour, displayFolder);

    // 3. Var to Bud KPI Detail
    string name_Bud_KPI_Detail = $"{prefix} Var to Bud {coreName} KPI Detail";
    string dax_Bud_KPI_Detail = $@"
VAR __actual = {actMeasureDaxName}
VAR __budget = {budMeasureDaxName}
VAR __variance = __actual - __budget
VAR __format_percent = FORMAT(__variance, ""#,0%;(#,0%)"")
VAR __sentiment =
    SWITCH(
        TRUE(),
        __variance > 0, [Arrow Up],
        __variance < 0, [Arrow down],
        [Arrow Neutral]
    )
VAR __text = __sentiment & "" "" & __format_percent
VAR __if_act_blank = IF(NOT(ISBLANK(__actual)), __text)
RETURN
    __if_act_blank
";
    table.AddMeasure(name_Bud_KPI_Detail, dax_Bud_KPI_Detail, displayFolder);

    // 4. Var to Bud KPI Labels Title
    string name_Bud_KPI_Labels_Title = $"{prefix} Var to Bud {coreName} KPI Labels Title";
    string dax_Bud_KPI_Labels_Title = $@"
VAR __label = ""Bud ""
VAR __number = {budMeasureDaxName}
VAR __format_percent = FORMAT(__number, ""#,0%;(#,0%)"")
VAR __text = __label & __format_percent
RETURN
    __text
";
    table.AddMeasure(name_Bud_KPI_Labels_Title, dax_Bud_KPI_Labels_Title, displayFolder);

    // 5. Var to Bud Labels
    string name_Bud_Labels = $"{prefix} Var to Bud {coreName} Labels";
    string dax_Bud_Labels = $@"
VAR __actual = {actMeasureDaxName}
VAR __budget = {budMeasureDaxName}
VAR __variance = __actual - __budget
VAR __format_percent = FORMAT(__variance, ""#,0%;(#,0%)"")
VAR __sentiment =
    SWITCH(
        TRUE(),
        __variance > 0, [Arrow Up],
        __variance < 0, [Arrow down],
        [Arrow Neutral]
    )
VAR __text = __sentiment & "" "" & __format_percent
VAR __if_act_blank = IF(NOT(ISBLANK(__actual)), __text)
RETURN
    __if_act_blank
";
    table.AddMeasure(name_Bud_Labels, dax_Bud_Labels, displayFolder);

    // --- Create Variance to LY Measures ---
    
    // 1. Var to LY KPI Colour
    string name_LY_KPI_Colour = $"{prefix} Var to LY {coreName} KPI Colour";
    string dax_LY_KPI_Colour = $@"
VAR __actual = {actMeasureDaxName}
VAR __ly = {lyMeasureDaxName}
VAR __variance = __actual - __ly
VAR __sentiment =
    SWITCH(
        TRUE(),
        __variance < 0, [Colour 3Positive],
        __variance > 0, [Colour 2Negative],
        [Colour 1Primary]
    )
VAR __format = FORMAT(__sentiment, ""0"")
RETURN
    __format
";
    table.AddMeasure(name_LY_KPI_Colour, dax_LY_KPI_Colour, displayFolder);

    // 2. Var to LY KPI Detail
    string name_LY_KPI_Detail = $"{prefix} Var to LY {coreName} KPI Detail";
    string dax_LY_KPI_Detail = $@"
VAR __actual = {actMeasureDaxName}
VAR __ly = {lyMeasureDaxName}
VAR __variance = __actual - __ly
VAR __format_percent = FORMAT(__variance, ""#,0%;(#,0%)"")
VAR __sentiment =
    SWITCH(
        TRUE(),
        __variance > 0, [Arrow Up],
        __variance < 0, [Arrow down],
        [Arrow Neutral]
    )
VAR __text = __sentiment & "" "" & __format_percent
VAR __if_act_blank = IF(NOT(ISBLANK(__actual)), __text)
RETURN
    __if_act_blank
";
    table.AddMeasure(name_LY_KPI_Detail, dax_LY_KPI_Detail, displayFolder);

    // 3. Var to LY KPI Labels Title
    string name_LY_KPI_Labels_Title = $"{prefix} Var to LY {coreName} KPI Labels Title";
    string dax_LY_KPI_Labels_Title = $@"
VAR __label = ""LY ""
VAR __number = {lyMeasureDaxName}
VAR __format_number = FORMAT(__number, ""#,0%;(#,0%)"")
VAR __text = __label & __format_number
RETURN
    __text
";
    table.AddMeasure(name_LY_KPI_Labels_Title, dax_LY_KPI_Labels_Title, displayFolder);
}

Info("Successfully created comparison measures for the selected item(s).");

