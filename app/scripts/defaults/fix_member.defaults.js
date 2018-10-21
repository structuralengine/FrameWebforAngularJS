'use strict';

/**
 * @ngdoc service
 * @name webframe.config/fixMemberDefaults
 * @description
 * # config/fixMemberDefaults
 * Constant in the webframe.
 */

const ROW = 10;                   //表の行数

angular.module('webframe').constant('fixMemberDefaults', Array(ROW).fill({}));