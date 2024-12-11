// For Tabular Editor - Create DAX Measures for the "Measures" Table

// Define the table where you want to add the measures.
// Replace "YourTableName" with the actual name of your table.
var table = Model.Tables["MeasuresTable"];
if (table == null)
{
    Error("Table 'MeasuresTable' not found.");
    return;
}

// Create DAX Measures based on provided definitions

// Measure: Total Ordinary Hours
var measure1 = table.AddMeasure(
    "Total Ordinary Hours",                        // Measure Name
    "[Permanent Ordinary Hours-FT/PT (Qty) Total] + [Casual- Ordinary Hours (Qty) Total] + [Afterhours Manager-Num 2- Casual (Hours) Total] + [Afterhours Manager-Num 2 (Hours) Total] + [Ordinary Hourly Rate- NUM 3 (Hours) Total]",   // DAX Expression
    "Payroll"                                      // Display Folder (optional)
);
measure1.FormatString = "#,##0;(#,##0)";

// Measure: Total Ordinary $
var measure2 = table.AddMeasure(
    "Total Ordinary $",                           // Measure Name
    "[Permanent Ordinary Hours-FT/PT ($) Total] + [Casual- Ordinary Hours ($) Total] + [Backpay Normal Total] + [Afterhours Manager-Num 2- Casual ($) Total] + [Afterhours Manager-Num 2 ($) Total] + [Ordinary Hourly Rate- NUM 3 ($) Total] + [Termination Normal Total]",        // DAX Expression
    "Payroll"                                     // Display Folder (optional)
);
measure2.FormatString = "$#,##0;($#,##0)";

// Measure: Total Overtime Hours
var measure3 = table.AddMeasure(
    "Total Overtime Hours",                      // Measure Name
    "[Overtime- Sunday (Qty) Total] + [Overtime over 2 hrs Mon- Sat (Qty) Total] + [Overtime Upto 2 hrs-Mon-Sat (Qty) Total] + [Overtime 2.5 - Public Holiday (Qty) Total] + [Overtime- Sunday-Casual (Qty) Total] + [Overtime over 2 hrs Mon- Fri- Casual (Qty) Total] + [Overtime Upto 2 hrs-Mon-Fri- Casual (Qty) Total] + [Recall - 2 hrs w/d - f/t triggers OT rates (Qty) Total]",   // DAX Expression
    "Payroll"                                     // Display Folder (optional)
);
measure3.FormatString = "#,##0;(#,##0)";

// Measure: Total Overtime $
var measure4 = table.AddMeasure(
    "Total Overtime $",                          // Measure Name
    "[Overtime- Sunday ($) Total] + [Overtime over 2 hrs Mon- Sat ($) Total] + [Overtime Upto 2 hrs-Mon-Sat ($) Total] + [Overtime 2.5 - Public Holiday ($) Total] + [Overtime- Sunday-Casual ($) Total] + [Overtime over 2 hrs Mon- Fri- Casual ($) Total] + [Overtime Upto 2 hrs-Mon-Fri- Casual ($) Total] + [Recall - 2 hrs w/d - f/t triggers OT rates ($) Total] + [Backpay Overtime Total]",       // DAX Expression
    "Payroll"                                     // Display Folder (optional)
);
measure4.FormatString = "$#,##0;($#,##0)";

// Measure: Casual Loading Hours
var measure5 = table.AddMeasure(
    "Casual Loading Hours",                      // Measure Name
    "[Normal Casual loading 25% (Qty) Total] + [After hour Manager Num 2-Casual loading 25% (Hours) Total]",  // DAX Expression
    "Payroll"                                     // Display Folder (optional)
);
measure5.FormatString = "#,##0;(#,##0)";

// Measure: Casual Loading$
var measure6 = table.AddMeasure(
    "Casual Loading$",                           // Measure Name
    "[Normal Casual loading 25% ($) Total] + [After hour Manager Num 2-Casual loading 25% ($) Total]",        // DAX Expression
    "Payroll"                                     // Display Folder (optional)
);
measure6.FormatString = "$#,##0;($#,##0)";

// Measure: Public Holiday Hours
var measure7 = table.AddMeasure(
    "Public Holiday Hours",                     // Measure Name
    "[Public Holiday Not Worked (Qty) Total] + [Public Holiday Worked (Qty) Total] + [Public Holiday Worked - Num2 (Qty) Total]",   // DAX Expression
    "Payroll"                                     // Display Folder (optional)
);
measure7.FormatString = "#,##0;(#,##0)";

// Measure: Public Holiday $
var measure8 = table.AddMeasure(
    "Public Holiday $",                         // Measure Name
    "[Public Holiday Not Worked ($) Total] + [Public Holiday Worked ($) Total] + [Public Holiday Worked - Num2 ($) Total]",        // DAX Expression
    "Payroll"                                     // Display Folder (optional)
);
measure8.FormatString = "$#,##0;($#,##0)";

// Measure: Total Annual Leave Hours
var measure9 = table.AddMeasure(
    "Total Annual Leave Hours",                 // Measure Name
    "[Annual Leave (Qty) Total] + [Leave Without Pay (Qty) Total] + [Annual Leave Payout (Qty) Total]",   // DAX Expression
    "Payroll"                                     // Display Folder (optional)
);
measure9.FormatString = "#,##0;(#,##0)";

// Measure: Total Annual Leave $
var measure10 = table.AddMeasure(
    "Total Annual Leave $",                     // Measure Name
    "[Annual Leave ($) Total] + [Annual Leave Loading ($) Total] + [Leave Without Pay ($) Total] + [Annual Leave Payout ($) Total]",   // DAX Expression
    "Payroll"                                     // Display Folder (optional)
);
measure10.FormatString = "$#,##0;($#,##0)";

// Measure: Penalty Hours
var measure11 = table.AddMeasure(
    "Penalty Hours",                            // Measure Name
    "[Penalty Rate - Saturday (Qty) Total] + [Penalty Rate - Sunday (Qty) Total] + [Casual Penalty Rate - Saturday-No casual loading (Qty) Total] + [Casual Penalty Rate - Sunday -No Casual loading (Qty) Total] + [Penalty Rate-Friday to Saturday- 50% Num 2 (Hours) Total] + [Penalty Rate- Saturday-Sunday-75% Num 2 (Hours) Total]",   // DAX Expression
    "Payroll"                                     // Display Folder (optional)
);
measure11.FormatString = "#,##0;(#,##0)";

// Measure: Penalty $
var measure12 = table.AddMeasure(
    "Penalty $",                                // Measure Name
    "[Penalty Rate - Saturday ($) Total] + [Penalty Rate - Sunday ($) Total] + [Casual Penalty Rate - Saturday-No casual loading ($) Total] + [Casual Penalty Rate - Sunday -No Casual loading ($) Total] + [Penalty Rate-Friday to Saturday- 50% Num 2 ($) Total] + [Penalty Rate- Saturday-Sunday-75% Num 2 ($) Total]",        // DAX Expression
    "Payroll"                                     // Display Folder (optional)
);
measure12.FormatString = "$#,##0;($#,##0)";
