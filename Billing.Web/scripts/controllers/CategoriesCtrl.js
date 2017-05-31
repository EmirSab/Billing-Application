(function(){

    application.controller("CategoriesCtrl", ['$scope', 'DataService',  function($scope, DataService) {
        $scope.modalShown=false;
        $scope.showCategorie = false;
        ListCategories();

        $scope.edit = function(current){
            $scope.categorie = current;
            $scope.modalShown=true;
            //$scope.showCategorie = true;
        };

        $scope.save = function(){
            if($scope.categorie.id == 0)
                DataService.insert("categories", $scope.categorie, function(data){ ListCategories();} );
            else
                DataService.update("categories", $scope.categorie.id, $scope.categorie, function(data){ListCategories();});
            $scope.modalShown=false;
        };

        $scope.delete = function(current){
            DataService.delete("categories", current.id, function(data){
                 swal({
                        title: "Are you sure?",
                        text: "You will not be able to recover this Category!",
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "Yes, delete it!",
                        closeOnConfirm: false
                    },
                    function() {
                        ListCategories();
                        swal("Deleted!", "Category has been deleted.", "success");
                    });
            });
        };

  $scope.new = function() {
            swal({
                    title: "Add new Category",
                    text: "Insert a new Category:",
                    type: "input",
                    showCancelButton: true,
                    closeOnConfirm: false,
                    animation: "slide-from-top",
                    inputPlaceholder: "Enter Category"
                },
                function(inputValue) {
                    if (inputValue === false) return false;

                    if (inputValue === "") {
                        swal.showInputError("You need to write something!");
                        return false
                    }
                    console.log(inputValue);
                    $scope.categorie = {
                        id: 0,
                        name: inputValue
                    }
                    $scope.save();
                    swal("Nice!", "You added a new category", "success");    
                });
            $scope.showCategorie = true;
        }; 

        function ListCategories(){
            DataService.list("categories", function(data){ $scope.categories = data});
        }
    }]);
}());