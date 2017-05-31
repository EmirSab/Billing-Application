(function() {
    application.directive('adminView', [function() {
        return {
            restrict: 'A',
            link: function(scope, elem, attr) {
                if (credentials.currentUser.roles.indexOf("admin") > -1)
                    elem.css("display", "table-cell");
                else
                    elem.css("display", "none");
            }
        };
    }]);
}());