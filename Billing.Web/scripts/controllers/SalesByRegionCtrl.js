(function () {
    application.controller("SalesByRegionCtrl", ['$scope', '$anchorScroll', 'DataService', function ($scope, $anchorScroll, DataService) {
        $scope.showRegion = false;
            $scope.requestData = {
            startDate: new Date(2016,1,1),
            endDate: new Date(2017,1,1)
        };

        $scope.save = function () {
            console.log("listing" + $scope.requestModel);
            DataService.insert("SalesByRegion", $scope.requestData, function (data) {
                $scope.salesByRegionData = data;
                $scope.showRegion = true;
            });
        }

         $scope.getAgents = function (currentAgent) {

            $scope.currentAgentName = currentAgent.agentName;

            var promise = DataService.promise("agents/" + currentAgent.agentName);
                promise.then(
                function(response){
                    $scope.requestData.id = response.data[0].id;
                    // $scope.isidtrue = response.data[0].id;
                    DataService.insert("SalesByAgent", $scope.requestData, function (data){
                        $scope.salesByAgentData = data
                    });                   
                },
                function(reason){}
            )
        }
    }]);
}());