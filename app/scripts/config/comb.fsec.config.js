'use strict';

/**
 * @ngdoc service
 * @name webframe.comb.fsecConfig
 * @description
 * # comb.fsecConfig
 * Factory in the webframe.
 */

angular.module('webframe')
  .factory('combFsecConfig', ['HtHelper', 'htSpeedInput',
    function(HtHelper, htSpeedInput) {

      return {
        '節点': {
            'column': {
              'data': 'n',
              'type': 'numeric',
              'format': '0.001'
            }
        },
        '着目点': {
            'column': {
                'data': 'np',
                'type': 'numeric',
                'format': '0.001'
            }
        },
        'Fx': {
          'column': {
            'data': 'fx',
            'type': 'numeric',
            'format': '0.001'
          }
        },
        'Fy': {
          'column': {
            'data': 'fy',
            'type': 'numeric',
            'format': '0.001'
          }
        },
        'Fz': {
          'column': {
            'data': 'fz',
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