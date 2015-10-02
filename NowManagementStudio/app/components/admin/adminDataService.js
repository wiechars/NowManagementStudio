app.factory('adminDataService', ['$http', '$q',
function ($http, $q) {
    var _users = [];


    /**************************************
    ***        Get Users                ***
    **************************************/
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


    /**************************************
    ***        Add  User                ***
    **************************************/
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

    /**************************************
    ***        Edit  User               ***
    **************************************/
    var _editUser = function (_user) {
        var deferred = $q.defer();
        var controllerQuery = "api/Account/EditUser";

        $http.post(controllerQuery, _user)
          .then(function (result) {
              //Success
              deferred.resolve();
          },
          function (error) {
              // Error
              alert(error.statusText);
              deferred.reject();
          });
        return deferred.promise;

    };

    /**************************************
    ***        Find By Username         ***
   **************************************/
    function _findUserByName(userName) {
        var found = null;
        $.each(_users, function (i, user) {
            if (user.userName == userName) {
                found = user;
                return false;
            }
        });
        return found;
    };
      

    //Expose methods and fields through revealing pattern
    return {
        users: _users,
        getUsers: _getUsers,
        addUser: _addUser,
        editUser: _editUser,
        findUserByName: _findUserByName

    }

}]);