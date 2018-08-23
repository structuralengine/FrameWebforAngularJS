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
                SendUnity(Node.CreateJson($lowdb));
                SendUnity('input mode change:element');        
            };
            Node.CreateJson = function (lowdb) {
                const collection = lowdb;
                let sendJson = '{';
        
                sendJson += ',"element":';
                sendJson += JSON.stringify(collection.get('elements').value());
        
                sendJson += '}';
                
                console.log(sendJson);
        
                return sendJson;
              }
        
            _.mixin(Element, HtHelper);
            Element.htInit(elementConfig);

            return Element;
        }
    ]);
