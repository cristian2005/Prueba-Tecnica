//employee controller 
myapp.controller('books-controller', function ($scope, booksService, toaster) {

    $scope.actualizar = false;
    $scope.spiner = true;

    loadBook();

    function loadBook() {
        var BooksRecords = booksService.getAllBooks();

        BooksRecords.then(function (d) {
            //success
            $scope.Books = d.data;
            $scope.spiner = false;
            angular.element(document).ready(function () {
                dTable = $('#tablaBook')
                dTable.DataTable();
            }); 
        }
            ,
            function () {
                alert("Error occured while fetching books list...");
            });
    }

    //Get book by Id
    $scope.getById = function (bookId) {

        $scope.spiner = true;

        booksService.getById(bookId).then(function (resp) {

            $scope.spiner = false;

            if (resp.data.title === null)
                toaster.error({ title: "error", body: "Book not found " });

            $scope.id = resp.data.id;
            $scope.title = resp.data.title;
            $scope.description = resp.data.description;
            $scope.excerpt = resp.data.excerpt;
            $scope.publishDate = resp.data.publishDate;
            $scope.actualizar = true;
        });
    }
    //save books data
    $scope.save = function () {
        var Books = {
            id: $scope.id,
            title: $scope.title,
            description: $scope.description,
            excerpt: $scope.excerpt,
            publishDate: $scope.publishDate
        };
        if ($scope.actualizar) {
            var saverecords = booksService.update(Books);
            saverecords.then(function (d) {
                if (d.data === 200) {
                    loadBook();
                    toaster.success({ title: "Success", body: "Book updated successfully" });
                    $scope.resetSave();
                    $scope.actualizar = false;
                }
                else {
                    toaster.error({ title: "error", body: "Book not updated " });
                    }
            },
                function () {
                    toaster.error({ title: "error", body: "Error occurred while adding book." });

                });
        } else {
            var saverecords = booksService.save(Books);
            saverecords.then(function (d) {
                if (d.data === 200) {
                    loadBook();
                    toaster.success({ title: "Success", body: "Book added successfully" });
                    $scope.resetSave();
                }
                else { toaster.error({ title: "Error", body: "Book not added " }); }
            },
                function () {
                    toaster.error({ title: "Error", body: "Error occurred while adding book." });
                });
        }
       
    }
    //reset controls after save operation
    $scope.resetSave = function () {
        $scope.id = '';
        $scope.title = '';
        $scope.description = '';
        $scope.excerpt = '';
        $scope.publishDate = '';
    }

    //delete Employee record
    $scope.delete = function (id) {
        var deleterecord = booksService.delete(id);
        deleterecord.then(function (d) {
            if (d.data === 200) {
                loadBook();
                toaster.success({ title: "Success", body: "Book deleted succussfully" });
            }
            else {
                toaster.error({ title: "Error", body: "Error occurred while deleted book." });
            }
        });
    }
});
