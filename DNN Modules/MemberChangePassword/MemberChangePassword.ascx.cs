// reset users password

public partial class DesktopModules_MemberChangePassword_MemberChangePassword : PortalModuleBase
{
    protected void Page_PreRender(object sender, EventArgs e)
    {
        //checks if the user is already logged in and if so then send them to the homepage
		if (Session[""] == null)
			Response.Redirect("/MyAccount/Login.aspx?url=" + Server.UrlEncode(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "")), true);
    }//end of Page_PreRender()

    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        try
        {
			//reset error messge
			lblError.Visible = false;
			
			DataTable dtUserDetails = DAL.getRow("", "Where  = '" + DAL.safeSql(Session[""].ToString()) + "' AND  = 1");//holds the users details

			//checks if the if the user is in the database and can login is in the database
			if (dtUserDetails.Rows.Count > 0)
			{
				//updates the user password
				DAL.updateUserBasicSetting(Convert.ToInt32(Session[""].ToString()), dtUserDetails.Rows[0][""].ToString(), dtUserDetails.Rows[0][""].ToString(), dtUserDetails.Rows[0][""].ToString(), );
				
				//sets the session & cookies valuable to tell the that the user has been loged in
				General.setSession(dtUserDetails, false);
				
				//turn on the thank you message and removes the body
				panThankYou.Visible = true;
				panBody.Visible = false;
			}//end of if
			else
				throw new Exception("Current password is incorrect.");
        }//end of try
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            lblError.Visible = true;
        }//end of catch
    }//end of cmdLogin_Click()
}//end of Module