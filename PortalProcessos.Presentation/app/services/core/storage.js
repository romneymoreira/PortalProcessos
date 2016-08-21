(function () {
    'use strict';

    angular.module('app.core')
           .factory('storage', storage);

    storage.$inject = ['appConfig', 'logger', 'notification', 'localStorageService'];

    function storage(appConfig, logger, notification, localStorageService) {

        var service = {
            set: set,
            get: get,
            remove: remove,
            clearAll: clearAll
        };

        return service;

        function set(key, value) {
            //console.log('localStorage setting: key -> ' + key);
            //console.log('Value:');
            //console.log(value);
            localStorageService.set(key, value);
        }

        function get(key) {
            //console.log('localStorage getting: key -> ' + key);
            
            return localStorageService.get(key);
        }

        function remove(key) {
            localStorageService.remove(key);
        }

        function clearAll() {
            localStorageService.clearAll();
        }
    }
})();
