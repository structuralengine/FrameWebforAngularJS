'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:MenuCtrl
 * @description
 * # MenuCtrl
 * Controller of the webframe
 */
angular.module('webframe')
  .controller('MenuCtrl', ['$scope', '$window', '$rootScope', '$lowdb', '$log', '$injector', 'msgConfig', 'appConfig',
    function ($scope, $window, $rootScope, $lowdb, $log, $injector, msgConfig, appConfig) {
      let menu = this;
      let resource;

      menu.createNewBucket = function() {
        appConfig.db.clear();
        reload();
      };

      menu.loadFile = function(file) {
        $lowdb.load(file)
          .then(function () {
            reload();
          }, function (err) {
            $window.alert(err);
          });
      };

      function reload() {
        let models = [
          'Node',
          'Member',
          'Panel'
        ];
        models.forEach(function(model) {
          let Model = $injector.get(model);
          Model.reload();
        });
        $rootScope.$broadcast('reload');
      }

      menu.saveAsFile = function() {
        try {
          let msg = msgConfig.files;
          throw msg.save.failed;
        }
        catch (e) {
          if ($window.confirm(e)) {
            $lowdb.download();
          }
        }

      };
    }
  ]);
