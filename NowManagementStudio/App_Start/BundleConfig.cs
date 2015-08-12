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

  
                        ));


            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      //"~/client/assets/scripts/bootstrap.js",
                      "~/xenon/assets/js/bootstrap.min.js",
                      //"~/client/assets/scripts/respond.js",
                     // "~/client/assets/scripts/respond.js",
                      //"~/client/assets/scripts/TweenMax.min.js",
                     
                      //"~/client/assets/scripts/resizeable.js",
                     // "~/xenon/assets/js/resizeable.js",
                     // "~/client/assets/scripts/joinable.js",
                      "~/xenon/assets/js/joinable.js"
                     // "~/client/assets/scripts/xenon-api.js",
                      
                      //"~/client/assets/scripts/xenon-toggles.js"
                      
                      ));

            bundles.Add(new ScriptBundle("~/bundles/angularApp").Include(
                                      //"~/client/assets/scripts/jquery-{version}.js",
                        "~/xenon/assets/js/jquery-1.11.1.min.js",
                        //"~/client/assets/scripts/jquery.signalR-2.2.0.min.js",
                        "~/xenon/assets/js/jquery.signalR-2.2.0.min.js",
                        //"~/client/assets/scripts/jquery.dataTables.js",
                        "~/xenon/assets/js/datatables/js/jquery.dataTables.js",
                        //"~/client/assets/scripts/Angular1.3.5/angular.js",
                        "~/xenon/app/js/angular/angular.js",
                        //"~/client/assets/scripts/datatables/angular-datatables.js"
                         "~/xenon/app/js/angular-datatables/angular-datatables.js",
                      
                      //"~/client/assets/scripts/Angular1.3.5/angular-route.js",
                      "~/xenon/app/js/angular/angular-route.js",
                      //"~/client/assets/scripts/Angular1.3.5/angular-resource.js",
                       "~/xenon/app/js/angular/angular-resource.js",
                      //"~/client/assets/scripts/Angular1.3.5/angular-ui-bootstrap.js",
                       //"~/xenon/app/js/angular-ui/ui-bootstrap-tpls-0.11.2.min.js",
                      //"~/client/app/app.js",
                       "~/xenon/app/js/app.js",
                      //"~/client/common/directives/ui-bootstrap-tpls-0.13.1.js",
                      "~/xenon/app/js/angular-ui/ui-bootstrap-tpls-0.11.2.min.js",
                     // "~/client/common/directives/angular-datatables.directive.js",
                      "~/xenon/app/js/angular-datatables/angular-datatables.directive.js",
                       //"~/client/common/directives/*.js",
                       "~/xenon/app/js/common/directives/*.js",
                      //"~/client/common/services/angular-datatables.factory.js",
                       "~/xenon/app/js/angular-datatables/angular-datatables.factory.js",
                     // "~/client/common/services/angular-datatables.instances.js",
                     "~/xenon/app/js/angular-datatables/angular-datatables.instances.js",
                      //"~/client/common/services/angular-datatables.options.js",
                      "~/xenon/app/js/angular-datatables/angular-datatables.options.js",
                      //"~/client/common/services/angular-datatables.renderer.js",
                      "~/xenon/app/js/angular-datatables/angular-datatables.renderer.js",
                      //"~/client/common/services/angular-datatables.util.js",
                      "~/xenon/app/js/angular-datatables/angular-datatables.util.js",
                      //"~/client/app/clientRoute.js",
                      //"~/client/app/dataservice/contactDataService.js",
                      //"~/client/app/contact/contactController.js"
                      "~/xenon/app/js/angular/angular-cookies.min.js",
                        "~/xenon/app/js/oc-lazyload/ocLazyLoad.min.js",
                          "~/xenon/app/js/angular-ui/angular-ui-router.min.js",
                          "~/xenon/assets/js/xenon-custom.js",
                          "~/xenon/assets/js/xenon-widgets.js",
                          "~/xenon/app/js/controllers.js",
                          "~/xenon/app/js/directives.js",
                          "~/xenon/app/js/services.js",
                          "~/xenon/app/js/factory.js",
                          "~/app/js/angular-fullscreen.js",
                          "~/xenon/assets/js/TweenMax.min.js",
                          "~/xenon/assets/js/TweenLite.min.js",
                          "~/xenon/assets/js/TweenMax.min.js",
                          "~/xenon/assets/js/resizeable.js",
                          "~/xenon/assets/js/xenon-api.js",
                          "~/xenon/assets/js/xenon-toggles.js",
                            "~/xenon/app/components/contacts/*.js",
                            "~/xenon/app/shared/*.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/client/assets/bootstrap.css",
                      "~/xenon/assets/css/bootstrap.css",
                      //"~/client/assets/custom.css",
                      "~/xenon/assets/css/custom.css",
                      //"~/client/assets/xenon-components.css",
                      "~/xenon/assets/css/xenon-components.css",
                      //"~/client/assets/xenon-core.css",
                      "~/xenon/assets/css/xenon-core.css",
                      //"~/client/assets/xenon-forms.css",  
                      "~/xenon/assets/css/xenon-forms.css",
                      //"~/client/assets/xenon-skins.css",
                      "~/xenon/assets/css/xenon-skins.css",
                      //"~/client/assets/xenon.css",
                      "~/xenon/assets/css/xenon.css",
                      //"~/client/assets/fonts/linecons/css/linecons.css",
                      "~/xenon/assets/css/fonts/linecons/css/linecons.css",
                      //"~/client/assets/fonts/fontawesome/css/font-awesome.min.css"
                      "~/xenon/assets/css/fonts/fontawesome/css/font-awesome.min.css"
                     ));


        }
    }
}
