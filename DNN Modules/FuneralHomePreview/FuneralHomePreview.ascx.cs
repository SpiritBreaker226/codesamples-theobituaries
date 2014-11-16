// Display THe Funeral Home Before Going Live

public partial class DesktopModules_FuneralHomePreview_FuneralHomePreview : PortalModuleBase
{
    protected void Page_PreRender(object sender, EventArgs e)
    {
		try
		{	
			if (!IsPostBack)
	        {
				//checks if there is a FH id to use
				if (!string.IsNullOrEmpty(Request.QueryString[""]))
				{
					int intFuneralHomeID = Convert.ToInt32(Request.QueryString[""]);//holds the id of the FH
					DataTable dtFHDetails = DAL.getRow("", "WHERE  = " + intFuneralHomeID);//holds the Funeral Home details
															
					//checks if there is any details
					if (dtFHDetails != null && dtFHDetails.Rows.Count > 0)
					{
						DataTable dtImage = DAL.getRow("", "Where  = " + intFuneralHomeID + " Order by ");//holds the Images for this Obituary
						DataTable dtFHOfferings = DAL.getRow("", "Where  = " + intFuneralHomeID);//holds all of the offering for this FH
						DataTable dtFHAffiliations = DAL.getRow("", "Where  = " + intFuneralHomeID);//holds all of the affiliations for this FH
						DataTable dtFHObituary = DAL.getRow("", "Where  = " + intFuneralHomeID + " AND  = 'Published'");//holds all of the obituary services for this FH
						
						string strFHIDDir = dtFHDetails.Rows[0][""].ToString();//holds the this FHID dirtory
						string strDraftDir = "Draft";//holds the location of where the draft images are going
						string strExecptToTheRule = "";//hoolds any execpt to the rules as there will be times that the FH will need to fit a particllar FH for some odd resaon
						int intOfferingIndex = 1;//holds the index of the offering
						
						
						//sets the basics FH informaiton
						lblFuneralHomeName.Text = dtFHDetails.Rows[0][""].ToString();
                        chkFuneralHome.Text = " Receive a copy of all announcements published by <strong>" + dtFHDetails.Rows[0][""].ToString() + "  </strong>";
						lblFuneralHomeTagLine.Text = dtFHDetails.Rows[0][""].ToString();
						hlViewAllServices.NavigateUrl = "/SearchResult.aspx?fh=" + dtFHDetails.Rows[0][""].ToString();
						hlViewAllServices.Text = "View All - " + dtFHObituary.Rows.Count;
						lblFuneralHomeLocation.Text = dtFHDetails.Rows[0][""].ToString() + ", " + dtFHDetails.Rows[0][""].ToString();
						hfOrginalFHID.Value = dtFHDetails.Rows[0][""].ToString();
						hfFHID.Value = dtFHDetails.Rows[0][""].ToString();
						hfOrginalCity.Value = dtFHDetails.Rows[0][""].ToString();
						hfOrginalProvince.Value = dtFHDetails.Rows[0][""].ToString();
						hfOrginalCountry.Value = dtFHDetails.Rows[0][""].ToString();
						hfOrginalPostalCode.Value = dtFHDetails.Rows[0][""].ToString();
						
						//checks if there is a tagline
						if (!string.IsNullOrEmpty(lblFuneralHomeTagLine.Text))
							//displays a - for the in order to septurate the tagline with the city 
							lblFuneralHomeTagLine.Text += " &mdash; ";
						
						//checks if there is a orginalID if so then use that instead
						if (Convert.ToInt32(dtFHDetails.Rows[0][""].ToString()) != 0)
							strFHIDDir = dtFHDetails.Rows[0][""].ToString();
						else
							//since this FH is the live data the draft dirtory is not needed
							strDraftDir = "";
						
						//checks if there is about use
                        if (!string.IsNullOrEmpty(dtFHDetails.Rows[0][""].ToString()))
                        {
                            string strAboutUs = Server.HtmlDecode(dtFHDetails.Rows[0][""].ToString().Trim());
                            string[] arrAboutUs = strAboutUs.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                            if (arrAboutUs.Length > 1)
                            {
                                for (int lineNo = 0; lineNo < 1; lineNo++)
                                {
                                    dvFuneralHomeAboutUs.InnerHtml += arrAboutUs[lineNo];
                                }

                                dvFuneralHomeAboutUsMore.InnerHtml = "<div class='divFuneralHomeSummary'>";
                                for (int lineNo = 0; lineNo < arrAboutUs.Length; lineNo++)
                                {
                                    dvFuneralHomeAboutUsMore.InnerHtml += arrAboutUs[lineNo];
                                }
                                dvFuneralHomeAboutUsMore.InnerHtml += "</div>";
                            }
                            else
                            {
                                dvFuneralHomeAboutUs.InnerHtml = strAboutUs;
                                panFuneralHomeAboutUs.Visible = false;

                                if (strAboutUs.Length > 200)
                                {
                                    dvFuneralHomeAboutUs.InnerHtml = strAboutUs.Substring(0, 200) + " ...";
                                    dvFuneralHomeAboutUsMore.InnerHtml = "<div class='divFuneralHomeSummary'>" + strAboutUs + "</div>";
                                    panFuneralHomeAboutUs.Visible = true;
                                }
                            }
                        }
                        else
                            panFuneralHomeAboutUs.Visible = false;
						
						//checks if there is a FH Logo
						if (!string.IsNullOrEmpty(dtFHDetails.Rows[0][""].ToString().Trim()))
							imgFuneralHomeLogo.ImageUrl = "/images/FH/" + strFHIDDir + "/" + strDraftDir + "/" + dtFHDetails.Rows[0][""].ToString();
						else
							//sets the general image
							imgFuneralHomeLogo.ImageUrl = "/Portals/_default/Skins/Obit/Images/ob-header-logo.png";
			
						//checks if there is a FH URL
						if (!string.IsNullOrEmpty(dtFHDetails.Rows[0][""].ToString().Trim()))
						{
							//sets the FH URL
							lnkFuneralHomeUrl.Text = dtFHDetails.Rows[0][""].ToString().Replace("http://","").Replace("https://","");
							
							//checks if the url has http:// or https://
							if(dtFHDetails.Rows[0][""].ToString().IndexOf("http://") > -1 || dtFHDetails.Rows[0][""].ToString().IndexOf("https://") > -1)
								//adds the URL to the link
								lnkFuneralHomeUrl.NavigateUrl = dtFHDetails.Rows[0][""].ToString();
							else
								//adds the http:// to the start of URL
								lnkFuneralHomeUrl.NavigateUrl = "http://" + dtFHDetails.Rows[0][""].ToString();
						}//end of if
						else
							//removes this section as it is not needed
							panFuneralHomeUrl.Visible = false;
			
						//checks if there is a general email
						if (!string.IsNullOrEmpty(dtFHDetails.Rows[0][""].ToString().Trim()))
						{
							//sets the FH general email
							lnkFuneralHomeGeneralEmail.Text = dtFHDetails.Rows[0][""].ToString().Replace("http://", "");
							lnkFuneralHomeGeneralEmail.NavigateUrl = "mailto:" + dtFHDetails.Rows[0][""].ToString();
						}//end of if
						else
							//removes this section as it is not needed
							panFuneralHomeGeneralEmail.Visible = false;
						
						//checks if there is a FH Phone number
						if (!string.IsNullOrEmpty(dtFHDetails.Rows[0][""].ToString().Trim()))
						{
							//sets the FH phone number
							lnkFuneralHomePhoneNo.Text = dtFHDetails.Rows[0][""].ToString();
							lnkFuneralHomePhoneNo.NavigateUrl = "tel:" + dtFHDetails.Rows[0][""].ToString();
						}//end of if
						else
							//removes this section as it is not needed
							panFuneralHomePhoneNo.Visible = false;
						
						//checks if there is a FH Fax
						if (!string.IsNullOrEmpty(dtFHDetails.Rows[0][""].ToString().Trim()))
						{
							//sets the FH Fax
							lnkFuneralHomeFax.Text = dtFHDetails.Rows[0][""].ToString();
							lnkFuneralHomeFax.NavigateUrl = "tel:" + dtFHDetails.Rows[0][""].ToString();
						}//end of if
						else
							//removes this section as it is not needed
							panFuneralHomeFax.Visible = false;
			
						//checks if there is a address
						if (!string.IsNullOrEmpty(dtFHDetails.Rows[0][""].ToString().Trim()))
						{
							lblFuneralAddress.Text = dtFHDetails.Rows[0][""].ToString() + ", <br/>" + dtFHDetails.Rows[0][""].ToString() + ", " + dtFHDetails.Rows[0][""].ToString() + " " + dtFHDetails.Rows[0][""].ToString();
							
							lnkGoogleMap.NavigateUrl = "https://maps.google.com/maps?q=" + dtFHDetails.Rows[0][""].ToString() + ", " + dtFHDetails.Rows[0][""].ToString() + ", " + dtFHDetails.Rows[0][""].ToString() + " " + dtFHDetails.Rows[0][""].ToString();
						}//end of if

						//checks if there is a already a location that the user wants to use
						if (dtFHDetails.Rows[0][""] == null || string.IsNullOrEmpty(dtFHDetails.Rows[0][""].ToString().Trim()))
							//finds the location base on the funeral homes address
							litLocGeo.Text = "getLocationGeo('" + dtFHDetails.Rows[0][""] + "," + dtFHDetails.Rows[0][""] + "," + dtFHDetails.Rows[0][""] + "',map,false,'" + hfMapLatitude.ClientID + "','" + hfMapLongitude.ClientID + "');";
						else
						{
							string[] arrLatLong = dtFHDetails.Rows[0][""].ToString().Trim().Split(',');//holds the latitude and longitude
			
							//makes a pin in the location that the user want it to be
							litLocGeo.Text = "//centers the map to the location\n" +
							"map.setCenter(new google.maps.LatLng(" + dtFHDetails.Rows[0][""] + "));\n" +
							"var marker = new google.maps.Marker({\n" +
								"draggable: false,\n" +
								"map: map,\n" +
								"position: new google.maps.LatLng(" + dtFHDetails.Rows[0][""] + ")\n" +
							"});//end of marker" +
							"//sets the defualt for the latitude and the longitude\n" +
							"getDocID('" + hfMapLatitude.ClientID + "').value = '" + arrLatLong[0] + "';\n" +
							"getDocID('" + hfMapLongitude.ClientID + "').value = '" + arrLatLong[1] + "';\n" +
							"//sets an event to change the latitude and longitude hidden fields\n" +
							"google.maps.event.addListener(marker, 'dragend', function (event) {\n" +
								"getDocID('" + hfMapLatitude.ClientID + "').value = this.getPosition().lat();\n" +
								"getDocID('" + hfMapLongitude.ClientID + "').value = this.getPosition().lng();\n" +
							"});";
						}//end of else
						
						//checks if there is any images to display
						if(dtImage.Rows.Count > 0)
							//sets an iframe to display the image slider as the image slider uses a advance jquery
							//the DNN does not run
							litImageSlider.Text = "<iframe id='iframeImageSlider' src='/ImageSlider.aspx?=" + hfFHID.Value + "' scrolling='no'></iframe>";
						
						//goes around adding the offerings
						foreach (DataRow drFHOfferings in dtFHOfferings.Rows)
						{
							//checks if this offering is even or odd as all even go to the left for display odd to the right
							if ((intOfferingIndex % 2 == 0))
								litOfferingsLeft.Text += "<li>" + drFHOfferings[""].ToString() + "</li>";
							else
								litOfferingsRight.Text += "<li>" + drFHOfferings[""].ToString() + "</li>";
								
							intOfferingIndex++;
						}//end of foreach
			
						//goes around adding the affiliations
						foreach (DataRow drFHAffiliations in dtFHAffiliations.Rows)
						{
							litAffiliationsLogo.Text += "<li>" + 
								"<img height='57' src='/Portals/_default/Skins/Obit/images/affiliates/" + drFHAffiliations[""].ToString() + "' />" + 
							"</li>";
						}//end of foreach
						
						//checks if there is any Affiliations
						if(string.IsNullOrEmpty(litAffiliationsLogo.Text))
							//turns off the Affiliation
							panAffiliation.Visible = false;
	
						panOurFuneralService.Visible = false;
						rpObituaries.Controls.Clear();
						
						//checks if this is the Sample FH
						if(intFuneralHomeID == 5996)
							//remove most of its Obituary and Memorial
							//as this is what the cient wants
							strExecptToTheRule = " AND ob.Id IN (495,494)";

						if (dtObituaries != null && dtObituaries.Rows.Count > 0)
						{
							rpObituaries.DataSource = dtObituaries;
							rpObituaries.DataBind();
							panOurFuneralService.Visible = true;
						}//end of if
					}//end of if
					else
						//sends the user to the homepage if there is no id
						Response.Redirect("/Home.aspx", true);
				}//end of if
				else
					//sends the user to the homepage if there is no id
					Response.Redirect("/Home.aspx", true);
					
				//checks if there is a edit section
				if (Request.QueryString["edit"] != null)
				{
					//checks if the user is already logged in and if so then send them to the homepage
					if (Session["MemberLogin"] == null)
						Response.Redirect("/MyAccount/Login.aspx?url=" + Server.UrlEncode(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "")), true);
										
					//displays the edit buttons for the user to submit the page
					cmdEdit.Visible = true;
					cmdSave.Visible = true;
				}//end of if

                if (Session["MemberLogin"] == null)
                {
                    panBookmark.Visible = false;
                }
			}//end of if
		}//end of try
        catch (Exception ex)
        {			
            lblError.Text = ex.Message;
            lblError.Visible = true;
        }//end of catch
    }//end of Page_PreRender()
	
	protected void cmdEdit_Click(object sender, EventArgs e)
    {
		//goes to the Preview to allow the user to preivew and submit
		Response.Redirect("/myaccount/funeralhomes/funeralhomemanagement.aspx?=" + hfFHID.Value);
	}//end of cmdEdit_Click()
	
	protected void cmdSave_Click(object sender, EventArgs e)
    {
        try
        {
			//checks if the user is already logged in and if so then send them to the homepage
			if (Session[""] == null)
				Response.Redirect("/MyAccount/Login.aspx?url=" + Server.UrlEncode(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "")), true);
			
			//resets the error message
			lblError.Visible = false;
			
			//checks if the this is a orginal FHID
			if (Convert.ToInt32(hfOrginalFHID.Value) != 0)
			{
				DataTable dtFHUserDetails = DAL.getRow("","Where  = " + hfOrginalFHID.Value + " AND  = " + Convert.ToInt32(Session[""]));//holds the FH and User details For send out the email
				string strLangEdit = "creat";//holds the if this is a create or edit for the email and display
			
				//checks if there is a publish date as this means it was already publish
				if(!string.IsNullOrEmpty(dtFHUserDetails.Rows[0][""].ToString()))
					//changes strLangEdit to edit for the email and display
					strLangEdit = "edit";
			
				//updates the states for both the orginal and the draft in order for the user not to edit it
				//DAL.updateFuneralHomeStatus(Convert.ToInt32(hfOrginalFHID.Value), -2);
				DAL.updateFuneralHomeStatus(Convert.ToInt32(hfFHID.Value), -2);
					
				//updates the row of the latest for the draft apporvall
				DAL.updateFuneralHomeStatus(Convert.ToInt32(hfOrginalFHID.Value), 1);
				
				//Because the client cannot make up there mind this is should be commented out if they can there mind
				//again as there is too much code foring on draft now that removing it would be a little bit of a 
				//undertaking espally when the client will more likily change there mind again and have drafts
								
				//changes the Thank you message to this
                litFHThankYou.Text = "Thank you for " + strLangEdit + "ing your Funeral Home Web Page.  We will contact you via email once your page is Published and Live.  At anytime, please return to the Funeral Home Management tab in your My Account area for additional edits, changes or updates including new images and service offerings.  If you have any suggestions or would like to see additional functionality added, please contact us through the Contact tab on the header above.";
				
				//sends an email to saying thate they have been update
				//uncomment this back when the client decides to change there mind on having drafts again
				General.sendHTMLMail(dtFHUserDetails.Rows[0][""].ToString(), "Your Request is Under Review.", string.Format(File.ReadAllText(Server.MapPath("~/EmailTemplate/FHSignUpReview.html")),dtFHUserDetails.Rows[0][""].ToString(), dtFHUserDetails.Rows[0][""].ToString(),dtFHUserDetails.Rows[0][""].ToString(), dtFHUserDetails.Rows[0][""].ToString(), strLangEdit));

                string fullAddress = dtFHUserDetails.Rows[0][""].ToString() + ", " + dtFHUserDetails.Rows[0][""].ToString() + "<br/> " + dtFHUserDetails.Rows[0][""].ToString() + ", " + dtFHUserDetails.Rows[0][""].ToString();
				
				//sends an email to the obituaries tell them to review and appove this funeral home
                General.sendHTMLMail("", dtFHUserDetails.Rows[0][""].ToString() + " is ready for review", string.Format(File.ReadAllText(Server.MapPath("~/EmailTemplate/FHCheckReview.html")), dtFHUserDetails.Rows[0][""].ToString(), dtFHUserDetails.Rows[0][""].ToString(), dtFHUserDetails.Rows[0][""].ToString(), dtFHUserDetails.Rows[0][""].ToString(), strLangEdit, fullAddress));
				
				//Turn on the thank you message and removes the edit funeral home
				panThankYou.Visible = true;
				panSignUp.Visible = false;
			}//end of if
        }//end of try
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            lblError.Visible = true;
        }//end of catch
    }//end of cmdSave_Click()
}//end of Module