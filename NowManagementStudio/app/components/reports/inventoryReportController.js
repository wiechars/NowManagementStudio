/// <reference path="angular.js" />
app.controller('inventoryReportController',
    ['$scope', 'reportDataService', '$http', '$q',
function inventoryReportController($scope, reportDataService, $http, $q) {

    init();


    function init() {
        $scope.lot = [];
        hideReportRender();
        $scope.alerts = [];
    }


    $scope.generateReport = function () {
        hideReportRender();
        $scope.alerts = [];
        var lotID = ($('#serial-no').val());
        if (lotID == '' || lotID == null) {
            $scope.alerts.push({ type: 'danger', msg: 'Please Selected an ID Tag' });
        } else {
            var dt = new Date($.now());
            $scope.timeGenerated = dt.getDate() + "/"
                    + (dt.getMonth() + 1) + "/"
                    + dt.getFullYear() + "  "
                    + dt.getHours() + ":"
                    + dt.getMinutes();


            reportDataService.getLot(lotID)
            .then(function () {
                $scope.lot = reportDataService.lot;
                showReportRender();
            },
                function () {
                    //$scope.alerts.push({ type: 'danger', msg: 'Error Retrieving Material Types.' });
                })
                .then(function () {
                    $scope.isBusy = false;
                });
        }
    };

    $scope.reportPDF = function reportPDF() {
        var deferred = $q.defer();
        $http.get('/api/Reports/InventoryReport/' + $scope.lot.id).success(function (results) {
            window.open('/api/Reports/InventoryReport/' + $scope.lot.id, '_self', '');
            deferred.resolve(results);
        }).error(function (data, status, headers, config) {
            deferred.reject('Failed generate pdf');
        });

        return deferred.promise;
    };

    $scope.generatePDF = function () {

        html2canvas($("#reportRender"), {
            onrendered: function (canvas) {
                var imgData = canvas.toDataURL(
                    'image/png');
                var doc = new jsPDF('landscape', 'pt', 'letter');
                doc.addImage(imgData, 'PNG', 10, 10);
                doc.save('sample-file.pdf');
            }
        });


        //var pdf = new jsPDF('portrait', 'pt', 'letter');
        //source = $('#reportRender')[0];


        //// we support special element handlers. Register them with jQuery-style 
        //// ID selector for either ID or node name. ("#iAmID", "div", "span" etc.)
        //// There is no support for any other type of selectors 
        //// (class, of compound) at this time.
        //specialelementhandlers = {
        //    // element with id of "bypass" - jquery style selector
        //    '#bypassme': function (element, renderer) {
        //        // true = "handled elsewhere, bypass text extraction"
        //        return true
        //    }
        //};
        //margins = {
        //    top: 30,
        //    bottom: 30,
        //    left: 30,
        //    width: '100%'
        //};
        //// all coords and widths are in jsPDF instance's declared units
        //// 'inches' in this case
        //pdf.fromHTML(
        //source, // HTML string or DOM elem ref.
        //margins.left, // x coord
        //margins.top, { // y coord
        //    'width': margins.width, // max width of content on PDF
        //    'elementHandlers': specialElementHandlers
        //},
        //function (dispose) {
        //    // dispose: object with X, Y of the last line add to the PDF 
        //    //          this allow the insertion of new lines after html
        //    pdf.save('InventoryReport.pdf');
        //}, margins);



        var specialElementHandlers =
                                    function (element, renderer) {
                                        return true;
                                    }
        doc.fromHTML($('#reportRender').html(), 15, 15, {
            'width': 170,
            'elementHandlers': specialElementHandlers
        });
        //doc.save('sample-file.pdf');

    };

    function hideReportRender() {
        $scope.hideReportRender = { 'visibility': 'hidden' };
    }

    function showReportRender() {
        $scope.hideReportRender = { 'visibility': 'visible' };

    }

}]);



