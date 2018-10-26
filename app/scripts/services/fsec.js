'use strict'

/**
 * @ngdoc service
 * @name webframe.fsec
 * @description
 * # fsec
 * Factory in the webframe.
 */
angular.module('webframe')
    .factory('Fsec', ['$lowdb', '$injector', '$filter', 'LowResource', 'fsecDefaults', 'fsecConfig', 'HtHelper',
       function ($lowdb, $injector, $filter, LowResource, fsecDefaults, fsecConfig, HtHelper) {

            let Fsec = LowResource({
                'table': 'fsecs',
                'defaultEntries': fsecDefaults
            });
            /* // this table is read only
            Fsec.afterChange = function (changes, source) {
                let hot = this;
                changes.forEach(function (change) {
                    let [row, prop, oldVal, newVal] = change;
                    let fsec = hot.getSourceDataAtRow(row);
                });
                let jsonObj = $lowdb.get('fsec').value();
                SendDataToUnity('fsec', jsonObj)
            };
            */
            _.mixin(Fsec, HtHelper);
            Fsec.htInit(fsecConfig);
            
            return Fsec;
       } 
    ]);