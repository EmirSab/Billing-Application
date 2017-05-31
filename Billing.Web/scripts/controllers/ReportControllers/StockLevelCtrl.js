(function () {
    application.controller("StockLevelCtrl", ['$scope', '$anchorScroll', 'DataService', function ($scope, $anchorScroll, DataService) {
        $scope.showStockLevel = false;
        ListCategories();  

$scope.ShowStock=function(id){
                 DataService.read("stocklevel", id, function (data) { //ubaciti neki id za testiranje
                  $scope.stockLevelData = data              
             });
             $scope.showStockLevel = true;
};

        function ListCategories() {
            DataService.list("categories", function (data) {$scope.categories = data;
            //console.log(data);
        });
        };
    }]);
}());