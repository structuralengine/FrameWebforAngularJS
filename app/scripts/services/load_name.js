'use strict'

/**
 * @ngdoc service
 * @name webframe.loadName
 * @description
 * # loadName
 * Factory in the webframe.
 */

angular.module('webframe')
    .factory('Load_name', ['$lowdb', '$injector', '$filter', 'LowResource', 'loadNameDefaults', 'loadNameConfig', 'HtHelper',
       function ($lowdb, $injector, $filter, LowResource, loadNameDefaults, loadNameConfig, HtHelper) {

            let Load_name = LowResource({
                'table': 'load_names',
                'defaultEntries': loadNameDefaults
            });

            Load_name.afterChange = function (changes, source) {
                let hot = this;
                changes.forEach(function (change) {
                    let [row, prop, oldVal, newVal] = change;
                    let load_name = hot.getSourceDataAtRow(row);
                });
                let jsonObj = $lowdb.get('load_names').value();
                SendDataToUnity('load_names', jsonObj)
            };

            _.mixin(Load_name, HtHelper);
            Load_name.htInit(loadNameConfig);
            
            return Load_name;
       } 
    ]);