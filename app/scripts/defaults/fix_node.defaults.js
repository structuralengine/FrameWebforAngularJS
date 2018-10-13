'use strict';

/**
 * @ngdoc service
 * @name webframe.config/fixNodeDefaults
 * @description
 * # config/fixNodeDefaults
 * Constant in the webframe.
 */

const ROW = 10;                   //表の行数

angular.module('webframe').constant('fixNodeDefaults', Array(ROW).fill({}));