<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SignUp.ascx.cs" Inherits="SignUp" %>

<%@ Register TagPrefix="uc1" TagName="FHSignUp" Src="~/Portals/_default/Skins/Obit/UC/FHSignUp.ascx" %>

<asp:Panel runat="server" ID="panSignUp">
    <div class="customContainer divSignUpHeaderContainer">
        <div class="customLeft divSignUpHeaderLeft">
            <div class="divSignUpHeaderText">
                 <label id="gothic104"><asp:RadioButton runat="server" ID="rdoProfile" GroupName="rblProfileFH" AutoPostBack="true" OnCheckedChanged="rblProfileFH_CheckedChanged" Enabled="true"></asp:RadioButton> Individual</label>
            </div>
        </div>
        <div class="customRight divSignUpHeaderRight">
            <div class="divSignUpHeaderText">
                <label id="gothic105"><asp:RadioButton runat="server" ID="rdoFH" GroupName="rblProfileFH" AutoPostBack="true" OnCheckedChanged="rblProfileFH_CheckedChanged" Checked="true"></asp:RadioButton> Funeral Home</label>
            </div>
        </div>
        <div class="customFooter divSignUpHeaderFooter"></div>
    </div>

    <asp:Panel runat="server" CssClass="divSignUpBody" ID="panSignUpBody">    
        <asp:Panel runat="server" ID="panGeneralUser" CssClass="divSignUpGenUser">
            <div class="divSubmitHeader">
                <label id="gothic1111" class="teal">Your Profile</label>
            </div>
			<div class="divSignUpIntroSectionText">
				<p>
                	<label>An email will be sent to the address provided for confirmation and will include your login credentials.</label>
                </p>
			</div>
            <div class="divError">
                <asp:Label runat="server" ID="lblEmailError" Visible="false"></asp:Label>
            </div>
            <div class="customContainer divSignUpBodyContainer">
                <div class="customLeft divSignUpBodyLeft">
                    <div class="customContainer divFieldsContainer">
                        <div class="customLeft divFieldsLeft">
                            <label>First Name *</label>
                        </div>
                        <div class="customRight divFieldsRight">
                            <asp:TextBox ID="txtFName" runat="server" MaxLength="50" TabIndex="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="FNameRequired" runat="server" Text="Required field" ErrorMessage="Fill in the first name" Display="Static" ControlToValidate="txtFName" ValidationGroup="OPC" CssClass="divError" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator runat="server" id="FNameValidator" ControlToValidate="txtFName" Text="Invalid format" ErrorMessage="Invalid first name format" ValidationExpression="^[a-zA-Z]+(([\'\,\.\-][a-zA-Z])?[a-zA-Z]*)*$" Display="Static" ValidationGroup="OPC" CssClass="divError" />
                        </div>
                         <div class="customFooter divFieldsFooter"></div>
                    </div>
                </div>
                <div class="customRight divSignUpBodyRight">
                    <div class="customContainer divFieldsContainer">
                        <div class="customLeft divFieldsLeft">
                            <label>Last Name *</label>
                        </div>
                        <div class="customRight divFieldsRight">
                            <asp:TextBox ID="txtLName" runat="server" MaxLength="50" TabIndex="2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="LNameRequired" runat="server" Text="Required field" ErrorMessage="Fill in the last name" Display="Static" ControlToValidate="txtLName" ValidationGroup="OPC" CssClass="divError" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator runat="server" id="LNameValidator" ControlToValidate="txtLName" Text="Invalid format" ErrorMessage="Invalid last name format" ValidationExpression="^[a-zA-Z]+(([\'\,\.\-][a-zA-Z])?[a-zA-Z]*)*$" Display="Static" ValidationGroup="OPC" CssClass="divError" />
                        </div>
                        <div class="customFooter divFieldsFooter"></div>
                    </div>
                </div>
                <div class="customFooter divSignUpBodyFooter"></div>
            </div>
                                
            <asp:UpdatePanel ID="ajaxCountryEmail" runat="server">
                <ContentTemplate>
                    <div class="customContainer divSignUpBodyContainer">
                        <div class="customLeft divSignUpBodyLeft">
                            <div class="customContainer divFieldsContainer">
                                <div class="customLeft divFieldsLeft">
                                    <label>Email *</label>
                                </div>
                                <div class="customRight divFieldsRight">
                                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" TabIndex="3" AutoPostBack="true" OnTextChanged="txtEmail_TextChanged"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" Text="Required field" ErrorMessage="Fill in the email" Display="Static" ControlToValidate="txtEmail" ValidationGroup="OPC" CssClass="divError" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" id="EmailValidator" ControlToValidate="txtEmail" Text="Invalid format" ErrorMessage="Invalid email format" ValidationExpression="^[\w-\.]{1,}\@([\da-zA-Z-]{1,}\.){1,}[\da-zA-Z-]{2,6}$" Display="Static" ValidationGroup="OPC" CssClass="divError" />
                                </div>
                                 <div class="customFooter divFieldsFooter"></div>
                            </div>
                        </div>
                        <div class="customRight divSignUpBodyRight">
                            <div class="customContainer divFieldsContainer">
                                <div class="customLeft divFieldsLeft">
                                    <label>Confirm Email *</label>
                                </div>
                                <div class="customRight divFieldsRight">
                                    <asp:TextBox ID="txtCEmail" runat="server" MaxLength="50" TabIndex="4" autocomplete="off"></asp:TextBox>
                                    <asp:CompareValidator runat="server" id="CEmailCompare" ControlToValidate="txtEmail" ControlToCompare="txtCEmail" Text="Confirm email and email do not match" ErrorMessage="Invalid confirm email and email do not match" Display="Static" ValidationGroup="OPC" CssClass="divError" SetFocusOnError="true" />
                                </div>
                                 <div class="customFooter divFieldsFooter"></div>
                            </div>
                        </div>
                        <div class="customFooter divSignUpBodyFooter"></div>
                    </div>
				</ContentTemplate>
            </asp:UpdatePanel>
            <div class="customContainer divFieldsContainer">
                <div class="customLeft divFieldsLeft divNewsletterCheckbox">
                    <asp:CheckBox runat="server" ID="chkNewsletter" Checked="true" TabIndex="5"></asp:CheckBox>
                </div>
                <div class="customRight divFieldsRight">
                    <label>I would like to receive further communication</label>
                </div>
                 <div class="customFooter divFieldsFooter"></div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="panGeneralFH" CssClass="divSignUpGenFH">
            <uc1:FHSignUp runat="server" ID="FHSignUp" />
        </asp:Panel>
        
        <div class="divSubmitHeader">
            <label id="gothic1117"  class="teal">Terms of Use</label>
        </div>
        <div class="divSignUpBody">
            <div>
                <label>Click here to view </label><a href="/termsofuse.aspx" target="_blank">TERMS OF USE</a> <label>&amp;</label> <a href="/CommercialTerms.aspx" target="_blank">COMMERCIAL TERMS</a> <label>(A new browser tab will open to display terms of use.)</label>
            </div>
            <div class="customContainer divSubmitAgreeContainer">
                <div class="customLeft divSubmitAgreeLeft">
                    <asp:CheckBox runat="server" ID="chkAgreeTerms" TabIndex="19"></asp:CheckBox>
                </div>
                <div class="customLeft divSubmitAgreeRight">
                    <label>I agree to the terms of use &amp; commercial terms</label>
                </div>    
                <div class="customLeft divSubmitAgreeLeft">
                </div>
                <div class="customFooter divSubmitAgreeFooter"></div>
            </div>
        </div>
        
        <div class="divSubmitHeader">
            <label id="gothic1114" class="teal">Security Validation</label>
        </div>
        <div class="divSignUpBody">
            <BotDetect:Captcha ID="Captcha" runat="server" />
            <div id="divCaptchaValidation">
                <asp:TextBox ID="txtCaptchaCode" TabIndex="20" runat="server" autocomplete="off"></asp:TextBox>
            </div>
        </div>
        
        <div class="divError">
            <asp:ValidationSummary ID="vsRegistration" runat="server" CssClass="vsRegistration" ValidationGroup="OPC" DisplayMode="List" HeaderText="Sign Up has been filled out incorrectly" />
        
            <asp:Label runat="server" ID="lblError" Visible="false"></asp:Label>
        </div>
        
        <div id="divSubmit">
            <asp:Button ID="cmdSave" TabIndex="21" runat="server" Text="Create Account" OnClick="cmdSave_Click" ValidationGroup="OPC" CssClass="green-button"></asp:Button>
        </div>
    </asp:Panel>
</asp:Panel>
<asp:Panel runat="server" ID="panThankYou" CssClass="divSignUpThankYou divSignUpBody" Visible="false">
	<div class="divSubmitHeader teal">
        <div id="gothic1200">Thank You</div>
    </div>
	<asp:Panel runat="server" ID="panThankYouIndividual" CssClass="divSignUpThankYouBody divSignUpBody" Visible="false">
    	<label>Thank you for registering for an account. Please check your inbox for confirmation and additional details.</label>
    </asp:Panel>
    <asp:Panel runat="server" ID="panThankYouFH" CssClass="divSignUpThankYouBody divSignUpBody" Visible="false"> 
    	<label>Thank you for registering for an account. Please check your inbox for confirmation and additional details.</label>
    </asp:Panel>
</asp:Panel>