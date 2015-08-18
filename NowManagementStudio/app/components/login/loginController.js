'use strict';
app.controller('loginController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {
    $scope.userName = "";
    $scope.loginData = {
        userName: "",
        password: ""
    };

    $scope.message = "";

    $scope.login = function () {
     
        authService.login($scope.loginData).then(function (response) {
            $location.path('/app/contacts');
            $scope.userName = "Wiecha";

        },
         function (err) {
             $scope.message = err.error_description;
         });
    };


    $scope.logout = function () {
        authService.logOut();
        $location.path('/login');

    };

}]);