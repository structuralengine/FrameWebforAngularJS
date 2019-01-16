'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:PopupCtrl
 * @description
 * # BasicInformationCtrl
 * Controller of the webframe
 */
angular.module('webframe')
    .controller('PopupCtrl', ['$scope', function ($scope) {
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

        // #41 select行を変えたら 対象のUnityオブジェクトをフォーカスする
        Handsontable.hooks.add('afterOnCellMouseDown', function() {
            if (arguments[1].col == -1) {   //行選択時のみ動作させる（セル選択時は列ID、行選択時は-1が入る）
                SendSelectItemToUnity(arguments[1].row+1);
            }
        });
    }]);