﻿app.config(['$routeProvider', function ($routeProvider) {
    
    $routeProvider.when('/', {
        templateUrl: "/app/Home/home.html"
    }),

    $routeProvider.when('/about', {
        templateUrl: "app/Home/about.html"
    }),

        $routeProvider.when('/chat', {
            templateUrl: "app/Home/chat.html"
        }),

    $routeProvider.when('/dashboard', {
        templateUrl: "app/dashboard/html/dashboard.html"
    }),
        $routeProvider.when('/datatables', {
            templateUrl: "app/datatables/html/datatables.html"
        }),

    $routeProvider.when('/mycontacts', {
        templateUrl: "app/contact/contactsList.tpl.html",
        controller: "contactController"
    }),

    $routeProvider.when('/mycontacts/newcontact', {
        templateUrl: "app/contact/html/contactForm.html",
        controller: "contactAddController"
    }),
    
    $routeProvider.when('/mycontacts/:id', {
        templateUrl: "app/contact/html/contactForm.html",
        controller: "contactEditController"
    }),

    $routeProvider.otherwise({
        redirectTo: '/'
    });

    app.run(['authService', function (authService) {
        authService.fillAuthData();
    }]);

}]);