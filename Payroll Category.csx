
// 1. Provide the list of categories (excluding the heading "Category").
var categories = new List<string>
{
    "Afterhours Manager-Num 2 ($)",
    "Afterhours Manager-Num 2 (Hours)",
    "Afterhours Manager-Num 2- Casual ($)",
    "Afterhours Manager-Num 2- Casual (Hours)",
    "Afternoon Shift Allowance- 12.5% Num 2 ($)",
    "Afternoon Shift Allowance- 12.5% Num 2 (Hours)",
    "Afternoon Shift Allowance(Apply after 12pm) ($)",
    "Afternoon Shift Allowance(Apply after 12pm) (Qty)",
    "Annual Leave ($)",
    "Annual Leave (Qty)",
    "Annual Leave Loading ($)",
    "Annual Leave Payout ($)",
    "Annual Leave Payout (Qty)",
    "Backpay Normal",
    "Backpay Overtime",
    "Casual- Ordinary Hours ($)",
    "Casual- Ordinary Hours (Qty)",
    "Casual Penalty Rate - Saturday-No casual loading ($)",
    "Casual Penalty Rate - Saturday-No casual loading (Qty)",
    "Casual Penalty Rate - Sunday -No Casual loading ($)",
    "Casual Penalty Rate - Sunday -No Casual loading (Qty)",
    "Company Paid Parental Leave ($)",
    "Company Paid Parental Leave (Qty)",
    "Compassionate Leave ($)",
    "Compassionate Leave (Qty)",
    "Gross",
    "Infectious Cleaning Allowance ($)",
    "Infectious Cleaning Allowance (Week)",
    "Laundry Allowance ($)",
    "Laundry Allowance (Hour)",
    "Lead Apron Allowance ($)",
    "Lead Apron Allowance (Hour)",
    "Leave Without Pay ($)",
    "Leave Without Pay (Qty)",
    "Meal Allowance on No Break ($)",
    "Meal Allowance on No Break (Unit)",
    "Meal Allowance on OT exceeds 4 hrs ($)",
    "Meal Allowance on OT exceeds 4 hrs (Unit)",
    "Night Shift Allowance (Apply after 6 pm) ($)",
    "Night Shift Allowance (Apply after 6 pm) (Qty)",
    "Nightshift Allowance-15% Num 2 ($)",
    "Nightshift Allowance-15% Num 2 (Hours)",
    "Normal Casual loading 25% ($)",
    "Normal Casual loading 25% (Qty)",
    "On call allowance On Public Holiday (N & M) ($)",
    "On call allowance On Public Holiday (N & M) (Unit)",
    "On call allowance on Weekends (N & M) ($)",
    "On call allowance on Weekends (N & M) (Unit)",
    "On call allowance Weekdays (N & M) ($)",
    "On call allowance Weekdays (N & M) (Unit)",
    "Ordinary Hourly Rate- NUM 3 ($)",
    "Ordinary Hourly Rate- NUM 3 (Hours)",
    "Overtime over 2 hrs Mon- Fri- Casual ($)",
    "Overtime over 2 hrs Mon- Fri- Casual (Qty)",
    "Overtime over 2 hrs Mon- Sat ($)",
    "Overtime over 2 hrs Mon- Sat (Qty)",
    "Overtime- Sunday ($)",
    "Overtime- Sunday (Qty)",
    "Overtime- Sunday-Casual ($)",
    "Overtime- Sunday-Casual (Qty)",
    "Overtime Upto 2 hrs-Mon-Fri- Casual ($)",
    "Overtime Upto 2 hrs-Mon-Fri- Casual (Qty)",
    "Overtime Upto 2 hrs-Mon-Sat ($)",
    "Overtime Upto 2 hrs-Mon-Sat (Qty)",
    "Overtime Upto 2 hrs-Mon-Sat Num 2 ($)",
    "Overtime Upto 2 hrs-Mon-Sat Num 2 (Hours)",
    "Penalty Rate - Saturday ($)",
    "Penalty Rate - Saturday (Qty)",
    "Penalty Rate - Sunday ($)",
    "Penalty Rate - Sunday (Qty)",
    "Penalty Rate- Saturday-Sunday-75% Num 2 ($)",
    "Penalty Rate- Saturday-Sunday-75% Num 2 (Hours)",
    "Permanent Ordinary Hours-FT/PT ($)",
    "Permanent Ordinary Hours-FT/PT (Qty)",
    "Personal Leave ($)",
    "Personal Leave (Qty)",
    "Phone Allowance",
    "PT Qualification Allowance-Diploma/Degree ($)",
    "PT Qualification Allowance-Diploma/Degree (Hour)",
    "PT Qualification Allowance-Master/PHD ($)",
    "PT Qualification Allowance-Master/PHD (Hour)",
    "PT Qualification Allowance-Post Grad Cert ($)",
    "PT Qualification Allowance-Post Grad Cert (Hour)",
    "Public Holiday Not Worked ($)",
    "Public Holiday Not Worked (Qty)",
    "Public Holiday Worked ($)",
    "Public Holiday Worked (Qty)",
    "Qualification Allowance (Dip/Degree)",
    "SG Contribution",
    "Shoe Allowance ($)",
    "Shoe Allowance (Hour)",
    "Termination Normal",
    "Workers Compensation ($)",
    "Workers Compensation (Qty)"
};


// 2. Reference your table (change the table name if necessary).
var measureTable = Model.Tables["FactPayRuns UnPivot"];

// 3. Create a measure for each category, appending " Total" to the measure name.
foreach (var category in categories)
{
    var measureName = category + " Total";
    
    // Build the DAX expression (the measure name isn't repeated inside the expression itself).
    var daxExpression = $@"
VAR __tbl =
    DISTINCT('FactPayRuns UnPivot'[Category])

VAR __filter =
    FILTER(__tbl, 'FactPayRuns UnPivot'[Category] = ""{category}"")

VAR __sumx =
    SUMX(__filter, [Payroll $ Unpivot])

RETURN
    __sumx
";

    // Add the measure to the table
    var newMeasure = measureTable.AddMeasure(measureName, daxExpression);
    
    // Place the measure in the "PayrollMeasures" folder
    newMeasure.DisplayFolder = "PayrollMeasures";

    // (Optional) Set a default format string
    newMeasure.FormatString = "#,0.00;(#,0.00);0.00";
}
