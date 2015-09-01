/**************************************
***        Get Contact             ***
**************************************/
app.controller('inventoryController',
    ['$scope', 'inventoryDataService', '$location',
    function categoryController($scope, inventoryDataService) {
        $scope.lots = [];
        $scope.locations = [];
        $scope.types = [];
        $scope.alerts = [];
        $scope.message = "";
        $scope.showEditForm = false;
        

        loadInventory();


        
        /**************************************
        ***        Hide Buttons              ***
        **************************************/
        function hideButtons() {
            $scope.disableTagButton = { 'visibility': 'hidden' }; 
        }

        /**************************************
        ***        Show Buttons              ***
        **************************************/
        function showButtons() {
            $scope.disableTagButton = { 'visibility': 'visible' };
        }

        /**************************************
        ***        Alert Popup              ***
        **************************************/
        $scope.closeAlert = function (index) {
            $scope.alerts.splice(index, 1);
        };

        /**************************************
        ***        Cancel Edit           ***
        **************************************/
        $scope.cancel = function () {
            $scope.lot = [];
            $scope.isEdit = false;
            $scope.editForm.$setPristine();
            $scope.showEditForm = false;
        };

        /**************************************
        ***        Edit Lot                 ***
        **************************************/
        $scope.edit = function (id) {
            showButtons();
            $scope.lot = [];
            $scope.isEdit = true;

            $scope.showEditForm = true;

            
          
             var lFirstChange = true;

             if (id) {

                 $scope.lot = inventoryDataService.findLotById(id);
                 getLocations();
                 getMatTypes();
                var editWatch = $scope.$watchCollection('lot', function () {
                    if (!lFirstChange) {
                        $('#deleteButton').hide(400);
                        $scope.isEdit = true;

                    }
                    lFirstChange = false;
                    
                });
            }
        };


        /**************************************
        ***        Add Lot             ***
        **************************************/
        $scope.add = function () {
            showButtons();
            $scope.lot = {};
            $scope.isEdit = false;
            $scope.showEditForm = true;
            $('#deleteButton').hide();


        };

        /**************************************
        ***        Save Lot                 ***
        **************************************/
        $scope.saveLot = function () {
           // if ($scope.editForm.$invalid) return;
            if ($scope.isEdit) {
                inventoryDataService.updateLot($scope.lot)
                .then(function () {
                    $scope.alerts.push({ type: 'success', msg: 'Successfully updated lot: ' + $scope.lot.serialNo });
                    loadInventory();
                    $scope.showEditForm = false;
                },
                function () {
                    $scope.alerts.push({ type: 'danger', msg: 'Failed to edit lot: ' + $scope.lot.serialNo });
                })
            }

            if (!$scope.isEdit) {
                inventoryDataService.addLot($scope.lot)
                .then(function () {
                    $scope.alerts.push({ type: 'success', msg: 'Successfully added lot: ' + $scope.lot.serialNo });
                    loadInventory();
                    $scope.showEditForm = false;
                },
                function () {
                    $scope.alerts.push({ type: 'danger', msg: 'Failed to create lot: ' + $scope.lot.serialNo });
                })
            }
        }


        /**************************************
        ***        Load Inventory            ***
        **************************************/
        function  loadInventory() {
            $scope.loading = true;
            inventoryDataService.getLots()
            .then(function () {
                $scope.lots = inventoryDataService.lots;
                $scope.loading = false;
            },
                function () {
                    $scope.alerts.push({ type: 'danger', msg: 'Error Retrieving Inventory.'});
                    $scope.loading = false;
                })
                .then(function () {
                    $scope.isBusy = false;
                });
            $('.page-loading-overlay').addClass('loaded');
        };

        /**************************************
        ***        Get Locations            ***
        **************************************/
        function getLocations() {
          
            inventoryDataService.getLocations()
            .then(function () {
                $scope.locations = inventoryDataService.locations;
              },
                function () {
                    $scope.alerts.push({ type: 'danger', msg: 'Error Retrieving Locations.' });
                })
                .then(function () {
                    $scope.isBusy = false;
                });
        };

        /**************************************
        ***        Get Locations            ***
        **************************************/
        function getMatTypes() {

            inventoryDataService.getMatTypes()
            .then(function () {
                $scope.types = inventoryDataService.types;
            },
                function () {
                    $scope.alerts.push({ type: 'danger', msg: 'Error Retrieving Material Types.' });
                })
                .then(function () {
                    $scope.isBusy = false;
                });
        };



    
    }]);

