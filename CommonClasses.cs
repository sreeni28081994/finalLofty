using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for CommonClasses
/// </summary>
public class CommonClasses
{
    public CommonClasses()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}
public class WeekPeriods
{
    public int SlNo { get; set; }
    public int TTWeekPeriodID { get; set; }
    public int TTPeriodID { get; set; }
    public string PeriodName { get; set; }
    public bool PeriodType { get; set; }
    public int WeekDayID { get; set; }
    public string WeekDayName { get; set; }
    public bool IsAllocated { get; set; }
    public bool IsBlocked { get; set; }

}
public class AllocationPeriod
{
    public int SlNo { get; set; }
    public int TTAllocationPeriodID { get; set; }
    public int TTAllocationID { get; set; }
    public int SuccessiveID { get; set; }
    public string IsSuccessive { get; set; }
    public int CollimateID { get; set; }
    public string IsCollimate { get; set; }
    public bool IsLocked { get; set; }
    public int TTClassSectionID { get; set; }
    public string ClassSectionName { get; set; }
    public int TTSubjectID { get; set; }
    public string SubjectName { get; set; }
    public string SubjectCode { get; set; }
    public string TTFacultyID { get; set; }
    public string FacultyName { get; set; }
    public string FacultyCode { get; set; }
    public List<FacultyItem> lstFaculty { get; set; }
    public int TTWeekPeriodID { get; set; }
}
public class FacultyItem
{
    public int TTFacultyID { get; set; }
    public string FacultyName { get; set; }
    public string FacultyCode { get; set; }
}
public class MyCustomTemplate : ITemplate
{
    string _columnName;
    public MyCustomTemplate(string colname)
    {
        _columnName = colname;
    }
    public void InstantiateIn(Control container)
    {
        CheckBox cb = new CheckBox();
        cb.ID = _columnName;
        cb.DataBinding += new EventHandler(Cb_DataBinding);
        container.Controls.Add(cb);
    }

    private void Cb_DataBinding(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        GridViewRow container = (GridViewRow)chk.NamingContainer;
        object dataValue = DataBinder.Eval(container.DataItem, _columnName);
        if (dataValue != DBNull.Value)
        {
            chk.Checked = (dataValue.ToString().ToLower()=="true")?true:false;
        }
    }
}
public class TTBlockedFaculty
{
    public int TTFacultyID { get; set; }
    public int TTWeekPeriodID { get; set; }
}
public class TTSubstitution
{
    public int TTFacultyID { get; set; }
    public int TTWeekPeriodID { get; set; }
    public DateTime AttendanceDate { get; set; }
    public int TTSFacultyID { get; set; }
}