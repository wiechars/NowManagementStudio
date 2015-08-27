'use strict';
app.factory('authService', ['$http', '$q', 'localStorageService', function ($http, $q, localStorageService) {

    var serviceBase = 'http://localhost:14855/';
    var authServiceFactory = {};

    var _authentication = {
        isAuth: false,
        userName: ""
        };

    var _currentUser = {
        userName: "",
        role: ""
    };

    /************************************************
    ***        _saveRegistration                  ***
    *** Returns a promise which will be resolved  ***
    *** in the controller.                        ***
    ************************************************/
    var _saveRegistration = function (registration) {

        _logOut();

        return $http.post(serviceBase + 'api/account/register', registration).then(function (response) {
            return response;
        });

    };

    /************************************************
    ***        _login                             ***
    *** Send post request to endpoint that will   ***
    *** validate credentials and return access_token*
    ************************************************/
    var _login = function (loginData) {

        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

        var deferred = $q.defer();

        $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

            localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName, userRoles: response.roles });
            _authentication.isAuth = true;
            _authentication.userName = loginData.userName;
            deferred.resolve(response);

        }).error(function (err, status) {
            _logOut();
            deferred.reject(err);
        });

        return deferred.promise;

    };

    /************************************************
    ***        _logout                            ***
    *** Removes Authenication Data                ***
    ************************************************/
    var _logOut = function () {

        localStorageService.remove('authorizationData');

        _authentication.isAuth = false;
        _authentication.userName = "";

    };

    /************************************************
    ***        _fillAuthData                      ***
    *** Gets the locally stored auth data         ***
    ************************************************/
    var _fillAuthData = function () {

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            _authentication.isAuth = true;
            _authentication.userName = authData.userName;
        }

    }

    /************************************************
    ***        _getCurrentUser                      ***
    *** Gets the locally stored auth data         ***
    ************************************************/
    var _currentUser = function () {

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            _currentUser.userName = authData.userName;
            _currentUser.role = JSON.parse(authData.userRoles);
            alert(_currentUser.role);
        }
        return _currentUser;
    }

    authServiceFactory.saveRegistration = _saveRegistration;
    authServiceFactory.login = _login;
    authServiceFactory.logOut = _logOut;
    authServiceFactory.fillAuthData = _fillAuthData;
    authServiceFactory.getCurrentUser = _currentUser;
    authServiceFactory.authentication = _authentication;

    return authServiceFactory;
}]);