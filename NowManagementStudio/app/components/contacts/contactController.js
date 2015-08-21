/**************************************
***        Get Contact             ***
**************************************/
app.controller('contactController',
    ['$scope', 'contactDataService', '$location',
    function categoryController($scope, contactDataService) {
        $scope.contacts = [];
        $scope.alerts = [];
        $scope.message = "";
        $scope.saveEditTitle = "Save/Edit Contact";
        hideButtons();
        loadContactData();

        hub = $.connection.contactHub; // create a proxy to signalr hub on web server

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
        ***        Add Contact             ***
        **************************************/
        $scope.add = function () {
            $scope.saveEditTitle = "Add Contact";
            showButtons();
            $scope.contact = {};
            $scope.isEdit = false;

        };

        /**************************************
        ***        Cancel Contact           ***
        **************************************/
        $scope.cancel = function () {
            $scope.contact = [];
            $scope.isEdit = false;
            //When Cancelling reset form validation
            $scope.contactForm.$setPristine();
            lFirstChange = true;
            hideButtons();
          


        };


        /**************************************
        ***        Edit Contact             ***
        **************************************/
        $scope.edit = function (id) {
            showButtons();
            $scope.contact = {};  //Pre Edit
            $scope.contactEdit = {}; //One that will be edited.
            $scope.isEdit = true;
            $scope.saveEditTitle = "Edit Contact";
            var lFirstChange = true;

            if (id) {

                $scope.contact = contactDataService.findContactById(id);
                $scope.contactEdit = $scope.contact;
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
                contactDataService.updateContact($scope.contactEdit)
                .then(function () {
                    $scope.alerts.push({ type: 'success', msg: 'Successfully updated contact: ' + $scope.contactEdit.name });
                },
                function () {
                    $scope.alerts.push({ type: 'danger', msg: 'Failed to edit contact: ' + $scope.contact.name });
                })
            }

            if (!$scope.isEdit) {
                contactDataService.addContact($scope.contactEdit)
                .then(function () {
                     $scope.alerts.push({ type: 'success', msg: 'Successfully added contact: ' + $scope.contactEdit.name });
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
            $scope.loading = true;
            contactDataService.getContacts()
            .then(function () {
                $scope.contacts = contactDataService.contacts;
               
                $scope.loading = false;

            },
                function () {
                    $scope.alerts.push({ type: 'danger', msg: 'Error Retrieving Contacts.'});
                    $scope.loading = false;
                })
                .then(function () {
                    $scope.isBusy = false;
                });
            
        };

        /**************************************
         ***  Signalr Push Updated Contact  ***
         **************************************/

        hub.client.updateItem = function (contact) {
            var array = $scope.contacts;
            for (var i = array.length - 1; i >= 0; i--) {
                if (array[i].id === contact.Id) {
                    array[i].name = contact.Name;
                    array[i].email = contact.Email;
                    array[i].mobile = contact.Mobile;
                    $scope.$apply();
                }
            }
        }

        /**************************************
         ***  Signalr Push Added Contact    ***
         **************************************/

        hub.client.addItem = function (contact) {
            alert(contact.Id);
            alert(contact.Name);
            $scope.contacts.push(contact);
            $scope.$apply();
        }

        // connect to signalr hub
        $.connection.hub.start()
           .done(function () { hub.server.subscribe("contact"); });

    }]);

/**************************************
***        Add Contact             ***
**************************************/
app.controller('contactAddController',
    ['$scope', 'contactDataService', '$window',
    function categoryController($scope, contactDataService, $window) {
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
    ['$scope', 'contactDataService', '$window', '$routeParams', '$modal',
    function categoryController($scope, contactDataService, $window, $routeParams, $modal) {
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
            //reset validation
            $scope.contactForm.$setUntouched();
            $scope.isEdit = false;
            $scope.contact = [];
           // $window.location = "#/mycontacts";
        };

        $scope.modalDelete = function (size, contact) {

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