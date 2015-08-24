'use strict';
app.controller('loginController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {
    $scope.currentUser = "";
    $scope.currentRole = "Admininstrator";
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