/**
*
*  Controller Login
*  ==============================================================
* 
**/

(function () {
    'use strict';

    angular.module('app')
           .controller('loginController', loginController);

    loginController.$inject = ['$rootScope', '$scope', '$state', 'common', 'notification', 'authService', 'ds.dashboard'];

    function loginController($rootScope, $scope, $state, common, notification, authService, DsDashboard) {
        var vm = this;
        vm.message = '';
        vm.formValid = false;
        vm.User = {
            UserName: "",
            Password: ""
        }

        $rootScope.$state = $state;
        authService.logOut();

        vm.login = function () {

            vm.message = '';
            vm.formValid = common.validateForm($scope.loginForm);

            if (vm.formValid) {
                authService.login(vm.User).then(function (response) {
                    console.log(response);
                   // $state.go('dashboard');
                DsDashboard.addVisita(response.id_user)
                .then(function (result) {
                        $state.go('dashboard');
                    })
                    .catch(function (ex) {
                    exception.throwEx(ex);
                    })['finally'](function () {
                        
                    });
                },
                function (err) {
                    notification.showError(err.error_description);
                });
            } else {
                vm.message = 'Favor preencher os campos abaixo:';
            }

        };

    }
})();