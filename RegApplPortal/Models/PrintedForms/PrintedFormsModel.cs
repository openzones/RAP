namespace RegApplPortal.WebApps.Models.PrintedForms
{
    public class PrintedFormsModel
    {
        public PrintedFormsModel()
        {
            BaseReport = new BaseReportModel();
        }

        public BaseReportModel BaseReport { get; set; }

    }
}