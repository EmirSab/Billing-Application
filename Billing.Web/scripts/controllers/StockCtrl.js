(function () {
    application.controller("StockCtrl", ['$scope', 'DataService', function ($scope, DataService) {
        $scope.showStock = false;
        ListStocks();
        ListProducts('');
        
        $scope.getStock = function(currentStock){
            $scope.stock = currentStock;
            $scope.showStock = true;
            //console.log(stock);
        };
         function ListStocks() {
             DataService.list("stock", function (data) {$scope.stock = data
                //console.log(data);
             });};

             function ListProducts(){
             DataService.list("products", function(data){ $scope.products = data 
                 //console.log(data);
                });};     
    }]);
}());
