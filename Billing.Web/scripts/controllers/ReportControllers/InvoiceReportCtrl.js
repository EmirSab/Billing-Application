(function(){
    application.controller("InvoiceReportCtrl",['$scope', '$rootScope','$anchorScroll', 'DataService',function($scope,$rootScope,$anchorScroll,DataService){
        ListInvoice();
        $scope.search=function(){
            ListAll($scope.invoiceNo);
        };
        
        function ListAll(Id){
        console.log($scope.InvoiceList);
          DataService.list("/InvoiceReport/"+Id,function(data){
              $scope.Report=data;
          });    
        };
        
        function ListInvoice(){
            if(credentials.currentUser.roles[0]=="admin")
                {
                    DataService.list("invoice", function(data){
                    $scope.invoices=data;            
                });
                }
            else
                {
                DataService.list("invoices?agntId="+credentials.currentUser.id, function(data){
                $scope.invoices=data;            
                });
            }  
        }    
    }]);
}());