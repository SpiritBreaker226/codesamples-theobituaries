<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchResultsForObituaries.ascx.cs" Inherits="SearchResultsForObituaries" %>

<div class="divError"> 
    <asp:Label runat="server" ID="lblError" Visible="false"></asp:Label>
</div>
<div class='customContainer' id="divSearchResultPageBodyContainer">
    <div class='customLeft' id="divSearchResultPageBodyLeft">
		<asp:Panel runat="server" ID="panKeywords" DefaultButton="ibKeywords">
        	<div class="divKeywordsSearch">
	    		<asp:Label runat="server" ID="lblKeywords" Text="Keywords" CssClass="lblSearchPageKeyword"></asp:Label>
            </div>
            
    	    <asp:ImageButton runat="server" ID="ibKeywords" OnClick="ibKeywords_Click" CssClass="imgSearchPageKeywordButton" ImageUrl="/Portals/_default/skins/Obit/images/green-search.png" AlternateText="Search"></asp:ImageButton>
            <asp:TextBox runat="server" ID="txtKeywords" CssClass="txtSearchPageKeyword" Placeholder="Keywords"></asp:TextBox>
		</asp:Panel>
        <asp:Panel runat="server" ID="panRefineSearch">
            <div id="divRefineSearch">
                <div class="divRefineSearch">
                    <label id="gothicUC1"><asp:Label runat="server" ID="lblRefineSearch" Text="Refine Your Search" CssClass="lblRefineSearch"></asp:Label></label>
                </div>
                <asp:Literal runat="server" ID="litSearchProvince"></asp:Literal>
            </div>
            <asp:Panel runat="server" ID="panCitySearch" DefaultButton="ibCitySearch">
                <asp:ImageButton runat="server" ID="ibCitySearch" OnClick="ibCitySearch_Click" CssClass="imgSearchPageKeywordButton" ImageUrl="/Portals/_default/skins/Obit/images/green-search.png" AlternateText="City or town search"></asp:ImageButton>
                <asp:TextBox runat="server" ID="txtCitySearch" CssClass="txtSearchPageKeyword" Placeholder="Enter a city or town"></asp:TextBox>
                <div id="divCitySearch"></div>
                <asp:Literal runat="server" ID="litSearchCity"></asp:Literal>
            </asp:Panel>
		</asp:Panel>
    </div>
    <div class='customRight' id="divSearchResultPageBodyRight">
        <asp:Panel runat="server" ID="panSearchResultsTabs" CssClass="customContainer divSearchResultPageHeaderTabContainer">
        	<div id="divSearchResultPageTab">
            	<asp:Panel runat="server" ID="panSearchResultsTabObit" CssClass="customLeft divSearchResultPageHeaderTabLeft">
                    <span id="gothic102">
                    	<asp:Label runat="server" ID="lblTabNameObit" Text="Obituaries"></asp:Label>
					</span>
                </asp:Panel>
                <asp:Panel runat="server" ID="panSearchResultsTabFH" CssClass="customLeft divSearchResultPageHeaderTabLeft">
                    <span id="gothic101">
                    	<asp:Label runat="server" ID="lblTabNameFH" Text="Funeral Homes"></asp:Label>
					</span>
                </asp:Panel>
                <asp:Panel runat="server" ID="panSearchResultsTabMem" CssClass="customLeft divSearchResultPageHeaderTabLeft">
                    <span id="gothic103">
                    	<asp:Label runat="server" ID="lblTabNameMem" Text="Memorials"></asp:Label>
					</span>
                </asp:Panel>
			</div>
            <div class='customRight divSearchResultPageHeaderTabRight'></div>
            <div class='customFooter divSearchResultPageHeaderTabFooter'></div>
        </asp:Panel>
        <asp:Panel runat="server" ID="panSearchResultPageBody">
            <asp:Panel runat="server" ID="panSearchResultsTabBodyObit" CssClass="divSearchResultsTab divJustHidden">
                <div class='customContainer divSearchResultPageHeaderFilterContainer'>
                    <div class='customLeft divSearchResultPageHeaderFilterLeft'>
                        <asp:Literal runat="server" ID="litResultsFoundObit"></asp:Literal>
                    </div>
                    <asp:Panel runat="server" ID="panSortResultObit" CssClass="customRight divSearchResultPageHeaderFilterRight">
                    	<div class='customContainer divSearchResultPageHeaderFilterSortFieldsContainer'>
                            <div class='customLeft divSearchResultPageHeaderFilterSortFieldsLeft'>
                                <asp:Label runat="server" ID="lblSortResultObit" Text="Sort by:"></asp:Label>
                            </div>
                            <div class='customRight divSearchResultPageHeaderFilterSortFieldsRight'>
                                <asp:DropDownList runat="server" ID="ddlSortResultObit" AutoPostBack="true" OnSelectedIndexChanged="ddlSortResult_SelectedIndexChanged" CssClass="selectSearchSort">
                                    <asp:ListItem Text="From Name (A-Z)" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="From Name (Z-A)" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="City / Town (A-Z)" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="City / Town (Z-A)" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Most Recent" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Oldest" Value="5"></asp:ListItem>
                                </asp:DropDownList>
							</div>
                            <div class='customFooter divSearchResultPageHeaderFilterSortFieldsFooter'></div>
                        </div>
                    </asp:Panel>
                    <div class='customFooter divSearchResultPageHeaderFilterFooter'></div>
                </div>
                <div class="divSearchResultsBody">
					<asp:Literal runat="server" ID="litSearchBodyObit"></asp:Literal>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="panSearchResultsTabBodyFH" CssClass="divSearchResultsTab divJustHidden">
                <div class='customContainer divSearchResultPageHeaderFilterContainer'>
                    <div class='customLeft divSearchResultPageHeaderFilterLeft'>
                        <asp:Literal runat="server" ID="litResultsFoundFH"></asp:Literal>
                    </div>
                    <asp:Panel runat="server" ID="panSortResultFH" CssClass="customRight divSearchResultPageHeaderFilterRight">
                    	<div class='customContainer divSearchResultPageHeaderFilterSortFieldsContainer'>
                            <div class='customLeft divSearchResultPageHeaderFilterSortFieldsLeft'>
                                <asp:Label runat="server" ID="lblSortResultFH" Text="Sort by:"></asp:Label>
                            </div>
                            <div class='customRight divSearchResultPageHeaderFilterSortFieldsRight'>
                            	<asp:DropDownList runat="server" ID="ddlSortResultFH" AutoPostBack="true" OnSelectedIndexChanged="ddlSortResult_SelectedIndexChanged" CssClass="selectSearchSort">
                                    <asp:ListItem Text="From Name (A-Z)" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="From Name (Z-A)" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Most Recent" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Oldest" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class='customFooter divSearchResultPageHeaderFilterSortFieldsFooter'></div>
                        </div>
                    </asp:Panel>
                    <div class='customFooter divSearchResultPageHeaderFilterFooter'></div>
                </div>
                <div class="divSearchResultsBody">
					<asp:Literal runat="server" ID="litSearchBodyFH"></asp:Literal>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="panSearchResultsTabBodyMem" CssClass="divSearchResultsTab divJustHidden">
                <div class='customContainer divSearchResultPageHeaderFilterContainer'>
                    <div class='customLeft divSearchResultPageHeaderFilterLeft'>
                        <asp:Literal runat="server" ID="litResultsFoundMem"></asp:Literal>
                    </div>
                    <asp:Panel runat="server" ID="panSortResultMem" CssClass="customRight divSearchResultPageHeaderFilterRight">
                    	<div class='customContainer divSearchResultPageHeaderFilterSortFieldsContainer'>
                            <div class='customLeft divSearchResultPageHeaderFilterSortFieldsLeft'>
                                <asp:Label runat="server" ID="lblSortResultMem" Text="Sort by:"></asp:Label>
                            </div>
                            <div class='customRight divSearchResultPageHeaderFilterSortFieldsRight'>
                                <asp:DropDownList runat="server" ID="ddlSortResultMem" AutoPostBack="true" OnSelectedIndexChanged="ddlSortResult_SelectedIndexChanged" CssClass="selectSearchSort">
                                    <asp:ListItem Text="From Name (A-Z)" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="From Name (Z-A)" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="City / Town (A-Z)" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="City / Town (Z-A)" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Most Recent" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Oldest" Value="5"></asp:ListItem>
                                </asp:DropDownList>
							</div>
                            <div class='customFooter divSearchResultPageHeaderFilterSortFieldsFooter'></div>
                        </div>
                    </asp:Panel>
                    <div class='customFooter divSearchResultPageHeaderFilterFooter'></div>
                </div>
                <div class="divSearchResultsBody">
					<asp:Literal runat="server" ID="litSearchBodyMem"></asp:Literal>
                </div>
            </asp:Panel>
			
            <asp:Panel runat="server" ID="panFHBanner" CssClass="divJustHidden divFHTabBottomBanner">
				<a href='/SignUp.aspx'><img src="/portals/_default/skins/obit/images/FreeProfileFH-banner.jpg" alt="Set up a Free Profile today" /></a>
                <!---<label>Are you one of the listed funeral homes without a full listing and Funeral Home page? <a href='/SignUp.aspx'>Set up a FREE profile today</a>.</label>--->
			</asp:Panel>
		</asp:Panel>
	</div>
    <div class='customFooter' id="divSearchResultPageBodyFooter"></div>
</div>