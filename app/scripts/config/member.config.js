'use strict';

/**
 * @ngdoc service
 * @name webframe.memberConfig
 * @description
 * # memberConfig
 * Factory in the webframe.
 */
angular.module('webframe')
  .factory('memberConfig', ['HtHelper', 'htSpeedInput',
    function(HtHelper, htSpeedInput) {

      return {
        '部材番号': {
          'en': 'Member No',
          'column': {
            'data': 'm_no',
            'type': 'numeric',
            'format': '0'
          },
          items: {
            '': {}
          }
        },
        '節点i': {
          'en': 'i Node no',
          'column': {
            'data': 'ni',
            'type': 'numeric',
            'format': '0'
          },
          items: {
            '': {}
          }
        },
        '節点j': {
          'en': 'j Node no',
          'column': {
            'data': 'nj',
            'type': 'numeric',
            'format': '0'
          },
          items: {
            '': {}
          }
        },
        '部材長': {
          'en': 'Member length',
          'column': {
            'data': 'm_len',
            'type': 'numeric',
            'format': '0.001'
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
