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
        templateUrl: 'views/nodes.html',
        controller: 'NodesCtrl',
        controllerAs: 'ctrl',
      })
      .when('/fix_nodes', {
        templateUrl: 'views/fix_nodes.html',
        controller: 'FixNodesCtrl',
        controllerAs: 'ctrl',
      })
      .when('/members', {
        templateUrl: 'views/members.html',
        controller: 'MembersCtrl',
        controllerAs: 'ctrl',
      })
      .when('/elements', {
        templateUrl: 'views/elements.html',
        controller: 'ElementsCtrl',
        controllerAs: 'ctrl',
      })
      .when('/joints', {
        templateUrl: 'views/joints.html',
        controller: 'JointsCtrl',
        controllerAs: 'ctrl',
      })
      .when('/notice_points', {
        templateUrl: 'views/notice_points.html',
        controller: 'NoticePointsCtrl',
        controllerAs: 'ctrl',
      })
      .when('/fix_members', {
        templateUrl: 'views/fix_members.html',
        controller: 'FixMembersCtrl',
        controllerAs: 'ctrl',
      })
      .when('/loads', {
        templateUrl: 'views/loads.html',
        controller: 'LoadsCtrl',
        controllerAs: 'ctrl',
      })
      .when('/combines', {
        templateUrl: 'views/combines.html',
        controller: 'CombinesCtrl',
        controllerAs: 'ctrl',
      })
      .otherwise({redirectTo: '/'});
  }]);
