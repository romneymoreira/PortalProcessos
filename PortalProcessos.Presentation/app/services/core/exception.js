(function () {
    'use strict';

    angular.module('app.core')
           .factory('exception', exception);

    exception.$inject = ['appConfig', 'logger', 'notification'];

    function exception(appConfig, logger, notification) {

        var service = {
            throwEx: throwEx
        };

        return service;
        
        function throwEx(ex) {
            if (appConfig.logExceptions) {
                logger.logEx(ex.data, ex.statusText);
            }

            if (ex.status === 500){
                notification.showError("Ocorreu um erro ao processar sua solicitação. Por favor, tente novamente mais tarde");                
            }
            else if (ex.status === 401) {
                notification.showError("Sua sessão foi finalizada.<br/> Por favor, entre novamente com suas credenciais");
            }
            else {
                notification.showError(ex.data);
            }
        }
    }
})();
