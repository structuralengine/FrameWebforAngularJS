# 本プロジェクトのセットアップ

## git clone

```
> cd working_directory 
> git clone "https://github.com/structuralengine/FrameWebforJS.git" FrameWebforJS
```
## bower, gulp の準備

```
> cd FrameWebforJS 
> npm install -g gulp-cli
> npm install -g bower
```

## 開発に必要なライブラリーの準備

```
> npm install 
> bower install
```

## テスト

### web サイトの起動

```
> gulp serve
```

「 http://localhost:9000 」でアクセス可能です。

### 配布用ファイルの作成 (dist/ ディレクトリ）

```
> gulp 
> ls dist/
```

command gulp により、内部で gulp build が実行され、dist ディレクトリが生成されます。この dist ディレクトリが公開用のファイル群です。

### 配布用ファイルを使った web サイトの起動

```
> gulp serve:dist
```
公開用ファイル (dist) を使って web サイトの確認ができます。



# javascript の作り方

## バンドラ

**バンドラ**とは、ブラウザには import や require の仕組みがないので、複数のファイルに分け、import してコーディングしたものを、強引に１つのファイルにまとめるツールのことです。
呼び出している部分を、別ファイルの内容で書き換える

よく利用するバンドラとしては

- WebPack
    - https://webpack.github.io
- Rollup
    - https://rollupjs.org

今回は、 WebPack を使います.


## トランスパイラ

**トランスパイラ**とは、ECMAScript6（以下 es6） でコーディングされたjavascriptと現代のブラウザで動くようにダウングレードするツールのことです。

よく利用するトランスパイラとしては

- babel
    - https://babeljs.io
- buble
    - https://buble.surge.sh

今回は、 babel を使います.


### babelをパソコン(グローバル環境)にインストールする

```
> npm install -g babel-cli
> npm install --save-dev babel-preset-es2015
```

### babelでes6のコードをトランスパイル＆実行する

```
> babel basic.es6.js -o basic.jp --presets es2015
```

これで、es6 で書かれた *basic.es6.js* を変換し、*basic.jp* として出力する
また、*basic.es6.js*の変更を監視して、変更都度に変換再処理を実行したければ、以下のように -w オプションを付与する

```
> babel -w basic.es6.js -o basic.jp --presets es2015
```


## タスクランナー

**タスクランナー**とは、上記の **トランスパイル** 処理を定型作業を自動化するツールのことです。

よく利用するタスクランナーとしては

- Grunt
    - https://gruntjs.com/
- Gulp
    - https://gulpjs.com/

今回は、 Gulp を使います.

### glupをパソコン(グローバル環境)にインストールする

```
> npm install -g gulp-cli 
```

### gulpfile.js(設定ファイル)を準備する

以下は、glupを利用するための基本的な定義ファイルです。

```javascript:gulpfile.js
gulp.task('hello', function() {
  console.log('Hello gulp!');
});
 
gulp.task('default', ['hello']);
```




# コーディング規約

## JAvaScript style guide(MDN)の主な規約

### 基本

- 1行あたりの桁数は 80文字以内に収めること
- ファイルの末尾は開業すること
- カンマ／セミコロンの後方にはスペースを入れること
- 関数やオブジェクトなどの定義ブロックの前後は空行で区切ること

### 空白

- インデントはスペース2個で表現すること(タブは使わない)
- 2項演算子は空白で区切ること
- カンマ／セミコロン、キーワードの後方には空白を含めること(ただし、行末の空白は不要)

### 命令規則

- 変数／関数名は先頭小文字の camelCase形式
- 定数名はすべて大文字のアンダースコア形式
- コンストラクター／クラス名は先頭大文字の camelCase形式
- プライベートメンバーは 「_(アンダースコア)」で始めること
- イベントハンドラー関数は「on」で始めること

### その他

- すべての変数は宣言、初期化すること
- 変数の宣言が重複しないこと
- 配列、オブジェクトの生成には、[...]、{...}などのリテラル構文を利用すること
- 真偽値を true／falseと比較しないこと

## Google 標準

- .js ファイルの名前は小文字で統一
- セミコロンは省略しない
- 文字列のくくり は「"」よりも「'」を優先して利用する
- 基本データ型(stringやnumber、boolean など)のラッパーオブジェクトは使用しない
- 名前空間を利用して、グローバルレベルの名前は最小限に抑える
- ブロックを表す{...}の前に改行は入れない
- ビルトインオブジェクトのプロトタイプは書き換えない
- with／eval 命令は利用しない
- for...in 命令は連想配列／ハッシュのみで利用する
