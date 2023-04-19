<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CallDataWithParam.aspx.cs"
    Inherits="WebApplication1.CallDataWithParam" %>

    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <title></title>
    </head>

    <body>
        <form id="form1" runat="server">
            <asp:TextBox ID="txtParameter" runat="server"></asp:TextBox>
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true"></asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </form>
    </body>

    </html>