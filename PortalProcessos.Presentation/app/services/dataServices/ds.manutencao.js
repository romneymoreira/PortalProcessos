(function () {
    'use strict';

    angular.module('app.dataServices')
           .factory('ds.manutencao', dsManutencao);

    dsManutencao.$inject = ['$http', 'appConfig', 'common'];

    function dsManutencao($http, appConfig, common) {

        var apiRoute = common.makeApiRoute('manutencao');
        var service = {
            constants: {
                Tipo: [
	                { Descricao: 'Inclusão', Codigo: '1' },
	                { Descricao: 'Alteração', Codigo: '2' },
	                { Descricao: 'Exclusão', Codigo: '3' }
                ],
                Status: [
                       { Descricao: 'Pendente', Codigo: '1' },
                       { Descricao: 'Em Andamento', Codigo: '2' },
                       { Descricao: 'Concluído', Codigo: '3' }
                ]
            },
            getAllSetores: getAllSetores,
            getAllAtividades: getAllAtividades,
            getAtividadeById: getAtividadeById,
            save: save,
            getAllUsers: getAllUsers,
            excluirAtividade: excluirAtividade,
            getAtividades: getAtividades
           
        };
        return service;

        function getAllSetores() {
            return $http.get(common.makeUrl([apiRoute, 'setores']));
        }

        function excluirAtividade(id) {
            return $http.get(common.makeUrl([apiRoute, 'excluirAtividade']), { params: { id: id } });
        }

        function getAllAtividades() {
            return $http.get(common.makeUrl([apiRoute, 'listar']));
        }

        function getAllUsers() {
            return $http.get(common.makeUrl([apiRoute, 'getAllUsers']));
        }

        function getAtividadeById(id) {
            return $http.get(common.makeUrl([apiRoute, 'byId']), { params: { id: id } });
        }

        function getAtividades(model) {
            return $http.post(common.makeUrl([apiRoute, 'getAtividades']), model);
        }

        function save(model) {
            return $http.post(common.makeUrl([apiRoute, 'save']), model);
        }
    }
})();
