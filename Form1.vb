Imports System.IO

Public Class Form1
    Dim werty As Boolean, jump As Boolean
    Dim k As Integer, tmr As Integer
    Dim left As Boolean, right As Boolean
    Public instalLocation As String
    Public appGame As Game
    Public Structure dimensions
        Public x As Single, y As Single, width As Single, height As Integer

        Sub New(x As Single, y As Single)
            Me.x = x
            Me.y = y
            width = 27.8
            height = 27.6
        End Sub

        Sub New(x, y, width, height)
            Me.x = x
            Me.y = y
            Me.height = height
            Me.width = width
        End Sub
    End Structure

    Class Game
        Public data As dataManager
        Public view As viewManager
        Public engine As gameEngine

        Sub New(dataLocation As String)
            data = New dataManager(dataLocation)
            view = New viewManager(data)
            engine = New gameEngine(data, view)
        End Sub
    End Class

    Class viewManager
        Dim dataSource As dataManager

        Sub New(dataSource As dataManager)
            Me.dataSource = dataSource
        End Sub

        Sub moveMap(offset As Single)

        End Sub
    End Class

    Class dataManager
        Public map As Map
        Public player1 As Player
        Dim player1sprites(7) As Image
        '0-1: stand, 2-3: jump, 4-5: run forwards, 6-7: run backwards
        Public player2 As Player
        Dim player2sprites(7) As Image

        Sub makePlayer1(name)
            Dim temp As String = InputBox("Name?")


            'player1 = New Player(, 1, temp)
        End Sub

        Sub New(datalocation As String)
            map = New Map(datalocation + "\map")
            'For i = 0 To 7
            '    'player1sprites(i) = Image.FromFile(datalocation + "\sprites\player1\" + i.ToString + ".png")
            '    'player2sprites(i) = Image.FromFile(datalocation + "\sprites\player2\" + i.ToString + ".png")
            '    player1sprites(i) = Image.FromFile(datalocation + "\sprites\player1\" + Trim(Str(i)) + ".png")
            '    player2sprites(i) = Image.FromFile(datalocation + "\sprites\player2\" + Trim(Str(i)) + ".png")

            'Next
            player1 = New Player(2, "player1")
            player2 = New Player(2, "player2")
        End Sub


    End Class

    Class gameEngine
        Dim dataSource As dataManager
        Dim view As viewManager

        Sub New(dataSource As dataManager, view As viewManager)
            Me.dataSource = dataSource
            Me.view = view
        End Sub

        Function isXCollisionRight(player As Player) As Boolean
            Dim temp As Tile
            temp = dataSource.map.getCollisionTile(player.dimensions)(2)
            If temp.isSolid And player.dimensions.x + player.dimensions.width >= temp.dimensions.x Then
                Return True
            End If
            Return False
        End Function

        Function isYCollisionUp(player As Player) As Boolean
            Dim temp As Tile
            temp = dataSource.map.getCollisionTile(player.dimensions)(0)
            If temp.isSolid And player.dimensions.y <= temp.dimensions.y + temp.dimensions.height Then
                Return True
            End If
            Return False
        End Function

        Function isXcollisionLeft(player As Player) As Boolean
            Dim temp As Tile
            temp = dataSource.map.getCollisionTile(player.dimensions)(3)
            If temp.isSolid And player.dimensions.x <= temp.dimensions.x + temp.dimensions.width Then
                Return True
            End If
            Return False
        End Function

        Function isYCollisionDown(player As Player) As Boolean
            Dim temp As Tile
            temp = dataSource.map.getCollisionTile(player.dimensions)(1)
            If temp.isSolid And player.dimensions.y + player.dimensions.height >= temp.dimensions.y Then
                Return True
            End If
            Return False
        End Function
    End Class

    Class Tile
        Public dimensions As dimensions
        Public isSolid As Boolean
        Public points As Integer

        Sub New(GridX As Integer, GridY As Integer, isSolid As Boolean, points As Integer)
            dimensions = New dimensions(GridX * 27.8 + 13, GridY * 27.6)
            Me.isSolid = isSolid
            Me.points = points
        End Sub
    End Class

    Class Map
        Public mapImage As Image
        Public x(194) As Array

        Sub New(mapFolderLocation As String)
            Fill(mapFolderLocation + "\map.txt")
        End Sub

        Function getCollisionTile(playerDimensions As dimensions) As Tile()
            Dim centerX As Integer
            Dim centerY As Integer
            centerX = Math.Floor((playerDimensions.x - 13) / 27.8)
            centerY = Math.Floor((355 - playerDimensions.y) / 27.6)
            Dim temp(3) As Tile
            temp(0) = x(centerX)(centerY + 1)
            temp(1) = x(centerX)(centerY - 1)
            temp(2) = x(centerX + 1)(centerY)
            temp(3) = x(centerX - 1)(centerY)
            Return temp
        End Function


        Sub Fill(maplocation As String)
            'For i = 0 To 194
            '    Dim temp(14) As Tile
            '    For j = 0 To 14
            '        temp(j) = New Tile()
            '    Next
            '    x(i) = temp
            'Next
            Dim tempStr As String
            Dim YblocksStr(14) As String
            Dim Yblocks(14) As Tile
            Dim numberOfTiles As Integer
            Dim numberOfRows As Integer
            Dim sr As New StreamReader(maplocation)
            numberOfRows = 0
            Do While Not sr.EndOfStream
                numberOfTiles = 0
                tempStr = sr.ReadLine
                YblocksStr = tempStr.Split
                For Each block In YblocksStr
                    Select Case Trim(block)
                        Case "S"
                            Yblocks(numberOfTiles) = New Tile(numberOfRows, numberOfTiles, True, 0)
                        Case "A"
                            Yblocks(numberOfTiles) = New Tile(numberOfRows, numberOfTiles, False, 0)
                        Case "P"
                            Yblocks(numberOfTiles) = New Tile(numberOfRows, numberOfTiles, False, 1000)
                        Case "Q"
                            Yblocks(numberOfTiles) = New Tile(numberOfRows, numberOfTiles, True, 100)
                    End Select
                    numberOfTiles += 1
                Next
                x(numberOfRows) = Yblocks.Clone
                numberOfRows += 1
            Loop
        End Sub



        'Work on this
    End Class

    'Also can you make the text files for the map

    Class Player
        Inherits entity
        Public name As String

        Sub New(health As Single, name As String)
            MyBase.New(health, 0, New dimensions(357, 300, 15, 27.6))
            Me.name = name
        End Sub
    End Class

    Class entity
        Public images() As Image
        Public health As Single
        Public points As Single
        Public dimensions As dimensions

        Function testCollision(map As Map)

        End Function

        Public Sub New(health As Single, points As Single, dimensions As dimensions)
            Me.health = health
            Me.points = points
            Me.dimensions = dimensions
        End Sub
    End Class

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.A Then
            If PictureBox1.Right > -1 And PictureBox1.Right < 5640 And Not appGame.engine.isXcollisionLeft(appGame.data.player1) Then
                left = True
                right = False
                PictureBox1.Left += 5
                PictureBox2.Left -= 5
                appGame.data.player1.dimensions.x -= 5
                If Not appGame.engine.isYCollisionDown(appGame.data.player1) And jump = False Then
                    werty = False
                    Timer2.Start()
                End If
            Else
                left = False
                right = False
                PictureBox2.ImageLocation = instalLocation + "\sprites\LeftStanding.png"
            End If
        ElseIf e.KeyCode = Keys.D Then
            If PictureBox1.Right <= 5640 And PictureBox1.Right > 0 And Not appGame.engine.isXCollisionRight(appGame.data.player1) Then
                right = True
                left = False
                PictureBox1.Left -= 5
                PictureBox2.Left += 5
                appGame.data.player1.dimensions.x += 5
                If Not appGame.engine.isYCollisionDown(appGame.data.player1) And jump = False Then
                    werty = False
                    Timer2.Start()
                End If
            Else
                PictureBox2.ImageLocation = instalLocation + "\sprites\Rightstanding.png"
                right = False
                left = False
            End If
        ElseIf e.KeyCode = Keys.W Then
            If appGame.engine.isYCollisionDown(appGame.data.player1) Then
                My.Computer.Audio.Play(instalLocation + "\mario_04.wav", AudioPlayMode.Background)
                Timer2.Start()
                werty = True
                jump = True

            End If
        Else
            left = False
            right = False
        End If

        Timer1.Start()
        Label5.Text = PictureBox2.Top
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        'instalLocation = "C:\Users\Saima\Documents\GitHub\FancyPants0\FancyPants0"
        instalLocation = "D:\Documents\Schoolwork\Computer Programing 2\VB.NET\FancyPants0"
        appGame = New Game(instalLocation)
        tmr = 1
        My.Computer.Audio.Play(instalLocation + "\SuperMarioBros.wav", AudioPlayMode.BackgroundLoop)

        jump = True
        With PictureBox2
            .Parent = PictureBox1
            .BackColor = Color.Transparent
        End With
        PictureBox2.ImageLocation = instalLocation + "\sprites\LeftStanding.png"
    End Sub

    Private Sub Form1_MouseHover(sender As Object, e As EventArgs) Handles MyBase.MouseHover

    End Sub

    Private Sub Form1_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.A Then
            If PictureBox1.Right > -1 And PictureBox1.Right < 4230 Then
                left = False
                PictureBox2.ImageLocation = instalLocation + "\sprites\LeftStanding.png"
            End If
        ElseIf e.KeyCode = Keys.D Then
            If PictureBox1.Right < 5640 And PictureBox1.Right > 0 Then
                right = False
                PictureBox2.ImageLocation = instalLocation + "\sprites\Rightstanding.png"
            End If
        Else


        End If

    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick

        If werty = False Then
            PictureBox2.Top += 3
            appGame.data.player1.dimensions.y += 3
            If appGame.engine.isYCollisionDown(appGame.data.player1) Then
                'werty = True
                Timer2.Stop()
            End If
            jump = False
        ElseIf werty = True Then
            PictureBox2.Top -= 3
            appGame.data.player1.dimensions.y -= 3
            If PictureBox2.Top <= 132 Then
                werty = False
            End If

        End If
        Label4.Text = PictureBox2.Top
    End Sub

    Private Sub Form1_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove, PictureBox1.MouseMove
        Dim x As Single = e.Location.X
        Dim y As Single = e.Location.Y
        Dim centerX As Integer
        Dim centerY As Integer
        centerX = Math.Floor((x - 13) / 27.8)
        centerY = Math.Floor((355 - y) / 27.6)
        Label1.Text = centerX
        Label2.Text = centerY
        Label3.Text = y
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'If Not appGame.engine.isYCollisionDown(appGame.data.player1) Then
        '    'PictureBox2.Top += 3
        '    'appGame.data.player1.dimensions.y += 3
        'End If
        Label1.Text = Math.Floor(appGame.data.player1.dimensions.x)
        Label4.Text = PictureBox2.Top

        If appGame.data.player1.dimensions.x > 5000 Then
            Timer1.Stop()
            MsgBox("YOU WIN! Your score was: " + Str(appGame.data.player1.dimensions.x * 1.23))
        End If


        Select Case tmr
            Case 1
                If left = True Then
                    PictureBox2.ImageLocation = instalLocation + "\sprites\left1walk.png"

                ElseIf right = True Then
                    PictureBox2.ImageLocation = instalLocation + "\sprites\right1walk.png"
                End If
                tmr = tmr + 1
            Case 2
                If left = True Then
                    PictureBox2.ImageLocation = instalLocation + "\sprites\left2walk.png"
                ElseIf right = True Then
                    PictureBox2.ImageLocation = instalLocation + "\sprites\right2walk.png"
                End If
                tmr = 1
        End Select


    End Sub
End Class



