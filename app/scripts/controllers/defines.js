'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:DefinesCtrl
 * @description
 * # DefinesCtrl
 * Controller of the webframe
 */

angular.module('webframe')
.controller('DefinesCtrl', ['$scope', '$filter', '$q', 'Define', 'defineDefaults',
    function ($scope, $filter, $q, Define, defineDefaults) {
        let ctrl = this;

        var element = document.getElementById('popupConfigElement');
        $scope = angular.element(element).scope();

        $scope.$on('reload', function (e) {
            init();
        });

        function init() {
            elementsMode = false;
            let defines = Define.query();

            if (defines.length == 0) {
                Define.createDefaultEntries();
                defines = Define.query();
            }

            ctrl.defines = defines;
            ctrl.settings = Define.settings;

            $scope.ngPopupConfig = {
                width: 400,
                height: 450,
                resizable: true,
                draggable: true,
                position:{
                    top: 135,
                    left: 15
                },
                title: '組合せ',
                hasTitleBar: true,
                pinned: false,
                isShow: true,
                onOpen: function () { },
                onClose: function () { },
                onDragStart: function () { },
                onDragEnd: function () { },
                onResize: function () { }
            }
        }

        init();
    }
]);