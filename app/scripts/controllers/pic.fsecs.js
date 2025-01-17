'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:PicFsecCtrl
 * @description
 * # PicFsecCtrl
 * Controller of the webframe
 */

// angular.module('webframe')
//     .controller('PicFsecCtrl', ['$scope', '$window', '$lowdb',
//         function ($scope, $window, $lowdb) {
//             $scope.testPicFsec = function () {
//                 alert('メニュー「pic.fsec」');
//             };
//         }
//     ]);


angular.module('webframe')
.controller('PicFsecCtrl', ['$scope', '$filter', '$q', 'PicFsec', 'picFsecDefaults',
function ($scope, $filter, $q, PicFsec, picFsecDefaults) {
    let ctrl = this;

    var element = document.getElementById('popupConfigElement');
    $scope = angular.element(element).scope();

    $scope.$on('reload', function (e) {
    init();
    });

    function init() {
        elementsMode = false;
        let picFsecs = PicFsec.query();

        if (picFsecs.length == 0) {
            PicFsec.createDefaultEntries();
            picFsecs = PicFsec.query();
        }

        ctrl.picFsecs = picFsecs;
        ctrl.settings = PicFsec.settings;
        ctrl.picFsec = picFsecDefaults;

        // データ保存の無いテーブルなので空データを初期値とする
        ctrl.picFsecs = picFsecDefaults;
        ctrl.settings = PicFsec.settings;

        // 計算結果を表示する
        angular.forEach($scope.result_pic_feacs, function(value, key) {
            angular.forEach(value, function(_value, _key) {
                ctrl.picFsecs[key][_key] = _value;
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
            title: 'ピックアップ断面力',
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