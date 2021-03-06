﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PlayersSearched.aspx.cs" Inherits="Rosteras.PlayersSearched" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <asp:Panel ID= "Panel2" CssClass = "labPanel77" runat="server" >
    <asp:Label ID="searchHeading" runat="server" Text=""></asp:Label>
    </asp:Panel>
</asp:Content>


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
        <asp:Button ID="SearchButton" CssClass = 'searchButton' runat="server" Text="Search" 
        onclick="SearchButton_Click" />
        </asp:Panel>
                <asp:Panel ID="Panel6" CssClass = 'selPanel7' runat="server">
                    <asp:Label ID="playersPassedFromTeamHTML" CssClass = 'resultsBelow2' runat="server" 

Text=""></asp:Label>
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
        <asp:Button ID="SearchButton2" CssClass = 'searchButton' runat="server" Text="Search" 
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="playersSearched" runat="server" Text="Label"></asp:Label>
</asp:Content>

