<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Query.aspx.cs" Inherits="Rosteras.Query" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:Button ID="ExecuteButton" runat="server" Text="Execute" Height="678px" 
        Width="280px" onclick="ExecuteButton_Click" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TextBox ID="query" runat="server" Height="677px" Width="674px"></asp:TextBox>
</asp:Content>

