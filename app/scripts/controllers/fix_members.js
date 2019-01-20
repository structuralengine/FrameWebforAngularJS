'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:FixMembersCtrl
 * @description
 * # FixMembersCtrl
 * Controller of the webframe
 */
// angular.module('webframe')
//     .controller('FixMembersCtrl', function () {
//     });

angular.module('webframe')
    .controller('FixMembersCtrl', ['$scope', '$filter', '$q', 'Fix_member', 'fixMemberDefaults',
        function ($scope, $filter, $q, Fix_member, fixMemberDefaults) {
            let ctrl = this;

            var element = document.getElementById('popupConfigElement');
            $scope = angular.element(element).scope();

            $scope.$on('reload', function (e) {
                init();
            });

            function init() {
                elementsMode = false;
                let fix_members = Fix_member.query();

                if (fix_members.length == 0) {
                    Fix_member.createDefaultEntries();
                    fix_members = Fix_member.query();
                }

                ctrl.fix_members = fix_members;
                Fix_member.settings['rowHeaders'] = false;
                ctrl.settings = Fix_member.settings;

                $scope.ngPopupConfig = {
                    width: 750,
                    height: 630,
                    resizable: false,
                    draggable: true,
                    position:{
                        top: 135,
                        left: 15
                    },
                    title: 'バネ',
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