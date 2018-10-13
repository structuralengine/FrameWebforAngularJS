'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:CombFsecCtrl
 * @description
 * # CombFsecCtrl
 * Controller of the webframe
 */

// angular.module('webframe')
//     .controller('CombFsecCtrl', ['$scope', '$window', '$lowdb',
//         function ($scope, $window, $lowdb) {
//             $scope.testCombFsec = function () {
//                 alert('メニュー「comb.fsec」');
//             };
//         }
//     ]);


angular.module('webframe')
    .controller('CombFsecCtrl', ['$scope', '$filter', '$q', 'CombFsec', 'combFsecDefaults',
    function ($scope, $filter, $q, CombFsec, combFsecDefaults) {
        let ctrl = this;

        var element = document.getElementById('popupConfigElement');
        $scope = angular.element(element).scope();

        $scope.$on('reload', function (e) {
        init();
        });

        function init() {
        elementsMode = false;
        let combFsecs = CombFsec.query();

        if (combFsecs.length == 0) {
            CombFsec.createDefaultEntries();
            combFsecs = CombFsec.query();
        }

        ctrl.combFsecs = combFsecs;
        ctrl.settings = CombFsec.settings;

        // 以下のオプションでポップアップのサイズ、初期位置等、タイトルバーの表示有無等を設定します
        $scope.ngPopupConfig = {
            width: 500,
            height: 600,
            resizable: true,
            draggable: true,
            position: {
                top: 135,
                left: 15
            },
            title: '組み合わせ断面力',
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
        setTimeout(function(){
            var content = $('.ht_master');
            content.css('height', '560px');
        }, 100);
        }

        init();
    }
    ]);    