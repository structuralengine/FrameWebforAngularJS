'use strict';

/**
 * @ngdoc service
 * @name webframe.BasicInformation
 * @description
 * # BasicInformation
 * Factory in the webframe.
 */
angular.module('webframe')
  .factory('BasicInformation', ['LowResource', 'basicInformationConfig',
    function (LowResource, basicInformationConfig) {

      let BasicInformation = LowResource({
        table: 'basicInformation',
      });

      return BasicInformation;
    }
  ]);