(function () {
    application.controller("SalesByCustomerCtrl", ['$scope', '$anchorScroll', 'DataService', function ($scope, $anchorScroll, DataService) {
        $scope.showCustomer = false;
            $scope.requestData = {
            startDate: new Date(2016,1,1),
            endDate: new Date(2017,1,1)
        };

        $scope.save = function () {
            console.log("listing" + $scope.requestModel);
            DataService.insert("SalesByCustomer", $scope.requestData, function (data) {
                $scope.salesByCustomerData = data;
                $scope.showCustomer = true;
            });
        }
    }]);
}());