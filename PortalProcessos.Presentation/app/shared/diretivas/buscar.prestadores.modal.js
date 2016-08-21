(function () {
    'use strict';

    angular
        .module('app')
        .controller('buscarPrestadores', buscarPrestadores);

    buscarPrestadores.$inject = ['$scope', '$modalInstance', 'appConfig', 'common', 'notification', 'exception', 'blockUI', 'ds.prestador', 'busca'];

    function buscarPrestadores($scope, $modalInstance, appConfig, common, notification, exception, blockUI, DsPrestador, busca) {

        //Variáveis de escopo
        var vm = this;
        vm.formValid = false;
        vm.message = '';
        vm.Session = {};

        //Funções
        vm.init = init;
        vm.buscar = buscar;
        vm.cancel = cancel;
        vm.selecionar = selecionar;
        vm.Busca = {};


        //Initialization
        init();

        function init() {
            console.log(busca);
            vm.exibir = 1;
            vm.Busca.TipoBusca = 1;
        }

        function buscar() {

            var blocker = blockUI.instances.get('blockModal');
            blocker.start();

            DsPrestador
                .getPrestadoresPorFiltro(vm.Busca)
                .then(function (result) {
                    vm.prestadores = result.data;
                })
                .catch(function (ex) {
                    exception.throwEx(ex);
                })['finally'](function () {
                    blocker.stop();
                });
        }

        function selecionar(item) {
            $modalInstance.close(item);
        }

        function cancel() {
            $modalInstance.dismiss('cancel');
        }

    }
})();
