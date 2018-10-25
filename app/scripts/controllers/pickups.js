'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:PickupsCtrl
 * @description
 * # PickupsCtrl
 * Controller of the webframe
 */

angular.module('webframe')
.controller('PickupsCtrl', ['$scope', '$filter', '$q', 'Pickup', 'pickupDefaults',
    function ($scope, $filter, $q, Pickup, pickupDefaults) {
        let ctrl = this;

        var element = document.getElementById('popupConfigElement');
        $scope = angular.element(element).scope();

        $scope.$on('reload', function (e) {
            init();
        });

        function init() {
            elementsMode = false;
            let pickups = Pickup.query();

            if (pickups.length == 0) {
                Pickup.createDefaultEntries();
                pickups = Pickup.query();
            }

            ctrl.pickups = pickups;
            ctrl.settings = Pickup.settings;

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