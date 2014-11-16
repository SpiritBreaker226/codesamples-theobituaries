// User can edit thier account

public partial class MemberEditAccount : PortalModuleBase
{
	protected void Page_PreRender(object sender, EventArgs e)
    {		
		if (!IsPostBack)
        {
			//checks if the user is already logged in and if so then send them to the homepage
            if (Session[""] == null)            
                Response.Redirect("/MyAccount/Login.aspx?url=" + Server.UrlEncode(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "")), true);
            
			DataTable dtLinkFHUser = DAL.getRow("", "Where = " + Session[""].ToString());//holds the user connection to any Funeral Home
			
			//checks if this user has any FH if not then change the intro text to remove any FH
			if(dtLinkFHUser.Rows.Count == 0)
				litMyAccountIntro.Text = "If you would like to create a new Obituary, Memorial or Pre-Plan Obituary, click the Create button at the top of the page. Click the Edit Services button on the right to edit or delete any of your existing Obituary, Memorial, or Pre-Plan records.";
		}//end of if
    }//end of Page_PreRender()
}//end of Page