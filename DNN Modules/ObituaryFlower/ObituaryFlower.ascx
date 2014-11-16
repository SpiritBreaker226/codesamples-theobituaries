<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ObituaryFlower.ascx.cs" Inherits="ObituaryFlower" %>

<%@ Register TagPrefix="uc1" TagName="FlowerHeader" Src="~/Portals/_default/Skins/Obit/UC/FlowerHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FlowerFooter" Src="~/Portals/_default/Skins/Obit/UC/FlowerFooter.ascx" %>

<uc1:FlowerHeader runat="server" ID="FlowerHeader" />

<div class="divError"> 
    <asp:Label runat="server" ID="lblError" Visible="false"></asp:Label>
    <asp:Literal runat="server" ID="litFlowerError"></asp:Literal>
</div>

<div class="customContainer divObiturayFlowersPagingContainer">
    <div class="customLeft divObiturayFlowersPagingLeft">
    	<label id="lblSearchResultText"><asp:Literal runat="server" ID="litSearchResultsText"></asp:Literal></label>
    </div>
    <div class="customRight divObiturayFlowersPagingRight">
    	<asp:Panel ID="PagerPanel" runat="server">
            <div id="divPager">
                <asp:Repeater ID="PagerControls" runat="server" OnItemCommand="PagerControls_ItemCommand">
                    <ItemTemplate>
                        <a class='<%#Eval("TagClass")%>' href='<%#Eval("NavigateUrl")%>'><%#Eval("Text")%></a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </asp:Panel>
    </div>
    <div class="customFooter divObiturayFlowersPagingFooter"></div>
</div>

<asp:PlaceHolder runat="server" ID="phFlowerProducts"></asp:PlaceHolder>

<div class="customContainer divObiturayFlowersPagingContainer">
    <div class="customLeft divObiturayFlowersPagingLeft">
    	<asp:Literal runat="server" ID="litSorting"></asp:Literal>
    </div>
    <div class="customRight divObiturayFlowersPagingRight">
    	<asp:Panel ID="PagerPanel2" runat="server">
            <div id="divPager">
                <asp:Repeater ID="PagerControls2" runat="server" OnItemCommand="PagerControls_ItemCommand">
                    <ItemTemplate>
                        <a class='<%#Eval("TagClass")%>' href='<%#Eval("NavigateUrl")%>'><%#Eval("Text")%></a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </asp:Panel>
    </div>
    <div class="customFooter divObiturayFlowersPagingFooter"></div>
</div>

<uc1:FlowerFooter runat="server" ID="FlowerFooter" />

<asp:HiddenField ID="HiddenPageIndex" runat="server" Value="0" />
<asp:HiddenField ID="hfPersonId" runat="server"></asp:HiddenField>
<asp:HiddenField ID="hfObituatyId" runat="server"></asp:HiddenField>
<asp:HiddenField ID="hfFHPID" runat="server"></asp:HiddenField>