using System.Collections.Generic;
using System.Web;
using System.Web.Optimization;

namespace RegApplPortal.WebApps
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles-js/startup-actions").Include(
				"~/Scripts/startupActions.js"));

			bundles.Add(new ScriptBundle("~/bundles-js/common-libs").Include(
				"~/Scripts/bootstrap/bootstrap.js",
				"~/Scripts/jquery/jquery.js",
				"~/Scripts/jquery-ui/jquery-ui.js",
                "~/Scripts/js/datepicker-ru.js",
                "~/Scripts/moment/moment.js",
				"~/Scripts/underscore/underscore.js"));

			bundles.Add(new ScriptBundle("~/bundles-js/angular").Include(
				"~/Scripts/angular/angular.js",
				"~/Scripts/angular-sanitize/angular-sanitize.js"));

			bundles.Add(new ScriptBundle("~/bundles-js/account").Include(
				"~/Scripts/js/Account.js",
                "~/Scripts/js/PanelCollapsed.js",
				"~/Scripts/js/PartialProcessor.js",
				"~/Scripts/jquery-migrate/jquery-migrate.js",
				"~/Scripts/MicrosoftAjax/jquery.unobtrusive-ajax.js",
				"~/Scripts/MicrosoftAjax/MicrosoftAjax.js",
				"~/Scripts/MicrosoftAjax/MicrosoftMvcAjax.js"));

			bundles.Add(new ScriptBundle("~/bundles-js/angular-ui").Include(
				"~/Scripts/angular-i18n/angular-locale_ru-ru.js",
				"~/Scripts/angular-bootstrap/ui-bootstrap-tpls.js",
				"~/Scripts/angular-ui-grid/ui-grid.js",
				"~/Scripts/angular-ui-mask/mask.js",
				"~/Scripts/angular-ui-select/select.js",
				"~/Scripts/angular-moment/angular-moment.js",
				"~/Scripts/angular-dateParser/angular-dateParser.js",
				"~/Scripts/angular-ui-uploader/uploader.js",
				"~/Scripts/angular-ui-validate/validate.js"));

			bundles.Add(new ScriptBundle("~/bundles-js/common-modules").Include(
				"~/AngularJS/Common/view.js",
				"~/AngularJS/Common/Components/selectUI/selectUI.js",
				"~/AngularJS/Common/Components/validator/validator.js",				
                "~/AngularJS/Common/Components/datepickerPopup/datepickerPopup.js",
				"~/AngularJS/Common/Components/fileUploader/fileUploader.js",
				"~/AngularJS/Common/Services/convertDate.js",
                "~/AngularJS/Common/Services/ajaxService.js",
				"~/AngularJS/Common/Services/modalService.js",
				"~/AngularJS/Common/Filters/directoryUsersItem.js",
                "~/AngularJS/Common/Filters/directoryItem.js"));

			bundles.Add(new ScriptBundle("~/bundles-js/home-statement-module").Include(
				"~/AngularJS/Home/Statement/statement.js",
				"~/AngularJS/Home/Statement/addFileCtrl.js",
				"~/AngularJS/Home/Statement/changeStatusCtrl.js",
				"~/AngularJS/Home/Statement/statementCtrl.js",
				"~/AngularJS/Home/Statement/statementService.js"));

			bundles.Add(new ScriptBundle("~/bundles-js/home-journal-module").Include(				
                "~/AngularJS/Home/Journal/journalStatement.js",
				"~/AngularJS/Home/Journal/customGridDateFilter.js",
				"~/AngularJS/Home/Journal/journalStatementCtrl.js",
				"~/AngularJS/Home/Journal/journalStatementService.js"));

			bundles.Add(new ScriptBundle("~/bundles-js/app-module").Include(
				"~/AngularJS/app.js",
				"~/AngularJS/mainCtrl.js"));

			bundles.Add(new StyleBundle("~/Content/bootstrap/css").Include(
				"~/Content/bootstrap/bootstrap.css"));

			bundles.Add(new StyleBundle("~/Content/angular-ui-grid/css").Include(
				"~/Content/angular-ui-grid/ui-grid.css"));

			bundles.Add(new StyleBundle("~/Content/angular-ui-select/css").Include(
				"~/Content/angular-ui-select/select.css"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/site.css"));
		}

		class NonOrderingBundleOrderer : IBundleOrderer
		{
			public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
			{
				return files;
			}

		}
	}
}
