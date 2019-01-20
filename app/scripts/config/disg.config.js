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
            readOnly: true,
            'data': 'dx',
            'type': 'numeric',
            'format': '0.000'
          }
        },
        'Y': {
          'column': {
            readOnly: true,
            'data': 'dy',
            'type': 'numeric',
            'format': '0.000'
          }
        },
        'Z': {
          'column': {
            readOnly: true,
            'data': 'dz',
            'type': 'numeric',
            'format': '0.000'
          }
        },
        'argX': {
            'column': {
              readOnly: true,
              'data': 'rx',
              'type': 'numeric',
              'format': '0.001'
            }
          },
          'argY': {
            'column': {
              readOnly: true,
              'data': 'ry',
              'type': 'numeric',
              'format': '0.000'
            }
          },
          'argZ': {
            'column': {
              readOnly: true,
              'data': 'rz',
              'type': 'numeric',
              'format': '0.000'
            }
          }
        };
    }
]);