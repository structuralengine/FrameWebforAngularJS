'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:JointsCtrl
 * @description
 * # JointsCtrl
 * Controller of the webframe
 */
angular.module('webframe')
    .controller('JointsCtrl', ['$scope', '$filter', '$q', 'Joint', 'jointDefaults',

        function ($scope, $filter, $q, Joint, jointDefaults) {
            let ctrl = this;

            var joint =  document.getElementById('popupConfigElement');
            $scope = angular.element(joint).scope();

            $scope.$on('reload', function (e) {
                init();
            });

            function init() {
                //jointsMode = true;
                let joints = Joint.query();

                if (joints.length == 0) {
                    Joint.createDefaultEntries();
                    joints = Joint.query();
                }

                ctrl.joints = joints;
                ctrl.settings = Joint.settings;

                $scope.ngPopupConfig = {
                    width: 850,
                    height: 450,
                    resizable: true,
                    draggable: true,
                    position:{
                        top: 135,
                        left: 15
                    },
                    title: '結合',
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