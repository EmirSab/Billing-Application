(function () {
    application.controller("SalesByCustomerCategoryCtrl", ['$scope', '$anchorScroll', 'DataService', function ($scope, $anchorScroll, DataService) {
        $scope.showCustomerCategory = false;
                 $scope.requestData = {
            startDate: new Date(2016,1,1),
            endDate: new Date(2017,1,1)
        };
        $scope.save = function () {
            console.log("listing" + $scope.requestModel);
            DataService.insert("CustomersByCategory", $scope.requestData, function (data) {
                $scope.salesByCustomerCategorydata = data;
                //console.log(data);
                $scope.showCustomerCategory = true;
            });
        }
    }]);
}());
