Public Class Form1
    Public instalLocation As String
    Public appGame As Game
    Structure dimensions
        Public x As Single, y As Single, width As Single, height As Integer, xGrid As Integer, yGrid As Single, xLast As Single, yLast As Single

        Sub New(x As Single, y As Single, xGrid As Integer, yGrid As Integer)
            Me.x = x
            Me.y = y
            width = 100
            height = 100
            Me.xGrid = xGrid
            Me.yGrid = yGrid
            Me.xLast = 0
            Me.yLast = 0
        End Sub

    End Structure

    Class Game
        Dim data As dataManager
        Dim view As viewManager
        Dim engine As gameEngine

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

        End Sub


    End Class

    Class gameEngine
        Dim dataSource As dataManager
        Dim view As viewManager

        Sub New(dataSource As dataManager, view As viewManager)
            Me.dataSource = dataSource
            Me.view = view
        End Sub

    End Class

    Class Tile
        'Work on this
    End Class

    Class Map
        Public mapImage As Image
        Public x(134) As Array

        Sub New(mapFolderLocation As String)
            Fill(mapFolderLocation + "\map.txt")
        End Sub

        Sub Fill(maplocation As String)
            For i = 1 To 134
                Dim temp(8) As Tile
                For j = 1 To 9
                    temp(j) = New Tile
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
            MyBase.New(images, health, 0)
            Me.name = name
        End Sub
    End Class

    Class entity
        Public images() As Image
        Public health As Single
        Public points As Single

        Sub New(images As Image(), health As Single, points As Single)
            Me.images = images
            Me.health = health
            Me.points = points
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
        game = New Game(instalLocation)

        'instalLocation = ""
        instalLocation = "D:\Documents\Schoolwork\Computer Programing 2\VB.NET\FancyPants0"

    End Sub
End Class



