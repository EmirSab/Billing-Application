(function() {

    //var app = angular.module("Billing");

    var DataService = function($http, $rootScope) {
        var source = BillingConfig.source;
        $http.defaults.headers.common.Token = credentials.token;
        $http.defaults.headers.common.ApiKey = BillingConfig.apiKey;

        return {
            promise: function(dataSet) {
                return $http.get(source + dataSet);
            },

            list: function(dataSet, callback) {
                $rootScope.message = "Please wait...";
                $http.get(source + dataSet)
                    .success(function (data, status, headers) {
                        $rootScope.message = "";
                        return callback(data);
                    })
                    .error(function (error) {
                        return callback(false);
                    });
            },

            read: function(dataSet, id, callback) {
                $http.get(source + dataSet + "/" + id)
                    .success(function(data) {
                        return callback(data);
                    })
                    .error(function(error) {
                        return callback(false);
                    });
            },

            insert: function(dataSet, data, callback) {
                $http({ method:"post", url:source + dataSet, data:data })
                    .success(function(data) {
                        return callback(data);
                    })
                    .error(function(error){
                        return callback(false);
                    });
            },

            update: function(dataSet, id, data, callback) {
                $http({ method:"put", url:source + dataSet + "/" + id, data: data })
                    .success(function(data) {
                        return callback(data);
                    })
                    .error(function(error){
                        return callback(false);
                    });
            },

            //function for download pdf
             download: function(id) {
                    $http.get(source + '/invoices/download/' + id, { responseType: 'arraybuffer' })
                        .success(function (data) {
                            var blob = new Blob([data], {
                                type: "application/pdf"
                            });
                            //saveAs provided by FileSaver.min.js
                            saveAs(blob, "Invoice-" + id + "-" + (Math.floor(new Date()).toString()) + '.pdf');
                        });
                },

            delete: function(dataSet, id, callback) {
                $http({ method:"delete", url:source + dataSet + "/" + id })
                    .success(function() {
                        return callback(true);
                    })
                    .error(function(error){
                        return callback(false);
                    });
            }
        };
    };

    application.factory("DataService", DataService);

}());
