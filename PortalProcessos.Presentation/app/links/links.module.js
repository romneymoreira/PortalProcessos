(function () {
    'use strict';

    var appLinks = angular.module('app.links', ['angular-loading-bar', 'app.config']);

    appLinks.config(["$stateProvider", "appConfig", function ($stateProvider, appConfig) {

        $stateProvider
        .state("links", {
            parent: 'app',
            url: appConfig.routePrefix + "/links-uteis",
            views: {
                'content': {
                    templateUrl: "app/links/links.list.html",
                    controller: "Links as vm"
                }
            },
            data: { requireAuth: true }
        });
    }]);

})();
