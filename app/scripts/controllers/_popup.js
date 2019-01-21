'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:PopupCtrl
 * @description
 * # BasicInformationCtrl
 * Controller of the webframe
 */
angular.module('webframe')
    .controller('PopupCtrl', ['$scope','hotRegisterer', function ($scope, hotRegisterer) {
        $scope.ngPopupConfig = {
            modelName: 'myNgPopup',
            template: '<main ng-view></main>',
            resizable: true,
            draggable: true,
            position: {
                top: 135,
                left: 15
            },
            title: '',
            hasTitleBar: true,
            pinned: false,
            isShow: true,
            onOpen: function () { },
            onClose: function () { },
            onDragStart: function () { },
            onDragEnd: function () { },
            onResize: function () { }
        }

        // テーブルのインスタンス名（Node等のクラスから設定する）
        $scope.hotId = '';

        // 計算結果（Object型）
        $scope.result_disgs = null;
        $scope.result_fsecs = null;
        $scope.result_reacs = null;
        
        // 断面力計算用（Object型）
        $scope.fsec_difines = null;
        $scope.fsec_combs = null;
        $scope.fsec_picks = null;
        $scope.result_comb_fsecs = null;
        $scope.result_pic_feacs = null;


        // 計算結果を保持する
        $scope.keepCulcResult = function (data) {
            $scope.result_disgs = data['1']['disg'];
            $scope.result_fsecs = data['1']['fsec'];
            $scope.result_reacs = data['1']['reac'];
            $scope.$broadcast('reload');
            if (GetModeName(location.hash) != 'difines' && GetModeName(location.hash) != 'combines' && GetModeName(location.hash) != 'pickups') $scope.culcFsec();
        }

        // 組み合わせ断面力・ピックアップ断面力の計算
        $scope.culcFsec = function () {
            // 組み合わせ断面力の計算：各データがnullなら何もしない
            if (!$scope.result_disgs || !$scope.fsec_difines || !$scope.fsec_combs) return;

            // データクリア
            $scope.result_comb_fsecs = {};

            // 組み合わせ断面力の計算
            _.each($scope.fsec_combs, function(comb_value, comb_key) {  //COMBINEの各行
                var nowLineFsec = null;
                _.each(comb_value, function(comb_cell, comb_cell_key) { //COMBINEの各セル
                    if (comb_cell_key == "id" || comb_cell == "") return true;

                    // 組合せのDIFINEを参照する
                    var dif = $scope.fsec_difines[comb_cell_key-1] || null;
                    var max = null;
                    if (dif) {
                        delete dif.id;
                        dif = _.map(dif, (item)=>{return item});
                        dif = _.compact(dif);   //""などfalseになるものを削除

                        var fsecs = [];
                        _.each(dif, function(item) {
                            var fsec = _.cloneDeep($scope.result_fsecs[item]);
                            if (fsec) fsecs.push(fsec);
                        });

                        // 候補の中から最大の物を取得（現在はn値で判定）
                        max = _.maxBy(fsecs, 'n');
                    }
                    // 最大の内容と入力値を掛け算
                    _.each(max, function(max_val, max_key) {
                        max[max_key] *= comb_cell;
                    });
                    
                    // 最大の内容を加算する
                    if (max) {
                        if (nowLineFsec) {
                            _.each(max, function(max_val, max_key) {
                                nowLineFsec[max_key] += max_val;
                            });
                        } else {
                            nowLineFsec = max;
                        }
                    }

                    // 組み合わせ断面力に追加
                    $scope.result_comb_fsecs[comb_key] = nowLineFsec;
                });
            });

            // ピックアップ断面力の計算：各データがnullなら何もしない
            if (!$scope.result_comb_fsecs || !$scope.fsec_pickups) return;

            // データクリア
            $scope.result_pic_feacs = {};

            // ピックアップ断面力の計算
            _.each($scope.fsec_pickups, function(comb_value, comb_key) {    //PICKUPの各行
                var nowLineFsec = null;
                _.each(comb_value, function(comb_cell, comb_cell_key) {
                    if (comb_cell_key == "id" || comb_cell == "") return true;

                    var fsec = _.cloneDeep($scope.result_comb_fsecs[comb_cell]);    //このセルで指定された組み合わせ断面力

                    if (nowLineFsec == null || nowLineFsec['n'] < fsec['n']) {  //大きさ比較（現在はn値で判定）
                        nowLineFsec = fsec;
                    }
                });

                // ピックアップ断面力に追加
                $scope.result_pic_feacs[comb_key] = nowLineFsec;
            });
        }

        // Unityで選択されたオブジェクトの行をフォーカス
        $scope.setRow = function (row){
            hotRegisterer.getInstance($scope.hotId).selectRows(row);
        }

        // 表のインスタンスを得る
        $scope.getHot = function () {
            var mode_name = GetModeName(location.hash);
            if (mode_name == '') {
                mode_name = 'nodes'; // デフォルトの設定
            }
            return hotRegisterer.getInstance(mode_name);
        }
    }]);