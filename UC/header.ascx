<%@ Control Language="C#" AutoEventWireup="True" CodeFile="header.ascx.cs" ClassName="header" Inherits="header" %>

<link rel="stylesheet" type="text/css" href="/Portals/_default/Skins/Obit/css/bootstrap.css">
<link rel="stylesheet" type="text/css" href="/Portals/_default/Skins/Obit/css/styles.css">
<link rel="stylesheet" type="text/css" href="/Portals/_default/Skins/Obit/css/atooltip.css">
<link rel="stylesheet" type="text/css" href="/Portals/_default/Skins/Obit/css/learn.css" media="all" />

<link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="stylesheet" type="text/css"/>

<link rel="shortcut icon" href="/O-favicon.ico" />

<div id="divGrayBG" class="divBasicHiddlenBackground"></div>
<div align="center">
	<div class="OB-container" align="left">
        <div class="OB-container-Header" id="divHeader" align="center">
            <div class="OB-header">
                <div class="OB-socialmedia">
                    <div class="OB-customLeft OB-ss-text">
                        <asp:Literal ID="litSocial" runat="server"></asp:Literal>
                    </div>
                    <div>
                        <div class="OB-customLeft OB-ss-icon">
                            <asp:HyperLink ID="lnkSignIn" runat="server" ImageUrl="/Portals/_default/Skins/Obit/Images/ob-header-signin-icon.jpg"></asp:HyperLink>
                        </div>
						<div class="OB-customLeft OB-ss-text">
                        	<asp:HyperLink runat="server" ID="hlAccount" NavigateUrl="/SignUp.aspx" Text="Sign Up"></asp:HyperLink>
						</div>
						<div class="OB-customLeft OB-ss-icon">
                        	<img src="/Portals/_default/Skins/Obit/Images/ob-header-signup-icon.jpg" />
						</div>
						<div class="OB-customLeft OB-ss-text">
                        	<asp:HyperLink runat="server" ID="hlLog" NavigateUrl="/MyAccount/Login.aspx" Text="Login"></asp:HyperLink>
						</div>
						<div class="OB-customLeft OB-ss-icon">
                        	<img src="/Portals/_default/Skins/Obit/Images/ob-header-newsletter-icon.jpg" />
                        </div>
						<div class="OB-customLeft OB-ss-text">
                        	<a href="/learn/aboutus/contactus.aspx">Contact</a>
						</div>
						<div class="OB-customLeft OB-ss-icon">
                        	<img src="/Portals/_default/Skins/Obit/Images/ob-header-socialmedia.jpg" usemap="#Map2" />
							<map name="Map2" id="Map2"><area shape="rect" coords="0,0,15,30" href="http://www.facebook.com/theobituaries" target="_blank" />
                            <area shape="rect" coords="17,0,47,30" href="https://twitter.com/theobituariesca" target="_blank" />
                            <area shape="rect" coords="48,0,69,30" href="http://www.linkedin.com/company/the-obituaries-inc-" target="_blank" />
                            </map>
						</div>
						<div class="OB-clear"></div>
					</div>              
                </div>
                <div>
                <header class="navbar" id="headerNav">
                    <div class="navbar-inner">
                        <div class="container OB-inner-container">
                            <div class="OB-logo">
                                <a href="/Default.aspx"><img src="/Portals/_default/Skins/Obit/Images/ob-header-logo.png" /></a>
                            </div>
                            <div class="nav-collapse"> 
                            	<div class="dropdown-menu" id="divMenuLearn" onMouseOver="classActiveToggleLayer(getDocID('headerNav'),getDocID('divMenuLearn'),getDocID('divGlassBG'),'','dropdown-menu','div');classToggleLayerChangeClass(getDocID('mainnav'),getDocID('liMenuLearn'),'liActiveMenu','dropdown','li');" onMouseOut="classActiveToggleLayer(getDocID('headerNav'),getDocID('divMenuLearn'),getDocID('divGlassBG'),'','dropdown-menu','div');classToggleLayerChangeClass(getDocID('mainnav'),getDocID('liMenuLearn'),'dropdown','dropdown','li');">
                                    <div class="OB-menu-item" align="center">
                                        <div>
                                            <asp:HyperLink ID="lnkOverview" runat="server" ImageUrl="/Portals/_default/Skins/Obit/Images/ob-icon-over2-off.png" NavigateUrl="/learn/overview.aspx"></asp:HyperLink>
                                        </div>
                                        <div>
                                            <a href="/learn/overview.aspx" id="gothicHeader24">Overview</a><br />
                                        </div>
                                    </div>
                                    <div class="OB-menu-divider"></div>						
                                    <div class="OB-menu-item">
                                        <div>
                                            <asp:HyperLink ID="lnkProdDetails" runat="server" ImageUrl="/Portals/_default/Skins/Obit/Images/ob-icon-over-off.png" NavigateUrl="/learn/productdetails.aspx"></asp:HyperLink>
                                        </div>
                                        <div>
                                            <a href="/learn/productdetails.aspx" id="gothicHeader23">Product<br />Details</a>
                                        </div>
                                    </div>
                                    <div class="OB-menu-divider"></div>
                                    <div class="OB-menu-item" align="center">
                                        <div>
                                           <asp:HyperLink ID="lnkCompare" runat="server" ImageUrl="/Portals/_default/Skins/Obit/Images/ob-icon-comp-off.png" NavigateUrl="/learn/comparingexperiences.aspx"></asp:HyperLink>
                                        </div>
                                        <div>
                                            <a href="/learn/comparingexperiences.aspx" id="gothicHeader22">Compare</a><br />
                                        </div>
                                    </div>
                                    <div class="OB-menu-divider"></div>
                                    <div class="OB-menu-item" align="center">
                                        <div>
                                            <asp:HyperLink ID="lnkPricing" runat="server" ImageUrl="/Portals/_default/Skins/Obit/Images/ob-icon-pricing-off.png" NavigateUrl="/learn/pricing.aspx"></asp:HyperLink>
                                        </div>
                                        <div>
                                            <a href="/learn/pricing.aspx" id="gothicHeader21">Pricing<br /><br /></a>
                                        </div>
                                    </div>
                                    <div class="OB-menu-divider"></div>  
                                    <div class="OB-menu-item" align="center">
                                        <div>
                                            <asp:HyperLink ID="lnkFH" runat="server" ImageUrl="/Portals/_default/Skins/Obit/Images/ob-icon-fh-off.png" NavigateUrl="/learn/funeralhomes.aspx"></asp:HyperLink>
                                        </div>
                                        <div>
                                            <a href="/learn/funeralhomes.aspx" id="gothicHeader20">Funeral<br />Homes</a>
                                        </div>
                                    </div>      
                                    <div class="OB-clear"></div>
                                </div>             
                                <div class="dropdown-menu" id="divMenuCreate" onMouseOver="classActiveToggleLayer(getDocID('headerNav'),getDocID('divMenuCreate'),getDocID('divGlassBG'),'','dropdown-menu','div');classToggleLayerChangeClass(getDocID('mainnav'),getDocID('liMenuCreate'),'liActiveMenu','dropdown','li');" onMouseOut="classActiveToggleLayer(getDocID('headerNav'),getDocID('divMenuCreate'),getDocID('divGlassBG'),'','dropdown-menu','div');classToggleLayerChangeClass(getDocID('mainnav'),getDocID('liMenuCreate'),'dropdown','dropdown','li');">
                                    <div class="OB-menu-item">
                                        <asp:Panel runat="server" ID="panCreObUser" Visible="false" CssClass="divHeaderUser">
                                            <div>
                                                <a href="/create/obituary.aspx"><img alt="Create an Obituary" src="/Portals/_default/Skins/Obit/Images/ob-icon-creOb-off.png" id="imgObituary" onMouseOut="changeImage('imgObituary','/Portals/_default/Skins/Obit/Images/ob-icon-creOb-off.png');" onMouseOver="changeImage('imgObituary','/Portals/_default/Skins/Obit/Images/ob-icon-creOb-on.png');" /></a>
                                            </div>
                                            <div>
                                                <a href="/create/obituary.aspx" id="gothicHeader1">Create an<br />Obituary</a>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel runat="server" ID="panCreObNonUser" CssClass="divHeaderNonUser">
                                            <div>
                                                <a href="/MyAccount/Login.aspx?url=/create/obituary.aspx"><img alt="Create an Obituary" src="/Portals/_default/Skins/Obit/Images/ob-icon-creOb-off.png" /></a>
                                            </div>
                                            <div>
                                                <!--span id="gothicHeader9">Create an<br />Obituary</span-->
                                                <a href="/MyAccount/Login.aspx?url=/create/obituary.aspx" id="gothicHeader1">Create an<br />Obituary</a>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div class="OB-menu-divider"></div>
                                    <div class="OB-menu-item">
                                        <asp:Panel runat="server" ID="panCreMemUser" Visible="false" CssClass="divHeaderUser">
                                            <div>
                                                <a href="/create/Memorial.aspx"><img alt="Create a Memorial" src="/Portals/_default/Skins/Obit/Images/ob-icon-mem-off.png" id="imgMemorial" onMouseOut="changeImage('imgMemorial','/Portals/_default/Skins/Obit/Images/ob-icon-mem-off.png');" onMouseOver="changeImage('imgMemorial','/Portals/_default/Skins/Obit/Images/ob-icon-mem-on.png');" /></a>
                                            </div>
                                            <div>
                                                <a href="/create/Memorial.aspx" id="gothicHeader2">Create a<br />Memorial</a>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel runat="server" ID="panCreMemNonUser" CssClass="divHeaderNonUser">
                                            <div>
                                                <a href="/MyAccount/Login.aspx?url=/create/Memorial.aspx"><img alt="Create a Memorial" src="/Portals/_default/Skins/Obit/Images/ob-icon-mem-off.png" /></a>
                                            </div>
                                            <div>
                                                <!--span id="gothicHeader10">Create a<br />Memorial</span-->
                                                <a href="/MyAccount/Login.aspx?url=/create/Memorial.aspx" id="gothicHeader2">Create a<br />Memorial</a>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                  
                                    <div class="OB-menu-divider"></div>
                                    <div class="OB-menu-item">
                                        <asp:Panel runat="server" ID="panPreplanUser" Visible="false" CssClass="divHeaderUser">
                                            <div>
                                                <a href="/create/PrePlan.aspx"><img alt="PrePlan Obituaries" src="/Portals/_default/Skins/Obit/Images/ob-icon-preplan-off.png" id="imgPrePlan" onMouseOut="changeImage('imgPrePlan','/Portals/_default/Skins/Obit/Images/ob-icon-preplan-off.png');" onMouseOver="changeImage('imgPrePlan','/Portals/_default/Skins/Obit/Images/ob-icon-preplan-on.png');" /></a>
                                            </div>
                                            <div>
                                                <a href="/create/PrePlan.aspx" id="gothicHeader4">PrePlan<br />Obituaries</a>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel runat="server" ID="panPreplanNonUser" CssClass="divHeaderNonUser">
                                            <div>
                                               <a href="/MyAccount/Login.aspx?url=/create/PrePlan.aspx"><img alt="PrePlan Obituaries" src="/Portals/_default/Skins/Obit/Images/ob-icon-preplan-off.png" /></a>
                                            </div>
                                            <div>
                                                <a href="/MyAccount/Login.aspx?url=/create/PrePlan.aspx" id="gothicHeader4">PrePlan<br />Obituaries</a>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                      <div class="OB-menu-divider"></div>
                                    <div class="OB-menu-item">
                                        <asp:Panel runat="server" ID="panRegUser" Visible="false" CssClass="divHeaderUser">
                                            <div>
                                                 <img alt="Quick Registration" src="/Portals/_default/Skins/Obit/Images/ob-icon-reg-on.png" />
                                            </div>
                                            <div>
                                                <span id="gothicHeader19">Quick<br />Registration</span>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel runat="server" ID="panRegNonUser" CssClass="divHeaderNonUser">
                                            <div>
                                                <img alt="Quick Registration" src="/Portals/_default/Skins/Obit/Images/ob-icon-reg-on.png" />
                                            </div>
                                            <div>
                                                <span id="gothicHeader11">Quick<br />Registration</span>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div class="OB-menu-divider"></div>
                                    <div class="OB-menu-item">
                                        <asp:Panel runat="server" ID="panNotifUser" Visible="false" CssClass="divHeaderUser">
                                            <div>
                                                <img alt="Subscribe for Notifications" src="/Portals/_default/Skins/Obit/Images/ob-icon-notif-on.png" />
                                            </div>
                                            <div>
                                                <span id="gothicHeader18">Subscribe for<br />Notifications</span>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel runat="server" ID="panNotifNonUser" CssClass="divHeaderNonUser">
                                            <div>
                                                <img alt="Subscribe for Notifications" src="/Portals/_default/Skins/Obit/Images/ob-icon-notif-on.png" />
                                            </div>
                                            <div>
                                                <span id="gothicHeader13">Subscribe for<br />Notifications</span>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div class="OB-clear"></div>
                                </div>
                                <div class="dropdown-menu" id="divMenuResources" onMouseOver="classActiveToggleLayer(getDocID('headerNav'),getDocID('divMenuResources'),getDocID('divGlassBG'),'','dropdown-menu','div');classToggleLayerChangeClass(getDocID('mainnav'),getDocID('liMenuResources'),'liActiveMenu','dropdown','li');" onMouseOut="classActiveToggleLayer(getDocID('headerNav'),getDocID('divMenuResources'),getDocID('divGlassBG'),'','dropdown-menu','div');classToggleLayerChangeClass(getDocID('mainnav'),getDocID('liMenuResources'),'dropdown','dropdown','li');">
                                    <div class="OB-menu-item">
                                        <asp:Panel runat="server" ID="panPlanUser" Visible="false" CssClass="divHeaderUser">
                                            <div>
                                                <img alt="Plan" src="/Portals/_default/Skins/Obit/Images/ob-icon-plan-on.png" />
                                            </div>
                                            <div>
                                                <span id="gothicHeader14">Plan</span>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel runat="server" ID="panPlanNonUser" CssClass="divHeaderNonUser">
                                            <div>
                                                <img alt="Plan" src="/Portals/_default/Skins/Obit/Images/ob-icon-plan-on.png" />
                                            </div>
                                            <div>
                                                <span id="gothicHeader14">Plan</span>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div class="OB-menu-divider"></div>
                                    <div class="OB-menu-item">
                                        <asp:Panel runat="server" ID="panGiveUser" Visible="false" CssClass="divHeaderUser">
                                            <div>
                                                <img alt="Give" src="/Portals/_default/Skins/Obit/Images/ob-icon-give-on.png" />
                                            </div>
                                            <div>
                                                <span  id="gothicHeader15">Give</span>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel runat="server" ID="panGiveNonUser" CssClass="divHeaderNonUser">
                                            <div>
                                                <img alt="Give" src="/Portals/_default/Skins/Obit/Images/ob-icon-give-on.png" />
                                            </div>
                                            <div>
                                                <span  id="gothicHeader15">Give</span>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div class="OB-menu-divider"></div>
                                    <div class="OB-menu-item">
                                        <asp:Panel runat="server" ID="panSupportUser" Visible="false" CssClass="divHeaderUser">
                                            <div>
                                                <img alt="Support" src="/Portals/_default/Skins/Obit/Images/ob-icon-supp-on.png" />
                                            </div>
                                            <div>
                                                <span id="gothicHeader16">Support</span>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel runat="server" ID="panSupportNonUser" CssClass="divHeaderNonUser">
                                            <div>
                                                <img alt="Support" src="/Portals/_default/Skins/Obit/Images/ob-icon-supp-on.png" />
                                            </div>
                                            <div>
                                                <span id="gothicHeader16">Support</span>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                   	<div class="OB-menu-divider"></div>  
                                    <div class="OB-menu-item" align="center">
                                        <div>
                                            <asp:HyperLink ID="lnkAbout" runat="server" ImageUrl="/Portals/_default/Skins/Obit/Images/ob-icon-about-off.png" NavigateUrl="/learn/aboutus.aspx"></asp:HyperLink>
                                        </div>
                                        <div>
                                            <a href="/learn/aboutus.aspx" id="gothicHeader17">About Us</a>
                                        </div>
                                    </div>          
                                    <div class="OB-menu-divider"></div>              
                                   	<div class="OB-menu-item">
                                        <div>
                                            <asp:HyperLink ID="lnkFaq" runat="server" ImageUrl="/Portals/_default/Skins/Obit/Images/ob-icon-faq-off.png" NavigateUrl="/resources/faq.aspx"></asp:HyperLink>
                                        </div>
                                        <div>
                                            <a href="/resources/faq.aspx" id="gothicHeader18">Frequently<br />Asked</a>
                                        </div>
                                    </div>
                                    <div class="OB-menu-divider"></div>
                                    <div class="OB-menu-item">
                                        <div>
                                           <asp:HyperLink ID="lnkContact" runat="server" ImageUrl="/Portals/_default/Skins/Obit/Images/ob-icon-contact-off.png" NavigateUrl="/learn/aboutus/contactus.aspx"></asp:HyperLink>
                                        </div>
                                        <div>
                                            <a href="/learn/aboutus/contactus.aspx" id="gothicHeader25">Contact</a>
                                        </div>
                                    </div>
                                    <div class="OB-clear"></div>
                                </div>               
                                <ul id="mainnav" class="nav">
                                    <li class="dropdown" id="liMenuLearn">
                                        <a href="javascript:void(0);" onMouseOver="classActiveToggleLayer(getDocID('headerNav'),getDocID('divMenuLearn'),getDocID('divGlassBG'),'','dropdown-menu','div');classToggleLayerChangeClass(getDocID('mainnav'),getDocID('liMenuLearn'),'liActiveMenu','dropdown','li');" onMouseOut="classActiveToggleLayer(getDocID('headerNav'),getDocID('divMenuLearn'),getDocID('divGlassBG'),'','dropdown-menu','div');classToggleLayerChangeClass(getDocID('mainnav'),getDocID('liMenuLearn'),'dropdown','dropdown','li');" id="gothicHeader26">Learn</a>
                                    </li>
                                    <li class="dropdown" id="liMenuCreate">
                                        <a href="javascript:void(0);" onMouseOver="classActiveToggleLayer(getDocID('headerNav'),getDocID('divMenuCreate'),getDocID('divGlassBG'),'','dropdown-menu','div');classToggleLayerChangeClass(getDocID('mainnav'),getDocID('liMenuCreate'),'liActiveMenu','dropdown','li');" onMouseOut="classActiveToggleLayer(getDocID('headerNav'),getDocID('divMenuCreate'),getDocID('divGlassBG'),'','dropdown-menu','div');classToggleLayerChangeClass(getDocID('mainnav'),getDocID('liMenuCreate'),'dropdown','dropdown','li');" id="gothicHeader27">Create</a>
                                    </li>        
                                    <li class="dropdown" id="liMenuResources">
                                        <a href="javascript:void(0);" onMouseOver="classActiveToggleLayer(getDocID('headerNav'),getDocID('divMenuResources'),getDocID('divGlassBG'),'','dropdown-menu','div');classToggleLayerChangeClass(getDocID('mainnav'),getDocID('liMenuResources'),'liActiveMenu','dropdown','li');" onMouseOut="classActiveToggleLayer(getDocID('headerNav'),getDocID('divMenuResources'),getDocID('divGlassBG'),'','dropdown-menu','div');classToggleLayerChangeClass(getDocID('mainnav'),getDocID('liMenuResources'),'dropdown','dropdown','li');" id="gothicHeader28">Resources</a>                   
                                    </li>
                                </ul><!--/.nav -->  
                            </div>
                            
                            <!--- SEARCH live-search -->
                            <div class="OB-search" id="divMainSearch">
                                <a href="javascript:void(0);" id="aMainSearch" class="aSearchPageKeywordButton" onClick="mainSearchClick('divMainSearchMessage', getDocID('txtSearch'));"><img alt="Search" src="/Portals/_default/skins/Obit/images/green-search.png" /></a>
                            	
                                <input type="text" id="txtSearch" class="search-query span3" autocomplete="off" placeholder="Search Obituaries, Funeral Homes & Memorials" onKeyup="mainSearchKeyUp('/ASP/SearchFHObituaries.aspx', 'divMainSearchMessage', getDocID('divMainSearchResults'), getDocID('txtSearch'));" onKeypress="if (event.keyCode == 13 && getDocID('txtSearch').value.length <= 2){return false;}">

                                <div id="divMainSearchResults" class="divJustHidden" onMouseOut="javascript:getDocID('divMainSearchResults').style.display='';" onMouseOver="javascript:getDocID('divMainSearchResults').style.display='block';"></div>

                                <div class="divBasicMessage" id="divMainSearchMessage"></div>
                            </div>
                            <div class="OB-clear"></div>
                        </div>
                    </div><!--/.navbar-inner -->
                </header>
                </div>
                <div class="divBasicHidden divBasicFloat" id="divHiddenHeaderMap">
                    <div class="customLeft divClose divHiddenCloseMap">
                        <a href="javascript:void(0);" class="aClose" onclick="javascript:toggleLayer('divHiddenHeaderMap', 'divGrayBG', '');">X</a>
                    </div>
                    <div id="divHiddenMapName" class="customRight">
                        <label id="lblHiddenMapName"></label>
                    </div>
                    <div class="customFooter"></div>
                    <div class="divHiddlenBody boardBox" id="divHiddenMapBody">
                        <div id='divHiddenMap'></div>
            		</div>
                </div><!-- end of Hidden Div -->
            </div>
        </div>
		<div id="divGlassBG" class="divBasicHiddlenBackground"></div>
