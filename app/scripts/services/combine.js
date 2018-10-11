'use strict'

/**
 * @ngdoc service
 * @name webframe.combine
 * @description
 * # combine
 * Factory in the webframe.
 */
angular.module('webframe')
    .factory('Combine', ['$lowdb', '$injector', '$filter', 'LowResource', 'combineDefaults', 'combineConfig', 'HtHelper',
       function ($lowdb, $injector, $filter, LowResource, combineDefaults, combineConfig, HtHelper) {

            let Combine = LowResource({
                'table': 'combines',
                'defaultEntries': combineDefaults
            });

            Combine.afterChange = function (changes, source) {
                let hot = this;
                changes.forEach(function (change) {
                    let [row, prop, oldVal, newVal] = change;
                    let combine = hot.getSourceDataAtRow(row);
                });
                SendJsonToUnity();
                SendUnity('input mode change @ combine');
            };

            _.mixin(Combine, HtHelper);
            Combine.htInit(combineConfig);
            
            return Combine;
       } 
    ]);