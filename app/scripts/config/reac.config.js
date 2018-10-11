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
            'data': 'rx',
            'type': 'numeric',
            'format': '0.001'
          }
        },
        'Ry': {
          'column': {
            'data': 'ry',
            'type': 'numeric',
            'format': '0.001'
          }
        },
        'Rz': {
          'column': {
            'data': 'rz',
            'type': 'numeric',
            'format': '0.001'
          }
        },
        'Mx': {
            'column': {
              'data': 'mx',
              'type': 'numeric',
              'format': '0.001'
            }
          },
          'My': {
            'column': {
              'data': 'my',
              'type': 'numeric',
              'format': '0.001'
            }
          },
          'Mz': {
            'column': {
              'data': 'mz',
              'type': 'numeric',
              'format': '0.001'
            }
          }
        };
    }
]);