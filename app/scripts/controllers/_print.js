var elementsMode = false;

// -----------------------------------
// 「印刷」ボタンが押された時の処理
// -----------------------------------
function Print() {

    // 印刷エリアを表示
    $('.print-page').show();

    var handsonParent = $('.ht-container').parent().attr('class');

    // 材料ポップアップは他と比べて大きく幅が異なるため、別途専用として作成します
    if (elementsMode) {
        AbjustLayoutExcelForElements(handsonParent);
    } else {
        AbjustLayoutExcel(handsonParent);
    }

    SendCapture();

    setTimeout(function () {

        // 印刷する箇所以外の要素を非表示にする
        $('.header').addClass('print-off');
        $('.webgl-content').addClass('print-off');
        $('.popup').addClass('print-off');

        //window.print()の実行後、プレビュー用の画面を元に戻す
        window.print();
        setTimeout(function() {
            // コピー＆加工したエクセル表を丸ごと削除
            $('.cloned').remove();
            $('.print-off').removeClass('print-off');
            // 印刷エリアを非表示にする
            $('.print-page').hide();

        },500);

    }, 500);
}

// -----------------------------------
// エクセル表を印刷Elements用に加工
// -----------------------------------
function AbjustLayoutExcelForElements() {

    // エクセル表を印刷エリアにコピーする
    var excelCopy = $('.handsontable-container').clone().appendTo('#excel_capture_area').addClass('cloned elements_handsontable');

    // エクセルを表示するエリアを最大限にする
    excelCopy.find('.wtHolder').css('width', '100%');
    excelCopy.find('.wtHolder').css('height', '390px');
    excelCopy.find('.wtHider').css('width', '100%');
    excelCopy.css('overflow', 'hidden');

    SetWorkID(excelCopy, '.ht_master');
    SetWorkID(excelCopy, '.ht_clone_top');

    // 二段目用にさらにエクセルをコピーする
    var excelCopyClone = $('.elements_handsontable').clone().appendTo('#excel_capture_area');

    // 二段目の弾性係数、せん断弾性係数、膨張係数、タイプ1欄を削除
    var copyClone_ht_master_colgroup = excelCopyClone.find('.ht_clone_top').find('colgroup');
    copyClone_ht_master_colgroup.find('.no2').remove();
    copyClone_ht_master_colgroup.find('.no3').remove();
    copyClone_ht_master_colgroup.find('.no4').remove();
    copyClone_ht_master_colgroup.find('.no5').remove();
    copyClone_ht_master_colgroup.find('.no6').remove();
    copyClone_ht_master_colgroup.find('.no7').remove();
    copyClone_ht_master_colgroup.find('.no8').remove();

    var copyClone_ht_clone_top_thead = excelCopyClone.find('.ht_clone_top').find('thead');
    copyClone_ht_clone_top_thead.find('.no2').remove();
    copyClone_ht_clone_top_thead.find('.no3').remove();
    copyClone_ht_clone_top_thead.find('.no4').remove();
    copyClone_ht_clone_top_thead.find('.no5').remove();
    copyClone_ht_clone_top_thead.find('.no18').remove();
    copyClone_ht_clone_top_thead.find('.no19').remove();
    copyClone_ht_clone_top_thead.find('.no20').remove();
    copyClone_ht_clone_top_thead.find('.no21').remove();
    copyClone_ht_clone_top_thead.find('.no22').remove();
    copyClone_ht_clone_top_thead.find('.no23').remove();
    copyClone_ht_clone_top_thead.find('.no34').remove();
    copyClone_ht_clone_top_thead.find('.no35').remove();
    copyClone_ht_clone_top_thead.find('.no36').remove();
    copyClone_ht_clone_top_thead.find('.no37').remove();
    copyClone_ht_clone_top_thead.find('.no38').remove();
    copyClone_ht_clone_top_thead.find('.no39').remove();
    copyClone_ht_clone_top_thead.find('.no40').remove();

    var copyClone_ht_master_colgroup= excelCopyClone.find('.ht_master').find('colgroup');
    copyClone_ht_master_colgroup.find('.no2').remove();
    copyClone_ht_master_colgroup.find('.no3').remove();
    copyClone_ht_master_colgroup.find('.no4').remove();
    copyClone_ht_master_colgroup.find('.no5').remove();
    copyClone_ht_master_colgroup.find('.no6').remove();
    copyClone_ht_master_colgroup.find('.no7').remove();
    copyClone_ht_master_colgroup.find('.no8').remove();

    var copyClone_ht_master_thead = excelCopyClone.find('.ht_master').find('thead');
    copyClone_ht_master_thead.find('.no2').remove();
    copyClone_ht_master_thead.find('.no3').remove();
    copyClone_ht_master_thead.find('.no4').remove();
    copyClone_ht_master_thead.find('.no5').remove();
    copyClone_ht_master_thead.find('.no18').remove();
    copyClone_ht_master_thead.find('.no19').remove();
    copyClone_ht_master_thead.find('.no20').remove();
    copyClone_ht_master_thead.find('.no34').remove();
    copyClone_ht_master_thead.find('.no35').remove();
    copyClone_ht_master_thead.find('.no36').remove();

    var copyClone_ht_master_tbody = excelCopyClone.find('.ht_master').find('tbody');
    var i = 1;
    copyClone_ht_master_tbody.find('tr').each(function() {
        $(this).find('.no1').remove();
        $(this).find('.no2').remove();
        $(this).find('.no3').remove();
        $(this).find('.no4').remove();  // タイプ1：A(m2)
        $(this).find('.no5').remove();  // タイプ1：J(m4)
        $(this).find('.no6').remove();  // タイプ1：ly(m4)
        $(this).find('.no7').remove();  // タイプ1：lz(m4)
    });

    // 一段目のタイプ2、タイプ3枠欄を削除
    DeleteType2Type3(excelCopy, '.ht_master');
    DeleteType2Type3(excelCopy, '.ht_clone_top');

}

// -----------------------------------
// 「材料」のタイプ2とタイプ3カラムを削除する
// -----------------------------------
function DeleteType2Type3(excelCopy, className) {
    var excelCopy_ht_master_colgroup = excelCopy.find(className).find('colgroup');
    excelCopy_ht_master_colgroup.find('.no9').remove();
    excelCopy_ht_master_colgroup.find('.no10').remove();
    excelCopy_ht_master_colgroup.find('.no11').remove();
    excelCopy_ht_master_colgroup.find('.no12').remove();
    excelCopy_ht_master_colgroup.find('.no13').remove();
    excelCopy_ht_master_colgroup.find('.no14').remove();
    excelCopy_ht_master_colgroup.find('.no15').remove();
    excelCopy_ht_master_colgroup.find('.no16').remove();

    var excelCopy_ht_master_thead = excelCopy.find(className).find('thead');
    excelCopy_ht_master_thead.find('.no9').remove();
    excelCopy_ht_master_thead.find('.no13').remove();
}

// -----------------------------------
// 「材料」の印刷作業用にクラスIDを割り当てる
// -----------------------------------
function SetWorkID(excelCopy, className) {
    var wtSpreader = excelCopy.find(className).find('.wtSpreader');
    var htCore = wtSpreader.find('.htCore');

    var i = 1;
    htCore.find('colgroup').find('col').each(function() {
        $(this).addClass('no'+i);
        i++;

    });

    i = 1;
    htCore.find('thead').find('tr').find('th').each(function() {
        $(this).addClass('no'+i);
        i++;

    });

    i = 1;
    htCore.find('tbody').find('tr').each(function(){
        $(this).addClass('no'+i);
        i++;

        var j = 1;
        $(this).find('td').each(function() {
            $(this).addClass('no'+j);
            j++;
        });
    });

}

// -----------------------------------
// エクセル表を印刷用に加工
// -----------------------------------
function AbjustLayoutExcel(handsonParent) {

    // エクセル表を印刷エリアにコピーする
    var excelCopy = $('.handsontable-container').clone().appendTo('#excel_capture_area').addClass('cloned');

    // エクセルを表示するエリアを最大限にする
    excelCopy.find('.wtHolder').css('width', '100%');
    excelCopy.find('.wtHider').css('width', '100%');
    excelCopy.css('overflow', 'hidden');

    var htMaster = excelCopy.find('.ht_master');
    var wtSpreader = htMaster.find('.wtSpreader');
    var htCore = htMaster.find('.htCore');
    htCore.attr('class', 'htCore table_class');

    wtSpreader.css('width', '100%');
    wtSpreader.attr('id', 'narabi');

    // 二段目のエクセル表を作成
    wtSpreader.append('<table class="htCoreCopy1 table_class cloned"></table>');
    htCore.find('colgroup').clone().appendTo('.htCoreCopy1');
    htCore.find('thead').clone().appendTo('.htCoreCopy1');
    htMaster.find('.htCoreCopy1').append('<tbody class="tbodyCopy1"></tbody>');

    // 三段目のエクセル表を作成
    wtSpreader.append('<table class="htCoreCopy2 table_class cloned"></table>');
    htCore.find('colgroup').clone().appendTo('.htCoreCopy2');
    htCore.find('thead').clone().appendTo('.htCoreCopy2');
    htMaster.find('.htCoreCopy2').append('<tbody class="tbodyCopy2"></tbody>');

    var TWO = 20;       // 二段目が開始する行数
    var THIRD = 40;     // 三段目が開始する行数

    var i = 0;
    // 一段目から二段目、三段目を切り出す
    htCore.find('tbody').find('tr').each(function(){
        // TWO行目〜二段目とする
        if (i >= TWO && i <= THIRD-1) {
            $(this).clone().appendTo('.tbodyCopy1');
            $(this).remove();

        // THIRD行目以降を三段目とする
        } else if (i >= THIRD) {
            $(this).clone().appendTo('.tbodyCopy2');
            $(this).remove();
        }
        i++;
    });

    var x_position = 0;
    // 表示している画面に応じて分割した列の座標を調整する
    if (handsonParent.match('nodes')) {
        x_position = 200;
    } else if (handsonParent.match('members')) {
        x_position = 260;
    } else if (handsonParent.match('panels')) {
        x_position = 260;
    } else if (handsonParent.match('elements')) {
        x_position = 400;
    }

    i = 0;
    // 一段目が何行あるか数える
    excelCopy.find('.ht_clone_left').find('tbody').find('tr').each(function(){
        i++;
    });

    // 二段目の行番号をコピー
    if (i >= TWO) {
        var htCloneleftCopy1 = excelCopy.find('.ht_clone_left').clone();
        htCloneleftCopy1.appendTo('.handsontable-container').attr('class', 'ht_clone_left_copy1 handsontable cloned');
        htCloneleftCopy1.css('left', x_position+'px');
    }

    // 三段目の行番号をコピー
    if (i >= THIRD) {
        var htCloneleftCopy2 = excelCopy.find('.ht_clone_left').clone();
        htCloneleftCopy2.appendTo('.handsontable-container').attr('class', 'ht_clone_left_copy2 handsontable cloned');
        htCloneleftCopy2.css('left', (x_position*2)+'px');
    }

    // 一段目の不要な行番号を削除
    i = 0;
    excelCopy.find('.ht_clone_left').find('tbody').find('tr').each(function(){
        if (i >= TWO) {
            $(this).remove();
        }
        i++;
    });

    var left_copy1_flg = 0;
    var left_copy2_flg = 0;
    if (i >= TWO) {
        left_copy1_flg = 1;
    }
    if (i >= THIRD) {
        left_copy2_flg = 1;
    }

    // 二段目の不要な行番号を削除
    i = 0;
    excelCopy.find('.ht_clone_left_copy1').find('tbody').find('tr').each(function(){
        if (i <= TWO-1) {
            $(this).remove();
        }
        i++;
    });

    // 三段目の不要な行番号を削除
    i = 0;
    excelCopy.find('.ht_clone_left_copy2').find('tbody').find('tr').each(function(){
        if (i <= THIRD-1) {
            $(this).remove();
        }
        i++;
    });

    // エクセルの列番号コピー処理
    if (left_copy1_flg == 1) {
        var htCloneTop1 = excelCopy.find('.ht_clone_top').clone();
        htCloneTop1.appendTo('.handsontable-container').attr('class', 'ht_clone_top_copy1 handsontable cloned');
        htCloneTop1.css('left', x_position+'px');
    }

    if (left_copy2_flg == 1) {
        var htCloneTop2 = excelCopy.find('.ht_clone_top').clone();
        htCloneTop2.appendTo('.handsontable-container').attr('class', 'ht_clone_top_copy2 handsontable cloned');
        htCloneTop2.css('left', (x_position*2)+'px');
    }

}
