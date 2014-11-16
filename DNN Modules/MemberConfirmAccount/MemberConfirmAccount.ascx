<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MemberConfirmAccount.ascx.cs" Inherits="MemberConfirmAccount" %>

<asp:UpdatePanel ID="ajaxGeneral" runat="server">
	<ContentTemplate>
    	<asp:Panel runat="server" ID="panLogin" CssClass="divSignUpBody">
            <div class="divSignUpBody">
                <div id="divError">
                    <asp:ValidationSummary ID="vsRegistration" runat="server" CssClass="vsRegistration" ValidationGroup="OPC" DisplayMode="List" HeaderText="Sign Up has been filled out incorrectly" />
                
                    <asp:Label runat="server" ID="lblError" Visible="false"></asp:Label>
                </div>
                <div class="divSubmitHeader">
                    <label id="gothic1117" class="teal">Terms of Use</label>
                </div>
				
                <div class="divSignUpBody">
                    <div>
                        <label>Click here to view </label><a href="/termsofuse.aspx" target="_blank">TERMS OF USE</a> <label>&amp;</label> <a href="/CommercialTerms.aspx" target="_blank">COMMERCIAL TERMS</a> <label>(A new browser tab will open to display terms of use.)</label>
                    </div>
                    <div class="customContainer divSubmitAgreeContainer">
                        <div class="customLeft divSubmitAgreeLeft">
                            <asp:CheckBox runat="server" ID="chkAgreeTerms"></asp:CheckBox>
                        </div>
                        <div class="customLeft divSubmitAgreeRight">
                            <label>I Agree to the terms of use</label>
                        </div>    
                        <div class="customLeft divSubmitAgreeLeft">
                        </div>
                        <div class="customFooter divSubmitAgreeFooter"></div>
                    </div>
                </div>
                
                <div class="divSubmitHeader">
                    <label id="gothic1118" class="teal">Log in to activate your account</label>
                </div>
                <div class="customContainer divFieldsContainer">
                    <div class="customLeft divFieldsLeft">
                        <label>User Name</label>
                    </div>
                    <div class="customRight divFieldsRight">
                        <asp:TextBox ID="txtEmail" runat="server" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="EmailRequired" runat="server" Text="*" ErrorMessage="Fill in the email" Display="Static" ControlToValidate="txtEmail" ValidationGroup="OPC" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" id="EmailValidator" ControlToValidate="txtEmail" Text="*" ErrorMessage="Invalid email format" ValidationExpression="^([1-zA-Z0-1@.\s]{1,255})$" Display="Static" ValidationGroup="OPC" />
                    </div>
                     <div class="customFooter divFieldsFooter"></div>
                </div>
                <div class="customContainer divFieldsContainer">
                    <div class="customLeft divFieldsLeft">
                        <label>Password</label>
                    </div>
                    <div class="customRight divFieldsRight">
                        <asp:TextBox ID="txtPassword" runat="server" MaxLength="50" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" Text="*" ErrorMessage="Fill in the password" Display="Static" ControlToValidate="txtPassword" ValidationGroup="OPC" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                     <div class="customFooter divFieldsFooter"></div>
                </div>
                <div id="divSubmit">
                    <asp:Button ID="cmdLogin" runat="server" class="green-button" Text="Log In" OnClick="cmdLogin_Click" ValidationGroup="OPC"></asp:Button>
                </div>
            </div>
		</asp:Panel>
        <asp:Panel runat="server" ID="panThankYou" CssClass="divSignUpThankYou divSignUpBody" Visible="false">
        	<div class="divSignUpBody">
        		<div class="divSubmitHeader" class="teal">
            	    <label id="gothic106">Thank You</label>
            	</div>
                
	        	<label>Congratulations! Your account is now activated!</label>
                <br/><br/>
                <a href="/MyAccount.aspx">Click here</a> to go to your account.
            </div>
        </asp:Panel>
	</ContentTemplate>
</asp:UpdatePanel>