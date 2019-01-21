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

        // テーブルのIDを設定
        $scope.$parent.hotId = 'pickups';

        $scope.$on('reload', function (e) {
            init();
        });

        // 表が変化した
        function afterChange(change) {
            $scope.$parent.fsec_pickups = ctrl.pickups;
            if (GetModeName(location.hash) == 'pickups') $scope.$parent.culcFsec();
        }

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
                width: 600,
                height: 630,
                resizable: false,
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
            // エクセル表が若干はみ出しているため、それの調整
            setTimeout(function () {
                var content = $('.ht_master');
                content.css('height', '490px');

                // init時点だとまだ表インスタンスが出来ていない場合があるのでここでやる
                Handsontable.hooks.add('afterChange', afterChange, $scope.$parent.getHot());
                afterChange(null);
            }, 100);
        }
        init();
    }
]);