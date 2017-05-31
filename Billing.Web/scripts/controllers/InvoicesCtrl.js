(function () {
	application.controller("InvoicesCtrl", ['$scope', 'DataService', '$http', function ($scope, DataService, $http) {

		$scope.shownInvoices = false;
		
		ListInvoices(0);
		ListAgents();
		ListCustomers();
		ListShipper();
		$scope.states = ["Canceled", "OrderCreated", "OrderConfirmed", "InvoiceCreated", "InvoiceSent", "InvoicePaid", "OnHold", "Ready", "Shipped"];

		$scope.mailData = {
			invoiceId: 0,
			mailTo: ""
		};

		$scope.edit = function (currentInvoice) {
			$scope.invoice = currentInvoice;
			$scope.shownInvoices = true;
		};

		//invoice report function for info button
		$scope.info = function (invoice) {
			DataService.read("invoicereport", invoice.id, function (data) {
				$scope.invoicesData = data;
			})
			$scope.showInvoices = true;
		};
		//end of the invoice report function

		$scope.save = function () {
			if ($scope.invoice.id == 0) DataService.insert("invoices", $scope.invoice, function (data) {
				ListInvoices($scope.currentPage - 1);
			});
			else DataService.update("invoices", $scope.invoice.id, $scope.invoice, function (data) {
				ListInvoices($scope.currentPage - 1);
			});
		};
		$scope.delete = function (currentInvoice) {
			DataService.delete("invoices", currentInvoice.id, function (data) {
				swal({
						title: "Are you sure?",
						text: "You will not be able to recover this Invoice!",
						type: "warning",
						showCancelButton: true,
						confirmButtonColor: "#DD6B55",
						confirmButtonText: "Yes, delete it!",
						closeOnConfirm: false
					},
					function () {
						ListInvoices();
						swal("Deleted!", "Invoice has been deleted.", "success");
					});
			});
		};
		var currentDate = new Date();
		var dateShipped = new Date(new Date(currentDate).setDate(currentDate.getDate() + 5)); // Set default shipping date to current + 5 days.
		$scope.new = function () {
			console.log("adding invoice");
			$scope.invoice = {
				id: 0,
				invoiceNo: "",
				date: new Date(),
				shippedOn: dateShipped,
				vat: 17
			};
			$scope.shownInvoices = true;
		};
		//ITEM PART OF CONTROLLER
		$scope.saveItem = function (item) {
			console.log(item);
			if (true) {
				var new_item = {
					id: 0,
					invoice: "New Invoice",
					product: item.product.name,
					unit: item.product.unit,
					subtotal: item.price * item.quantity,
					productId: item.product.id,
					invoiceId: $scope.invoice.id,
					price: item.price,
					quantity: item.quantity
				};

				DataService.insert("items", new_item, function (data) {
					$scope.invoice.items = data;
				});
			} else {
				DataService.update("items", item.id, item, function () {});
			}
		};
		//REMOVING ITEM
		$scope.removeItem = function (item) {
			DataService.delete("items", item.id, function () {
				DataService.read("invoices", $scope.invoice.id, function (data) {
					$scope.invoice = data;
				})
			});
		}


		$scope.printDiv = function (divName) {
			var printContents = document.getElementById(divName).innerHTML;
			var popupWin = window.open('', '_blank', 'width=1000,height=1000');
			popupWin.document.open();
			popupWin.document.write('<html><head><link rel="stylesheet" type="text/css" href="styles/bootstrap.min.css" /><link rel="stylesheet" type="text/css" href="styles/style.css" /></head><body onload="window.print()">' + printContents + '</body></html>');
			popupWin.document.close();
		};

		$scope.saveAsPdf = function (id) {
			DataService.download(id);
		};

		$scope.send = function (invoiceId) {
			$scope.mailData.invoiceId = invoiceId;
			DataService.insert("invoices/mail", $scope.mailData, function (data) {
				swal("Success!", "Your email is sent to given address!", "success")
			});
		}

		function getTowns(name) {
			DataService.list("towns/" + name, function (data) {
				$scope.towns = data;
			});
		}
		//LISTINGS
		function ListInvoices(page) {
			DataService.list("invoices?page=" + page, function (data) {
				$scope.invoices = data.invoicesList;
				$scope.totalPages = data.totalPages;
				$scope.currentPage = data.currentPage + 1;
				$scope.pages = new Array($scope.totalPages);
				for (var i = 0; i < $scope.totalPages; i++) $scope.pages[i] = i + 1;
			});
		}
		$scope.goto = function (page) {
			ListInvoices(page - 1);
		}
		$scope.getProducts = function (str) {
			console.log(str);
            return $http.get(BillingConfig.source + 'products/' + str).then(function (response) {
                return response.data;
            });
        }

		function ListAgents(agentName) {
			DataService.list("agents/", function (data) {
				$scope.agents = data
			});
		}

		function ListCustomers(customerName) {
			DataService.list("customers/", function (data) {
				$scope.customers = data
			});
		}

		function ListShipper(shipperName) {
			DataService.list("shippers/" + name, function (data) {
				$scope.shippers = data
			});
		}
    }]);
}());