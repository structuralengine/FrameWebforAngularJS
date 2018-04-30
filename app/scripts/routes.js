'use strict';
/**
 * @ngdoc overview
 * @name webframe:routes
 * @description
 * # routes.js
 *
 */
angular.module('webframe')
  .config(['$routeProvider', function($routeProvider) {
    $routeProvider
      .when('/', {
        templateUrl: 'views/basic-information.html',
        controller: 'BasicInformationCtrl',
        controllerAs: 'ctrl',
      })
      .when('/members', {
        templateUrl: 'views/members.html',
        controller: 'MembersCtrl',
        controllerAs: 'ctrl',
      })
      .otherwise({redirectTo: '/'});
  }]);
