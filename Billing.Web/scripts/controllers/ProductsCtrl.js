(function () {
	application.controller("ProductsCtrl", ['$scope', '$http', 'DataService', function ($scope, $http, DataService) {

		$scope.modalShown = false;
		//$scope.showProducts=false;
		ListProducts(0);
		ListCategories();
		//get all products

		$scope.getProduct = function (currentProduct) {
			$scope.product = currentProduct;
			//$scope.showProducts = true;
			$scope.modalShown = true;
		};
		//update or create products
		$scope.save = function () {
			if ($scope.product.id == 0) {
				DataService.insert("products", $scope.product, function (data) {
					ListProducts($scope.currentPage - 1);
					//$scope.showProducts=false;
					$scope.modalShown = false;
				});
			} else {
				DataService.update("products", $scope.product.id, $scope.product, function (data) {
					ListProducts($scope.currentPage - 1);
					//$scope.showProducts=false;
					$scope.modalShown = false;
				});
			}
		};
		//delete product

		$scope.deleteProduct = function (currentProduct) {
			DataService.delete("products", currentProduct.id, function (data) {
				swal({
						title: "Are you sure?",
						text: "You will not be able to recover this Product!",
						type: "warning",
						showCancelButton: true,
						confirmButtonColor: "#DD6B55",
						confirmButtonText: "Yes, delete it!",
						closeOnConfirm: false
					},
					function () {
						ListProducts();
						swal("Deleted!", "Product has been deleted.", "success");
					});
			});
			//$scope.showProducts = false;
			$scope.modalShown = false;
		};
		//create new
		$scope.new = function () {
			$scope.product = {
				id: 0,
				name: "",
				unit: "",
				price: "",
				category: "",
				categoryId: "",
				input: "",
				output: "",
				inventory: ""
			};
			//$scope.showProducts=true;
			$scope.modalShown = true;
		};

		//get Products by page pagginaton
		function ListProducts(page) {
			DataService.list("products?page=" + page, function (data) {
				$scope.products = data.productsList;
				$scope.totalPages = data.totalPages;
				$scope.currentPage = data.currentPage + 1;
				$scope.pages = new Array($scope.totalPages);
				for (var i = 0; i < $scope.totalPages; i++) $scope.pages[i] = i + 1;
				console.log($scope.currentPage);
			});
		}
		$scope.goto = function (page) {
			ListProducts(page - 1);
		}
		//end of paggination

		// Deleted because we are getting them from page number
		//get list categories from the database
		// function ListProducts() {
		//     DataService.list("products", function (data) {
		//         $scope.products = data
		//     });

		// };
		function ListCategories() {
			DataService.list("categories", function (data) {
				$scope.categories = data
			});

		};


	}]);
}());