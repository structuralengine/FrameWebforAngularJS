'use strict'

/**
 * @ngdoc service
 * @name webframe.define
 * @description
 * # define
 * Factory in the webframe.
 */

angular.module('webframe')
    .factory('Define', ['$lowdb', '$injector', '$filter', 'LowResource', 'defineDefaults', 'defineConfig', 'HtHelper',
       function ($lowdb, $injector, $filter, LowResource, defineDefaults, defineConfig, HtHelper) {

            let Define = LowResource({
                'table': 'defines',
                'defaultEntries': defineDefaults
            });

            Define.afterChange = function (changes, source) {
                let hot = this;
                changes.forEach(function (change) {
                    let [row, prop, oldVal, newVal] = change;
                    let define = hot.getSourceDataAtRow(row);
                });
                /* // this table is not send unity
                let jsonObj = $lowdb.get('defines').value();
                SendDataToUnity('defines', jsonObj)
                */
            };

            _.mixin(Define, HtHelper);
            Define.htInit(defineConfig);
            
            return Define;
       } 
    ]);