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

        Dim msg As String
        Dim number_of_sharps As Integer

        Dim default_dir, choice_dir, dir As String '初期パス ユーザ指定パス 出力先パス
        Dim overwrite_warning As Boolean

        '変数のセット(デフォ出力先はデスクトップ)
        id = 0 : name = "" : kadai = ""

        msg = "" : number_of_sharps = 0

        choice_dir = "" : dir = ""
        default_dir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)

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
            choice_dir = JObject("makedir").ToString()
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

        Debug.Print(default_dir)
        Debug.Print(choice_dir)
        Debug.Print(id)
        Debug.Print(name)
        Debug.Print(overwrite_warning)

        '出力先ディレクトリの決定
        If My.Computer.FileSystem.DirectoryExists(choice_dir) Then
            dir = choice_dir
        Else
            dir = default_dir
        End If
        dir = dir & "\" & id & " " & name & " <課題名>"
        msg = "作成されるフォルダのパスは「" & dir & "」です！"

        '#を出力する数をカウントする(全角=2)
        number_of_sharps = Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(msg)
        Debug.Print(number_of_sharps)

        Dim i, j As Integer
        msg = ""
        For i = 0 To 1
            For j = 0 To number_of_sharps / 2
                msg += "#"
            Next j
            If i = 0 Then msg += "ようこそ"
        Next i

        WriteLine(msg)
        WriteLine("設定ファイルのロードに成功しました！")

        WriteLine("作成されるフォルダのパスは「" & dir & "」です！")
        For i = 0 To number_of_sharps + 8
            Write("#")
        Next i
        WriteLine()

        Write("課題名 >> ") : kadai = ReadLine() : Debug.Print(kadai)

        Read()

    End Sub

End Module
