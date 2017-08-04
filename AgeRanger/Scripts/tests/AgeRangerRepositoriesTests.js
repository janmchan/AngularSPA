
var getResponse = "[{\"Age\":null,\"FirstName\":\"ABC\",\"Id\":1,\"LastName\":\"DEF\"},{\"Age\":null,\"FirstName\":\"GHI\",\"Id\":2,\"LastName\":\"JKL\"}]";

repositoriesModule.config(function ($provide) {
    $provide.decorator('$httpBackend', angular.mock.e2e.$httpBackendDecorator);
});

var repositoryInjector = angular.injector(['ng', 'AgeRanger.Repositories']);
var http = repositoryInjector.get('$httpBackend');
var personRepository;



QUnit.module('Repository Tests', {
    beforeEach: function () {
        personRepository = repositoryInjector.get('PersonRepository');
    }
});

test('Person Data Properly Returned',
    function() {
        http.expectGET('/api/Person').respond(getResponse);
        personRepository.find(function(returnVal) {
            ok(angular.equals(returnVal, JSON.parse(getResponse)), "Person Data from repository is expected");
        }, function(err) {
            ok(false, "Error Data Was returned");
        });
        http.flush();
    });