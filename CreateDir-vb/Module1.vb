Imports System.Console
Imports System.IO
Imports Newtonsoft.Json.Linq

Module Module1

    Sub Main()
        'やること 各種情報を読み込む

        '設定ファイル関係
        Dim Config_Path, JsonString As String
        Dim JObject As JObject
        Config_Path = "" : JsonString = ""

        '出力に使う値
        Dim id As Integer
        Dim name, kadai As String
        Dim makedir As String
        Dim overwrite_warning As Boolean

        '変数のセット(デフォ出力先はデスクトップ)
        id = 0
        name = "" : kadai = ""
        makedir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        overwrite_warning = True

        '設定ファイル(Config.json)の存在を確認し読み込む
        Try
            Config_Path = My.Application.Info.DirectoryPath & "\Config.json"
            JsonString = File.ReadAllText(Config_Path)
            JObject = JObject.Parse(JsonString)
        Catch ex As FileNotFoundException
            WriteLine("設定ファイルの読み込みに失敗しました")
            WriteLine(ex.Message)
            Read()
            Environment.Exit(1)
        Catch ex As Exception
            WriteLine("予期せぬエラー")
            WriteLine(ex.Message)
            Read()
            Environment.Exit(1)
        End Try

        'キーの読み込み
        Try
            overwrite_warning = JObject("Overwrite_Warning").ToString()
            id = JObject("ID").ToString()
            name = JObject("Name").ToString()
        Catch ex As NullReferenceException
            WriteLine("設定ファイルの内容が不正です")
            WriteLine(ex.Message)
            Read()
            Environment.Exit(1)
        Catch ex As Exception
            WriteLine("予期せぬエラー")
            WriteLine(ex.Message)
            Read()
            Environment.Exit(1)
        End Try

        Debug.Print(makedir)
        Debug.Print(id)
        Debug.Print(name)
        Debug.Print(overwrite_warning)

    End Sub

End Module
