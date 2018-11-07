'use strict'

/**
 * @ngdoc service
 * @name webframe.load
 * @description
 * # load
 * Factory in the webframe.
 */
angular.module('webframe')
    .factory('Load', ['$lowdb', '$injector', '$filter', 'LowResource', 'loadDefaults', 'loadConfig', 'HtHelper',
       function ($lowdb, $injector, $filter, LowResource, loadDefaults, loadConfig, HtHelper) {

            let Load = LowResource({
                'table': 'loads',
                'defaultEntries': loadDefaults
            });

            Load.afterChange = function (changes, source) {
                let hot = this;
                changes.forEach(function (change) {
                    let [row, prop, oldVal, newVal] = change;
                    let load = hot.getSourceDataAtRow(row);
                });
                let namesObj = $lowdb.get('load_names').value();
                let loadsObj = $lowdb.get('loads').value();
                let jsonObj = {};
                jsonObj['load_names'] = namesObj;
                jsonObj['loads'] = loadsObj;
                SendDataToUnity('loads', jsonObj)
            };

            _.mixin(Load, HtHelper);
            Load.htInit(loadConfig);
            
            return Load;
       } 
    ]);