(function() {

    application.controller("LoginCtrl", ['$scope', '$rootScope', '$http', '$location', 'LoginService', 'localStorageService',
        function($scope, $rootScope, $http, $location, LoginService, localStorageService) {
            $http.get("config.json").then(
                function(response){
                    BillingConfig = response.data;
                    $scope.debug = BillingConfig.debugMode;
                    var remToken = localStorageService.get('MistralBilling');
                    if(remToken != null){
                        var promise = $http({
                            method: "post",
                            url: BillingConfig.source + "remember",
                            data: {
                                "apiKey": BillingConfig.apiKey,
                                "signature": BillingConfig.signature,
                                "remember": remToken
                            }
                        });
                        promise.then(
                            function(response){
                                credentials = response.data;
                                var expireDate = (new Date());
                                expireDate.setDate(expireDate.getDate() + 30);
                                localStorageService.set('MistralBilling', credentials.remember, { 'expired': expireDate });
                                $rootScope.currentUser = credentials.currentUser.name;
                                $location.path(redirectTo);
                                $rootScope.showDashboard = false; 
                            },
                            function(reason){
                                console.log(reason);
                            })
                    }
                },  function(reason){
                    console.log(reason);
                });

            $scope.loginAs = function(username){
                $scope.user = { name : username, pass : "billing", remember: true };
                $scope.login();
                $rootScope.showDashboard = false;             
            };
            
            $rootScope.showDashboard = true;
            $scope.login = function() {
                console.log($scope.user);
                var userData = LoginService.encode($scope.user.name + ":" + $scope.user.pass);
                $http.defaults.headers.common.Authorization = "Basic " + userData;
                var promise = $http({
                    method: "post",
                    url: BillingConfig.source + "login",
                    data: {
                        "apiKey": BillingConfig.apiKey,
                        "signature": BillingConfig.signature,
                        "remember": $scope.user.remember
                    }
                });
                promise.then(
                    function(response) {
                        credentials = response.data;
                        if ($scope.user.remember){
                            var expireDate = (new Date());
                            expireDate.setDate(expireDate.getDate() + 30);
                            localStorageService.set('MistralBilling', credentials.remember, { 'expired': expireDate });
                        }
                        $rootScope.currentUser = credentials.currentUser.name;
                        $location.path(redirectTo);
                        $rootScope.showDashboard = false;
                    },
                    function(reason){
                        credentials.currentUser.id = 0;
                        $location.path("/login");
                    });
            };
        }
    ]);

    application.controller("LogoutCtrl", ['$http', 'localStorageService', function($http, localStorageService) {

        var request = $http({
            method: "get",
            url: BillingConfig.source + "logout",
            async: false
        });
        request.then(
            function (response) {
                localStorageService.clearAll("MistralBilling");
                window.location.reload();
                return true;
            },
            function (reason) {
                return false;
            });
    }]);
}());