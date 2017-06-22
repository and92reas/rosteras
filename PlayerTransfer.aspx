<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDatabase.Master" AutoEventWireup="true" CodeBehind="PlayerTransfer.aspx.cs" Inherits="Rosteras.PlayerTransfer" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <asp:Panel ID="Panel1" CssClass = "selpanel1" runat="server" Width="325px">
        <asp:Label ID="TeamSelection" runat="server" CssClass = "TeamSelection1" Text="<h6> Τωρινή Ομάδα </h6>"></asp:Label>
        <asp:DropDownList ID="ddlTeams" runat="server" OnSelectedIndexChanged ="Team_Selecting" DataSourceID="Roster" CssClass = "ddlTeams" 
        DataTextField="Ονομασία" DataValueField="Ονομασία" AutoPostBack = "True" >
        </asp:DropDownList>
        <asp:SqlDataSource ID="Roster" runat="server" OnSelecting ="Team_Selecting"
        ConnectionString="<%$ ConnectionStrings:ΡόστερConnectionString %>" 
        SelectCommand="SELECT [Ονομασία] FROM [Ομάδες] ORDER BY [Ονομασία]"> 
        </asp:SqlDataSource>

        <asp:Label ID="PlayerSelection1" runat="server" CssClass = "PlayerSelection" Text="<h6> Παίκτης </h6>"></asp:Label>
    <asp:DropDownList ID="ddlPlayers" runat="server" DataSourceID="Roster2" CssClass = "ddlPlayers" 
        DataTextField="Όνομα" DataValueField="Όνομα">
    </asp:DropDownList>
    <asp:SqlDataSource ID="Roster2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ΡόστερConnectionString %>" 
        
        SelectCommand="SELECT [Όνομα] FROM [Παίκτες] WHERE ([Τρέχουσα_Ομάδα] = @Τρέχουσα_Ομάδα) ORDER BY [Όνομα],[ID_Παίκτη]">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlTeams" Name="Τρέχουσα_Ομάδα" 
                PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    </asp:Panel> 
    <asp:Panel ID="Panel5" runat="server">
    <asp:Panel ID="Panel2" runat="server">
        <asp:Label ID="TeamSelection2" runat="server" CssClass = "PlayerSelection" Text="<h6>Επόμενη Ομάδα<h6>"></asp:Label>
        <asp:DropDownList ID="ddlTeams2" runat="server" AutoPostBack="True" CssClass="ddlTeams" DataSourceID="Roster" DataTextField="Ονομασία" DataValueField="Ονομασία">
        </asp:DropDownList>

         </asp:Panel>
        <asp:Panel ID="Panel3" runat="server">
            <ul style= "list-style: none">
            <li style="margin-left: 264px"><asp:Label ID="pstflag" runat="server" AssociatedControlID ="PastTeamsFlag" Text="Don't add previous team to previous teams"></asp:Label>
            <asp:CheckBox ID="PastTeamsFlag" runat="server" /> </li>
            <li> <asp:Label ID="pstflag2" runat="server" AssociatedControlID ="PastTeamsFlag2" Text="Add more previous teams"> </asp:Label>
            <asp:CheckBox ID="PastTeamsFlag2" OnCheckedChanged ="ActivatePastTeams" AutoPostBack ="true" runat="server" /> </li>
            <li> <asp:Label ID="loanedByTeam" runat="server" AssociatedControlID ="loanedBy" Text="Loaned By"> </asp:Label>
            <asp:TextBox ID="loanedBy" runat="server" Text ="-"></asp:TextBox></li>
             </ul>
            
        </asp:Panel>
        <asp:Panel ID="Panel4" runat="server"> 
            <asp:Label ID="ptlabel" runat="server" AssociatedControlID ="PastTeamsNumber" Visible ="false" Text="Past Teams Number"></asp:Label>
            <asp:TextBox ID="PastTeamsNumber" CssClass ="PastTeamsNumber" Visible="false" AutoPostBack="true" OnTextChanged = "AdjustTextBoxes" runat="server"></asp:TextBox>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="Panel6" runat="server">
        <asp:Button ID="Submit" CssClass = "SubmitButton2" runat="server" Text="Submit" OnClick="Submit_Click" />
      </asp:Panel>

   
    
</asp:Content>

