Imports System.Console
Imports System.IO
Imports Newtonsoft.Json.Linq

Module Module1

    Sub Main()

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
            'キーが存在しない状態でデバッグあり開始を実行するとエラーになる
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

        '出力先ディレクトリの決定
        If My.Computer.FileSystem.DirectoryExists(choice_dir) Then
            dir = choice_dir
        Else
            dir = default_dir
        End If
        dir = dir & "\" & id & " " & name
        msg = "作成されるフォルダのパスは「" & dir & "課題名」です！"

        '#を出力する数をカウントする(全角=2)
        number_of_sharps = Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(msg)

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
        WriteLine("課題番号を入力してください。")
        WriteLine("宿題課題の場合は番号の前にsを入れてください。例「s00」")

        WriteLine("作成されるフォルダのパスは「" & dir & " 課題名」です！")
        For i = 0 To number_of_sharps + 8
            Write("#")
        Next i
        WriteLine()

        '適切なフォルダ名になるまでループ
        Dim check_dir_name As Boolean = True
        While check_dir_name

            Write("課題番号 >> ") : kadai = ReadLine()

            If kadai = "" Then
                WriteLine("課題番号が確認できません")
            ElseIf kadai.Contains("s") Then
                kadai = kadai.Remove(0, 1)
                If kadai = "" Then
                    WriteLine("課題番号が確認できません")
                Else
                    kadai = "宿題課題 " & kadai
                    check_dir_name = False
                End If
            Else
                kadai = "課題 " & kadai
                check_dir_name = False
            End If

            WriteLine()

        End While

        '出力するパスを決定
        dir = dir & " " & kadai

        'フォルダが存在するかの確認(overwrite_warningが有効なときのみ)
        If overwrite_warning = True Then
            If Directory.Exists(dir) Then
                WriteLine("フォルダが存在するため続行できません")
                Read()
                Environment.Exit(1)
            End If
        End If

        'フォルダを作成する
        Try
            Directory.CreateDirectory(dir)
            File.Create(dir & "\取り組んで感じたこと.txt")
            Process.Start(dir)
            Process.Start("notepad.exe", dir & "\取り組んで感じたこと")
        Catch ex As Exception
            WriteLine("フォルダの作成に失敗しました")
            WriteLine(ex.Message)
            Read()
            Environment.Exit(1)
        End Try

        'フォルダが作成できたことをチェックする
        If Not File.Exists(dir & "\取り組んで感じたこと.txt") Then
            WriteLine("正常に完了できませんでした")
            Read()
            Environment.Exit(1)
        End If

        Environment.Exit(0)

    End Sub

End Module
