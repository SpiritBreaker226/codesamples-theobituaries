// Lean Sections About Us Page Sub Menu

partial class LearnAboutUsSubMenu : PortalModuleBase
{
    protected void Page_Init(System.Object sender, System.EventArgs e)
    {
        string strTabName = "";//holds the Name of the Tab for which ever launage the user is using
        string strExtraClasses = "";//holds and extra class that needs to be used for the link
        int intSecLevel = 2;//holds the for secound level
        DotNetNuke.Entities.Tabs.TabController ctlTab = new TabController();//holds the Tab Contorller
        DataTable dtSecLevel = DAL.getSecLevelTabs(91);//holds the Secound Level Tab
        TabInfo tabCurrentInfo = ctlTab.GetTab(PortalSettings.ActiveTab.TabID);//holds the Current Page Info
        TabInfo tabParentInfo = null;//holds the Parent Info
        TabInfo tabParentParentInfo = null;//holds the Parent Parent Info

        //checks if there is a Parent
        if (tabCurrentInfo.ParentId > 0)
            tabParentInfo = ctlTab.GetTab(tabCurrentInfo.ParentId);

        //checks if there is a Parent Parent Info
        if (tabParentInfo != null && tabParentInfo.ParentId > 0)
            tabParentParentInfo = ctlTab.GetTab(tabParentInfo.ParentId);

        //checks if there is any items to display
        if (dtSecLevel != null && dtSecLevel.Rows.Count > 0)
        {
            //goes around for each item and adds it to the menu then gets checks if there is a sub menu and if so then
            //adds that to the menu and put a different background on it, then checks if there is a sub menu for that and if so then adds
            //that to the menu and puts a different background on that too
            foreach (DataRow drSecLevel in dtSecLevel.Rows)
            {
                TabInfo tabSecLevelInfo = ctlTab.GetTab(Convert.ToInt32(drSecLevel[""].ToString()));//holds the Current Page Info	

                //checks if the tab status and make sure it is not deleted, disable and that user can look
                if (PortalSecurity.IsInRoles(tabSecLevelInfo.AuthorizedRoles) && (!tabSecLevelInfo.IsDeleted) && (!tabSecLevelInfo.DisableLink))
                {                    
                    string currentTabStyle = string.Empty;
                    if (PortalSettings.ActiveTab. == Convert.ToInt32(drSecLevel[""].ToString()))
                        currentTabStyle = " HighLighted";

                    if (drSecLevel[""].ToString() == "109" && PortalSettings.ActiveTab. == 91)
                        currentTabStyle = " HighLighted";

                    strTabName = Server.HtmlDecode(drSecLevel[""].ToString());

                    //adds the item to the menu
                    litSubMenu.Text += "<li>" +
                        "<div class='OBLevel2RightNavBody" + currentTabStyle + "'>" +
                            "<a href='" + DotNetNuke.Common.Globals.NavigateURL(Convert.ToInt32(drSecLevel[""].ToString()), "", "") + "'>" + strTabName + "</a>" +
                        "</div>" +
                    "</li>";

                    intSecLevel++;

                    //resets the Extra Classes 
                    strExtraClasses = "";
                }//end of if
            }//end of foreach
        }//end of if
    }//end of Page_Init()
}//end of Class