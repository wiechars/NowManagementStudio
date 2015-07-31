/**************************************
***        Get Contact             ***
**************************************/
nmsApp.controller('contactController',
    ['$scope', 'contactDataService', '$location',
    function categoryController($scope, contactDataService) {
        $scope.contacts = [];


        
        loadContactData();

        function loadContactData() {
            contactDataService.getContacts()
            .then(function () {
                $scope.contacts = contactDataService.contacts;
             },
                function () {
                    alert("error");
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
nmsApp.controller('contactAddController',
    ['$scope', 'contactDataService', '$window',
    function categoryController($scope, contactDataService,$window) {
        $scope.contact = {};
        $scope.isEdit = false;

        $scope.cancel = function () {
            $window.location = "#/mycontacts";
        };

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
nmsApp.controller('contactEditController',
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