'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:CombinesCtrl
 * @description
 * # CombinesCtrl
 * Controller of the webframe
 */

angular.module('webframe')
.controller('CombinesCtrl', ['$scope', '$filter', '$q', 'Combine', 'combineDefaults',
    function ($scope, $filter, $q, Combine, combineDefaults) {
        let ctrl = this;

        var element = document.getElementById('popupConfigElement');
        $scope = angular.element(element).scope();

        $scope.$on('reload', function (e) {
            init();
        });

        function init() {
            elementsMode = false;
            let combines = Combine.query();

            if (combines.length == 0) {
                Combine.createDefaultEntries();
                combines = Combine.query();
            }

            ctrl.combines = combines;
            ctrl.settings = Combine.settings;

            $scope.ngPopupConfig = {
                width: 1200,
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
            }, 100);
        }
        init();
    }
]);