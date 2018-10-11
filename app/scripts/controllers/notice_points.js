'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:NoticePointsCtrl
 * @description
 * # NoticePointsCtrl
 * Controller of the webframe
 */
// angular.module('webframe')
//     .controller('NoticePointsCtrl', function () {
//     });

angular.module('webframe')
    .controller('NoticePointsCtrl', ['$scope', '$filter', '$q', 'Notice_point', 'noticePointDefaults',
        function ($scope, $filter, $q, Notice_point, noticePointDefaults) {
            let ctrl = this;

            var element = document.getElementById('popupConfigElement');
            $scope = angular.element(element).scope();

            $scope.$on('reload', function (e) {
                init();
            });

            function init() {
                elementsMode = false;
                let notice_points = Notice_point.query();

                if (notice_points.length == 0) {
                    Notice_point.createDefaultEntries();
                    notice_points = Notice_point.query();
                }

                ctrl.notice_points = notice_points;
                ctrl.settings = Notice_point.settings;

                $scope.ngPopupConfig = {
                    width: 850,
                    height: 450,
                    resizable: true,
                    draggable: true,
                    position:{
                        top: 135,
                        left: 15
                    },
                    title: '着目点',
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