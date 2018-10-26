'use strict'

/**
 * @ngdoc service
 * @name webframe.disg
 * @description
 * # disg
 * Factory in the webframe.
 */
angular.module('webframe')
    .factory('Disg', ['$lowdb', '$injector', '$filter', 'LowResource', 'disgDefaults', 'disgConfig', 'HtHelper',
       function ($lowdb, $injector, $filter, LowResource, disgDefaults, disgConfig, HtHelper) {

            let Disg = LowResource({
                'table': 'disgs',
                'defaultEntries': disgDefaults
            });
            /* // this table is read only
            Disg.afterChange = function (changes, source) {
                let hot = this;
                changes.forEach(function (change) {
                    let [row, prop, oldVal, newVal] = change;
                    let disg = hot.getSourceDataAtRow(row);
                });
                let jsonObj = $lowdb.get('disg').value();
                SendDataToUnity('disg', jsonObj)
            };
            */
            _.mixin(Disg, HtHelper);
            Disg.htInit(disgConfig);
            
            return Disg;
       } 
    ]);