Imports System.Text
Imports System.ComponentModel
Imports System.IO

Public Class DataTable
    Private StrArray As String()
    Private TitleName As String = "DataTable"
    Private TimeOut As Integer = 180000
    Private TableHeight As Integer = 400
    Private TableWidth As Integer = 600
   
    Private TotalColumnWidth As Integer = 0
    Private TotalRowHeight As Integer = TableHeight

    Private ScreenWt As Integer = Screen.PrimaryScreen.WorkingArea.Width
    Private ScreenHt As Integer = Screen.PrimaryScreen.WorkingArea.Height

    Public Sub New(ByVal StrInput As String, ByVal sTitleName As String)
        MyBase.New()
        InitializeComponent()
        StrArray = StrInput.Split(New String() {vbNewLine}, StringSplitOptions.None)
        TitleName = sTitleName
    End Sub

    Private Sub DataTable_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Close()
            End If

            If e.KeyCode = Keys.A AndAlso e.Modifiers = Keys.Control Then
                CopytoClipBoard()
            End If
        Catch Ex As Exception
        End Try
    End Sub

    Public Sub CopytoClipBoard()
        Try
            Dim headers = (From ch In ListView1.Columns _
         Let header = DirectCast(ch, ColumnHeader) _
         Select header.Text).ToArray()

            Dim items = (From item In ListView1.Items _
                  Let lvi = DirectCast(item, ListViewItem) _
                  Select (From subitem In lvi.SubItems _
                      Let si = DirectCast(subitem, ListViewItem.ListViewSubItem) _
                      Select si.Text).ToArray()).ToArray()

            Dim table As String = String.Join(vbTab, headers) & Environment.NewLine
            For Each aa In items
                table &= String.Join(vbTab, aa) & Environment.NewLine
            Next
            table = table.TrimEnd(CChar(Environment.NewLine))
            Clipboard.SetText(table)
        Catch Ex As Exception
        End Try
    End Sub

    Private Sub DataTable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With ListView1
            .BackColor = Color.White
            .MultiSelect = False
            .View = View.Details
            .FullRowSelect = True
            .GridLines = True
            If TitleName.ToUpper = "ORDER BOOK" Then
                .ContextMenuStrip = ContextMenuStrip2
            ElseIf TitleName.ToUpper = "POSITIONS" Then
                .ContextMenuStrip = ContextMenuStrip3
            Else
                .ContextMenuStrip = ContextMenuStrip1
            End If
        End With

        'Adding Headers
        Try
            Dim ColNames As List(Of ColumnHeader) = New List(Of ColumnHeader)
            Dim ColumnArray() As String = StrArray(0).Split(",")
            For i = 0 To ColumnArray.Count - 1
                ColNames.Add(New ColumnHeader)
                ColNames(i).Name = ColumnArray(i)
                ColNames(i).Text = ColumnArray(i)
            Next
            ListView1.Columns.AddRange(ColNames.ToArray)
        Catch Ex As Exception
        End Try

        'Adding Data
        Try
            For i = 1 To StrArray.Count - 1
                Dim col() As String = StrArray(i).Split(",")
                Dim NewLVItem As ListViewItem = New ListViewItem(col(0))
                NewLVItem.Name = col(0)
                For j = 1 To col.Count - 1
                    NewLVItem.SubItems.Add(col(j))
                Next
                ListView1.Items.Add(NewLVItem)
            Next
        Catch Ex As Exception
        End Try

        'Fixing Table Width
        Try
            For i As Integer = 0 To ListView1.Columns.Count - 1
                If StrArray.Count > 1 Then
                    ListView1.Columns(i).Width = -2
                    TotalColumnWidth = ListView1.Columns(i).Width + TotalColumnWidth
                Else
                    ListView1.Columns(i).Width = -2
                    TotalColumnWidth = ListView1.Columns(i).Width + TotalColumnWidth
                End If
            Next i
        Catch Ex As Exception
        End Try

        'Fixing Table Width
        Try
            If StrArray.Count > 1 Then
                Dim rc As Rectangle = ListView1.Items(0).GetBounds(ItemBoundsPortion.Entire)
                TotalRowHeight = (rc.Height * ListView1.Items.Count) + 200
            End If
        Catch Ex As Exception
        End Try

        'Highlight Row
        Try
            If TitleName.ToUpper = "POSITIONS" Then
                For Each Lvi As ListViewItem In ListView1.Items
                    Dim NetQty As Integer = 0
                    Dim RPnL As Double = 0
                    Dim URPnL As Double = 0
                    Integer.TryParse(Lvi.SubItems(14).Text, NetQty)
                    Double.TryParse(Lvi.SubItems(17).Text, RPnL)
                    Double.TryParse(Lvi.SubItems(18).Text, URPnL)

                    If (NetQty = 0 AndAlso RPnL < 0) OrElse (NetQty <> 0 AndAlso URPnL < 0) Then
                        Lvi.ForeColor = Color.Red
                    Else
                        Lvi.ForeColor = Color.Blue
                    End If
                Next
            ElseIf TitleName.ToUpper = "BRIDGE POSITIONS" Then
                For Each Lvi As ListViewItem In ListView1.Items
                    Dim MTM As Double = 0
                    Double.TryParse(Lvi.SubItems(13).Text, MTM)
                    If MTM < 0 Then
                        Lvi.ForeColor = Color.Red
                    ElseIf MTM > 0 Then
                        Lvi.ForeColor = Color.Blue
                    End If
                Next
            ElseIf TitleName.ToUpper = "BRIDGE POSITIONS ALL" Then
                For Each Lvi As ListViewItem In ListView1.Items
                    Dim MTM As Double = 0
                    Double.TryParse(Lvi.SubItems(6).Text, MTM)
                    If MTM < 0 Then
                        Lvi.ForeColor = Color.Red
                    ElseIf MTM > 0 Then
                        Lvi.ForeColor = Color.Blue
                    End If
                Next
            ElseIf TitleName.ToUpper = "ORDER BOOK" Then
                For Each Lvi As ListViewItem In ListView1.Items
                    If Lvi.SubItems(10).Text = "S" Then
                        Lvi.ForeColor = Color.Red
                    ElseIf Lvi.SubItems(10).Text = "B" Then
                        Lvi.ForeColor = Color.Blue
                    End If
                Next
            ElseIf TitleName.ToUpper = "BRIDGE LOGS" Then
                For Each Lvi As ListViewItem In ListView1.Items
                    If Lvi.SubItems(6).Text = "SELL" OrElse Lvi.SubItems(6).Text = "SHORT" Then
                        Lvi.ForeColor = Color.Red
                    ElseIf Lvi.SubItems(6).Text = "BUY" OrElse Lvi.SubItems(6).Text = "COVER" Then
                        Lvi.ForeColor = Color.Blue
                    End If
                Next
            ElseIf TitleName.ToUpper = "TRADE BOOK" Then
                For Each Lvi As ListViewItem In ListView1.Items
                    If Lvi.SubItems(5).Text = "S" Then
                        Lvi.ForeColor = Color.Red
                    ElseIf Lvi.SubItems(5).Text = "B" Then
                        Lvi.ForeColor = Color.Blue
                    End If
                Next
            End If
        Catch Ex As Exception
        End Try

        Me.Text = TitleName
        Me.KeyPreview = True
        Me.Width = Math.Max(TableWidth, Math.Min(ScreenWt * 0.75, totalColumnWidth))
        Me.Height = Math.Max(TableHeight, Math.Min(ScreenHt * 0.75, totalRowHeight))
        Me.StartPosition = FormStartPosition.Manual

        Dim FrmHt As Integer = Me.Height
        Dim FrmWt As Integer = Me.Width

        Dim PointX As Integer = (ScreenWt - frmwt) / 2
        Dim PointY As Integer = (ScreenHt - frmHt) / 2
        Me.Location = New Point(PointX, PointY)


        Dim Tmr As New System.Timers.Timer()
        Tmr.Interval = TimeOut
        tmr.Enabled = True
        tmr.Start()
        AddHandler tmr.Elapsed, AddressOf CloseForm
        Me.TopMost = True
    End Sub

    Public Sub CloseForm()
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New MethodInvoker(AddressOf CloseForm))
                Exit Sub
            End If
            Me.Close()
        Catch Ex As Exception
        End Try
    End Sub

    Public Sub ExportToExcelBgWorker(ByVal FileName As String)
        Try
            If File.Exists(FileName) Then
                File.Delete(FileName)
            End If

            Using Sw As New StreamWriter(FileName, False)
                Sw.Write(String.Join(vbNewLine, StrArray))
            End Using

            If MsgBox(TitleName & " Exported to Excel. Open File Now...?") = MsgBoxResult.Ok Then
                System.Diagnostics.Process.Start(FileName)
            End If

        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    Private Sub ExportToExcel()
        Try
            SaveFileDialog1.OverwritePrompt = False
            SaveFileDialog1.Title = TitleName
            SaveFileDialog1.Filter = "Excel Files (*.csv)|*.csv"

            If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If

            If SaveFileDialog1.FileName = String.Empty Then
                Exit Sub
            End If

            ExportToExcelBgWorker(SaveFileDialog1.FileName)
        Catch Ex As Exception
        End Try
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As System.Object, e As CancelEventArgs) Handles ContextMenuStrip1.Opening
        Try
            If Me.ListView1.SelectedItems.Count = 0 Then
                e.Cancel = True
                Exit Sub
            End If
        Catch Ex As Exception
        End Try
    End Sub

    Private Sub ExportToExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportToExcelToolStripMenuItem.Click
        ExportToExcel()
    End Sub

    Private Sub ExportToExcelToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ExportToExcelToolStripMenuItem1.Click
        ExportToExcel()
    End Sub

    Private Sub ExportToExcelToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ExportToExcelToolStripMenuItem2.Click
        ExportToExcel()
    End Sub

    Private Sub ContextMenuStrip2_Opening(sender As System.Object, e As CancelEventArgs) Handles ContextMenuStrip2.Opening
        Try
            If Me.ListView1.SelectedItems.Count = 0 Then
                e.Cancel = True
                Exit Sub
            End If

            Dim Status As String = ListView1.SelectedItems(0).SubItems(19).Text
            Dim ProdType As String = ListView1.SelectedItems(0).SubItems(3).Text
            Dim ParentId As String = ListView1.SelectedItems(0).SubItems(15).Text

            If Status = "complete" OrElse Status = "rejected" OrElse Status = "cancelled" OrElse Status = "cancelled after market order" Then
                SimpleToolStripMenuItem.Enabled = False
                OCOToolStripMenuItem.Enabled = False
                COToolStripMenuItem.Enabled = False
                Exit Sub
            End If

            Dim OrdType As String = ListView1.SelectedItems(0).SubItems(4).Text

            If ProdType = "OCO" Then
                SimpleToolStripMenuItem.Enabled = True
                OCOToolStripMenuItem.Enabled = False
                COToolStripMenuItem.Enabled = False
                If ParentId = "NA" OrElse ParentId = "" Then
                    ModifyEntryToolStripMenuItem.Enabled = True
                    CancelEntryOrderToolStripMenuItem.Enabled = True
                    ModifyTargetToolStripMenuItem.Enabled = False
                    ModifyStoplossToolStripMenuItem.Enabled = False
                    ExitToolStripMenuItem.Enabled = False
                ElseIf OrdType = "L" Then
                    ModifyEntryToolStripMenuItem.Enabled = False
                    CancelEntryOrderToolStripMenuItem.Enabled = False
                    ModifyTargetToolStripMenuItem.Enabled = True
                    ModifyStoplossToolStripMenuItem.Enabled = False
                    ExitToolStripMenuItem.Enabled = True
                Else
                    ModifyEntryToolStripMenuItem.Enabled = False
                    CancelEntryOrderToolStripMenuItem.Enabled = False
                    ModifyTargetToolStripMenuItem.Enabled = False
                    ModifyStoplossToolStripMenuItem.Enabled = True
                    ExitToolStripMenuItem.Enabled = True
                End If
            ElseIf ProdType = "CO" Then
                SimpleToolStripMenuItem.Enabled = False
                OCOToolStripMenuItem.Enabled = False
                COToolStripMenuItem.Enabled = True
                If ParentId = "NA" OrElse ParentId = "" Then
                    CancelEntryToolStripMenuItem.Enabled = True
                    ModifyStoplossToolStripMenuItem1.Enabled = False
                    ExitToolStripMenuItem1.Enabled = False
                Else
                    CancelEntryToolStripMenuItem.Enabled = False
                    ModifyStoplossToolStripMenuItem1.Enabled = True
                    ExitToolStripMenuItem1.Enabled = True
                End If
            Else
                SimpleToolStripMenuItem.Enabled = True
                OCOToolStripMenuItem.Enabled = False
                COToolStripMenuItem.Enabled = False
                If Status = "after market order req received" Then
                    ModifyToolStripMenuItem.Enabled = False
                Else
                    ModifyToolStripMenuItem.Enabled = True
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ContextMenuStrip3_Opening(sender As System.Object, e As CancelEventArgs) Handles ContextMenuStrip3.Opening
        Try
            If Me.ListView1.SelectedItems.Count = 0 Then
                e.Cancel = True
                Exit Sub
            End If

            Dim ProdType As String = ListView1.SelectedItems(0).SubItems(1).Text
            Dim NetQty As Integer = 0
            Integer.TryParse(ListView1.SelectedItems(0).SubItems(14).Text, NetQty)
            If ProdType = "CO" OrElse ProdType = "OCO" OrElse NetQty = 0 Then
                ExitPositionToolStripMenuItem.Enabled = False
            Else
                ExitPositionToolStripMenuItem.Enabled = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ExitPositionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitPositionToolStripMenuItem.Click
        ExitPosition()
    End Sub

    Private Sub ExitPosition()
        Try
            Dim Exch As String = ListView1.SelectedItems(0).SubItems(0).Text
            Dim TrdSym As String = ListView1.SelectedItems(0).SubItems(2).Text
            Dim ProdType As String = ListView1.SelectedItems(0).SubItems(1).Text
            Dim LotSize As Integer = Upstox.GetLotSize(Exch, TrdSym)
            Dim NetQty As Integer = 0
            Integer.TryParse(ListView1.SelectedItems(0).SubItems(14).Text, NetQty)
            Dim Trans As String = If(NetQty > 0, "S", "B")
            Dim Qty As Integer = Math.Abs(NetQty) / LotSize
            Upstox.PlaceSimpleOrder(Exch, TrdSym, Trans, "M", Qty, ProdType)
        Catch ex As Exception
            Upstox.ShowMsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ModifyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ModifyToolStripMenuItem.Click
        ModifySimple()
    End Sub

    Sub ModifySimple()
        Try
            Dim OrderId As String = ListView1.SelectedItems(0).SubItems(16).Text
            Upstox.ShowModifyWindow(OrderId)
        Catch ex As Exception
        End Try
    End Sub

    Sub ModifyOCOMain()
        Try
            Dim OrderId As String = ListView1.SelectedItems(0).SubItems(16).Text
            Upstox.ShowModifyWindow(OrderId)
        Catch ex As Exception
        End Try
    End Sub

    Sub ModifyOCOTgt()
        Try
            Dim OrderId As String = ListView1.SelectedItems(0).SubItems(16).Text
            Upstox.ShowModifyWindow(OrderId)
        Catch ex As Exception
        End Try
    End Sub

    Sub ModifyOCOSl()
        Try
            Dim OrderId As String = ListView1.SelectedItems(0).SubItems(16).Text
            Upstox.ShowModifyWindow(OrderId)
        Catch ex As Exception
        End Try
    End Sub

    Sub ModifyCOSl()
        Try
            Dim OrderId As String = ListView1.SelectedItems(0).SubItems(16).Text
            Upstox.ShowModifyWindow(OrderId)
        Catch ex As Exception
        End Try
    End Sub

    Sub CancelSimple()
        Try
            Dim Status As String = ListView1.SelectedItems(0).SubItems(19).Text
            Dim OrderId As String = ListView1.SelectedItems(0).SubItems(16).Text
            If Status = "after market order req received" Then
                Upstox.CancelAmo(OrderId)
            Else
                Upstox.CancelSimpleOrder(OrderId)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Sub CancelOCOMain()
        Try
            Dim OrderId As String = ListView1.SelectedItems(0).SubItems(16).Text
            Upstox.CancelOCOMain(OrderId)
        Catch ex As Exception
        End Try
    End Sub

    Sub CancelCOMain()
        Try
            Dim OrderId As String = ListView1.SelectedItems(0).SubItems(16).Text
            Upstox.CancelCOMain(OrderId)
        Catch ex As Exception
        End Try
    End Sub

    Sub ExitOCO()
        Try
            Dim OrderId As String = ListView1.SelectedItems(0).SubItems(16).Text
            Upstox.ExitOCO(OrderId)
        Catch ex As Exception
        End Try
    End Sub


    Sub ExitCO()
        Try
            Dim OrderId As String = ListView1.SelectedItems(0).SubItems(16).Text
            Upstox.ExitCO(OrderId)
        Catch ex As Exception
        End Try
    End Sub


    Private Sub CancelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CancelToolStripMenuItem.Click
        CancelSimple()
    End Sub

    Private Sub CancelEntryOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CancelEntryOrderToolStripMenuItem.Click
        CancelOCOMain()
    End Sub

    Private Sub ModifyEntryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ModifyEntryToolStripMenuItem.Click
        ModifyOCOMain()
    End Sub

    Private Sub ModifyTargetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ModifyTargetToolStripMenuItem.Click
        ModifyOCOTgt()
    End Sub

    Private Sub ModifyStoplossToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ModifyStoplossToolStripMenuItem.Click
        ModifyOCOSl()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        ExitOCO()
    End Sub

    Private Sub CancelEntryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CancelEntryToolStripMenuItem.Click
        CancelCOMain()
    End Sub

    Private Sub ModifyStoplossToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ModifyStoplossToolStripMenuItem1.Click
        ModifyCOSl()
    End Sub

    Private Sub ExitToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem1.Click
        ExitCO()
    End Sub
End Class
