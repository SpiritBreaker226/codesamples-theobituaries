<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MemberFuneralHomeAdd.ascx.cs" Inherits="MemberFuneralHomeAdd" %>

<%@ Register TagPrefix="uc1" TagName="FHSignUp" Src="~/Portals/_default/Skins/Obit/UC/FHSignUp.ascx" %>

<asp:UpdatePanel ID="ajaxGeneral" runat="server">
	<ContentTemplate>    
    	<asp:Panel runat="server" ID="panSignUp">
            <asp:Panel runat="server" CssClass="divSignUpBody" ID="panSignUpBody">
                <asp:Panel runat="server" ID="panGeneralFH" CssClass="divSignUpGenFH">
	                <uc1:FHSignUp runat="server" ID="FHSignUp" WhatIsTheStatus="-2" UserEmailTemplate="FHAddThankYou" UserEmailSubject="theObituaries.ca Additional Funeral Home administration request." />
                </asp:Panel>
                                
                <div class="divSubmitHeader">
                    <label id="gothic1114"  class="teal lblFuneralHomeHeader">Security Validation</label>
                </div>
                <div class="divSignUpBody">
                    <BotDetect:Captcha ID="Captcha" runat="server" />
                    <div id="divCaptchaValidation">
                        <asp:TextBox ID="txtCaptchaCode" runat="server" autocomplete="off"></asp:TextBox>
                    </div>
                </div>
                
                <div class="divError">
                    <asp:ValidationSummary ID="vsRegistration" runat="server" CssClass="vsRegistration" ValidationGroup="OPC" DisplayMode="List" HeaderText="Sign Up has been filled out incorrectly" />
                
                    <asp:Label runat="server" ID="lblError" Visible="false"></asp:Label>
                </div>
                
                <div id="divSubmit">
                    <asp:Button ID="cmdSave" runat="server" Text="Add Funeral Home" OnClick="cmdSave_Click" ValidationGroup="OPC" CssClass="green-button"></asp:Button>
                </div>
            </asp:Panel>
		</asp:Panel>
        <asp:Panel runat="server" ID="panThankYou" CssClass="divSignUpThankYou divSignUpBody" Visible="false">
        	<div class="divSubmitHeader teal" >
                <label id="gothic1115">Thank You</label>
            </div>

            <asp:Panel runat="server" ID="panThankYouFH" CssClass="divSignUpThankYouBody divSignUpBody"> 
            	<label>Thank you for adding your Funeral Home.  Please check your inbox for confirmation and additional details.</label>
            </asp:Panel>
        </asp:Panel>
	</ContentTemplate>
</asp:UpdatePanel>