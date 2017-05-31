(function(){
    application.controller("CustomersCtrl", ['$scope','$rootScope','$anchorScroll', 'DataService',  function($scope,$rootScope,$anchorScroll ,DataService) {
        
        $scope.modalShown = false;
        
        //$scope.showCustomer = false;
        ListCustomers();
        ListTowns();
        
        $scope.edit = function(currentCustomer){
            $scope.customer = currentCustomer;
            $scope.modalShown = true;
            $anchorScroll();
            //$scope.showCustomer = true;
        };
        $scope.save = function(){
            if($scope.customer.id == 0)
                DataService.insert("customers", $scope.customer, function(data){ ListCustomers();} );
            else
                DataService.update("customers", $scope.customer.id, $scope.customer, function(data){ListCustomers();});
            $scope.modalShown = false;
        };
        $scope.delete = function (currentCustomer) {
          DataService.delete("customers", currentCustomer.id, function (data) {
                    swal({
                        title: "Are you sure?",
                        text: "You will not be able to recover this Customer!",
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "Yes, delete it!",
                        closeOnConfirm: false
                    },
                    function() {
                        ListCustomers();
                        swal("Deleted!", "Customer has been deleted.", "success");
                    });
          });
        };
        $scope.new = function(){
            console.log("adding customer");
            $scope.customer = {
                    id: 0,
                    name: "",
                    address: "",
                    town: "",
            };
             document.getElementById('townsel').style.visibility = 'hidden';
                $scope.modalShown = true;
            
            //$scope.showCustomer = true;
        };
        function ListCustomers(){
            DataService.list("customers", function(data){ $scope.customers = data});
        } 
        
        function ListTowns(name){
                DataService.list("towns/" + name, function(data){
                    $scope.towns = data;
                });
            }
         $scope.textUp = function(keyEvent){
                if(keyEvent.key == "ArrowDown") document.getElementById('townsel').focus();
            };
            
         $scope.townSelected = function(keyEvent){
                if(keyEvent.key == "Enter") {
                    for(var i=0; i<$scope.towns.length; i++){
                        if($scope.towns[i].id === $scope.customer.townId){
                            $scope.customer.town = $scope.towns[i].name;
                            document.getElementById('townsel').style.visibility = 'hidden';
                            break;
                        }
                    }
                }
            };
          $scope.autocomplete = function(autoStr){
                if (autoStr.length >= 3){
                    ListTowns(autoStr);
                    document.getElementById('townsel').style.visibility = 'visible';
                    document.getElementById('townsel').size = 4;
                }
                else {
                    document.getElementById('townsel').style.visibility = 'hidden';
                }
            };   
    }]);
}());
