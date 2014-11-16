// The user is able to view the details of a Funeral Home that is currently displaying on the site under their users name

public partial class ReviewFuneralHomes : PortalModuleBase
{	
	private int _Cols = 4;
    private int _Rows = 3;
    private int _PageSize;
    private int _HiddenPageIndex;
    private int _LastPageIndex;
	private int _Count;
	private bool _PagingVarsInitialized = false;
	private string strSearchWhere;//holds the where for the search
	private string strSearchSort;//holds the sort for the search
	
	private void Bind(int intSelectedIndex)
    {
		//UPDATE THE RESULT INDEX MESSAGE
        int startRowIndex = (_PageSize * _HiddenPageIndex);
        int endRowIndex = startRowIndex + _PageSize;
		
        if (endRowIndex > _Count) 
			endRowIndex = _Count;
		
		if (_HiddenPageIndex > 0) 
			startRowIndex++;

		DataTable dtFH = DAL.getRows(""," DESC",startRowIndex,endRowIndex,strSearchWhere);//holds the where will be display to the user
			
		//checks if there is any items to find if not then dispaly it to the user and change the text around
		gdFH.Visible = !(dtFH.Rows.Count == 0);
		litNoFound.Visible = (dtFH.Rows.Count == 0);
		litNoFound.Text = "No Funeral Home found!<br/><br/>";
				
		//re-gets the FH
        gdFH.DataSource = dtFH;
		gdFH.DataBind();
    }//end of Bind()
	
	private void BindDesignsPanel()
    {
        //INITIALIZE PAGING VARIABLES
        InitializePagingVars(false);

        //BIND THE ASK QUESTION
        Bind(-1);

        //BIND THE PAGING CONTROLS FOOTER
        BindPagingControls();
    }//end of BindDesignsPanel()
	
    private void InitializePagingVars(bool forceRefresh)
    {
        if (!_PagingVarsInitialized || forceRefresh)
        {
			DataTable dtCount = DAL.countRows("",strSearchWhere);

			_HiddenPageIndex = Convert.ToInt32(HiddenPageIndex.Value);
			_Count = Convert.ToInt32(dtCount.Rows[0]["count"].ToString());
			_LastPageIndex = ((int)Math.Ceiling(((double)_Count / (double)_PageSize))) - 1;
			_PagingVarsInitialized = true;
        }//end of if
    }//end of InitializePagingVars()
	
	//loads the data into the Update section
	private void loadUpdateDate(int intFHID)
	{
		try
		{
			DataTable dtFH = DAL.getRow("", "Where  = " + intFHID);//holds the details of the FH
			DataTable dtFHUser = DAL.getRow("", "Where  = 1 AND  = " + intFHID);//holds the details of the FH and the User
			DataTable dtFHAffiliations = DAL.getRow("", "Where  = " + intFHID);//holds the Funeral Home / Affiliations Details
			DataTable dtFHOfferings = DAL.getRow("", "Where  = " + intFHID);//holds the Funeral Home / Offerings Details
			string strSQLOfferingsUserID = "";//holds the sql statment to fidn the user's offering
						
			//truns on the Image Uploader for Updates
			ciFH.Visible = true;
			
			//checks if this is a Draft in order to get the details for the user that is connected to it
			if(dtFH.Rows[0][""].ToString() != "0")
			{
				//gets the details of the orginal FH as this is connected to the user
				dtFHUser = DAL.getRow("", "where  = 1 AND  = " + dtFH.Rows[0][""].ToString());
				
				//sets the images id for the draft but sets the dritory for the orginalid as all images should be where the orginal has them store
				ciFH.setFHID = Convert.ToInt32(dtFH.Rows[0][""].ToString());
				ciFH.setFHIDDir = Convert.ToInt32(dtFH.Rows[0][""].ToString());
			}//end of if
			else
			{
				//sets the id as the ID and Dirtor
				ciFH.setFHID = Convert.ToInt32(intFHID);
				ciFH.setFHIDDir = Convert.ToInt32(intFHID);
			}//end of else
			
			//loads the images into for display and allow the user upload more
			ciFH.loadImages();
			
			//checks if this FH has a user attach to it
			if(dtFHUser.Rows.Count > 0)
			{				
				//sets the basic informaiton for the suer
				lblUserFirstName.Text = dtFHUser.Rows[0][""].ToString();
				lblUserLastName.Text = dtFHUser.Rows[0][""].ToString();
				hlUserEmail.Text = dtFHUser.Rows[0][""].ToString();
				hlUserEmail.NavigateUrl = "mailto:" + dtFHUser.Rows[0][""].ToString();
				
				//checks if this user has data for this field
				if(!string.IsNullOrEmpty(dtFHUser.Rows[0][""].ToString().Trim()))
				{
					lblUserTitle.Text = dtFHUser.Rows[0][""].ToString();
					panUserTitle.Visible = true;
				}//end of if
								
				//checks if this user has data for this field
				if(!string.IsNullOrEmpty(dtFHUser.Rows[0][""].ToString().Trim()))
				{
					hlUserPhone.Text = dtFHUser.Rows[0][""].ToString();
					hlUserPhone.NavigateUrl = "tel:" + dtFHUser.Rows[0][""].ToString();
					panUserPhone.Visible = true;
				}//end of if
				
				//checks if this user has data for this field
				if(!string.IsNullOrEmpty(dtFHUser.Rows[0][""].ToString().Trim()))
				{
					lblUserFax.Text = dtFHUser.Rows[0][""].ToString();
					panUserFax.Visible = true;
				}//end of if
				
				//checks if this user has data for this field
				if(!string.IsNullOrEmpty(dtFHUser.Rows[0][""].ToString().Trim()))
				{
					lblUserCompany.Text = dtFHUser.Rows[0][""].ToString();
					panUserCompany.Visible = true;
				}//end of if
				
				//checks if this user has data for this field
				if(!string.IsNullOrEmpty(dtFHUser.Rows[0][""].ToString().Trim()))
				{
					lblUserAddress1.Text = dtFHUser.Rows[0][""].ToString();
					panUserAddress1.Visible = true;
				}//end of if
				
				//checks if this user has data for this field
				if(!string.IsNullOrEmpty(dtFHUser.Rows[0][""].ToString().Trim()))
				{
					lblUserAddress2.Text = dtFHUser.Rows[0][""].ToString();
					panUserAddress2.Visible = true;
				}//end of if
				
				//checks if this user has data for this field
				if(!string.IsNullOrEmpty(dtFHUser.Rows[0][""].ToString().Trim()))
				{
					lblUserCity.Text = dtFHUser.Rows[0][""].ToString();
					panUserCity.Visible = true;
				}//end of if
				
				//checks if this user has data for this field
				if(!string.IsNullOrEmpty(dtFHUser.Rows[0][""].ToString().Trim()))
				{
					lblUserActive.Text = dtFHUser.Rows[0][""].ToString();
					panUserActive.Visible = true;
				}//end of if
								
				//turns on the details for this user
				panUserAttach.Visible = true;
				panNoUserAttach.Visible = false;
				
				//sets the User ID for the Funeral Home
				lblUserID.Text = dtFHUser.Rows[0][""].ToString();
			
				//clears the Offergins as one FH will be different from another one
				chkLstOfferingText.Items.Clear();
				
				//sets the user id in order to do a where 
				strSQLOfferingsUserID = " OR  = " + lblUserID.Text;
			}//end of if
			
			//gets the all of the Default and this user's Offerings
			chkLstOfferingText.DataSource = DAL.getRow("","Where  = 'false'" + strSQLOfferingsUserID + " Order by ");
			chkLstOfferingText.DataBind();
									
			//sets the basic items
			lblID.Text = dtFH.Rows[0][""].ToString();
			lblOrginalID.Text = dtFH.Rows[0][""].ToString();
			txtFuneralName.Text = dtFH.Rows[0][""].ToString();
			lblUpdate.Text = dtFH.Rows[0][""].ToString();
			lblCreated.Text = dtFH.Rows[0][""].ToString();
			chkFHNew.Checked = Convert.ToBoolean(dtFH.Rows[0][""].ToString());
			
			//adds a client click to do a pop up in order to display a preview without having to load the page
			panReviewLogo.Visible = true;
			cmdReview.OnClientClick = "window.open('/funeralhome.aspx?=" + lblID.Text + "', ''); return false;";	
			
			//checks if there is a ddlLiveDraft has any items if so then display it in
			//the button in order for the user to know which review page they are cliking
			if(ddlLiveDraft.Items.Count > 0)
				cmdReview.Text = "Review (" + ddlLiveDraft.SelectedItem.Text + " View)";
	
			//checks if this cells content exits
			if(!string.IsNullOrEmpty(dtFH.Rows[0][""].ToString().Trim()))
				txtFuneralURL.Text = dtFH.Rows[0][""].ToString();
			
			//checks if this cells content exits
			if(!string.IsNullOrEmpty(dtFH.Rows[0][""].ToString().Trim()))
				txtAboutUs.Text = Server.HtmlDecode(dtFH.Rows[0][""].ToString());
			
			//checks if this cells content exits
			if(!string.IsNullOrEmpty(dtFH.Rows[0][""].ToString().Trim()))
				txtTagLine.Text = dtFH.Rows[0][""].ToString();
			
			//checks if this cells content exits
			if(!string.IsNullOrEmpty(dtFH.Rows[0][""].ToString().Trim()))
				txtFeneralInqueryEmail.Text = dtFH.Rows[0][""].ToString();
			
			//checks if this cells content exits
			if(!string.IsNullOrEmpty(dtFH.Rows[0][""].ToString().Trim()))
				txtFuneralAddress1.Text = dtFH.Rows[0][""].ToString();
			
			//checks if this cells content exits
			if(!string.IsNullOrEmpty(dtFH.Rows[0][""].ToString().Trim()))
				txtFuneralAddress2.Text = dtFH.Rows[0][""].ToString();
			
			//checks if this cells content exits
			if(!string.IsNullOrEmpty(dtFH.Rows[0][""].ToString().Trim()))
				txtFuneralCity.Text = dtFH.Rows[0][""].ToString();
						
			//checks if this cells content exits
			if(!string.IsNullOrEmpty(dtFH.Rows[0][""].ToString().Trim()))
				ddlFuneralPro.SelectedValue = dtFH.Rows[0][""].ToString();
			
			//checks if this cells content exits
			if(!string.IsNullOrEmpty(dtFH.Rows[0][""].ToString().Trim()))
				ddlFuneralCountry.SelectedValue = dtFH.Rows[0][""].ToString();
				
			//checks if this cells content exits
			if(!string.IsNullOrEmpty(dtFH.Rows[0][""].ToString().Trim()))
				txtFuneralPhone.Text = dtFH.Rows[0][""].ToString();
			
			///checks if this cells content exits
			if(!string.IsNullOrEmpty(dtFH.Rows[0][""].ToString().Trim()))
				txtFuneralFax.Text = dtFH.Rows[0][""].ToString();
				
			//checks if this cells content exits
			if(!string.IsNullOrEmpty(dtFH.Rows[0][""].ToString().Trim()))
			{
				ddlFHActive.SelectedValue = dtFH.Rows[0][""].ToString();
				lblStatus.Text = dtFH.Rows[0][""].ToString();
			}//end of if
						
			//checks if this cells content exits
			if(!string.IsNullOrEmpty(dtFH.Rows[0][""].ToString().Trim()))
				lblPublish.Text = dtFH.Rows[0][""].ToString();
				
			//checks if this cells content exits
			if(!string.IsNullOrEmpty(dtFH.Rows[0][""].ToString().Trim()))
				txtPC.Text = dtFH.Rows[0][""].ToString();
				
			DataTable dtProv = DAL.getRow("", "Where  = " + ddlFuneralPro.SelectedValue);//holds the Province Details
			string strJSLocationGeo = "getLocationGeo(getDocID('" + txtFuneralAddress1.ClientID + "').value + ',' + getDocID('" + txtFuneralCity.ClientID + "').value + '," + dtProv.Rows[0][""] + "',map,true,'" + hfMapLatitude.ClientID + "','" + hfMapLongitude.ClientID + "');";//holds the javascript function location 
			
			//adds the basic map optionals to both the onload and onclick
			litLocGeo.Text = "var latLngDefault = new google.maps.LatLng(43.68575,-79.37645);\n" + 
			"var mapOptions = {\n" + 
				"center: latLngDefault,\n" + 
				"mapTypeControl: true,\n" + 
				"mapTypeControlOptions: {\n" + 
				"    style: google.maps.MapTypeControlStyle.DROPDOWN_MENU\n" + 
				"},\n" + 
				"zoom: 15,\n" + 
				"zoomControl: true,\n" + 
				"zoomControlOptions: {\n" + 
				"    style: google.maps.ZoomControlStyle.SMALL\n" + 
				"},\n" + 
				"mapTypeId: google.maps.MapTypeId.ROADMAP\n" + 
			"}//holds the options of the map\n\n" + 
			"var map = new google.maps.Map(document.getElementById('divMap'), mapOptions);\n\n";
											
			//there maybe the times where the address is not found if so then this give the user the option
			//of finding the add again + "," + dtFHDetails.Rows[0][""]
			litLocGeoAgain.Text = litLocGeo.Text + strJSLocationGeo;
					
			//checks if there is a already a location that the user wants to use
			if(string.IsNullOrEmpty(dtFH.Rows[0][""].ToString().Trim()) && !string.IsNullOrEmpty(txtFuneralAddress1.Text) && !string.IsNullOrEmpty(txtFuneralCity.Text))								
				//finds the location base on the funeral homes address
				litLocGeo.Text += litLocGeoAgain.Text;
			else
			{
				string strDefault = "43.64100156269233,-79.38599562435303";//holds the default location in case there is no address to find
				string[] arrLatLong = strDefault.Split(',');//holds the latitude and longitude
				
				//checks if there is a map location to used instead of the default
				if(!string.IsNullOrEmpty(dtFH.Rows[0][""].ToString().Trim()) && dtFH.Rows[0][""].ToString().Trim() != ",")
					arrLatLong = dtFH.Rows[0][""].ToString().Split(',');
				
				//makes a pin in the location that the user want it to be
				litLocGeo.Text += "//centers the map to the location\n" + 
				"map.setCenter(new google.maps.LatLng(" + arrLatLong[0] + "," + arrLatLong[1] + "));\n" + 
				"var marker = new google.maps.Marker({\n" + 
					"draggable: true,\n" + 
					"map: map,\n" + 
					"position: new google.maps.LatLng(" + arrLatLong[0] + "," + arrLatLong[1] + ")\n" + 
				"});//end of marker\n" + 
				"//sets the defualt for the latitude and the longitude\n" + 
				"getDocID('" + hfMapLatitude.ClientID + "').value = '" + arrLatLong[0] + "';\n" + 
				"getDocID('" + hfMapLongitude.ClientID + "').value = '" + arrLatLong[1] + "';\n" + 
				"//sets an event to change the latitude and longitude hidden fields\n" + 		
				"google.maps.event.addListener(marker, 'dragend', function (event) {\n" + 
					"getDocID('" + hfMapLatitude.ClientID + "').value = this.getPosition().lat();\n" + 
					"getDocID('" + hfMapLongitude.ClientID + "').value = this.getPosition().lng();\n" + 
				"});";
			}//end of else
						
			//checks if there is a funeral logo if so then display it in the image banner
			if(!string.IsNullOrEmpty(dtFH.Rows[0][""].ToString().Trim()))
			{
				//turns on the Current Logo section and displays the image
				panCurrentLogo.Visible = true;
				lblImageSource.Text =  dtFH.Rows[0][""].ToString();
				
				//checks if this is a Draft
				if(dtFH.Rows[0][""].ToString() != "0")
					//get draft logo image instead
					imgLogo.ImageUrl ="/images/FH/" + lblOrginalID.Text + "/Draft/" + dtFH.Rows[0][""].ToString();
				else
					//gets the current main image
					imgLogo.ImageUrl ="/images/FH/" + lblID.Text + "/" + dtFH.Rows[0][""].ToString();
			}//end of if
							
			//goes around each Offering connect this funeral home and make it selected
			foreach (DataRow drFHOfferings in dtFHOfferings.Rows)
			{
				ListItem liCheckBox = chkLstOfferingText.Items.FindByValue(drFHOfferings[""].ToString());//holds the location of the Offering in chkLstOfferings
				
				//checks if this Offering is in chkLstOfferings
				if (liCheckBox != null)
					//makes this list item selected
					liCheckBox.Selected = true;
			}//end of foreach
							
			//goes around each affiliation connect this funeral home and make it selected
			foreach (DataRow drFHAffiliations in dtFHAffiliations.Rows)
			{
				ListItem liCheckBox = chkLstAffiliations.Items.FindByValue(drFHAffiliations[""].ToString());//holds the location of the affiliation in chkLstAffiliations
				
				//checks if this affiliation is in chkLstAffiliations
				if (liCheckBox != null)
					//makes this list item selected
					liCheckBox.Selected = true;
			}//end of foreach
						
			//checks if this is a Draft
			if(dtFH.Rows[0][""].ToString() != "0")
			{
				//chanages the button to do a approveal instead as the draft will need to merge with the orginal FH
				//if approve this will make the code less complex hopfully
				cmdApprove.Visible = true;
				cmdSave.Visible = false;
			}//end of if
			else
			{
				//changes the approve button back to save this is the default save button
				cmdApprove.Visible = false;
				cmdSave.Visible = true;
				cmdSave.Text = "Update";
			}//end of else
		}//end of try
		catch (Exception ex)
		{
			lblError.Text = ex.Message;
			lblError.Visible = true;
		}//end of catch
	}//end of loadUpdateDate()
	
	//resets the add and Update section
	private void resetAddUpdateSection()
	{
		try
		{
			//clears all of the Items in the Fourm
			lblID.Text = "";
			lblImageSource.Text =  "";
			lblStatus.Text = "";
			lblUserFirstName.Text = "";
			lblUserLastName.Text = "";
			lblUserTitle.Text = "";
			lblUserFax.Text = "";
			lblUserCompany.Text = "";
			lblUserAddress1.Text = "";
			lblUserAddress2.Text = "";
			lblUserCity.Text = "";
			lblUserProvince.Text = "";
			lblUserCountry.Text = "";
			lblUserActive.Text = "";
			txtFuneralName.Text = "";
			txtFuneralURL.Text = "";
			txtAboutUs.Text = "";
			txtTagLine.Text = "";
			txtFeneralInqueryEmail.Text = "";
			txtFuneralAddress1.Text = "";
			txtFuneralAddress2.Text = "";
			txtFuneralCity.Text = "";
			txtFuneralPhone.Text = "";
			txtFuneralFax.Text = "";
			txtPC.Text = "";
			hlUserPhone.Text = "";
			hlUserPhone.NavigateUrl = "";
			hlUserEmail.Text = "";
			hlUserEmail.NavigateUrl = "";
			imgLogo.ImageUrl = "";
			hfOtherOfferingsValue.Value = "";
			hfOtherOfferings.Value = "1";
			ddlFuneralPro.SelectedValue = "1";
			ddlFuneralCountry.SelectedValue = "1";
			ddlFHActive.SelectedValue = "-1";
			chkFHNew.Checked = false;
			ciFH.Visible = false;
			panUserActive.Visible = false;		
			panCurrentLogo.Visible = false;
			panOtherOfferings.Visible = false;
			panUserAttach.Visible = false;
			panNoUserAttach.Visible = true;
			panUserTitle.Visible = false;
			panUserPhone.Visible = false;
			panUserFax.Visible = false;
			panUserCompany.Visible = false;
			panUserAddress1.Visible = false;
			panUserAddress2.Visible = false;
			panUserCity.Visible = false;
			panUserProvince.Visible = false;
			panUserCountry.Visible = false;
			phOtherOfferings.Controls.Clear();
									
			//clear all for offering from chkLstOfferingText and recreted them again
			//for the next time the offerings is needed with out having the custom offerings from the
			//last offering
			chkLstOfferingText.DataSource = DAL.getRow("","Order by ");
			chkLstOfferingText.DataBind();
			
			//uncheck all for affiliations
			foreach (ListItem liCheckbox in chkLstAffiliations.Items)
			{
				//uncheck all items
				liCheckbox.Selected = false;
			}//end of foreach
		}//end of try
		catch (Exception ex)
		{
			lblError.Text = ex.Message;
			lblError.Visible = true;
		}//end of catch
	}//end of resetAddUpdateSection()
	
	//Send email and approving user if they are not only approved
    private void sendEmailFH(int intFHID)
    {
		try
		{
			string strUserPassword = General.genPassword();//holds the random password that will be sent to the user
			DataTable dtFHUser = DAL.getRow("", "where  = 1 AND  = " + intFHID);//holds the details of the FH and the User
			
			//resets the error message
			lblGirdError.Visible = false;
			
			//checks if htere is a user attach to this FH
			if (dtFHUser.Rows.Count > 0 && (!string.IsNullOrEmpty(dtFHUser.Rows[0][""].ToString())))
			{				
				//checks if there is a any email to use
				if (!string.IsNullOrEmpty(dtFHUser.Rows[0][""].ToString()))
					//sends an email to the obituaries tell them to apporvle this funeral home and user
					General.sendHTMLMail(dtFHUser.Rows[0][""].ToString(), "Your Account is Approved!", string.Format(File.ReadAllText(Server.MapPath("~/EmailTemplate/FHSignUpApproved.html")), dtFHUser.Rows[0][""].ToString(), dtFHUser.Rows[0][""].ToString(), dtFHUser.Rows[0][""].ToString(), strUserPassword, dtFHUser.Rows[0][""].ToString()));
				else
					throw new Exception("The email is empty");
					
				//Reset password and approving user
				DAL.addUpdateUsers(Convert.ToInt32(dtFHUser.Rows[0][""].ToString()), dtFHUser.Rows[0][""].ToString(), dtFHUser.Rows[0][""].ToString(), dtFHUser.Rows[0][""].ToString(), dtFHUser.Rows[0][""].ToString(), dtFHUser.Rows[0][""].ToString(), dtFHUser.Rows[0][""].ToString(), dtFHUser.Rows[0][""].ToString(), dtFHUser.Rows[0][""].ToString(), dtFHUser.Rows[0][""].ToString(), dtFHUser.Rows[0][""].ToString(), dtFHUser.Rows[0][""].ToString(), dtFHUser.Rows[0][""].ToString(), PasswordHash.passwordHashtable.createHash(strUserPassword), "", Convert.ToInt32(dtFHUser.Rows[0][""].ToString()), true, Convert.ToBoolean(dtFHUser.Rows[0][""].ToString()));
			}//end of if
			else
				throw new Exception("There are no users registered for this Funeral Home. Therefore, email was sent out.");
		}//end of try
		catch (Exception ex)
		{
			lblGirdError.Text = ex.Message;
			lblGirdError.Visible = true;
		}//end of catch
    }//end of sendEmailFH()
	
	protected void Page_PreRender(object sender, EventArgs e)
    {
		//sets strSearchWhere before the page is rendered.
	    ViewState.Add("viewStateSearch", strSearchWhere);

        //BIND PAGE
        BindDesignsPanel();
		
		if (IsPostBack)
		{
			string[] arrOtherOfferings = hfOtherOfferingsValue.Value.Split(new string[] {"#2@"}, StringSplitOptions.None);//holds the values of the other offerings
			
			//resets hfOtherOfferingsValue
			hfOtherOfferingsValue.Value = "";
			
			//goes around adding number of items back into phOtherOfferings
			for(int intIndex = 0;intIndex < Convert.ToInt32(hfOtherOfferings.Value);intIndex++)
			{
				TextBox txtNewOtherOffering = new TextBox();//holds the new Other Offering textbox
				
				//adds the id and any text for this offering
				txtNewOtherOffering.ID = "txtOtherOfferings" + intIndex;
				txtNewOtherOffering.Text = arrOtherOfferings[intIndex];
				txtNewOtherOffering.Attributes.Add("onchange", "changeFHOffering(getDocID('" + hfOtherOfferingsValue.ClientID + "'),getDocID('dnn_ctr585_ReviewFuneralHomes_" + txtNewOtherOffering.ID + "'))");
				
				//sets the value into hfOtherOfferingsValue as a way of remebering what was the values 
				//after the reload
				hfOtherOfferingsValue.Value += arrOtherOfferings[intIndex] + "#2@";
				
				//adds the new offering to phOtherOfferings
				phOtherOfferings.Controls.Add(txtNewOtherOffering);
				phOtherOfferings.Controls.Add(new LiteralControl("<br/>"));
			}//end of for loop
		}//end of else
		else
		{
			//gets the Country
			ddlFuneralCountry.DataSource = DAL.getRow("","");
			ddlFuneralCountry.DataBind();
					
			//gets the all of the Canada's Province as the default selection
			ddlFuneralPro.DataSource = DAL.getRow("","Where  = 1 Order by ");
			ddlFuneralPro.DataBind();
		}//end of else
    }//end of Page_PreRender()
				
	protected void Page_Load(object sender, EventArgs e)
	{
		_PageSize = (_Cols * _Rows);
		
        if (!Page.IsPostBack)
        {
			//INITIALIZE SEARCH CRITERIA ON FIRST VISIT
			//checks if there is a p to uses it else then give it a zero
			if(!string.IsNullOrEmpty(Request.QueryString["p"]))
				//INITIALIZE SEARCH CRITERIA ON FIRST VISIT
				HiddenPageIndex.Value = Request.QueryString["p"];
			else
				//INITIALIZE SEARCH CRITERIA ON FIRST VISIT
				HiddenPageIndex.Value = "0";
				
			//checks if there is a search text if so then update the search textbox
			if(!string.IsNullOrEmpty(Request.QueryString["search"]))
			{
				txtSearch.Text = Server.UrlDecode(Request.QueryString["search"]);
				
				//adds the strWhere content agian for this page
				strSearchWhere = " LIKE '%" + DAL.safeSql(txtSearch.Text) + "%' AND  = 0";
			}//end of if
			else
				//resets strSearch
				strSearchWhere = " = 0";
				
			//checks if there is a sort
			if(!string.IsNullOrEmpty(Request.QueryString["s"]))
				strSearchSort = Server.UrlDecode(Request.QueryString["s"]);
			else
				//sets the default
				strSearchSort = " DESC";//", ";
							
            //gets the Affiliations currently in the database
			DataTable dtAffiliation = DAL.getData("dbo.");
			
			if (dtAffiliation != null && dtAffiliation.Rows.Count > 0)
			{
				foreach (DataRow drAffiliation in dtAffiliation.Rows)
				{
					FileInfo file = new System.IO.FileInfo(drAffiliation[""].ToString());
					
					ListItem item = new ListItem("<img src='" + "/portals/_default/skins/obit/images/affiliates/" + file.Name + "' alt='" + drAffiliation[""].ToString() + "' title='" + drAffiliation[""].ToString() + "' />&nbsp;&nbsp;" + drAffiliation[""].ToString(), drAffiliation[""].ToString());
					
					chkLstAffiliations.Items.Add(item);
				}//end of foreach
			}//end of if
        }//end of if
		
		//checks if there is something in viewState for where if so then
		if (ViewState["viewStateSearch"] != null)
			//sets the search where for the search
			strSearchWhere = (string)ViewState["viewStateSearch"];
		else			
			SetPagerIndex();
	}//end of Page_Load()
			
	protected void gdFH_ItemDataBound(object sender, DataGridItemEventArgs e)
	{
		DataRowView drvData = (DataRowView)e.Item.DataItem;//holds the data from the DataGrid
		
		if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
		{
		}//end of if
		
		//checks if there is any data
        if (drvData != null)
        {
			CheckBox chkApprove = (CheckBox)e.Item.FindControl("chkApprove");//holds the display if the FH is approve
			CheckBox chkPublish = (CheckBox)e.Item.FindControl("chkPublish");//holds the display if the FH is publish
			CheckBox chkSuspended = (CheckBox)e.Item.FindControl("chkSuspended");//holds the display if the FH is suspended
			CheckBox chkDraft = (CheckBox)e.Item.FindControl("chkDraft");//holds the display if the FH is a draft
			CheckBox chkCreated = (CheckBox)e.Item.FindControl("chkCreated");//holds the display if the FH is a created
            Button cmdNotice = (Button)e.Item.FindControl("cmdNotice");//holds the send notice button
			Label lblNotice = (Label)e.Item.FindControl("lblNotice");//holds the notice of the person who is attach this FH
			Label lblFullAddress = (Label)e.Item.FindControl("lblFullAddress");//holds the full address of the FH
			DataTable dtFHUser = DAL.getRow("", "where  = 1 AND  = " + drvData[""].ToString() + " OR  = " + drvData[""].ToString() + " order by  DESC");//holds the details of the FH and the User
			DataTable dtFH = DAL.getRow("", "where  = " + drvData[""].ToString() + " OR  = " + drvData[""].ToString());//holds the details of the FH
						
			//sets the full address
			lblFullAddress.Text = dtFH.Rows[0][""] + ", " + dtFH.Rows[0][""] + ", " + dtFH.Rows[0][""] + ", " + dtFH.Rows[0][""];
			
			//checks if this is there is a draft as there should be two versions of the FH
			if(dtFHUser.Rows.Count > 1 && dtFHUser.Rows[1][""].ToString() == "-2")
				//turns on the draft notifted
				chkDraft.Checked = true;
			
			//checks if this FH is publish by checking if there is a Publish date
			if(!string.IsNullOrEmpty(drvData[""].ToString()))
				//if it is publish then do a checkmark for the publish
				chkPublish.Checked = true;
			
			//checks if there is a user and a Publish Date for Created to display as it does looks wired
			if(!string.IsNullOrEmpty(drvData[""].ToString()) || dtFHUser.Rows.Count > 1)
				//client request in order to tell that the user has created a draft for the first time
				//as the client is unable to see that if publish is not checked and draft is check then
				//the user has created a draft for the frist time this is some what pointless
				chkCreated.Checked = true;
				
			//checks if this FH is suspended by if the status is 2 for suspended
			if(drvData[""].ToString() == "2")
				//if it is suspended then do a checkmark for the suspended
				chkSuspended.Checked = true;
				
			//checks if there is the FH is active if so then a noticed does not need to be sent
            if (dtFHUser.Rows.Count > 0 && !string.IsNullOrEmpty(dtFHUser.Rows[0][""].ToString()) && Convert.ToBoolean(dtFHUser.Rows[0][""].ToString()) == true)
			{
				//remvoes the notice and adds a checkmark that this has been approve
				cmdNotice.Visible = false;
				chkApprove.Checked = true;
			}//end of if
			else if (dtFHUser.Rows.Count > 0 && !string.IsNullOrEmpty(dtFHUser.Rows[0][""].ToString()))
				cmdNotice.Visible = true;
			else
			{
				//removes the notice button and tells the user that there is no user attach to this FH
				//lblNotice.Visible = true;
				cmdNotice.Visible = false;
			}//end of if
        }//end of if
	}//end of gdFH_ItemDataBound()
	
	protected void gdFH_ItemCommand(object sender, DataGridCommandEventArgs e)
	{
		//checks which Command to use
        if (e.CommandName == "Delete")
        {
			//removes the FH images from the system
			if (Directory.Exists(Server.MapPath(".\\images\\FH\\" + e.Item.Cells[0].Text)))
			{
				DirectoryInfo diFH = new DirectoryInfo(Server.MapPath(".\\images\\FH\\" + e.Item.Cells[0].Text));//holds the FH dirtory where all of the images are located for this FH
				
				//deletes the FH directory
				diFH.Delete(true);
			}//end of if
			
            //Deltes a Row from FH table
            DAL.deleteFuneralHome(Convert.ToInt32(e.Item.Cells[0].Text));

			//reloads the FH
            cmdCancel_Click(sender, e);
        }//end of if
		
		if (e.CommandName == "Notice")	
			//Send email to Funeral Home that they have been approved by the Admin
			sendEmailFH(Convert.ToInt32(e.Item.Cells[0].Text));
		
        if (e.CommandName == "Update")
        {
			DataTable dtFH = DAL.getRow("", "where  = " + e.Item.Cells[0].Text + " OR  = " + e.Item.Cells[0].Text);//holds the details of the FH
			
			//loads the FH content into the Update section
			loadUpdateDate(Convert.ToInt32(e.Item.Cells[0].Text));
									
			//loads the live and draft ddlLiveDraft
			ddlLiveDraft.Items.Add(new ListItem("Publish", e.Item.Cells[0].Text));
			
			//checks if this is ready to be looked at if so then give them the option
			if(dtFH != null && dtFH.Rows.Count > 1 && dtFH.Rows[1][""].ToString() == "-2")
			{
				//sets the id of the draft in order to load if it is selected
				ddlLiveDraft.Items.Add(new ListItem("Edit Request", dtFH.Rows[1][""].ToString()));
				panLiveDraft.Visible = true;
			}//end of if
			
			//checks if there is a ddlLiveDraft has any items if so then display it in
			//the button in order for the user to know which review page they are cliking
			if(ddlLiveDraft.Items.Count > 0)
				cmdReview.Text = "Review (" + ddlLiveDraft.SelectedItem.Text + " View)";
				
			//sets if the require fields are needed as inactive status the admin may just need to make
			//a change to a FH where no user is attach to or just needs to appove it to be added to
			//user account
			ddlFHActive_SelectedIndexChanged(sender, e);
					
			//changes the layout for Updating
			panFHDisplay.Visible = false;
			panFHAdd.Visible = true;
			cmdCancel.Visible = true;
							
			lblMainTitle.Text = "Fill the form to update the Item data";
        }//end of if
	}//end of gdFH_ItemCommand()
			
	protected void cmdSave_Click(object sender, EventArgs e)
	{
		try
		{
			//checks if the page is valid if so then prcess the event
			if (Page.IsValid)
			{
				string strOfferings = "";//holds the all of the offerings the user wants
				string strAffiliations = "";//holds the all of the affiliations the user wants
				string[] arrOtherOfferings = hfOtherOfferingsValue.Value.Split(new string[] {"#2@"}, StringSplitOptions.RemoveEmptyEntries);//holds the values of the other offerings
				
				//goes around checking which offerings the user wants
				foreach (ListItem liOfferings in chkLstOfferingText.Items)
				{
					//checks if the user wants this offering
					if (liOfferings.Selected)
						strOfferings += liOfferings.Value + "*";
				}//end of foreach
				
				//goes around checking which affiliations the user wants
				foreach (ListItem liAffiliations in chkLstAffiliations.Items)
				{
					//checks if the user wants this affiliations
					if (liAffiliations.Selected)
						strAffiliations += liAffiliations.Value + "*";
				}//end of foreach
				
				//checks if there is any offerings in if so then go on if not then tell the user so
				if(!string.IsNullOrEmpty(strOfferings) || arrOtherOfferings.Length > 0 || Convert.ToInt32(ddlFHActive.SelectedValue) == -1)
				{
					string[] arrFHAffilation = strAffiliations.Split('*');//holds each affliation that is in this FH
					
					//checks if arrFHAffilation is less then three as 
					//the user can only added to have three affilation attach to a FH
					if((arrFHAffilation.Length - 1) < 4)
					{
						//turns off the error message
						lblError.Visible = false;
			
						string strImageLoc = "";//holds the loction for the image
						string strLangEdit = "create";//holds the if this is a create or edit for the email and display
						int intFHID = 0;//hold the FHID if the user has createed
						string[] arrFHOffering = strOfferings.Split('*');//holds each offering that is in this FH						
						
						//Change if they what to changes the Main Image of the Category
						if (fuLogo.HasFile) 
						{
							string strLogoFilePath = "/images/FH/" + lblID.Text + "/";//holds the dirtory
							
							//checks if there is a dirtory for this Funeral Home logo in it if not then create one
							if(!Directory.Exists(Server.MapPath("~/" + strLogoFilePath.Replace("Draft/", ""))))
							{
								//creates dirtory for where the main image folder and its draft folder
								Directory.CreateDirectory(Server.MapPath("~/" + strLogoFilePath.Replace("Draft/", "")));
								Directory.CreateDirectory(Server.MapPath("~/" + strLogoFilePath));
							}//end of if
							//checks if there is a draft folder 
							else if(!Directory.Exists(Server.MapPath("~/" + strLogoFilePath)))
								//creates a draft folder for the drafts
								Directory.CreateDirectory(Server.MapPath("~/" + strLogoFilePath));
															
							//checks if this FH has a FH if so then delete it form the draft has it is not needed
							if (!string.IsNullOrEmpty(lblImageSource.Text) && File.Exists(Server.MapPath("~/" + strLogoFilePath + lblImageSource.Text)))
							{
								File.Delete(Server.MapPath("~/" + strLogoFilePath + lblImageSource.Text));
															
								//checks if this FH has already has a logo thumbnail
								if (!string.IsNullOrEmpty(lblImageSource.Text.Replace(".","_upload_thumbnail.")) && File.Exists(Server.MapPath("~/" + strLogoFilePath + lblImageSource.Text.Replace(".","_upload_thumbnail."))))
									//delete it form the draft has it is not needed
									File.Delete(Server.MapPath("~/" + strLogoFilePath + lblImageSource.Text.Replace(".","_upload_thumbnail.")));
							}//end of if
			
							//uplaods the Logo Image to the site
							strImageLoc = General.uploadImage(strLogoFilePath, fuLogo.PostedFile, 96, 96);
							
							//checks if there was an error with the upliad
							if(strImageLoc.IndexOf("ERROR! ") >= 0)
								throw new Exception(strImageLoc);
							else
								//removes the strLogoFilePath from the start of the start of the files in order 
								//to make it more flexiable to use in different places
								strImageLoc = strImageLoc.Replace(strLogoFilePath, "");
						}//end of if
						else
							strImageLoc = lblImageSource.Text;
			
						//checks if there if this is an updated as only an actully FH in the database can have images
						if(!string.IsNullOrEmpty(lblID.Text))
						{
							string[] arrImageOrder = ciFH.getImages();//holds all of the images in the order the user wants to display them in
								
							//goes around find the image using the name and the FH ID in the database 
							//then updates the order
							for(int intIndex = 0;intIndex < arrImageOrder.Length;intIndex++)
							{
								DataTable dtFHImageDetails = DAL.getRow("","Where  = " + lblID.Text + " AND  = '" + arrImageOrder[intIndex] + "'");//holds the Funeral Home and User details
								
								//updates this image order
								DAL.updateFuneralHomeImageOrder(Convert.ToInt32(dtFHImageDetails.Rows[0][""].ToString()), (intIndex + 1));
							}//end of for loop
						}//end of if
													
						//checks if there is a id if so then do an Update on the FH if not then do Insert 
						//.Replace("&lt;/p&gt;","&lt;br&gt;").Replace("&lt;p&gt;","")
						if(!string.IsNullOrEmpty(lblID.Text))
							intFHID = DAL.addUpdateFuneralHome(Convert.ToInt32(lblID.Text), txtFuneralName.Text.Trim(), txtFuneralURL.Text.Trim(), txtFuneralAddress1.Text.Trim(), txtFuneralAddress2.Text.Trim(), txtFuneralCity.Text.Trim(), ddlFuneralPro.SelectedValue, ddlFuneralCountry.SelectedValue, txtFuneralPhone.Text.Trim(), txtFuneralFax.Text.Trim(), txtPC.Text.Trim(), chkFHNew.Checked, strImageLoc, txtAboutUs.Text.Trim(), txtTagLine.Text.Trim(), hfMapLatitude.Value + "," + hfMapLongitude.Value, txtFeneralInqueryEmail.Text.Trim(), Convert.ToInt32(ddlFHActive.SelectedValue));
						else
							intFHID = DAL.addUpdateFuneralHome(0, txtFuneralName.Text.Trim(), txtFuneralURL.Text.Trim(), txtFuneralAddress1.Text.Trim(), txtFuneralAddress2.Text.Trim(), txtFuneralCity.Text.Trim(), ddlFuneralPro.SelectedValue, ddlFuneralCountry.SelectedValue, txtFuneralPhone.Text.Trim(), txtFuneralFax.Text.Trim(), txtPC.Text.Trim(), chkFHNew.Checked, strImageLoc, txtAboutUs.Text.Trim(), txtTagLine.Text.Trim(), hfMapLatitude.Value + "," + hfMapLongitude.Value, txtFeneralInqueryEmail.Text.Trim(), Convert.ToInt32(ddlFHActive.SelectedValue));
							
						DataTable dtFHUserDetails = DAL.getRow("", "where  = 1 AND  = " + intFHID);//holds the details of the FH and the User
						
						//removes a link all offerings to this funeral home in order to enter it again
						//if the user has selected it
						DAL.addUpdateLinkTableFHOfferings(Convert.ToInt32(intFHID),0,-1);
							
						//goes around each checking if a Offering number is in there
						for(int intIndex = 0;intIndex < arrFHOffering.Length - 1;intIndex++)
						{
							DataTable dtOfferingLink = DAL.getData(" Where  = " + intFHID + " AND  = " + arrFHOffering[intIndex]);//holds if the Offering id is already link with this funeral home
					
							//check if the Offering and funeral home are already link 
							if (dtOfferingLink == null)
								//adds a link for this offerings ID to this funeral home
								DAL.addUpdateLinkTableFHOfferings(intFHID,Convert.ToInt32(arrFHOffering[intIndex]),0);
						}//end of for loop
	
						//goes around adding the users offerings to the database
						for(int intIndex = 0;intIndex < arrOtherOfferings.Length;intIndex++)
						{
							//adds the user's offering to the database and then adds the offering to this FH
							DAL.addUpdateLinkTableFHOfferings(intFHID, DAL.addUpdateOfferings(0, arrOtherOfferings[intIndex], "", true, Convert.ToInt32(lblUserID.Text)), 0);
						}//end of for loop
						
						//removes a link all Affiliations to this funeral home in order to enter it again
						//if the user has selected it
						DAL.addUpdateLinkTableFHAffiliations(intFHID,0,-1);
		
						//goes around each checking if a affiliation number is in there
						for(int intIndex = 0;intIndex < arrFHAffilation.Length - 1;intIndex++)
						{
							DataTable dtAffiliationLink = DAL.getData(" Where  = " + intFHID + " AND  = " + arrFHAffilation[intIndex]);//holds if the affiliation id is already link with this funeral home
					
							//check if the affiliation and funeral home are already link 
							if (dtAffiliationLink == null)
								//adds a link for this Affiliations ID to this funeral home
								DAL.addUpdateLinkTableFHAffiliations(intFHID, Convert.ToInt32(arrFHAffilation[intIndex]), 0);
						}//end of for loop

						//checks if there is a publish date as this means it was already publish
						if(dtFHUserDetails.Rows.Count > 0 && !string.IsNullOrEmpty(dtFHUserDetails.Rows[0][""].ToString()))
							//changes strLangEdit to edit for the email and display
							strLangEdit = "edit";
																					
						//checks if the oginal was sign up request or add to the user as the email sent are different
						if(lblStatus.Text == "-2" && ddlFHActive.SelectedValue != "-2" && dtFHUserDetails.Rows.Count > 0)
							//sends an email to tell the user that there additional FH is approvaed
							General.sendHTMLMail(dtFHUserDetails.Rows[0][""].ToString(), "Your Request is Approved.", string.Format(File.ReadAllText(Server.MapPath("~/EmailTemplate/FHAddThankYouPublish.html")), dtFHUserDetails.Rows[0][""].ToString(), dtFHUserDetails.Rows[0][""].ToString(),dtFHUserDetails.Rows[0][""].ToString(), strLangEdit));
												
						//resets the page
						cmdCancel_Click(sender,e);
					}//end of if
					else
						throw new Exception("You can only have three affiliations attach to this funeral home");
				}//end of if
				else
					throw new Exception("You must have a offering");
			}//end of if
		}//end of try
		catch (Exception ex)
		{
			lblError.Text = ex.Message;
			lblError.Visible = true;
		}//end of catch
	}//end of cmdSave_Click()
	
	protected void cmdApprove_Click(object sender, EventArgs e)
	{	
		try
		{
			string strOfferings = "";//holds the all of the offerings the user wants
			string strAffiliations = "";//holds the all of the affiliations the user wants
			string[] arrOtherOfferings = hfOtherOfferingsValue.Value.Split(new string[] {"#2@"}, StringSplitOptions.RemoveEmptyEntries);//holds the values of the other offerings
					
			//goes around checking which offerings the user wants
			foreach (ListItem liOfferings in chkLstOfferingText.Items)
			{
				//checks if the user wants this offering
				if (liOfferings.Selected)
					strOfferings += liOfferings.Value + "*";
			}//end of foreach
			
			//goes around checking which affiliations the user wants
			foreach (ListItem liAffiliations in chkLstAffiliations.Items)
			{
				//checks if the user wants this affiliations
				if (liAffiliations.Selected)
					strAffiliations += liAffiliations.Value + "*";
			}//end of foreach
			
			//checks if there is any offerings in if so then go on if not then tell the user so
			if(!string.IsNullOrEmpty(strOfferings) || arrOtherOfferings.Length > 0)
			{
				string[] arrFHAffilation = strAffiliations.Split('*');//holds each affliation that is in this FH
				
				//checks if arrFHAffilation is less then three as 
				//the user can only added to have three affilation attach to a FH
				if((arrFHAffilation.Length - 1) < 4)
				{
					//turns off the error message
					lblError.Visible = false;
		  
					int intCurrentStatus = Convert.ToInt32(ddlFHActive.SelectedValue);//holds what is the current status
					int intFHID = 0;//hold the FHID if the user has createed
					string strImageLoc = "";//holds the loction for the image
					string strLogoFilePath = "/images/FH/" + lblOrginalID.Text + "/Draft/";//holds the dirtory
					string strLangEdit = "create";//holds the if this is a create or edit for the email and display
					string[] arrImageOrder = ciFH.getImages();//holds all of images in order the user wants to display
					string[] arrLiveImageFiles = Directory.GetFiles(Server.MapPath("~/" + strLogoFilePath.Replace("Draft/", "") + "/"));//holds the live images files
					string[] arrFHOffering = strOfferings.Split('*');//holds each offering that is in this FH
										
					//goes around find the image using the name and the FH ID in the database 
					//then updates the order for the last time in order to get current order
					for(int intIndex = 0;intIndex < arrImageOrder.Length;intIndex++)
					{
						DataTable dtFHImageDetails = DAL.getRow("","Where  = " + lblID.Text + " AND  = '" + arrImageOrder[intIndex] + "'");//holds the Image Details
						
						//updates this image order
						DAL.updateFuneralHomeImageOrder(Convert.ToInt32(dtFHImageDetails.Rows[0][""].ToString()), (intIndex + 1));
					}//end of for loop				
					
					DataTable dtImage = DAL.getRow("", "Where  = " + lblID.Text + " Order by ");//holds all of the images for this FH 
					
					//checks if the current status is under review if so then changes it to activate as this draft will be delete and the main one should not be under review but turn on
					if(intCurrentStatus == -2)
						intCurrentStatus = 1;
						
					//goes around for each file in arrLiveImageFiles and deletes it
					foreach (string strImageName in arrLiveImageFiles)
					{					
						//checks if the image is in the live dirtory
						if (File.Exists(strImageName))
							//deletes the live images as to make room for the in draft images
							File.Delete(strImageName);
					}//end of foreach
										
					//resets the images that belong to the orginal FH in order to add in the current images
					DAL.deleteAllFuneralHomeImage(Convert.ToInt32(lblOrginalID.Text));
					
					//Change if they what to changes the Logo
					if (fuLogo.HasFile) 
					{
						//checks if this FH has already logo
						if (!string.IsNullOrEmpty(lblImageSource.Text) && File.Exists(Server.MapPath("~/" + strLogoFilePath + lblImageSource.Text)))
						{
							//delete logo form the draft has it is not needed anymore
							File.Delete(Server.MapPath("~/" + strLogoFilePath + lblImageSource.Text));
							
							//checks if this FH has already has a logo thumbnail
							if (!string.IsNullOrEmpty(lblImageSource.Text.Replace(".","_upload_thumbnail.")) && File.Exists(Server.MapPath("~/" + strLogoFilePath + lblImageSource.Text.Replace(".","_upload_thumbnail."))))
								//delete the logo thumbnail form the draft has it is not needed anymore
								File.Delete(Server.MapPath("~/" + strLogoFilePath + lblImageSource.Text.Replace(".","_upload_thumbnail.")));
						}//end of if
		  
						//uplaods the Logo Image to the site
						strImageLoc = General.uploadImage(strLogoFilePath, fuLogo.PostedFile, 96, 96);
						
						//checks if there was an error with the upliad
						if(strImageLoc.IndexOf("ERROR! ") >= 0)
							throw new Exception(strImageLoc);
						else
							//removes the strLogoFilePath from the start of the start of the files in order 
							//to make it more flexiable to use in different places
							strImageLoc = strImageLoc.Replace(strLogoFilePath, "");
						
						//move the logo from draft to live
						//this is an add error as it does save image however when save to the database 
						//all / are remove for the image name
						File.Move(Server.MapPath("~/" + strLogoFilePath + strImageLoc), Server.MapPath("~/" + strLogoFilePath.Replace("Draft/", "") + "/" + strImageLoc));
						
						//move the logo thumbnail from draft to live
						File.Move(Server.MapPath("~/" + strLogoFilePath + strImageLoc.Replace(".","_upload_thumbnail.")), Server.MapPath("~/" + strLogoFilePath.Replace("Draft/", "") + "/" + strImageLoc.Replace(".","_upload_thumbnail.")));
					}//end of if
					else
					{
						//gets existing image to the site
						strImageLoc = lblImageSource.Text;
						
						//checks if the file is already in the live if not then move it to the draft to live
						if(File.Exists(Server.MapPath("~/" + strLogoFilePath + strImageLoc)) && !string.IsNullOrEmpty(strImageLoc))
						{
							//move the logo from draft to live
							File.Move(Server.MapPath("~/" + strLogoFilePath + strImageLoc), Server.MapPath("~/" + strLogoFilePath.Replace("Draft/", "") + "/" + strImageLoc));
							
							//checks if there logo thumbnail is in the folder as well
							if (File.Exists(Server.MapPath("~/" + strLogoFilePath + lblImageSource.Text.Replace(".","_upload_thumbnail."))))
								//move the logo thumbnail from draft to live
								File.Move(Server.MapPath("~/" + strLogoFilePath + strImageLoc.Replace(".","_upload_thumbnail.")), Server.MapPath("~/" + strLogoFilePath.Replace("Draft/", "") + "/" + strImageLoc.Replace(".","_upload_thumbnail.")));
						}//end of if
					}//end of else
					
					//goes around adding the images from the draft and puting them in the orginal id 
					foreach(DataRow drImage in dtImage.Rows)
					{
						//moves the images from draft to live
						File.Move(Server.MapPath("~/" + strLogoFilePath + drImage[""].ToString()), Server.MapPath("~/" + strLogoFilePath.Replace("Draft/", "") + "/" + drImage[""].ToString()));
						
						//checks if there larger is in the folder as well
						if (File.Exists(Server.MapPath("~/" + strLogoFilePath + drImage[""].ToString().Replace(".","_LG."))))
							//move the larger from draft to live
							File.Move(Server.MapPath("~/" + strLogoFilePath + drImage[""].ToString().Replace(".","_LG.")), Server.MapPath("~/" + strLogoFilePath.Replace("Draft/", "") + "/" + drImage[""].ToString().Replace(".","_LG.")));
						
						//checks if there thumbnail of the upload as it can create a thumbnail in the folder as well
						if (File.Exists(Server.MapPath("~/" + strLogoFilePath + drImage[""].ToString().Replace(".","_upload_thumbnail."))))
							//move the thumbnail of the upload from draft to live
							File.Move(Server.MapPath("~/" + strLogoFilePath + drImage[""].ToString().Replace(".","_upload_thumbnail.")), Server.MapPath("~/" + strLogoFilePath.Replace("Draft/", "") + "/" + drImage[""].ToString().Replace(".","_upload_thumbnail.")));
						
						//checks if there thumbnail is in the folder as well
						if (File.Exists(Server.MapPath("~/" + strLogoFilePath + drImage[""].ToString().Replace(".","_thumbnail."))))
							//move the thumbnail from draft to live
							File.Move(Server.MapPath("~/" + strLogoFilePath + drImage[""].ToString().Replace(".","_thumbnail.")), Server.MapPath("~/" + strLogoFilePath.Replace("Draft/", "") + "/" + drImage[""].ToString().Replace(".","_thumbnail.")));
							
						//checks if there icon is in the folder as well
						if (File.Exists(Server.MapPath("~/" + strLogoFilePath + drImage[""].ToString().Replace(".","_icon."))))
							//move the icon from draft to live
							File.Move(Server.MapPath("~/" + strLogoFilePath + drImage[""].ToString().Replace(".","_icon.")), Server.MapPath("~/" + strLogoFilePath.Replace("Draft/", "") + "/" + drImage[""].ToString().Replace(".","_icon.")));
						
						//adds the images into the database this time connected to the orginal id as they have been apporved
						DAL.addUpdateFuneralHomeImage(0, Convert.ToInt32(lblOrginalID.Text), Convert.ToInt32(drImage[""].ToString()), drImage[""].ToString(), true);
					}//end of for each
												
					//Update on the Main FH .Replace("&lt;/p&gt;","&lt;br&gt;").Replace("&lt;p&gt;","")
					intFHID = DAL.addUpdateFuneralHome(Convert.ToInt32(lblOrginalID.Text), txtFuneralName.Text.Trim(), txtFuneralURL.Text.Trim(), txtFuneralAddress1.Text.Trim(), txtFuneralAddress2.Text.Trim(), txtFuneralCity.Text.Trim(), ddlFuneralPro.SelectedValue, ddlFuneralCountry.SelectedValue, txtFuneralPhone.Text.Trim(), txtFuneralFax.Text.Trim(), txtPC.Text.Trim(), chkFHNew.Checked, strImageLoc, txtAboutUs.Text.Trim(), txtTagLine.Text.Trim(), hfMapLatitude.Value + "," + hfMapLongitude.Value, txtFeneralInqueryEmail.Text.Trim(), intCurrentStatus);
									
					DataTable dtFHUserDetails = DAL.getRow("", "where  = 1 AND  = " + intFHID);//holds the details of the FH and the User
									
					//removes a link all offerings to this funeral home in order to enter it again
					//if the user has selected it
					DAL.addUpdateLinkTableFHOfferings(Convert.ToInt32(intFHID), 0, -1);
	  
					//goes around each checking if a Offering number is in there
					for(int intIndex = 0;intIndex < arrFHOffering.Length - 1;intIndex++)
					{
						DataTable dtOfferingLink = DAL.getData(" Where  = " + intFHID + " AND  = " + arrFHOffering[intIndex]);//holds if the Offering id is already link with this funeral home
				
						//check if the Offering and funeral home are already link 
						if (dtOfferingLink == null)
							//adds a link for this offerings ID to this funeral home
							DAL.addUpdateLinkTableFHOfferings(intFHID,Convert.ToInt32(arrFHOffering[intIndex]),0);
					}//end of for loop
		  
					//goes around adding the users offerings to the database
					for(int intIndex = 0;intIndex < arrOtherOfferings.Length;intIndex++)
					{
						//adds the user's offering to the database and then adds the offering to this FH
						DAL.addUpdateLinkTableFHOfferings(intFHID, DAL.addUpdateOfferings(0, arrOtherOfferings[intIndex], "", true, Convert.ToInt32(lblUserID.Text)), 0);
					}//end of for loop
									
					//removes a link all Affiliations to this funeral home in order to enter it again
					//if the user has selected it
					DAL.addUpdateLinkTableFHAffiliations(intFHID,0,-1);
	  
					//goes around each checking if a affiliation number is in there
					for(int intIndex = 0;intIndex < arrFHAffilation.Length - 1;intIndex++)
					{
						DataTable dtAffiliationLink = DAL.getData(" Where  = " + intFHID + " AND  = " + arrFHAffilation[intIndex]);//holds if the affiliation id is already link with this funeral home
				
						//check if the affiliation and funeral home are already link 
						if (dtAffiliationLink == null)
							//adds a link for this Affiliations ID to this funeral home
							DAL.addUpdateLinkTableFHAffiliations(intFHID,Convert.ToInt32(arrFHAffilation[intIndex]),0);
					}//end of for loop
					
					//checks if there is a publish date as this means it was already publish
					if(!string.IsNullOrEmpty(dtFHUserDetails.Rows[0][""].ToString()))
						//changes strLangEdit to edit for the email and display
						strLangEdit = "edit";
	
					//checks if there is a user attach to this FH
					if (dtFHUserDetails.Rows.Count > 0)
						//sends an email to saying thate they have been update
						General.sendHTMLMail(dtFHUserDetails.Rows[0][""].ToString(), "Your Web Presence is LIVE!", string.Format(File.ReadAllText(Server.MapPath("~/EmailTemplate/FHSignUpPublish.html")), dtFHUserDetails.Rows[0][""].ToString(), dtFHUserDetails.Rows[0][""].ToString(),dtFHUserDetails.Rows[0][""].ToString(), strLangEdit, dtFHUserDetails.Rows[0][""].ToString()));

					//deletes the draft as it is not need
					DAL.deleteFuneralHome(Convert.ToInt32(lblID.Text));
											
					//resets the page
					cmdCancel_Click(sender,e);
				}//end of if
				else
					throw new Exception("You can only have three affiliations attach to this funeral home");
			}//end of if
			else
				throw new Exception("You must have a offering");
		}//end of try
		catch (Exception ex)
		{
			lblError.Text = ex.Message;
			lblError.Visible = true;
		}//end of catch
	}//end of cmdApprove_Click()
	
	protected void cmdCancel_Click(object sender, EventArgs e)
	{
		//resets the add and Update section 
		resetAddUpdateSection();
				
		//resets the which fourm view to use
		ddlLiveDraft.Items.Clear();
		panLiveDraft.Visible = false;
		
		//removes the Review Button and resets the text
		panReviewLogo.Visible = false;
		cmdReview.Text = "Review ";
						
		//changes the layout for Canceling
		panFHDisplay.Visible = true;
		panFHAdd.Visible = false;
		cmdApprove.Visible = false;
		cmdSave.Visible = true;
		cmdCancel.Visible = false;
		lblError.Visible = false;
		cmdSave.Text = "Save";
		lblMainTitle.Text = "Fill the form to insert the Item data";

		//re-gets the FH
        Bind(-1);
	}//end of cmdCancel_Click()
	
	protected void cmdAdd_Click(object sender, EventArgs e)
	{		
		//changes the layout for Adding
		panFHDisplay.Visible = false;
		panFHAdd.Visible = true;
		cmdCancel.Visible = true;
	}//end of cmdAdd_Click()
	
	protected void cmdSearch_Click(object sender, EventArgs e)
	{
		//sets the search where so that when the page reloads it does the search
		strSearchWhere = " LIKE '%" + DAL.safeSql(txtSearch.Text.Replace("'", "''")) + "%' AND  = 0";
	}//end of cmdSearch_Click()
	
	protected void cmdClearSearch_Click(object sender, EventArgs e)
	{
		//resets the search parts
		strSearchWhere = " = 0";
		txtSearch.Text = "";
	}//end of cmdClearSearch_Click()
	
	protected void lbAddOtherOfferings_Click(object sender, EventArgs e)
	{		
		//adds to the offerings count to now how many to count
		hfOtherOfferings.Value = Convert.ToString(Convert.ToInt32(hfOtherOfferings.Value) + 1);
	}//end of lbAddOtherOfferings_Click()
	
	protected void ddlFuneralCountry_SelectedIndexChanged(object sender, EventArgs e)
	{
		//checks which contry the user is using Canada or use and change the name of the postal to 
		if(ddlFuneralCountry.SelectedValue == "2")
		{
			//sets the display format for US
			lblPC.Text = "Zip";
			lblProvince.Text = "State";
			revPC.ErrorMessage = "Invalid zip code format";
			revPC.ValidationExpression = @"^\d{5}$";
		}//end of if
		else
		{
			//sets the display format for Canada/Other
			lblPC.Text = "Postal";
			lblProvince.Text = "Province";
			revPC.ErrorMessage = "Invalid postal code format";
			revPC.ValidationExpression = @"^[A-Z]\d[A-Z][ ]\d[A-Z]\d$";
		}//end of else
		
		//changes the Provance to the country selected
		ddlFuneralPro.DataSource = DAL.getRow("","Where  != 64 AND  = " + ddlFuneralCountry.SelectedValue + " Order by ");
		ddlFuneralPro.DataBind();
	}//end of ddlFuneralCountry_SelectedIndexChanged()
	
	protected void ddlLiveDraft_SelectedIndexChanged(object sender, EventArgs e)
	{
		//resets the add and Update section 
		resetAddUpdateSection();
		
		//loads the the selected FH content into the Update section
		loadUpdateDate(Convert.ToInt32(ddlLiveDraft.SelectedValue));
	}//end of ddlLiveDraft_SelectedIndexChanged()
	
	protected void ddlFHActive_SelectedIndexChanged(object sender, EventArgs e)
	{
		//checks if this Inactive
		if(Convert.ToInt32(ddlFHActive.SelectedValue) == -1)
		{
			//disable some validation as this status is for addional FH 
			//where they may not have all of the required fields used
			FuneralPhoneRequired.Visible = false;
			rfvAboutUs.Visible = false;
		}//end of if
		else
		{
			//reenable some validation for the rest of the status
			FuneralPhoneRequired.Visible = true;
			rfvAboutUs.Visible = true;
		}//end of else
	}//end of ddlFHActive_SelectedIndexChanged()
	
	#region PagingControls

    protected void BindPagingControls()
    {
        if (_LastPageIndex > 0)
        {
            PagerPanel.Visible = true;
            List<PagerLinkData> pagerLinkData = new List<PagerLinkData>();
            float tempIndex = ((float)_HiddenPageIndex / 10) * 10;
            int currentPagerIndex = (int)tempIndex ;

            int lastPagerIndex = currentPagerIndex + _PageSize;
            if (lastPagerIndex > _LastPageIndex) lastPagerIndex = _LastPageIndex;
            // + "&s=" + Server.UrlEncode(strSearchSort)
			string baseUrl = "reviewfuneralhomes.aspx?search=" + Server.UrlEncode(txtSearch.Text.Replace("'", "''")) + "&p=";
            string navigateUrl;
			
            if (currentPagerIndex > 0)
            {
				//checks if the currentOager is a least two away from the Pager Index
				//in order fro the arrow to work
				if((currentPagerIndex - 2) > 0)
				{
					pagerLinkData.Add(new PagerLinkData("<<", baseUrl + "0", (currentPagerIndex - 2), true));
					
					navigateUrl = baseUrl + (currentPagerIndex - 2).ToString();
					pagerLinkData.Add(new PagerLinkData("<", navigateUrl, (currentPagerIndex - 2), true));
				}//end of if 
				else if((currentPagerIndex - 1) > 0)
				{
	                navigateUrl = baseUrl + (currentPagerIndex - 2);
					pagerLinkData.Add(new PagerLinkData(((int)(currentPagerIndex - 1)).ToString(), navigateUrl, (currentPagerIndex - 2), true));
				}//end of else if
								
				string linkText = ((int)(currentPagerIndex)).ToString();
				
                navigateUrl = baseUrl + (currentPagerIndex - 1);
                pagerLinkData.Add(new PagerLinkData(linkText, navigateUrl, (currentPagerIndex - 1), true));				
            }//end of if
            
			while (currentPagerIndex <= lastPagerIndex)
            {
                string linkText = ((int)(currentPagerIndex + 1)).ToString();
				
                if (currentPagerIndex != _HiddenPageIndex)
                {
                    navigateUrl = baseUrl + currentPagerIndex.ToString();
                    pagerLinkData.Add(new PagerLinkData(linkText, navigateUrl, currentPagerIndex, (currentPagerIndex != _HiddenPageIndex)));
                }//end of if
                else
                {
                    navigateUrl = "#";
                    pagerLinkData.Add(new PagerLinkData(linkText, navigateUrl, currentPagerIndex, (currentPagerIndex != _HiddenPageIndex), "divCurrentPage"));
                }//end of else
				
                currentPagerIndex++;
            }//end of while loop
			
            if (lastPagerIndex < _LastPageIndex)
            {
                navigateUrl = baseUrl + (lastPagerIndex + 1).ToString();
                pagerLinkData.Add(new PagerLinkData(">", navigateUrl,lastPagerIndex+1, true));
            }//end of if
			
            PagerControls.DataSource = pagerLinkData;
            PagerControls.DataBind();
        }//end of if
        else
            PagerPanel.Visible = false;
    }//end of BindPagingControls()

    public class PagerLinkData
    {
        private string _Text;
        private int _PageIndex;
        private string _NavigateUrl;
        public int PageIndex { get { return _PageIndex; } }
        private bool _Enabled;
        public string Text { get { return _Text; } }
        public string NavigateUrl { get { return _NavigateUrl; } }
        public bool Enabled { get { return _Enabled; } }
        private string _tagClass;
        public string TagClass { get { return _tagClass; } set { _tagClass = value; } } 
		
        public PagerLinkData(string text, string navigateUrl,int pageIndex, bool enabled)
        {
            _Text = text;
            _NavigateUrl = navigateUrl;
            _PageIndex = pageIndex;
            _Enabled = enabled;
        }//end of PagerLinkData()

        public PagerLinkData(string text, string navigateUrl, int pageIndex, bool enabled,string tagClass)
        {
            _Text = text;
            _NavigateUrl = navigateUrl;
            _PageIndex = pageIndex;
            _Enabled = enabled;
            _tagClass = tagClass;
        }//end of PagerLinkData()
    }//end of PagerLinkData()

    protected void PagerControls_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Page")
        {
            InitializePagingVars(false);
            _HiddenPageIndex = Convert.ToInt32((string)e.CommandArgument);
            if (_HiddenPageIndex < 0) _HiddenPageIndex = 0;

            if (_HiddenPageIndex > _LastPageIndex) _HiddenPageIndex = _LastPageIndex;
            HiddenPageIndex.Value = _HiddenPageIndex.ToString();
        }//end of if
    }//end of PagerControls_ItemCommand()
	
    protected void SetPagerIndex()
    {
        InitializePagingVars(false);
		
        //checks if there is a p to uses it else then give it a zero
		if(!string.IsNullOrEmpty(Request.QueryString["p"]))		
	        _HiddenPageIndex = Convert.ToInt32(Request.QueryString["p"]);
		else
			_HiddenPageIndex = Convert.ToInt32(0);
        
		if (_HiddenPageIndex < 0) _HiddenPageIndex = 0;
        if (_HiddenPageIndex > _LastPageIndex) _HiddenPageIndex = _LastPageIndex;
        HiddenPageIndex.Value = _HiddenPageIndex.ToString();
    }//end of SetPagerIndex()
	
    #endregion
}//end of Page