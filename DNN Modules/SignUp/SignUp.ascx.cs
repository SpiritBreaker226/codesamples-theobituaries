// Sign Up Page For Teh Site

public partial class SignUp : PortalModuleBase
{
	protected void Page_PreRender(object sender, EventArgs e)
    {								
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
			//checks if the page is valid if so then prcess the event
			if (Page.IsValid)
			{
				//turns off the error messages
				lblError.Visible = false;
				lblEmailError.Visible = false;
				
				//checks if the user's email is in the database 
				//if so then tell them that they have to user another email
				if(General.checkIfEmailExist(txtEmail.Text) == false)
				{					
					//checks if the user has found an item in search if this is the selection the user has choosen
					//the other ones are just to let it go throw
					if(rdoProfile.Checked == false && FHSignUp.checkSearchValue() == true || rdoProfile.Checked == true)
					{
						//checks if the Captcha is validated
						if (Captcha.Validate(txtCaptchaCode.Text.Trim().ToUpper()))
						{						
							//checks if the user agree Terms of Use
							if(chkAgreeTerms.Checked == true)
							{
								//creates a new user
								int intUserID = General.createNewUser(txtFName.Text, txtLName.Text, txtEmail.Text, chkNewsletter.Checked, rdoProfile.Checked);//holds the new users id just in case they want to create a Funeral Home
								
								//checks if this user wants to create a Funeral Home
								if(rdoProfile.Checked == false)
								{						
									//does the sign up for the FH
									FHSignUp.saveFH(intUserID, txtFName.Text, txtLName.Text, txtEmail.Text);
		
									//turns on the thank you message for the Funeral Home
									panThankYouFH.Visible = true;
								}//end of if
								else
									//turns on the thank you message for the Individual
									panThankYouIndividual.Visible = true;

								//Turn on the thank you message and removes the sign up
								panThankYou.Visible = true;
								panSignUp.Visible = false;
							}//end of if
							else
								throw new Exception("You must agree to the terms of user before you can sign up");
						}//end of if
						else
							throw new Exception("Incorrect! Captcha");
					}//end of if
					else
						throw new Exception("You must have at least one selected funeral home from our listings");
				}//end of if
				else
					throw new Exception("Email already exists in the database");
			}//end of if
			
			//clear previous user input
			txtCaptchaCode.Text = null;
		}//end of try
		catch (Exception ex)
		{
			//clear previous user input
			txtCaptchaCode.Text = null;
			
			lblError.Text = ex.Message;// + " " + ex.StackTrace;
			lblError.Visible = true;
		}//end of catch
	}//end of cmdSave_Click()		
	
	protected void txtEmail_TextChanged(object sender, EventArgs e)
	{
		//checks if the user's email is in the database 
		//if so then tell them that they have to user another email
        if (General.checkIfEmailExist(txtEmail.Text) == true)
        {
            lblEmailError.Text = "Email already exists in the database";
            lblEmailError.Visible = true;
        }//end of else
        else
        {
            lblEmailError.Visible = false;
            ScriptManager.GetCurrent(this.Page).SetFocus(txtCEmail);
        }
	}//end of txtEmail_TextChanged()
	
	protected void rblProfileFH_CheckedChanged(object sender, EventArgs e)
	{
		//checks which sign up the user is creating a Funeral Home or a normal profile
		if(rdoProfile.Checked == true)
		{
			//changes the layout for General Users
			panSignUpBody.Visible = true;
			panGeneralFH.Visible = false;
			
			//disable the FH fourm's validator in order not
			FHSignUp.disableValidator(false);
		}//end of if
		else
		{
			//changes the layout for Funerial Homes
			panSignUpBody.Visible = true;
			panGeneralFH.Visible = true;
			
			//enable the FH fourm's validator in order not
			FHSignUp.disableValidator(false);
		}//end of else
	}//end of rblProfileFH_CheckedChanged()
}//end of Page