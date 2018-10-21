'use strict';

/**
 * @ngdoc service
 * @name webframe.config/panelDefaults
 * @description
 * # config/panelDefaults
 * Constant in the webframe.
 */

const ROW = 10;                   //表の行数

angular.module('webframe').constant('panelDefaults', Array(ROW).fill({}));