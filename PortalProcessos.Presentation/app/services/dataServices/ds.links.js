(function () {
    'use strict';

    angular.module('app.dataServices')
           .factory('ds.links', DsLinks);

    DsLinks.$inject = ['$http', 'appConfig', 'common'];

    function DsLinks($http, appConfig, common) {

        var apiRoute = common.makeApiRoute('links');
        var service = {
            getAll: getAll,
            save: save,
            excluir: excluir,
            getByIdSetor: getByIdSetor
        };
        return service;

        function getAll() {
            return $http.get(common.makeUrl([apiRoute, 'getAll']));
        }
        function excluir(id) {
            return $http.get(common.makeUrl([apiRoute, 'excluir']), { params: { idLink: id } });
        }
        function getByIdSetor(id) {
            return $http.get(common.makeUrl([apiRoute, 'getByIdSetor']), { params: { idSetor: id } });
        }
        function save(link) {
            return $http.post(common.makeUrl([apiRoute, 'save']), link);
        }
    }
})();
