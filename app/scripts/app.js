'use strict';

/**
 * @ngdoc overview
 * @name webframe
 * @description
 * # FrameWebforJS
 *
 * Main module of the application.
 */
angular.module('webframe', [
  'ngAnimate',
  'ngAria',
  'ngCookies',
  'ngMessages',
  'ngResource',
  'ngRoute',
  'ngSanitize',
  'ngTouch',

  'firebase.ref.app',
  'firebase.auth.app',

  'angularMoment',
  'ngHandsontable',
  'ngPopup',
  'local-db',
  'ht-utils',
  'ui.bootstrap'
])
  .config(['dbConfig', 'appConfig',
    function (dbConfig, appConfig) {
      angular.extend(dbConfig, appConfig.db);
    }
  ]);