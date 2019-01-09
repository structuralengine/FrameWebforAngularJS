'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:LoadNamesCtrl
 * @description
 * # LoadNamesCtrl
 * Controller of the webframe
 */
// angular.module('webframe')
//     .controller('LoadNamesCtrl', function () {
//     });


angular.module('webframe')
.controller('LoadNamesCtrl', ['$scope', '$filter', '$q', 'Load_name', 'loadNameDefaults',
    function ($scope, $filter, $q, Load_name, loadNameDefaults) {
        let ctrl = this;

        var element = document.getElementById('popupConfigElement');
        $scope = angular.element(element).scope();

        $scope.$on('reload', function (e) {
            init();
        });

        function init() {
            elementsMode = false;
            let load_names = Load_name.query();

            if (load_names.length == 0) {
                Load_name.createDefaultEntries();
                load_names = Load_name.query();
            }

            ctrl.load_names = load_names;
            ctrl.settings = Load_name.settings;

            $scope.ngPopupConfig = {
                width: 730,
                height: 630,
                resizable: false,
                draggable: true,
                position:{
                    top: 135,
                    left: 15
                },
                title: '荷重名称',
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