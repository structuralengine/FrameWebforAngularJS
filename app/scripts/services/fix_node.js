'use strict'

/**
 * @ngdoc service
 * @name webframe.fix_node
 * @description
 * # fix_node
 * Factory in the webframe.
 */
angular.module('webframe')
    .factory('Fix_node', ['$lowdb', '$injector', '$filter', 'LowResource', 'fixNodeDefaults', 'fixNodeConfig', 'HtHelper',
       function ($lowdb, $injector, $filter, LowResource, fixNodeDefaults, fixNodeConfig, HtHelper) {

            let Fix_node = LowResource({
                'table': 'fix_nodes',
                'defaultEntries': fixNodeDefaults
            });

            Fix_node.afterChange = function (changes, source) {
                let hot = this;
                changes.forEach(function (change) {
                    let [row, prop, oldVal, newVal] = change;
                    let fix_node = hot.getSourceDataAtRow(row);
                });
                SendDataToUnity('fix_nodes');
            };

            _.mixin(Fix_node, HtHelper);
            Fix_node.htInit(fixNodeConfig);
            
            return Fix_node;
       } 
    ]);