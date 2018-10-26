'use strict';

/**
 * @ngdoc service
 * @name webframe.fsecConfig
 * @description
 * # fsecConfig
 * Factory in the webframe.
 */

angular.module('webframe')
  .factory('fsecConfig', ['HtHelper', 'htSpeedInput',
    function(HtHelper, htSpeedInput) {

      return {
        '節点': {
            'column': {
              readOnly: true,
              'data': 'n',
              'type': 'numeric',
              'format': '0.001'
            }
        },
        '着目点': {
            'column': {
              readOnly: true,
              'data': 'np',
              'type': 'numeric',
              'format': '0.001'
            }
        },
        'Fx': {
          'column': {
            readOnly: true,
            'data': 'fx',
            'type': 'numeric',
            'format': '0.001'
          }
        },
        'Fy': {
          'column': {
            readOnly: true,
            'data': 'fy',
            'type': 'numeric',
            'format': '0.001'
          }
        },
        'Fz': {
          'column': {
            readOnly: true,
            'data': 'fz',
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