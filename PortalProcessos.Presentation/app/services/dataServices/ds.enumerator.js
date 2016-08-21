(function () {
	'use strict';

	angular.module('app.dataServices')
           .factory('ds.enumerator', DsEnumerator);

    DsEnumerator.$inject = ['$http', '$q', 'appConfig', 'common'];
    function DsEnumerator($http, $q, appConfig, common) {

        var apiRoute = common.makeApiRoute('enumerator');

		var service = {
		    getTipoLogradouro: getTipoLogradouro,
		    getEstados: getEstados,
		    getCidades: getCidades,
		    getEstadoCivil: getEstadoCivil,
		    getRegioes: getRegioes,
		    getParentescos: getParentescos,
		    getSexos: getSexos
		};

		return service;

		function getTipoLogradouro() {
            return $http.post(common.makeUrl([apiRoute, 'getTipoLogradouro']));
		}

		function getEstados() {
		    return $http.post(common.makeUrl([apiRoute, 'getEstados']));
		}

		function getCidades(estado) {
		    return $http.get(common.makeUrl([apiRoute, 'getCidades', estado]));
		}

		function getEstadoCivil() {
		    return $http.post(common.makeUrl([apiRoute, 'getEstadoCivil']));
		}

		function getParentescos() {
		    return $http.post(common.makeUrl([apiRoute, 'getParentescos']));
		}

		function getSexos() {
		    return $http.post(common.makeUrl([apiRoute, 'getSexos']));
		}

		function getRegioes() {
		    return $http.post(common.makeUrl([apiRoute, 'getRegioes']));
		}
	}
})();
