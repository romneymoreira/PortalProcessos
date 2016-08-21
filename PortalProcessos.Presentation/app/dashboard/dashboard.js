(function () {
    'use strict';

    angular
        .module('app.dashboard')
        .controller('Dashboard', Dashboard);

    Dashboard.$inject = ['$scope', '$http', '$q', 'blockUI', 'common', 'exception', 'ds.dashboard', 'ds.session'];

    function Dashboard($scope, $http, $q,blockUI, common, exception, DsDashboard, dsSession) {

        common.setBreadcrumb('dashboard');
        var vm = this;

        //Funções
        vm.init = init;
        var usuario = dsSession.getUsuario();

        //Feature Start
        init();

        //Implementations
        function init() {
            console.log(usuario);
            var blocker = blockUI.instances.get('blockModal');
            blocker.start();
            var pLinks = DsDashboard.getLinks();
            pLinks.then(function (result) {
                vm.links = result.data;
            });
            var pDocs = DsDashboard.getTotalDocs();
            pDocs.then(function (result) {
                vm.docs = result.data;
            });
            var pVisitas = DsDashboard.totalVisitas(usuario.IdUser);
            pVisitas.then(function (result) {
                vm.visitas = result.data;
            });


            $q.all([pLinks, pDocs, pVisitas]).then(function () {
                console.log(vm.visitas);
            })['finally'](function () {
                blocker.stop();
            }).catch(function (ex) {
                exception.throwEx(ex);
            });
        }
    }
})();