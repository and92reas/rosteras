<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Rosteras.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="server"> 
    <table id = "menu"> 
        <tr>
        <th> <a href = "Default.aspx">Superleague</a> </th>
        <th> <a href = "FootballLeagueNorth.aspx">Football League North</a> </th>
        <th> <a href = "FootballLeagueSouth.aspx">Football League South</a> </th>
        <th> <a href = "ComparePlayers.aspx">Compare players</a> </th>
        <th> <a href = "CompareTeams.aspx">Compare teams</a> </th>
        </tr>
        </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Label ID="teamsHTML" runat="server" Text="Label"></asp:Label>

</asp:Content>

<asp:Content ID="Content5" runat="server" 
    contentplaceholderid="ContentPlaceHolder5">
    <asp:Label ID="moreLeagueInfoHTML" runat="server" Text="Label"></asp:Label>
</asp:Content>

