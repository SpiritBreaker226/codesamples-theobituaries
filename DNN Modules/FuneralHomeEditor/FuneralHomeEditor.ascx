<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FuneralHomeEditor.ascx.cs" Inherits="DesktopModules_FuneralHomeEditor_FuneralHomeEditor" %>

<%@ Register TagPrefix="uc1" TagName="CropImage" Src="~/Portals/_default/Skins/Obit/UC/CropImages.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FHSearch" Src="~/Portals/_default/Skins/Obit/UC/FHSearch.ascx" %>

<script src="/Portals/_default/Skins/Obit/js/tinymce/js/tinymce/tinymce.min.js"></script>
<script type="text/javascript">
    tinymce.init({
        selector: ".tinymce",
        theme: "modern",
        menubar: false,
        resize: false,
        statusbar: false,
        plugins: ["advlist autolink lists link charmap preview hr anchor",
            "code nonbreaking table contextmenu directionality paste"],
        toolbar1: "code preview",
		toolbar2: "bold italic underline | undo redo | link | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent"
    });
</script>

<link rel="stylesheet" type="text/css" href="/Portals/_default/Skins/Obit/CSS/ForDreamwaverUsers.css" media="screen" />
<link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="stylesheet" type="text/css"/>

<asp:Panel runat="server" ID="panMainError" CssClass="divError" Visible="false">            
    <asp:Label runat="server" ID="lblMainError"></asp:Label>
</asp:Panel>

<asp:Panel runat="server" ID="panThankYou" CssClass="divSignUpThankYou divSignUpBody" Visible="false">
    <div class="divSubmitHeader" id="divFuneralHomePhotos">
        <label id="gothic1111" class="teal lblFontSize28">Funeral Home Photos</label>
    </div>
    <div class="OB-level2-introtext">
    	<label><asp:Literal runat="server" ID="lblStep2Intro" Text="We recommend selecting good quality JPG images that do not exceed 3MB in file size or 2048 pixels in width. You may change the sequence of your photos by clicking and dragging the image to their new location. The image in the first position will be your <strong>PRIMARY IMAGE</strong>."></asp:Literal></label>
	</div>
    <div class="divSubmitHeader">
        <label id="gothic1116" class="teal">Please upload images that best represent your funeral home.</label>
    </div>
    
    <uc1:CropImage runat="server" ID="ciFH" />
    
    <div class="OB-level2-introtext">
    	<br/>
    	<label><asp:Literal runat="server" ID="lblStep2Instruction" Text="If you are happy with your selection and sequence of images, click the <strong>Preview</strong> button to see your creation. You can come back to this section to edit again before final submission. You can also come back anytime after publishing to edit and enhance your content and images - and as often as you wish.
        <br /><br />
        If you are pleased with the final look of your page, click the <strong>Submit For Review</strong> button at the top of the Preview page.
        <br /><br />
        Once you begin adding Funeral Services (Obituary announcements), the deceased name and the link to their announcements will appear on your page below your Google Map."></asp:Literal></label>
	</div>
    
    <div id="divSubmit">
        <asp:Button ID="cmdPreview" runat="server" OnClick="cmdPreview_Click" ValidationGroup="OPC" CssClass="green-button" Text="Preview"></asp:Button>
    </div>
</asp:Panel>
<asp:Panel runat="server" ID="panSignUp">

	<div class="OB-level2-introtext">
		<label><asp:Literal runat="server" ID="lblStep1Instruction" Text="Simply click the listed home that you wish to work with, and the fields, steps and help will be presented - much of the information has already been populated for you.
        <br /><br />
        In two easy steps, you can have all of your pertinent Funeral Home information and images submitted for approval. Once published, you can begin to extend our comprehensive services to your clients.
        <br /><br />
        Step 1: Provide Funeral Home details and logo
        <br />
        Step 2: Upload images and submit for approval"></asp:Literal></label>
	</div>
	<div class="divChooseFuneralHomeCurrentlySelected">
        <div>
			<br />
			<label class="lblFontSize14">Select the Funeral Home you wish to edit:</label>
        </div>
        <div>
            <asp:ListBox runat="server" ID="lbChooseUserFH" SelectionMode="Multiple" Rows="8" AutoPostBack="true" OnSelectedIndexChanged="lbChooseUserFH_SelectedIndexChanged"></asp:ListBox>
        </div>
	</div>
        
    <asp:Panel runat="server" ID="panEdit" Visible="false">
        <asp:Panel runat="server" ID="panSiteAdmin" Visible="false">
        	<asp:UpdatePanel ID="ajaxSiteAdministrator" runat="server">
	            <ContentTemplate>
                    <div class="divSubmitHeader">
                        <label id="gothic1111" class="teal lblFuneralHomeHeader">Site Administrator</label>
                    </div>
                    <div class="divFHSectionDescription">
						<label>If you would like to authorize an additional person to have access to your 'My Account' area and associated management capabilities, please click the Add Site Administrator link, provide their information and click the Save button.</label>   
                    </div>
                    
                    <div class="divError">            
                        <asp:Label runat="server" ID="lblFHError" Visible="false"></asp:Label>
                    </div>
                    
                    <asp:DataGrid id="gdSiteAdmin" runat="server" Width="640px" GridLines="None" CellPadding="3" AutoGenerateColumns="False" OnItemCommand="gdSiteAdmin_ItemCommand" CssClass="divFHImageMapDisplayContainer">
                        <Columns>            
                            <asp:BoundColumn DataField="" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="" ItemStyle-Width="15%" HeaderText="First Name"></asp:BoundColumn>
                            <asp:BoundColumn DataField="" ItemStyle-Width="15%" HeaderText="Last Name"></asp:BoundColumn>
                            <asp:BoundColumn DataField="" ItemStyle-Width="24%" HeaderText="Email"></asp:BoundColumn>
                            <asp:BoundColumn DataField="" ItemStyle-Width="9%" HeaderText="Is Active"></asp:BoundColumn>
                            <asp:TemplateColumn ItemStyle-Width="31%">
                                <itemtemplate>
                                    <asp:Button runat="server" ID="cmdDelete" Text="Delete" CommandName="Delete" OnClientClick="javascript: return confirm('Do you what to remove this user from accessing this funeral home?');"></asp:Button>
                                    
                                    <asp:Button runat="server" ID="cmdMake" Text="Make User Main" CommandName="Make" OnClientClick="javascript: return confirm('Warning! only one user can be main site administrator, processing will result in you no longer being main site administrator. Do you want to continue ?');"></asp:Button>
                                </itemtemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                                        
                    <div id="divAddSiteAdmin">
                    	<asp:LinkButton runat="server" ID="lbAddSiteAdmin" CssClass="aDarkBlueButton" onClick="lbAddSiteAdmin_Click" Text="Add site administrator"></asp:LinkButton>
                    </div>
                    
                    <asp:Panel runat="server" ID="panAdditionalSiteAdministrator" Visible="false">
                        <asp:Panel runat="server" ID="panSearchYourUser" CssClass="divChooseSiteAdminSearch" Visible="false">
                        	<asp:Panel runat="server" ID="panChooseUser" CssClass="divChooseFuneralHomeSearch divChooseSiteAdminSearchIntro">
                                <label id="gothic1118" class="teal">Enter the name of a user.</label> <label class="lblSmallGaryText">If it does not appear in the list below, you can add your Site Administrator to our listing by <asp:LinkButton runat="server" ID="lbSearchYourUser" OnClick="lbSearchYourUser_Click" Text="clicking here"></asp:LinkButton>.</label>
                            </asp:Panel>
                            
                       		<uc1:FHSearch runat="server" ID="fhsUser" AjaxFile="/ASP/SearchUser.aspx" TypeName="Users" />
						</asp:Panel>
						<asp:Panel runat="server" ID="panAddYouUser" Visible="true">
                        	<div class="divSiteAdminSignUpIntroText">
                                <p>
                                    <label>Please provide your information below. Some fields are mandatory, and indicated with an asterisk (*).</label>
                                </p>
                            </div>
                            <div class="customContainer divFieldsContainer">
                                <div class="customLeft divFieldsLeft">
                                    <label>First Name *</label>
                                </div>
                                <div class="customRight divFieldsRight">
                                    <asp:TextBox ID="txtSiteAdminFName" runat="server" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="FNameRequired" runat="server" CssClass="divError" Text="Required field" ErrorMessage="Fill in the first name" Display="Dynamic" ControlToValidate="txtSiteAdminFName" ValidationGroup="OPC1" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" id="revSiteAdminFName" ControlToValidate="txtSiteAdminFName" CssClass="divError" Text="Invalid format" ErrorMessage="Invalid first name format" ValidationExpression="^[a-zA-Z]+(([\'\,\.\-][a-zA-Z])?[a-zA-Z]*)*$" Display="Dynamic" ValidationGroup="OPC1"></asp:RegularExpressionValidator>
                                </div>
                                 <div class="customFooter divFieldsFooter"></div>
                            </div>
            
                            <div class="customContainer divFieldsContainer">
                                <div class="customLeft divFieldsLeft">
                                    <label>Last Name *</label>
                                </div>
                                <div class="customRight divFieldsRight">
                                    <asp:TextBox ID="txtSiteAdminLName" runat="server" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="LNameRequired" runat="server" CssClass="divError" Text="Required field" ErrorMessage="Fill in the last name" Display="Dynamic" ControlToValidate="txtSiteAdminLName" ValidationGroup="OPC1" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" id="revSiteAdminLName" ControlToValidate="txtSiteAdminLName" CssClass="divError" Text="Invalid format" ErrorMessage="Invalid last name format" ValidationExpression="^[a-zA-Z]+(([\'\,\.\-][a-zA-Z])?[a-zA-Z]*)*$" Display="Dynamic" ValidationGroup="OPC1"></asp:RegularExpressionValidator>
                                </div>
                                <div class="customFooter divFieldsFooter"></div>
                            </div>
                            
                            <div class="customContainer divFieldsContainer">
                                <div class="customLeft divFieldsLeft">
                                    <label>Email *</label>
                                </div>
                                <div class="customRight divFieldsRight">
                                    <asp:TextBox ID="txtSiteAdminEmail" runat="server" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" CssClass="divError" Text="Required field" ErrorMessage="Fill in the email" Display="Dynamic" ControlToValidate="txtSiteAdminEmail" ValidationGroup="OPC1" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" id="revSiteAdminEmail" ControlToValidate="txtSiteAdminEmail" CssClass="divError" Text="Invalid format" ErrorMessage="Invalid email format" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" Display="Dynamic" ValidationGroup="OPC1"></asp:RegularExpressionValidator>
                                </div>
                                 <div class="customFooter divFieldsFooter"></div>
                            </div>
                            <div class="customContainer divFieldsContainer">
                                <div class="customLeft divFieldsLeft">
                                    <label>Confirm Email *</label>
                                </div>
                                <div class="customRight divFieldsRight">
                                    <asp:TextBox ID="txtSiteAdminCEmail" runat="server" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    <asp:CompareValidator runat="server" id="CEmailCompare" ControlToValidate="txtSiteAdminEmail" ControlToCompare="txtSiteAdminCEmail" CssClass="divError" Text="Does not match" ErrorMessage="Invalid confirm email and email do not match" Display="Dynamic" ValidationGroup="OPC1"></asp:CompareValidator>
                                </div>
                                 <div class="customFooter divFieldsFooter"></div>
                            </div>
						</asp:Panel>
                         
                        <div class="divError">
                             <asp:ValidationSummary ID="vsSiteAdministrator" runat="server" CssClass="vsRegistration" ValidationGroup="OPC1" DisplayMode="List" HeaderText="Additional site administrator has been filled out incorrectly"></asp:ValidationSummary>
                        </div>
                         
                        <div class="customContainer divFieldsContainer">
                            <div class="customLeft divFieldsLeft">
                                <asp:Button ID="btnSaveAdditionalSiteAdmin" runat="server" class="green-button" Text="Save" OnClick="btnSaveAdditionalSiteAdmin_Click" ValidationGroup="OPC1"></asp:Button>
                            </div>
                            <div class="customRight divFieldsRight">
                                &nbsp;
                            </div>
                             <div class="customFooter divFieldsFooter"></div>
                        </div>
                    </asp:Panel>
				</ContentTemplate>
	        </asp:UpdatePanel>
    	</asp:Panel>
        
        <div class="divSubmitHeader">
    	    <label id="gothic1112" class="teal lblFuneralHomeHeader">General Information</label>
	    </div>
        <div class="divFHSectionDescription">
		    <label>To create or edit your Funeral Home page, please review pre-populated information and complete or edit as required.</label>
		</div>
    
        <div class="customContainer divFieldsContainer">
            <div class="customLeft divFieldsLeft">
                <label>Name *</label>
            </div>
            <div class="customRight divFieldsRight">
                <asp:TextBox ID="txtFuneralName" runat="server" MaxLength="255"></asp:TextBox>
                
                <asp:RequiredFieldValidator ID="FuneralNameRequired" runat="server" CssClass="divError" Text="Required field" ErrorMessage="Fill in the funeral name" Display="Dynamic" ControlToValidate="txtFuneralName" ValidationGroup="OPC" SetFocusOnError="true"></asp:RequiredFieldValidator>
                <%--<asp:RegularExpressionValidator runat="server" id="FuneralNameValidator" ControlToValidate="txtFuneralName" CssClass="divError" Text="Invalid format" ErrorMessage="Invalid funeral name format" ValidationExpression="^[a-zA-Z]+(([\'\,\.\-\&\ ][a-zA-Z])?[a-zA-Z]*)*$" Display="Dynamic" ValidationGroup="OPC"></asp:RegularExpressionValidator>--%>
            </div>
            <div class="customFooter divFieldsFooter"></div>
        </div>
        <!--<div class="customContainer divFieldsContainer">
            <div class="customLeft divFieldsLeft divNewsletterCheckbox">
                <asp:CheckBox runat="server" ID="chkFHNew"></asp:CheckBox>
            </div>
            <div class="customRight divFieldsRight">
                <label>This is a new funeral home</label>
            </div>
             <div class="customFooter divFieldsFooter"></div>
        </div>-->
        <div class="customContainer divFieldsContainer">
            <div class="customLeft divFieldsLeft">
                <label>Upload Logo</label>
            </div>
            <div class="customRight divFieldsRight">
                <asp:FileUpload ID="fuLogo" runat="server"></asp:FileUpload>
            </div>
            <div class="customFooter divFieldsFooter"></div>
        </div>
        <asp:Panel runat="server" ID="panCurrentLogo" Visible="false">
            <div class="customContainer divFieldsContainer">
                <div class="customLeft divFieldsLeft">
                    <label>Current Image</label>
                </div>
                <div class="customNavigation divFieldsRight">
                    <asp:Image runat="server" ID="imgLogo" Width="360"></asp:Image>
                    <asp:Label runat="server" ID="lblImageSource" Visible="False"></asp:Label>
                </div>
                <div class="customFooter divFieldsFooter"></div>
            </div>
        </asp:Panel>
        <div class="customContainer divFieldsContainer">
            <div class="customLeft divFieldsLeft">
                <label>Tag Line</label>
            </div>
            <div class="customRight divFieldsRight">
                <asp:TextBox ID="txtTagLine" runat="server" MaxLength="75"></asp:TextBox>
                <label> Max 75 characters</label>             
            </div>
            <div class="customFooter divFieldsFooter"></div>
        </div>    
        <div class="customContainer divFieldsContainer">
            <div class="customLeft divFieldsLeft">
                <label>URL</label>
            </div>
            <div class="customRight divFieldsRight">
                <asp:TextBox ID="txtFuneralURL" runat="server" MaxLength="255"></asp:TextBox>
                <label class="lblPaymentExample"> ex. http://www.yourfuneralhome.ca</label>
                
                <asp:RegularExpressionValidator runat="server" id="revURL" ControlToValidate="txtFuneralURL" CssClass="divError" Text="Invalid format" ErrorMessage="Invalid funeral URL format" ValidationExpression="((http(s?):\/\/)|(www\.[^ \[\]\(\)\n\r\t]+)|(([012]?[0-9]{1,2}\.){3}[012]?[0-9]{1,2})\/)([^ \[\]\(\),;&quot;'&lt;&gt;\n\r\t]+)([^\. \[\]\(\),;&quot;'&lt;&gt;\n\r\t])|(([012]?[0-9]{1,2}\.){3}[012]?[0-9]{1,2})" Display="Dynamic" ValidationGroup="OPC"></asp:RegularExpressionValidator>
            </div>
            <div class="customFooter divFieldsFooter"></div>
        </div>

        <div class="customContainer divFieldsContainer">
            <div class="customLeft divFieldsLeft">
                <label>Address 1 *</label>
            </div>
            <div class="customRight divFieldsRight">
                <asp:TextBox ID="txtFuneralAddress1" runat="server" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="FuneralAddress1Required" runat="server" CssClass="divError" Text="Required field" ErrorMessage="Fill in the funeral address 1" Display="Dynamic" ControlToValidate="txtFuneralAddress1" ValidationGroup="OPC" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </div>
            <div class="customFooter divFieldsFooter"></div>
        </div>
        <div class="customContainer divFieldsContainer">
            <div class="customLeft divFieldsLeft">
                <label>Address 2</label>
            </div>
            <div class="customRight divFieldsRight divFieldsFH">
                <asp:TextBox ID="txtFuneralAddress2" runat="server" MaxLength="50"></asp:TextBox>
            </div>
            <div class="customFooter divFieldsFooter"></div>
        </div>
        <div class="customContainer divFieldsContainer">
            <div class="customLeft divFieldsLeft">
                <label>City *</label>
            </div>
            <div class="customRight divFieldsRight">
                <asp:TextBox ID="txtFuneralCity" runat="server" MaxLength="50"></asp:TextBox>
                
                <asp:RequiredFieldValidator ID="FuneralCityRequired" runat="server" CssClass="divError" Text="Required field" ErrorMessage="Fill in the funeral city" Display="Dynamic" ControlToValidate="txtFuneralCity" ValidationGroup="OPC" SetFocusOnError="true"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator runat="server" id="revCity" ControlToValidate="txtFuneralCity" CssClass="divError" Text="Invalid format" ErrorMessage="Invalid funeral city format" ValidationExpression="^[a-zA-Z]+(([\'\,\.\-\ ][a-zA-Z])?[a-zA-Z]*)*$" Display="Dynamic" ValidationGroup="OPC"></asp:RegularExpressionValidator>
            </div>
            <div class="customFooter divFieldsFooter"></div>
        </div>

        <asp:UpdatePanel ID="ajaxCountryProvince" runat="server">
            <ContentTemplate>
                <div class="customContainer divFieldsContainer">
                    <div class="customLeft divFieldsLeft">
                        <label><asp:Label runat="server" ID="lblProvince" Text="Province"></asp:Label> *</label>
                    </div>
                    <div class="customRight divFieldsRight divFieldsFH">
                        <asp:DropDownList runat="server" ID="ddlFuneralPro" runat="server" DataTextField="ProvinceName" DataValueField="ProvinceID"></asp:DropDownList>
                    </div>
                    <div class="customFooter divFieldsFooter"></div>
                </div>
                <div class="customContainer divFieldsContainer">
                    <div class="customLeft divFieldsLeft">
                        <label>Country *</label>
                    </div>
                    <div class="customRight divFieldsRight divFieldsFH">
                        <asp:DropDownList runat="server" ID="ddlFuneralCountry" runat="server" DataTextField="CountryName" DataValueField="CountryID" AutoPostBack="true" OnSelectedIndexChanged="ddlFuneralCountry_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="customFooter divFieldsFooter"></div>
                </div>
                <div class="customContainer divFieldsContainer">
                    <div class="customLeft divFieldsLeft">
                        <label><asp:Label runat="server" ID="lblPC" Text="Postal"></asp:Label> Code *</label>
                    </div>
                    <div class="customRight divFieldsRight">
                        <asp:TextBox ID="txtPC" runat="server" MaxLength="9"></asp:TextBox>
                        <label class="lblPaymentExample"> ex. <asp:Label runat="server" ID="lblPCEx" Text="A1A 1A1"></asp:Label></label>
                        
                        <asp:RegularExpressionValidator runat="server" id="revPC" ControlToValidate="txtPC" CssClass="divError" Text="Invalid format" ErrorMessage="Invalid postal code format" ValidationExpression="^[A-Z]\d[A-Z][ ]\d[A-Z]\d$" Display="Dynamic" ValidationGroup="OPC"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="PCRequired" runat="server" CssClass="divError" Text="Required field" ErrorMessage="Fill in the postal/zip code" Display="Dynamic" ControlToValidate="txtPC" ValidationGroup="OPC" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                    <div class="customFooter divFieldsFooter"></div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
                
        <div class="customContainer divFieldsContainer">
            <div class="customLeft divFieldsLeft">
                <label>Phone Number *</label>
            </div>
            <div class="customRight divFieldsRight">
                <asp:TextBox ID="txtFuneralPhone" runat="server" MaxLength="14"></asp:TextBox>
                <label class="lblPaymentExample"> ex. (123) 456-7890</label>
                
                <asp:RegularExpressionValidator runat="server" id="revPhone" ControlToValidate="txtFuneralPhone" CssClass="divError" Text="Invalid format" ErrorMessage="Invalid phone format ex.(123) 123-4567" ValidationExpression="^\(?\d{3}\)? ?-? ?\d{3} ?-? ?\d{4}$" Display="Dynamic" ValidationGroup="OPC"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="FuneralPhoneRequired" runat="server" CssClass="divError" Text="Required field" ErrorMessage="Fill in the funeral phone" Display="Dynamic" ControlToValidate="txtFuneralPhone" ValidationGroup="OPC" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </div>
            <div class="customFooter divFieldsFooter"></div>
        </div>
        
        <div class="customContainer divFieldsContainer">
            <div class="customLeft divFieldsLeft">
                <label>Fax Number</label>
            </div>
            <div class="customRight divFieldsRight">
                <asp:TextBox ID="txtFuneralFax" runat="server" MaxLength="14"></asp:TextBox>
                <label> ex. (123) 456-7890</label>
                
                <asp:RegularExpressionValidator runat="server" id="revFax" ControlToValidate="txtFuneralFax" CssClass="divError" Text="Invalid format" ErrorMessage="Invalid fax format ex.(123) 123-4567" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" Display="Dynamic" ValidationGroup="OPC"></asp:RegularExpressionValidator>
            </div>
            <div class="customFooter divFieldsFooter"></div>
        </div>
        <div class="customContainer divFieldsContainer">
            <div class="customLeft divFieldsLeft">
                <label>About Us *</label>
            </div>
            <div class="customRight divFieldsRight">
                <label>Feel free to add as much detail regarding your facility, services, staff and the community you serve.</label>
                 
                <asp:RequiredFieldValidator ID="rfvAboutUs" runat="server" CssClass="divError" Text="Required field" ErrorMessage="Fill in the about us" Display="Dynamic" ControlToValidate="txtAboutUs" ValidationGroup="OPC" SetFocusOnError="true"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtAboutUs" runat="server" ClientIDMode="Static" TextMode="MultiLine" Width="100%" Height="300px" CssClass="tinymce" />
                
                <%--<dnn:TextEditor ID="txtAboutUs" ChooseRender="false" ChooseMode="false" DefaultMode="Basic" HtmlEncode="true" Width="100%" Height="300px" runat="server" ToolbarMode="Default"></dnn:TextEditor>--%>
                
                
            	<%-- ToolbarMode="Default"<CKEditor:CKEditorControl ID="txtAboutUs" runat="server" Toolbar="Cut|Copy|Paste|PasteText|PasteFromWord|-|Undo|Redo|
                Find|Replace|-|SpellChecker|Scayt
				/
				Bold|Italic|Underline|Strike|Subscript|Superscript'"></CKEditor:CKEditorControl>--%>
            </div>
            <div class="customFooter divFieldsFooter"></div>
        </div>
        <div class="customContainer divFieldsContainer">
            <div class="customLeft divFieldsLeft">
                <label>Offerings *</label>
            </div>
			<asp:UpdatePanel ID="ajaxOfferings" runat="server">
            	<ContentTemplate>
                    <asp:Panel runat="server" ID="panOfferings" CssClass="customRight divFieldsRight">
                        <asp:CheckBoxList ID="chkLstOfferingText" runat="server" CssClass="checkboxlist_nowrap tblOffering" CellSpacing="20" DataTextField="OfferingsName" DataValueField="OfferingsID"></asp:CheckBoxList>
                        <br/>
                        <asp:PlaceHolder runat="server" ID="phOtherOfferings"></asp:PlaceHolder>
                        
                        <div id="divOtherOfferings">
                            <asp:LinkButton ID="lbAddOtherOfferings" runat="server" OnClick="lbAddOtherOfferings_Click" Text="Add additional offerings not listed above"></asp:LinkButton>
                        </div>
                        <asp:HiddenField ID="hfOtherOfferings" runat="server" Value="0" />
                        <asp:HiddenField ID="hfOtherOfferingsValue" runat="server" Value="" />
                    </asp:Panel>
	            </ContentTemplate>		
            </asp:UpdatePanel>
            <div class="customFooter divFieldsFooter"></div>
        </div>
        <div class="customContainer divFieldsContainer">
            <div class="customLeft divFieldsLeft">
                <label>Membership Affiliations</label>
            </div>
            <div class="customRight divFieldsRight">
           <div></div>
            Add up to three memberships or affiliations.
            <br /><br />
                <asp:CheckBoxList ID="chkLstAffiliations" runat="server" CssClass="checkboxlist_nowrap tblAffiliations"></asp:CheckBoxList>
            </div>
            <div class="customFooter divFieldsFooter"></div>
        </div>
        <div class="customContainer divFieldsContainer">
            <div class="customLeft divFieldsLeft">
                <label>General Inquiries Email</label>
            </div>
            <div class="customRight divFieldsRight">
                <asp:TextBox ID="txtFeneralInqueryEmail" runat="server" MaxLength="30"></asp:TextBox>
                
                <asp:RegularExpressionValidator runat="server" id="revInqueryEmail" ControlToValidate="txtFeneralInqueryEmail" CssClass="divError" Text="Invalid format" ErrorMessage="Invalid general inquiries email format" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" Display="Dynamic" ValidationGroup="OPC"></asp:RegularExpressionValidator>
            </div>
            <div class="customFooter divFieldsFooter"></div>
        </div>
        <asp:Panel runat="server" ID="panActive" CssClass="customContainer divFieldsContainer" Visible="false">
        	<div class="customLeft divFieldsLeft">
                 <label>Status</label>
            </div>
            <div class="customRight divFieldsRight">
                <asp:DropDownList runat="server" ID="ddlFHActive" runat="server">
                    <asp:ListItem Text="Inactive" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="Publish" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Suspended" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="customFooter divFieldsFooter"></div>
        </asp:Panel>
        
        <asp:Panel runat="server" ID="panMap">
        	<div class="divSubmitHeader">
    		    <label id="gothic1117" class="teal lblFuneralHomeHeader">Map</label>
			</div>
			<div class="divFHSectionDescription">
	    		<label>Please verify your Funeral Home Location on map provided.  Should you need to adjust your location, click on icon and drag to the correct location.</label>
			</div>
            
			<script type='text/javascript'>
                function googleMapsInit()
                {                                                                            
                    <asp:Literal runat="server" ID="litLocGeo"></asp:Literal>							
                }//end of googleMapsInit()
            </script>
                
            <div id='divMap'></div>
            
            <div id="divMapSearch">
            	<a href="javascript:void(0);" onClick="<asp:Literal runat="server" ID="litLocGeoAgain"></asp:Literal>">Refresh Map</a>
			</div>
            
            <asp:HiddenField ID="hfMapLatitude" runat="server" />
            <asp:HiddenField ID="hfMapLongitude" runat="server" />
        </asp:Panel>
                
        <div class="divSubmitHeader">
            <label id="gothic1115" class="teal lblFuneralHomeHeader">Terms of Alliance Program</label>
        </div>
        <div class="divSignUpBody">
            <div>
                <a href="/funeralhomeallianceprogram.aspx" target="_blank">Click here to view Terms of Alliance Program</a>
            </div>
            <div class="customContainer divSubmitAgreeContainer">
                <div class="customLeft divSubmitAgreeLeft">
                    <asp:CheckBox runat="server" ID="chkAgreeTerms"></asp:CheckBox>
                </div>
                <div class="customLeft divSubmitAgreeRight">
                    <label>I accept Terms of Alliance Program</label>
                </div>
                <div class="customLeft divSubmitAgreeLeft"></div>
                <div class="customFooter divSubmitAgreeFooter"></div>
            </div>
        </div>
        
        <div class="divError">            
        	<asp:ValidationSummary ID="vsRegistration" runat="server" CssClass="vsRegistration" ValidationGroup="OPC" DisplayMode="List" HeaderText="Editor has been filled out incorrectly"></asp:ValidationSummary>
         
    	    <asp:Label runat="server" ID="lblError" Visible="false"></asp:Label>
	    </div>
        
        <div id="divSubmit">
            <asp:Button ID="cmdSave" runat="server" OnClick="cmdSave_Click" ValidationGroup="OPC" CssClass="green-button"></asp:Button>
            
            <asp:HiddenField ID="hfCurrentFHID" runat="server" />
            <asp:HiddenField ID="hfOrginalFHID" runat="server" />
        </div>
	</asp:Panel>
</asp:Panel>