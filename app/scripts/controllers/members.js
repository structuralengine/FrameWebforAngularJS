'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:MembersCtrl
 * @description
 * # MembersCtrl
 * Controller of the webframe
 */
angular.module('webframe')
  .controller('MembersCtrl', ['$scope', '$filter', '$q', 'Member', 'memberDefaults',
    function ($scope, $filter, $q, Member, memberDefaults) {
      let ctrl = this;

      var element = document.getElementById('popupConfigElement');
      $scope = angular.element(element).scope();

      // テーブルのIDを設定
      $scope.$parent.hotId = 'members';

      $scope.$on('reload', function (e) {
        init();
      });

      function init() {
        elementsMode = false;
        let members = Member.query();

        if (members.length == 0) {
          Member.createDefaultEntries();
          members = Member.query();
        }

        ctrl.members = members;
        ctrl.settings = Member.settings;

        // 以下のオプションでポップアップのサイズ、初期位置等、タイトルバーの表示有無等を設定します
        $scope.ngPopupConfig = {
            width: 350,
            height: 630,
            resizable: false,
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
