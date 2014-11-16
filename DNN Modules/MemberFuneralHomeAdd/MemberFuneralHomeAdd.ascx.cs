// Adds Users Funeral Home To Site

public partial class MemberFuneralHomeAdd : PortalModuleBase
{
	protected void Page_PreRender(object sender, EventArgs e)
    {
		//checks if the user is already logged in and if so then send them to the homepage
		if (Session["MemberLogin"] == null)
			Response.Redirect("/MyAccount/Login.aspx?url=" + Server.UrlEncode(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "")), true);
		
		if (!IsPostBack)
        {
			//creates an event for captcha to for when captcha is loaded to add in a set number of random numbers
			//in a image
			Captcha.InitializedCaptchaControl += new EventHandler<InitializedCaptchaControlEventArgs>(Captcha_InitializedCaptchaControl);
			
			//setup client-side input processing
			Captcha.UserInputClientID = txtCaptchaCode.ClientID;
		}//end of if
    }//end of Page_PreRender()
	
	protected void cmdSave_Click(object sender, EventArgs e)
	{
		try
		{
			//turns off the error messages
			lblError.Visible = false;
			
			//checks if the page is valid if so then prcess the event
			if (Page.IsValid)
			{							
				//checks if the user has found an item in search if this is the selection the user has choosen
				//the other ones are just to let it go throw
				if(FHSignUp.checkSearchValue() == true)
				{
					//checks if the Captcha is validated
					if (Captcha.Validate(txtCaptchaCode.Text.Trim().ToUpper()))
					{						
						DataTable dtUserDetails = DAL.getRow("","Where = " + Convert.ToInt32(Session[""]));//gets the users details for the email that will be sent out
						
						//does the sign up for the FH
						FHSignUp.saveFH(Convert.ToInt32(Session[""]), dtUserDetails.Rows[0][""].ToString(), dtUserDetails.Rows[0][""].ToString(), dtUserDetails.Rows[0][""].ToString());
																							
						//Turn on the thank you message and removes the sign up
						panThankYou.Visible = true;
						panSignUp.Visible = false;
					}//end of if
					else
						throw new Exception("Incorrect! Captcha");
				}//end of if
				else
					throw new Exception("You must have at least one selected funeral home from our listings");
			}//end of if
			
			//clear previous user input
			txtCaptchaCode.Text = null;
		}//end of try
		catch (Exception ex)
		{
			//clear previous user input
			txtCaptchaCode.Text = null;
			
			lblError.Text = ex.Message;
			lblError.Visible = true;
		}//end of catch
	}//end of cmdSave_Click()		
}//end of Page