// Sign Up Process For User that has a funeral home attach to them

public partial class FHSignUp : System.Web.UI.UserControl
{
	private string strUserEmailTemplate = "FHSignUpThankYou";//holds the email template that the user is going to use
    private string strUserEmailSubject = "Validating Your Registration Request";//holds the email subject that the user is going to use
	private int intWhatIsTheStatus = -1;//holds the status of what it will be when when the user creates a FH
	
	#region Properties
		
	public string UserEmailTemplate
    { 
        get { return strUserEmailTemplate; } 
        set { strUserEmailTemplate = value; }
    }//end of UserEmailTemplate
	
	public string UserEmailSubject
    { 
        get { return strUserEmailSubject; } 
        set { strUserEmailSubject = value; }
    }//end of UserEmailSubject
	
	public int WhatIsTheStatus
    { 
        get { return intWhatIsTheStatus; } 
        set { intWhatIsTheStatus = value; }
    }//end of WhatIsTheStatus

    #endregion
	
	#region Public Funcations
	
	//either enables or disables the validator as they may or may not be used
	public void disableValidator(bool boolDisableFHVailidator)
	{
		//checks if the vailidator is going to be disable if so then reset the fourm
		if(boolDisableFHVailidator == false)
		{			
			//clears the FH fourm
			txtFuneralName.Text = "";
			txtFuneralURL.Text = "";
			txtFuneralTitle.Text = "";
			txtFuneralCity.Text = "";
			txtFuneralAddress1.Text = "";
			txtFuneralAddress2.Text = "";
			txtFuneralPhone.Text = "";
			txtFuneralFax.Text = "";		
			txtFuneralComments.Text = "";
			txtPC.Text = "";
			ddlFuneralPro.SelectedValue = "1";
			ddlFuneralCountry.SelectedValue = "1";
			chkFHNew.Checked = false;
			
			//resets the FH search
			FHSearch.resetValues();
		}//end of if
		
		//enable or disable the FH Vailidator
		FuneralNameRequired.Visible = boolDisableFHVailidator;
		revURL.Visible = boolDisableFHVailidator;
		FuneralTitleRequired.Visible = boolDisableFHVailidator;
		revTitle.Visible = boolDisableFHVailidator;
		FuneralCityRequired.Visible = boolDisableFHVailidator;
		revCity.Visible = boolDisableFHVailidator;
		FuneralAddress1Required.Visible = boolDisableFHVailidator;
		FuneralPhoneRequired.Visible = boolDisableFHVailidator;
		revPhone.Visible = boolDisableFHVailidator;
		revFax.Visible = boolDisableFHVailidator;
		PCRequired.Visible = boolDisableFHVailidator;
		revPC.Visible = boolDisableFHVailidator;
	}//end of disableValidator()
	
	//checks if the user has is doing a search if so if there is any FH that have ben selected
	public bool checkSearchValue()
	{
		string[] arrFHSearch = FHSearch.getValues();//holds all of the items that the user has choosen
					
		//checks if the user has found an item in search if this is the selection the user has choosen
		//the other ones are just to let it go throw
		if(panSearchYourFH.Visible == true && arrFHSearch.Length > 0 || panSearchYourFH.Visible == false)
			return true;
		else
			return false;
	}//end of checkSearchValue()
	
	//saves the FH to the database
	public int saveFH(int intUserID, string strFirstName, string strLastName, string strEmail)
	{
		int intFuneralHomeID = 0;//holds the Funeral Home id that is either added to the database
		string strFHName = string.Empty;//holds the user manage FH
        string strFHNameAndAddress = string.Empty; //Holds FH address
		string[] arrFHSearch = FHSearch.getValues();//holds all of the items that the user has choosen
		
		try
		{
            //checks if have not choosen a funeral and wants to create a new one
            if (arrFHSearch.Length == 0)
            {
                //add the basic funeral home database into the database and returns the funeral home id
                intFuneralHomeID = DAL.addUpdateFuneralHome(0, txtFuneralName.Text.Replace("'","&lsquo;"), txtFuneralURL.Text, txtFuneralAddress1.Text.Replace("'","&lsquo;"), txtFuneralAddress2.Text.Replace("'","&lsquo;"), txtFuneralCity.Text.Replace("'","&lsquo;"), ddlFuneralPro.SelectedValue, ddlFuneralCountry.SelectedValue, txtFuneralPhone.Text, txtFuneralFax.Text, txtPC.Text, chkFHNew.Checked, "", "", "", "", "", intWhatIsTheStatus);

                //updates the Funeral Home selection
                DAL.addUpdateLinkTableFHUser(intFuneralHomeID, intUserID, 0, true);

                //gets the funeral name for the email
                strFHName = txtFuneralName.Text;

                //gets the funeral home name and address for email
                strFHNameAndAddress = strFHName + "<br/>" + txtFuneralAddress1.Text + " " + txtFuneralCity.Text; ;
            }//end of if
            else
            {
                //goes around each Funeral Home that has been selected and updates the user id
                for (int intIndex = 0; intIndex < arrFHSearch.Length; intIndex = intIndex + 3)
                {
					DataTable dtFuneralHome = DAL.queryDbTable("SELECT * FROM  WHERE  = '" + arrFHSearch[(intIndex + 1)].Replace("'","''") + "'");//holds the details of the funeral home that the user has selected
                    string strFHAddress = string.Empty;//holds the FH Address
					
                    //adds the Funeral Home to the link table that has the user id
                    DAL.addUpdateLinkTableFHUser(Convert.ToInt32(arrFHSearch[(intIndex + 1)]), intUserID, 0, true);

                    //updates the status to make sure lock it down and not activated it until the Admin appoves it
                    DAL.updateFuneralHomeStatus(Convert.ToInt32(arrFHSearch[(intIndex + 1)]), intWhatIsTheStatus);
					
					//gets the funeral home address for email
                    if (dtFuneralHome != null && dtFuneralHome.Rows.Count > 0)
                        strFHAddress = dtFuneralHome.Rows[0][""].ToString() + ", " + dtFuneralHome.Rows[0][""].ToString();

                    //checks if this is the last item if so then add in AND instead of ,
                    if ((arrFHSearch.Length - intIndex) == 2)
                    {
                        //gets the funeral name and Address for the email
                        strFHName += " and " + arrFHSearch[(intIndex)];
                        strFHNameAndAddress += " and " + arrFHSearch[(intIndex)] + "<br/>" + strFHAddress + "<br/>";
                    }//end of if
                    else
                    {
                        //gets the funeral name for the email
                        strFHName += arrFHSearch[(intIndex)] + (strFHName.Length > 1 ? ", " : "");
                        strFHNameAndAddress += arrFHSearch[(intIndex)] + "<br/>" + strFHAddress + "<br/>";
                    }//end of else
                }//end of for loop
            }//end of else if
			
			//sends the user an email that the have been add to the database
			//this is so wrong and needs to change however this is what the client whats
            General.sendHTMLMail(strEmail, strUserEmailSubject, string.Format(File.ReadAllText(Server.MapPath("~/EmailTemplate/" + strUserEmailTemplate + ".html")), strFirstName, strLastName, strFHName));
	
			//sends an email to the obituaries tell them to apporvle this funeral home and user 
            General.sendHTMLMail("", "theObituaries.ca Site Administrator Credentials", string.Format(File.ReadAllText(Server.MapPath("~/EmailTemplate/FHCheckSignUp.html")), strFirstName, strLastName, strFHName, txtFuneralPhone.Text, txtFuneralComments.Text, strFHNameAndAddress));
		}//end of try
		catch (Exception ex)
		{
			Response.Write("FH Sign Up: " + ex.Message);// + " " + ex.StackTrace;
		}//end of catch
		
		return intFuneralHomeID;
	}//end of saveFH()
	
	#endregion
	
	protected void Page_PreRender(object sender, EventArgs e)
	{
        if (!IsPostBack)
        {
			//gets the Country
			ddlFuneralCountry.DataSource = DAL.getRow("","");
			ddlFuneralCountry.DataBind();
					
			//gets the all of the Canada's Province as the default selection
			ddlFuneralPro.DataSource = DAL.getRow("","Where  = 1 Order by ");
			ddlFuneralPro.DataBind();
			
			//adds a javascript for RegularExpressionValidator to check if the 
			//txtbox that is connect to the field needs to be hightlight for the error
			txtFuneralURL.Attributes.Add("onblur", "validateExpressionCheck('" + revURL.ClientID + "', '" + txtFuneralURL.ClientID + "');");
			txtFuneralTitle.Attributes.Add("onblur", "validateExpressionCheck('" + revTitle.ClientID + "', '" + txtFuneralTitle.ClientID + "');");
			txtFuneralCity.Attributes.Add("onblur", "validateExpressionCheck('" + revCity.ClientID + "', '" + txtFuneralCity.ClientID + "');");
			txtPC.Attributes.Add("onblur", "validateExpressionCheck('" + revPC.ClientID + "', '" + txtPC.ClientID + "');");
			txtFuneralPhone.Attributes.Add("onblur", "validateExpressionCheck('" + revPhone.ClientID + "', '" + txtFuneralPhone.ClientID + "');");
			txtFuneralFax.Attributes.Add("onblur", "validateExpressionCheck('" + revFax.ClientID + "', '" + txtFuneralFax.ClientID + "');");
			
			//check is the language is other then english
			// if so then changes this file images and text to fit that language
            if (Thread.CurrentThread.CurrentCulture.Name == "fr-FR")
            {
            }//end of if
        }//end of if
		else
		{
			//loads the items back into lbChooseFH after postback
			FHSearch.loadChooseFH();
			
			//enables or disables the required fields for the add your funeral home section
			//lbChooseFH_SelectedIndexChanged(sender, e);
		}//end of else
	}//end of Page_PreRender()

	protected void ddlFuneralCountry_SelectedIndexChanged(object sender, EventArgs e)
	{
		//checks which contry the user is using Canada or use and change the name of the postal to 
		if(ddlFuneralCountry.SelectedValue == "2")
		{
			//sets the display format for US
			lblPC.Text = "Zip";
			lblPCEx.Text = "12345";
			lblProvince.Text = "State";
			revPC.ErrorMessage = "Invalid zip code format";
			revPC.ValidationExpression = @"^\d{5}$";
		}//end of if
		else
		{
			//sets the display format for Canada/Other
			lblPC.Text = "Postal";
			lblPCEx.Text = "A1A 1A1";
			lblProvince.Text = "Province";
			revPC.ErrorMessage = "Invalid postal code format";
			revPC.ValidationExpression = @"^[A-Z]\d[A-Z][ ]\d[A-Z]\d$";
		}//end of else
		
		//changes the Provance to the country selected
		ddlFuneralPro.DataSource = DAL.getRow("","Where  != 64 AND  = " + ddlFuneralCountry.SelectedValue + " Order by ");
		ddlFuneralPro.DataBind();
	}//end of ddlFuneralCountry_SelectedIndexChanged()
	
	protected void lbSearchYourFH_Click(object sender, EventArgs e)
	{
		//hides panSearchYourFH and displays panAddYouFH
		panSearchYourFH.Visible = false;
		panAddYouFH.Visible = true;
		
		//enable the requirements as the the FH fourm is needed
		disableValidator(true);
	}//end of lbSearchYourFH_Click()
	
	protected void lbAddYouFH_Click(object sender, EventArgs e)
	{
		//hides panAddYouFH and displays panSearchYourFH
		panSearchYourFH.Visible = true;
		panAddYouFH.Visible = false;
		
		//disable the requirments as the search is needed
		disableValidator(false);
	}//end of lbAddYouFH_Click()
}//end of User Contorl