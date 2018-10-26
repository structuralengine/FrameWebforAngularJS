'use strict'

/**
 * @ngdoc service
 * @name webframe.pic.fsec
 * @description
 * # pic.fsec
 * Factory in the webframe.
 */
angular.module('webframe')
    .factory('PicFsec', ['$lowdb', '$injector', '$filter', 'LowResource', 'picFsecDefaults', 'picFsecConfig', 'HtHelper',
       function ($lowdb, $injector, $filter, LowResource, picFsecDefaults, picFsecConfig, HtHelper) {

            let PicFsec = LowResource({
                'table': 'picFsecs',
                'defaultEntries': picFsecDefaults
            });
            /* // this table is read only
            PicFsec.afterChange = function (changes, source) {
                let hot = this;
                changes.forEach(function (change) {
                    let [row, prop, oldVal, newVal] = change;
                    let picFsec = hot.getSourceDataAtRow(row);
                });
                let jsonObj = $lowdb.get('pic.fsec').value();
                SendDataToUnity('pic.fsec', jsonObj)
            };
            */
            _.mixin(PicFsec, HtHelper);
            PicFsec.htInit(picFsecConfig);
            
            return PicFsec;
       } 
    ]);