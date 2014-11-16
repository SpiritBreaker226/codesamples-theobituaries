// Editor The Funernal Home

public partial class DesktopModules_FuneralHomeEditor_FuneralHomeEditor : PortalModuleBase
{
	//creates the other Offerings textbox
	private void createOtherOfferings()
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
			txtNewOtherOffering.Attributes.Add("onchange", "changeFHOffering(getDocID('" + hfOtherOfferingsValue.ClientID + "'),getDocID('dnn_ctr582_FuneralHomeEditor_" + txtNewOtherOffering.ID + "'))");
			txtNewOtherOffering.MaxLength = 50;
			
			//sets the value into hfOtherOfferingsValue as a way of remebering what was the values 
			//after the reload
			hfOtherOfferingsValue.Value += arrOtherOfferings[intIndex] + "#2@";
			
			//adds the new offering to phOtherOfferings
			phOtherOfferings.Controls.Add(txtNewOtherOffering);
			phOtherOfferings.Controls.Add(new LiteralControl("<br/>"));
		}//end of for loop
	}//end of createOtherOfferings()
	
	//either enables or disables the validator as they may or may not be used
	private void disableValidator(bool boolDisableFHVailidator)
	{
		//checks if the vailidator is going to be disable if so then reset the fourm
		if(boolDisableFHVailidator == false)
		{			
			//clears the User fourm
			txtSiteAdminFName.Text = "";
			txtSiteAdminLName.Text = "";
			txtSiteAdminEmail.Text = "";
			txtSiteAdminCEmail.Text = "";
						
			//resets the User search
			fhsUser.resetValues();
		}//end of if
		
		//enable or disable the User Vailidator
		FNameRequired.Visible = boolDisableFHVailidator;
		revSiteAdminFName.Visible = boolDisableFHVailidator;
		LNameRequired.Visible = boolDisableFHVailidator;
		revSiteAdminLName.Visible = boolDisableFHVailidator;
		EmailRequired.Visible = boolDisableFHVailidator;
		revSiteAdminEmail.Visible = boolDisableFHVailidator;
	}//end of disableValidator()
	
    protected void Page_PreRender(object sender, EventArgs e)
    {
		//checks if the user is already logged in and if so then send them to the homepage
		if (Session[""] == null)
			Response.Redirect("/MyAccount/Login.aspx?url=" + Server.UrlEncode(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "")), true);
		
        if (!IsPostBack)
        {
            DataTable dtChooseFH = DAL.getRow("", "Where  = '" + Session[""].ToString() + "' AND  != -2 AND  != 0");//holds the FH currently in the database without a user

			//gets the Country
			ddlFuneralCountry.DataSource = DAL.getRow("tblCountry","");
			ddlFuneralCountry.DataBind();
					
			//gets the all of the Canada's Province as the default selection
			ddlFuneralPro.DataSource = DAL.getRow("","Where  = 1 Order by ");
			ddlFuneralPro.DataBind();
			
			//gets the all of the Default and this user's Offerings
			chkLstOfferingText.DataSource = DAL.getRow("","Where  = 'false' OR  = " + Session[""].ToString() + " Order by ");
			chkLstOfferingText.DataBind();
						
            //gets the Affiliations currently in the database
			DataTable dtAffiliation = DAL.getData("dbo.");
			
			if (dtAffiliation != null && dtAffiliation.Rows.Count > 0)
			{
				foreach (DataRow drAffiliation in dtAffiliation.Rows)
				{
					FileInfo file = new System.IO.FileInfo(drAffiliation[""].ToString());
					
					ListItem item = new ListItem("<img src='" + "/portals/_default/skins/obit/images/affiliates/" + file.Name + "' alt='" + drAffiliation[""].ToString() + "' title='" + drAffiliation[""].ToString() + "' />" + drAffiliation[""].ToString(), drAffiliation[""].ToString());
					
					chkLstAffiliations.Items.Add(item);
				}//end of foreach
			}//end of if
			
			//goes around for each item in dtChooseFH and adds them to lbChooseUserFH
			foreach(DataRow drChooseFH in dtChooseFH.Rows)
			{
				DataTable dFHDraft = DAL.getRow("", "Where  = " + drChooseFH[""].ToString());//holds weather this Funeral Home is a draft or not
                ListItem ltFuneralHome = null;
				
				//checks if this Funeral Home has a draft
                if (dFHDraft.Rows.Count > 0)
                {
                    //adds the items into lbChooseUserFH from the database
                    ltFuneralHome = new ListItem(dFHDraft.Rows[0][""] + " - " + dFHDraft.Rows[0][""] + ", " + dFHDraft.Rows[0][""] + ", " + dFHDraft.Rows[0][""] + ", " + dFHDraft.Rows[0][""], dFHDraft.Rows[0][""].ToString());
        
					//checks the status of the draft Funeral Home in order to see if it is in Under Review Status(-2)
					if(dFHDraft.Rows[0][""].ToString() == "-2")
					{
						//adds to the Text to tell the user the status change
						ltFuneralHome.Text += " - Under Review";
						
						//disables the funeral home if the draft is in Under Review Status(-2)
						ltFuneralHome.Attributes.Add("disabled", "");
					}//end of if
                }//end of if
                else
                    //adds the items into lbChooseUserFH from the database
                    ltFuneralHome = new ListItem(drChooseFH[""] + " - " + drChooseFH[""] + ", " + drChooseFH[""] + ", " + drChooseFH[""] + ", " + drChooseFH[""], drChooseFH[""].ToString());

				//addds to lbChooseUserFH
                lbChooseUserFH.Items.Add(ltFuneralHome);
			}//end of for loop
			
			try
			{
				//checks if there is a FH id to use
				if (!string.IsNullOrEmpty(Request.QueryString[""]))
				{
					//seletes the what the user whats and displays it contents to the user
					lbChooseUserFH.SelectedValue = DAL.safeSql(Request.QueryString[""]);
					lbChooseUserFH_SelectedIndexChanged(sender,e);
					
					//creates the other Offerings textbox
					createOtherOfferings();
				}//end of if
			}//end of try
			catch (Exception ex)
			{
				//sends the user to the homepage as they are trying to hack the site with a id that is not assign to them
				Response.Redirect("/Home.aspx", true);
			}//end of catch
			
			//adds a javascript for RegularExpressionValidator to check if the 
			//txtbox that is connect to the field needs to be hightlight for the error
			txtFuneralURL.Attributes.Add("onblur", "validateExpressionCheck('" + revURL.ClientID + "', '" + txtFuneralURL.ClientID + "');");
			txtFuneralCity.Attributes.Add("onblur", "validateExpressionCheck('" + revCity.ClientID + "', '" + txtFuneralCity.ClientID + "');");
			txtPC.Attributes.Add("onblur", "validateExpressionCheck('" + revPC.ClientID + "', '" + txtPC.ClientID + "');");
			txtFuneralPhone.Attributes.Add("onblur", "validateExpressionCheck('" + revPhone.ClientID + "', '" + txtFuneralPhone.ClientID + "');");
			txtFuneralFax.Attributes.Add("onblur", "validateExpressionCheck('" + revFax.ClientID + "', '" + txtFuneralFax.ClientID + "');");
			txtFeneralInqueryEmail.Attributes.Add("onblur", "validateExpressionCheck('" + revInqueryEmail.ClientID + "', '" + txtFeneralInqueryEmail.ClientID + "');");
			txtSiteAdminFName.Attributes.Add("onblur", "validateExpressionCheck('" + revSiteAdminFName.ClientID + "', '" + txtSiteAdminFName.ClientID + "');");
			txtSiteAdminLName.Attributes.Add("onblur", "validateExpressionCheck('" + revSiteAdminLName.ClientID + "', '" + txtSiteAdminLName.ClientID + "');");
			txtSiteAdminEmail.Attributes.Add("onblur", "validateExpressionCheck('" + revSiteAdminEmail.ClientID + "', '" + txtSiteAdminEmail.ClientID + "');");
        }//end of if
		else
			//creates the other Offerings textbox
			createOtherOfferings();
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
	
	protected void lbChooseUserFH_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			//turns on the Edit area
			panEdit.Visible = false;
			
			//checks if this is FH is under review if so then tell the user so else do the selectoin
			if(lbChooseUserFH.SelectedItem.Text.IndexOf(" - Under Review") > 0)
				throw new Exception("Funeral Home is currently under review");
			else
			{
				//turns off the error message
				panMainError.Visible = false;
				
				DataTable dtFHDetails = DAL.getRow("", "Where  = " + lbChooseUserFH.SelectedValue + " OR  = " + lbChooseUserFH.SelectedValue);//holds the Funeral Home details
				DataTable dtFHAffiliations = DAL.getRow("", "Where  = " + lbChooseUserFH.SelectedValue);//holds the Funeral Home / Affiliations Details
				DataTable dtFHOfferings = DAL.getRow("", "Where  = " + lbChooseUserFH.SelectedValue);//holds the Funeral Home / Affiliations Details
										
				//checks if this is a draft or not
				if(Convert.ToInt32(dtFHDetails.Rows[0][""].ToString()) == 0)
				{
					//adds the  to hfOrginalFHID
					hfOrginalFHID.Value = dtFHDetails.Rows[0][""].ToString();
					
					//resets hfOrginalFHID to zero as a new draft needs to be created
					hfCurrentFHID.Value = "0";
				}//end of if
				else
				{
					//adds  to hfOrginalFHID as it is already has FH Orginal ID
					hfOrginalFHID.Value = dtFHDetails.Rows[0][""].ToString();
					
					//sets hfOrginalFHID to what the user has selected as this is a draft
					hfCurrentFHID.Value = lbChooseUserFH.SelectedValue;
				}//end of else
				
				DataTable dtLinkFHUser = DAL.getRow("", "Where  = " + hfOrginalFHID.Value + " AND UserID = " + Session[""].ToString());//holds the user connection to this fh
				
				string strLogoFilePath = "/images/FH/" + hfOrginalFHID.Value + "/";//holds the image folder
				
				//checks if there this is a draft
                if(Convert.ToInt32(dtFHDetails.Rows[0][""].ToString()) != 0)
					//point to the draft image folder as this a draft now and all of the images
					//are in the draft folder, this is so that the user does not destory the live
					//images before it is looked at
                    strLogoFilePath += "Draft/";
				
				//resets FH Error and thank you message
				lblFHError.Visible = false;
				panThankYou.Visible = false;
				
				//sets the values for this page of the editing of the users Funeral Home
				txtTagLine.Text = dtFHDetails.Rows[0][""].ToString();
				txtAboutUs.Text = Server.HtmlDecode(dtFHDetails.Rows[0][""].ToString());
				txtFeneralInqueryEmail.Text = dtFHDetails.Rows[0][""].ToString();
				txtFuneralName.Text = dtFHDetails.Rows[0][""].ToString();
				txtFuneralURL.Text = dtFHDetails.Rows[0][""].ToString();
				txtFuneralAddress1.Text = dtFHDetails.Rows[0][""].ToString();
				txtFuneralAddress2.Text = dtFHDetails.Rows[0][""].ToString();
				txtFuneralCity.Text = dtFHDetails.Rows[0][""].ToString();
				txtPC.Text = dtFHDetails.Rows[0][""].ToString();
				ddlFuneralPro.SelectedValue = dtFHDetails.Rows[0][""].ToString();
				ddlFuneralCountry.SelectedValue = dtFHDetails.Rows[0][""].ToString();
				chkFHNew.Checked = Convert.ToBoolean(dtFHDetails.Rows[0][""].ToString());
				
				//resets the agree check
				chkAgreeTerms.Checked = false;
				
				//checks if this column content exits
				if(!string.IsNullOrEmpty(dtFHDetails.Rows[0][""].ToString().Trim().Replace("&nbsp;","").Trim()))		
					txtFuneralPhone.Text = dtFHDetails.Rows[0][""].ToString().Trim();
				
				//checks if this column content exits
				if(!string.IsNullOrEmpty(dtFHDetails.Rows[0][""].ToString().Trim().Replace("&nbsp;","").Trim()))
					txtFuneralFax.Text = dtFHDetails.Rows[0][""].ToString().Trim();

				cmdSave.Text = "Next Step - Upload Images";
				
				//clear all for offering from chkLstOfferingText and recreted them again
				//for the next time the offerings is needed with out having the custom offerings from the
				//last offering
				chkLstOfferingText.DataSource = DAL.getRow("","Where  = 'false' OR  = " + Session[""].ToString() + " Order by ");
				chkLstOfferingText.DataBind();
				
				//clears the other offerings from the last FH and add in a new txtOtherOfferings to start
				phOtherOfferings.Controls.Clear();
				hfOtherOfferingsValue.Value = "";
				hfOtherOfferings.Value = "1";
				
				//uncheck all for affiliations

				foreach (ListItem liCheckbox in chkLstAffiliations.Items)
				{
					//uncheck all items
					liCheckbox.Selected = false;
				}//end of foreach
						
				//checks if this is users the main user if so then give the option to add new users
				if(Convert.ToBoolean(dtLinkFHUser.Rows[0][""].ToString()) == true)
				{
					DataTable dtSiteAdmin = DAL.getRows(""," DESC",0,100," = " + hfOrginalFHID.Value + " AND  != " + Session[""].ToString());//holds all the users
					
					//turns on the site admin section
					panSiteAdmin.Visible = true;
					
					//checks if there is any users attach to this FH if not then do nto display the DataGrid
					gdSiteAdmin.Visible = !(dtSiteAdmin.Rows.Count == 0);
					
					//gets the data
					gdSiteAdmin.DataSource = dtSiteAdmin;
					gdSiteAdmin.DataBind();
				}//end of if
				else
					//turns off the site admin section
					panSiteAdmin.Visible = false;
				
				//checks if there is a funeral logo if so then display it in the image banner
				if(!string.IsNullOrEmpty(dtFHDetails.Rows[0][""].ToString().Trim()))
				{
					//turns on the Current Logo section and displays the image
					panCurrentLogo.Visible = true;
					lblImageSource.Text =  dtFHDetails.Rows[0][""].ToString();
					imgLogo.ImageUrl = strLogoFilePath +  dtFHDetails.Rows[0][""].ToString();
				}//end of if
				else
				{
					//turns off the Current Logo section and removes the image
					panCurrentLogo.Visible = false;
					lblImageSource.Text =  "";
					imgLogo.ImageUrl = "";
				}//end of else
				
				//goes around each offering connect this funeral home and make it selected
				foreach (DataRow drFHOfferings in dtFHOfferings.Rows)
				{
					ListItem liCheckBox = chkLstOfferingText.Items.FindByValue(drFHOfferings[""].ToString());//holds the location of the Offering in chkLstOfferingText
					
					//checks if this Offering is in chkLstOfferingText
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
					
				try
				{
					string strJSLocationGeo = "getLocationGeo(getDocID('" + txtFuneralAddress1.ClientID + "').value + ',' + getDocID('" + txtFuneralCity.ClientID + "').value + '," + dtFHDetails.Rows[0][""] + "',map,true,'" + hfMapLatitude.ClientID + "','" + hfMapLongitude.ClientID + "');";//holds the javascript function location 
					
					//adds the basic map optionals to both the onload and onclick
					litLocGeo.Text += "var latLngDefault = new google.maps.LatLng(43.68575,-79.37645);\n" + 
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
                    "var map = new google.maps.Map(document.getElementById('divMap'), mapOptions);\n\n" + strJSLocationGeo;
					
					//there maybe the times where the address is not found if so then this give the user the option
					//of finding the add again
					litLocGeoAgain.Text = litLocGeo.Text + strJSLocationGeo;
					
					//checks if there is a already a location that the user wants to use
					if(dtFHDetails.Rows[0][""] == null || string.IsNullOrEmpty(dtFHDetails.Rows[0][""].ToString().Trim()))
						//finds the location base on the funeral homes address
						litLocGeo.Text += litLocGeoAgain.Text;
					else
					{
						string[] arrLatLong = dtFHDetails.Rows[0][""].ToString().Trim().Split(',');//holds the latitude and longitude
						
						//makes a pin in the location that the user want it to be
						litLocGeo.Text += "//centers the map to the location\n" + 
						"map.setCenter(new google.maps.LatLng(" + dtFHDetails.Rows[0][""] + "));\n" + 
						"var marker = new google.maps.Marker({\n" + 
							"draggable: true,\n" + 
							"map: map,\n" + 
							"position: new google.maps.LatLng(" + dtFHDetails.Rows[0][""] + ")\n" + 
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
				}//end of try
				catch (Exception ex)
				{
					//turns off map as the user did not enter the current data
					//panMap.Visible = false;
					lblError.Text = ex.Message;// + " " + ex.StackTrace;
					lblError.Visible = true;
				}//end of catch
						
				//turns on the Edit area
				panEdit.Visible = true;
			}//end of if
		}//end of try
		catch (Exception ex)
		{
			lblMainError.Text = ex.Message;// + " " + ex.StackTrace;
			panMainError.Visible = true;
		}//end of catch
	}//end of lbChooseUserFH_SelectedIndexChanged()
	
	protected void gdSiteAdmin_ItemCommand(object sender, DataGridCommandEventArgs e)
	{
		//checks which Command to use
        if (e.CommandName == "Delete")
        {			
            //Deletes the link for this funeral home
			DAL.addUpdateLinkTableFHUser(Convert.ToInt32(hfOrginalFHID.Value), Convert.ToInt32(e.Item.Cells[0].Text), -1, false);

			//reloads the choosen fh
            lbChooseUserFH_SelectedIndexChanged(sender, e);
        }//end of if
		
        if (e.CommandName == "Make")
        {			
			//Updates and removes the main site admin as the main user
			DAL.addUpdateLinkTableFHUser(Convert.ToInt32(hfOrginalFHID.Value), 0, -2, false);
			
			//Updates and makes the selected user the new main site admin
			DAL.addUpdateLinkTableFHUser(Convert.ToInt32(hfOrginalFHID.Value), Convert.ToInt32(e.Item.Cells[0].Text), 1, true);
        }//end of if
	}//end of gdSiteAdmin_ItemCommand()
	
    protected void cmdSave_Click(object sender, EventArgs e)
    {
        try
        {
			//checks if the page is valid if so then prcess the event
			if (Page.IsValid)
			{
				//turns off the error message
				lblError.Visible = false;
	
				//checks if the user agree Terms of Use
				if (chkAgreeTerms.Checked == true)
				{
					string strOfferings = "";//holds the all of the offerings the user wants
					string strAffiliations = "";//holds the all of the affiliations the user wants
					string strLogoFilePath = "/images/FH/" + hfOrginalFHID.Value + "/Draft/";//holds the dirtory							
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
							string strImageLoc = "";//holds the loction for the image
							string[] arrFHOffering = strOfferings.Split('*');//holds each offering that is in this FH
							
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
																										
							//check if they what to changes the Main Image of the Category
							if (fuLogo.HasFile) 
							{								
								//checks if this FH has already logo
								if (!string.IsNullOrEmpty(lblImageSource.Text) && File.Exists(Server.MapPath("~/" + strLogoFilePath + lblImageSource.Text)))
								{
									//delete it form the draft has it is not needed
									File.Delete(Server.MapPath("~/" + strLogoFilePath + lblImageSource.Text));
																		
									//checks if this FH has already has a logo thumbnail
									if (!string.IsNullOrEmpty(lblImageSource.Text.Replace(".","_upload_thumbnail.")) && File.Exists(Server.MapPath("~/" + strLogoFilePath + lblImageSource.Text.Replace(".","_upload_thumbnail."))))
										//delete it form the draft has it is not needed
										File.Delete(Server.MapPath("~/" + strLogoFilePath + lblImageSource.Text.Replace(".","_upload_thumbnail.")));
								}//end of if

								//uplaods the Image to the site
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
							{
								//gets existing image to the site			
								strImageLoc = lblImageSource.Text;
								
								//checks if the file is already in the draft if not then copy it to the draft from live
								if(!string.IsNullOrEmpty(strImageLoc) && File.Exists(Server.MapPath("~/" + strLogoFilePath + strImageLoc)) == false)
								{
									//copies the logo from live to draft
									File.Copy(Server.MapPath("~/" + strLogoFilePath.Replace("Draft/", "") + "/" + strImageLoc), Server.MapPath("~/" + strLogoFilePath + strImageLoc));
									
									//checks if there logo thumbnail is in the folder as well
									if (File.Exists(Server.MapPath("~/" + strLogoFilePath.Replace("Draft/", "") + "/" + lblImageSource.Text.Replace(".","_upload_thumbnail."))))
										//add the logo thumbnail to draft as well
										File.Copy(Server.MapPath("~/" + strLogoFilePath.Replace("Draft/", "") + "/" + strImageLoc.Replace(".","_upload_thumbnail.")), Server.MapPath("~/" + strLogoFilePath + strImageLoc.Replace(".","_upload_thumbnail.")));
								}//end of if
							}//end of else
							
							//checks if hfMapLatitude.Value or hfMapLongitude.Value is undefined 
							if(hfMapLatitude.Value == "undefined" || hfMapLongitude.Value == "undefined" || hfMapLatitude.Value == "" || hfMapLongitude.Value == "")
							{
								//sets the defualt again as the address was not found
								hfMapLatitude.Value = "43.64100156269233";
								hfMapLongitude.Value = "-79.38599562435303";
							}//end of if
							
							//add new draft or updates this funeral home draft Convert.ToInt32(ddlFHActive.SelectedValue)
							hfCurrentFHID.Value = Convert.ToString(DAL.addUpdateFuneralHome(Convert.ToInt32(hfCurrentFHID.Value), txtFuneralName.Text.Trim(), txtFuneralURL.Text.Trim(), txtFuneralAddress1.Text.Trim(), txtFuneralAddress2.Text.Trim(), txtFuneralCity.Text.Trim(), ddlFuneralPro.SelectedValue, ddlFuneralCountry.SelectedValue, txtFuneralPhone.Text.Trim(), txtFuneralFax.Text.Trim(), txtPC.Text.Trim(), chkFHNew.Checked, strImageLoc, txtAboutUs.Text.Trim(), txtTagLine.Text.Trim(), hfMapLatitude.Value + "," + hfMapLongitude.Value, txtFeneralInqueryEmail.Text.Trim(),  0, Convert.ToInt32(hfOrginalFHID.Value)));
							//.Replace("&lt;/p&gt;","&lt;br&gt;").Replace("&lt;p&gt;","")
							
							//checks if theere is any images in draft dirtory if not then copy live dirtory images over
							if(Directory.GetFiles(Server.MapPath("~/" + strLogoFilePath)).Length == 0 || Directory.GetFiles(Server.MapPath("~/" + strLogoFilePath)).Length <= 2 && !string.IsNullOrEmpty(strImageLoc))
							{
								DataTable dtImage = DAL.getRow("", "Where  = " + hfOrginalFHID.Value + " Order by ");//holds all of the images for the Orginal FH
								
								//goes around adding the images from the live and puting them in the draft
								//in order for the user to mange it
								foreach(DataRow drImage in dtImage.Rows)
								{
									//copies the images from live to draft
									File.Copy(Server.MapPath("~/" + strLogoFilePath.Replace("Draft/", "") + drImage[""].ToString()), Server.MapPath("~/" + strLogoFilePath + "/" + drImage[""].ToString()));
									//checks if there larger is in the folder as well
									if (File.Exists(Server.MapPath("~/" + strLogoFilePath.Replace("Draft/", "") + drImage[""].ToString().Replace(".","_LG."))))
										//copies the larger from draft to live
										File.Copy(Server.MapPath("~/" + strLogoFilePath.Replace("Draft/", "") + drImage[""].ToString().Replace(".","_LG.")), Server.MapPath("~/" + strLogoFilePath + "/" + drImage[""].ToString().Replace(".","_LG.")));
									
									//checks if there thumbnail of the upload as it can create a thumbnail in the folder as well
									if (File.Exists(Server.MapPath("~/" + strLogoFilePath.Replace("Draft/", "") + drImage[""].ToString().Replace(".","_upload_thumbnail."))))
										//copies the thumbnail of the upload from draft to live
										File.Copy(Server.MapPath("~/" + strLogoFilePath.Replace("Draft/", "") + drImage[""].ToString().Replace(".","_upload_thumbnail.")), Server.MapPath("~/" + strLogoFilePath + "/" + drImage[""].ToString().Replace(".","_upload_thumbnail.")));
									
									//checks if there thumbnail is in the folder as well
									if (File.Exists(Server.MapPath("~/" + strLogoFilePath.Replace("Draft/", "") + drImage[""].ToString().Replace(".","_thumbnail."))))
										//copies the thumbnail from draft to live
										File.Copy(Server.MapPath("~/" + strLogoFilePath.Replace("Draft/", "") + drImage[""].ToString().Replace(".","_thumbnail.")), Server.MapPath("~/" + strLogoFilePath + "/" + drImage[""].ToString().Replace(".","_thumbnail.")));
										
									//checks if there icon is in the folder as well
									if (File.Exists(Server.MapPath("~/" + strLogoFilePath.Replace("Draft/", "") + drImage[""].ToString().Replace(".","_icon."))))
										//copies the icon from draft to live
										File.Copy(Server.MapPath("~/" + strLogoFilePath.Replace("Draft/", "") + drImage[""].ToString().Replace(".","_icon.")), Server.MapPath("~/" + strLogoFilePath + "/" + drImage[""].ToString().Replace(".","_icon.")));
																	
									//adds the images the database this time connected to the orginal id as they have been apporved
									DAL.addUpdateFuneralHomeImage(0, Convert.ToInt32(hfCurrentFHID.Value), Convert.ToInt32(drImage[""].ToString()), drImage[""].ToString(), true);
								}//end of for each
							}//end of if
							
							//removes a link all offerings to this funeral home in order to enter it again
							//if the user has selected it
							DAL.addUpdateLinkTableFHOfferings(Convert.ToInt32(hfCurrentFHID.Value),0,-1);
	
							//goes around each checking if a Offering number is in there
							for(int intIndex = 0;intIndex < arrFHOffering.Length - 1;intIndex++)
							{
								DataTable dtOfferingLink = DAL.getData(" Where  = " + hfCurrentFHID.Value + " AND  = " + arrFHOffering[intIndex]);//holds if the Offering id is already link with this funeral home
						
								//check if the Offering and funeral home are already link 
								if (dtOfferingLink == null)
									//adds a link for this offerings ID to this funeral home
									DAL.addUpdateLinkTableFHOfferings(Convert.ToInt32(hfCurrentFHID.Value),Convert.ToInt32(arrFHOffering[intIndex]),0);
							}//end of for loop
							
							//goes around adding the users offerings to the database
							for(int intIndex = 0;intIndex < arrOtherOfferings.Length;intIndex++)
							{
								//adds the user's offering to the database and then adds the offering to this FH
								DAL.addUpdateLinkTableFHOfferings(Convert.ToInt32(hfCurrentFHID.Value), DAL.addUpdateOfferings(0, arrOtherOfferings[intIndex], "", true, Convert.ToInt32(Session[""].ToString())), 0);
							}//end of for loop
							
							//removes a link all Affiliations to this funeral home in order to enter it again
							//if the user has selected it
							DAL.addUpdateLinkTableFHAffiliations(Convert.ToInt32(hfCurrentFHID.Value),0,-1);
	
							//goes around each checking if a affiliation number is in there
							for(int intIndex = 0;intIndex < arrFHAffilation.Length - 1;intIndex++)
							{
								DataTable dtAffiliationLink = DAL.getData(" Where  = " + hfCurrentFHID.Value + " AND  = " + arrFHAffilation[intIndex]);//holds if the affiliation id is already link with this funeral home
						
								//check if the affiliation and funeral home are already link 
								if (dtAffiliationLink == null)
									//adds a link for this Affiliations ID to this funeral home
									DAL.addUpdateLinkTableFHAffiliations(Convert.ToInt32(hfCurrentFHID.Value),Convert.ToInt32(arrFHAffilation[intIndex]),0);
							}//end of for loop
							
							//loads the images into for display and allow the user upload more
							ciFH.setFHID = Convert.ToInt32(hfCurrentFHID.Value);
							ciFH.setFHIDDir = Convert.ToInt32(hfOrginalFHID.Value);
							ciFH.loadImages();
							
							//Turn on the thank you message and removes the edit funeral home
							panThankYou.Visible = true;
							panSignUp.Visible = false;
						}//end of if
						else
							throw new Exception("You can only have three affiliations attach to this funeral home");
					}//end of if
					else
						throw new Exception("You must have a offering");
				}//end of if
				else
					throw new Exception("You must agree to the terms of alliance program");
			}//end of if
        }//end of try
        catch (Exception ex)
        {
            lblError.Text = ex.Message;// + " " + ex.StackTrace;
            lblError.Visible = true;
        }//end of catch
    }//end of cmdSave_Click()
	
	protected void lbSearchYourUser_Click(object sender, EventArgs e)
	{
		//displays the Additional Site Administrator section
		panAdditionalSiteAdministrator.Style.Add("display", "block");
		
		//hides panSearchYourUser and displays panAddYouUser
		panSearchYourUser.Visible = false;
		panAddYouUser.Visible = true;
		
		//enable the requirements as the the User fourm is needed
		disableValidator(true);
	}//end of lbSearchYourUser_Click()
	
	protected void lbAddYouUser_Click(object sender, EventArgs e)
	{
		//hides panAddYouUser and displays panSearchYourUser
		panSearchYourUser.Visible = true;
		panAddYouUser.Visible = false;
		
		//disable the requirments as the search is needed
		disableValidator(false);
	}//end of lbAddYouUser_Click()
	
	protected void lbAddSiteAdmin_Click(object sender, EventArgs e)
	{
		//checks if is hiden then display panAdditionalSiteAdministrator 
		if(panAdditionalSiteAdministrator.Visible == false)
			//shows panAdditionalSiteAdministrator
			panAdditionalSiteAdministrator.Visible = true;
		else
			//hides panAdditionalSiteAdministrator 
			panAdditionalSiteAdministrator.Visible = false;
	}//end of lbAddSiteAdmin_Click()

    protected void btnSaveAdditionalSiteAdmin_Click(object sender, EventArgs e)
    {
        try
        {
            //turns off the error messages
            lblFHError.Visible = false;
			
			string[] arrUserSearch = fhsUser.getValues();//holds all of the items that the user has choosen
			DataTable dtFHDetails = DAL.getRow("", "Where  = 1 AND  = " + hfOrginalFHID.Value);//holds the Funeral Home details

			//checks if have choosen a user
            if (arrUserSearch.Length > 0)
            {
				//goes around each User that has been selected and updates the user id for this user
                for (int intIndex = 0; intIndex < arrUserSearch.Length; intIndex = intIndex + 3)
                {
					DataTable dtLinkFHUser = DAL.getRow("", "Where  = " + hfOrginalFHID.Value + " AND  = " + arrUserSearch[intIndex + 1]);//holds the 
					
					//checks if this user is already connected to this FH
					if (dtLinkFHUser.Rows.Count == 0)
					{
	                	//adds the link between this selection User and FH
						DAL.addUpdateLinkTableFHUser(Convert.ToInt32(hfOrginalFHID.Value), Convert.ToInt32(arrUserSearch[intIndex + 1]), 0, false);
						
						//sends the user an email that the have been add to the database
						//this is so wrong and needs to change however this is what the client whats
						General.sendHTMLMail(arrUserSearch[intIndex + 2], "You have been Add as a Site Administrator", string.Format(File.ReadAllText(HttpContext.Current.Server.MapPath("~/EmailTemplate/FHExistingUserSiteAdmin.html")), dtFHDetails.Rows[0][""].ToString(), dtFHDetails.Rows[0][""].ToString(), dtFHDetails.Rows[0][""].ToString()));
					}//end of if
				}//end of for loop
				
				//hides panAdditionalSiteAdministrator 
				panAdditionalSiteAdministrator.Visible = false;
				
				//resets the choose FH and User Search
				disableValidator(false);
				lbChooseUserFH_SelectedIndexChanged(sender, e);
            }//end of if
            //checks if there is a email
			else if(!string.IsNullOrEmpty(txtSiteAdminEmail.Text))
			{
				//checks if the user's email is in the database 
				//if so then tell them that they have to user another email
                if (General.checkIfEmailExist(txtSiteAdminEmail.Text) == false)
                {
                    string strUserPassword = General.genPassword();//holds the random password that be sent to the user

                    //sends the user an email that the have been add to the database
                    //this is so wrong and needs to change however this is what the client whats
                    General.sendHTMLMail(txtSiteAdminEmail.Text.Trim(), "Confirmation of Your Email for Site Administrator", string.Format(File.ReadAllText(Server.MapPath("~/EmailTemplate/thankYouSiteAdminSignUp.html")), txtSiteAdminFName.Text.Trim(), txtSiteAdminLName.Text.Trim(), txtSiteAdminEmail.Text.Trim(), strUserPassword, dtFHDetails.Rows[0][""].ToString(), dtFHDetails.Rows[0][""].ToString(), dtFHDetails.Rows[0][""].ToString()));

                    int intUserID = DAL.addUpdateUsers(0, txtSiteAdminFName.Text.Trim(), txtSiteAdminLName.Text.Trim(), "", "", "", "", "", "1", "1", "", "", txtSiteAdminEmail.Text.Trim(), PasswordHash.passwordHashtable.createHash(strUserPassword), "", 2, false, false);//holds the new users id just in case they want to create a Funeral Home

                    //adds the link between this selection User and FH
                    DAL.addUpdateLinkTableFHUser(Convert.ToInt32(hfOrginalFHID.Value), intUserID, 0, false);
                }//end of if
                else if (General.checkIfEmailExist(txtSiteAdminEmail.Text))//Email already exists in the database
                {
                    //sends the user an email that the have been add to the database
                    //this is so wrong and needs to change however this is what the client whats
                    General.sendHTMLMail(txtSiteAdminEmail.Text.Trim(), "Confirmation of Your Email for Site Administrator", string.Format(File.ReadAllText(Server.MapPath("~/EmailTemplate/FHExistingUserSiteAdmin.html")), dtFHDetails.Rows[0][""].ToString(), dtFHDetails.Rows[0][""].ToString(), dtFHDetails.Rows[0][""].ToString()));

                    //adds the link between this selection User and FH
                    DAL.addUpdateLinkTableFHUser(Convert.ToInt32(hfOrginalFHID.Value), Convert.ToInt32(intExistingUserID), 0, false);
                }//end of else

                //resets the add new admin sectoin
                txtSiteAdminFName.Text = string.Empty;
                txtSiteAdminLName.Text = string.Empty;
                txtSiteAdminEmail.Text = string.Empty;
                txtSiteAdminCEmail.Text = string.Empty;

                //hides panAdditionalSiteAdministrator 
                panAdditionalSiteAdministrator.Visible = false;

                //resets the choose FH
                lbChooseUserFH_SelectedIndexChanged(sender, e);
			}//end of else
			else
				throw new Exception("You Must have either selected a user or create one");
        }//end of try
        catch (Exception ex)
        {
            lblFHError.Text = ex.Message;// + " " + ex.StackTrace;
            lblFHError.Visible = true;
        }//end of catch
    }//end of btnSaveAdditionalSiteAdmin_Click()
	
	protected void lbAddOtherOfferings_Click(object sender, EventArgs e)
	{		
		//adds to the offerings count to now how many to count
		hfOtherOfferings.Value = Convert.ToString(Convert.ToInt32(hfOtherOfferings.Value) + 1);
	}//end of lbAddOtherOfferings_Click()
	
	protected void cmdPreview_Click(object sender, EventArgs e)
	{
		string[] arrImageOrder = ciFH.getImages();//holds all of the images in the order the user wants to display them in
		
		//goes around find the image using the name and the FH ID in the database 
		//then updates the order
		for(int intIndex = 0;intIndex < arrImageOrder.Length;intIndex++)
		{
			DataTable dtFHImageDetails = DAL.getRow("","Where  = " + hfCurrentFHID.Value + " AND  = '" + arrImageOrder[intIndex] + "'");//holds the Funeral Home and User details
			
			//checks if there is a image details found
			if(dtFHImageDetails.Rows.Count > 0)
				//updates this image order
				DAL.updateFuneralHomeImageOrder(Convert.ToInt32(dtFHImageDetails.Rows[0][""].ToString()), (intIndex + 1));
		}//end of for loop
		
		//goes to the Preview to allow the user to preivew and submit
		Response.Redirect("/funeralhome.aspx?edit=1&=" + hfCurrentFHID.Value);
	}//end of cmdPreview_Click()
}//end of Page