(function() {
    application.directive('isFromAgent', [function() {
        return {
            priority: 10,
            restrict: 'A',
            scope: { param: '@' },
            link: function(scope, element, attr) {
                var name = scope.param;
                if (name === credentials.currentUser.name) //we use name not username for adminview
                    element.css("display", "table-cell");               
            }
        };
    }]);
}());