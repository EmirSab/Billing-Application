(function(){

    application = angular.module("Billing", ["ngRoute", "LocalStorageModule","ui.bootstrap"]);

    credentials = {
        token: "",
        expiration: "",
        currentUser: {
            id: 0,
            name: "",
            role: ""
        }
    };

    function authenticated() {
        return (credentials.currentUser.id != 0)
    }

    redirectTo = '/';
    
    application.config(function($routeProvider){
        $routeProvider
            .when("/agents", { templateUrl: "views/agents.html", controller: "AgentsCtrl" })
            .when("/customers", { templateUrl: "views/customers.html", controller: "CustomersCtrl" })
            .when("/categories", { templateUrl: "views/categories.html", controller: "CategoriesCtrl" })
            .when("/items", {templateUrl: "views/items.html", controller: "ItemsCtrl"})
            .when("/stock", { templateUrl: "views/stock.html", controller: "StockCtrl" })
            .when("/invoices", {templateUrl: "views/invoices.html", controller: "InvoicesCtrl"})
            .when("/suppliers", { templateUrl: "views/suppliers.html", controller: "SuppliersCtrl" })
            .when("/products", { templateUrl: "views/products.html", controller: "ProductsCtrl" })
            .when("/shippers", { templateUrl: "views/shippers.html", controller: "ShippersCtrl" })
            //reports
            .when("/AgentsByRegion",{templateUrl:"views/agentsregions.html",controller:"AgentsRegionsCtrl"})
            .when("/SalesByCustomer",{templateUrl:"views/salesbycustomer.html",controller:"SalesByCustomerCtrl"})
            .when("/SalesByRegion",{templateUrl:"views/salesbyregion.html",controller:"SalesByRegionCtrl"})
            .when("/SalesByCategory",{templateUrl:"views/Reports/salesbycategory.html",controller:"SalesByCategoryCtrl"})
            .when("/CustomersByCategory",{templateUrl:"views/Reports/salesbycustomercategory.html",controller:"SalesByCustomerCategoryCtrl"})
            .when("/Dashboard",{templateUrl:"views/Reports/dashboard.html",controller:"DashboardCtrl"})
            .when("/StockLevel",{templateUrl:"views/Reports/stocklevel.html",controller:"StockLevelCtrl"})
            .when("/InvoicesReview",{templateUrl:"views/Reports/invoicesreview.html",controller:"InvoicesReviewCtrl"})
            //end reports
            .when("/procurements", { templateUrl: "views/procurements.html", controller: "ProcurementsCtrl" })
            .when("/login", { templateUrl: "views/login.html", controller: "LoginCtrl" })
            .when("/logout", { template: "", controller: "LogoutCtrl"})
            .otherwise({ redirectTo: "/agents" }); //mozda login controller
    }).run(function($rootScope, $location){
        $rootScope.$on("$routeChangeStart", function(event, next, current){
            if(!authenticated()){
                if(next.templateUrl != "views/login.html"){
                    redirectTo = $location.path();
                    if(redirectTo == "/login") redirectTo = "/agents";
                    if(redirectTo == "/logout") redirectTo = "/agents";
                    $location.path("/login");
                }
            }
        })
		 $rootScope.authenticated=authenticated;
    });
}());
