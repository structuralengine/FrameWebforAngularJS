'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:PanelsCtrl
 * @description
 * # PanelsCtrl
 * Controller of the webframe
 */
angular.module('webframe')
  .controller('PanelsCtrl', ['$scope', '$filter', '$q', 'Panel', 'panelDefaults',
    function ($scope, $filter, $q, Panel, panelDefaults) {
      let ctrl = this;

      var element = document.getElementById('popupConfigElement');
      $scope = angular.element(element).scope();

      $scope.$on('reload', function (e) {
        init();
      });

      function init() {
        elementsMode = false;
        let panels = Panel.query();

        if (panels.length == 0) {
          Panel.createDefaultEntries();
          panels = Panel.query();
        }

        ctrl.panels = panels;
        ctrl.settings = Panel.settings;

        // 以下のオプションでポップアップのサイズ、初期位置等、タイトルバーの表示有無等を設定します
        $scope.ngPopupConfig = {
            width: 350,
            height: 450,
            resizable: true,
            draggable: true,
            position: {
                top: 135,
                left: 15
            },
            title: 'パネル',
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
