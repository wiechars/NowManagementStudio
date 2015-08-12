nmsApp.service('AlertService', function () {
    var activeAlert;
    this.setAlert = function (alert) {
        activeAlert = alert;
    };

    this.getAlert = function () {
        return activeAlert;
    }
});