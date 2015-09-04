app.factory('inventoryDataService', ['$http', '$q',
function ($http, $q) {
    var _lots = [];
    var _locations = [];
    var _types = [];
    var _images = [];
    var _newLotId = -1;


    var _getMatTypes = function () {
        var deferred = $q.defer();
        var controllerQuery = "api/Inventory/MatTypes";

        $http.get(controllerQuery)
          .then(function (result) {
              // Successful
              angular.copy(result.data, _types);
              deferred.resolve();
          },
          function (error) {
              // Error
              deferred.reject();
          });
        return deferred.promise;
    }

    var _getLocations = function () {
        var deferred = $q.defer();
        var controllerQuery = "api/Inventory/Locations";

        $http.get(controllerQuery)
          .then(function (result) {
              // Successful
              angular.copy(result.data, _locations);
              deferred.resolve();
          },
          function (error) {
              // Error
              deferred.reject();
          });
        return deferred.promise;
    }

    var _getLots = function () {
        var deferred = $q.defer();
        var controllerQuery = "api/Inventory";

        $http.get(controllerQuery)
          .then(function (result) {
              // Successful
              angular.copy(result.data, _lots);
              deferred.resolve();
          },
          function (error) {
              // Error
              deferred.reject();
          });
        return deferred.promise;
    }

    var _addLot = function (_lot) {
        var deferred = $q.defer();
        var controllerQuery = "api/Inventory/AddLot";

        $http.post(controllerQuery, _lot)
          .then(function (result) {
              //Success
              _newLotId = result.data.id;
              deferred.resolve();
          },
          function (error) {
              // Error
              deferred.reject();
          });
        return deferred.promise;

    };

    var _updateLot = function (_lot) {
        var deferred = $q.defer();
        var controllerQuery = "api/Inventory/UpdateLot";

        $http.post(controllerQuery, _lot)
          .then(function (result) {
              deferred.resolve();
          },
          function (error) {
              // Error
              deferred.reject();
          });
        return deferred.promise;

    };

    function _findLotById(id) {
        var found = null;
        $.each(_lots, function (i, lot) {
            if (lot.id == id) {
                found = lot;
                return false;
            }
        });
        return found;
    };

    function _getNewLotId() {
        return _newLotId
    }


    function _findImagesByLotId(_lot) {
        var deferred = $q.defer();
        var controllerQuery = "api/Inventory/Images/"+_lot.id;

        $http.get(controllerQuery)
          .then(function (result) {
              // Successful
              angular.copy(result.data, _images);
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
        types: _types,
        images: _images,
        getMatTypes: _getMatTypes,
        locations: _locations,
        getLocations: _getLocations,
        lots: _lots,
        newLotId: _newLotId,
        getNewLotId: _getNewLotId,
        getLots: _getLots,
        addLot: _addLot,
        updateLot: _updateLot,
        findLotById: _findLotById,
        findImagesByLotId: _findImagesByLotId
    }

}]);