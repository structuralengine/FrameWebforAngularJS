'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:ReacCtrl
 * @description
 * # ReacCtrl
 * Controller of the webframe
 */

// angular.module('webframe')
//     .controller('ReacCtrl', ['$scope', '$window', '$lowdb',
//         function ($scope, $window, $lowdb) {
//             $scope.testReac = function () {
//                 alert('メニュー「reac」');
//             };
//         }
//     ]);


angular.module('webframe')
  .controller('ReacCtrl', ['$scope', '$filter', '$q', 'Reac', 'reacDefaults',
    function ($scope, $filter, $q, Reac, reacDefaults) {
      let ctrl = this;

      var element = document.getElementById('popupConfigElement');
      $scope = angular.element(element).scope();

      $scope.$on('reload', function (e) {
        init();
      });

      function init() {
        elementsMode = false;
        let reacs = Reac.query();

        if (reacs.length == 0) {
          Reac.createDefaultEntries();
          reacs = Reac.query();
        }

        ctrl.reacs = reacs;
        ctrl.settings = Reac.settings;

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
            title: '支点反力',
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