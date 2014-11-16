<%@ Control Language="C#" AutoEventWireup="true" CodeFile="printObituary.ascx.cs" Inherits="printObituary" %>

<asp:Panel runat="server" ID="panMainError" CssClass="divError" Visible="false">
    <asp:Label runat="server" ID="lblMainError"></asp:Label>
</asp:Panel>

<div>
	<br /><br /><br />
    <div class="OB-pageTitle" id="gothicObitPrev1">
        <asp:Label ID="lblName" runat="server"></asp:Label>
    </div>
 	<div id="gothicObitPrev2">
		<asp:Label ID="lblBirthDateAndPassingDate" runat="server"></asp:Label>
	</div>
	<br />
    <asp:Image runat="server" ID="imgObituary" Width="548" Height="291" Visible="false"></asp:Image>
	<br /><br /><br />
    <div><asp:Literal ID="litObituaryDetails" runat="server"></asp:Literal></div>
	<br /><br />
 	<div>&nbsp;</div>
 	<img src="/Portals/_default/Skins/Obit/Images/ob-header-logo.png" alt="theObituaries.ca" />
	<br /><br /><br />
</div>

<script src="/Portals/_default/Skins/Obit/JS/Trade_Gothic_20.font.js" type="text/javascript"></script>

<script type="text/javascript">
	var oldonload=window.onload;//holds any prevs onload function from the js file
	    
	//gets the onload window event checks if there is a function that is already in there
	window.onload=function(){
		window.print();
	}
	
    Cufon.replace('#gothicObitPrev1', { fontWeight: 'normal' });
    Cufon.replace('#gothicObitPrev2', { fontWeight: 'normal' });
    Cufon.replace('#gothicObitPrev3', { fontWeight: 'normal' });
    Cufon.replace('#gothicObitPrev4', { fontWeight: 'normal' });
    Cufon.replace('#gothicObitPrev5', { fontWeight: 'normal' });
    Cufon.replace('#gothicObitPrev6', { fontWeight: 'normal' });
    Cufon.replace('#gothicObitPrev7', { fontWeight: 'normal' });
    Cufon.replace('#gothicObitPrev8', { fontWeight: 'normal' });
    Cufon.replace('#gothicObitPrev9', { fontWeight: 'normal' });
    Cufon.replace('#gothicObitPrev10', { fontWeight: 'normal' });
</script>