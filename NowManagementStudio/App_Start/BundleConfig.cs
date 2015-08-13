using System.Web;
using System.Web.Optimization;

namespace NowManagementStudio
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/xenon/assets/js/bootstrap.min.js"                
                      
                      ));

            bundles.Add(new ScriptBundle("~/bundles/angularApp").Include(
                        "~/xenon/assets/js/jquery-1.11.1.min.js",
                        "~/xenon/assets/js/jquery.signalR-2.2.0.min.js",
                        "~/xenon/assets/js/datatables/js/jquery.dataTables.js",
                        "~/xenon/app/js/angular/angular.js",
                        "~/xenon/app/js/angular-datatables/angular-datatables.js",
                        "~/xenon/app/js/angular/angular-route.js",
                        "~/xenon/app/js/angular/angular-resource.js",
                        "~/xenon/app/js/app.js",
                        "~/xenon/app/js/angular-ui/ui-bootstrap-tpls-0.11.2.min.js",
                        "~/xenon/app/js/angular-datatables/angular-datatables.directive.js",
                        "~/xenon/app/js/common/directives/*.js",
                        "~/xenon/app/js/angular-datatables/angular-datatables.factory.js",
                        "~/xenon/app/js/angular-datatables/angular-datatables.instances.js",
                        "~/xenon/app/js/angular-datatables/angular-datatables.options.js",
                        "~/xenon/app/js/angular-datatables/angular-datatables.renderer.js",
                        "~/xenon/app/js/angular-datatables/angular-datatables.util.js",
                        "~/xenon/app/js/angular/angular-cookies.min.js",
                        "~/xenon/app/js/oc-lazyload/ocLazyLoad.min.js",
                        "~/xenon/app/js/angular-ui/angular-ui-router.min.js",
                        "~/xenon/assets/js/xenon-custom.js",
                        "~/xenon/assets/js/xenon-widgets.js",
                        "~/xenon/app/js/controllers.js",
                        "~/xenon/app/js/directives.js",
                        "~/xenon/app/js/services.js",
                        "~/xenon/app/js/factory.js",
                        "~/xenon/app/js/angular-fullscreen.js",
                        "~/xenon/assets/js/TweenMax.min.js",
                        "~/xenon/assets/js/TweenLite.min.js",
                        "~/xenon/assets/js/TweenMax.min.js",
                        "~/xenon/assets/js/resizeable.js",
                        "~/xenon/assets/js/xenon-api.js",
                        "~/xenon/assets/js/xenon-toggles.js",
                        "~/xenon/assets/js/joinable.js",
                        "~/xenon/app/components/contacts/*.js",
                        "~/xenon/app/shared/*.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/xenon/assets/css/bootstrap.css",
                      "~/xenon/assets/css/custom.css",
                      "~/xenon/assets/css/xenon-components.css",
                      "~/xenon/assets/css/xenon-core.css",
                      "~/xenon/assets/css/xenon-forms.css",
                      "~/xenon/assets/css/xenon-skins.css",
                      "~/xenon/assets/css/xenon.css",
                      "~/xenon/assets/css/fonts/linecons/css/linecons.css",
                      "~/xenon/assets/css/fonts/fontawesome/css/font-awesome.min.css",
                      "~/xenon/assets/js/datatables/css/jquery.dataTables.min.css"
                     ));
        }
    }
}
