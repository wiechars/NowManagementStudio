using System.Web;
using System.Web.Optimization;

namespace NowManagementStudio
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.dataTables.js",
                        "~/Scripts/Angular1.3.5/angular.js",
                         "~/Scripts/datatables/angular-datatables.js"
                        ));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/TweenMax.min.js",
                      "~/Scripts/resizeable.js",
                      "~/Scripts/joinable.js",
                      "~/Scripts/xenon-api.js",
                      "~/Scripts/xenon-toggles.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/angularApp").Include(
                      
                      "~/Scripts/Angular1.3.5/angular-route.js",
                      "~/Scripts/Angular1.3.5/angular-resource.js",
                      "~/Scripts/Angular1.3.5/angular-ui-bootstrap.js",
                      "~/App/nmsApp.js",
                      "~/App/directives/ui-bootstrap-tpls-0.13.1.js",
                      "~/App/directives/angular-datatables.directive.js",
                      "~/App/services/angular-datatables.factory.js",
                      "~/App/services/angular-datatables.instances.js",
                      "~/App/services/angular-datatables.options.js",
                      "~/App/services/angular-datatables.renderer.js",
                      "~/App/services/angular-datatables.util.js",
                      "~/App/clientRoute.js",
                      "~/App/DataService/contactDataService.js",
                      "~/App/contact/controller/contactController.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/custom.css",
                      "~/Content/xenon-components.css",
                      "~/Content/xenon-core.css",
                      "~/Content/xenon-forms.css",        
                      "~/Content/xenon-skins.css",
                      "~/Content/xenon.css",
                      "~/Content/fonts/linecons/css/linecons.css",
                      "~/Content/fonts/fontawesome/css/font-awesome.min.css"
                     ));


            bundles.Add(new StyleBundle("~/bundles/datatables").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/custom.css",
                      "~/Content/xenon-components.css",
                      "~/Content/xenon-core.css",
                      "~/Content/xenon-forms.css",
                      "~/Content/xenon-skins.css",
                      "~/Content/xenon.css",
                      "~/Content/fonts/linecons/css/linecons.css",
                      "~/Content/fonts/fontawesome/css/font-awesome.min.css"
                     ));


        }
    }
}
