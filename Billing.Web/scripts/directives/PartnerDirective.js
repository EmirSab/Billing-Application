(function() {

    application.directive("partner", [function(){
        return {
            restrict: 'E',
            scope: { partner: '=partnerData'},
            templateUrl: 'views/partner.html',
            template: '<h4>{{ partner.name }} [{{partner.id}}]</h4><div>{{partner.town}} :: {{partner.address}}</div>'
        }
    }]);
}());
