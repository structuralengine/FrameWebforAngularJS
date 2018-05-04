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
    'angularMoment',
    'local-db',
    'ht-utils',
    'ngHandsontable',
    'ngPopup',
    'ui.bootstrap'
  ])
  .config(['dbConfig', 'appConfig',
    function (dbConfig, appConfig) {
      angular.extend(dbConfig, appConfig.db);
    }
  ]);