(function () {
    'use strict';

    angular.module('app.dataServices')
           .factory('ds.session', DsSession);

    DsSession.$inject = ['localStorageService', '$rootScope'];

    function DsSession(localStorageService, $rootScope) {

        var service = {
            setOperador: setOperador,
            getUsuario: getUsuario,
            setUsuario: setUsuario,
            limparSessao: limparSessao,
            removerPropriedade: removerPropriedade
        };

        return service;

        function setUsuario(value) {
            localStorageService.set('usuario', value);
            $rootScope.usuario = value;
        }

        //Retorno:  isAuth, UserName, Name, Roles
        function getUsuario() {
            return localStorageService.get('usuario');
        }


        function setOperador(value) {
            localStorageService.set('operador', value);
            $rootScope.operador = value;
        }

        //Limpar o localStorage
        function limparSessao() {
            return localStorageService.clearAll();
        }

        //Remover propriedade
        function removerPropriedade(prop) {
            return localStorageService.remove(prop);
        }

    }
})();
