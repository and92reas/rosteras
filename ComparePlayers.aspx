<%@ Page Title="" Language="C#" MasterPageFile="~/Nested1.Master" AutoEventWireup="true" CodeBehind="ComparePlayers.aspx.cs" Inherits="Rosteras.ComparePlayers" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:Panel ID="selPanel5" CssClass = 'selPanel5' runat="server">
        
        <asp:Panel ID= "labelPanel1" CssClass = "labPanel1" runat="server" >
        <asp:Label ID="teamSearchHTML" CssClass = 'teamSearchHTML' runat="server" Text=""></asp:Label>
        </asp:Panel>

        <asp:DropDownList ID="ddlTeams3" CssClass = 'ddlTeams3' runat="server" AutoPostBack = "True" 
        DataSourceID="Roster9" DataTextField="Ομάδα" DataValueField="Ομάδα">
        </asp:DropDownList>
        <asp:SqlDataSource ID="Roster9" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ΡόστερConnectionString %>" 
        SelectCommand="SELECT DISTINCT [Ομάδα] FROM [Προηγούμενες_Ομάδες] ORDER BY [Ομάδα]">
        </asp:SqlDataSource>
        
        <asp:Panel ID= "buttonPanel" CssClass = "butPanel" runat="server" >
        <asp:Button ID="SearchButton1" CssClass = 'searchButton' runat="server" Text="Search" 
        onclick="SearchButton_Click" />
        </asp:Panel>
                <asp:Panel ID="Panel6" CssClass = 'selPanel7' runat="server">
                    <asp:Label ID="playersPassedFromTeamHTML" CssClass = 'resultsBelow2' runat="server" Text=""></asp:Label>
                </asp:Panel>
    </asp:Panel>
        
    <asp:Panel ID="Panel5" CssClass = 'selPanel5' runat="server">
        
        <asp:Panel ID= "labelPanel2" CssClass = "labPanel2" runat="server" >
        <asp:Label ID="teamSearchHTML2" CssClass = 'teamSearchHTML2' runat="server" Text=""></asp:Label>
        </asp:Panel>

        <asp:DropDownList ID="ddlTeams4" CssClass = 'ddlTeams4' runat="server" AutoPostBack = "True" 
        DataSourceID="Roster10" DataTextField="Χώρα" DataValueField="Χώρα">
        </asp:DropDownList>
        <asp:SqlDataSource ID="Roster10" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ΡόστερConnectionString %>" 
            SelectCommand="SELECT DISTINCT [Χώρα] FROM [Παίκτες] WHERE ([Τρέχουσα_Ομάδα] &lt;&gt; @Τρέχουσα_Ομάδα) ORDER 

BY [Χώρα]">
            <SelectParameters>
                <asp:Parameter DefaultValue="&#39;;Εκτός Βάσης&#39;;" Name="Τρέχουσα_Ομάδα" 
                    Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ΡόστερConnectionString %>" 
        SelectCommand="SELECT DISTINCT [Ομάδα] FROM [Προηγούμενες_Ομάδες] ORDER BY [Ομάδα]">
        </asp:SqlDataSource>

        <asp:Panel ID= "buttonPanel2" CssClass = "butPanel" runat="server" >
        <asp:Button ID="SearchButton2" CssClass = 'searchButton'  runat="server" Text="Search" 
        onclick="SearchButton2_Click" />
            </asp:Panel>
                <asp:Panel ID="Panel9" CssClass = 'selPanel7' runat="server">
                    <asp:Label ID="playersComingFromCountryHTML" CssClass = 'resultsBelow2' runat="server" 

Text=""></asp:Label>
                </asp:Panel>
    </asp:Panel>

     <asp:Panel ID="Panel7" CssClass = 'selPanel6' runat="server">
        
        <asp:Panel ID= "labelPanel3" CssClass = "labPanel3" runat="server" >
        <asp:Label ID="teamSearchHTML3" CssClass = 'teamSearchHTML3' runat="server" Text=""></asp:Label>
        </asp:Panel>

        <asp:DropDownList ID="ddlTeams5" CssClass = 'ddlTeams5' runat="server" AutoPostBack = "True" 
        DataSourceID="Roster11" DataTextField="Χώρα" DataValueField="Χώρα">
        </asp:DropDownList>
        <asp:SqlDataSource ID="Roster11" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ΡόστερConnectionString %>" 
        
            SelectCommand="SELECT DISTINCT [Χώρα] FROM [Προηγούμενες_Ομάδες] ORDER BY [Χώρα]">
        </asp:SqlDataSource>
              
        <asp:Panel ID= "buttonPanel3" CssClass = "butPanel" runat="server" >
         <asp:Button ID="SearchButton3" CssClass = 'searchButton' runat="server" Text="Search" 
        onclick="SearchButton3_Click" /> 
        </asp:Panel>
                <asp:Panel ID="Panel10" CssClass = 'selPanel7' runat="server">
                    <asp:Label ID="playersPlayedInCountryHTML" CssClass = 'resultsBelow2' runat="server" 
Text=""></asp:Label>
                </asp:Panel>
    </asp:Panel>
        
</asp:Content>




<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
   <asp:Panel ID="Panel11" CssClass = "selpanel5" runat="server" Width="680px"> 
   
    <asp:Panel ID="Panel1" CssClass = "selpanel1" runat="server" Width="325px">
        <asp:Label ID="Selection" runat="server" CssClass = "Selection1" Text="<h3> Eπιλογή 1ου Παίκτη </h3>"></asp:Label>
        <asp:Label ID="TeamSelection" runat="server" CssClass = "TeamSelection1" Text="<h6> Ομάδα1 </h6>"></asp:Label>
        <asp:DropDownList ID="ddlTeams" runat="server" DataSourceID="Roster" CssClass = "ddlTeams" 
        DataTextField="Ονομασία" DataValueField="Ονομασία" AutoPostBack = "True" >
        </asp:DropDownList>
        <asp:SqlDataSource ID="Roster" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ΡόστερConnectionString %>" 
        SelectCommand="SELECT [Ονομασία] FROM [Ομάδες] WHERE ([Πρωτάθλημα] &lt;&gt; 'Άλλο') ORDER BY [Ονομασία]" 
        onselecting="Roster_Selecting" > 
        </asp:SqlDataSource>
        <asp:Label ID="PlayerSelection1" runat="server" CssClass = "PlayerSelection" Text="<h6> Παίκτης1 </h6>"></asp:Label>
        <asp:DropDownList ID="ddlPlayers" runat="server" DataSourceID="Roster2" CssClass = "ddlPlayers" 
        DataTextField="Όνομα" DataValueField="Όνομα">
    </asp:DropDownList>
    <asp:SqlDataSource ID="Roster2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ΡόστερConnectionString %>" 
        
        SelectCommand="SELECT [Όνομα] FROM [Παίκτες] WHERE ([Τρέχουσα_Ομάδα] = @Τρέχουσα_Ομάδα) ORDER BY [Όνομα]">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlTeams" Name="Τρέχουσα_Ομάδα" 
                PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    </asp:Panel>
    


    <asp:Panel ID="Panel2" CssClass = "selpanel2" runat="server" Width="325px">
        <asp:Label ID="Selection2" runat="server" CssClass="Selection2" Text="<h3> Eπιλογή 2ου Παίκτη </h3>"></asp:Label>
        <asp:Label ID="TeamSelection2" runat="server" CssClass = "TeamSelection2" Text="<h6> Ομάδα2 </h6>"> </asp:Label>
        <asp:DropDownList ID="ddlTeams2" runat="server" DataSourceID="Roster3" CssClass = "ddlTeams2"
        DataTextField="Ονομασία" DataValueField="Ονομασία" AutoPostBack = "true"
         >
        </asp:DropDownList>
        <asp:SqlDataSource ID="Roster3" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ΡόστερConnectionString %>" 
        onselecting="Roster3_Selecting" 
        SelectCommand="SELECT [Ονομασία] FROM [Ομάδες] WHERE ([Πρωτάθλημα] &lt;&gt; 'Άλλο') ORDER BY [Ονομασία]">
        </asp:SqlDataSource>
        <asp:Label ID="PlayerSelection2" runat="server" CssClass = "PlayerSelection2" Text="<h6> Παίκτης2 </h6>"></asp:Label>
        <asp:DropDownList ID="ddlPlayers2" runat="server" DataSourceID="Roster4" CssClass = "ddlPlayers2"
        DataTextField="Όνομα" DataValueField="Όνομα" AutoPostBack ="true" 
        >
        </asp:DropDownList>
        <asp:SqlDataSource ID="Roster4" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ΡόστερConnectionString %>" 
        SelectCommand="SELECT [Όνομα] FROM [Παίκτες] WHERE ([Τρέχουσα_Ομάδα] = @Τρέχουσα_Ομάδα) ORDER BY [Όνομα]">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlTeams2" Name="Τρέχουσα_Ομάδα" 
                PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
        </asp:SqlDataSource>
     </asp:Panel>
    
     <asp:Panel ID="Panel3" CssClass = "selpanel3" runat="server" Width = "680px">
         <asp:Button ID="compareButton" runat="server" CssClass = "compareButton"  UseSubmitBehavior = "true"   
             Text="Compare" onclick="compareButton_Click" />
         
     </asp:Panel>
     <asp:Panel ID="Panel4" CssClass = "selpanel4" runat="server" Width = "680px">
     <asp:Label ID="comparisonHTML" CssClass = "resultsBelow" runat="server" Text=""></asp:Label>
     </asp:Panel>
       </asp:Panel>
    
</asp:Content>
