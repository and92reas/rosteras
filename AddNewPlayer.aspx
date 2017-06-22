<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDatabase.Master" AutoEventWireup="true" CodeBehind="AddNewPlayer.aspx.cs" Inherits="Rosteras.AddNewPlayer" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="newPlayerPanel" runat="server">
    <asp:Label ID="newPlayerWarnings" cssclass ="newPlayerWarnings" runat="server" Text=""></asp:Label>
    <table id = "newPlayer" >
    <tr>
    <th><asp:Label ID="lastPlayerID" runat="server" Text="Last Player's ID"></asp:Label>
    </th>
    <td><asp:TextBox ID="lpid" runat="server"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <th><asp:Label ID="newPlayerID" runat="server" Text="New Player's ID"></asp:Label>
    </th>
    <td><asp:TextBox ID="npid" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <th><asp:Label ID="newPlayerName" runat="server" Text="New Player's Name"></asp:Label>
    </th>
    <td><asp:TextBox ID="npname" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <th><asp:Label ID="newPlayerTeam" runat="server" Text="New Player's Team"></asp:Label>
    </th>
    <td><asp:TextBox ID="npteam" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <th><asp:Label ID="newPlayerHeight" runat="server" Text="New Player's Height"></asp:Label>
    </th>
    <td><asp:TextBox ID="npheight" Text ="0" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <th><asp:Label ID="newPlayerSapps" runat="server" Text="New Player's total Superleague Apps"></asp:Label>
    </th>
    <td><asp:TextBox ID="npsapps" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <th><asp:Label ID="newPlayerSgoals" runat="server" Text="New Player's total Superleague Goals"></asp:Label>
    </th>
    <td><asp:TextBox ID="npsgoals" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <th><asp:Label ID="newPlayerFapps" runat="server" Text="New Player's total Football League Apps"></asp:Label>
    </th>
    <td><asp:TextBox ID="npfapps" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <th><asp:Label ID="newPlayerFgoals" runat="server" Text="New Player's Football total League Goals"></asp:Label>
    </th>
    <td><asp:TextBox ID="npfgoals" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <th><asp:Label ID="newPlayerSPapps" runat="server" Text="New Player's present Superleague Apps"></asp:Label>
    </th>
    <td><asp:TextBox ID="npspapps" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <th><asp:Label ID="newPlayerSPgoals" runat="server" Text="New Player's present Superleague Goals"></asp:Label>
    </th>
    <td><asp:TextBox ID="npspgoals" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <th><asp:Label ID="newPlayerFPapps" runat="server" Text="New Player's present Football League Apps"></asp:Label>
    </th>
    <td><asp:TextBox ID="npfpapps" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <th><asp:Label ID="newPlayerFPgoals" runat="server" Text="New Player's Football present League Goals"></asp:Label>
    </th>
    <td><asp:TextBox ID="npfpgoals" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <th><asp:Label ID="newPlayerAge" runat="server" Text="New Player's Age"></asp:Label>
    </th>
    <td><asp:TextBox ID="npage" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <th><asp:Label ID="newPlayerPosition" runat="server" Text="New Player's Position"></asp:Label>
    </th>
    <td><asp:TextBox ID="npposition" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <th><asp:Label ID="newPlayerCountry" runat="server" Text="New Player's Country"></asp:Label>
    </th>
    <td><asp:TextBox ID="npcountry" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <th><asp:Label ID="newPlayerLoanedby" runat="server" Text="New Player Loaned By"></asp:Label>
    </th>
    <td><asp:TextBox ID="nploanedby" Text ="-" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <th><asp:Label ID="newPlayerPastTeamNumber" runat="server" Text="New Player's number of Past Teams"></asp:Label>
    </th>
    <td><asp:TextBox ID="npptnumber" AutoPostBack="true" OnTextChanged = "AdjustTextBoxes" runat="server"></asp:TextBox></td>
    </tr>        
    </table>
    <asp:Panel ID="PastTeamsPanel" runat="server"></asp:Panel>
    <asp:Button ID="newPlayerSubmit" runat="server" Text="Submit" OnClick="newPlayerSubmit_Click" />
    </asp:Panel>
</asp:Content>

