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

            Disg.afterChange = function (changes, source) {
                let hot = this;
                changes.forEach(function (change) {
                    let [row, prop, oldVal, newVal] = change;
                    let disg = hot.getSourceDataAtRow(row);
                });
                SendJsonToUnity();
                SendUnity('input mode change @ disg');
            };

            _.mixin(Disg, HtHelper);
            Disg.htInit(disgConfig);
            
            return Disg;
       } 
    ]);