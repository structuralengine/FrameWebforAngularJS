# 本プロジェクトのセットアップ

## git clone

```
> cd working_directory 
> git clone "https://github.com/structuralengine/FrameWebforJS.git" FrameWebforJS
> cd FrameWebforJS 
```

## bower, gulp の準備

```
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

Nisual studio Core を使う場合は、"**F5**" でも同じです

「 http://localhost:9000 」でアクセス可能です。


### 配布用ファイルの作成 (dist/ ディレクトリ）

```
> gulp 
> ls dist/
```

command gulp により、内部で gulp build が実行され、dist ディレクトリが生成されます。
この dist ディレクトリが公開用のファイル群です。

### 配布用ファイルを使った web サイトの起動

```
> gulp serve:dist
```
公開用ファイル (dist) を使って web サイトの確認ができます。



# javascript の作り方

## トランスパイラ

**トランスパイラ**とは、ECMAScript6（以下 es6） でコーディングされたjavascriptと現代のブラウザで動くようにダウングレードするツールのことです。

よく利用するトランスパイラとしては

- babel
    - https://babeljs.io
- buble
    - https://buble.surge.sh

今回は、 babel を使います.


## タスクランナー

**タスクランナー**とは、上記の **トランスパイル** 処理を定型作業を自動化するツールのことです。

よく利用するタスクランナーとしては

- Grunt
    - https://gruntjs.com/
- Gulp
    - https://gulpjs.com/

今回は、 Gulp を使います.



## バンドラ

**バンドラ**とは、ブラウザには import や require の仕組みがないので、複数のファイルに分け、import してコーディングしたものを、強引に１つのファイルにまとめるツールのことです。
呼び出している部分を、別ファイルの内容で書き換える

よく利用するバンドラとしては

- WebPack
    - https://webpack.github.io
- Rollup
    - https://rollupjs.org

今回は、 上記の **タスクランナー** が代替えしているので使いません


