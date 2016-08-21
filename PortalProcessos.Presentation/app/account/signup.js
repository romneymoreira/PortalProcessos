/**
*
*  Controller Login
*  ==============================================================
* 
**/

(function () {
    'use strict';

    angular.module('app')
           .controller('signUpController', signUpController);

    signUpController.$inject = ['$scope', '$state', 'common', 'notification', 'authService'];

    function signUpController($scope, $state, common, notification, authService) {
        var vm = this;
        vm.message = '';
        vm.success = '';
        vm.formValid = false;
        vm.showForm = true;
        vm.User = {
            Email: "",
            Cnpj: ""
        }

        vm.signUp = function () {

            vm.message = '';
            vm.formValid = common.validateForm($scope.signupForm);

            if (vm.formValid) {
                authService.signUp(vm.User).then(function (response) {

                    vm.success = 'Sua corretora foi cadastrada com sucesso.<br/>Por favor, verifique seu e-mail para ativação de seu cadastro.';

                }, function (err) {
                    notification.showErrorTop(err.statusText);
                });

            } else {
                if ($scope.signupForm.Cnpj.$error.cnpj) {
                    vm.message = 'O CNPJ digitado não é válido';
                }
                else if ($scope.signupForm.Email.$error.email) {
                    vm.message = 'O e-mail digitado não é válido';
                } else {
                    vm.message = 'Favor preencher os campos abaixo:';
                }
            }

        };

    }
})();