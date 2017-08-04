var repositoriesModule = angular.module('AgeRanger.Repositories', []);

//Returns Promise
repositoriesModule.service('PersonRepository', function ($http) {

    this.find = function (callback, errorCallback) {
        
        $http.get('/api/Person').then(function (response) {
            callback(response.data);

        }, function (err) {
            errorCallback(err.data);

        });
    };
    this.findById = function (id, callback, errorCallback) {

        $http.get('/api/Person/' + id).then(function (response) {
            callback(response.data);

        }, function (err) {
            errorCallback(err.data);

        });
    };

    this.add = function (formData, callback) {
        $http.post(
            '/api/Person', {
                FirstName: formData.FirstName,
                LastName: formData.LastName,
                Age: formData.Age
            }
        )
        .success(function (response) {
            callback(response);
        }).error(function (err) {
            callback(err);
        });
    };
    this.put = function (formData, callback) {
        $http.put(
            '/api/Person', {
                FirstName: formData.FirstName,
                LastName: formData.LastName,
                Age: formData.Age,
                Id: formData.Id
            }
        )
        .success(function (response) {
            callback(response);
        }).error(function (err) {
            callback(err);
        });
    };
});
