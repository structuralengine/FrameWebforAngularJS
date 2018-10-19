'use strict';
/**
 * @ngdoc overview
 * @name webframe:routes
 * @description
 * # routes.js
 *
 */
angular.module('webframe')

  // credits for this idea: https://groups.google.com/forum/#!msg/angular/dPr9BpIZID0/MgWVluo_Tg8J
  // unfortunately, a decorator cannot be use here because they are not applied until after
  // the .config calls resolve, so they can't be used during route configuration, so we have
  // to hack it directly onto the $routeProvider object
  .config(['$routeProvider', 'SECURED_ROUTES', function ($routeProvider, SECURED_ROUTES) {
    $routeProvider.whenAuthenticated = function (path, route) {
      route.resolve = route.resolve || {};
      route.resolve.user = ['Auth', function (Auth) {
        return Auth.$requireSignIn();
      }];
      $routeProvider.when(path, route);
      SECURED_ROUTES[path] = true;
      return $routeProvider;
    };
  }])  

  // configure views; whenAuthenticated adds a resolve method to ensure users authenticate
  // before trying to access that route
  .config(['$routeProvider', function ($routeProvider) {
    $routeProvider
      .when('/login', {
        templateUrl: 'views/login.html',
        controller: 'LoginCtrl'
      })
      .whenAuthenticated('/account', {
        templateUrl: 'views/account.html',
        controller: 'AccountCtrl'
      })      
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
      .when('/panels', {
        templateUrl: 'views/panels.html',
        controller: 'PanelsCtrl',
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
      .when('/disg', {
        templateUrl: 'views/disg.html',
        controller: 'DisgCtrl',
        controllerAs: 'ctrl',
      })
      .when('/fsec', {
        templateUrl: 'views/fsec.html',
        controller: 'FsecCtrl',
        controllerAs: 'ctrl',
      })
      .when('/comb.fsec', {
        templateUrl: 'views/comb.fsec.html',
        controller: 'CombFsecCtrl',
        controllerAs: 'ctrl',
      })
      .when('/pic.fsec', {
        templateUrl: 'views/pic.fsec.html',
        controller: 'PicFsecCtrl',
        controllerAs: 'ctrl',
      }) 
      .when('/reac', {
        templateUrl: 'views/reac.html',
        controller: 'ReacCtrl',
        controllerAs: 'ctrl',
      })       
      .otherwise({redirectTo: '/'});
  }])

  /**
   * Apply some route security. Any route's resolve method can reject the promise with
   * "AUTH_REQUIRED" to force a redirect. This method enforces that and also watches
   * for changes in auth status which might require us to navigate away from a path
   * that we can no longer view.
   */
  .run(['$rootScope', '$location', 'Auth', 'SECURED_ROUTES', 'loginRedirectPath', '$window',
    function ($rootScope, $location, Auth, SECURED_ROUTES, loginRedirectPath, $window) {
      // watch for login status changes and redirect if appropriate
      Auth.$onAuthStateChanged(check);

      $rootScope.$on('$routeChangeStart', function (event, next, current) {
        $window.SendModeToUnity(next.templateUrl);
      });

      // some of our routes may reject resolve promises with the special {authRequired: true} error
      // this redirects to the login page whenever that is encountered
      $rootScope.$on('$routeChangeError', function (e, next, prev, err) {
        if (err === 'AUTH_REQUIRED') {
          $location.path(loginRedirectPath);
        }
      });

      function check(user) {
        if (!user && authRequired($location.path())) {
          $location.path(loginRedirectPath);
        }
      }

      function authRequired(path) {
        return SECURED_ROUTES.hasOwnProperty(path);
      }
    }
  ])

  // used by route security
  .constant('SECURED_ROUTES', {});