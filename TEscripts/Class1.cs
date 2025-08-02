
using TabularEditor.TOMWrapper;
using TabularEditor.Scripting;


namespace TEscripts
{
    public class Class1
    {
        Model Model;
        TabularEditor.UI.UITreeSelection Selected;

        void script1()
        {
            foreach (Column c in Selected.Columns)
            {
                Measure measure = c.Table.AddMeasure(
                    name: "Sum of " + c.Name,
                    expression: "SUM(" + c.DaxObjectFullName + ")"
                );
                measure.FormatString = c.FormatString;
                measure.FormatDax();
            }

        }
    }
}
