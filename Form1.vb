﻿Public Class Form1
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
            For i = 0 To 7
                player1sprites(i) = Image.FromFile(datalocation + "\sprites\player1\" + i + ".png")
                player2sprites(i) = Image.FromFile(datalocation + "\sprites\player2\" + i + ".png")
            Next
            player1 = New Player(player1sprites, 2, "player1")
            player2 = New Player(player2sprites, 2, "player2")
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
    End Class

    Class Map
        Public mapImage As Image
        Public x(134) As Array

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
            For i = 1 To 134
                Dim temp(8) As Tile
                For j = 1 To 9
                    temp(j - 1) = New Tile
                Next
                x(i) = temp
            Next
        End Sub



        'Work on this
    End Class

    'Also can you make the text files for the map

    Class Player
        Inherits entity
        Public name As String

        Sub New(images As Image(), health As Single, name As String)
            MyBase.New(images, health, 0, New dimensions(152, 300, 45, 56))
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

        Public Sub New(images As Image(), health As Single, points As Single, dimensions As dimensions)
            Me.images = images
            Me.health = health
            Me.points = points
            Me.dimensions = dimensions
        End Sub
    End Class

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.A Then
            If PictureBox1.Left <> 2 Then
                PictureBox1.Left += 2
            End If
        ElseIf e.KeyCode = Keys.D Then
            If PictureBox1.Right >= -55 Then
                PictureBox1.Left -= 2
            End If
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        appGame = New Game(instalLocation)

        'instalLocation = ""
        instalLocation = "D:\Documents\Schoolwork\Computer Programing 2\VB.NET\FancyPants0"

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
        Label3.Text = x
    End Sub
End Class



