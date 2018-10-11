'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:LoadsCtrl
 * @description
 * # LoadsCtrl
 * Controller of the webframe
 */
// angular.module('webframe')
//     .controller('LoadsCtrl', function () {
//     });

angular.module('webframe')
.controller('LoadsCtrl', ['$scope', '$filter', '$q', 'Load', 'loadDefaults',
    function ($scope, $filter, $q, Load, loadDefaults) {
        let ctrl = this;

        var element = document.getElementById('popupConfigElement');
        $scope = angular.element(element).scope();

        $scope.$on('reload', function (e) {
            init();
        });

        function init() {
            elementsMode = false;
            let loads = Load.query();

            if (loads.length == 0) {
                Load.createDefaultEntries();
                loads = Load.query();
            }

            ctrl.loads = loads;
            ctrl.settings = Load.settings;

            $scope.ngPopupConfig = {
                width: 850,
                height: 450,
                resizable: true,
                draggable: true,
                position:{
                    top: 135,
                    left: 15
                },
                title: '荷重',
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