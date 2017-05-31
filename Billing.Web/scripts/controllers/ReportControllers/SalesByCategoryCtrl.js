
  (function () {
     application.controller("SalesByCategoryCtrl", ['$scope', '$anchorScroll', 'DataService', function ($scope, $anchorScroll, DataService) {
         $scope.showCategory = false;
         $scope.requestData = {
            startDate: new Date(2016,1,1),
            endDate: new Date(2017,1,1)
        };

         $scope.save = function () {
             console.log("listing" + $scope.requestModel);
          DataService.insert("SalesByCategory", $scope.requestData, function (data) {
                 $scope.salesByCategoryData = data;
                 $scope.showCategory = true;
             });
          }

          $scope.getProduct = function (currentCategory) {

            $scope.currentCategoryName = currentCategory.categoryName;
            var promise = DataService.promise("categories/" + currentCategory.categoryName);
                promise.then(
                function(response){
                    $scope.requestData.id = response.data[0].id;
                    DataService.insert("SalesByProduct", $scope.requestData, function (data){
                        $scope.salesByCategoryData = data
                    });                   
                },
                function(reason){}
            )
        }
    }]);
}());

  