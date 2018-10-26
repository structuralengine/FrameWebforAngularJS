'use strict';

/**
 * @ngdoc service
 * @name webframe.reacConfig
 * @description
 * # reacConfig
 * Factory in the webframe.
 */

angular.module('webframe')
  .factory('reacConfig', ['HtHelper', 'htSpeedInput',
    function(HtHelper, htSpeedInput) {

      return {     
        'Rx': {
          'column': {
            readOnly: true,
            'data': 'rx',
            'type': 'numeric',
            'format': '0.001'
          }
        },
        'Ry': {
          'column': {
            readOnly: true,
            'data': 'ry',
            'type': 'numeric',
            'format': '0.001'
          }
        },
        'Rz': {
          'column': {
            readOnly: true,
            'data': 'rz',
            'type': 'numeric',
            'format': '0.001'
          }
        },
        'Mx': {
            'column': {
              readOnly: true,
              'data': 'mx',
              'type': 'numeric',
              'format': '0.001'
            }
          },
          'My': {
            'column': {
              readOnly: true,
              'data': 'my',
              'type': 'numeric',
              'format': '0.001'
            }
          },
          'Mz': {
            'column': {
              readOnly: true,
              'data': 'mz',
              'type': 'numeric',
              'format': '0.001'
            }
          }
        };
    }
]);