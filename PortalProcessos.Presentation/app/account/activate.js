/**
*
*  Controller Ativação Cadastro
*  ==============================================================
* 
**/

(function () {
    'use strict';

    angular.module('app')
           .controller('activateAccountController', activateAccountController);

    activateAccountController.$inject = ['$scope', '$state', '$stateParams', 'common', 'notification', 'authService'];

    function activateAccountController($scope, $state, $stateParams, common, notification, authService) {
        var vm = this;
        vm.message = '';
        vm.formValid = false;

        vm.Account = {
            UserId: '',
            Password: '',
            ConfirmPassword: '',
            HashCode: ''
        }

        authService.logOut();

        //Verifica as variáves GET
        if (angular.isUndefined($stateParams) || angular.isUndefined($stateParams.userId) || angular.isUndefined($stateParams.hashcode)) {
            $state.go('login');
        } else {
            vm.Account.UserId = $stateParams.userId;
            vm.Account.HashCode = $stateParams.hashcode;
        }

        vm.confirmAccount = function () {

            vm.message = '';
            vm.formValid = common.validateForm($scope.confirmAccountForm);

            if (vm.formValid) {

                if (vm.Account.Password.length < 6 || vm.Account.Password.length > 20) {
                    vm.message = 'Sua senha deve conter entre 6 e 20 caracteres';
                    return;
                }

                if (vm.Account.Password != vm.Account.ConfirmPassword) {
                    vm.message = 'Senha e confirma senha não conferem';
                    return;
                }

                authService.confirmAccount(vm.Account).then(function (response) {

                    //Usuário ativado, loga o mesmo
                    authService.login({
                        UserName: response.data.UserName,
                        Password: vm.Account.Password
                    }).then(function () {

                        notification.showSuccess('Sua conta foi ativada.');
                        $state.go('dashboard');

                    },
                    function (err) {
                        notification.showErrorTop(err.statusText);
                    });

                },
                function (err) {
                    notification.showErrorTop(err.statusText);
                });
                
            } else {
                vm.message = 'Favor preencher os campos abaixo:';
            }
        };

    }
})();