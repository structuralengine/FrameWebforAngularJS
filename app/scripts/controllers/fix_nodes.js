'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:FixNodesCtrl
 * @description
 * # FixNodesCtrl
 * Controller of the webframe
 */
angular.module('webframe')
    .controller('FixNodesCtrl', ['$scope', '$filter', '$q', 'Fix_node', 'fixNodeDefaults',
        function ($scope, $filter, $q, Fix_node, fixNodeDefaults) {
            let ctrl = this;

            var element = document.getElementById('popupConfigElement');
            $scope = angular.element(element).scope();

            $scope.$on('reload', function (e) {
                init();
            });

            function init() {
                elementsMode = false;
                let fix_nodes = Fix_node.query();

                if (fix_nodes.length == 0) {
                    Fix_node.createDefaultEntries();
                    fix_nodes = Fix_node.query();
                }

                ctrl.fix_nodes = fix_nodes;
                Fix_node.settings['rowHeaders'] = false;
                ctrl.settings = Fix_node.settings;

                $scope.ngPopupConfig = {
                    width: 1050,
                    height: 630,
                    resizable: false,
                    draggable: true,
                    position:{
                        top: 135,
                        left: 15
                    },
                    title: '支点',
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
                    content.css('height', '560px');
                }, 100);
            }

            init();
        }
    ]);