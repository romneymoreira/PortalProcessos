(function () {

    'use strict';
    angular
        .module('app')
        .controller('appController', appController);

    appController.$inject = ['$rootScope', '$scope', '$state', '$timeout','authService'];

    function appController($rootScope, $scope, $state, $timeout,authService) {

        //Roles/Profile
        $scope.$state = $state;
        authService.fillAuthData();
        var roles = authService.authentication.Roles;

        $rootScope.User = { UserName: authService.authentication.UserName, Name: authService.authentication.Name };

        //App Core (Theme)
        Core.init();
    }


})();