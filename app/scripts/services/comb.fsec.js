'use strict'

/**
 * @ngdoc service
 * @name webframe.comb.fsec
 * @description
 * # comb.fsec
 * Factory in the webframe.
 */
angular.module('webframe')
    .factory('CombFsec', ['$lowdb', '$injector', '$filter', 'LowResource', 'combFsecDefaults', 'combFsecConfig', 'HtHelper',
       function ($lowdb, $injector, $filter, LowResource, combFsecDefaults, combFsecConfig, HtHelper) {

            let CombFsec = LowResource({
                'table': 'combFsecs',
                'defaultEntries': combFsecDefaults
            });

            CombFsec.afterChange = function (changes, source) {
                let hot = this;
                changes.forEach(function (change) {
                    let [row, prop, oldVal, newVal] = change;
                    let combFsec = hot.getSourceDataAtRow(row);
                });
                SendDataToUnity('comb.fsec');
            };

            _.mixin(CombFsec, HtHelper);
            CombFsec.htInit(combFsecConfig);
            
            return CombFsec;
       } 
    ]);