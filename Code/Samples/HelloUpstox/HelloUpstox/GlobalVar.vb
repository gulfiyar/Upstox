Imports UpstoxNet

Module GlobalVar
    Public Upstox As New Upstox 'Initialize
    Public DisableMarketWatchUpdate As Boolean = My.Settings.DisableMarketWatchUpdate
    Public Const StringEmpty As String = ""
    Public ListExch As New List(Of String)(New String() {"NSE_EQ", "NSE_FO", "BSE_EQ", "NCD_FO", "BCD_FO", "MCX_FO", "NSE_INDEX", "BSE_INDEX"})
    Public ListSymbolMarketWatch As New List(Of String)
    Public LockAddSymbol As New Object
End Module
