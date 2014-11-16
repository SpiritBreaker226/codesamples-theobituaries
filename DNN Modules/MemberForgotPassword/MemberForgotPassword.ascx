<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MemberForgotPassword.ascx.cs" Inherits="DesktopModules_MemberForgotPassword_MemberForgotPassword" %>

<asp:Panel runat="server" ID="panThankYou" CssClass="divSignUpThankYou divSignUpBody" Visible="false">	
    <div class="divSignUpThankYouBody divSignUpBody">
        <label><asp:Literal runat="server" ID="litFHThankYou" Text="Please check your email to finalize password reset."></asp:Literal></label>
	</div>
</asp:Panel>
<asp:Panel runat="server" ID="panBody">

<label>Please enter your user login email address and click Submit. A password reset link will be sent to you to complete the process.</label>
<br /><br />
    <div class="divSignUpBody">
        <div id="divError">
            <asp:ValidationSummary ID="vsRegistration" runat="server" CssClass="vsRegistration" ValidationGroup="OPC" DisplayMode="List" HeaderText="" />
                
            <asp:Label runat="server" ID="lblError" Visible="false"></asp:Label>
        </div>    			
        <div class="customContainer divFieldsContainer">
            <div class="customLeft divFieldsLeft">
                <label>Email</label>
            </div>
            <div class="customRight divFieldsRight">
                <asp:TextBox ID="txtEmail" runat="server" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" Text="*" ErrorMessage="Fill in the email" Display="Static" ControlToValidate="txtEmail" ValidationGroup="OPC" SetFocusOnError="true"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="EmailValidator"  runat="server" ControlToValidate="txtEmail" Text="*" ErrorMessage="Invalid email format" ValidationExpression="^[\w-\.]{1,}\@([\da-zA-Z-]{1,}\.){1,}[\da-zA-Z-]{2,6}$" Display="Static" ValidationGroup="OPC" SetFocusOnError="true"/>
            </div>
                <div class="customFooter divFieldsFooter"></div>
        </div>
                
        <div id="divSubmit">
            <div class="customLeft">
                <asp:Button ID="cmdSubmit" class="green-button" runat="server" Text="Submit" OnClick="cmdSubmit_Click" ValidationGroup="OPC"></asp:Button>
            </div>
        </div>
    </div>
</asp:Panel>