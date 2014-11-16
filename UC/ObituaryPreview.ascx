<%@ Control Language="C#" AutoEventWireup="True" CodeFile="ObituaryPreview.ascx.cs" Inherits="ObituaryPreview" %>

<link href="/Portals/_default/Skins/Obit/css/app.css" rel="stylesheet" />
<link rel="stylesheet" href="/Portals/_default/Skins/Obit/css/bootstrap.css" />
<link rel="stylesheet" href="/Portals/_default/Skins/Obit/css/styles.css" />
<link rel="stylesheet" href="/Portals/_default/Skins/Obit/css/atooltip.css" />
<link rel="stylesheet" href="/Portals/_default/Skins/Obit/css/printObituaries.css" media="print" />

<link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="stylesheet" type="text/css"/>

<div id="fb-root"></div>
<script src="http://connect.facebook.net/en_US/all.js"></script>

<div class="divError">
    <asp:Label runat="server" ID="lblMainError" Visible="false"></asp:Label>
</div>

<div id="main" class="container">
    <div class="row">
        <div class="obituary-title OB-PageTitle" id="divObituaryHeader">
        	<div id="divObiturayDetailsHeaderContainer" class="customContainer">
            	<div id="divObiturayDetailsHeaderLeft" class="customLeft">  
                    <div class="OB-pageTitle" id="gothicObitPrev1" align="left">
                        <asp:Label ID="lblName" runat="server"></asp:Label>
                    </div>
                    <p id="gothicObitPrev2" class="pBirthDate" align="left">
                        <asp:Label ID="lblBirthDateAndPassingDate" runat="server"></asp:Label>
                    </p>
				</div>
                <div id="divObiturayDetailsHeaderRight" class="customRight">   
                    <asp:Panel runat="server" ID="panPrint">
                        <a href="javascript:void(0);" onClick="window.print();"><img alt="Print" src="/portals/_default/skins/obit/images/print.jpg" /></a>
                    </asp:Panel>
				</div>
                <div id="divObiturayDetailsHeaderFooter" class="customFooter"></div>
			</div>
        </div>
    </div>

    <div id="single-page" class="row">
        <!-- main-content -->
        <div id="main-content" class="span8">
            <div id="intro-condolence" class="page-content">
                <div class="divPrintBody obituary-title" id="divPrintBodyTop">
                    <div class="OB-PageTitle divPrintName" id="gothicObitPrev6">
                        <asp:Label ID="lblPrintName" runat="server"></asp:Label>
                    </div>
                    <p id="gothicObitPrev7" class="divPrintDates">
                        <asp:Label ID="lblPrintBirthDateAndPassingDate" runat="server"></asp:Label>
                    </p>
                </div>
                        
                <div class="obituary-image">
                	<asp:Literal runat="server" ID="litImageSlider"></asp:Literal>
                </div>
                
                <div class="divPrintBody" id="divPrintBodyBottom">
                	<br /><br /><br />
                    <div id="divPrintDetails">
                    	<asp:Literal ID="litPrintDetails" runat="server"></asp:Literal>
					</div>
                    <br /><br />
                    <div class="divPrintLine">&nbsp;</div>
                    <img src="/Portals/_default/Skins/Obit/Images/ob-header-logo.png" alt="theObituaries.ca" />
                </div>
                
                <div id="divObituarySummary" class="divJustHidden">
                    <div class="divObituraySummaryBorder">
                        <div id="dvObituarySummary" runat="server"></div>
					</div>
                    
                    <asp:Panel runat="server" ID="panObituarySummary" CssClass="divObituaryMore">
                        <br />
                        <a href="javascript:void(0);" class="more" onClick="javascript:duelToggleLayer('divObituaryMore','divObituarySummary','')">More</a>
                    </asp:Panel>
                </div>
                
                <div id="divObituaryMore" class="divObituaryMore divJustHidden">
                    <div id="dvObituaryMore" runat="server"></div>   
                    <br />
                    <a href="javascript:void(0);" class="more less-content" onClick="javascript:duelToggleLayer('divObituarySummary','divObituaryMore','')">Close</a>
                </div>
            </div>
            <%--<asp:UpdatePanel ID="ajaxLeaveCondolence" runat="server">
                <ContentTemplate>--%>
                    <asp:Panel runat="server" ID="panCondolonces" CssClass="divCondoloncesBody">
                        <asp:Panel runat="server" ID="panCondoloncesFourm">
                            <div id="leave-condolonce" class="page-content">
                                <div id="divLeaveCondolenceTitleContainer" class="customContainer">
                                	<asp:Panel runat="server" ID="panLeaveCondolenceTitleLeft" CssClass="divLeaveCondolenceTitleLeft customLeft">
                                    	<div id="gothicObitPrev3">
	                                    	<asp:HyperLink runat="server" ID="hlLeaveCondolence" Text="Leave a Condolence" CssClass="trigger-leave-condolence aLeaveCondolenceTigger" NavigateUrl="javascript:void(0);"></asp:HyperLink>
										</div>
									</asp:Panel>
                                    <div id="divLeaveCondolenceTitleRight" class="customRight">
                                    	<img src="/portals/_default/skins/obit/images/green-arrow.png" />
									</div>
                                    <div id="divLeaveCondolenceTitleFooter" class="customFooter"></div>
                                </div>
                                
                                <asp:Panel runat="server" ID="panLeaveCondolence" CssClass="leave-condolence-box divJustHidden">
                                    <asp:Panel runat="server" ID="panCondolenceSocialOption">
                                        <p class="smaller">
                                            <label>Conveniently connect with an existing account. We never post without your permission.</label>
                                        </p>
                                                                                
                                      	<ul class="connect-with">
                                            <li>                                                
                                                <asp:ImageButton runat="server" ID="ibFBLogin" OnClientClick="fblogin();return false;" ImageUrl="/Portals/_default/Skins/Obit/Images/Medium_127x37.png" AlternateText="Facebook Connect"></asp:ImageButton>
                                            </li>
                                            <li>
                                                <asp:ImageButton runat="server" ID="ibTwitterLogin" OnClick="ibTwitterLogin_Click" ImageUrl="/Portals/_default/Skins/Obit/Images/twitter-connect-button.jpg" AlternateText="Twitter Connect" Visible="false"></asp:ImageButton>
                                            </li>
                                            <%--<li>
                                                <script type="in/Login"></script>
                                                <asp:ImageButton runat="server" ID="ibLinkedinLogin" OnClientClick="" ImageUrl="/Portals/_default/Skins/Obit/Images/Medium_127x37.png" AlternateText="Linkedin Connect" Visible="false"></asp:ImageButton>
                                            </li>--%>
                                        </ul>
                                        <div class="clearfix"></div>

                                        <asp:Panel runat="server" ID="panOrSep" CssClass="or-separator">
                                            <label>or</label>
                                        </asp:Panel>
                                    </asp:Panel>
                                    
                                    <div class="divError">
                                        <asp:ValidationSummary ID="vsRegistration" runat="server" CssClass="vsRegistration" ValidationGroup="OPC" DisplayMode="List" HeaderText="Condolonce has been filled out incorrectly" />

                                        <asp:Label runat="server" ID="lblConError" Visible="false"></asp:Label>
                                    </div>
                                    
                                    <asp:Panel runat="server" ID="panConNameEmail" Visible="false">
                                        <div class="customContainer divLeaveCondolenceTextboxContainer">
                                            <div class="customLeft divLeaveCondolenceTextboxLeft">
                                                <div class="customContainer divFieldsContainer">
                                                    <div class="customLeft divFieldsLeft">
                                                        <label>Your Name</label>
                                                    </div>
                                                    <div class="customRight divFieldsRight divFieldsFH">
                                                        <asp:Label ID="lblConnectedName" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="customFooter divFieldsFooter"></div>
                                                </div>
                                            </div>
                                            <asp:Panel runat="server" ID="panConnectedEmail" CssClass="customRight divLeaveCondolenceTextboxRight">
                                                <div class="customContainer divFieldsContainer">
                                                    <div class="customLeft divFieldsLeft">
                                                        <label>Your Email</label>
                                                    </div>
                                                    <div class="customRight divFieldsRight divFieldsFH">
                                                        <asp:Label ID="lblConnectedEmail" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="customFooter divFieldsFooter"></div>
                                                </div>
                                            </asp:Panel>
                                            <div class="customFooter divLeaveCondolenceTextboxFooter"></div>
                                        </div>
                                        
                                        <asp:Label ID="lblConnectedUser" runat="server" Visible="false"></asp:Label>
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="panConEnterNameEmail">
                                        <div class="customContainer divLeaveCondolenceTextboxContainer">
                                            <div class="customLeft divLeaveCondolenceTextboxLeft">
                                                <asp:RequiredFieldValidator ID="rfvConName" runat="server" Text="*" ErrorMessage="Fill in the your name" Display="Static" ControlToValidate="txtConName" ValidationGroup="OPC" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <asp:TextBox runat="server" ID="txtConName" CssClass="text required" MaxLength="50" TabIndex="1" ValidationGroup="OPC" placeholder="Your Name *"></asp:TextBox>
                                            </div>
                                            <div class="customRight divLeaveCondolenceTextboxRight">
                                                <asp:RequiredFieldValidator ID="rfvConEMail" runat="server" Text="*" ErrorMessage="Fill in the your email address" Display="Static" ControlToValidate="txtConEMail" ValidationGroup="OPC" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator runat="server" ID="EmailValidator" ControlToValidate="txtConEMail" Text="*" ErrorMessage="Invalid email format" ValidationExpression="^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$" Display="Static" ValidationGroup="OPC" />
                                                <asp:TextBox runat="server" ID="txtConEMail" CssClass="text required" MaxLength="50" TabIndex="2" ValidationGroup="OPC" placeholder="Your Email Address *"></asp:TextBox>
                                            </div>
                                            <div class="customFooter divLeaveCondolenceTextboxFooter"></div>
                                        </div>
                                    </asp:Panel>
                                    
                                    <div id="divPrivateCondolonces">
                                        <div class="customContainer divSubmitAgreeContainer">
                                            <div class="customLeft divSubmitAgreeLeft">
                                                <asp:CheckBox runat="server" ID="chkPrivateCon"></asp:CheckBox>
                                            </div>
                                            <div class="customLeft divSubmitAgreeRight">
                                                <label>Make this a private condolence</label>
                                            </div>
                                            <asp:Panel runat="server" ID="panLogOutFB" CssClass="customLeft  divSubmitAgreeRight divSocialCondoloncesLogouts" Visible="false">
                                                <asp:ImageButton runat="server" ID="ibFBLogout" OnClientClick="fblogout();return false;" ImageUrl="/Portals/_default/Skins/Obit/Images/FacebookLogout.png" AlternateText="Facebook Logout"></asp:ImageButton>
                                            </asp:Panel>
                                            <asp:Panel runat="server" ID="panLogOutTwitter" CssClass="customLeft  divSubmitAgreeRight divSocialCondoloncesLogouts" Visible="false">
                                            </asp:Panel>
                                            
                                            <div class="customFooter divSubmitAgreeFooter"></div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="separator divConSeparator"></div>
                                    <div class="divCommentArea">
                                        <asp:RequiredFieldValidator ID="rfvConMessage" runat="server" Text="*" ErrorMessage="Fill in the your message" Display="Static" ControlToValidate="txtConMessage" ValidationGroup="OPC" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <asp:TextBox runat="server" ID="txtConMessage" TextMode="MultiLine" MaxLength="500" TabIndex="3" ValidationGroup="OPC" Rows="12" placeholder="Your Message *"></asp:TextBox>
                                    </div>
                                    <div class="divCondoloncesCaptcha">
                                        <BotDetect:Captcha ID="Captcha" runat="server" />
                                        <div id="divCaptchaValidation">
                                            <asp:TextBox ID="txtCaptchaCode" TabIndex="20" runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div id="divSaveCondolonces">
                                        <asp:Button ID="cmdSaveCondolonces" runat="server" Text="Submit" OnClick="cmdSaveCondolonces_Click" CssClass="green-button" ValidationGroup="OPC" TabIndex="4"></asp:Button>
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="green-button"></asp:Button>

                                        <!--a href="javascript:void(0);" class="green-button" tabindex="5" onclick="toggleLayer('<%= panLeaveCondolence.ClientID %>','','');">Cancel</a-->
                                    </div>
                                </asp:Panel>
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="panCondoloncesThankYou" Visible="false">
                            <div class="divCondoloncesThankYou divSignUpBody">
                                <label><strong><asp:Literal runat="server" ID="litThankYou" Text="Your condolence has been added and a copy has been sent to your email address"></asp:Literal></strong></label>
                            </div>
                        </asp:Panel>

                        <div id="condolonces" class="page-content nobg">
                        	<div align="center">
                       			<label class="lblFontSize24" id="gothicObitPrev4"><asp:Label runat="server" ID="lblNumberOfCondolences"></asp:Label><asp:Label runat="server" ID="lblNoOfCondolences" Text=" Condolences"></asp:Label></label>
							</div>
                        
                        	<div class="customContainer divObiturayCondoloncesPagingContainer">
                                <div class="customLeft divObiturayCondoloncesPagingLeft">
       		                        <%--<label class="lblFontSize24" id="gothicObitPrev4"><asp:Label runat="server" ID="lblNumberOfCondolences"></asp:Label><asp:Label runat="server" ID="lblNoOfCondolences" Text=" Condolences"></asp:Label></label>--%>
                                </div>
                                <div class="customRight divObiturayCondoloncesPagingRight">
                                    <asp:Panel ID="PagerPanel" runat="server">
                                        <div id="divPager">
                                            <asp:Repeater ID="PagerControls" runat="server" OnItemCommand="PagerControls_ItemCommand">
                                                <ItemTemplate>
                                                    <a class='<%#Eval("TagClass")%>' href='<%#Eval("NavigateUrl")%>'><%#Eval("Text")%></a>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class="customFooter divObiturayCondoloncesPagingFooter"></div>
                            </div>
                                                    	                            
                            <asp:DataList runat="server" ID="dlCondolence" OnItemDataBound="dlCondolence_ItemDataBound" RepeatLayout="Flow">
                                <ItemTemplate>
                                    <div class="divCondolence">
                                        <div class="divConMessage" align="center">
                                            <div class="divConMessageText">
                                                <label>&quot;<asp:Label ID="lblConMessage" runat="server" Text='<%#Eval("ObituaryCondolenceMessage") %>'></asp:Label>&quot;</label>
                                            </div>
                                        </div>
                                        <div class="customContainer divCondoloncesUserDetailsContainer" align="center">
                                            <div class="customLeft divCondoloncesUserDetailsLeft">
                                                <div class="divCondoloncesUserDetailsText">
                                                    <label>
                                                        <asp:Label ID="lblConName" runat="server" Text='<%#Eval("ObituaryCondolenceName") %>'></asp:Label></label>
                                                </div>
                                            </div>
                                            <div class="customMiddle divCondoloncesUserDetailsMiddle">
                                                <asp:Image runat="server" ID="imgConUser" AlternateText='<%#Eval("ObituaryCondolenceSocialUserName") %>'></asp:Image>
                                            </div>
                                            <div class="customRight divCondoloncesUserDetailsRight">
                                                <div class="divCondoloncesUserDetailsText">
                                                    <label>
                                                        <asp:Label ID="lblConCreated" runat="server" Text='<%#Eval("ObituaryCondolenceCreatedDate") %>'></asp:Label></label>
                                                </div>
                                            </div>
                                            <div class="customFooter divCondoloncesUserDetailsFooter"></div>
                                        </div>
                                    </div>
                                    <asp:Label ID="lblSocialNetwork" runat="server" Visible="false" Text='<%#Eval("ObituaryCondolenceSocial") %>'></asp:Label>
                                    <asp:Label ID="lblUserID" runat="server" Visible="false" Text='<%#Eval("UserID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:DataList>
                            
							<div class="customContainer divObiturayCondoloncesPagingContainer">
                                <div class="customLeft divObiturayCondoloncesPagingLeft">&nbsp;</div>
                                <div class="customRight divObiturayCondoloncesPagingRight">
                                    <asp:Panel ID="PagerPanel2" runat="server">
                                        <div id="divPager">
                                            <asp:Repeater ID="PagerControls2" runat="server" OnItemCommand="PagerControls_ItemCommand">
                                                <ItemTemplate>
                                                    <a class='<%#Eval("TagClass")%>' href='<%#Eval("NavigateUrl")%>'><%#Eval("Text")%></a>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class="customFooter divObiturayCondoloncesPagingFooter"></div>
                            </div>
                        </div>
                    </asp:Panel>
                <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
        </div>
        <!-- /#main-content -->

        <!-- sidebar -->
        <div id="sidebar" class="span4">
            <asp:Panel runat="server" ID="panObituaryServices" runat="server">
                <div id="services-widget" class="widget widget-white divWidgetServices">
                    <h4 class="scroll-hide h4Services" id="gothicObitPrev5">
                    	<label>Services</label>
					</h4>
                    <div id="divObiturayDetailsService">
                        <asp:Literal ID="litObituaryServices" runat="server"></asp:Literal>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="panSidebarLinks">
                <div id="obituary-widget" class="widget">
                    <ul id="obituary-collapse" class="accordion obituary-options">
						<asp:Panel runat="server" ID="panSectionFlowers">
                            <li class="accordion-group">
                                <a id="toggleFlowers-box" class="flowers" href="javascript:void(0);" onclick="if(getDocID('<%= panFlowers.ClientID %>').style.display == ''){classToggleLayer(getDocID('obituary-collapse'),getDocID('<%= panFlowers.ClientID %>'),'accordion-body divJustHidden','div');}else{getDocID('<%= panFlowers.ClientID %>').style.display = '';}">Send Flowers</a>
                                
                                <asp:Panel runat="server" ID="panFlowers" CssClass="accordion-body divJustHidden">
                                    <div class="accordion-inner">
                                        <asp:Panel runat="server" ID="panFlowersFH" Visible="false">
                                            <label>Send flowers to the <strong>Funeral Home</strong> <asp:Label runat="server" ID="lblFHFlowersName"></asp:Label></label>
                                            <asp:HyperLink runat="server" ID="hlFlowerFH" NavigateUrl="/Obituaries/flower.aspx?person=0&FHPID=" CssClass="button" Text="Select &amp; Send Flowers"></asp:HyperLink>
                                        </asp:Panel>
                                        <div class="br_12"></div>
                                        <asp:DataList runat="server" ID="dlFlowerRecipient" RepeatLayout="Flow">
                                            <ItemTemplate>
                                                <label>Send flowers to <strong><asp:Label ID="lblFlowerName" runat="server" Text='<%# Eval("FirstName") + " " + Eval("LastName") %>'></asp:Label></strong></label>
                                                <br/>
                                                <a href='<%# "/Obituaries/flower.aspx?person=1&FHPID=" + Eval("Id") + "&oid=" + Eval("ObituaryID") %>' class="button">Select &amp; Send Flowers</a>
                                                <div class="br_12"></div>
                                            </ItemTemplate>
                                        </asp:DataList>
                                        
                                        <asp:Panel runat="server" ID="panFlowersOr" CssClass="divOrSendAnotherAddress">
                                        	<label>or</label>
										</asp:Panel>
                                        <asp:HyperLink runat="server" ID="hlSendFlowersAnotherAddress" Text="Alternative Address" CssClass="button"></asp:HyperLink><%--Send flowers to someone else--%>
                                    </div>
                                </asp:Panel>
	                        </li>
						</asp:Panel>
                        <asp:Panel runat="server" ID="panSectionDonation">
                            <li class="accordion-group">
                                <asp:HyperLink ID="lnkMakeDonation" runat="server" Text="Make a Donation" class="donation"></asp:HyperLink>
                            </li>
						</asp:Panel>
                        <asp:Panel runat="server" ID="panSectionDonationCanadaHelp" Visible="false">
                            <li class="accordion-group">
                                <a id="toggleMakeDonationbox" href="javascript:void(0);" class="donation" onclick="if(getDocID('<%= panMakeDonation.ClientID %>').style.display == ''){classToggleLayer(getDocID('obituary-collapse'),getDocID('<%= panMakeDonation.ClientID %>'),'accordion-body divJustHidden','div');}else{getDocID('<%= panMakeDonation.ClientID %>').style.display = '';}">Make a Donation</a>
                                <asp:Panel runat="server" ID="panMakeDonation" CssClass="accordion-body divJustHidden">
                                    <div class="accordion-inner">
                                        <p>
                                            There is no charities assigned to this announcement. <a href='http://CanadaHelps.org' target='_blank'>Please click here to donate.</a>
                                        </p>
                                    </div>
                                </asp:Panel>
                            </li>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="panSectionCondolence">
                            <li class="accordion-group">
                            	<asp:HyperLink runat="server" ID="hlSectionCondolence" Text="Leave a Condolence" CssClass="condolence slidetocondolence" NavigateUrl="javascript:void(0);"></asp:HyperLink>
                            </li>
						</asp:Panel>
						<asp:Panel runat="server" ID="panSectionCard">
	                        <li class="accordion-group">
                                <a id="toggleSendCard-box" href="javascript:void(0);" class="card" onclick="if(getDocID('<%= panCard.ClientID %>').style.display == ''){classToggleLayer(getDocID('obituary-collapse'),getDocID('<%= panCard.ClientID %>'),'accordion-body divJustHidden','div');}else{getDocID('<%= panCard.ClientID %>').style.display = '';}">Send a Card</a>
                                
                                <asp:Panel runat="server" ID="panCard" CssClass="accordion-body divJustHidden">
                                    <div class="accordion-inner">
                                        <label>Send a customized, printed card to the family, Funeral Home or other address.</label>
                                        <div class="br_12"></div>
                                        <asp:HyperLink ID="lnkSendCardToFuneralHome" runat="server" Text="Send a Card To Funeral Home" CssClass="button" NavigateUrl="/Obituaries/sympathycards.aspx"></asp:HyperLink>
                                        <div class="br_12"></div>
                                        <asp:Repeater ID="rpCardReceiver" runat="server">
                                            <ItemTemplate>
                                                <label>Send a card to <strong><asp:Label ID="lblReceiverName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FullName")%>'></asp:Label></strong></label>
                                                <br/>
                                                <asp:HyperLink ID="lnkSendCard" runat="server" Text="Send a Card" CssClass="button" NavigateUrl='<%# "/Obituaries/sympathycards.aspx?ObituariesID=" + DataBinder.Eval(Container.DataItem, "ObituaryID") + "&CardReceiverId=" + DataBinder.Eval(Container.DataItem, "Id") %>'></asp:HyperLink>
                                                <div class="br_12"></div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        
                                        <asp:Panel runat="server" ID="panCardOr" CssClass="divOrSendAnotherAddress">
                                        	<label>or</label>
										</asp:Panel>
                                        <asp:HyperLink runat="server" ID="hlSendCardsAnotherAddress" Text="Alternative Address" CssClass="button"></asp:HyperLink><%--Send a card to someone else--%>
                                    </div>
                                </asp:Panel>
	                        </li>
						</asp:Panel>
                       	<asp:Panel runat="server" ID="panSectionShare">
							<li class="accordion-group">
                                <a id="toggleShare-box" class="shareobituary" href="javascript:void(0);" onclick="if(getDocID('<%= panShare.ClientID %>').style.display == ''){classToggleLayer(getDocID('obituary-collapse'),getDocID('<%= panShare.ClientID %>'),'accordion-body divJustHidden','div');}else{getDocID('<%= panShare.ClientID %>').style.display = '';}">Share</a>
                                
                                <asp:Panel runat="server" ID="panShare" CssClass="accordion-body divJustHidden">
                                    <div class="accordion-inner">
                                        <label>Make sure your friends and family know about <asp:Label runat="server" ID="lblShareForName"></asp:Label>'s passing and funeral services:</label>
                                        <br /><br />
                                        <asp:Panel runat="server" ID="panShareEmailThankYou" Visible="false">
                                            <div class="divSignUpThankYouBody divSignUpBody">
                                                <label><strong><asp:Literal runat="server" ID="litShareEmailThankYou" Text="Your email has been sent"></asp:Literal></strong></label>
                                            </div>
                                            <br />
                                        </asp:Panel>
                                        <asp:Panel runat="server" ID="panShareEmailBody">
                                            <div class="email-fields">
                                                <asp:TextBox ID="txtShareEmail1" runat="server" CssClass="email-field" placeholder="Email Address"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="revShareEmail1" runat="server" Text="*" ErrorMessage="Invalid email format for email 1" ControlToValidate="txtShareEmail1" ValidationExpression="^[\w-\.]{1,}\@([\da-zA-Z-]{1,}\.){1,}[\da-zA-Z-]{2,6}$" Display="Static" ValidationGroup="validateShareEmail"></asp:RegularExpressionValidator>
                                                
                                                <asp:TextBox ID="txtShareEmail2" runat="server" CssClass="email-field" placeholder="Email Address"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="revShareEmail2" runat="server" Text="*" ErrorMessage="Invalid email format for email 2" ControlToValidate="txtShareEmail2" ValidationExpression="^[\w-\.]{1,}\@([\da-zA-Z-]{1,}\.){1,}[\da-zA-Z-]{2,6}$" Display="Static" ValidationGroup="validateShareEmail"></asp:RegularExpressionValidator>
                                                
                                                <asp:TextBox ID="txtShareEmail3" runat="server" CssClass="email-field" placeholder="Email Address"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="revShareEmail3" runat="server" Text="*" ErrorMessage="Invalid email format for email 3" ControlToValidate="txtShareEmail3" ValidationExpression="^[\w-\.]{1,}\@([\da-zA-Z-]{1,}\.){1,}[\da-zA-Z-]{2,6}$" Display="Static" ValidationGroup="validateShareEmail"></asp:RegularExpressionValidator>
                                                <!--a href="javascript:void(0);" class="add" id="add-emails">Add another recipient</a-->
                                            </div>
                                            
                                            <div class="divError"> 
                                                <asp:ValidationSummary ID="vsShareEmail" runat="server" CssClass="vsRegistration" ValidationGroup="validateShareEmail" DisplayMode="List" />
                                                    
                                                <asp:Label runat="server" ID="lblShareEmailError"></asp:Label>
                                            </div>
                                            
                                            <div class="send-mail-btn">
                                                <asp:Button ID="btnShareEmail" runat="server" Text="Send Now" CssClass="mail-btn" OnClick="btnShareEmail_Click" ValidationGroup="validateShareEmail"></asp:Button>
                                            </div>
                                        </asp:Panel>
                                        
                                        <div id="divObiturayDetailsShare">
                                            <label>Share on</label>
                                        </div>
                                        <div class="customContainer divObiturayDetailsShareContainer">
                                            <div class="customLeft divObiturayDetailsShareLeft">
                                                <asp:Literal runat="server" ID="litFB"></asp:Literal>
                                            </div>
                                            <div class="customMiddle divObiturayDetailsShareMiddle">
                                                <asp:HyperLink runat="server" ID="hlShareTwiiter" Target="_blank" Text="Twitter" CssClass="aTwitterShare"></asp:HyperLink>
                                            </div>
                                            <div class="customRight divObiturayDetailsShareRight">
                                                <asp:Literal ID="ltlLinkin" runat="server"></asp:Literal>
                                            </div>
                                            <div class="customFooter divObiturayDetailsShareFooter"></div>
                                        </div>
                                    </div>
                                </asp:Panel>
	                        </li>
						</asp:Panel>
                        <asp:Panel runat="server" ID="panSectionReminders">
                            <li class="accordion-group">
                                <a id="toggleReminders-box" class="reminders" href="javascript:void(0);" onclick="if(getDocID('<%= panReminders.ClientID %>').style.display == ''){classToggleLayer(getDocID('obituary-collapse'),getDocID('<%= panReminders.ClientID %>'),'accordion-body divJustHidden','div');}else{getDocID('<%= panReminders.ClientID %>').style.display = '';}">Get Reminders</a>
                                
                                <asp:Panel runat="server" ID="panReminders" CssClass="accordion-body divJustHidden">
                                    <asp:Panel runat="server" ID="panReminderBody" CssClass="accordion-inner">
                                        <p>
                                            <label>Get notified of updates, anniversary dates or all announcements from this Funeral Home:</label>
                                        </p>
                                        <asp:CheckBox runat="server" ID="chkReminderServices" CssClass="prettycheckbox" Checked="true"></asp:CheckBox>
                                        <label for="update-services">Updates to Services dates, times or location. </label>
                                        <br />
                                        <asp:CheckBox runat="server" ID="chkReminderAniversary" CssClass="prettycheckbox" Checked="true"></asp:CheckBox>
                                        <label for="update-aniversary">Anniversary reminder of <strong><asp:Label runat="server" ID="lblReminderForName"></asp:Label></strong> date of passing</label>
                                        <br />
                                        <asp:CheckBox runat="server" ID="chkReminderBirthdate" CssClass="prettycheckbox" Checked="true"></asp:CheckBox>
                                        <label for="update-birthdate">Annual reminder of the date of birth of <strong><asp:Label runat="server" ID="lblReminderForName2"></asp:Label></strong></label>
                                        <br />
                                        <asp:CheckBox runat="server" ID="chkFuneralHome" CssClass="prettycheckbox" Checked="true" Text=" All announcements from Funeral Home"></asp:CheckBox>
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
                                    <asp:Panel runat="server" ID="panReminderThankYou" Visible="false">
                                        <div class="divSignUpThankYouBody divSignUpBody">
                                            <label>
                                                <strong>
                                                    <asp:Literal runat="server" ID="litReminderThankYou" Text="You are subscribed to receive reminders"></asp:Literal></strong>
											</label>
                                        </div>
                                    </asp:Panel>
                                </asp:Panel>
                            </li>
						</asp:Panel>

                        <asp:Panel runat="server" ID="panSectionBookmark">
                            <li class="accordion-group">
                                <a id="toggleBookmark-box" class="reminders" href="javascript:void(0);" onclick="if(getDocID('<%= panBookmark.ClientID %>').style.display == ''){classToggleLayer(getDocID('obituary-collapse'),getDocID('<%= panBookmark.ClientID %>'),'accordion-body divJustHidden','div');}else{getDocID('<%= panBookmark.ClientID %>').style.display = '';}">Book Mark</a>
                                <asp:Panel runat="server" ID="panBookmark" CssClass="accordion-body divJustHidden">
                                    <asp:Panel runat="server" ID="panBookmarkBody" CssClass="accordion-inner">
                                        <p>
                                            <label>Please click this button to book mark this page.</label>
                                        </p>
                                        <asp:Label ID="lblBookmarkError" runat="server" Visible="false"></asp:Label>                                        
                                        <div class="send-mail-btn">
                                            <asp:Button ID="btnBookmark" runat="server" Text="Book Mark" OnClick="btnBookmark_Click" class="mail-btn"></asp:Button>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="panBookmarkThankYou" Visible="false">
                                        <div class="divSignUpThankYouBody divSignUpBody">
                                            <label>
                                                <strong>
                                                    <asp:Literal runat="server" ID="litBookmarkThankYou" Text="You have book marked this page."></asp:Literal></strong>
											</label>
                                        </div>
                                    </asp:Panel>
                                </asp:Panel>
                            </li>
						</asp:Panel>
                    </ul>
                </div>
            </asp:Panel>
            <asp:Image ID="imgPreviewAccordion" runat="server" src="/portals/_default/skins/obit/images/ob-previewAccordion.jpg" />
        </div>
        <!-- /#sidebar -->
		
    </div>
</div>

<asp:HiddenField ID="HiddenPageIndex" runat="server" Value="0" />
<asp:HiddenField ID="hfObituaryID" runat="server" Value="0" />
<asp:HiddenField ID="hfObituaryCondolenceSocialNetwork" runat="server" Value="0" />
<asp:HiddenField ID="hfObituaryCreatorEmail" runat="server" Value="" />

<!-- Placed at the end of the document so the pages load faster -->

<%--
<script src='http://maps.googleapis.com/maps/api/js?sensor=false' type='text/javascript'></script>
<script src="/Portals/_default/Skins/Obit/JS/Master.js" type="text/javascript"></script>--%>
<script src="/Portals/_default/Skins/Obit/JS/cufon-yui.js" type="text/javascript"></script>
<script src="/Portals/_default/Skins/Obit/JS/Trade_Gothic_20.font.js" type="text/javascript"></script>
<%--<script src="/Portals/_default/Skins/Obit/JS/jquery.atooltip.js"></script>
<script src="/Portals/_default/Skins/Obit/JS/learn.js"></script>
<script src="/Portals/_default/Skins/Obit/js/bootstrap.min.js"></script>
<script src="/Portals/_default/Skins/Obit/js/ui/jquery.ui.core.js"></script>
<script src="/Portals/_default/Skins/Obit/js/ui/jquery.ui.widget.js"></script>
<script src="/Portals/_default/Skins/Obit/js/ui/jquery.ui.position.js"></script>
<script src="/Portals/_default/Skins/Obit/js/ui/jquery.ui.autocomplete.js"></script>
<script src="/Portals/_default/Skins/Obit/js/ui/jquery.ui.progressbar.js"></script>
<script src="/Portals/_default/Skins/Obit/js/jquery.validate.min.js"></script>
<script src="/Portals/_default/Skins/Obit/js/plugins.js"></script>
<script src="/Portals/_default/Skins/Obit/js/app.js"></script>--%>

<script type="text/javascript">
    Cufon.replace('#gothicObitPrev1', { fontWeight: 'normal' });
    Cufon.replace('#gothicObitPrev2', { fontWeight: 'normal' });
    Cufon.replace('#gothicObitPrev3', { fontWeight: 'normal' });
    Cufon.replace('#gothicObitPrev4', { fontWeight: 'normal' });
    Cufon.replace('#gothicObitPrev5', { fontWeight: 'normal' });
    Cufon.replace('#gothicObitPrev6', { fontWeight: 'normal' });
    Cufon.replace('#gothicObitPrev7', { fontWeight: 'normal' });
    Cufon.replace('#gothicObitPrev8', { fontWeight: 'normal' });
    Cufon.replace('#gothicObitPrev9', { fontWeight: 'normal' });
    Cufon.replace('#gothicObitPrev10', { fontWeight: 'normal' });
</script>