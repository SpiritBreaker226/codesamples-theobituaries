// user confirm account

public partial class MemberConfirmAccount : PortalModuleBase
{	
	protected void Page_PreRender(object sender, EventArgs e)
    {			
		if (!IsPostBack)
        {	
			//checks if the user is already logged in and if so then send them to the homepage
			if (Session[""] != null)
				Response.Redirect("/Home.aspx", true);
		}//end of if
    }//end of Page_PreRender()
		
	protected void cmdLogin_Click(object sender, EventArgs e)
	{
		try
		{
			//turns off the error messages
			lblError.Visible = false;
			
			//checks if the user agree Terms of Use
			if(chkAgreeTerms.Checked == true)
			{
				DataTable dtUserDetails = DAL.getRow("", "Where  = '" + DAL.safeSql(txtEmail.Text) + "' AND  = 0");//holds the users details
				
				//checks if the if the user is in the database and can login is in the database
				if (dtUserDetails.Rows.Count > 0)
				{
					//updates the users status to be on
					DAL.updateUserActivated(Convert.ToInt32(dtUserDetails.Rows[0][""].ToString()), true);
									
					//sets the session & cookies valuable to tell the that the user has been loged in
					General.setSession(dtUserDetails, false);
					
					//Turn on the thank you message and removes the login
					panThankYou.Visible = true;
					panLogin.Visible = false;
				}//end of if
				else
					throw new Exception("User name or password do not match");
			}//end of if
			else
				throw new Exception("You must agree to the terms & conditions");
		}//end of try
		catch (Exception ex)
		{
			lblError.Text = ex.Message;
			lblError.Visible = true;
		}//end of catch
	}//end of cmdLogin_Click()
}//end of Page