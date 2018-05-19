'use strict';

/**
 * @ngdoc service
 * @name webframe.nodeConfig
 * @description
 * # nodeConfig
 * Factory in the webframe.
 */
angular.module('webframe')
  .factory('nodeConfig', ['HtHelper', 'htSpeedInput',
    function(HtHelper, htSpeedInput) {

      return {     
        'X': {
          'column': {
            'data': 'x',
            'type': 'numeric',
            'format': '0.001'
          }
        },
        'Y': {
          'column': {
            'data': 'y',
            'type': 'numeric',
            'format': '0.001'
          }
        },
        'Z': {
          'column': {
            'data': 'z',
            'type': 'numeric',
            'format': '0.001'
          }
        }
      };
    }
  ]);
