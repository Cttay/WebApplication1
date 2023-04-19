<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

        <div class="row">
             <h2>Web Hosting</h2>
                        <p>
                            You can easily find a web hosting company that offers the right mix of features and price
                            for your applications.
                        </p>
            <asp:DataList ID="DataList1" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" Width="100%"
                DataSourceID="getEmployee">
                <ItemTemplate>
                        <div class="overlay">
                            <%# Eval("IconId") %>
                        </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
        <asp:ObjectDataSource ID="getEmployee" runat="server" SelectMethod="GetEmployees" TypeName="WebData">
        </asp:ObjectDataSource>
    </asp:Content>