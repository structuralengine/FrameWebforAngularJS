'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:NodesCtrl
 * @description
 * # NodesCtrl
 * Controller of the webframe
 */
angular.module('webframe')
  .controller('NodesCtrl', ['$scope', '$filter', '$q', 'Node', 'nodeDefaults',
    function ($scope, $filter, $q, Node, nodeDefaults) {
      let ctrl = this;

      var element = document.getElementById('popupConfigElement');
      $scope = angular.element(element).scope();

      $scope.$on('reload', function (e) {
        init();
      });

      function init() {
        elementsMode = false;
        let nodes = Node.query();

        if (nodes.length == 0) {
          Node.createDefaultEntries();
          nodes = Node.query();
        }

        ctrl.nodes = nodes;
        ctrl.settings = Node.settings;

        // 以下のオプションでポップアップのサイズ、初期位置等、タイトルバーの表示有無等を設定します
        $scope.ngPopupConfig = {
            width: 230,
            height: 600,
            resizable: true,
            draggable: true,
            position: {
                top: 135,
                left: 15
            },
            title: '格点',
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
