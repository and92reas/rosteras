<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PlayerInfo.aspx.cs" Inherits="Rosteras.PlayerInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <asp:Label ID="playerTeamDataHTML" runat="server" Text=""></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
     <asp:Label ID="PlayerOrderHTML" runat="server" Text=""></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder5" runat="server">
    <table id = 'menu2'>
            <tr>
                <th><a href = "League.aspx?league=Superleague">Superleague</a> </th>
            </tr>
            <tr>
                <th><a href = "League.aspx?league=FootballLeague">Football League</a> </th>
            </tr>
            <tr>
                <th><a href = "ComparePlayers.aspx">Compare players</a> </th>
            </tr>
            <tr>
                <th><a href = "CompareTeams.aspx">Compare teams</a> </th>
            </tr>
        </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="playerInfoHTML" runat="server" Text=""></asp:Label>
</asp:Content>
