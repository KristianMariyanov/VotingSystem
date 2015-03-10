namespace VotingSystem.Web
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            RegisterScriptsBundles(bundles);
            RegisterStylesBundles(bundles);

            BundleTable.EnableOptimizations = false;
        }

        private static void RegisterStylesBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/custum").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/kendo").Include(
                "~/Content/kendo/kendo.common.min.css",
                "~/Content/kendo/kendo.common-bootstrap.min.css",
                "~/Content/kendo/kendo.silver.min.css"));
        }

        private static void RegisterScriptsBundles(BundleCollection bundles)
        {
            bundles.Add(
                new ScriptBundle("~/bundles/jquery").Include("~/Scripts/kendo/jquery.min.js"));
            bundles.Add(
                new ScriptBundle("~/bundles/global").Include("~/Scripts/global.js"));
            bundles.Add(
                new ScriptBundle("~/bundles/checkbox").Include("~/Scripts/VoteWithCheckbox.js"));
            bundles.Add(
                new ScriptBundle("~/bundles/fileupload").Include("~/Scripts/bootstrap-fileinput/fileinput.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                "~/Scripts/kendo/kendo.all.min.js",
                "~/Scripts/kendo/kendo.aspnetmvc.min.js",
                "~/Scripts/kendo/kendo.culture.en-GB.min.js",
                "~/Scripts/kendo/kendo.culture.bg.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryajax").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

        }
    }
}
