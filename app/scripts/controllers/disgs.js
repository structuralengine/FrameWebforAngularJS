'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:DisgCtrl
 * @description
 * # DisgCtrl
 * Controller of the webframe
 */

// angular.module('webframe')
//     .controller('DisgCtrl', ['$scope', '$window', '$lowdb',
//         function ($scope, $window, $lowdb) {
//             $scope.testDisg = function () {
//                 alert('メニュー「disg」');
//             };
//         }
//     ]);


angular.module('webframe')
    .controller('DisgCtrl', ['$scope', '$filter', '$q', 'Disg', 'disgDefaults',
    function ($scope, $filter, $q, Disg, disgDefaults) {
        let ctrl = this;

        var element = document.getElementById('popupConfigElement');
        $scope = angular.element(element).scope();

        $scope.$on('reload', function (e) {
        init();
        });

        function init() {
        elementsMode = false;
        let disgs = Disg.query();

        if (disgs.length == 0) {
            Disg.createDefaultEntries();
            disgs = Disg.query();
        }

        ctrl.disgs = disgs;
        ctrl.settings = Disg.settings;

        // 以下のオプションでポップアップのサイズ、初期位置等、タイトルバーの表示有無等を設定します
        $scope.ngPopupConfig = {
            width: 400,
            height: 600,
            resizable: true,
            draggable: true,
            position: {
                top: 135,
                left: 15
            },
            title: '変位',
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