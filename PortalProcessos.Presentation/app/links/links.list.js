(function () {
    'use strict';

    angular
        .module('app.links')
        .controller('Links', Links);

    Links.$inject = ['$scope', '$http', '$modal', '$q', 'blockUI', 'common', 'ds.links', 'ds.manutencao'];

    function Links($scope, $http, $modal, $q, blockUI, common, dsLinks, dsManutencao) {

        common.setBreadcrumb('links uteis');
        var vm = this;
        vm.setorSelecionado = undefined;

        //Funções
        vm.init = init;
        vm.buscaLinksSetor = buscaLinksSetor;
        vm.novoLink = novoLink;

        //Feature Start
        init();

        //Implementations
        function init() {
            var blocker = blockUI.instances.get('blockModal');
            blocker.start();
            var pLinks = dsLinks.getAll();
            pLinks.then(function (result) {
                vm.links = result.data;
            });

            var pSetores = dsManutencao.getAllSetores();
            pSetores.then(function (result) {
                vm.setores = result.data;
            });


            $q.all([pLinks, pSetores]).then(function () {

            })['finally'](function () {
                blocker.stop();
            }).catch(function (ex) {
                exception.throwEx(ex);
            });
        }

        function novoLink() {
            var modalInstance = $modal.open({
                templateUrl: 'app/links/links.crud.html',
                controller: 'LinksCrud as vm',
                backdrop: true,
                size: 'lg'
            });
            modalInstance.result.then(function (result) {
                var blocker = blockUI.instances.get('blockModal');
                blocker.start();
                var pLinks = dsLinks.getByIdSetor(result.IdSetor);
                pLinks.then(function (result) {
                    vm.links = result.data;
                });
                $q.all([pLinks]).then(function () {
                })['finally'](function () {
                    blocker.stop();
                }).catch(function (ex) {
                    exception.throwEx(ex);
                });
            });
        }

        function buscaLinksSetor() {
            if (vm.setorSelecionado != undefined) {
                var blocker = blockUI.instances.get('blockModal');
                blocker.start();
                var pLinks = dsLinks.getByIdSetor(vm.setorSelecionado.idSetor);
                pLinks.then(function (result) {
                    vm.links = result.data;
                });

                $q.all([pLinks]).then(function () {
                })['finally'](function () {
                    blocker.stop();
                }).catch(function (ex) {
                    exception.throwEx(ex);
                });
            }
        }
    }
})();
