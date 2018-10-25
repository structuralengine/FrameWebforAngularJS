'use strict';

/**
 * @ngdoc service
 * @name webframe.config/pickupDefaults
 * @description
 * # config/pickupDefaults
 * Constant in the webframe.
 */

const ROW = 10;                   //表の行数

angular.module('webframe').constant('pickupDefaults', Array(ROW).fill({}));