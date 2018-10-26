'use strict';

/**
 * @ngdoc service
 * @name webframe.element
 * @description
 * # element
 * Factory in the webframe.
 */
angular.module('webframe')
    .factory('Element', ['$lowdb', '$injector', '$filter', 'LowResource', 'elementDefaults', 'elementConfig', 'HtHelper',
        function ($lowdb, $injector, $filter, LowResource, elementDefaults, elementConfig, HtHelper) {

            let Element = LowResource({
                'table': 'elements',
                'defaultEntries': elementDefaults
            });

            Element.afterChange = function (changes, source) {
                let hot = this;
                changes.forEach(function (change) {
                    let [row, prop, oldVal, newVal] = change;
                    let element = hot.getSourceDataAtRow(row);
                });
                let jsonObj = $lowdb.get('elements').value();
                SendDataToUnity('elements', jsonObj)
            };
        
            _.mixin(Element, HtHelper);
            Element.htInit(elementConfig);

            return Element;
        }
    ]);
