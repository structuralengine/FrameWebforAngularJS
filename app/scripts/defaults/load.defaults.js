'use strict';

/**
 * @ngdoc service
 * @name webframe.config/loadDefaults
 * @description
 * # config/loadDefaults
 * Constant in the webframe.
 */

const ROW = 10;                   //表の行数

angular.module('webframe').constant('loadDefaults', Array(ROW).fill({}));