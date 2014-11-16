<%@ Control Language="C#" AutoEventWireup="True" CodeFile="FlowerHeader.ascx.cs" Inherits="FlowerHeader" %>

<%@ Register TagPrefix="uc1" TagName="RecipentAnotherAddress" Src="~/UC/RecipentAnotherAddress.ascx" %>

<div class="OB-PageTitle divObitPageTitle customContainer" id="divObiturayFlowersHeaderTitleContainer">
    <div class="customLeft" id="divObiturayFlowersHeaderTitleLeft">
        <label id="gothic100" class="lblObitPageSubTitle">Send <strong>Flowers</strong> </label><label class="lblObitPageSubSmallTitle" id="gothic101">to the Funeral Home, Family or Other Location</label>
    </div>    
    <div class="customRight" id="divObiturayFlowersHeaderTitleRight">
        <asp:HyperLink ID="lnkMyCart" runat="server" Text="My Cart" ImageUrl="/Images/Cartgraphic.png"></asp:HyperLink>
    </div>
    <div class="customFooter" id="divObiturayFlowersHeaderTitleFooter"></div>

</div>

<div class="customContainer" id="divObiturayFlowersContainer">
	<uc1:RecipentAnotherAddress runat="server" ID="raaFlowers" />
 
    <div class="customLeft" id="divObiturayFlowersLeft">
  
        <label id="gothic1112">
        	<asp:HyperLink ID="hlFullName" runat="server" CssClass="lblFullName"></asp:HyperLink>
        </label>
        <br/>
        <label id="gothic1113" class="lblLightGray"><asp:Label runat="server" ID="lblBirthDateAndPassingDate" CssClass="lblBirthDateAndPassingDate"></asp:Label></label>
        <br /><br />
        <asp:HyperLink ID="hlObituaryImage" runat="server" Visible="false"></asp:HyperLink>
        <br /><br />
        
        <label class="lblLightGray">(Click image to return to announcement)</label>
    </div>
    <div class="customMiddle" id="divObiturayFlowersMiddle"></div>
    <div class="customRight" id="divObiturayFlowersRight" >
    	<asp:Panel runat="server" ID="panHeaderFlowersRightContent" CssClass="customContainer divObiturayFlowersRightContentContainer">
        	<div class="customHeader divObiturayFlowersRightContentHeader">
                <label id="gothic1114">
                    <asp:Label ID="lblFuneralFlowersCategory" runat="server" CssClass="lblFullName" Text="Funeral &amp; Sympathy Flowers"></asp:Label>
                </label>
                <div id="divObiturayFlowersCategory">
					<label class="lblFlowersCategory">Please select a category: </label>
	            </div>
            </div>
            <div class="customLeft divObiturayFlowersRightContentLeft">
            	<asp:HyperLink runat="server" ID="hlHeaderCategoryFA">Table Arrangements</asp:HyperLink>
                <asp:HyperLink runat="server" ID="hlHeaderCategoryFB">Baskets</asp:HyperLink>
                <asp:HyperLink runat="server" ID="hlHeaderCategoryFS">Sprays</asp:HyperLink>
                <asp:HyperLink runat="server" ID="hlHeaderCategoryP">Plants</asp:HyperLink>
                <asp:HyperLink runat="server" ID="hlHeaderCategoryFL">Inside Casket</asp:HyperLink>
            </div>
            <div class="customRight divObiturayFlowersRightContentRight">
                <asp:HyperLink runat="server" ID="hlHeaderCategoryFW">Wreaths</asp:HyperLink>
                <asp:HyperLink runat="server" ID="hlHeaderCategoryFH">Hearts</asp:HyperLink>
                <asp:HyperLink runat="server" ID="hlHeaderCategoryFX">Crosses</asp:HyperLink>
                <asp:HyperLink runat="server" ID="hlHeaderCategoryFC">Casket Sprays</asp:HyperLink>
            </div>            
            <div class="customFooter divObiturayFlowersRightContentFooter"></div>
        </asp:Panel>
        <asp:Panel runat="server" ID="panHeaderFlowersRightContentCartHeader" Visible="false">
			<asp:Literal runat="server" ID="litContentCartError"></asp:Literal>
        
            <label id="gothic1114" class="lblFullName"><asp:Label runat="server" ID="lblDeliveryTo" Text="Delivery To:"></asp:Label></label>
            <br/><br/>
            <label id="gothic1116" class="lblFontSize16"><asp:Literal runat="server" ID="litCartHeader"></asp:Literal></label>
			<br/><br/>
            
            <asp:Panel runat="server" ID="panFlowersFH" CssClass="divFlowerRecipient">
                <asp:RadioButton runat="server" ID="rdoFlowersFH" GroupName="FlowersRecipient" Text="Send flowers to the Funeral Home" CssClass="inputRecipient" AutoPostBack="true" OnCheckedChanged="rdoFlowersFH_CheckedChanged"></asp:RadioButton>
            </asp:Panel>
            <asp:Panel runat="server" ID="panFlowersRecipient" CssClass="divFlowerPersonDataList">
                <asp:DataList ID="dlRecipient" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" OnItemDataBound="dlRecipient_ItemDataBound">
                    <ItemTemplate>
                    	<asp:Panel runat="server" ID="panFlowersPerson" CssClass="divFlowerRecipient">
	                        <asp:RadioButton runat="server" ID="rdoFlowersPerson" GroupName="FlowersRecipient" Text='<%# "Send flowers to " + Eval("FirstName") + " " + Eval("LastName") %>' AutoPostBack="true" OnCheckedChanged="rdoFlowersPerson_CheckedChanged" CssClass='<%# Eval("Id") + " inputRecipient" %>'></asp:RadioButton>
						</asp:Panel>
                    </ItemTemplate>
                </asp:DataList>
            </asp:Panel>
			<asp:Panel runat="server" ID="panFlowersAnotherPerson" CssClass="divFlowerRecipient">
	           	<asp:RadioButton runat="server" ID="rdoFlowersAnotherPerson" GroupName="FlowersRecipient" Text="Send flowers to another address" CssClass="inputRecipient"></asp:RadioButton>
			</asp:Panel>
        </asp:Panel> 
    </div>
    <div class="customFooter" id="divObiturayFlowersFooter">
    	<img src="/Images/ob-main-horizontal-divider.png" alt="" />
    </div>
</div>