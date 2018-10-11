'use strict';

/**
 * @ngdoc service
 * @name webframe.disgConfig
 * @description
 * # disgConfig
 * Factory in the webframe.
 */

angular.module('webframe')
  .factory('disgConfig', ['HtHelper', 'htSpeedInput',
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
        },
        'argX': {
            'column': {
              'data': 'x',
              'type': 'numeric',
              'format': '0.001'
            }
          },
          'argY': {
            'column': {
              'data': 'y',
              'type': 'numeric',
              'format': '0.001'
            }
          },
          'argZ': {
            'column': {
              'data': 'z',
              'type': 'numeric',
              'format': '0.001'
            }
          }
        };
    }
]);