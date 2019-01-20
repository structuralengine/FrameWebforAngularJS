'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:PopupCtrl
 * @description
 * # BasicInformationCtrl
 * Controller of the webframe
 */
angular.module('webframe')
    .controller('PopupCtrl', ['$scope','hotRegisterer', function ($scope, hotRegisterer) {
        $scope.ngPopupConfig = {
            modelName: 'myNgPopup',
            template: '<main ng-view></main>',
            resizable: true,
            draggable: true,
            position: {
                top: 135,
                left: 15
            },
            title: '',
            hasTitleBar: true,
            pinned: false,
            isShow: true,
            onOpen: function () { },
            onClose: function () { },
            onDragStart: function () { },
            onDragEnd: function () { },
            onResize: function () { }
        }

        // テーブルのインスタンス名（Node等のクラスから設定する）
        $scope.hotId = '';

        // 計算結果（Object型）
        $scope.result_disgs = null;
        $scope.result_fsecs = null;
        $scope.result_reacs = null;

        // 計算結果を保持する
        $scope.keepCulcResult = function (data) {
            $scope.result_disgs = data['1']['disg'];
            $scope.result_fsecs = data['1']['fsec'];
            $scope.result_reacs = data['1']['reac'];
            $scope.$broadcast('reload');
        }

        // Unityで選択されたオブジェクトの行をフォーカス
        $scope.setRow = function (row){
            hotRegisterer.getInstance($scope.hotId).selectRows(row);
        }
    }]);