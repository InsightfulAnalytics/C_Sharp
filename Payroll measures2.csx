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

// Measure: Allowance $
var measure13 = table.AddMeasure(
    "Allowance $",                          // Measure Name
    "[Afternoon Shift Allowance(Apply after 12pm) ($) Total] + [Night Shift Allowance (Apply after 6 pm) ($) Total] + [Casual Penalty Rate - Public Holiday Worked ($) Total] + [Night Shift Allowance(Apply after 6pm)-cas loading ($) Total] + [Travel Allowance Klm 85 cents > 5000Klms ($) Total] + [On call allowance Weekdays (N & M) ($) Total] + [On call allowance on Weekends (N & M) ($) Total] + [On call allowance On Public Holiday (N & M) ($) Total] + [On call allowance On Public Holiday(Health Supprt) ($) Total] + [Laundry Allowance ($) Total] + [Shoe Allowance ($) Total] + [Infectious Cleaning Allowance ($) Total] + [Lead Apron Allowance ($) Total] + [Meal Allowance on OT exceeds 4 hrs ($) Total] + [Meal Allowance on No Break ($) Total] + [Afternoon Shift Allowance- 12.5% Num 2 ($) Total] + [Nightshift Allowance-15% Num 2 ($) Total] + [Qualification Allowance (Dip/Degree) Total] + [Qualification Allowance (Masters/PhD) Total] + [Phone Allowance Total] + [PT Qualification Allowance-Diploma/Degree ($) Total] + [PT Qualification Allowance-Master/PHD ($) Total] + [PT Qualification Allowance-Post Grad Cert ($) Total]",        // DAX Expression
    "Payroll"                                     // Display Folder (optional)
);
measure13.FormatString = "$#,##0;($#,##0)";

// Measure: Parental Leave Hours
var measure14 = table.AddMeasure(
    "Parental Leave Hours",                      // Measure Name
    "[Company Paid Parental Leave (Qty) Total]",  // DAX Expression
    "Payroll"                                     // Display Folder (optional)
);
measure14.FormatString = "#,##0;(#,##0)";

// Measure: Parental Leave $
var measure15 = table.AddMeasure(
    "Parental Leave $",                          // Measure Name
    "[Company Paid Parental Leave ($) Total]",    // DAX Expression
    "Payroll"                                     // Display Folder (optional)
);
measure15.FormatString = "$#,##0;($#,##0)";

// Measure: Workers Compensation Hours
var measure16 = table.AddMeasure(
    "Workers Compensation Hours",                // Measure Name
    "[Workers Compensation (Qty) Total]",         // DAX Expression
    "Payroll"                                     // Display Folder (optional)
);
measure16.FormatString = "#,##0;(#,##0)";

// Measure: Workers Compensation $
var measure17 = table.AddMeasure(
    "Workers Compensation $",                    // Measure Name
    "[Workers Compensation ($) Total]",           // DAX Expression
    "Payroll"                                     // Display Folder (optional)
);
measure17.FormatString = "$#,##0;($#,##0)";

// Measure: Sick/Comp Leave Hours
var measure18 = table.AddMeasure(
    "Sick/Comp Lease Hours",                     // Measure Name
    "[Personal Leave (Qty) Total] + [Compassionate Leave (Qty) Total]",  // DAX Expression
    "Payroll"                                     // Display Folder (optional)
);
measure18.FormatString = "#,##0;(#,##0)";

// Measure: Sick/Comp Leave $
var measure19 = table.AddMeasure(
    "Sick/Comp Leave $",                         // Measure Name
    "[Personal Leave ($) Total] + [Compassionate Leave ($) Total]",        // DAX Expression
    "Payroll"                                     // Display Folder (optional)
);
measure19.FormatString = "$#,##0;($#,##0)";

// Measure: Superannuation
var measure20 = table.AddMeasure(
    "Superannuation",                           // Measure Name
    "[SG Contribution Total]",                    // DAX Expression
    "Payroll"                                     // Display Folder (optional)
);
measure20.FormatString = "$#,##0;($#,##0)";

// Measure: Total Working Hours
var measure21 = table.AddMeasure(
    "Total Working Hours",                      // Measure Name
    "[Total Ordinary Hours Total] + [Total Overtime Hours Total] + [Public Holiday Hours Total] + [Total Annual Leave Hours Total] + [Sick/Comp Lease Hours Total] + [Parental Leave Hours Total]", // DAX Expression
    "Payroll"                                     // Display Folder (optional)
);
measure21.FormatString = "#,##0;(#,##0)";

// Measure: Total Remuneration
var measure22 = table.AddMeasure(
    "Total Remuneration",                       // Measure Name
    "[Gross Total] + [SG Contribution Total]",    // DAX Expression
    "Payroll"                                     // Display Folder (optional)
);
measure22.FormatString = "$#,##0;($#,##0)";
