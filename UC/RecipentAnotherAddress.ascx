<%@ Control Language="C#" AutoEventWireup="True" CodeFile="RecipentAnotherAddress.ascx.cs" Inherits="RecipentAnotherAddress" %>

<asp:HiddenField ID="hfObituatyId" runat="server" Value="0" />
   
<div id="divGrayOrderBG" class="divBasicHiddlenBackground"></div>
   
<div id="divAnotherPerson" class="divBasicHidden divHiddenRound">
	<asp:Panel runat="server" ID="panAnotherPersonBody">
        <div class="customLeft divClose divHiddenRoundClose" id="divAnotherPersonClose">
            <a href="javascript:void(0);" class="aClose" onclick="javascript:toggleLayer('divAnotherPerson', 'divGrayOrderBG', '');">X</a>
        </div>
        <div class="customRight divHiddenMapName divHiddenRoundName">
            <label class="lblHiddenRoundName">Add Recipent &amp; Address</label>
        </div>
        <div class="customFooter"></div>
        <div class="divHiddlenBody boardBox">
            <asp:UpdatePanel ID="ajaxRecipent" runat="server">
                <ContentTemplate>
                    <div class="divError"> 
                        <asp:ValidationSummary ID="vsRecipient" runat="server" CssClass="vsRegistration" ValidationGroup="validateRecipient" DisplayMode="List" />
                        
                        <asp:Literal runat="server" ID="litError"></asp:Literal>
                    </div>
                
                    <div>
                        <asp:TextBox ID="txtRecipientFirstName" runat="server" Text="First Name" MaxLength="50" TabIndex="1"></asp:TextBox>
                        <label class="lblError">&nbsp;*</label>
                        <asp:RequiredFieldValidator ID="rfvRecipientFirstName" runat="server" ControlToValidate="txtRecipientFirstName" Display="Dynamic" Text="*" ErrorMessage="Fill in the first name" InitialValue="First Name" ValidationGroup="validateRecipient" CssClass="lblError"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" ID="FirstNameValidator" ControlToValidate="txtRecipientFirstName" Text="*" ErrorMessage="Invalid first name format" ValidationExpression="^[a-zA-Z]+(([\'\,\.\-\ ][a-zA-Z])?[a-zA-Z]*)*$" Display="Dynamic" ValidationGroup="validateRecipient" CssClass="lblError"  /> 
                    </div>
                    <div>
                        <asp:TextBox ID="txtRecipientLastName" runat="server" Text="Last Name" MaxLength="50" TabIndex="2"></asp:TextBox>
                        <label class="lblError">&nbsp;*</label>
                        <asp:RequiredFieldValidator ID="rfvRecipientLastName" runat="server" ControlToValidate="txtRecipientLastName" Display="Dynamic" Text="*" ErrorMessage="Fill in the last name" InitialValue="Last Name" ValidationGroup="validateRecipient" CssClass="lblError"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" ID="LastNameValidator" ControlToValidate="txtRecipientLastName" Text="*" ErrorMessage="Invalid last name format" ValidationExpression="^[a-zA-Z]+(([\'\,\.\-\ ][a-zA-Z])?[a-zA-Z]*)*$" Display="Dynamic" ValidationGroup="validateRecipient" CssClass="lblError"  />
                    </div>
                    <div>
                        <asp:TextBox ID="txtRecipientEmail" runat="server" Text="Email" MaxLength="50" TabIndex="3"></asp:TextBox>
                        <br />
                        <asp:RegularExpressionValidator ID="rfvRecipientEmail" runat="server" Text="Invalid email format" ErrorMessage="Invalid email format" ControlToValidate="txtRecipientEmail" ValidationExpression="(Email)|^[\w-\.]{1,}\@([\da-zA-Z-]{1,}\.){1,}[\da-zA-Z-]{2,6}$"  Display="Dynamic" ValidationGroup="validateRecipient" CssClass="lblError"></asp:RegularExpressionValidator>
                    </div>
                    <div>
                        <asp:TextBox ID="txtRecipientAddress1" runat="server" Text="Address 1" MaxLength="30" TabIndex="4"></asp:TextBox>
                        <label class="lblError">&nbsp;*</label>
                        <asp:RequiredFieldValidator ID="rfvRecipientAddress1" runat="server" ControlToValidate="txtRecipientAddress1" Display="Dynamic" Text="*" ErrorMessage="Fill in the address 1" InitialValue="Address 1" ValidationGroup="validateRecipient" CssClass="lblError"></asp:RequiredFieldValidator>
                    </div>
                    <div>
                        <asp:TextBox ID="txtRecipientAddress2" runat="server" Text="Address 2" MaxLength="30" TabIndex="5"></asp:TextBox>
                    </div>
                    <div>
                        <asp:TextBox ID="txtRecipientCity" runat="server" Text="City Or Town" MaxLength="30" TabIndex="6"></asp:TextBox>
                        <label class="lblError">&nbsp;*</label>
                        <asp:RequiredFieldValidator ID="rfvRecipientCity" runat="server" ControlToValidate="txtRecipientCity" Display="Dynamic" Text="*" ErrorMessage="Fill in the city" InitialValue="City Or Town" ValidationGroup="validateRecipient" CssClass="lblError"></asp:RequiredFieldValidator>                       
                    </div>
                    <asp:UpdatePanel ID="ajaxRecipientCountryProvince" runat="server">
                        <ContentTemplate>
                            <div>
                                <asp:DropDownList runat="server" ID="ddlRecipientProvince" DataTextField="ProvinceName" DataValueField="ProvinceID" TabIndex="7"></asp:DropDownList>
                            </div>
                            <div>
                                <asp:DropDownList runat="server" ID="ddlRecipientCountry" DataTextField="CountryName" DataValueField="CountryID" AutoPostBack="true" OnSelectedIndexChanged="ddlRecipientCountry_SelectedIndexChanged" TabIndex="8"></asp:DropDownList>
                            </div>
                            <div>
                                <asp:TextBox ID="txtRecipientPostalCode" runat="server" Text="Postal Code" Width="100" MaxLength="7" TabIndex="9"></asp:TextBox>
                                <label> ex. <asp:Label runat="server" ID="lblPCEx" Text="A1A 1A1"></asp:Label> *</label>
                                <asp:RequiredFieldValidator ID="rfvRecipientPostalCode" runat="server" ControlToValidate="txtRecipientPostalCode" Display="Dynamic" Text="*" ErrorMessage="Fill in the postal code" InitialValue="Postal Code" ValidationGroup="validateRecipient" CssClass="lblError"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" id="revPC" ControlToValidate="txtRecipientPostalCode" Text="*" ErrorMessage="Invalid postal code format" ValidationExpression="^[A-Z]\d[A-Z][ ]\d[A-Z]\d$" Display="Dynamic" ValidationGroup="validateRecipient" CssClass="lblError" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                    <div>
                        <asp:TextBox ID="txtRecipientPhoneNo" runat="server" Text="Phone" MaxLength="14" TabIndex="10" Width="100"></asp:TextBox>
                        <label> <asp:Label runat="server" ID="lblPhoneRequiredStar" Text="*"></asp:Label></label><label> ex. (123) 456-7890</label>
                        <asp:RequiredFieldValidator ID="rfvRecipientPhoneNo" runat="server" ControlToValidate="txtRecipientPhoneNo" Display="Dynamic" Text="*" ErrorMessage="Fill in the phone" InitialValue="Phone" ValidationGroup="validateRecipient" CssClass="lblError"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" ID="PhoneNoValidator" ControlToValidate="txtRecipientPhoneNo" ErrorMessage="Invalid phone format" ValidationExpression="^\(?\d{3}\)? ?-? ?\d{3} ?-? ?\d{4}$" Text="*" Display="Dynamic" ValidationGroup="validateRecipient" CssClass="lblError"></asp:RegularExpressionValidator>
                    </div>
                                        
                    <div>
                        <asp:Button ID="cmdSaveRecipient" runat="server" Text="Save" OnClick="cmdSaveRecipient_Click" class="green-button" ValidationGroup="validateRecipient" TabIndex="11"></asp:Button>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
	</asp:Panel>
</div><!-- end of Hidden Div -->