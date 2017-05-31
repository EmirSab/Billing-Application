(function () {
    application.controller("ShippersCtrl", ['$scope', 'DataService', function ($scope, DataService) {
        //$scope.showShipper = false;
        $scope.modalShown = false;
        getTowns('');
        ListShippers();
        
        $scope.edit = function (current) {
            $scope.shipper = current;
            //$scope.showShipper = true;
            $scope.modalShown = true;
        };
        $scope.save = function () {
            if ($scope.shipper.id == 0) DataService.insert("shippers", $scope.shipper, function (data) {
                ListShippers();
            });
            else DataService.update("shippers", $scope.shipper.id, $scope.shipper, function (data) {
                ListShippers();
            });
            $scope.modalShown = false;
        };
        $scope.delete = function (current) {
            DataService.delete("shippers", current.id, function (data) {
                swal({
                        title: "Are you sure?",
                        text: "You will not be able to recover this Shipper!",
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "Yes, delete it!",
                        closeOnConfirm: false
                    },
                    function() {
                        ListShippers();
                        swal("Deleted!", "Shipper has been deleted.", "success");
                    });
            });
        };
        $scope.new = function () {
            $scope.shipper = {
                id: 0
                , name: ""
                , address: ""
                , town: ""
            };
            //$scope.showShipper = true;
            $scope.modalShown = true;
        };

        function getTowns(name){
                DataService.list("towns/" + name, function(data){
                    $scope.towns = data;
                });
        }

        function ListShippers() {
            DataService.list("shippers", function (data) {
                $scope.shippers = data
            });
        }
    }]);
}());