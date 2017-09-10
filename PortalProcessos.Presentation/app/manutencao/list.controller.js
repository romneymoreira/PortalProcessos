(function () {
    'use strict';

    angular
        .module('app.manutencao')
        .controller('Manutencao', Manutencao);

    Manutencao.$inject = ['$scope', '$rootScope', '$http', '$q', '$modal', 'blockUI', 'appConfig', 'common', 'notification', 'exception', 'ds.manutencao'];
    function Manutencao($scope, $rootScope, $http, $q, $modal, blockUI, appConfig, common, notification, exception, dsManutencao) {

        common.setBreadcrumb('Manutenção');
        var vm = this;
        vm.search = {
            DataInicio: null,
            DataFim: null,
            Responsavel: undefined,
            IdSetor: 0,
            Tipo: 0,
            Status: 0
        };

        //Funções
        vm.init = init;
        vm.edit = edit;
        vm.excluir = excluir;
        vm.novaAtividade = novaAtividade;
        vm.buscarResponsavel = buscarResponsavel;
        vm.Buscar = Buscar;

        //Feature Start
        init();

        //Implementations
        function init() {
            
            vm.tipos = dsManutencao.constants.Tipo;
            vm.status = dsManutencao.constants.Status;
            var blocker = blockUI.instances.get('blockModal');
            blocker.start();
            var pAtividades = dsManutencao.getAllAtividades();
            pAtividades.then(function (result) {
                vm.atividades = result.data;
            });

            var pUser = dsManutencao.getAllUsers();
            pUser.then(function (result) {
                vm.responsaveis = result.data;
            });

            var pSetores = dsManutencao.getAllSetores();
            pSetores.then(function (result) {
                vm.setores = result.data;
            });

            $q.all([pAtividades, pSetores, pUser]).then(function () {
            })['finally'](function () {
                blocker.stop();
            }).catch(function (ex) {
                exception.throwEx(ex);
            });
        }

        function Buscar() {

            vm.search = {
                DataInicio: vm.dtInicio,
                DataFim: vm.dtTermino,
                Responsavel: vm.ReponsavelSelecionado == undefined ? "" : vm.ReponsavelSelecionado.name,
                IdSetor: vm.SetorSelecionado == undefined ? 0 : vm.SetorSelecionado.idSetor,
                Tipo: vm.TipoSelecionado == undefined ? 0 : vm.TipoSelecionado.Codigo,
                Status: vm.StatusSelecionado == undefined ? 0 : vm.StatusSelecionado.idStatus
            };
            var blocker = blockUI.instances.get('blockModal');
            blocker.start();

            var pBusca = dsManutencao.getAtividades(vm.search);
            pBusca.then(function (result) {
                vm.atividades = result.data;
                console.log(result.data);
            });
            $q.all([pBusca]).then(function () {
            })['finally'](function () {
                blocker.stop();
            }).catch(function (ex) {
                exception.throwEx(ex);
            });

        }

        function edit(item) {
            var modalInstance = $modal.open({
                templateUrl: 'app/manutencao/crud.html',
                controller: 'CrudManutencao as vm',
                backdrop: true,
                size: 'lg',
                resolve: {
                    atividade: function () {
                        return item;
                    }
                }
            });
            modalInstance.result.then(function () {
                init();
            });
        }

        function excluir(id) {
            vm.askOptions = { Title: 'Excluir atividade', Text: 'Deseja mesmo excluir esta atividade?', Yes: 'Sim', No: 'Não' };
            notification.ask(vm.askOptions, function (confirm) {
                if (confirm) {
                    var blocker = blockUI.instances.get('blockModal');
                    blocker.start();
                    var pExcluir = dsManutencao.excluirAtividade(id);
                    pExcluir.then(function (result) {
                    });
                    $q.all([pExcluir]).then(function () {
                    })['finally'](function () {
                        blocker.stop();
                        notification.showSuccess('Atividade excluida com sucesso.');
                    }).catch(function (ex) {
                        exception.throwEx(ex);
                    });
                }
            });
        }

        function buscarResponsavel(search) {
            if (search.length > 2) {
                var pUser = dsManutencao.getUserByName(search);
                pUser.then(function (result) {
                    vm.responsaveis = result.data;
                });
                $q.all([pUser]).then(function () {
                })['finally'](function () {
                }).catch(function (ex) {
                    exception.throwEx(ex);
                });
            }
        }

        function novaAtividade() {
            var modalInstance = $modal.open({
                templateUrl: 'app/manutencao/crud.html',
                controller: 'CrudManutencao as vm',
                backdrop: true,
                size: 'lg',
                resolve: {
                    atividade: function () {
                        return null;
                    }
                }
            });
            modalInstance.result.then(function () {
                init();
            });
        }
    }
})();