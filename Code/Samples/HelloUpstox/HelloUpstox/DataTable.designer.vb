<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DataTable
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DataTable))
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ExportToExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ListView1 = New ListViewFlickerFree
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SimpleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ModifyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CancelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OCOToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ModifyEntryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CancelEntryOrderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ModifyTargetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ModifyStoplossToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.COToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CancelEntryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ModifyStoplossToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportToExcelToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip3 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ExitPositionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportToExcelToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.ContextMenuStrip2.SuspendLayout()
        Me.ContextMenuStrip3.SuspendLayout()
        Me.SuspendLayout()
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExportToExcelToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(151, 26)
        '
        'ExportToExcelToolStripMenuItem
        '
        Me.ExportToExcelToolStripMenuItem.Name = "ExportToExcelToolStripMenuItem"
        Me.ExportToExcelToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.ExportToExcelToolStripMenuItem.Text = "Export to Excel"
        '
        'ListView1
        '
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.Location = New System.Drawing.Point(0, 0)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(284, 262)
        Me.ListView1.TabIndex = 1
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.OverwritePrompt = False
        Me.SaveFileDialog1.RestoreDirectory = True
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SimpleToolStripMenuItem, Me.OCOToolStripMenuItem, Me.COToolStripMenuItem, Me.ExportToExcelToolStripMenuItem2})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip2"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(151, 92)
        '
        'SimpleToolStripMenuItem
        '
        Me.SimpleToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ModifyToolStripMenuItem, Me.CancelToolStripMenuItem})
        Me.SimpleToolStripMenuItem.Name = "SimpleToolStripMenuItem"
        Me.SimpleToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.SimpleToolStripMenuItem.Text = "Simple"
        '
        'ModifyToolStripMenuItem
        '
        Me.ModifyToolStripMenuItem.Name = "ModifyToolStripMenuItem"
        Me.ModifyToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ModifyToolStripMenuItem.Text = "Modify"
        '
        'CancelToolStripMenuItem
        '
        Me.CancelToolStripMenuItem.Name = "CancelToolStripMenuItem"
        Me.CancelToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.CancelToolStripMenuItem.Text = "Cancel"
        '
        'OCOToolStripMenuItem
        '
        Me.OCOToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ModifyEntryToolStripMenuItem, Me.CancelEntryOrderToolStripMenuItem, Me.ModifyTargetToolStripMenuItem, Me.ModifyStoplossToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.OCOToolStripMenuItem.Name = "OCOToolStripMenuItem"
        Me.OCOToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.OCOToolStripMenuItem.Text = "OCO"
        '
        'ModifyEntryToolStripMenuItem
        '
        Me.ModifyEntryToolStripMenuItem.Name = "ModifyEntryToolStripMenuItem"
        Me.ModifyEntryToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.ModifyEntryToolStripMenuItem.Text = "Modify Entry"
        '
        'CancelEntryOrderToolStripMenuItem
        '
        Me.CancelEntryOrderToolStripMenuItem.Name = "CancelEntryOrderToolStripMenuItem"
        Me.CancelEntryOrderToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.CancelEntryOrderToolStripMenuItem.Text = "Cancel Entry"
        '
        'ModifyTargetToolStripMenuItem
        '
        Me.ModifyTargetToolStripMenuItem.Name = "ModifyTargetToolStripMenuItem"
        Me.ModifyTargetToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.ModifyTargetToolStripMenuItem.Text = "Modify Target"
        '
        'ModifyStoplossToolStripMenuItem
        '
        Me.ModifyStoplossToolStripMenuItem.Name = "ModifyStoplossToolStripMenuItem"
        Me.ModifyStoplossToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.ModifyStoplossToolStripMenuItem.Text = "Modify Stoploss"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'COToolStripMenuItem
        '
        Me.COToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CancelEntryToolStripMenuItem, Me.ModifyStoplossToolStripMenuItem1, Me.ExitToolStripMenuItem1})
        Me.COToolStripMenuItem.Name = "COToolStripMenuItem"
        Me.COToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.COToolStripMenuItem.Text = "CO"
        '
        'CancelEntryToolStripMenuItem
        '
        Me.CancelEntryToolStripMenuItem.Name = "CancelEntryToolStripMenuItem"
        Me.CancelEntryToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.CancelEntryToolStripMenuItem.Text = "Cancel Entry"
        '
        'ModifyStoplossToolStripMenuItem1
        '
        Me.ModifyStoplossToolStripMenuItem1.Name = "ModifyStoplossToolStripMenuItem1"
        Me.ModifyStoplossToolStripMenuItem1.Size = New System.Drawing.Size(159, 22)
        Me.ModifyStoplossToolStripMenuItem1.Text = "Modify Stoploss"
        '
        'ExitToolStripMenuItem1
        '
        Me.ExitToolStripMenuItem1.Name = "ExitToolStripMenuItem1"
        Me.ExitToolStripMenuItem1.Size = New System.Drawing.Size(159, 22)
        Me.ExitToolStripMenuItem1.Text = "Exit"
        '
        'ExportToExcelToolStripMenuItem2
        '
        Me.ExportToExcelToolStripMenuItem2.Name = "ExportToExcelToolStripMenuItem2"
        Me.ExportToExcelToolStripMenuItem2.Size = New System.Drawing.Size(150, 22)
        Me.ExportToExcelToolStripMenuItem2.Text = "Export to Excel"
        '
        'ContextMenuStrip3
        '
        Me.ContextMenuStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExitPositionToolStripMenuItem, Me.ExportToExcelToolStripMenuItem1})
        Me.ContextMenuStrip3.Name = "ContextMenuStrip3"
        Me.ContextMenuStrip3.Size = New System.Drawing.Size(151, 48)
        '
        'ExitPositionToolStripMenuItem
        '
        Me.ExitPositionToolStripMenuItem.Name = "ExitPositionToolStripMenuItem"
        Me.ExitPositionToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.ExitPositionToolStripMenuItem.Text = "Exit Position"
        '
        'ExportToExcelToolStripMenuItem1
        '
        Me.ExportToExcelToolStripMenuItem1.Name = "ExportToExcelToolStripMenuItem1"
        Me.ExportToExcelToolStripMenuItem1.Size = New System.Drawing.Size(150, 22)
        Me.ExportToExcelToolStripMenuItem1.Text = "Export to Excel"
        '
        'DataTable
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.Silver
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.Controls.Add(Me.ListView1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DataTable"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "ListView"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.ContextMenuStrip3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ExportToExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuStrip2 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ContextMenuStrip3 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SimpleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ModifyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CancelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OCOToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ModifyEntryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CancelEntryOrderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitPositionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExportToExcelToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ModifyTargetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ModifyStoplossToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents COToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CancelEntryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ModifyStoplossToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExportToExcelToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem

End Class
