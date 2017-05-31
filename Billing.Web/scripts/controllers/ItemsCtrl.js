(function(){
    application.controller("ItemsCtrl", ['$scope', 'DataService', function($scope, DataService){
        $scope.showItem = false;
        ListItems();
        
        //get all items function
        $scope.getItem = function(currentItem){
          $scope.item = currentItem;
          $scope.showItem = true;
        };
        
        // post and put functions
        $scope.save = function(){
            if($scope.item.id == 0)
                DataService.insert("items", $scope.item, function(data){ListItems();});
            else
                DataService.update("items", $scope.item.id, $scope.item, function(data){ListItems();});
        };
        
        // Delete item
        $scope.delete = function(currentItem){
          console.log(currentItem.id);
          DataService.delete("items", currentItem.id, function(data){
              ListItems();
          });
        };
        
        $scope.new = function(){
          $scope.item = {
              id: 0,
              quantity: 0,
              price: 0,
              invoice: "",
              product: "",
              unit: "",
              subTotal: 0,
              invoiceId: 0,
              productId: 0
          };  
            $scope.showItem = true;
        };
        function ListItems(){
            DataService.list("items", function(data){$scope.items = data});
        }
    }]);
}());