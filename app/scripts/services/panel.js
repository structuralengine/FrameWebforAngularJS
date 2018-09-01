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
        SendUnity(CreateJson($lowdb));
        SendUnity('input mode change:panel');
      };
      function CreateJson (lowdb) {
        const collection = lowdb;
        let sendJson = '{';

        sendJson += '"panel":';
        sendJson += JSON.stringify(collection.get('panels').value());

        sendJson += '}';
        
        console.log(sendJson);

        return sendJson;
      }

      _.mixin(Panel, HtHelper);
      Panel.htInit(panelConfig);

      return Panel;
    }
  ]);
