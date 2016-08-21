(function () {
	'use strict';

	angular.module('app.dataServices')
           .factory('ds.dashboard', DsDashboard);

	DsDashboard.$inject = ['$http', 'appConfig', 'common'];

	function DsDashboard($http, appConfig, common) {

	    var apiRoute = common.makeApiRoute('home');
		var service = {
		    getAll: getAll,
		    getTotalDocs: getTotalDocs,
		    getLinks: getLinks,
		    addVisita: addVisita,
		    totalVisitas: totalVisitas
		};
		return service;

		function getAll() {
		    return $http.get(apiRoute);
		}

		function totalVisitas(id) {
		    return $http.get(common.makeUrl([apiRoute, 'totalVisitas']), { params: { idUser: id } });
		}

		function addVisita(id) {
		    return $http.get(common.makeUrl([apiRoute, 'addVisita']), { params: { idUser: id } });
		}

		function getTotalDocs(id) {
		    return $http.get(common.makeUrl([apiRoute, 'getDocs']));
		}
		function getLinks() {
		    return $http.get(common.makeUrl([apiRoute, 'getLinks']));
		}
	}
})();
