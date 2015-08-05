/**************************************
***        Get Contact             ***
**************************************/
app.controller('contactController',
    ['$scope', 'contactDataService', '$location',
    function categoryController($scope, contactDataService) {
        $scope.contacts = [];
        $scope.alerts = [];
        $scope.message = "";

        loadContactData();

        /**************************************
        ***        Alert Popup              ***
        **************************************/
        $scope.closeAlert = function (index) {
            $scope.alerts.splice(index, 1);
        };

        /**************************************
        ***        Add Contact             ***
        **************************************/
        $scope.add = function () {
            $scope.contact = {};
            $scope.isEdit = false;
       
        };

        /**************************************
        ***        Cancel Contact           ***
        **************************************/
        $scope.cancel = function () {
            $scope.contact = [];

        };


        /**************************************
        ***        Edit Contact             ***
        **************************************/
        $scope.edit = function (id) {
           
            $scope.contact = {};
            $scope.isEdit = true;
            var lFirstChange = true;

            if (id) {
                
               $scope.contact = contactDataService.findContactById(id);
               $scope.$watchCollection('contact', function () {
                    if (!lFirstChange) {
                        $('#deleteButton').hide(400);
                    }
                    lFirstChange = false;
                });
            }
        };

        /**************************************
        ***        Save Contact      ***
        **************************************/
        $scope.saveContact = function () {
            if ($scope.contactForm.$invalid) return;
            if ($scope.isEdit) {
                contactDataService.updateContact($scope.contact)
                .then(function () {
                    $scope.alerts.push({ type: 'success', msg: 'Successfully updated contact: ' + $scope.contact.name });
                },
                function () {
                    $scope.alerts.push({ type: 'danger', msg: 'Failed to edit contact: ' + $scope.contact.name });
                })
            }

            if (!$scope.isEdit) {
                contactDataService.addContact($scope.contact)
                .then(function () {
                    //Push to scope so it appears in the table without having to reload gird
                    $scope.contacts.push($scope.contact);
                    $scope.alerts.push({ type: 'success', msg: 'Successfully added contact: ' + $scope.contact.name });
                },
                function () {
                    $scope.alerts.push({ type: 'danger', msg: 'Failed to create contact: ' + $scope.contact.name });
                })
            }
        }


        /**************************************
        ***        Delete Contact          ***
        **************************************/
        $scope.modalDelete = function (size, contact) {

            alert("modal");
            var modalInstance = $modal.open({
                templateUrl: 'app/contact/html/deleteModal.html',
                controller: function ($scope, $modalInstance, contact) {
                    $scope.contact = contact;
                    $scope.cancel = function () {
                        $modalInstance.dismiss('cancel');
                    };

                    $scope.ok = function (contact) {
                        contactDataService.deleteContact(contact.id)
                        .then(function () {
                            $window.location = "#/mycontacts";
                            $modalInstance.close(contact);
                        },
                        function () {
                            //Error        
                        })
                    };
                },
                size: size,
                resolve: {
                    contact: function () {
                        return contact;
                    }
                }
            });
        };
        /**************************************
        ***        Get Contact             ***
        **************************************/
        function loadContactData() {
            contactDataService.getContacts()
            .then(function () {
                $scope.contacts = contactDataService.contacts;
               

             },
                function () {
                    $scope.alerts.push({ type: 'danger', msg: 'Error Retrieving Contacts.' });
                    //Error goes here...
                })
                .then(function () {
                    $scope.isBusy = false;
                });
        };

    }]);

/**************************************
***        Add Contact             ***
**************************************/
app.controller('contactAddController',
    ['$scope', 'contactDataService', '$window',
    function categoryController($scope, contactDataService,$window) {
        $scope.contact = {};
        $scope.isEdit = false;


        $scope.saveContact = function () {
            
            if ($scope.contactForm.$invalid) return;
            contactDataService.addContact($scope.contact)
            .then(function () {
                
                $window.location = "#/mycontacts";
            },
            function () {
                //Error        
            })
        };
    }]);

/**************************************
***        Edit Contact             ***
**************************************/
app.controller('contactEditController',
    ['$scope', 'contactDataService', '$window','$routeParams','$modal',
    function categoryController($scope, contactDataService, $window,$routeParams,$modal) {
        $scope.contact = {};
        $scope.isEdit = true;

        var lFirstChange = true;

        if ($routeParams.id) {
            $scope.contact = contactDataService.findContactById($routeParams.id);
            $scope.$watchCollection('contact', function () {
                if (!lFirstChange) {
                    $('#deleteButton').hide(400);
                }
                lFirstChange = false;
            });
        }

        $scope.cancel = function () {
            $window.location = "#/mycontacts";
        };

        $scope.modalDelete = function (size, contact) {

            var modalInstance = $modal.open({
                templateUrl: 'app/contact/html/deleteModal.html',
                controller: function ($scope, $modalInstance,contact) {
                    $scope.contact = contact;
                    $scope.cancel = function () {
                        $modalInstance.dismiss('cancel');
                    };

                    $scope.ok = function (contact) {
                        contactDataService.deleteContact(contact.id)
                        .then(function () {
                            $window.location = "#/mycontacts";
                            $modalInstance.close(contact);
                        },
                        function () {
                            //Error        
                        })
                    };
                },
                size: size,
                resolve: {
                    contact: function () {
                        return contact;
                    }
                }
            });
        };

        $scope.saveContact = function () {
            if ($scope.contactForm.$invalid) return;
            contactDataService.updateContact($scope.contact)
            .then(function () {
                
                $window.location = "#/mycontacts";
            },
            function () {
                //Error        
            })
            
        };
    }]);