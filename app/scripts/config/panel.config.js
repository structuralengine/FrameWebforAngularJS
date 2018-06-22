'use strict';

/**
 * @ngdoc service
 * @name webframe.panelConfig
 * @description
 * # panelConfig
 * Factory in the webframe.
 */
angular.module('webframe')
  .factory('panelConfig', ['HtHelper', 'htSpeedInput',
    function(HtHelper, htSpeedInput) {

      return {     
        '構成節点No': {
          en: 'Node Nombers',
          items: {
            'No1': {
              column: {
                data: 'no1',
                type: 'numeric',
                'format': '0'
              }
            },
            'No2': {
              column: {
                data: 'no2',
                type: 'numeric',
                'format': '0'
              }
            },
            'No3': {
              column: {
                data: 'no3',
                type: 'numeric',
                'format': '0'
              }
            }
          }
        },
        '面積': {
          en: 'Area',
          'column': {
            'data': 'area',
            'type': 'numeric',
            'format': '0.001',
            readOnly: true
          },
          items: {
            '': {}
          }
        },
        '材料 No': {
          'en': 'Element no',
            'column': {
              'data': 'e',
              'type': 'numeric',
              'format': '0'
            },
          items: {
            '': {}
          }
        }
      };
    }
  ]);
