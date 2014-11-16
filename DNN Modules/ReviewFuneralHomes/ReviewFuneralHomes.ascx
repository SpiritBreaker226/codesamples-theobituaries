<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReviewFuneralHomes.ascx.cs" Inherits="ReviewFuneralHomes" %>

<%@ Register TagPrefix="uc1" TagName="CropImage" Src="~/Portals/_default/Skins/Obit/UC/CropImages.ascx" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx" %>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.9.2/jquery-ui.min.js"></script>
<script src='http://maps.googleapis.com/maps/api/js?sensor=false' type='text/javascript'></script>
<script src="/Portals/_default/Skins/Obit/JS/Master.js" type="text/javascript" ></script>

<link rel="stylesheet" type="text/css" href="/Portals/_default/Skins/Obit/CSS/ForDreamwaverUsers.css" media="screen" />
<link rel="stylesheet" type="text/css" href="/Portals/_default/Skins/Obit/CSS/adminStyle.css" media="screen" />
<link rel="stylesheet" type="text/css" href="/Portals/_default/Skins/Obit/CSS/styles.css" media="screen" />

<asp:Panel runat="server" ID="panFHDisplay">
    <div class="customContainer divReportContainer">
        <div class="customLeft divReportLeft">
        	<div class="customContainer divSectionHeaderContainer">
                <div class="customLeft divSectionHeaderLeft">
                   <%-- <asp:Button ID="cmdAddNew" runat="server" Text="Add Funeral Home" OnClick="cmdAdd_Click"></asp:Button>--%>&nbsp;
                </div>
                <div class="customRight divSectionHeaderRight">
                    <%--<asp:Button ID="cmdRefresh" runat="server" Text="Refresh" OnClick="cmdCancel_Click"></asp:Button>--%>&nbsp;
                </div>
                <div class="customFooter divSectionHeaderFooter"></div>
            </div>
        </div>
        <div class="customRight divReportRight">
        	<asp:Panel id="panSearch" runat="server" DefaultButton="cmdSearch">
                <div class="customContainer divSearchContainer">
                    <div class="customLeft divSearchLeft">
                        <label>Name</label>
                    </div>
                    <div class="customMiddle divSearchMiddle">
                        <asp:TextBox ID="txtSearch" runat="server" MaxLength="255"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="SearchRequired" runat="server" Text="*" ErrorMessage="Fill in the name for search" Display="Static" ControlToValidate="txtSearch" ValidationGroup="Search" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" id="NameValidator" ControlToValidate="txtSearch" Text="*" ErrorMessage="Invalid name format" ValidationExpression="^[a-zA-Z]+(([\'\,\.\-\ \&][a-zA-Z])?[a-zA-Z]*)*$" Display="Static" ValidationGroup="OPC" />
                    </div>
                    <div class="customRight divSearchRight">
                        <asp:Button ID="cmdSearch" runat="server" Text="Search" OnClick="cmdSearch_Click" ValidationGroup="Search"></asp:Button>
                        <asp:Button ID="cmdClearSearch" runat="server" Text="Clear Search" OnClick="cmdClearSearch_Click"></asp:Button>
                    </div>
                    <div class="customFooter divSearchFooter"></div>
                </div>
			</asp:Panel>
        </div>
        <div class="customFooter divReportFooter"></div>
    </div>
    
    <div class="divError">  
        <asp:Label runat="server" ID="lblGirdError" Visible="false"></asp:Label>
    </div>

    <asp:DataGrid id="gdFH" runat="server" Width="950px" GridLines="None" CellPadding="3" AutoGenerateColumns="False" OnItemCommand="gdFH_ItemCommand" OnItemDataBound="gdFH_ItemDataBound" CssClass="tblAdminFH" HeaderStyle-CssClass="divReviewFHHeader" ItemStyle-CssClass="divReviewFHBody">
        <Columns>
        	<asp:BoundColumn DataField="" Visible="False"></asp:BoundColumn>
        	<asp:BoundColumn DataField="" ItemStyle-Width="15%" HeaderText="Name"></asp:BoundColumn>
        	<asp:BoundColumn DataField="" Visible="False"></asp:BoundColumn>
        	<asp:BoundColumn DataField="" Visible="False"></asp:BoundColumn>
        	<asp:BoundColumn DataField="" Visible="False"></asp:BoundColumn>
        	<asp:BoundColumn DataField="" Visible="False"></asp:BoundColumn>
        	<asp:BoundColumn DataField="" Visible="False"></asp:BoundColumn>
        	<asp:BoundColumn DataField="" Visible="False"></asp:BoundColumn>
        	<asp:BoundColumn DataField="" Visible="false"></asp:BoundColumn>
            <asp:TemplateColumn>
            	<headertemplate>
               		<label>Address</label>
                </headertemplate>
                <itemtemplate>
                	<div class="divFullAddress">
	                	<asp:Label runat="server" ID="lblFullAddress"></asp:Label>
					</div>
                </itemtemplate>
            </asp:TemplateColumn>
        	<asp:BoundColumn DataField="" Visible="false"></asp:BoundColumn>
        	<asp:BoundColumn DataField="" Visible="False"></asp:BoundColumn>
        	<asp:BoundColumn DataField="" Visible="False"></asp:BoundColumn>
        	<asp:BoundColumn DataField="" Visible="False"></asp:BoundColumn>
        	<asp:BoundColumn DataField="" Visible="False"></asp:BoundColumn>
        	<asp:BoundColumn DataField="" HeaderText="Phone"></asp:BoundColumn>
        	<asp:BoundColumn DataField="" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" HeaderText="Last Update Date" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
        	<asp:BoundColumn DataField="" Visible="False"></asp:BoundColumn>
        	<asp:BoundColumn DataField="" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="" Visible="False"></asp:BoundColumn>
            <asp:TemplateColumn>
            	<headertemplate>
               		<div class="customContainer divReviewFuneralHomeHeaderContainer">
                        <div class="customLeft divReviewFuneralHomeHeaderLeft divReviewFHHeader">
                        	<label>User ID</label>
                        </div>
                        <div class="customLeft divReviewFuneralHomeHeaderLeft divReviewFHHeader">
                        	<label>Created</label>
                        </div>
                        <div class="customMiddle divReviewFuneralHomeHeaderMiddle divReviewFHHeader">
                        	<label>Publish</label>
                        </div>
                        <div class="customLeft divReviewFuneralHomeHeaderLeft divReviewFHHeader">
                        	<label>Edit Request</label>
                        </div>
                        <div class="customRight divReviewFuneralHomeHeaderRight divReviewFHHeader">
                        	<label>Suspended</label>
                        </div>
                    	<div class="customFooter divReviewFuneralHomeHeaderFooter"></div>
                    </div>
                </headertemplate>
                <itemtemplate>
                	<div class="customContainer divReviewFuneralHomeHeaderContainer">
                        <div class="customLeft divReviewFuneralHomeHeaderLeft">
                        	<asp:CheckBox ID="chkApprove" runat="server" Enabled="false"></asp:CheckBox>
                        </div>
                        <div class="customLeft divReviewFuneralHomeHeaderLeft">
                        	<asp:CheckBox ID="chkCreated" runat="server" Enabled="false"></asp:CheckBox>
                        </div>
                        <div class="customMiddle divReviewFuneralHomeHeaderMiddle">
                        	<asp:CheckBox ID="chkPublish" runat="server" Enabled="false"></asp:CheckBox>
                        </div>
                        <div class="customLeft divReviewFuneralHomeHeaderLeft">
                        	<asp:CheckBox ID="chkDraft" runat="server" Enabled="false"></asp:CheckBox>
                        </div>
                        <div class="customRight divReviewFuneralHomeHeaderRight">
                        	<asp:CheckBox ID="chkSuspended" runat="server" Enabled="false"></asp:CheckBox>
                        </div>
                        <div class="customMiddle divReviewFuneralHomeHeaderMiddleButtons divReviewFuneralHomeNoticeButtons">
                        	<asp:Button runat="server" ID="cmdNotice" Text="Send Password" CommandName="Notice"></asp:Button>
		                    <asp:Label runat="server" ID="lblNotice" Text="No User Assigned" Visible="false"></asp:Label>
                        </div>
                        <div class="customMiddle divReviewFuneralHomeHeaderMiddleButtons divReviewFuneralHomeEditButtons">
							<asp:Button runat="server" ID="cmdUpdate" Text="Edit" CommandName="Update"></asp:Button>
                        </div>
                        <div class="customMiddle divReviewFuneralHomeHeaderMiddleButtons divReviewFuneralHomeDeleteButtons">
							<asp:Button runat="server" ID="cmdDelete" Text="Delete" CommandName="Delete" OnClientClick="javascript: return confirm('Do you what to remove this Funeral Home?');"></asp:Button>
                        </div>
                    	<div class="customFooter divReviewFuneralHomeHeaderFooter"></div>
                    </div>
                </itemtemplate>
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>	   
    <asp:HiddenField ID="HiddenPageIndex" runat="server" Value="0" />
    <asp:Panel ID="PagerPanel" runat="server">
        <div id="divPager">
            <asp:Repeater ID="PagerControls" runat="server" OnItemCommand="PagerControls_ItemCommand">
                <ItemTemplate>
                    <a class='<%#Eval("TagClass")%>' href='<%#Eval("NavigateUrl")%>'><%#Eval("Text")%></a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </asp:Panel> 
    <asp:Literal ID="litNoFound" runat="server" Visible="false"></asp:Literal>     
</asp:Panel>
<asp:Panel runat="server" ID="panFHAdd" Visible="false">
    <asp:Label runat="server" ID="lblID" Visible="False"></asp:Label>
    <asp:Label runat="server" ID="lblOrginalID" Visible="False"></asp:Label>
    <asp:Label runat="server" ID="lblUserID" Visible="False"></asp:Label>
    <asp:Label runat="server" ID="lblStatus" Visible="False"></asp:Label>
    <asp:Label runat="server" ID="lblCreated" Visible="False"></asp:Label>
    <asp:Label runat="server" ID="lblUpdate" Visible="False"></asp:Label>
    <asp:Label runat="server" ID="lblPublish" Visible="False"></asp:Label>
    <asp:Label ID="lblMainTitle" runat="server" Text="Fill the form to insert the Item data" CssClass="lblMainTitle"></asp:Label>
    <br /><br />
    <div id="divError">
        <asp:ValidationSummary ID="vsRegistration" runat="server" CssClass="vsRegistration" ValidationGroup="OPC" DisplayMode="List" HeaderText="Funeral Home has been filled out incorrectly" />
  
        <asp:Label runat="server" ID="lblError" Visible="false"></asp:Label>
    </div>
    
    <asp:Panel runat="server" ID="panLiveDraft" CssClass="divLiveDraft" Visible="false">
    	<div class="customContainer divFieldsContainer">
            <div class="customLeft divFieldsLeft">
                <label>Mode</label>
            </div>
            <div class="customRight divFieldsRight divFieldsFH">
		    	<asp:DropDownList runat="server" ID="ddlLiveDraft" AutoPostBack="true" OnSelectedIndexChanged="ddlLiveDraft_SelectedIndexChanged"></asp:DropDownList>
			</div>
            <div class="customFooter divFieldsFooter"></div>
        </div>
    </asp:Panel>
    
    <div class="customContainer divFHContainer">
        <div class="customLeft divFHLeft">
        	<div class="customContainer divFieldsContainer">
                <div class="customLeft divFieldsLeft">
                    <label>Name *</label>
                </div>
                <div class="customRight divFieldsRight divFieldsFH">
                    <asp:TextBox ID="txtFuneralName" runat="server" MaxLength="255"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="FuneralNameRequired" runat="server" Text="*" ErrorMessage="Fill in the funeral name" Display="Static" ControlToValidate="txtFuneralName" ValidationGroup="OPC" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    <%--<asp:RegularExpressionValidator runat="server" id="FuneralNameValidator" ControlToValidate="txtFuneralName" Text="*" ErrorMessage="Invalid funeral name format" ValidationExpression="^[a-zA-Z]+(([\'\,\.\-\ \&][a-zA-Z])?[a-zA-Z]*)*$" Display="Static" ValidationGroup="OPC" />--%>
                </div>
                <div class="customFooter divFieldsFooter"></div>
            </div>
            <div class="customContainer divFieldsContainer">
                <div class="customLeft divFieldsLeft">
	                <label>Status</label>
                </div>
                <div class="customRight divFieldsRight">
                    <asp:DropDownList runat="server" ID="ddlFHActive" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFHActive_SelectedIndexChanged">
                        <asp:ListItem Text="Under Review" Value="-2"></asp:ListItem>
                        <asp:ListItem Text="Inactive" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Publish" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Suspended" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="customFooter divFieldsFooter"></div>
            </div>
            <div class="customContainer divFieldsContainer">
                <div class="customLeft divFieldsLeft">
                    <label>Tag Line</label>
                </div>
                <div class="customRight divFieldsRight">
                    <asp:TextBox ID="txtTagLine" runat="server" MaxLength="75"></asp:TextBox>                
                </div>
                <div class="customFooter divFieldsFooter"></div>
            </div>
            <div class="customContainer divFieldsContainer">
                <div class="customLeft divFieldsLeft">
                    <label>City *</label>
                </div>
                <div class="customRight divFieldsRight divFieldsFH">
                    <asp:TextBox ID="txtFuneralCity" runat="server" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="FuneralCityRequired" runat="server" Text="*" ErrorMessage="Fill in the funeral city" Display="Static" ControlToValidate="txtFuneralCity" ValidationGroup="OPC" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" id="FuneralCityValidator" ControlToValidate="txtFuneralCity" Text="*" ErrorMessage="Invalid funeral city format" ValidationExpression="^[a-zA-Z]+(([\'\,\.\-\ ][a-zA-Z])?[a-zA-Z]*)*$" Display="Static" ValidationGroup="OPC" />
                </div>
                <div class="customFooter divFieldsFooter"></div>
            </div>
            <div class="customContainer divFieldsContainer">
                <div class="customLeft divFieldsLeft">
                    <label>Address 1 *</label>
                </div>
                <div class="customRight divFieldsRight divFieldsFH">
                    <asp:TextBox ID="txtFuneralAddress1" runat="server" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="FuneralAddress1Required" runat="server" Text="*" ErrorMessage="Fill in the funeral address 1" Display="Static" ControlToValidate="txtFuneralAddress1" ValidationGroup="OPC" SetFocusOnError="true"></asp:RequiredFieldValidator>
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
                            <asp:RequiredFieldValidator ID="PCRequired" runat="server" Text="*" ErrorMessage="Fill in the funeral postal/zip code" Display="Static" ControlToValidate="txtPC" ValidationGroup="OPC" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator runat="server" id="revPC" ControlToValidate="txtPC" Text="*" ErrorMessage="Invalid postal code format" ValidationExpression="^[A-Z]\d[A-Z][ ]\d[A-Z]\d$" Display="Static" ValidationGroup="OPC" />
                        </div>
                        <div class="customFooter divFieldsFooter"></div>
                    </div>                    
                </ContentTemplate>
            </asp:UpdatePanel>
                        
            <div class="customContainer divFieldsContainer">
                <div class="customLeft divFieldsLeft">
                    <label>General Inquiries Email</label>
                </div>
                <div class="customRight divFieldsRight">
                    <asp:TextBox ID="txtFeneralInqueryEmail" runat="server" MaxLength="30"></asp:TextBox>
                    <asp:RegularExpressionValidator runat="server" id="revFeneralInqueryEmail" ControlToValidate="txtFeneralInqueryEmail" Text="*" ErrorMessage="Invalid general inquiries email format" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" Display="Static" ValidationGroup="OPC" />
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
                            <asp:CheckBoxList ID="chkLstOfferingText" runat="server" CssClass="checkboxlist_nowrap tblOffering" DataTextField="OfferingsName" DataValueField="OfferingsID"></asp:CheckBoxList>
                            <asp:Panel runat="server" ID="panOtherOfferings" Visible="false">
                            	<br/>
                            	<asp:PlaceHolder runat="server" ID="phOtherOfferings"></asp:PlaceHolder>
                            
                            	<div id="divOtherOfferings">
                            	    <asp:LinkButton ID="lbAddOtherOfferings" runat="server" OnClick="lbAddOtherOfferings_Click" Text="Add more other offerings"></asp:LinkButton>
                            	</div>
                            	<asp:HiddenField ID="hfOtherOfferings" runat="server" Value="0" />
                            	<asp:HiddenField ID="hfOtherOfferingsValue" runat="server" Value="" />
							</asp:Panel>
                        </asp:Panel>
                    </ContentTemplate>		
                </asp:UpdatePanel>
                <div class="customFooter divFieldsFooter"></div>
            </div>
		</div>
        <div class="customRight divFHRight">
        	<asp:Panel runat="server" ID="panReviewLogo" CssClass="divReviewLogo" Visible="false">
            	<asp:Button runat="server" ID="cmdReview" Text="Review "></asp:Button>
            </asp:Panel>
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
                        <asp:Image runat="server" ID="imgLogo" Width="230"></asp:Image>
                        <asp:Label runat="server" ID="lblImageSource" Visible="False"></asp:Label>
                    </div>
                    <div class="customFooter divFieldsFooter"></div>
                </div>
            </asp:Panel>
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
                    <label>URL</label>
                </div>
                <div class="customRight divFieldsRight divFieldsFH">
                    <asp:TextBox ID="txtFuneralURL" runat="server" MaxLength="255"></asp:TextBox>
                    <asp:RegularExpressionValidator runat="server" id="FuneralURLValidator" ControlToValidate="txtFuneralURL" Text="*" ErrorMessage="Invalid funeral URL format" ValidationExpression="((http(s?):\/\/)|(www\.[^ \[\]\(\)\n\r\t]+)|(([012]?[0-9]{1,2}\.){3}[012]?[0-9]{1,2})\/)([^ \[\]\(\),;&quot;'&lt;&gt;\n\r\t]+)([^\. \[\]\(\),;&quot;'&lt;&gt;\n\r\t])|(([012]?[0-9]{1,2}\.){3}[012]?[0-9]{1,2})" Display="Static" ValidationGroup="OPC" />
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
                    <label>Phone Number *</label>
                </div>
                <div class="customRight divFieldsRight divFieldsFH">
                    <asp:TextBox ID="txtFuneralPhone" runat="server" MaxLength="15"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="FuneralPhoneRequired" runat="server" Text="*" ErrorMessage="Fill in the funeral phone" Display="Static" ControlToValidate="txtFuneralPhone" ValidationGroup="OPC" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" id="FuneralPhoneValidator" ControlToValidate="txtFuneralPhone" Text="*" ErrorMessage="Invalid funeral phone format ex.(123) 123-4567" ValidationExpression="([\(]?(?<AreaCode>[0-9]{3})[\)]?)?[ \.\-]?(?<Exchange>[0-9]{3})[ \.\-](?<Number>[0-9]{4})" Display="Static" ValidationGroup="OPC" />
                </div>
                <div class="customFooter divFieldsFooter"></div>
            </div>
            <div class="customContainer divFieldsContainer">
                <div class="customLeft divFieldsLeft">
                    <label>Fax Number</label>
                </div>
                <div class="customRight divFieldsRight divFieldsFH">
                    <asp:TextBox ID="txtFuneralFax" runat="server" MaxLength="15"></asp:TextBox>
                    <asp:RegularExpressionValidator runat="server" id="FuneralFaxValidator" ControlToValidate="txtFuneralFax" Text="*" ErrorMessage="Invalid funeral fax format ex.(123) 123-4567" ValidationExpression="([\(]?(?<AreaCode>[0-9]{3})[\)]?)?[ \.\-]?(?<Exchange>[0-9]{3})[ \.\-](?<Number>[0-9]{4})" Display="Static" ValidationGroup="OPC" />
                </div>
                <div class="customFooter divFieldsFooter"></div>
            </div>
            <div id="divUserAttachTitle">
                <label><strong>User Attach Profile</strong></label>
            </div>
            
            <asp:Panel runat="server" ID="panNoUserAttach" CssClass="divNoUserAttach">
            	<label>No User Attach to this Funeral Home</label>
            </asp:Panel>
            <asp:Panel runat="server" ID="panUserAttach" Visible="false" CssClass="divUserAttach">
            	<asp:Panel runat="server" ID="panUserTitle" CssClass="customContainer divFieldsContainer" Visible="false">
                    <div class="customLeft divFieldsLeft">
                        <label>Title:</label>
                    </div>
                    <div class="customRight divFieldsRight divFieldsFH lblUserAttachField">
                        <asp:Label ID="lblUserTitle" runat="server"></asp:Label>
                    </div>
                    <div class="customFooter divFieldsFooter"></div>
                </asp:Panel>
                <div class="customContainer divFieldsContainer">
                    <div class="customLeft divFieldsLeft">
                        <label>First Name:</label>
                    </div>
                    <div class="customRight divFieldsRight divFieldsFH lblUserAttachField">
                        <asp:Label ID="lblUserFirstName" runat="server"></asp:Label>
                    </div>
                    <div class="customFooter divFieldsFooter"></div>
                </div>
                <div class="customContainer divFieldsContainer">
                    <div class="customLeft divFieldsLeft">
                        <label>Last Name:</label>
                    </div>
                    <div class="customRight divFieldsRight divFieldsFH lblUserAttachField">
                        <asp:Label ID="lblUserLastName" runat="server"></asp:Label>
                    </div>
                    <div class="customFooter divFieldsFooter"></div>
                </div>
                <div class="customContainer divFieldsContainer">
                    <div class="customLeft divFieldsLeft">
                        <label>Email:</label>
                    </div>
                    <div class="customRight divFieldsRight divFieldsFH lblUserAttachField">
                        <asp:HyperLink ID="hlUserEmail" runat="server"></asp:HyperLink>
                    </div>
                    <div class="customFooter divFieldsFooter"></div>
                </div>
                <asp:Panel runat="server" ID="panUserPhone" CssClass="customContainer divFieldsContainer" Visible="false">
                    <div class="customLeft divFieldsLeft">
                        <label>Phone:</label>
                    </div>
                    <div class="customRight divFieldsRight divFieldsFH lblUserAttachField">
                        <asp:HyperLink ID="hlUserPhone" runat="server"></asp:HyperLink>
                    </div>
                    <div class="customFooter divFieldsFooter"></div>
                </asp:Panel>
                <asp:Panel runat="server" ID="panUserFax" CssClass="customContainer divFieldsContainer" Visible="false">
                    <div class="customLeft divFieldsLeft">
                        <label>Fax:</label>
                    </div>
                    <div class="customRight divFieldsRight divFieldsFH lblUserAttachField">
                        <asp:Label ID="lblUserFax" runat="server"></asp:Label>
                    </div>
                    <div class="customFooter divFieldsFooter"></div>
                </asp:Panel>
                <asp:Panel runat="server" ID="panUserCompany" CssClass="customContainer divFieldsContainer" Visible="false">
                    <div class="customLeft divFieldsLeft">
                        <label>Company:</label>
                    </div>
                    <div class="customRight divFieldsRight divFieldsFH lblUserAttachField">
                        <asp:Label ID="lblUserCompany" runat="server"></asp:Label>
                    </div>
                    <div class="customFooter divFieldsFooter"></div>
                </asp:Panel>
                <asp:Panel runat="server" ID="panUserAddress1" CssClass="customContainer divFieldsContainer" Visible="false">
                    <div class="customLeft divFieldsLeft">
                        <label>Address 1:</label>
                    </div>
                    <div class="customRight divFieldsRight divFieldsFH lblUserAttachField">
                        <asp:Label ID="lblUserAddress1" runat="server"></asp:Label>
                    </div>
                    <div class="customFooter divFieldsFooter"></div>
                </asp:Panel>
                <asp:Panel runat="server" ID="panUserAddress2" CssClass="customContainer divFieldsContainer" Visible="false">
                    <div class="customLeft divFieldsLeft">
                        <label>Address 2:</label>
                    </div>
                    <div class="customRight divFieldsRight divFieldsFH lblUserAttachField">
                        <asp:Label ID="lblUserAddress2" runat="server"></asp:Label>
                    </div>
                    <div class="customFooter divFieldsFooter"></div>
                </asp:Panel>
                <asp:Panel runat="server" ID="panUserCity" CssClass="customContainer divFieldsContainer" Visible="false">
                    <div class="customLeft divFieldsLeft">
                        <label>City:</label>
                    </div>
                    <div class="customRight divFieldsRight divFieldsFH lblUserAttachField">
                        <asp:Label ID="lblUserCity" runat="server"></asp:Label>
                    </div>
                    <div class="customFooter divFieldsFooter"></div>
                </asp:Panel>
                <asp:Panel runat="server" ID="panUserProvince" CssClass="customContainer divFieldsContainer" Visible="false">
                    <div class="customLeft divFieldsLeft">
                        <label>Province:</label>
                    </div>
                    <div class="customRight divFieldsRight divFieldsFH lblUserAttachField">
                        <asp:Label ID="lblUserProvince" runat="server"></asp:Label>
                    </div>
                    <div class="customFooter divFieldsFooter"></div>
                </asp:Panel>
                <asp:Panel runat="server" ID="panUserCountry" CssClass="customContainer divFieldsContainer" Visible="false">
                    <div class="customLeft divFieldsLeft">
                        <label>Country:</label>
                    </div>
                    <div class="customRight divFieldsRight divFieldsFH lblUserAttachField">
                        <asp:Label ID="lblUserCountry" runat="server"></asp:Label>
                    </div>
                    <div class="customFooter divFieldsFooter"></div>
                </asp:Panel>
                 <asp:Panel runat="server" ID="panUserActive" CssClass="customContainer divFieldsContainer" Visible="false">
                    <div class="customLeft divFieldsLeft">
                        <label>Is Active:</label>
                    </div>
                    <div class="customRight divFieldsRight divFieldsFH lblUserAttachField">
                        <asp:Label ID="lblUserActive" runat="server"></asp:Label>
                    </div>
                    <div class="customFooter divFieldsFooter"></div>
                </asp:Panel>
			</asp:Panel>
        </div>
        <div class="customFooter divFHFooter"></div>
	</div>
    
    <div class="customContainer divFieldsContainer">
        <div class="customLeft divFieldsLeft">
            <label>About Us *</label>
            <asp:RequiredFieldValidator ID="rfvAboutUs" runat="server" Text="*" ErrorMessage="Fill in the about us" Display="Static" ControlToValidate="txtAboutUs" ValidationGroup="OPC" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </div>
        <div class="customRight divFieldsRight">
        	<dnn:TextEditor ID="txtAboutUs" ChooseRender="false" ChooseMode="false" DefaultMode="Basic" HtmlEncode="true" Width="100%" Height="300px" runat="server" ToolbarMode="Default"></dnn:TextEditor>
            <%--<CKEditor:CKEditorControl ID="txtAboutUs" runat="server" Toolbar="Cut|Copy|Paste|PasteText|PasteFromWord|-|Undo|Redo|
        Find|Replace|-|SpellChecker|Scayt
        /
        Bold|Italic|Underline|Strike|Subscript|Superscript'"></CKEditor:CKEditorControl>--%>
        </div>
        <div class="customFooter divFieldsFooter"></div>
    </div>
    
    <div class="customContainer divFieldsContainer">
        <div class="customLeft divFieldsLeft">
            <label>Membership Affiliations</label>
        </div>
        <div class="customRight divFieldsRight">
            <asp:CheckBoxList ID="chkLstAffiliations" runat="server" CssClass="checkboxlist_nowrap tblAffiliations"></asp:CheckBoxList>
        </div>
        <div class="customFooter divFieldsFooter"></div>
    </div>
    
    <div class="divSubmitHeader">
        <label id="gothic1116" class="teal">Images</label>
    </div>
    <uc1:CropImage runat="server" ID="ciFH" />
        
    <asp:Panel runat="server" ID="panMap">
		<div class="divSubmitHeader">
        	<label id="gothic1117" class="teal">Map</label>
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
    
    <div id="divLeftSide">
        <asp:Button ID="cmdApprove" runat="server" Text="Approve Changes" OnClick="cmdApprove_Click" ValidationGroup="OPC" Visible="false"></asp:Button>
        <asp:Button ID="cmdSave" runat="server" Text="Save" OnClick="cmdSave_Click" ValidationGroup="OPC"></asp:Button>
        <asp:Button ID="cmdCancel" runat="server" Text="Cancel" OnClick="cmdCancel_Click" Visible="false"></asp:Button>
    </div>
    
    <script type="text/javascript">
		startUp();
	</script>
</asp:Panel>