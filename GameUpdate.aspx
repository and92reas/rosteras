<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDatabase.Master" AutoEventWireup="true" CodeBehind="GameUpdate.aspx.cs" Inherits="Rosteras.GameUpdate" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel0" CssClass ="WhichLeague" runat="server">
    <asp:Label ID="leagueIdent" runat="server"  Text="<h6> Πρωτάθλημα </h6>"></asp:Label>
        <asp:DropDownList ID="dllLeague" runat="server" DataSourceID="SqlDataSource1"  
        DataTextField="Ονομασία" DataValueField="Ονομασία" AutoPostBack = "True" >
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ΡόστερConnectionString %>" 
        SelectCommand="SELECT [Ονομασία] FROM [Πρωταθλήματα] WHERE ([Ονομασία]<>'Άλλο') ORDER BY [Τάξη],[Ονομασία]" > 
        </asp:SqlDataSource>
     </asp:Panel>
    <asp:Panel ID="Panel3" CssClass ="FirstTeamUpdated" runat="server">
    <asp:Panel ID="Panel1" CssClass ="FirstTeamUpdated" runat="server">
        
        <asp:TextBox ID="PlayerNumber1" Enabled ="True" Text ="0" runat="server"></asp:TextBox>
        <asp:Label ID="Team11" runat="server" CssClass = "TeamUpdated1" Text="<h6> Γηπεδούχος Ομάδα </h6>"></asp:Label>
        <asp:DropDownList ID="ddlTeams" runat="server" OnTextChanged ="Team_Changing" DataSourceID="Roster" CssClass = "ddlTeams" 
        DataTextField="Ονομασία" DataValueField="Ονομασία" AutoPostBack = "True" >
        </asp:DropDownList>
        <asp:SqlDataSource ID="Roster" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ΡόστερConnectionString %>" 
        SelectCommand="SELECT [Ονομασία] FROM [Ομάδες] WHERE ([Πρωτάθλημα]<>'Άλλο' AND [Ονομασία] <> 'Εκτός Βάσης')   ORDER BY [Ονομασία]" 
         ></asp:SqlDataSource>
    </asp:Panel>

    <asp:Panel ID="Panel2" CssClass ="SecondTeamUpdated" runat="server">
        <asp:TextBox ID="PlayerNumber2" Enabled ="True" Text ="0" runat="server"></asp:TextBox>
        <asp:Label ID="Team12" runat="server" CssClass = "TeamUpdated2" Text="<h6> Φιλοξενούμενη Ομάδα </h6>"></asp:Label>
        <asp:DropDownList ID="ddlTeams2" runat="server" OnTextChanged ="Team_Changing" DataSourceID="Roster0" CssClass = "ddlTeams" 
        DataTextField="Ονομασία" DataValueField="Ονομασία" AutoPostBack = "True" >
        </asp:DropDownList>
        <asp:SqlDataSource ID="Roster0" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ΡόστερConnectionString %>" 
        SelectCommand="SELECT [Ονομασία] FROM [Ομάδες] WHERE ([Πρωτάθλημα]<>'Άλλο' AND [Ονομασία] <> 'Εκτός Βάσης') ORDER BY [Ονομασία]" 
         > 
        </asp:SqlDataSource>
    </asp:Panel>
    </asp:Panel> 
     <asp:Panel ID="Panel6" runat="server">
        <asp:Button ID="Submit" CssClass = "SubmitButton2" runat="server" Text="Submit" OnClick="Submit_Click" />
      </asp:Panel>  
</asp:Content>
