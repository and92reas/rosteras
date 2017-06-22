<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FootballLeague.aspx.cs" Inherits="Rosteras.FootballLeague" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <table id = 'menu'> 
        <tr>
        <th> <a href = "Default.aspx">Superleague</a> </th>
        <th> <a href = "FootballLeague.aspx">Football League</a> </th>
        <th> <a href = "ComparePlayers.aspx">Compare players</a> </th>
        <th> <a href = "CompareTeams.aspx">Compare teams</a> </th>
        </tr>
        </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Label ID="teamsHTML" runat="server" Text="Label"></asp:Label>

</asp:Content>

<asp:Content ID="Content4" runat="server" 
    contentplaceholderid="ContentPlaceHolder4">
    <asp:Label ID="leagueInfoHTML" runat="server" Text="Label"></asp:Label>
</asp:Content>


