Public Class frmMain

   Private mobjFormBitmap As Bitmap
   Private mobjBitmapGraphics As Graphics
   Private mintFormWidth As Integer
   Private mintFormHeight As Integer
   Private mblnDraw As Boolean = False
   Private mobjDrops As List(Of Point)
   Private mobjRandom As Random

   Private Sub frmMain_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

      Static blnDoneOnce As Boolean = False

      If Not blnDoneOnce Then
         blnDoneOnce = True
         mintFormWidth = Me.Width
         mintFormHeight = Me.Height
         mobjFormBitmap = New Bitmap(mintFormWidth, mintFormHeight, Me.CreateGraphics())
         mobjBitmapGraphics = Graphics.FromImage(mobjFormBitmap)
         mobjBitmapGraphics.FillRectangle(Brushes.White, 0, 0, mintFormWidth, mintFormHeight)
         mobjRandom = New Random()
         mobjDrops = New List(Of Point)
      End If

   End Sub

   Private Sub frmMain_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint

      e.Graphics.DrawImage(mobjFormBitmap, 0, 0)

   End Sub


   Private Sub frmMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

      If e.KeyCode = Keys.Return Then
         If mblnDraw Then
            mblnDraw = False
         Else
            mblnDraw = True
            StartDrawing()
         End If
      End If

   End Sub

   Private Sub StartDrawing()

      Dim objPoint As Point
      Dim intIndex As Int32



      For intX As Int32 = 0 To 10000
         objPoint = New Point((mintFormWidth / 2), (mintFormHeight / 2))
         mobjDrops.Add(objPoint)
      Next

      Do While mblnDraw
         intIndex = 0
         Do While intIndex < mobjDrops.Count
            objPoint = mobjDrops(intIndex)
            objPoint.X += CType(20 - (mobjRandom.NextDouble * 40), Int16)
            objPoint.Y += CType(20 - (mobjRandom.NextDouble * 40), Int16)
            mobjDrops(intIndex) = objPoint
            If objPoint.Y > mintFormHeight Then
               objPoint.Y = mintFormHeight
            End If
            If objPoint.Y < 0 Then
               objPoint.Y = 0
            End If

            If objPoint.X > mintFormWidth Then
               objPoint.X = mintFormWidth
            End If
            If objPoint.X < 0 Then
               objPoint.X = 0
            End If

            intIndex += 1
         Loop

         mobjBitmapGraphics.FillRectangle(Brushes.White, 0, 0, mintFormWidth, mintFormHeight)

         intIndex = 0
         Do While intIndex < mobjDrops.Count
            objPoint = mobjDrops(intIndex)
            mobjBitmapGraphics.DrawRectangle(Pens.Black, objPoint.X, objPoint.Y, 1, 1)
            intIndex += 1
         Loop

         Me.Invalidate()
         Application.DoEvents()
      Loop

   End Sub


   Private Sub frmMain_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

      If mblnDraw Then
         mblnDraw = False
      End If

   End Sub

   Protected Overrides Sub OnPaintBackground(ByVal pevent As System.Windows.Forms.PaintEventArgs)

      ' to remove the flickering

   End Sub

End Class




