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

        // Unityで選択されたオブジェクトの行をフォーカス
        $scope.setRow = function (row){
            $scope.getHot().selectRows(row);
        }
        
        // 表のインスタンスを得る
        $scope.getHot = function () {
            var mode_name = GetModeName(location.hash);
            if (mode_name == '') {
                mode_name = 'nodes'; // デフォルトの設定
            }
            return hotRegisterer.getInstance(mode_name);
        }
    }]);