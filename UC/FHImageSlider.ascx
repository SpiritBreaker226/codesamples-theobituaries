<%@ Control Language="C#" AutoEventWireup="True" CodeFile="FHImageSlider.ascx.cs" Inherits="FHImageSlider" %>

<asp:Literal runat="server" ID="litScript"></asp:Literal>

<asp:HiddenField ID="hfCurrentID" runat="server" Value="0" />
<asp:HiddenField ID="hfCurrentIDDir" runat="server" Value="0" />
<asp:HiddenField ID="hfDir" runat="server" Value="FH" />

<asp:Panel runat="server" ID="panSliderImage">
    <div class="myloader"></div>

    <ul class="bannerscollection_zoominout_list">
        <asp:Literal ID="litSliderImage" runat="server"></asp:Literal>
    </ul>
</asp:Panel>