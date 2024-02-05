# CreateDir-vb
* プログラミング１ プログラミング２ の授業で課題を提出するときにプロジェクトをまとめるフォルダを作成します。
* その中に`取り組んで感じたこと.txt`を作成します。

## 動作環境
* Windows10
* Windows11

## 使い方
1. Releaseからzipファイルをダウンロードしてください。

2. `CreateDir-vb.exe`と同じディレクトリに`config.json`を作成する
    ```json
    {
        "ID": 1000000,
        "Name": "田中なかた",
        "makedir": "C:\\",
        "Overwrite_Warning": true
    }
    ```
    |項目|形式|用途|
    |:---:|:---:|:---:|
    |ID|数値|学籍番号をセット|
    |Name|文字列|名前をセット|
    |makedir|文字列|フォルダを作成する場所を指定します。<br>`\`を使うときは`\\`にしましょう|
    |Overwrite_Warning|真偽|trueの場合、フォルダが上書きされることを防止します|

3. `CreateDir-vb.exe`を実行する
