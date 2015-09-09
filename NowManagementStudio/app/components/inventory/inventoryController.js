/**************************************
***        Get Contact             ***
**************************************/
app.controller('inventoryController',
    ['$scope', 'inventoryDataService', '$location',
    function categoryController($scope, inventoryDataService) {
        $scope.lots = [];
        $scope.images = [];
        $scope.locations = [];
        $scope.locationSelected = [];
        $scope.types = [];
        $scope.typeSelected = [];
        $scope.alerts = [];
        $scope.message = "";
        $scope.hideEditForm = "";
        $scope.formTitle = "";
        loadInventory();
        getLocations();
        getMatTypes();
        hideEditForm();
        

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
            hideEditForm();
        };

        /**************************************
        ***        Edit Lot                 ***
        **************************************/
        $scope.edit = function (id) {
            $scope.alerts = [];
            $scope.formTitle = "Edit Inventory Item";
            showButtons();
            $scope.lot = [];
            $scope.images = [];
            $scope.isEdit = true;
            showEditForm();
            var lFirstChange = true;

            if (id) {

                $scope.lot = inventoryDataService.findLotById(id);
                $scope.formTitle = $scope.formTitle + ": " + $scope.lot.serialNo;
                inventoryDataService.findImagesByLotId($scope.lot)

                    .then(function () {
                        $scope.images = inventoryDataService.images;

                    });
                setSelectedOptions();
                $scope.$watchCollection('lot', function () {
                    if (!lFirstChange) {
                        $('#deleteButton').hide(400);
                    } else {
                        $('#deleteButton').show();
                    }
                    lFirstChange = false;

                });
            }
        };


        /**************************************
        ***        Add Lot             ***
        **************************************/
        $scope.add = function () {
            $scope.alerts = [];
            $scope.formTitle = "Add Inventory Item:"
            $scope.isEdit = false;
            $scope.images = [];
            showButtons();
            $scope.lot = [];
            setSelectedOptions();
            $scope.lot = { price: 0, weight: 0, volume: 0, height: 0, width: 0, lastInvDate:'',nextInvDate:'',expirationDate:'',purchaseDate:'' }
            
            showEditForm();
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
                uploadImages();
                $scope.alerts.push({ type: 'success', msg: 'Successfully updated lot: ' + $scope.lot.serialNo });
                hideEditForm();
            },
            function () {
                $scope.alerts.push({ type: 'danger', msg: 'Failed to edit lot: ' + $scope.lot.serialNo });
            })
            }

            if (!$scope.isEdit) {
                inventoryDataService.addLot($scope.lot)
              .then(function () {
                  $scope.lot.id = inventoryDataService.getNewLotId();
                  uploadImages();

                  $scope.alerts.push({ type: 'success', msg: 'Successfully added lot: ' + $scope.lot.serialNo });
                  hideEditForm();
                  $scope.lots.push($scope.lot);

              },
              function () {
                  $scope.alerts.push({ type: 'danger', msg: 'Failed to create lot: ' + $scope.lot.serialNo });
              })
            }
        }


        /**************************************
        ***        Load Inventory            ***
        **************************************/
        function loadInventory() {
            $scope.loading = true;
            inventoryDataService.getLots()
            .then(function () {
                $scope.lots = inventoryDataService.lots;
                $scope.loading = false;
            },
                function () {
                    $scope.alerts.push({ type: 'danger', msg: 'Error Retrieving Inventory.' });
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

        /**************************************
        ***        Update Location Select   ***
        **************************************/
        $scope.updateLocation = function () {
            $scope.lot.locationId = $scope.locationSelected.id;
            $scope.lot.location = $scope.locationSelected.location;
        };

        /**************************************
        ***        Update Category Select   ***
        **************************************/
        $scope.updateType = function () {
            $scope.lot.typeId = $scope.typeSelected.id;
            $scope.lot.type = $scope.typeSelected.type;
        };

        /**************************************
        ***        Update Category Select   ***
        **************************************/
        function setSelectedOptions() {
            $scope.typeSelected = lookup($scope.types, 'id', parseInt($scope.lot.typeId));
            $scope.locationSelected = lookup($scope.locations, 'id', parseInt($scope.lot.locationId));
        };

        /**************************************
          ***   Function to parse an array  ***
         **************************************/
        function lookup(array, prop, value) {
            for (var i = 0, len = array.length; i < len; i++)
                if (array[i][prop] === value) return array[i];
        };


        /*************************************
        ***     Upload Images              ***
        *************************************/
        function uploadImages() {
            var dz = Dropzone.forElement("#dropzone");
              dz.options.url = "/api/Files/UploadInventory/" + $scope.lot.id;
            dz.processQueue();
        };

        function hideEditForm() {
            $scope.hideEditForm = { 'visibility': 'hidden' };
            hideButtons();
        }

        function showEditForm() {
            $scope.hideEditForm = { 'visibility': 'visible' };
            showButtons();
        }

    }]);

