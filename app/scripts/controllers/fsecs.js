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

        // テーブルのIDを設定
        $scope.$parent.hotId = 'fsecs';

        $scope.$on('reload', function (e) {
        init();
        });

        function init() {
            elementsMode = false;

            // データ保存の無いテーブルなので空データを初期値とする
            ctrl.fsecs = fsecDefaults;
            ctrl.settings = Fsec.settings;

            // 計算結果を表示する
            angular.forEach($scope.result_fsecs, function(value, key) {
                angular.forEach(value, function(_value, _key) {
                    ctrl.fsecs[key-1][_key] = _value;
                });
            });

            // 以下のオプションでポップアップのサイズ、初期位置等、タイトルバーの表示有無等を設定します
            $scope.ngPopupConfig = {
                width: 500,
                height: 630,
                resizable: false,
                draggable: true,
                position: {
                    top: 135,
                    left: 15
                },
                title: '基本荷重断面力',
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