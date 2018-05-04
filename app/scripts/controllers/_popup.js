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
            width: 600,
            height: 500,
            template: '<main ng-view></main>',
            resizable: true,
            draggable: true,
            position: {
                top: 150,
                left: 1
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
    }]);