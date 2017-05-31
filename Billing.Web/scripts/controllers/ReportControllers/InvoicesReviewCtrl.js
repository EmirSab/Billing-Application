  (function () {
     application.controller("InvoicesReviewCtrl", ['$scope', '$anchorScroll', 'DataService', function ($scope, $anchorScroll, DataService) {
         ListCustomers();
         $scope.showProducts = false;
         $scope.showInvoice = false;
        $scope.requestData = {
            startDate: new Date(2016,1,1),
            endDate: new Date(2017,1,1)
        };

         $scope.getInvoices = function(currentId) {
            $scope.requestData.id=currentId;
            DataService.insert("InvoicesReview", $scope.requestData, function(data) {
                $scope.invoices = data;
                $scope.showInvoice = true;
            });
        }

        $scope.getProducts = function (item) {                   
                DataService.read("invoicereview",item, function (data) { 
                    $scope.invoice = data;
                    $scope.showProducts = true;
                });
            }
        function ListCustomers(){
            DataService.list("customers", function(data){ $scope.customers = data});           
        }; 
    }]);
}());