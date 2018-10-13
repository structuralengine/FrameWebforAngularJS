'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:ElementsCtrl
 * @description
 * # ElementsCtrl
 * Controller of the webframe
 */
angular.module('webframe')
    .controller('ElementsCtrl', ['$scope', '$filter', '$q', 'Element', 'elementDefaults',
        function ($scope, $filter, $q, Element, elementDefaults) {
            let ctrl = this;

            var element = document.getElementById('popupConfigElement');
            $scope = angular.element(element).scope();      

            $scope.$on('reload', function (e) {
                init();
            });

            function init() {
                elementsMode = true;
                let elements = Element.query();

                if (elements.length == 0) {
                    Element.createDefaultEntries();
                    elements = Element.query();
                }

                ctrl.elements = elements;
                ctrl.settings = Element.settings;

                // 以下のオプションでポップアップのサイズ、初期位置等、タイトルバーの表示有無等を設定します
                $scope.ngPopupConfig = {
                    width: 1200,
                    height: 450,
                    resizable: true,
                    draggable: true,
                    position: {
                        top: 135,
                        left: 15
                    },
                    title: '材料',
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