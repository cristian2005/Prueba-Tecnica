//Service to get data from employee mvc controller 
myapp.service('booksService', function ($http) {

    //get all book
    this.getAllBooks = function () {

        return $http.get("api/Books");
    }

    //add new book
    this.save = function (Book) {
        var request = $http({
            method: 'post',
            url: 'api/Books',
            data: Book
        });
        return request;
    }

    //update Book records
    this.update = function (Book) {
        var updaterequest = $http({
            method: 'put',
            url: 'api/Books/'+Book.id,
            data: Book
        });
        return updaterequest;
    }

    //get book by id
    this.getById = function (id) {

        return $http.get("api/Books/"+id);
    }

    //delete record
    this.delete = function (id) {
        var deleterequest = $http({
            method: 'delete',
            url: 'api/Books/'+id
        });
        return deleterequest;
    }
    
});
