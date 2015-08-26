'use strict';
app.controller('loginController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {
    $scope.currentUser = "";
    $scope.currentRole = "";
    $scope.loginData = {
        userName: "",
        password: ""
    };

    //Get Current Logged in user
    var user = authService.getCurrentUser();
    if (user) {
        $scope.currentUser = user.userName;
        $scope.currentRole = user.role;
    }

    $scope.message = "";

    $scope.login = function () {
     
        authService.login($scope.loginData).then(function (response) {
            $location.path('/app/contacts');
            //Remove Light theme - this is a hack due to our custom login screen
            //instead of burying this deep in the xenon controller.js
            $('body').removeClass('login-page login-light');
           
        },
         function (err) {
             $('.page-loading-overlay').addClass('loaded');
             $scope.message = err.error_description
             
         });
    };


    $scope.logout = function () {
        authService.logOut();
        $location.path('/login');

    };

}]);