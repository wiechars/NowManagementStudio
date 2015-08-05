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
                        "~/client/assets/scripts/jquery-{version}.js",
                        "~/client/assets/scripts/jquery.signalR-2.2.0.min.js",
                        "~/client/assets/scripts/jquery.dataTables.js",
                        "~/client/assets/scripts/Angular1.3.5/angular.js",
                         "~/client/assets/scripts/datatables/angular-datatables.js"
                        ));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/client/assets/scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/client/assets/scripts/bootstrap.js",
                      "~/client/assets/scripts/respond.js",
                      "~/client/assets/scripts/TweenMax.min.js",
                      "~/client/assets/scripts/resizeable.js",
                      "~/client/assets/scripts/joinable.js",
                      "~/client/assets/scripts/xenon-api.js",
                      "~/client/assets/scripts/xenon-toggles.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/angularApp").Include(
                      
                      "~/client/assets/scripts/Angular1.3.5/angular-route.js",
                      "~/client/assets/scripts/Angular1.3.5/angular-resource.js",
                      "~/client/assets/scripts/Angular1.3.5/angular-ui-bootstrap.js",
                      "~/client/app/app.js",
                      "~/client/common/directives/ui-bootstrap-tpls-0.13.1.js",
                      "~/client/common/directives/angular-datatables.directive.js",
                       "~/client/common/directives/*.js",
                      "~/client/common/services/angular-datatables.factory.js",
                      "~/client/common/services/angular-datatables.instances.js",
                      "~/client/common/services/angular-datatables.options.js",
                      "~/client/common/services/angular-datatables.renderer.js",
                      "~/client/common/services/angular-datatables.util.js",
                      "~/client/app/clientRoute.js",
                      "~/client/app/dataservice/contactDataService.js",
                      "~/client/app/contact/contactController.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/client/assets/bootstrap.css",
                      "~/client/assets/custom.css",
                      "~/client/assets/xenon-components.css",
                      "~/client/assets/xenon-core.css",
                      "~/client/assets/xenon-forms.css",        
                      "~/client/assets/xenon-skins.css",
                      "~/client/assets/xenon.css",
                      "~/client/assets/fonts/linecons/css/linecons.css",
                      "~/client/assets/fonts/fontawesome/css/font-awesome.min.css"
                     ));


            bundles.Add(new StyleBundle("~/bundles/datatables").Include(
                      "~/client/assets/bootstrap.css",
                      "~/client/assets/custom.css",
                      "~/client/assets/xenon-components.css",
                      "~/client/assets/xenon-core.css",
                      "~/client/assets/xenon-forms.css",
                      "~/client/assets/xenon-skins.css",
                      "~/client/assets/xenon.css",
                      "~/client/assets/fonts/linecons/css/linecons.css",
                      "~/client/assets/fonts/fontawesome/css/font-awesome.min.css"
                     ));


        }
    }
}
