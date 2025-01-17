'use strict';

/**
 * @ngdoc service
 * @name webframe.config/msgConfig
 * @description
 * # config/msgConfig
 * Constant in the webframe.
 */
angular.module('webframe')
  .constant('msgConfig', {
    files: {
      save: {
        failed: [
          'ファイルの保存に失敗しました',
          'ファイルをダウンロードします'
        ].join('\n'),
        noData: [
          'データが読み込まれていません',
          'データを新規作成するか、ファイルを読み込んでください'
        ].join('\n')
      }
    },
    
  });
