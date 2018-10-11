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

            Fsec.afterChange = function (changes, source) {
                let hot = this;
                changes.forEach(function (change) {
                    let [row, prop, oldVal, newVal] = change;
                    let fsec = hot.getSourceDataAtRow(row);
                });
                SendJsonToUnity();
                SendUnity('input mode change @ fsec');
            };

            _.mixin(Fsec, HtHelper);
            Fsec.htInit(fsecConfig);
            
            return Fsec;
       } 
    ]);