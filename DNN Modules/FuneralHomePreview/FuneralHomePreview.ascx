<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FuneralHomePreview.ascx.cs" Inherits="DesktopModules_FuneralHomePreview_FuneralHomePreview" %>

<link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />
<link href="/Portals/_default/default.css?cdv=34" rel="stylesheet" type="text/css" />
<link href="/Portals/_default/Skins/Obit/css/app.css" rel="stylesheet" />
<link href="/Portals/_default/Skins/Obit/css/print.css" rel="stylesheet" type="text/css" media="print" />
<link href="/Portals/_default/Skins/Obit/css/atooltip.css" rel="stylesheet" />

<div id="divFHDetails" class="container">
    <asp:Panel runat="server" ID="panThankYou" CssClass="divSignUpThankYou divSignUpBody" Visible="false">
        <div class="divSubmitHeader">
            <label id="gothic106" class="teal">Thank You</label>
        </div>
        <div class="divSignUpThankYouBody divSignUpBody">
            <label><asp:Literal runat="server" ID="litFHThankYou" Text="This Funeral Home has been updated."></asp:Literal></label>
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="panSignUp">
    	<div class="divError"> 
    	    <asp:Label runat="server" ID="lblError" Visible="false"></asp:Label>
	    </div>
        <div class="OB-page-title">
            <div class="divPreviewFuneralHomeHeaderTitle">
                <label id="gothic100" class="lblFuneralHomeName"><asp:Label ID="lblFuneralHomeName" runat="server"></asp:Label></label>
                <p>
                    <asp:Label ID="lblFuneralHomeTagLine" runat="server" CssClass="lblFuneralHomeTagLine"></asp:Label>
                    <strong>
                        <asp:Label ID="lblFuneralHomeLocation" runat="server" CssClass="lblFuneralHomeLocation"></asp:Label>
                    </strong>
                </p>
            </div>
            <div class="divPreviewFuneralHomeHeaderCommend">
                <asp:Button ID="cmdEdit" runat="server" OnClick="cmdEdit_Click" CssClass="green-button" Text="Edit" Visible="false"></asp:Button>
                <asp:Button ID="cmdSave" runat="server" OnClick="cmdSave_Click" CssClass="green-button" Text="Submit For Review" Visible="false"></asp:Button>
                                
                <asp:Label ID="lblFuneralAddress2" runat="server" Visible="false"></asp:Label>
            </div>
        </div>
    
        <div id="single-page" class="row">
            <!-- main-content -->
            <div id="main-content" class="span8">
                <div class="page-content" id="divFeatureFUImage">
                    <div class="featured-image">
                        <asp:Literal runat="server" ID="litImageSlider"></asp:Literal>
                    </div>
        
                    <div class="subtitle OB-subTitle divFHSubTitle">
                        <label id="gothic1112">About Us</label>
                    </div>
                    <div id="divAboutUsSummary" class="divJustHidden">
                    	<div id="dvFuneralHomeAboutUs" runat="server" class="divFuneralHomeSummary"></div>
                        <asp:Panel runat="server" ID="panFuneralHomeAboutUs">
                        	<br />
                    		<a href="javascript:void(0);" class="more" onClick="javascript:duelToggleLayer('divAboutUsMore','divAboutUsSummary','')">More</a>
						</asp:Panel>
					</div>
                    <div id="divAboutUsMore" class="divAboutUsMore divJustHidden">
                        <div id="dvFuneralHomeAboutUsMore" runat="server"></div>   
                        <br />
                        <a href="javascript:void(0);" class="more less-content" onClick="javascript:duelToggleLayer('divAboutUsSummary','divAboutUsMore','')">Close</a>
                    </div>
    
                    <div class="subtitle OB-subTitle divFHSubTitle">
                        <label id="gothic1113">Our Services Include</label>
                    </div>
                    <div class="row">
                        <div class="span3">
                            <ul class="check-list">
                                <asp:Literal ID="litOfferingsLeft" runat="server"></asp:Literal>
                            </ul>	
                        </div>
                        <div class="span4 customwidth">
                            <ul class="check-list">
                                <asp:Literal ID="litOfferingsRight" runat="server"></asp:Literal>							
                            </ul>	
                        </div>
                    </div>
                        
					<asp:Panel runat="server" ID="panAffiliation">
                        <div class="subtitle OB-subTitle divFHSubTitle">
                            <label id="gothic1114">Associations</label>
                        </div>
                        <ul class="logo-list">
                            <asp:Literal ID="litAffiliationsLogo" runat="server"></asp:Literal>					
                        </ul>
					</asp:Panel>
                </div>
            </div><!-- /#main-content -->
    
            <!-- sidebar -->
            <div id="sidebar" class="span4">
                <div class="widget widget-white">
                    <asp:Image Width="270" ID="imgFuneralHomeLogo" runat="server" ImageUrl="/Portals/_default/Skins/Obit/Images/ob-header-logo.png"></asp:Image>
                    
                    <ul class="funeral-data">
                        <li>
                        	<asp:Panel runat="server" ID="panFuneralHomeUrl">
                        		<asp:HyperLink ID="lnkFuneralHomeUrl" runat="server" Target="_blank" CssClass="lnkFuneralHomeUrl"></asp:HyperLink>
							</asp:Panel>
						</li>
                        <li>
                        	<asp:Panel runat="server" ID="panFuneralHomeGeneralEmail">
                        		<asp:HyperLink ID="lnkFuneralHomeGeneralEmail" runat="server" CssClass="lnkFuneralHomeGeneralEmail"></asp:HyperLink>
							</asp:Panel>
						</li>
                        <li>
                        	<asp:Panel runat="server" ID="panFuneralHomePhoneNo">
                        		<label>Phone: </label>
                                <asp:HyperLink ID="lnkFuneralHomePhoneNo" runat="server"></asp:HyperLink>
							</asp:Panel>
						</li>
                        <li>
                        	<asp:Panel runat="server" ID="panFuneralHomeFax">
                        		<label>Fax: </label>
                                <asp:HyperLink ID="lnkFuneralHomeFax" runat="server"></asp:HyperLink>
							</asp:Panel>
						</li>
                    </ul>
                    <div class="map-location">
                        <script type='text/javascript'>
                            function googleMapsInit()
                            {
                                var latLngDefault = new google.maps.LatLng(43.68575,-79.37645);
                                var mapOptions = {
                                    center: latLngDefault,
                                    mapTypeControl: true,
                                    mapTypeControlOptions: {
                                        style: google.maps.MapTypeControlStyle.DROPDOWN_MENU
                                    },
                                    zoom: 15,
                                    zoomControl: true,
                                    zoomControlOptions: {
                                        style: google.maps.ZoomControlStyle.SMALL
                                    }, 
                                    mapTypeId: google.maps.MapTypeId.ROADMAP
                                }//holds the options of the map
                        
                                var map = new google.maps.Map(document.getElementById('divMap'), mapOptions);
                                                                                
                                <asp:Literal runat="server" ID="litLocGeo"></asp:Literal>							
                            }//end of googleMapsInit()
                        </script>
                    
                        <div id='divMap' class="divFHDetailsMap"></div>
                    </div>
					<div align="center">
                    	<br />
					 	<strong><asp:Label ID="lblFuneralAddress" runat="server"></asp:Label></strong>
                        <br />                            
                        <asp:HyperLink ID="lnkGoogleMap" runat="server" Text="Full Map &amp; Directions" Target="_blank" CssClass="right-arrow"></asp:HyperLink>
					</div>
					
                    <asp:Panel ID="panOurFuneralService" runat="server" CssClass="divWidgetServices widget-funeral-services OB-subTitle">
                        <div class="OB-subTitle divFHServices divFHSubTitle" id="gothic1115">
                        	<label>Our Funeral Services</label>
						</div>
                        <div class="divFHServicesBorder"></div>
						<asp:Repeater ID="rpObituaries" runat="server">
                            <HeaderTemplate>
                                 <ul class="funeral-data" id="ulFHServices">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li>
                                    <asp:HyperLink ID="lblFullName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FullName") %>' NavigateUrl='<%# "/Obituaries.aspx?ObituariesID=" + DataBinder.Eval(Container.DataItem, "ObituaryID") %>' CssClass="teal"></asp:HyperLink><br />
                                    <asp:Label ID="lblServiceDate" runat="server" Text='<%# string.Format("{0:MMMM dd, yyyy}", Convert.ToDateTime(Eval("PassingDate"))) %>'></asp:Label>
                                </li>
                            </ItemTemplate>
                            <FooterTemplate>
                                </ul>                                
                            </FooterTemplate>                            
                        </asp:Repeater>
                        
						<div class="divFHServicesBorder"></div>
                        
                        <asp:HyperLink ID="hlViewAllServices" runat="server" CssClass="view-blue" Text="View all services"></asp:HyperLink>
                    </asp:Panel>

                    <asp:Panel runat="server" ID="panReminders" CssClass="divWidgetServices widget-funeral-services OB-subTitle">
                        <asp:Panel runat="server" ID="panReminderBody" CssClass="accordion-inner">                                                    
                            <asp:CheckBox runat="server" ID="chkFuneralHome" CssClass="prettycheckbox" Checked="true" Text=" Receive a copy of all announcements published by Funeral Home"></asp:CheckBox>
                            <br />
                            <asp:Panel runat="server" ID="panReminderEnterEmail">
                                <div class="divError">
                                    <asp:Label runat="server" ID="lblReminderError" Visible="false"></asp:Label>
                                </div>
    
                                <asp:TextBox runat="server" ID="txtReminderEMail" MaxLength="50" ValidationGroup="Reminder" placeholder="Your Email Address"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvReminderEMail" runat="server" ErrorMessage="<br/>Please fill your email address" Display="Dynamic" ControlToValidate="txtReminderEMail" ValidationGroup="Reminder"  ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="revReminderEmail" ControlToValidate="txtReminderEMail" ErrorMessage="<br/>Invalid email format" ValidationExpression="^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$" Display="Dynamic" ValidationGroup="Reminder" ForeColor="Red"/>
                            </asp:Panel>
                                        
                            <asp:Label ID="lblReminderEmail" runat="server" Visible="false"></asp:Label>
                                        
                            <div class="send-mail-btn">
                                <asp:Button ID="cmdSaveReminder" runat="server" Text="Get Reminders" OnClick="cmdSaveReminder_Click" class="mail-btn" ValidationGroup="Reminder"></asp:Button>
                            </div>
                        </asp:Panel>
                        <asp:Label runat="server" ID="litReminderThankYou" Text="You are subscribed to receive reminders" Font-Bold="true" Visible="false" Font-Size="Smaller"></asp:Label>
                    </asp:Panel>

                    <asp:Panel runat="server" ID="panBookmark" CssClass="widget-funeral-services OB-subTitle">
                        <asp:Panel runat="server" ID="panBookmarkBody" CssClass="accordion-inner">                                                    
                            <asp:Label runat="server" ID="lblBookmark" Text="Please click this button to book mark this page."></asp:Label>
                            <br /><br />
                            <asp:Label runat="server" ID="lblBookmarkError" Visible="false"></asp:Label>                                        
                                        
                            <div class="send-mail-btn">
                                <asp:Button ID="btnBookmark" runat="server" Text="Book Mark" OnClick="btnBookmark_Click" class="mail-btn"></asp:Button>
                            </div>
                        </asp:Panel>
                        <asp:Label runat="server" ID="litBookmarkThankYou" Text="You have book marked this page." Font-Bold="true" Visible="false" Font-Size="Smaller"></asp:Label>
                    </asp:Panel>
                </div><!-- /.widget -->
            </div><!-- /#sidebar -->
        </div>
	</asp:Panel>
</div>

<asp:HiddenField ID="hfFHID" Value="0" runat="server" />
<asp:HiddenField ID="hfOrginalFHID" Value="0" runat="server" />
<asp:HiddenField ID="hfOrginalCity" Value="0" runat="server" />
<asp:HiddenField ID="hfOrginalProvince" Value="0" runat="server" />
<asp:HiddenField ID="hfOrginalCountry" Value="0" runat="server" />
<asp:HiddenField ID="hfOrginalPostalCode" Value="0" runat="server" />
<asp:HiddenField ID="hfMapLatitude" runat="server" />
<asp:HiddenField ID="hfMapLongitude" runat="server" />