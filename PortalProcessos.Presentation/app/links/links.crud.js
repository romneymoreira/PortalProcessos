(function () {
    'use strict';

    angular
        .module('app.links')
        .controller('LinksCrud', LinksCrud);

    LinksCrud.$inject = ['$scope', '$http', 'exception', 'notification', '$modal', '$modalInstance', '$q', 'blockUI', 'common', 'ds.links', 'ds.manutencao'];

    function LinksCrud($scope, $http, exception, notification, $modal, $modalInstance, $q, blockUI, common, dsLinks, dsManutencao) {

        common.setBreadcrumb('links uteis');
        var vm = this;
        $scope.forms = {};
        vm.formValid = true;

        //Funções
        vm.init = init;
        vm.add = add;
        vm.excluir = excluir;
        vm.cancel = cancel;


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

        function cancel() {
            $modalInstance.dismiss('cancel');
        }

        function excluir(id) {
            notification.ask({ Text: "Deseja excluir este link?" }, function (confirm) {
                if (confirm) {
                    dsLinks
                          .excluir(id)
                          .then(function (result) {
                              init();
                          })
                          .catch(function (ex) {
                              exception.throwEx(ex);
                          })['finally'](function () {
                              notification.showSuccess('Link Excluido com sucesso!');
                          });
                }
            });
        }

        function add() {
            vm.formValid = common.validateForm($scope.forms.dados);
            if (vm.formValid) {
                vm.AlertMessage = "";
                vm.Link.IdSetor = vm.setorSelecionado.idSetor;
                dsLinks
                   .save(vm.Link)
                   .then(function (result) {
                       init();
                   })
                   .catch(function (ex) {
                       exception.throwEx(ex);
                   })['finally'](function () {
                       notification.showSuccess('Link incluído com sucesso');
                   });
            }
            else {
                vm.AlertClassI = 'fa fa-exclamation-triangle';
                vm.AlertClassDiv = 'alert alert-danger';
                vm.AlertMessage = "Preencha os campos em vermelho.";
            }
        }
    }
})();