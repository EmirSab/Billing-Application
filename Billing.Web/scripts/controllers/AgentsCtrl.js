(function () {
    application.controller("AgentsCtrl", ['$scope', 'DataService', '$http', function ($scope, DataService, $http) {
        $scope.modalShown = false;
        //$scope.showAgent = false;
        ListAgents();
        

         $scope.add = function(town) {
            $scope.agent.towns.push(town);
        };

        $scope.remove = function(town, towns){
            var i = $scope.agent.towns.indexOf(town);
            $scope.agent.towns.splice(i, 1);
        };


        //Ovdje Typeahead
        var _selected;
        $scope.selected = (undefined);

        $scope.selectedTown = { id: 0, name: '', zip: 0, region: '' };
        $scope.getTowns = function(name) {
            return $http.get('http://localhost:59959/api/towns/' + name).then(function(response) {
                return response.data;
            });
        };

        $scope.ngModelOptionsSelected = function(value) {
            if (arguments.length) {
                _selected = value;
            } else {
                return _selected;
            }
        };

        $scope.modelOptions = {
            debounce: {
                default: 500,
                blur: 250
            },
            getterSetter: true
        };
        //Do ovog
        $scope.edit = function (currentAgent) {
            $scope.agent = currentAgent;
            console.log(currentAgent);
            $scope.modalShown = true;
            //$scope.showAgent = true;
        };
        $scope.save = function () {
            if ($scope.agent.id == 0) DataService.insert("agents", $scope.agent, function (data) {
                ListAgents();
            });
            else DataService.update("agents", $scope.agent.id, $scope.agent, function (data) {
                ListAgents();
            });
            $scope.modalShown = false;
        };

        $scope.delete = function(currentAgent){
            DataService.delete("agents", currentAgent.id, function(data){
                   swal({
                        title: "Are you sure?",
                        text: "You will not be able to recover this Agent!",
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "Yes, delete it!",
                        closeOnConfirm: false
                    },
                    function() {
                        ListAgents();
                        swal("Deleted!", "Agent has been deleted.", "success");
                    });
            });
        };
        $scope.new = function () {
            $scope.agent = {
                id: 0, 
                name: "",
                username: ""
            };
            $scope.modalShown = true;
            //$scope.showAgent = true;
        };
        $scope.submitform = function() {
             $scope.modalShown = false;
            if($scope.agent.id === 0){
                DataService.insert("agents", $scope.agent, function(data) {
                   ListAgents(); 
                });
            }
            else {
                DataService.update("agents", $scope.agent.id, $scope.agent, function(data) {
                   ListAgents(); 
                });
            }
        };

        function ListAgents() {
            DataService.list("agents", function (data) {
                $scope.agents = data
                console.log(data);
            });
        }
    }]);
}());