'use strict';

/**
 * @ngdoc service
 * @name webframe.node
 * @description
 * # node
 * Factory in the webframe.
 */
angular.module('webframe')
  .factory('node', ['LowResource', 'nodeConfig',
    function (LowResource, nodeConfig) {

      let node = LowResource({
        table: 'node',
      });

      return node;
    }
  ]);