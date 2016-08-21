(function () {

    'use strict';
    angular
        .module('app');

    //Menu do usuário
    angular.module('app').directive('pdevBuscarPrestadores',
     ['$modal', 'exception', 'ds.session', function ($modal, exception, dsSession) {
         return {
             restrict: 'EA',
             transclude: true,
             templateUrl: 'app/shared/diretivas/buscar.prestadores.html',
             scope: {
                 idLocalAtendimento: '@',
                 tipoBusca: '@',
                 tipoPrestador: '@',
                 prestadorSelecionado: '='
             },
             controllerAs: "vmbp",
             controller: function ($scope) {
                 var vmbp = this;
                 vmbp.buscar = buscar;
                 vmbp.buscarPrestador = buscarPrestador;
                 vmbp.selecionado = selecionado;
                 vmbp.pSelecionado = [];

                 vmbp.tipoBusca = $scope.tipoBusca;
                 vmbp.tipoPrestador = $scope.tipoPrestador;//1 = executantes, 2 = solicitantes, 3 = locais de atendimneto
                 vmbp.idLocalAtendimento = $scope.idLocalAtendimento;

                 if (vmbp.tipoPrestador == 1)
                     vmbp.NomeTipo = "Executante";
                 else if (vmbp.tipoPrestador == 2)
                     vmbp.NomeTipo = "Solicitante";
                 else
                     vmbp.NomeTipo = "Local de Atendimento";

                 function selecionado(item) {
                     $scope.prestadorSelecionado = item;
                     vmbp.pSelecionado = item;
                 }

                 function buscar() {
                     var busca = { tipoBusca: vmbp.tipoBusca, idLocalAtendimento: vmbp.idLocalAtendimento, tipoPrestador: vmbp.tipoPrestador }

                     var modalInstance = $modal.open({
                         templateUrl: 'app/shared/diretivas/buscar.prestadores.modal.html',
                         controller: 'buscarPrestadores as vm',
                         backdrop: true,
                         size: 'lg',
                         resolve: {
                             busca: busca
                         }
                     });
                     modalInstance.result.then(function (item) {
                         $scope.prestadorSelecionado = item;
                         vmbp.pSelecionado = item;
                     });
                 }

                 function buscarPrestador(search) {
                     if (search.length > 2) {
                         DsPrestador
                             .getPrestadoresPorNome(search, vmbp.tipoBusca, vmbp.idLocalAtendimento)
                             .then(function (result) {
                                 vmbp.prestadores = result.data;
                             })
                             .catch(function (ex) {
                                 exception.throwEx(ex);
                             });
                     }
                 }
             }
         }
     }
     ]);
    //Menu do usuário
    angular.module('app').directive('pdevUsuarioMenu',
     ['ds.session', 'authService', function (dsSession, authService) {
         return {
             restrict: 'E',
             transclude: true,
             templateUrl: 'app/shared/diretivas/usuario.menu.html',
             controllerAs: "vm",
             controller: function ($scope) {
                 var vm = this;
                 vm.Usuario = dsSession.getUsuario();
                 vm.Sair = authService.logOut;
             }
         }
     }
     ]);
})();