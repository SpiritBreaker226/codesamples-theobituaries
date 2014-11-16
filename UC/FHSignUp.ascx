<%@ Control Language="C#" AutoEventWireup="True" CodeFile="FHSignUp.ascx.cs" Inherits="FHSignUp" %>

<%@ Register TagPrefix="uc1" TagName="FHSearch" Src="~/Portals/_default/Skins/Obit/UC/FHSearch.ascx" %>

<div class="divSubmitHeader">
    <label id="gothic1113" class="teal">Funeral Home Name</label>
</div>
<div class="divSignUpIntroSectionText">
</div>
<asp:Panel runat="server" ID="panSearchYourFH">
    <div id="divChooseFuneralHome">
        <asp:Panel runat="server" ID="panChooseFuneralHomeTop" CssClass="divChooseFuneralHomeSearch">
            <label class="teal">Enter the name of a Funeral Home <img src="/Portals/_default/skins/Obit/images/green-search.png" /></label>
            <br /><br />
            <label>If it does not appear in the list below, you can add your Funeral Home to our listing by </label>

            <asp:LinkButton runat="server" ID="lbSearchYourFH" TabIndex="5" OnClick="lbSearchYourFH_Click" CssClass="aFHSignUpCreate" Text="clicking here"></asp:LinkButton>.
        </asp:Panel>
        
        <uc1:FHSearch runat="server" ID="FHSearch" />
    </div>
</asp:Panel>
<asp:Panel runat="server" ID="panAddYouFH" Visible="false">
    <div class="divSubmitHeader divSubmitSubHeader">
        <label id="gothic1117" class="teal">Add Your Funeral Home</label>
    </div>
    <div class="divSignUpIntroSectionText">
        <p>
            <label>If you were unable to find your Funeral Home in our database, please provide your information below. Some fields are mandatory, and indicated with an asterisk (*). <asp:LinkButton runat="server" ID="lbAddYouFH" TabIndex="6" OnClick="lbAddYouFH_Click" CssClass="aFHSignUpCreate" Text="click here to go back to funeral home search"></asp:LinkButton>.</label>
        </p>
    </div>
    <div class="customContainer divSignUpBodyContainer">
        <div class="customLeft divSignUpBodyLeft">
            <div class="customContainer divFieldsContainer">
                <div class="customLeft divFieldsLeft">
                    <label>Funeral Home Name *</label>
                </div>
                <div class="customRight divFieldsRight">
                    <asp:TextBox ID="txtFuneralName" TabIndex="7" runat="server" MaxLength="255"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="FuneralNameRequired" runat="server" Text="Required field" ErrorMessage="Fill in the funeral name" Display="Dynamic" ControlToValidate="txtFuneralName" ValidationGroup="OPC" SetFocusOnError="true" CssClass="divError" Visible="false"></asp:RequiredFieldValidator>
                </div>
                <div class="customFooter divFieldsFooter"></div>
            </div>
        </div>
        <div class="customRight divSignUpBodyRight">
            <div class="customContainer divFieldsContainer">
                <div class="customLeft divFieldsLeft">
                    <label>Funeral Home URL</label>
                </div>
                <div class="customRight divFieldsRight">
                    <asp:TextBox ID="txtFuneralURL" TabIndex="8" runat="server" MaxLength="255"></asp:TextBox>
                    
                    <asp:RegularExpressionValidator runat="server" id="revURL" ControlToValidate="txtFuneralURL" Text="Invalid format" ErrorMessage="Invalid funeral URL format" ValidationExpression="((http(s?):\/\/)|(www\.[^ \[\]\(\)\n\r\t]+)|(([012]?[0-9]{1,2}\.){3}[012]?[0-9]{1,2})\/)([^ \[\]\(\),;&quot;'&lt;&gt;\n\r\t]+)([^\. \[\]\(\),;&quot;'&lt;&gt;\n\r\t])|(([012]?[0-9]{1,2}\.){3}[012]?[0-9]{1,2})" Display="Dynamic" ValidationGroup="OPC" CssClass="divError" Visible="false" />
                </div>
                <div class="customFooter divFieldsFooter"></div>
            </div>
        </div>
        <div class="customFooter divSignUpBodyFooter"></div>                    
    </div>
    <div class="customContainer divSignUpBodyContainer">
        <div class="customLeft divSignUpBodyLeft">
            <div class="customContainer divFieldsContainer">
                <div class="customLeft divFieldsLeft">
                    <label>Business Title *</label>
                </div>
                <div class="customRight divFieldsRight">
                    <asp:TextBox ID="txtFuneralTitle" TabIndex="9" runat="server" MaxLength="50"></asp:TextBox>
                    
                    <asp:RequiredFieldValidator ID="FuneralTitleRequired" runat="server" Text="Required field" ErrorMessage="Fill in the funeral title" Display="Dynamic" ControlToValidate="txtFuneralTitle" ValidationGroup="OPC" SetFocusOnError="true" CssClass="divError" Visible="false"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" id="revTitle" ControlToValidate="txtFuneralTitle" Text="Invalid format" ErrorMessage="Invalid funeral title format" ValidationExpression="^[a-zA-Z]+(([\'\,\.\-\&\ ][a-zA-Z])?[a-zA-Z]*)*$" Display="Dynamic" ValidationGroup="OPC" CssClass="divError" Visible="false" />
                </div>
                <div class="customFooter divFieldsFooter"></div>
            </div>
        </div>
        <div class="customRight divSignUpBodyRight">
            <div class="customContainer divFieldsContainer">
                <div class="customLeft divFieldsLeft">
                    <label>Address 1 *</label>
                </div>
                <div class="customRight divFieldsRight">
                    <asp:TextBox ID="txtFuneralAddress1" TabIndex="10" runat="server" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="FuneralAddress1Required" runat="server" Text="Required field" ErrorMessage="Fill in the funeral address 1" Display="Dynamic" ControlToValidate="txtFuneralAddress1" ValidationGroup="OPC" SetFocusOnError="true" CssClass="divError" Visible="false"></asp:RequiredFieldValidator>
                </div>
                <div class="customFooter divFieldsFooter"></div>
            </div>
        </div>
        <div class="customFooter divSignUpBodyFooter"></div>                    
    </div>
    <div class="customContainer divSignUpBodyContainer">
        <div class="customLeft divSignUpBodyLeft">
            <div class="customContainer divFieldsContainer">
                <div class="customLeft divFieldsLeft">
                    <label>Address 2</label>
                </div>
                <div class="customRight divFieldsRight">
                    <asp:TextBox ID="txtFuneralAddress2" TabIndex="11" runat="server" MaxLength="50"></asp:TextBox>
                </div>
                <div class="customFooter divFieldsFooter"></div>
            </div>
        </div>
        <div class="customRight divSignUpBodyRight">
            <div class="customContainer divFieldsContainer">
                <div class="customLeft divFieldsLeft">
                    <label>City *</label>
                </div>
                <div class="customRight divFieldsRight">
                    <asp:TextBox ID="txtFuneralCity" TabIndex="12" runat="server" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="FuneralCityRequired" runat="server" Text="Required field" ErrorMessage="Fill in the funeral city" Display="Dynamic" ControlToValidate="txtFuneralCity" ValidationGroup="OPC" SetFocusOnError="true" CssClass="divError" Visible="false"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" id="revCity" ControlToValidate="txtFuneralCity" Text="Invalid format" ErrorMessage="Invalid funeral city format" ValidationExpression="^[a-zA-Z]+(([\'\,\.\-\ ][a-zA-Z])?[a-zA-Z]*)*$" Display="Dynamic" ValidationGroup="OPC" CssClass="divError" Visible="false" />
                </div>
                <div class="customFooter divFieldsFooter"></div>
            </div>
        </div>
        <div class="customFooter divSignUpBodyFooter"></div>                    
    </div>
    <asp:UpdatePanel ID="ajaxCountryProvince" runat="server">
        <ContentTemplate>
            <div class="customContainer divSignUpBodyContainer">
                <div class="customLeft divSignUpBodyLeft">
                    <div class="customContainer divFieldsContainer">
                        <div class="customLeft divFieldsLeft">
                            <label><asp:Label runat="server" ID="lblProvince" Text="Province"></asp:Label> *</label>
                        </div>
                		<div class="customRight divFieldsRight">
                            <asp:DropDownList runat="server" ID="ddlFuneralPro" TabIndex="13" DataTextField="ProvinceName" DataValueField="ProvinceID"></asp:DropDownList>
                        </div>
                        <div class="customFooter divFieldsFooter"></div>
                    </div>
                </div>
                <div class="customRight divSignUpBodyRight">
                    <div class="customContainer divFieldsContainer">
                        <div class="customLeft divFieldsLeft">
                            <label>Country *</label>
                        </div>
                		<div class="customRight divFieldsRight">
                            <asp:DropDownList runat="server" TabIndex="14" ID="ddlFuneralCountry" DataTextField="CountryName" DataValueField="CountryID" AutoPostBack="true" OnSelectedIndexChanged="ddlFuneralCountry_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="customFooter divFieldsFooter"></div>
                    </div>
                </div>
                <div class="customFooter divSignUpBodyFooter"></div>                    
            </div>
            <div class="customContainer divFieldsContainer">
                <div class="customLeft divFieldsLeft">
                    <label><asp:Label runat="server" ID="lblPC" Text="Postal"></asp:Label> Code *</label>
                </div>
                <div class="customRight divFieldsRight">
                    <asp:TextBox ID="txtPC" TabIndex="15" runat="server" MaxLength="9"></asp:TextBox>
                    <label class="lblPaymentExample"> ex. </label><asp:Label runat="server" ID="lblPCEx" Text="A1A 1A1"></asp:Label>
                    
                    <asp:RegularExpressionValidator runat="server" id="revPC" ControlToValidate="txtPC" Text="Invalid format" ErrorMessage="Invalid postal code format" ValidationExpression="^[A-Z]\d[A-Z][ ]\d[A-Z]\d$" Display="Dynamic" ValidationGroup="OPC" CssClass="divError" />
                    <asp:RequiredFieldValidator ID="PCRequired" runat="server" Text="Required field" ErrorMessage="Fill in the funeral postal/zip code" Display="Dynamic" ControlToValidate="txtPC" ValidationGroup="OPC" CssClass="divError" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div class="customFooter divFieldsFooter"></div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="customContainer divSignUpBodyContainer">
        <div class="customLeft divSignUpBodyLeft">
            <div class="customContainer divFieldsContainer">
                <div class="customLeft divFieldsLeft">
                    <label>Phone Number *</label>
                </div>
                <div class="customRight divFieldsRight">
                    <asp:TextBox ID="txtFuneralPhone" TabIndex="16" runat="server" MaxLength="15"></asp:TextBox>
                    <label class="lblPaymentExample"> ex. (123) 123-4567</label> 
                    
                    <asp:RegularExpressionValidator runat="server" id="revPhone" ControlToValidate="txtFuneralPhone" Text="Invalid format" ErrorMessage="Invalid funeral phone format ex.(123) 123-4567" ValidationExpression="^\(?\d{3}\)? ?-? ?\d{3} ?-? ?\d{4}$" Display="Dynamic" ValidationGroup="OPC" CssClass="divError" Visible="false"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="FuneralPhoneRequired" runat="server" Text="Required field" ErrorMessage="Fill in the funeral phone" Display="Dynamic" ControlToValidate="txtFuneralPhone" ValidationGroup="OPC" SetFocusOnError="true" CssClass="divError" Visible="false"></asp:RequiredFieldValidator>
                </div>
                <div class="customFooter divFieldsFooter"></div>
            </div>
        </div>
        <div class="customRight divSignUpBodyRight">
            <div class="customContainer divFieldsContainer">
                <div class="customLeft divFieldsLeft">
                    <label>Fax Number</label>
                </div>
                <div class="customRight divFieldsRight">
                    <asp:TextBox ID="txtFuneralFax" TabIndex="17" runat="server" MaxLength="15"></asp:TextBox>
                    
                    <asp:RegularExpressionValidator runat="server" id="revFax" ControlToValidate="txtFuneralFax" Text="Invalid format" ErrorMessage="Invalid funeral fax format ex.(123) 123-4567" ValidationExpression="([\(]?(?<AreaCode>[0-9]{3})[\)]?)?[ \.\-]?(?<Exchange>[0-9]{3})[ \.\-](?<Number>[0-9]{4})" Display="Dynamic" ValidationGroup="OPC" CssClass="divError" Visible="false"></asp:RegularExpressionValidator><label class="lblPaymentExample"> ex. (123) 123-4567</label>
                </div>
                <div class="customFooter divFieldsFooter"></div>
            </div>
        </div>
        <div class="customFooter divSignUpBodyFooter"></div>                    
    </div>
    <div class="customContainer divSignUpBodyContainer">
        <div class="customContainer divFieldsContainer">
            <div class="customLeft divFieldsLeft">
                <label>Comments</label>
            </div>
            <div class="customRight divFieldsRight">
                <asp:TextBox ID="txtFuneralComments" TabIndex="18" runat="server" CssClass="txtComments" TextMode="MultiLine" MaxLength="500" Rows="12"></asp:TextBox>
            </div>
            <div class="customFooter divFieldsFooter"></div>
        </div>
    </div>
</asp:Panel>