app.config(['$routeProvider', function ($routeProvider) {
    
    $routeProvider.when('/', {
        templateUrl: "client/app//Home/home.html"
    }),

    $routeProvider.when('/about', {
        templateUrl: "client/app/Home/about.html"
    }),

        $routeProvider.when('/chat', {
            templateUrl: "client/app/Home/chat.html"
        }),

    $routeProvider.when('/dashboard', {
        templateUrl: "client/app/dashboard/html/dashboard.html"
    }),
        $routeProvider.when('/datatables', {
            templateUrl: "client/app/datatables/html/datatables.html"
        }),

    $routeProvider.when('/mycontacts', {
        templateUrl: "client/app/contact/contactsList.tpl.html",
        controller: "contactController"
    }),

    $routeProvider.when('/mycontacts/newcontact', {
        templateUrl: "client/app/contact/html/contactForm.html",
        controller: "contactAddController"
    }),
    
    $routeProvider.when('/mycontacts/:id', {
        templateUrl: "client/app/contact/html/contactForm.html",
        controller: "contactEditController"
    }),

    $routeProvider.otherwise({
        redirectTo: '/'
    });

    app.run(['authService', function (authService) {
        authService.fillAuthData();
    }]);

}]);
