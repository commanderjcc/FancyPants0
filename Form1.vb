Public Class Form1

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
        Dim engine As New gameEngine()
        Dim data As New dataManager()
        Dim view As New viewManager(data)
    End Class

    Class viewManager
        Dim dataSource As dataManager

        Sub New(dataSource As dataManager)
            Me.dataSource = dataSource
        End Sub

        Sub moveMap(offset As Single)
            For Each row In dataSource.map.x
                For Each tile In row
                    tile.position.x -= offset
                    If tile.xLast - tile.position.x <= -100 Then
                        tile.position.xGrid -= 1
                    End If
                Next
            Next
        End Sub
    End Class

    Class dataManager
        Public map As Map
    End Class

    Class gameEngine

    End Class

    Class Tile
        'Work on this
    End Class

    Class Map
        Public x() As Array
        'Work on this
    End Class

    'Also can you make the text files for the map

    Class Player

    End Class

    Class entity

    End Class


End Class



