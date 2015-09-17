app.factory('reportDataService', ['$http', '$q',
function ($http, $q) {
    var _lot = [];
  

    var _getLot = function (id) {
        var deferred = $q.defer();
        var controllerQuery = "api/Inventory/Report/"+id;

        $http.get(controllerQuery)
          .then(function (result) {
              // Successful
              angular.copy(result.data[0], _lot);
              deferred.resolve();
          },
          function (error) {
              // Error
              deferred.reject();
          });
        return deferred.promise;
    }

 

    //Expose methods and fields through revealing pattern
    return {
        lot: _lot,
        getLot: _getLot,

    }

}]);