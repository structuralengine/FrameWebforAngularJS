'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:FsecCtrl
 * @description
 * # FsecCtrl
 * Controller of the webframe
 */

// angular.module('webframe')
//     .controller('FsecCtrl', ['$scope', '$window', '$lowdb',
//         function ($scope, $window, $lowdb) {
//             $scope.testFsec = function () {
//                 alert('メニュー「fsec」');
//             };
//         }
//     ]);


angular.module('webframe')
    .controller('FsecCtrl', ['$scope', '$filter', '$q', 'Fsec', 'fsecDefaults',
    function ($scope, $filter, $q, Fsec, fsecDefaults) {
        let ctrl = this;

        var element = document.getElementById('popupConfigElement');
        $scope = angular.element(element).scope();

        $scope.$on('reload', function (e) {
        init();
        });

        function init() {
        elementsMode = false;
        let fsecs = Fsec.query();

        if (fsecs.length == 0) {
            Fsec.createDefaultEntries();
            fsecs = Fsec.query();
        }

        ctrl.fsecs = fsecs;
        ctrl.settings = Fsec.settings;

        // 以下のオプションでポップアップのサイズ、初期位置等、タイトルバーの表示有無等を設定します
        $scope.ngPopupConfig = {
            width: 350,
            height: 600,
            resizable: true,
            draggable: true,
            position: {
                top: 135,
                left: 15
            },
            title: '部材',
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