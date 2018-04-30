'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:TabsCtrl
 * @description
 * # TabsCtrl
 * Controller of the webframe
 */
angular.module('webframe')
  .controller('TabsCtrl', ['$scope', '$rootScope',
    function($scope, $rootScope) {
      let tabs = this;

      $rootScope.$on('$routeChangeSuccess', function(e, $route) {
        //tabs.path = $route.$$route.originalPath.substring(1) || 'basic-information';
      });
    }
  ]);
