(function(){
    application.controller("AgentsRegionsCtrl", ['$scope', '$rootScope','$anchorScroll', 'DataService', function($scope, $rootScope,$anchorScroll, DataService){
        $scope.regionlist=[ "Banja Luka", "Bihac", "Doboj", "Mostar", "Sarajevo", "Trebinje", "Tuzla", "Zenica" ];
        $scope.save=function(){
            console.log("listing" + $scope.requestModel);
            DataService.insert("AgentsByRegion",$scope.request,
            function(data){
                $scope.CrossAgentRegion=data;
                 angular.forEach(data.agents[0].sales, function(value, key) {
                    if(key != "$id") {
                        average.push(value/8);
                    }
                });
                          });        
        };
        
        var vch = document.getElementById("salesChart");
        var average = new Array();
        $scope.show = false;
        
         $scope.showAgent = function(agent){
            var sales = new Array();
            var maxv = 0;
            angular.forEach(agent.sales, function(value, key) {
                if(key != "$id") sales.push(value);
                if(value>maxv) maxv = value;
            });
            maxv = 100000 * Math.ceil(maxv / 100000);
            var step = maxv / 5;
            var cht = new Chart(vch, {
                type: "bar",
                data: {
                    labels: BillingConfig.regions,
                    datasets: [
                        {
                            type: "line",
                            label: "average",
                            data: average,
                            //backgroundColor: 'rgba(255, 159, 64, 0.2)',
                            borderColor: 'rgba(255, 159, 64, 1)',
                            borderWidth: 1,
                            yAxisID: "avg"
                        },
                        {
                            label: "revenue",
                            data: sales,
                            backgroundColor: 'rgba(64, 159, 255, 0.2)',
                            borderColor: 'rgba(64, 159, 255, 1)',
                            borderWidth: 1,
                            yAxisID: "rev"
                        }]
                },
                options: {
                    title: { display: true, text: agent.name, padding:8, fontFamily:'Open Sans', fontSize:16 },
                    legend: { position: "none" },
                    scales: {
                        yAxes: [
                            { type: "linear", id: "avg", display:false, position:"right", ticks: { stepSize: step, min: 0, max: maxv } },
                            { type: "linear", id: "rev", display:true, position:"right", ticks: { stepSize: step, min: 0, max: maxv } }
                        ]
                    }
                }});
            $scope.show = true;
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
        }
      //date-time picker --- end
        angular.module("app", ["chart.js"]).controller("LineCtrl", function ($scope) {

  $scope.labels = ["January", "February", "March", "April", "May", "June", "July"];
  $scope.series = ['Series A', 'Series B'];
  $scope.data = [
    [65, 59, 80, 81, 56, 55, 40],
    [28, 48, 40, 19, 86, 27, 90]
  ];
  $scope.onClick = function (points, evt) {
    console.log(points, evt);
  };
  $scope.datasetOverride = [{ yAxisID: 'y-axis-1' }, { yAxisID: 'y-axis-2' }];
  $scope.options = {
    scales: {
      yAxes: [
        {
          id: 'y-axis-1',
          type: 'linear',
          display: true,
          position: 'left'
        },
        {
          id: 'y-axis-2',
          type: 'linear',
          display: true,
          position: 'right'
        }
      ]
    }
  };
});
              
        
    }]);
}());