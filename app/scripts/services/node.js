'use strict';

/**
 * @ngdoc service
 * @name webframe.node
 * @description
 * # node
 * Factory in the webframe.
 */
angular.module('webframe')
  .factory('Node', ['$lowdb', '$injector', '$filter', 'LowResource', 'nodeDefaults', 'nodeConfig', 'HtHelper',
    function ($lowdb, $injector, $filter, LowResource, nodeDefaults, nodeConfig, HtHelper) {

      let Node = LowResource({
        'table': 'nodes',
        'defaultEntries': nodeDefaults
      });

      Node.afterChange = function (changes, source) {
        let hot = this;
        changes.forEach(function (change) {
          let [row, prop, oldVal, newVal] = change;
          let node = hot.getSourceDataAtRow(row);
        });
        SendUnity(CreateJson($lowdb));
        SendUnity('input mode change:node');
      };
      function CreateJson (lowdb) {
        const collection = lowdb;
        let sendJson = '{';
        sendJson += '"node":';
        sendJson += JSON.stringify(collection.get('nodes').value());

        sendJson += '}';
        
        console.log(sendJson);

        return sendJson;
      }

      _.mixin(Node, HtHelper);
      Node.htInit(nodeConfig);

      return Node;
    }
  ]);
