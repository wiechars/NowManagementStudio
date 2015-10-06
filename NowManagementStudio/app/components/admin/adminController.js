/**************************************
***        Get Contact             ***
**************************************/
app.controller('adminController',
    ['$scope', 'adminDataService', '$location',
    function categoryController($scope, adminDataService) {
        $scope.users = [];
        //$scope.roles = [];
        $scope.rolesByUser = [];
        $scope.alerts = [];
        //$scope.message = "";
        //$scope.saveEditTitle = "Save/Edit Contact";
        // hideButtons();
        $scope.formTitle = "";
        $scope.hideEditForm = "";
        $scope.isEdit = false;
        hideEditForm();
        loadUserData();
       // loadUserRoles();



        //hub = $.connection.contactHub; // create a proxy to signalr hub on web server

        /**************************************
        ***        Hide Buttons              ***
        **************************************/
        //function hideButtons() {
        //    $scope.disableTagButton = { 'visibility': 'hidden' }; 
        //}

        /**************************************
        ***        Show Buttons              ***
        **************************************/
        //function showButtons() {
        //    $scope.disableTagButton = { 'visibility': 'visible' };
        //}

        /**************************************
        ***        Alert Popup              ***
        **************************************/
        $scope.closeAlert = function (index) {
            $scope.alerts.splice(index, 1);
        };

        /**************************************
        ***        Add User                 ***
        **************************************/
        $scope.newUser = function () {
            $scope.formTitle = "New User Information";
            showEditForm();
            $scope.isEdit = false;
            $scope.user = {};

        };

        /**************************************
        ***        Cancel User           ***
        **************************************/
        $scope.cancel = function () {
            $scope.user = [];
            hideEditForm();
            //When Cancelling reset form validation
            //$scope.contactForm.$setPristine();
            //lFirstChange = true;
            //hideButtons();



        };


        /**************************************
        ***        Edit user             ***
        **************************************/
        $scope.edit = function (userName) {
            //    showButtons();
            $scope.user = {};  //Pre Edit
            showEditForm();
            $scope.isEdit = true;
            //    $scope.isEdit = true;
            //    $scope.saveEditTitle = "Edit Contact";
            //    var lFirstChange = true;

            if (userName) {

                $scope.user = adminDataService.findUserByName(userName);
                $scope.formTitle = "Edit User Information : " + $scope.user.userName;

                adminDataService.getRolesByUser($scope.user)
                 .then(function () {
                     $scope.rolesByUser = adminDataService.rolesByUser;
                 },
                     function () {
                         $scope.alerts.push({ type: 'danger', msg: 'Error Retrieving User Security Groups.' });
                     })
                     .then(function () {
                         $scope.isBusy = false;
                     });
                //$scope.$watchCollection('user', function () {
                //    if (!lFirstChange) {
                //        $('#deleteButton').hide(400);
                //    }
                //    lFirstChange = false;
                //});
            }
        };

        /**************************************
        ***        Save User                ***
        **************************************/
        $scope.saveUser = function () {
            //if ($scope.contactForm.$invalid) return;

            if (!$scope.isEdit) {
                adminDataService.addUser($scope.user)
                .then(function () {

                    $scope.alerts.push({ type: 'success', msg: 'Successfully created user: ' + $scope.user.userName });
                    editUserRoles();
                },
                function () {
                    $scope.alerts.push({ type: 'danger', msg: 'Failed to create user: ' + $scope.user.userName });
                })
            } else {
                adminDataService.editUser($scope.user)
               .then(function () {

                   $scope.alerts.push({ type: 'success', msg: 'Successfully edited user: ' + $scope.user.userName });
                   editUserRoles();
               },
               function () {
                   $scope.alerts.push({ type: 'danger', msg: 'Failed to edit user: ' + $scope.user.userName });
               })
            }
        };

        /**************************************
       ***        Edit User Roles           ***
       **************************************/
        function editUserRoles() {
            adminDataService.editUserRoles($scope.rolesByUser,$scope.user.id)
            .then(function () {
               // $scope.alerts.push({ type: 'success', msg: 'Successfully edited user group membership'  });
            },
            function () {
                $scope.alerts.push({ type: 'danger', msg: 'Failed to edit group membership for user: ' + $scope.user.userName });
            })
        }





        /**************************************
        ***        Delete Contact          ***
        **************************************/
        //$scope.modalDelete = function (size, contact) {

        //    alert("modal");
        //    var modalInstance = $modal.open({
        //        templateUrl: 'app/contact/html/deleteModal.html',
        //        controller: function ($scope, $modalInstance, contact) {
        //            $scope.contact = contact;
        //            $scope.cancel = function () {
        //                $modalInstance.dismiss('cancel');
        //            };

        //            $scope.ok = function (contact) {
        //                contactDataService.deleteContact(contact.id)
        //                .then(function () {
        //                    $window.location = "#/mycontacts";
        //                    $modalInstance.close(contact);
        //                },
        //                function () {
        //                    //Error        
        //                })
        //            };
        //        },
        //        size: size,
        //        resolve: {
        //            contact: function () {
        //                return contact;
        //            }
        //        }
        //    });
        //};
        /**************************************
        ***        Get Users                ***
        **************************************/
        function loadUserData() {

            adminDataService.getUsers()
            .then(function () {
                $scope.users = adminDataService.users;
            },
                function () {
                    $scope.alerts.push({ type: 'danger', msg: 'Error Retrieving Users.' });
                })
                .then(function () {
                    $scope.isBusy = false;
                });

        };


        /**************************************
        ***        Get User Roles           ***
        **************************************/
        //function loadUserRoles() {

        //    adminDataService.getUserRoles()
        //    .then(function () {
        //        $scope.roles = adminDataService.roles;

        //    },
        //        function () {
        //            $scope.alerts.push({ type: 'danger', msg: 'Error Retrieving User Roles.' });
        //        })
        //        .then(function () {
        //            $scope.isBusy = false;
        //        });
        //};


        /**************************************
         ***  Signalr Push Updated Contact  ***
         **************************************/

        //hub.client.updateItem = function (contact) {
        //    var array = $scope.contacts;
        //    for (var i = array.length - 1; i >= 0; i--) {
        //        if (array[i].id === contact.Id) {
        //            array[i].name = contact.Name;
        //            array[i].email = contact.Email;
        //            array[i].mobile = contact.Mobile;
        //            $scope.$apply();
        //        }
        //    }
        //}

        /**************************************
         ***  Signalr Push Added Contact    ***
         **************************************/

        //    hub.client.addItem = function (contact) {
        //        alert(contact.Id);
        //        alert(contact.Name);
        //        $scope.contacts.push(contact);
        //        $scope.$apply();
        //    }

        //    // connect to signalr hub
        //    $.connection.hub.start()
        //       .done(function () { hub.server.subscribe("contact"); });

        //}]);

        /**************************************
        ***        Add Contact             ***
        **************************************/
        //app.controller('contactAddController',
        //    ['$scope', 'contactDataService', '$window',
        //    function categoryController($scope, contactDataService, $window) {
        //        $scope.contact = {};
        //        $scope.isEdit = false;


        //        $scope.saveContact = function () {

        //            if ($scope.contactForm.$invalid) return;
        //            contactDataService.addContact($scope.contact)
        //            .then(function () {

        //                $window.location = "#/mycontacts";
        //            },
        //            function () {
        //                //Error        
        //            })
        //        };

        /**************************************
        ***        Helper Methods           ***
        **************************************/
        function hideEditForm() {
            $scope.hideEditForm = { 'visibility': 'hidden' };
            //hideButtons();
        }

        function showEditForm() {
            $scope.hideEditForm = { 'visibility': 'visible' };
            //showButtons();
        }
    }]);

