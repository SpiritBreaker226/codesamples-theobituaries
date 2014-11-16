<%@ Control Language="C#" AutoEventWireup="True" CodeFile="FlowerFooter.ascx.cs" Inherits="FlowerFooter" %>

<div class="divFlowersFooterDivider">
    <img src="/Portals/_default/Skins/Obit/Images/ob-main-horizontal-divider.png" alt="" />
</div>
<div class="customContainer divObiturayFlowersFooterContainer">
    <div class="customLeft divObiturayFlowersFooterLeft">
        <asp:Button runat="server" ID="cmdBackToMain" CssClass="green-button" Text="Back To " OnClick="cmdBackToMain_Click"></asp:Button>
    </div>
    <div class="customRight divObiturayFlowersFooterRight">
		<asp:Label runat="server" ID="lblUSD" CssClass="lblFlowerItem" Text="Floral Orders are in USD"></asp:Label>
    </div>
    <div class="customFooter divObiturayFlowersFooterFooter"></div>
</div>