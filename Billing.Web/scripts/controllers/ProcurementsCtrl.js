(function () {
	application.controller("ProcurementsCtrl", ['$scope', '$http', 'DataService', function ($scope, $http, DataService) {

		$scope.showProcurements = false;
		ListProcurements(0);
		ListProducts('');
		ListSuppliers('');

		$scope.getProcurement = function (currentProcurement) {
			$scope.procurement = currentProcurement;
			console.log(procurement);
			$scope.showProcurements = true;
		};

		$scope.save = function () {
			if ($scope.procurement.id == 0) {
				DataService.insert("procurements", $scope.procurement, function (data) {
					ListProcurements($scope.currentPage - 1);
					$scope.showProcurements = false;
				});
			} else {
				DataService.update("procurements", $scope.procurement.id, $scope.procurement, function (data) {
					ListProcurements($scope.currentPage - 1);
					$scope.showProcurements = false;
				});
			}
		};
		$scope.deleteProcurement = function (currentProcurement) {
			DataService.delete("procurements", currentProcurement.id, function (data) {
				swal({
						title: "Are you sure?",
						text: "You will not be able to recover this Procurement!",
						type: "warning",
						showCancelButton: true,
						confirmButtonColor: "#DD6B55",
						confirmButtonText: "Yes, delete it!",
						closeOnConfirm: false
					},
					function () {
						ListProcurements();
						swal("Deleted!", "Procurement has been deleted.", "success");
					});
			});
			$scope.showProcurements = false;
		};

		$scope.new = function () {
			$scope.procurement = {

				id: 0,
				date: "",
				document: "",
				quantity: "",
				price: "",
				total: "",
				supplier: "",
				supplierId: "",
				product: "",
				productId: ""

			};
			$scope.showProcurements = true;

		};

		//get Products by page pagginaton
		function ListProcurements(page) {
			DataService.list("procurements?page=" + page, function (data) {
				$scope.procurements = data.procurementsList;
				$scope.totalPages = data.totalPages;
				$scope.currentPage = data.currentPage + 1;
				$scope.pages = new Array($scope.totalPages);
				for (var i = 0; i < $scope.totalPages; i++) $scope.pages[i] = i + 1;
				console.log($scope.currentPage);
			});
		}
		$scope.goto = function (page) {
			ListProcurements(page - 1);
		}
		//end of paggination

		//get all procurements function deleted because we are getting them by page number
		// function ListProcurements() {
		//     DataService.list("procurements", function (data) {
		//         $scope.procurements = data
		//     });

		// };


		function ListProducts() {
			DataService.list("products", function (data) {
				$scope.products = data;
			});
		};

		function ListSuppliers() {

			DataService.list("suppliers", function (data) {
				$scope.suppliers = data;
			});

		};

	}]);
}());