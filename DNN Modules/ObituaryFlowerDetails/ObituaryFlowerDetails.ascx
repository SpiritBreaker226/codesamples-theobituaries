<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ObituaryFlowerDetails.ascx.cs" Inherits="ObituaryFlowerDetails" %>

<%@ Register TagPrefix="uc1" TagName="FlowerHeader" Src="~/Portals/_default/Skins/Obit/UC/FlowerHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FlowerFooter" Src="~/Portals/_default/Skins/Obit/UC/FlowerFooter.ascx" %>

<uc1:FlowerHeader runat="server" ID="FlowerHeader" />

<div class="divError"> 
    <asp:Label runat="server" ID="lblError" Visible="false"></asp:Label>
</div>

<div class="customContainer" id="divObiturayFlowersDetailsContainer">
    <div class="customLeft" id="divObiturayFlowersDetailsLeft">
    	<div id="divMainFlowerImage">
	    	<asp:Image runat="server" ID="imgMainFlower"></asp:Image>
		</div>
        <asp:HyperLink runat="server" ID="hlBack" CssClass="green-button" Text="Back To Flower Selections"></asp:HyperLink>
    </div>
    <div class="customRight" id="divObiturayFlowersDetailsRight">
        <label id="gothic1119">
            <strong><asp:Label ID="lblFlowerName" runat="server" CssClass="lblFullName"></asp:Label></strong>
        </label>
        <br /><br />
		<label><strong><asp:Label ID="lblFlowerItem" runat="server" CssClass="lblFlowerItem" Text="Item No. "></asp:Label></strong></label>
        <br />
        <asp:Literal runat="server" ID="litFlowerDetails"></asp:Literal>
        <br /><br />
        <label><asp:Label ID="lblFlowerPrice" runat="server" CssClass="lblFlowerItem"></asp:Label></label>
        <div id="divFlowerDetailsDelivery">
	        <%--<asp:Literal ID="litFlowerDelivery" runat="server" Text="<label class='lblFlowerDelivery'>Same day delivery by a &quot;Preferred Local Florist&quot; is available in the U.S. and Canada on orders placed by 1:00pm in the recipient's time zone Monday through Saturday.
            <br/><br/>
Style of basket may vary.If sending to a funeral home please make sure to include viewing days and times in &quot;special instructions.&quot;</label>"></asp:Literal>--%>&nbsp;
		</div>
        <asp:LinkButton runat="server" ID="lbAddCart" CssClass="green-button" Text="Add to cart" OnClick="lbAddCart_Click"></asp:LinkButton>
        <div id="divFlowerDetailsDeliveryNote">
	        <label>Same day delivery by a "Preferred Local Florist" is available in the U.S. and Canada on orders placed by 1:00pm in the recipient's time zone Monday through Saturday. 
    	    <br/><br/>
			All items featured on this Web site represent the types of arrangements Florist One&reg; offers and may vary depending upon availability in certain regions.</label>
		</div>
    </div>
    <div class="customFooter" id="divObiturayFlowersDetailsFooter"></div>
</div>

<uc1:FlowerFooter runat="server" ID="FlowerFooter" />

<asp:HiddenField ID="hfPersonId" runat="server"></asp:HiddenField>
<asp:HiddenField ID="hfObituatyId" runat="server"></asp:HiddenField>
<asp:HiddenField ID="hfFlowerId" runat="server"></asp:HiddenField>
<asp:HiddenField ID="hfFHPID" runat="server"></asp:HiddenField>