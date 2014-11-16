<%@ Control Language="C#" AutoEventWireup="True" CodeFile="FHSearch.ascx.cs" Inherits="FHSearch" %>

<link rel="stylesheet" type="text/css" href="/Portals/_default/Skins/Obit/CSS/ForDreamwaverUsers.css" media="screen" />

<asp:Panel runat="server" ID="panChooseFuneralHomeSearchText" CssClass="divChooseFuneralHomeSearchText">
    <div class="divBasicMessage" id="divSearchMessage"></div>
    <asp:TextBox runat="server" ID="txtSearch" TabIndex="6" CssClass="txtSearch" autocomplete="off" /><%-- onBlur="getDocID('divSearchResults').style.display = '';" divJustHidden--%>
    <div id="divSearchResults" class="divJustHidden"></div>
</asp:Panel>

<asp:Panel runat="server" ID="panChooseFuneralHomeCurrentlySelected" CssClass="divChooseFuneralHomeCurrentlySelected divJustHidden">
	<div class="divError">
        <asp:Label runat="server" ID="lblError" Visible="false"></asp:Label>
    </div>
    <asp:Label runat="server" ID="lblCurrentlySelected"></asp:Label>
    
    <asp:ListBox runat="server" ID="lbChooseFH" SelectionMode="Multiple" Rows="8"></asp:ListBox>
    <asp:HiddenField ID="hfChooseFHItems" runat="server" />
</asp:Panel>

<asp:Panel runat="server" ID="panChooseFuneralHomeBottom" CssClass="divChooseFuneralHomeBottom divChooseFuneralHomeSearch divJustHidden">

	<asp:LinkButton runat="server" ID="lbRemoveSelected" PostBackUrl="javascript:void(0);" CssClass="green-button">Remove Selected Items</asp:LinkButton>
    <asp:LinkButton runat="server" ID="lbRemoveAll" PostBackUrl="javascript:void(0);" CssClass="green-button">Clear All</asp:LinkButton>
   
    <div class="divBelowTextOfFHRemove">
    	<label>If the incorrect <asp:Label runat="server" ID="lblBelowTextOfFHRemove" Text="Funeral Home"></asp:Label> is displayed in the box above, and you wish to remove it; click the name  in the box (now highlighted) and then click the <strong>Remove Selected Items</strong>. If you wish to remove all entries, click <strong>Clear All</strong>.</label>
	</div>
</asp:Panel>