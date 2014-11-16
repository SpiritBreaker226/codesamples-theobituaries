<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MemberChangePassword.ascx.cs" Inherits="DesktopModules_MemberChangePassword_MemberChangePassword" %>

<asp:Panel runat="server" ID="panThankYou" CssClass="divSignUpThankYou divSignUpBody" Visible="false">    
    <div class="divSignUpThankYouBody divSignUpBody">
        <label><asp:Literal runat="server" ID="litFHThankYou" Text="Password has been changed successfully. <br /><bR />If you would like to create a new Obituary, Memorial or Pre-Plan Obituary, click the Create button at the top of the page. Click the Edit Services button on the right to edit or delete any of your existing Obituary, Memorial, or Pre-Plan records."></asp:Literal></label>
	</div>
</asp:Panel>
<asp:Panel runat="server" ID="panBody">
    <div class="divSignUpBody">
        <div>Password must be at least 4 characters, no more than 15 characters, and must include at least one upper case letter, one lower case letter, and one numeric digit...</div><br />

        <div id="divError">
            <asp:ValidationSummary ID="vsRegistration" runat="server" CssClass="vsRegistration" ValidationGroup="OPC" DisplayMode="List" HeaderText="" />
                
            <asp:Label runat="server" ID="lblError" Visible="false"></asp:Label>
        </div>		
        <div class="customContainer divFieldsContainer">
            <div class="customLeft divFieldsLeft">
                <label>Current Password *</label>
            </div>
            <div class="customRight divFieldsRight">
                <asp:TextBox ID="txtCurrentPassword" runat="server" MaxLength="50" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCurrentPassword" runat="server" Text="*" ErrorMessage="Fill in the curent password" Display="Static" ControlToValidate="txtCurrentPassword" ValidationGroup="OPC" SetFocusOnError="true"></asp:RequiredFieldValidator>            
            </div>
            <div class="customFooter divFieldsFooter"></div>
        </div>
        <div class="customContainer divFieldsContainer">
            <div class="customLeft divFieldsLeft">
                <label>New Password *</label>
            </div>
            <div class="customRight divFieldsRight">
                <asp:TextBox ID="txtPassword" runat="server" MaxLength="50" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" Text="*" ErrorMessage="Fill in the password" Display="Static" ControlToValidate="txtPassword" ValidationGroup="OPC" SetFocusOnError="true"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revPassword" runat="server" Text="*" ValidationExpression="^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{4,15}$" ErrorMessage="Password must be at least 4 characters, no more than 15 characters, and must include at least one upper case letter, one lower case letter, and one numeric digit.." Display="Dynamic" ControlToValidate="txtPassword" ValidationGroup="OPC" SetFocusOnError="true"></asp:RegularExpressionValidator>
            </div>
            <div class="customFooter divFieldsFooter"></div>
        </div>
        <div class="customContainer divFieldsContainer">
            <div class="customLeft divFieldsLeft">
                <label>Confirm Password</label>
            </div>
            <div class="customRight divFieldsRight">
                <asp:TextBox ID="txtConfirmPassword" runat="server" MaxLength="50" autocomplete="off" TextMode="Password"></asp:TextBox>
                <asp:CompareValidator runat="server" id="CPasswordCompare" ControlToValidate="txtPassword" ControlToCompare="txtConfirmPassword" Text="*" ErrorMessage="Confirm password does not match" Display="Static" ValidationGroup="OPC" />
            </div>
            <div class="customFooter divFieldsFooter"></div>
        </div>
       
        <div id="divSubmit">
			<asp:Button ID="cmdSubmit" class="green-button" runat="server" Text="Submit" OnClick="cmdSubmit_Click" ValidationGroup="OPC"></asp:Button>
        </div>
    </div>
</asp:Panel>