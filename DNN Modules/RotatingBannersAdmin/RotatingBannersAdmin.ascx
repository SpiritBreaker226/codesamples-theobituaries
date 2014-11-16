<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RotatingBannersAdmin.ascx.cs" Inherits="RotatingBannersAdmin" %>

<link rel="stylesheet" type="text/css" href="/Portals/_default/Skins/Obit/CSS/adminStyle.css" media="screen" />
<link rel="stylesheet" type="text/css" href="/Portals/_default/Skins/Obit/CSS/styles.css" media="screen" />

<asp:Panel runat="server" ID="panRotatingBannersDisplay">
    <div class="customContainer divHeaderContainer">
        <div class="customLeft divHeaderLeft">
        	<div class="customContainer divSectionHeaderContainer">
                <div class="customLeft divSectionHeaderLeft">
                    <asp:Button ID="cmdAddNew" runat="server" Text="Add Banner" OnClick="cmdAdd_Click"></asp:Button>
                </div>
                <div class="customRight divSectionHeaderRight">
                    <%--<asp:Button ID="cmdRefresh" runat="server" Text="Refresh" OnClick="cmdCancel_Click"></asp:Button>--%>
                </div>
                <div class="customFooter divSectionHeaderFooter"></div>
            </div>
        </div>
        <div class="customRight divHeaderRight">
        	<asp:Panel id="panSearch" runat="server" DefaultButton="cmdSearch">
                <div class="customContainer divBannerFieldsContainer">
                    <div class="customLeft divBannerFieldsLeft">
                        <label>Search By Title</label>
                    </div>
                    <div class="customMiddle divBannerFieldsMiddle">
                        <asp:TextBox ID="txtSearch" runat="server" MaxLength="11"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="SearchPhoneNoRequired" runat="server" Text="*" ErrorMessage="Please, fill in the text for search" Display="Static" ControlToValidate="txtSearch" ValidationGroup="Search" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                    <div class="customRight divBannerFieldsRight">
                        <asp:Button ID="cmdSearch" runat="server" Text="Search" OnClick="cmdSearch_Click" ValidationGroup="Search"></asp:Button>
                        <asp:Button ID="cmdClearSearch" runat="server" Text="Clear Search" OnClick="cmdClearSearch_Click"></asp:Button>
                    </div>
                    <div class="customFooter divBannerFooter"></div>
                </div>
			</asp:Panel>
        </div>
        <div class="customFooter divHeaderFooter"></div>
    </div>

    <asp:DataGrid id="gdRotatingBanners" runat="server" Width="950px" GridLines="None" CellPadding="3" AutoGenerateColumns="False" OnItemCommand="gdRotatingBanners_ItemCommand" OnItemDataBound="gdRotatingBanners_ItemDataBound">
        <Columns>            
            <asp:BoundColumn DataField="" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="" ItemStyle-Width="30%" HeaderText="Title"></asp:BoundColumn>
            <asp:BoundColumn DataField="" ItemStyle-Width="30%" HeaderText="Start Date"></asp:BoundColumn>
            <asp:BoundColumn DataField="" ItemStyle-Width="35%" HeaderText="End Date"></asp:BoundColumn>
            <asp:BoundColumn DataField="" ItemStyle-Width="5%" HeaderText="Enable"></asp:BoundColumn>
            <asp:BoundColumn DataField="" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="" Visible="False"></asp:BoundColumn>
			<asp:BoundColumn DataField="" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="" ItemStyle-Width="5%" HeaderText="Order"></asp:BoundColumn>
            <asp:ButtonColumn Text="Update" CommandName="Update" ButtonType="PushButton"></asp:ButtonColumn>
            <asp:TemplateColumn>
                <itemtemplate>
                    <asp:Button runat="server" ID="cmdDelete" Text="Delete" CommandName="Delete" OnClientClick="javascript: return confirm('Do you what to remove this banner?');"></asp:Button>
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
<asp:Panel runat="server" ID="panRotatingBannersAdd" Visible="false">
    <asp:Label runat="server" ID="lblID" Visible="False"></asp:Label>
    <asp:Label ID="lblMainTitle" runat="server" Text="Fill the form to insert the Item data" CssClass="lblMainTitle"></asp:Label>
    <br /><br />
    <div id="divError">
        <asp:ValidationSummary ID="vsRegistration" runat="server" CssClass="vsRegistration" ValidationGroup="OPC" DisplayMode="List" HeaderText="Rotating Banners has been filled out incorrectly" />
  
        <asp:Label runat="server" ID="lblError" Visible="false"></asp:Label>
    </div>
    
    <div class="customContainer divBannerContainer">
        <div class="customLeft divBannerLeft">
            <div class="customContainer divBannerFieldsContainer">
                <div class="customLeft divBannerFieldsLeft">
                    <label>Title *</label>
                </div>
                <div class="customRight divBannerFieldsRight">
                    <asp:TextBox ID="txtTitle" runat="server" MaxLength="120"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="TitleRequired" runat="server" Text="*" ErrorMessage="Fill in the title" Display="Static" ControlToValidate="txtTitle" ValidationGroup="OPC" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                 <div class="customFooter divBannerFooter"></div>
            </div>
            <div class="customContainer divBannerFieldsContainer">
                <div class="customLeft divBannerFieldsLeft">
                    <label>Link *</label>
                </div>
                <div class="customRight divBannerFieldsRight">
                    <asp:TextBox ID="txtBannersLink" runat="server" MaxLength="120"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="LinkRequired" runat="server" Text="*" ErrorMessage="Fill in the a link" Display="Static" ControlToValidate="txtBannersLink" ValidationGroup="OPC" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                 <div class="customFooter divBannerFooter"></div>
            </div>
            <div class="customContainer divBannerFieldsContainer">
                <div class="customLeft divBannerFieldsLeft">
                    <label>Start Date</label>
                </div>
                <div class="customRight divBannerFieldsRight">
                    <asp:UpdatePanel ID="ajaxCalStartDate" runat="server">
                        <ContentTemplate>
                            <asp:Calendar ID="calStartDate" runat="server" Width="200"></asp:Calendar>
                            <asp:Label runat="server" ID="lblStartTime" Visible="False"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="customFooter divBannerFooter"></div>
			</div>
            <div class="customContainer divBannerFieldsContainer">
                <div class="customLeft divBannerFieldsLeft">
                    <label>End Date</label>
                </div>
                <div class="customRight divBannerFieldsRight">
                    <asp:UpdatePanel ID="ajaxCalEndDate" runat="server">
                        <ContentTemplate>
                            <asp:Calendar ID="calEndDate" runat="server" Width="200"></asp:Calendar>
                            <asp:Label runat="server" ID="lblEndTime" Visible="False"></asp:Label>
                            <br />
                            <asp:LinkButton runat="server" ID="lbEndClearDate" OnClick="lbEndClearDate_Click" Text="Clear End Date"></asp:LinkButton>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="customFooter divBannerFooter"></div>
			</div>
            <div class="customContainer divBannerFieldsContainer">
                <div class="customLeft divBannerFieldsLeft">
                    <label>Enable ?</label>
                </div>
                <div class="customRight divBannerFieldsRight">
                    <asp:RadioButtonList ID="rdoEnable" runat="server" RepeatDirection="Horizontal">
						<asp:ListItem Value="False">False</asp:ListItem>
                        <asp:ListItem Value="True" Selected="True">True</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                 <div class="customFooter divBannerFooter"></div>
            </div>           
        </div>
        <div class="customRight divBannerRight">
            <div class="customContainer divBannerFieldsContainer">
                <div class="customLeft divBannerFieldsLeft">
                    <label>Image</label>
                </div>
                <div class="customRight divBannerFieldsRight">
                    <asp:FileUpload ID="fulImageUpload" runat="server" />
                </div>
                <div class="customFooter divBannerFooter"></div>
            </div>
            <!--<div class="customContainer divBannerFieldsContainer">
                <div class="customLeft divBannerFieldsLeft">
                    <label>Image Postion</label>
                </div>
                <div class="customRight divBannerFieldsRight">
                    <asp:RadioButtonList ID="rdoImagePostion" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="L">Left</asp:ListItem>
                        <asp:ListItem Value="C" Selected="true">Center</asp:ListItem>
                        <asp:ListItem Value="R">Right</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                 <div class="customFooter divBannerFooter"></div>
            </div>-->
	        <div class="customContainer divBannerFieldsContainer">
                <div class="customLeft divBannerFieldsLeft">
                    <label>Current Image</label>
                </div>
                <div class="customNavigation divBannerFieldsRight">
                    <asp:Image runat="server" ID="imgBanner" Width="530"></asp:Image>
                    <asp:Label runat="server" ID="lblImageSource" Visible="False"></asp:Label>
                </div>
                <div class="customFooter divBannerFooter"></div>
            </div>
            <!--<div class="customContainer divBannerFieldsContainer">
                <div class="customLeft divBannerFieldsLeft">
                    <label>Text</label>
                </div>
                <div class="customRight divBannerFieldsRight">
                    <asp:TextBox ID="txtHTML" runat="server" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div class="customFooter divBannerFooter"></div>
            </div>-->
            <div class="customContainer divBannerFieldsContainer">
                <div class="customLeft divBannerFieldsLeft">
                    <label>Order</label>
                </div>
                <div class="customRight divBannerFieldsRight">
                    <asp:TextBox ID="txtBannersOrder" runat="server" MaxLength="2"></asp:TextBox>
                    <asp:RegularExpressionValidator runat="server" id="OrderValidator" ControlToValidate="txtBannersOrder" Text="*" ErrorMessage="Invalid order format" ValidationExpression="^[0-9]+$" Display="Static" ValidationGroup="OPC" />
                </div>
                 <div class="customFooter divBannerFooter"></div>
            </div>
        </div>
        <div class="customFooter divFooter"></div>          
    </div>
    <div id="divLeftSide">
        <asp:Button ID="cmdSave" runat="server" Text="Save" OnClick="cmdSave_Click" ValidationGroup="OPC"></asp:Button>
        <asp:Button ID="cmdCancel" runat="server" Text="Cancel" OnClick="cmdCancel_Click" Visible="false"></asp:Button>
    </div>
</asp:Panel>