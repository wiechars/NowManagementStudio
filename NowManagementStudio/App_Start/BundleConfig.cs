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
                      "~/assets/js/bootstrap.min.js"                
                      
                      ));

            bundles.Add(new ScriptBundle("~/bundles/angularApp").Include(
                        "~/assets/js/jquery-1.11.1.min.js",
                        "~/assets/js/jquery.signalR-2.2.0.min.js",
                       // "~/signalr/hubs",
                        "~/assets/js/datatables/js/jquery.dataTables.js",
                        "~/app/shared/xenon/angular/angular.js",
                        "~/app/shared/xenon/angular-datatables/angular-datatables.js",
                        "~/app/shared/xenon/angular/angular-route.js",
                        "~/app/shared/xenon/angular/angular-resource.js",
                        "~/app/app.js",
                        "~/app/shared/xenon/angular-ui/ui-bootstrap-tpls-0.11.2.min.js",
                        "~/app/shared/xenon/angular-datatables/angular-datatables.directive.js",
                        "~/app/shared/xenon/common/directives/*.js",
                        "~/app/shared/xenon/angular-datatables/angular-datatables.factory.js",
                        "~/app/shared/xenon/angular-datatables/angular-datatables.instances.js",
                        "~/app/shared/xenon/angular-datatables/angular-datatables.options.js",
                        "~/app/shared/xenon/angular-datatables/angular-datatables.renderer.js",
                        "~/app/shared/xenon/angular-datatables/angular-datatables.util.js",
                        "~/app/shared/xenon/angular/angular-cookies.min.js",
                        "~/app/shared/xenon/oc-lazyload/ocLazyLoad.min.js",
                        "~/app/shared/xenon/angular-ui/angular-ui-router.min.js",
                        "~/app/shared/signalr/signalr-hub.js",
                        "~/signalr/hubs",
                        "~/assets/js/xenon-custom.js",
                        "~/assets/js/xenon-widgets.js",
                        "~/app/shared/xenon/controllers.js",
                        "~/app/shared/xenon/directives.js",
                        "~/app/shared/xenon/services.js",
                        "~/app/shared/xenon/factory.js",
                        "~/app/shared/xenon/angular-fullscreen.js",
                        "~/app/shared/xenon/angular-local-storage.min.js",
                        "~/assets/js/TweenMax.min.js",
                        "~/assets/js/TweenLite.min.js",
                        "~/assets/js/TweenMax.min.js",
                        "~/assets/js/resizeable.js",
                        "~/assets/js/xenon-api.js",
                        "~/assets/js/xenon-toggles.js",
                        "~/assets/js/joinable.js",
                        "~/app/components/contacts/*.js",
                        "~/app/components/login/*.js",
                        "~/app/shared/authentication/*.js",
                        "~/app/shared/scrolling/*.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/assets/css/bootstrap.css",
                      "~/assets/css/custom.css",
                      "~/assets/css/xenon-components.css",
                      "~/assets/css/xenon-core.css",
                      "~/assets/css/xenon-forms.css",
                      "~/assets/css/xenon-skins.css",
                      "~/assets/css/xenon.css",
                      "~/assets/css/fonts/linecons/css/linecons.css",
                      "~/assets/css/fonts/fontawesome/css/font-awesome.min.css",
                      "~/assets/js/datatables/css/jquery.dataTables.min.css"
                     ));
        }
    }
}
