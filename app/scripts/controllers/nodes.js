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

      $scope.$on('reload', function (e) {
        init();
      });

      function init() {
        let nodes = Node.query();

        if (nodes.length == 0) {
          Node.createDefaultEntries();
          nodes = Node.query();
        }

        ctrl.nodes = nodes;
        ctrl.settings = Node.settings;
      }

      init();
    }
  ]);
