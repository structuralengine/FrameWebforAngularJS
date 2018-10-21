'use strict';

/**
 * @ngdoc service
 * @name webframe.config/memberDefaults
 * @description
 * # config/memberDefaults
 * Constant in the webframe.
 */

const ROW = 10;                   //表の行数

angular.module('webframe').constant('memberDefaults', Array(ROW).fill({}));