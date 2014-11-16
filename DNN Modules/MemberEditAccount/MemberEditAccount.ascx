<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MemberEditAccount.ascx.cs" Inherits="MemberEditAccount" %>

<asp:Panel runat="server" ID="panBody">
	<div class="OB-level2-introtext">
    	<asp:Literal runat="server" ID="litMyAccountIntro" Text="<label>If you are logging on for the first time and want to start creating your Funeral Home page or would like to edit an existing funeral home page, click the <strong>Funeral Home Management</strong> button to get started. Your approved Home(s) will be listed in the selection box. Simply click the one that you wish to work with, and the fields, steps and help will be presented to you.
		<br /><br />
		In two easy steps, you can have all of your pertinent Funeral Home information and images submitted for approval. Once published, you can begin to extend our comprehensive announcements and services to your clients.
		<br /><br />
		If you have already setup and published your Funeral Home page(s) and are now ready to create an <strong>Obituary, Memorial,</strong> or <strong>Pre-Plan Obituary</strong>, click the <strong>Create</strong> button at the top of the page.
		<br /><br />
		If you would like to create a new Obituary, Memorial of Pre-Plan Obituary, click the Create button at the top of page.  Click the <strong>Edit Services</strong> button on the right to edit, delete or publish any of your existing <strong>Obituary, Memorial</strong> or <strong>Pre-Plan</strong> records.</label>"></asp:Literal>
	</div>
</asp:Panel>
<asp:Panel runat="server" ID="panThankYou" CssClass="divSignUpThankYou divSignUpBody" Visible="false">
    <div class="divSubmitHeader">
		<label id="gothic106" class="teal">Thank You</label>
    </div>
    <div class="divSignUpThankYouBody divSignUpBody">
        <label><asp:Literal runat="server" ID="litThankYou" Text="Your Account has been deleted"></asp:Literal></label>
    </div>
</asp:Panel>
<asp:Panel ID="panCoupon" runat="server">
    <div class="OB-level2-introtext">Available or remaining coupons for your account </div>
    <asp:DataGrid id="gdUserList" runat="server"  GridLines="Horizontal" CellPadding="5" CellSpacing="2" AutoGenerateColumns="False" Width="97%" BorderWidth="0" OnItemDataBound="gdUserList_ItemDataBound"> 
        <AlternatingItemStyle BackColor="#e7e7e7"></AlternatingItemStyle>
        <Columns>
            <asp:BoundColumn Visible="false" DataField=""></asp:BoundColumn>
            <asp:BoundColumn Visible="false" DataField=""></asp:BoundColumn>
            <asp:BoundColumn DataField="" HeaderText="FH Name" HeaderStyle-Font-Bold="true"   HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#333333">
                <HeaderStyle Width="35%"></HeaderStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="" HeaderText="FH Address" HeaderStyle-Font-Bold="true"   HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#333333">
                <HeaderStyle Width="25%"></HeaderStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="" HeaderText="City" HeaderStyle-Font-Bold="true"   HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#333333">
                <HeaderStyle Width="15%"></HeaderStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="" HeaderText="# Coupons" HeaderStyle-Font-Bold="true"   HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#333333">
                <HeaderStyle Width="5%"></HeaderStyle>
            </asp:BoundColumn>                                
            <asp:TemplateColumn HeaderText="Expiry Date" HeaderStyle-Font-Bold="true"  HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#333333" HeaderStyle-Width="20%">
                <ItemTemplate>
                    <asp:Label ID="lblCouponExpiryDate" runat="server" Text=""></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>                            
        </Columns>
    </asp:DataGrid>
</asp:Panel>