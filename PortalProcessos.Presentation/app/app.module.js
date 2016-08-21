(function () {
    'use strict';

    var app = angular.module('app', [

        /*
        * Dependencies
        */
        'ui.router',
        'ui.bootstrap',
        'angular-loading-bar',
        'ngAnimate',
        'ui.bootstrap.showErrors',
        'datatables',
        'ui.utils.masks',
        'idf.br-filters',
        'ui.select',
        'ngSanitize',
        'anim-in-out',
        'blockUI',
        'LocalStorageModule',
         'ui.utils.masks',
         'angular.filter',
         'ng-nestable',
        /*
         * App Core
         */
         'app.config',
         'app.core',
         'app.dataServices',

        /*
         * Shared Modules
         */

        /*
         * Modules
         */
        'app.dashboard',
        'app.manutencao',
        'app.users',
        'app.links'
    ]);


    /*
    * Routing
    */
    app.config(["$stateProvider", "$urlRouterProvider", function ($stateProvider, $urlRouterProvider) {

        $stateProvider.
            state('login', {
                url: '/login',
                templateUrl: 'app/account/login.html',
                controller: 'loginController as vm',
                data: {requireAuth: false }
            }).
            state('activateAccount', {
                url: '/activate/:userId/:hashcode',
                templateUrl: 'app/account/activate.html',
                controller: 'activateAccountController as vm',
                data: { requireAuth: false }
            }).
            state('createAccount', {
                 url: '/signup',
                 templateUrl: 'app/account/signup.html',
                 controller: 'signUpController as vm',
                 data: { requireAuth: false }
            }).
            state('app', {
                url: '',
                abstract: true,
                templateUrl: 'app/shared/layouts/app.html',
                controller: 'appController'
            });
            

        $urlRouterProvider.otherwise(function ($injector, $location) {
            var $state = $injector.get("$state");
            $state.go("dashboard");
        });

    }]);

    //Previne Cache (TODO:remover apos publicar)
    function configureTemplateFactory($provide) {
        // Set a suffix outside the decorator function 
        var cacheBust = Date.now().toString();

        function templateFactoryDecorator($delegate) {
            var fromUrl = angular.bind($delegate, $delegate.fromUrl);
            $delegate.fromUrl = function (url, params) {
                if (url !== null && angular.isDefined(url) && angular.isString(url)) {
                    url += (url.indexOf("?") === -1 ? "?" : "&");
                    url += "v=" + cacheBust;
                }

                return fromUrl(url, params);
            };

            return $delegate;
        }

        $provide.decorator('$templateFactory', ['$delegate', templateFactoryDecorator]);
    }

    app.config(['$provide', configureTemplateFactory]);


    /*
    * Authentication
    */
    app.run(['authService', function (authService) {
        authService.fillAuthData();
    }]);

    app.config(function ($httpProvider) {
        $httpProvider.defaults.useXDomain = true;
        $httpProvider.interceptors.push('authInterceptorService');
        delete $httpProvider.defaults.headers.common['X-Requested-With'];
    });



    app.directive('onlyDigits', function () {
        return {
            require: 'ngModel',
            restrict: 'A',
            link: function (scope, element, attr, ctrl) {
                function inputValue(val) {
                    if (val) {
                        var digits = val.replace(/[^0-9]/g, '');

                        if (digits !== val) {
                            ctrl.$setViewValue(digits);
                            ctrl.$render();
                        }
                        return parseInt(digits, 10);
                    }
                    return undefined;
                }
                ctrl.$parsers.push(inputValue);
            }
        };
    });



    /*
    * ui-select2
    */
    app.config(function (uiSelectConfig) {
        uiSelectConfig.theme = 'bootstrap';
        uiSelectConfig.resetSearchInput = true;
    });







    /*
    * RootScope
    */
    app.constant('_', window._);
    app.run(['$rootScope','$timeout', '$state', 'authService', function ($rootScope, $timeout, $state, authService) {

        authService.fillAuthData();

        //Evento de mudança de estado em rotas
        $rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState) {

            var shouldLogin = toState.data !== undefined
                           && toState.data.requireAuth
                           && !authService.authentication.isAuth;

            if (shouldLogin) {
                $state.go('login');
                event.preventDefault();
                return;
            }
        });

        //Evento apos carregamento do DOM em rotas
        $rootScope.$on('$viewContentLoaded', function (event) {
            $timeout(function () {
                initAppJs();
            },0);
        });

        //Lodash
        $rootScope._ = window._;   // use in views, ng-repeat="x in _.range(3)"
    }]);







   /*
   * BlockUI
   */
    app.config(function(blockUIConfig) {

        // Change the default overlay message
        //blockUIConfig.message = 'Aguarde...';
        blockUIConfig.autoBlock = false;
        //blockUIConfig.template = '<div class="block-ui-overlay"></div><div class="block-ui-message-container" aria-live="assertive" aria-atomic="true"><div class="block-ui-message ng-binding" ng-class="$_blockUiMessageClass"><img src="/assets/img/loader.gif" alt=""></div></div>';
        //blockUIConfig.template = '<div class="block-ui-overlay"></div><div class="block-ui-message-container" aria-live="assertive" aria-atomic="true"><div class="block-ui-message ng-binding" ng-class="$_blockUiMessageClass"><div class="windows8"><div class="wBall" id="wBall_1"><div class="wInnerBall"></div></div><div class="wBall" id="wBall_2"><div class="wInnerBall"></div></div><div class="wBall" id="wBall_3"><div class="wInnerBall"></div></div><div class="wBall" id="wBall_4"><div class="wInnerBall"></div></div><div class="wBall" id="wBall_5"><div class="wInnerBall"></div></div></div></div></div>';
        blockUIConfig.template = '<div class="block-ui-overlay"></div><div class="block-ui-message-container" aria-live="assertive" aria-atomic="true"><div class="block-ui-message ng-binding" ng-class="$_blockUiMessageClass"><div id="followingBallsG"><div id="followingBallsG_1" class="followingBallsG"></div><div id="followingBallsG_2" class="followingBallsG"></div><div id="followingBallsG_3" class="followingBallsG"></div><div id="followingBallsG_4" class="followingBallsG"></div></div></div></div>';
    });









   /*
   *
   * Directives
   * ============================
   */

    // Diretiva para 'Loading'
    // Usage: Incluir na div desejada: <loading></loading>
    app.directive('loading', function () {
        return {
            restrict: 'E',
            replace: true,
            template: '<div class="row"><div class="col-md-12"><div style="text-align: center"><img src="/content/img/loading.gif" /></div></div></div>',
            link: function (scope, element, attr) {
                scope.$watch('loading', function (val) {
                    if (val)
                        $(element).show();
                    else
                        $(element).hide();
                });
            }
        };
    });


    //Spin (Up/Down em inputs)
    app.directive('spin', function () {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs, ngModel) {
                
                var min = 0;
                var max = 999;
                var step = 1;
                
                element = angular.element(element);

                if (angular.isDefined(attrs)) {
                  min = typeof attrs.min !== 'undefined' ? attrs.min : 0;
                  max = typeof attrs.max !== 'undefined' ? attrs.max : 999;
                }

                element.TouchSpin({
                  verticalbuttons: true
                });

                if(!scope.$$phase) {
                  scope.$apply();
                }
            }
        };
    });

    //DatePicker
    app.directive('bsdatepicker', function () {
        return {
            restrict: 'A',            
             link: function (scope, element) {
                element.datepicker({format: 'dd/MM/yyyy', language: 'pt-BR'});
            }
        };
    });








    /*
    * Overrides TPLS ui-bootstrap
    */

    ////Tabs
    //angular.module("template/tabs/tabset.html", []).run(["$templateCache", function ($templateCache) {
    //    $templateCache.put("template/tabs/tabset.html",
    //      "<div>\n" +
    //      "  <ul class=\"nav nav-{{type || 'tabs'}} nav-tabs-simple\" ng-class=\"{'nav-stacked': vertical, 'nav-justified': justified}\" ng-transclude></ul>\n" +
    //      "  <div class=\"tab-content\">\n" +
    //      "    <div class=\"tab-pane\" \n" +
    //      "         ng-repeat=\"tab in tabs\" \n" +
    //      "         ng-class=\"{active: tab.active}\"\n" +
    //      "         tab-content-transclude=\"tab\">\n" +
    //      "    </div>\n" +
    //      "  </div>\n" +
    //      "</div>\n" +
    //      "");
    //}]);

    ////Accordion
    //angular.module("template/accordion/accordion-group.html", []).run(["$templateCache", function ($templateCache) {
    //    $templateCache.put("template/accordion/accordion-group.html",
    //      "<div class=\"panel panel-default\">\n" +
    //      "  <div class=\"panel-heading {{hclass}}\">\n" +
    //      "    <h4 class=\"panel-title\">\n" +
    //      "      <a href ng-class=\"{'collapsed': !isOpen}\" class=\"accordion-toggle\" ng-click=\"toggleOpen()\" accordion-transclude=\"heading\"><span ng-class=\"{'text-muted': isDisabled}\">{{heading}}</span></a>\n" +
    //      "    </h4>\n" +
    //      "  </div>\n" +
    //      "  <div class=\"panel-collapse\" collapse=\"!isOpen\">\n" +
    //      "	  <div class=\"panel-body\" ng-transclude></div>\n" +
    //      "  </div>\n" +
    //      "</div>\n" +
    //      "");
    //}]);

    ////Modal
    //angular.module("template/modal/backdrop.html", []).run(["$templateCache", function ($templateCache) {
    //    $templateCache.put("template/modal/backdrop.html",
    //      "<div class=\"modal-backdrop fade {{ backdropClass }}\"\n" +
    //      "     ng-class=\"{in: animate}\"\n" +
    //      "     ng-style=\"{'z-index': 11000 + (index && 1 || 0) + index*10}\"\n" +
    //      "></div>\n" +
    //      "");
    //}]);

    //angular.module("template/modal/window.html", []).run(["$templateCache", function ($templateCache) {
    //    $templateCache.put("template/modal/window.html",
    //      "<div tabindex=\"-1\" role=\"dialog\" class=\"modal fade slide-up disable-scroll\" ng-class=\"{in: animate}\" ng-style=\"{'z-index': 11010 + index*10, display: 'block'}\" ng-click=\"close($event)\">\n" +
    //      "    <div class=\"modal-dialog\" ng-class=\"{'modal-sm': size == 'sm', 'modal-lg': size == 'lg', 'modal-xl': size == 'xl'}\"><div class=\"modal-content-wrapper\"><div class=\"modal-content\" modal-transclude></div></div></div>\n" +
    //      "</div>");
    //}]);







    /*
    * Theme Global Scripts
    */
    function initAppJs() {

       
    }


})();