'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:MenuCtrl
 * @description
 * # MenuCtrl
 * Controller of the webframe
 */
angular.module('webframe')
  .controller('MenuCtrl', ['$scope', '$window', '$rootScope', '$log', '$injector', 'moment',
    function ($scope, $window, $rootScope, $log, $injector, moment) {
      let menu = this;
      let resource;

      menu.createNewBucket = function() {
        reload();
      };

      menu.loadFile = function(file) {
      };

      function reload() {
        let models = [
          'BasicInformation'
        ];
        models.forEach(function(model) {
          let Model = $injector.get(model);
          Model.reload();
        });
        $rootScope.$broadcast('reload');
      }

      menu.saveAsFile = function() {
        try {
        }
        catch (e) {
        }
      };
    }
  ]);
