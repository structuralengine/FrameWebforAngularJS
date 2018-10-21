'use strict';

/**
 * @ngdoc service
 * @name webframe.Panel
 * @description
 * # Panel
 * Factory in the webframe.
 */

angular.module('webframe')
  .factory('Panel', ['$lowdb', '$injector', '$filter', 'LowResource', 'panelDefaults', 'panelConfig', 'HtHelper',
    function ($lowdb, $injector, $filter, LowResource, panelDefaults, panelConfig, HtHelper) {

      let Panel = LowResource({
        'table': 'panels',
        'defaultEntries': panelDefaults
      });

      Panel.afterChange = function (changes, source) {
        let hot = this;
        changes.forEach(function (change) {
          let [row, prop, oldVal, newVal] = change;
          let Panel = hot.getSourceDataAtRow(row);
        });
        SendDataToUnity('panels');
      };

      _.mixin(Panel, HtHelper);
      Panel.htInit(panelConfig);

      return Panel;
    }
  ]);