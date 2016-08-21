(function () {
    'use strict';

    angular.module('app.dataServices')
           .factory('ds.users', DsUsers);

    DsUsers.$inject = ['$http', '$q', 'appConfig', 'common', 'exception', 'logger'];

    function DsUsers($http, $q, appConfig, common, exception, logger) {

        var apiRoute = common.makeApiRoute('users');
        var service = {
            getAll: getAll,
            getById: getById,
            getLevel: getLevel,
            save: save
        };

        return service;


        function getAll() {
            return $http.get(apiRoute);
        }

        function getLevel() {
            return $http.get(common.makeUrl([apiRoute, 'getLevel']));
        }

        function getById(id) {
            return $http.get(common.makeUrl([apiRoute, id]));
        }

        function save(user) {
            return $http.post(common.makeUrl([apiRoute, 'salvar']), user);
        }

    }
})();