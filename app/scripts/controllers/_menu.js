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
          'Node',
          'Member',
          'Panel',
          'Element'
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

        // テスト用アカウントでアクセス
        var userName = 'test1105';
        var password = 'test1105';

        var storage = localStorage.getItem('webframe.2').replace('{', '');  // 最初の { を消す

        var json = 'inp_grid=' + '{' + '"username":' + JSON.stringify(userName) + ',"password":' + JSON.stringify(password)+','+storage;

        // $httpでのリクエスト送信
        HttpSendRequest($http, json);
      };

      // 印刷ボタン処理
      menu.print = function() {
        Print();
      }

    }
  ]);