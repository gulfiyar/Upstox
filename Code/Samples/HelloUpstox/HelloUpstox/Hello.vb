Imports UpstoxNet
Imports System.IO
Imports System.Threading
Imports System.Globalization
Imports System.Runtime.InteropServices

Public Class Hello
    Private Const WM_SETTEXT As Integer = &H1501
    <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, <MarshalAs(UnmanagedType.LPWStr)> ByVal lParam As String) As Int32
    End Function

    Private Sub Hello_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Upstox.ShowMsgBoxOkCancel("Are you sure exit application?") <> Windows.Forms.DialogResult.OK Then
            e.Cancel = True
        End If
    End Sub

    Private Sub Hello_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.A AndAlso e.Modifiers = Keys.Control Then
            Call CopytoClipBoard()
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SendMessage(TextBox2.Handle, WM_SETTEXT, 0, "SYMBOL")
            ComboExch.Items.AddRange(ListExch.ToArray)
            ComboExch.SelectedIndex = 0
            RichTextBox1.Text = "Hello World! App built with UpstoxNet"
            CheckBox1.CheckState = If(DisableMarketWatchUpdate, CheckState.Checked, CheckState.Unchecked)
            AddHandler Upstox.QuotesReceivedEvent, AddressOf Quotes_Received
            AddHandler Upstox.AppUpdateEvent, AddressOf App_Update
            AddHandler Upstox.OrderUpdateEvent, AddressOf Order_Update
            AddHandler Upstox.PositionUpdateEvent, AddressOf Position_Update

            'Login Instructions
            RichTextBox1.AppendText(Environment.NewLine)
            RichTextBox1.SelectionColor = Color.Blue
            RichTextBox1.AppendText("***See Help for Login procedure***")
        Catch ex As Exception
        End Try
    End Sub

    Public Sub Quotes_Received(sender As Object, e As UpstoxNet.QuotesReceivedEventArgs)
        Console.WriteLine(e.TrdSym & " LTP:" & e.LTP & " Volume:" & e.Volume & " LTT:" & e.LTT.ToString("dd-MMM-yy HH:mm:ss"))
    End Sub

    Public Sub Order_Update(sender As Object, e As UpstoxNet.OrderUpdateEventArgs)
        SetRichBox("OrderUpdate: " & "OrderId: " & e.OrderId & " Symbol: " & e.TrdSym & " Status: " & e.Status)
        Try
            SyncLock (LockAddSymbol)
                Dim SymbolExch As String = e.TrdSym & "." & e.Exch
                If Not ListSymbolMarketWatch.Contains(SymbolExch) Then
                    ListSymbolMarketWatch.Add(SymbolExch)
                    Dim InstToken As String = Upstox.GetInstToken(e.Exch, e.TrdSym)
                    AddSymbolToMarketWatch(e.Exch, e.TrdSym, InstToken)
                End If
            End SyncLock
        Catch ex As Exception
        End Try
    End Sub

    Public Sub Position_Update(sender As Object, e As UpstoxNet.PositionUpdateEventArgs)
        SetRichBox("PositionUpdate: " & "Symbol: " & e.TrdSym & " NetQty: " & e.NetQty & " Mtm: " & e.MTM)
    End Sub

    Public Sub App_Update(sender As Object, e As UpstoxNet.AppUpdateEventArgs)
        SetRichBox("AppUpdate: " & "Code: " & e.EventCode & " Message: " & e.EventMessage)
        If e.EventCode = 1 Then
            SetCtrlColor(Button38, Color.SkyBlue)
            SetCtrlColor(Button41, Color.Salmon) 'Ready
        End If
        If e.EventCode = 2 Then
            SetCtrlColor(Button38, Color.Salmon) 'Login
            SetCtrlColor(Button37, Color.Salmon) 'Access
            SetCtrlColor(Button39, Color.Salmon) 'Symbol
            SetCtrlColor(Button40, Color.Salmon) 'WebSock
            SetCtrlColor(Button41, Color.Salmon) 'Ready
            SetCtrl(ComboExch, False)
            SetCtrl(TextBox2, False)
            SetCtrl(ButtonAdd, False)
            SetCtrl(ButtonDel, False)
        End If
        If e.EventCode = 3 Then
            SetCtrlColor(Button37, Color.SkyBlue) 'Access
        End If
        If e.EventCode = 4 Then
            SetCtrlColor(Button39, Color.SkyBlue) 'Symbol
            SetCtrlColor(Button41, Color.SkyBlue) 'Reday
            ProcessSymbolListMarketWatch()
            Dim t As New Thread(AddressOf UpdateListViewThread)
            t.IsBackground = True
            t.Start()
            SetCtrl(ComboExch, True)
            SetCtrl(TextBox2, True)
            SetCtrl(ButtonAdd, True)
            SetCtrl(ButtonDel, True)
        End If
        If e.EventCode = 5 Then
            SetCtrlColor(Button40, Color.Salmon) 'WebSock
            SetCtrlColor(Button41, Color.Salmon) 'Reday
        End If
        If e.EventCode = 7 Then
            SetCtrlColor(Button40, Color.Salmon) 'WebSock
            SetCtrlColor(Button41, Color.Salmon) 'Reday
        End If
        If e.EventCode = 8 Then
            SetCtrlColor(Button40, Color.SkyBlue) 'WebSock
            SetCtrlColor(Button41, Color.SkyBlue) 'Reday
        End If
    End Sub

    Delegate Sub SetRichBoxCallback(ByVal StrText As String)
    Public Sub SetRichBox(ByVal StrText As String)
        Try
            Dim tt As String = DateTime.Now.ToString("HH:mm:ss") & ": "
            If RichTextBox1.InvokeRequired Then
                Dim d As New SetRichBoxCallback(AddressOf SetRichBox)
                Me.RichTextBox1.Invoke(d, New Object() {StrText})
                Exit Sub
            End If
            RichTextBox1.Text &= Environment.NewLine & tt & StrText
        Catch ex As Exception
        End Try
    End Sub

    Delegate Sub SetCtrlCallback(ByVal BtnCtrl As Control, ByVal Status As Boolean)
    Public Sub SetCtrl(ByVal BtnCtrl As Control, ByVal Status As Boolean)
        Try
            If BtnCtrl.InvokeRequired Then
                Dim d As New SetCtrlCallback(AddressOf SetCtrl)
                BtnCtrl.Invoke(d, New Object() {BtnCtrl, Status})
                Exit Sub
            End If
            BtnCtrl.Enabled = Status
        Catch ex As Exception
        End Try
    End Sub

    Delegate Sub SetCtrlColorCallback(ByVal BtnCtrl As Control, ByVal Color As Color)
    Public Sub SetCtrlColor(ByVal BtnCtrl As Control, ByVal Color As Color)
        Try
            If BtnCtrl.InvokeRequired Then
                Dim d As New SetCtrlColorCallback(AddressOf SetCtrlColor)
                BtnCtrl.Invoke(d, New Object() {BtnCtrl, Color})
                Exit Sub
            End If
            BtnCtrl.BackColor = Color
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If String.IsNullOrEmpty(Upstox.Api_Key) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If String.IsNullOrEmpty(Upstox.Api_Secret) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If String.IsNullOrEmpty(Upstox.Redirect_Url) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If Upstox.Login_Status Then
                Upstox.ShowMsgBox("Already Logged-in")
                Exit Sub
            End If

            Upstox.Login()

        Catch ex As Exception
            Upstox.ShowMsgBox(ex.Message)
        End Try
    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged
        RichTextBox1.SelectionStart = RichTextBox1.Text.Length
        RichTextBox1.ScrollToCaret()
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        Try
            If String.IsNullOrEmpty(Upstox.Api_Key) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If String.IsNullOrEmpty(Upstox.Api_Secret) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If String.IsNullOrEmpty(Upstox.Redirect_Url) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If Upstox.Authorization_Status Then
                Upstox.ShowMsgBox("Access Token Received")
                Exit Sub
            End If

            If Not Upstox.Login_Status Then
                Upstox.ShowMsgBox("Please login to get access code")
                Exit Sub
            End If

            Upstox.GetAccessToken()
            Upstox.GetMasterContract()
        Catch ex As Exception
            Upstox.ShowMsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Try
            Dim StrInput As String = Upstox.GetHoldings()
            Dim Frm As New DataTable(StrInput, "HOLDINGS")
            Frm.ShowDialog()
        Catch ex As Exception
            Upstox.ShowMsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Try
            Dim StrInput As String = Upstox.GetOrderBook()
            Dim Frm As New DataTable(StrInput, "ORDER BOOK")
            Frm.ShowDialog()
        Catch ex As Exception
            Upstox.ShowMsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Try
            Dim StrInput As String = Upstox.GetTradeBook()
            Dim Frm As New DataTable(StrInput, "TRADE BOOK")
            Frm.ShowDialog()
        Catch ex As Exception
            Upstox.ShowMsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Try
            Dim StrInput As String = Upstox.GetPositions()
            Dim Frm As New DataTable(StrInput, "POSITIONS")
            Frm.ShowDialog()
        Catch ex As Exception
            Upstox.ShowMsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Try
            Dim StrInput As String = Upstox.GetFunds()
            Dim Frm As New DataTable(StrInput, "FUNDS")
            Frm.ShowDialog()
        Catch ex As Exception
            Upstox.ShowMsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        Try
            If String.IsNullOrEmpty(Upstox.Api_Key) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If String.IsNullOrEmpty(Upstox.Api_Secret) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If String.IsNullOrEmpty(Upstox.Redirect_Url) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If Upstox.Login_Status Then
                Upstox.ShowMsgBox("Already Logged-in")
                Exit Sub
            End If

            Upstox.Login("chrome")
        Catch ex As Exception
            Upstox.ShowMsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        Try
            If String.IsNullOrEmpty(Upstox.Api_Key) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If String.IsNullOrEmpty(Upstox.Api_Secret) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If String.IsNullOrEmpty(Upstox.Redirect_Url) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If Upstox.Authorization_Status Then
                Upstox.ShowMsgBox("Access Token Received")
                Exit Sub
            End If

            Dim Code As String = Upstox.ShowInputBox("Enter your Access code", "copied from browser Url")

            Upstox.GetAccessToken(Code)
            Upstox.GetMasterContract()

        Catch ex As Exception
            Upstox.ShowMsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click
        Try
            Upstox.ShowSettingsWindow()
        Catch ex As Exception
            Upstox.ShowMsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click
        Try
            Upstox.BatchOrderPlacement()
        Catch ex As Exception
            Upstox.ShowMsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button29_Click(sender As Object, e As EventArgs) Handles Button29.Click
        Try
            Upstox.ShowModifyWindow()
        Catch ex As Exception
            Upstox.ShowMsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
        Try
            Upstox.ShowOrderWindow()
        Catch ex As Exception
            Upstox.ShowMsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click
        Try
            Dim Obj As Object() = Upstox.ShowHistDataInputBox()
            Dim Exch As String = Obj(0)
            Dim TrdSym As String = Obj(1)
            Dim Interval As String = Obj(2)
            Dim NoOfDays As Integer = Obj(3)

            Dim ToDate As Date = Today
            Dim FromDate As Date = ToDate.AddDays(-NoOfDays)

            Dim StrInput As String = String.Join(vbNewLine, Upstox.GetHistData(Exch, TrdSym, Interval, FromDate, ToDate))
            Dim Frm As New DataTable(StrInput, "HISTORICAL DATA : " & TrdSym.ToUpper)
            Frm.ShowDialog()

        Catch ex As Exception
            Upstox.ShowMsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
        Try
            If Not Upstox.Login_Status Then
                Upstox.ShowMsgBox("User not logged-in")
                Exit Sub
            End If
            Dim Exch As String = Upstox.ShowInputBox("Enter Exch", "Example: NSE_EQ")
            Dim Trdsym As String = Upstox.ShowInputBox("Enter TrdSymbol", "Example: ICICIBANK")
            Upstox.ShowMarketDepth(Exch, Trdsym)
        Catch ex As Exception
            Upstox.ShowMsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        Try
            Dim StrInput As String = Upstox.GetBridgeLogs()
            Dim Frm As New DataTable(StrInput, "BRIDGE LOGS")
            Frm.ShowDialog()
        Catch ex As Exception
            Upstox.ShowMsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        Try
            Upstox.ShowOrderWindowBridge()
        Catch ex As Exception
            Upstox.ShowMsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.CheckState = CheckState.Checked Then
            DisableMarketWatchUpdate = True
            My.Settings.DisableMarketWatchUpdate = DisableMarketWatchUpdate
            My.Settings.Save()
        Else
            DisableMarketWatchUpdate = False
            My.Settings.DisableMarketWatchUpdate = DisableMarketWatchUpdate
            My.Settings.Save()
        End If
    End Sub

    Private Sub Button36_Click(sender As Object, e As EventArgs) Handles Button36.Click
        Try
            Dim StrInput As String = Upstox.GetBridgePositions()
            Dim Frm As New DataTable(StrInput, "BRIDGE POSITIONS")
            Frm.ShowDialog()
        Catch ex As Exception
            Upstox.ShowMsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
        Try
            Dim StrInput As String = Upstox.GetBridgePositionsAll()
            Dim Frm As New DataTable(StrInput, "BRIDGE POSITIONS ALL")
            Frm.ShowDialog()
        Catch ex As Exception
            Upstox.ShowMsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button34_Click(sender As Object, e As EventArgs) Handles Button34.Click
        Try
            Dim d As Double = Upstox.CheckOrderExecutionLatency
            Upstox.ShowMsgBox("Order executed in " & d & " milliseconds")
        Catch ex As Exception
            Upstox.ShowMsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            If String.IsNullOrEmpty(Upstox.Api_Key) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If String.IsNullOrEmpty(Upstox.Api_Secret) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If String.IsNullOrEmpty(Upstox.Redirect_Url) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If Not Upstox.Login_Status Then
                Upstox.ShowMsgBox("User not logged-in")
                Exit Sub
            End If

            If Upstox.Logout_Status Then
                Upstox.ShowMsgBox("Already Logged-out")
                Exit Sub
            End If

            Upstox.Logout()
        Catch ex As Exception
            Upstox.ShowMsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DelSymbolFromMarketWatch()
        Try
            If ListView1.SelectedItems.Count <= 0 Then
                Exit Sub
            End If

            If Not Upstox.Login_Status Then
                Exit Sub
            End If

            If Not Upstox.Symbol_Download_Status Then
                Exit Sub
            End If

            If Upstox.Logout_Status Then
                Exit Sub
            End If

            With ListView1
                Dim InstToken As String = .SelectedItems(0).Text
                Dim Exch As String = .SelectedItems(0).SubItems(1).Text
                Dim TrdSym As String = .SelectedItems(0).SubItems(2).Text

                Dim SymbolExch As String = TrdSym & "." & Exch
                ListSymbolMarketWatch.Remove(SymbolExch)

                Upstox.UnSubscribeQuotes(Exch, TrdSym)

                .SelectedItems(0).Remove()
            End With
        Catch ex As Exception
            Upstox.ShowMsgBox(ex.Message)
        End Try
    End Sub

    Private Sub AddSymbolToMarketWatch(ByVal Exch As String, ByVal TrdSym As String, ByVal InstToken As String)
        Try
            If ListView1.InvokeRequired Then
                ListView1.Invoke(New MethodInvoker(Sub() AddSymbolToMarketWatch(Exch, TrdSym, InstToken)))
                Exit Sub
            End If

            If ListView1.Items.Count > 0 Then
                Dim OldItem As ListViewItem
                With ListView1
                    OldItem = .FindItemWithText(InstToken, False, 0, False)
                    If Not OldItem Is Nothing Then
                        Exit Sub
                    End If
                End With
            End If

            With ListView1.Items.Add(InstToken)
                .SubItems.Add(Exch)
                .SubItems.Add(TrdSym)
                .SubItems.Add("0")
                .SubItems.Add("0")
                .SubItems.Add("0")
                .SubItems.Add("0")
                .SubItems.Add("0")
                .UseItemStyleForSubItems = False
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub AddSymbolToMarketWatch()
        Try
            If ComboExch.SelectedIndex < 0 Then
                Upstox.ShowMsgBox("Enter Exchange")
                ComboExch.Focus()
                Exit Sub
            End If

            If String.IsNullOrEmpty(TextBox2.Text) Then
                Upstox.ShowMsgBox("Enter Trade Symbol")
                TextBox2.Focus()
                Exit Sub
            End If

            If Not Upstox.Login_Status Then
                Exit Sub
            End If

            If Not Upstox.Symbol_Download_Status Then
                Exit Sub
            End If

            If Upstox.Logout_Status Then
                Exit Sub
            End If

            Dim TrdSym As String = TextBox2.Text.ToUpper
            Dim Exch As String = ComboExch.SelectedItem
            Dim InstToken As String = Upstox.GetInstToken(Exch, TrdSym).ToUpper

            If ListView1.Items.Count > 0 Then
                Dim OldItem As ListViewItem
                With ListView1
                    OldItem = .FindItemWithText(InstToken, False, 0, False)
                    If Not OldItem Is Nothing Then
                        Upstox.ShowMsgBox("Symbol Already Available in Market Watch")
                        Exit Sub
                    End If
                End With
            End If

            With ListView1.Items.Add(InstToken)
                .SubItems.Add(Exch)
                .SubItems.Add(TrdSym)
                .SubItems.Add("0")
                .SubItems.Add("0")
                .SubItems.Add("0")
                .SubItems.Add("0")
                .SubItems.Add("0")
                .UseItemStyleForSubItems = False
            End With

            Dim SymbolExch As String = TrdSym & "." & Exch
            ListSymbolMarketWatch.Add(SymbolExch)

            Upstox.SubscribeQuotes(Exch, TrdSym)

            TextBox2.Text = StringEmpty
        Catch ex As Exception
            Upstox.ShowMsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ButtonAdd_Click(sender As Object, e As EventArgs) Handles ButtonAdd.Click
        AddSymbolToMarketWatch()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        DelSymbolFromMarketWatch()
    End Sub

    Private Sub ProcessSymbolListMarketWatch()
        Try
            If ListView1.InvokeRequired Then
                ListView1.Invoke(New MethodInvoker(AddressOf ProcessSymbolListMarketWatch))
                Exit Sub
            End If

            Dim MWSymbols As String = Upstox.GetMWSymbols
            Dim SymbolArray() As String = MWSymbols.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
            ListSymbolMarketWatch.AddRange(SymbolArray)

            For i = 0 To SymbolArray.Count - 1
                Try
                    Dim StrArr() As String = SymbolArray(i).Split(New String() {"."}, StringSplitOptions.RemoveEmptyEntries)
                    Dim Exch As String = StrArr(1)
                    Dim TrdSym As String = StrArr(0)
                    Dim InstToken As String = Upstox.GetInstToken(Exch, TrdSym)
                    With ListView1.Items.Add(InstToken)
                        .SubItems.Add(Exch)
                        .SubItems.Add(TrdSym)
                        .SubItems.Add("0")
                        .SubItems.Add("0")
                        .SubItems.Add("0")
                        .SubItems.Add("0")
                        .SubItems.Add("0")
                        .UseItemStyleForSubItems = False
                    End With
                Catch ex As Exception
                End Try
            Next
        Catch ex As Exception
            Upstox.ShowMsgBox(ex.Message)
        End Try
    End Sub

    Private Sub UpdateListViewThread()
        Do
            Threading.Thread.Sleep(1000)

            If Not Upstox.Login_Status Then
                Exit Sub
            End If

            If Not Upstox.Symbol_Download_Status Then
                Exit Sub
            End If

            If Upstox.Logout_Status Then
                Exit Sub
            End If

            If DisableMarketWatchUpdate Then
                Continue Do
            End If

            UpdateListView()
        Loop
    End Sub

    Private Sub UpdateListView()
        Try
            If ListView1.InvokeRequired Then
                ListView1.Invoke(New MethodInvoker(AddressOf UpdateListView))
                Exit Sub
            End If

            ListView1.BeginUpdate()

            For i = 0 To ListView1.Items.Count - 1
                Try
                    With ListView1.Items(i)
                        Dim InstToken As String = .SubItems(0).Text
                        Dim Exch As String = .SubItems(1).Text
                        Dim TrdSym As String = .SubItems(2).Text

                        Dim Ltp As Double = Upstox.GetLtp(Exch, TrdSym)
                        If Ltp = 0 Then
                            Continue For
                        End If

                        Dim Volume As Double = Upstox.GetVolume(Exch, TrdSym)
                        Dim NetQty As Double = Upstox.GetNetQty(Exch, TrdSym)
                        Dim MTM As Double = Upstox.GetMtm(Exch, TrdSym)
                        Dim UpdateTime As String = Upstox.GetLUT(Exch, TrdSym).ToString("dd-MMM-yyyy HH:mm:ss")
                        .SubItems(3).Text = Ltp
                        .SubItems(4).Text = Volume
                        .SubItems(5).Text = NetQty
                        .SubItems(6).Text = MTM
                        .SubItems(7).Text = UpdateTime
                    End With
                Catch ex As Exception
                End Try
            Next
            ListView1.EndUpdate()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            AddSymbolToMarketWatch()
        End If
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        Dim Exch As String = ListView1.SelectedItems(0).SubItems(1).Text
        Dim TrdSym As String = ListView1.SelectedItems(0).SubItems(2).Text
        ShowMarketDepth(Exch, TrdSym)
    End Sub

    Private Sub ListView1_KeyDown(sender As Object, e As KeyEventArgs) Handles ListView1.KeyDown
        If e.KeyCode = Keys.Delete Then
            DelSymbolFromMarketWatch()
        End If
    End Sub

    Public Sub CopytoClipBoard()
        Try
            Dim Headers = (From ch In ListView1.Columns Let header = DirectCast(ch, ColumnHeader) Select header.Text).ToArray()
            Dim Items = (From item In ListView1.Items Let lvi = DirectCast(item, ListViewItem) Select (From subitem In lvi.SubItems Let si = DirectCast(subitem, ListViewItem.ListViewSubItem) Select si.Text).ToArray()).ToArray()

            Dim Table As String = String.Join(vbTab, Headers) & Environment.NewLine
            For Each Aaa In Items
                Table &= String.Join(vbTab, Aaa) & Environment.NewLine
            Next
            Table = Table.TrimEnd(CChar(Environment.NewLine))
            Clipboard.SetText(Table)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ShowMarketDepth(ByVal Exch As String, ByVal TrdSym As String)
        Try
            If ListView1.SelectedItems.Count <= 0 Then
                Exit Sub
            End If

            If Not Upstox.Login_Status Then
                Exit Sub
            End If

            If Not Upstox.Symbol_Download_Status Then
                Exit Sub
            End If

            If Upstox.Logout_Status Then
                Exit Sub
            End If

            Upstox.ShowMarketDepth(Exch, TrdSym)
        Catch ex As Exception
            Upstox.ShowMsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Help.ShowDialog()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        About.ShowDialog()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Upstox.GetHistDataBatch()
        Catch ex As Exception
            Upstox.ShowMsgBox(ex.Message)
        End Try
    End Sub
End Class
