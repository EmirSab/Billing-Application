(function(){
    application.controller("CustomerSalesCtrl", ['$scope', '$rootScope','$anchorScroll', 'DataService', function($scope, $rootScope,$anchorScroll, DataService){
        show();
        function show(){
        $scope.pageNo=1;
        $scope.request={
            id: credentials.currentUser.id,
            startDate: new Date(),
            endDate: new Date()
        };
         $scope.request.startDate.setFullYear($scope.request.startDate.getFullYear() - 1);
            if(credentials.currentUser.roles[0]=="admin")
            {
            $scope.request.id=0;        
            };
            ListAll(0);
        };
        $scope.save=function(){
            $scope.pageNo=1;   
            ListAll(0);
        };
        
           $scope.goto = function(page){
            if(page<1)page=1;
            if(page>$scope.totalPages) page=$scope.totalPages;
            $scope.pageNo=page;
            ListAll(page-1);
}
        function ListAll(page){
            DataService.insert("SalesByCustomer?page="+page,$scope.request, function(data){
                $scope.CustomerSales=data.report;
                $scope.totalPages=data.totalPages;
                $scope.currentPage=data.currentPage;
            });
        };
        //date-time picker --- start
        $scope.today = function() {
          $scope.procurement.date = new Date();
        };

        $scope.clear = function() {
          $scope.procurement.date = null;
        };

        $scope.inlineOptions = {
          customClass: getDayClass,
          minDate: new Date(),
          showWeeks: true
        };

        $scope.dateOptions = {
          dateDisabled: disabled,
          formatYear: 'yy',
          maxDate: new Date(2020, 5, 22),
          minDate: new Date(),
          startingDay: 1
        };

        // Disable weekend selection
        function disabled(data) {
          var date = data.date,
            mode = data.mode;
          return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
        }

        $scope.toggleMin = function() {
          $scope.inlineOptions.minDate = $scope.inlineOptions.minDate ? null : new Date();
          $scope.dateOptions.minDate = $scope.inlineOptions.minDate;
        };

        $scope.toggleMin();

        $scope.open2 = function() {
          $scope.popup2.opened = true;
        };
        $scope.open3 = function() {
          $scope.popup3.opened = true;
        };
        $scope.setDate = function(year, month, day) {
          $scope.procurement.date = new Date(year, month, day);
        };

        $scope.format = 'dd-MMMM-yyyy'
        
        $scope.popup2 = {
          opened: false
        };
        $scope.popup3 = {
          opened: false
        };

        var tomorrow = new Date();
        tomorrow.setDate(tomorrow.getDate() + 1);
        var afterTomorrow = new Date();
        afterTomorrow.setDate(tomorrow.getDate() + 1);
        $scope.events = [
          {
            date: tomorrow,
            status: 'full'
          },
          {
            date: afterTomorrow,
            status: 'partially'
          }
        ];

        function getDayClass(data) {
          var date = data.date,
            mode = data.mode;
          if (mode === 'day') {
            var dayToCheck = new Date(date).setHours(0,0,0,0);

            for (var i = 0; i < $scope.events.length; i++) {
              var currentDay = new Date($scope.events[i].date).setHours(0,0,0,0);

              if (dayToCheck === currentDay) {
                return $scope.events[i].status;
              }
            }
          }

          return '';
        };
      //date-time picker --- end
    }]);
}());