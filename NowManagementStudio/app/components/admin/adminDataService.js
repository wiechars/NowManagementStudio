app.factory('adminDataService', ['$http', '$q',
function ($http, $q) {
    var _users = [];

    var _getUsers = function () {
        var deferred = $q.defer();
        var controllerQuery = "api/Users";

        $http.get(controllerQuery)
          .then(function (result) {
              // Successful
              angular.copy(result.data, _users);
              deferred.resolve();
          },
          function (error) {
              // Error
              deferred.reject();
          });
        return deferred.promise;
    }

    var _addUser = function (_user) {
        var deferred = $q.defer();
        var controllerQuery = "api/Account/Register";

        $http.post(controllerQuery, _user)
          .then(function (result) {
              //Success
              deferred.resolve();
          },
          function (error) {
              // Error
              deferred.reject();
          });
        return deferred.promise;

    };
      

    //Expose methods and fields through revealing pattern
    return {
        users: _users,
        getUsers: _getUsers,
        addUser: _addUser,

    }

}]);