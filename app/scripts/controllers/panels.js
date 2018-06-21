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

      $scope.$on('reload', function (e) {
        init();
      });

      function init() {
        let panels = Panel.query();

        if (panels.length == 0) {
          Panel.createDefaultEntries();
          panels = Panel.query();
        }

        ctrl.panels = panels;
        ctrl.settings = Panel.settings;
      }

      init();
    }
  ]);
