'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:MenuCtrl
 * @description
 * # MenuCtrl
 * Controller of the webframe
 */
angular.module('webframe')
  .controller('MenuCtrl', ['$scope', '$window', '$http', '$rootScope', '$lowdb', '$log', '$injector', 'msgConfig', 'appConfig',
    function ($scope, $window, $http, $rootScope, $lowdb, $log, $injector, msgConfig, appConfig) {
      let menu = this;
      let resource;

      menu.createNewBucket = function() {
        $lowdb.clear();
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
          'CombFsec',
          'Combine',
          'Define',
          'Disg',
          'Fix_member',
          'Fix_node',
          'Fsec',
          'Joint',
          'Load',
          'Load_name',
          'Member',
          'Node',
          'Notice_point',
          'Panel',
          'PicFsec',
          'Pickup',
          'Reac'
        ];
        models.forEach(function(model) {
          let Model = $injector.get(model);
          Model.reload();
        });
        $rootScope.$broadcast('reload');
        location.reload();
      }

      menu.saveAsFile = function() {
        try {
          $lowdb.download();
        }
        catch (e) {
          $window.confirm(e);
        }
      };

      // ----------------------------------------
      // 計算ボタン処理
      menu.calculation = function () {
        // $httpでのリクエスト送信
        $window.HttpSendRequest($http);
      };

      // 印刷ボタン処理
      menu.print = function() {
        $window.Print();
      }

    }
  ]);