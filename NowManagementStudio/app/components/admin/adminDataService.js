app.factory('adminDataService', ['$http', '$q',
function ($http, $q) {
    var _users = [];
    //var _roles = [];
    var _rolesByUser = [];

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
     ***        Get User Roles          ***
     **************************************/
    //var _getUserRoles = function () {
    //    var deferred = $q.defer();
    //    var controllerQuery = "api/Users/Roles";
    //    $http.get(controllerQuery)
    //      .then(function (result) {
    //          // Successful
    //          angular.copy(result.data, _roles);
    //          deferred.resolve();
    //      },
    //      function (error) {
    //          // Error
    //          deferred.reject();
    //      });
    //    return deferred.promise;
    //}

    /**************************************
   ***        Get Roles By User         ***
   **************************************/
    var _getRolesByUser = function (_user) {
        var deferred = $q.defer();
        var controllerQuery = "api/Users/RolesByUser/"+_user.userName;
        $http.get(controllerQuery)
          .then(function (result) {
              // Successful
              angular.copy(result.data, _rolesByUser);
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
        var controllerQuery = "api/Users/Register";

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
        var controllerQuery = "api/Users/EditUser";

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
    ***        Edit  User Roles         ***
    **************************************/
    var _editUserRoles = function (roles, userId) {
        var deferred = $q.defer();
        var controllerQuery = "api/Users/EditRoles/" + userId + "/" + roles;

        $http.post(controllerQuery, roles)
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
        //roles: _roles,
        rolesByUser: _rolesByUser,
        //getUserRoles: _getUserRoles,
        getRolesByUser: _getRolesByUser,
        addUser: _addUser,
        editUser: _editUser,
        editUserRoles: _editUserRoles,
        findUserByName: _findUserByName

    }

}]);