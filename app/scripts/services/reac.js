'use strict'

/**
 * @ngdoc service
 * @name webframe.reac
 * @description
 * # reac
 * Factory in the webframe.
 */
angular.module('webframe')
    .factory('Reac', ['$lowdb', '$injector', '$filter', 'LowResource', 'reacDefaults', 'reacConfig', 'HtHelper',
       function ($lowdb, $injector, $filter, LowResource, reacDefaults, reacConfig, HtHelper) {

            let Reac = LowResource({
                'table': 'reacs',
                'defaultEntries': reacDefaults
            });
            /* // this table is read only
            Reac.afterChange = function (changes, source) {
                let hot = this;
                changes.forEach(function (change) {
                    let [row, prop, oldVal, newVal] = change;
                    let reac = hot.getSourceDataAtRow(row);
                });
                let jsonObj = $lowdb.get('reac').value();
                SendDataToUnity('reac', jsonObj)
            };
            */
            _.mixin(Reac, HtHelper);
            Reac.htInit(reacConfig);
            
            return Reac;
       } 
    ]);