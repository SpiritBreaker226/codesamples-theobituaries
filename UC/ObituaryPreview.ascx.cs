/// displays the left part of the obituary details page

public partial class ObituaryPreview : System.Web.UI.UserControl
{
	private int _Cols = 1;
    private int _Rows = 6;
    private int _PageSize;
    private int _HiddenPageIndex;
    private int _LastPageIndex;
	private int _Count;
	private bool _PagingVarsInitialized = false;
	private string strPreviewSytleClass = "";//holds the class when the obituary is in preview mode
	
	private void Bind(int intSelectedIndex)
    {
		//UPDATE THE RESULT INDEX MESSAGE
        int startRowIndex = (_PageSize * _HiddenPageIndex);
        int endRowIndex = startRowIndex + _PageSize;
	
        if (endRowIndex > _Count) 
			endRowIndex = _Count;
		
		if (_HiddenPageIndex > 0) 
			startRowIndex++;

		//sets the condolences and the number of them for this Obituary
		dlCondolence.DataSource = DAL.getRows("", " DESC", startRowIndex, endRowIndex, " = 0 AND  = " + hfObituaryID.Value);
		dlCondolence.DataBind();
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
		try
		{
			if (!_PagingVarsInitialized || forceRefresh)
			{
				//checks if there is a id of the Obituery if not then 
				//send the user to the home page
				if (!string.IsNullOrEmpty(Request.QueryString["ObituariesID"]))
				{
					DataTable dtCount = DAL.countRows("", " = 0 AND  = " + Request.QueryString["ObituariesID"]);//holds the count grand total of condolences for this obituary/memorial
	
					//sets the grand total of condolences for this obituary/memorial
					lblNumberOfCondolences.Text = dtCount.Rows[0][""].ToString();
		
					_HiddenPageIndex = Convert.ToInt32(HiddenPageIndex.Value);
					_Count = Convert.ToInt32(dtCount.Rows[0][""].ToString());
					_LastPageIndex = ((int)Math.Ceiling(((double)_Count / (double)_PageSize))) - 1;
					_PagingVarsInitialized = true;
				}//end of if
			}//end of if
		}//end of try
        catch (Exception ex)
        {
            lblMainError.Text = ex.Message;
            lblMainError.Visible = true;
        }//end of catch
    }//end of InitializePagingVars()
		
	protected void Page_PreRender(object sender, EventArgs e)
    {
		try
		{
			//code will load and initialize the JavaScript SDK with all standard options
			if (!Page.ClientScript.IsClientScriptBlockRegistered("facebook_api"))
               	Page.ClientScript.RegisterClientScriptInclude("facebook_api", String.Format("http://connect.facebook.net/{0}/all.js", "en_US"));
 
           	if (!Page.ClientScript.IsStartupScriptRegistered("facebook_api_init"))
				Page.ClientScript.RegisterStartupScript(typeof(string), "facebook_api_init", String.Format("FB.init({{appId: '{0}', status: true, cookie: true, xfbml: true, oauth: true }});", "574005609290762"), true);
 
           	if (!Page.ClientScript.IsStartupScriptRegistered("facebook_login"))
           	{
               	string facebookLogin = String.Format("function fblogin() {{ FB.login(function(response) {{ if (response.authResponse) {{ {0} }} else {{ {1} }}}}, {{ scope: '{2}' }});}}", this.Page.ClientScript.GetPostBackEventReference(this.Page, "FacebookLogin", false), this.Page.ClientScript.GetPostBackEventReference(this.Page, "FacebookLogout", false), "publish_stream,email");
				
               	Page.ClientScript.RegisterStartupScript(typeof(string), "facebook_login", facebookLogin, true);
           	}//end of if
 
           	if (!Page.ClientScript.IsStartupScriptRegistered("facebook_logout"))
           	{
               	string facebookLogout = String.Format("function fblogout() {{ FB.logout(function(response) {{ {0} }}); }}", this.Page.ClientScript.GetPostBackEventReference(this.Page, "FacebookLogout", false));
               	Page.ClientScript.RegisterStartupScript(typeof(string), "facebook_logout",
                  facebookLogout, true);
           	}//end of if
			
			FacebookApp facebookApp = new FacebookApp();//holds an object of the FB
				
			//checks if the user is connected to the session
			if (facebookApp.Session != null && facebookApp.AccessToken != null)
			{
				var facebookUser = facebookApp.Api("me") as JsonObject;//holds the object of the user
	
				//checks if the the user has submited a condolence		
				if(panCondoloncesThankYou.Visible == false)
					//displays the Condolace section
					panLeaveCondolence.Style.Add("display", "block");
				else
					//closes the Leave Condolence section
					panLeaveCondolence.Style.Add("display", "none");
					
				//removes the socail and user text boxes as the user is now already sign in
				//and turns on the label of the user and displays the logout button
				panCondolenceSocialOption.Visible = false;
				panConEnterNameEmail.Visible = false;
				panConNameEmail.Visible = true;
				panLogOutFB.Visible = true;
				
				//checks if there is a user name 
				if (facebookUser.ContainsKey("username") && facebookUser["username"] != null)
					lblConnectedUser.Text = facebookUser["username"].ToString();

				//checks if there is a name from FB	
				if (facebookUser.ContainsKey("name") && facebookUser["name"] != null)		
					//displays the name from FB
					lblConnectedName.Text = facebookUser["name"].ToString();
				
				//checks if there is a email from FB
				if (facebookUser.ContainsKey("email") && facebookUser["email"] != null)
					//displays the email from FB
					lblConnectedEmail.Text = facebookUser["email"].ToString();
					
				//sets the which social network the user is usiing
				hfObituaryCondolenceSocialNetwork.Value = "1";
			}//end of if
			else
			{
				//adds the Social Options and allow the user to enter their own email
				//and removes the FB Logout and email
				panCondolenceSocialOption.Visible = true;
				panConEnterNameEmail.Visible = true;
				panConNameEmail.Visible = false;
				panLogOutFB.Visible = false;
		
				//resets all of the social fields
				lblConnectedUser.Text = "";
				lblConnectedName.Text = "";
				lblConnectedEmail.Text = "";
				hfObituaryCondolenceSocialNetwork.Value = "0";
			}//end of else
			
			//checks if the user is logged into twitter
			if (Request["oauth_token"] != null)
			{
				Session["TwitterRequestToken"] = Request["oauth_token"].ToString();
                Session["TwitterPin"] = Request["oauth_verifier"].ToString();
 
                var tokens = OAuthUtility.GetAccessToken(
                    ConfigurationManager.AppSettings["consumerKey"],
                    ConfigurationManager.AppSettings["consumerSecret"],
                    Session["TwitterRequestToken"].ToString(),
                    Session["TwitterPin"].ToString());
 
                OAuthTokens oatAccess = new OAuthTokens()
					{
						AccessToken = tokens.Token,
						AccessTokenSecret = tokens.TokenSecret,
						ConsumerKey = ConfigurationManager.AppSettings["consumerKey"],
						ConsumerSecret = ConfigurationManager.AppSettings["consumerSecret"]
					};
 
 				TwitterResponse<TwitterUser> twitterResponse = TwitterAccount.VerifyCredentials(oatAccess);//holds the response that has come from twitter with if this is a good token
 
                if (twitterResponse.Result == RequestResult.Success)
				{
                   //We now have the credentials, so make a call to the Twitter API.
					HtmlMeta hmTwitter = new HtmlMeta();//holds the meta takes that will go into the header
			    	HtmlHead head = (HtmlHead)Page.Header;//holds the reference of the Header
										
					//removes the socail and user text boxes as the user is now already sign in
					//and turns on the label of the user
					panCondolenceSocialOption.Visible = false;
					panConEnterNameEmail.Visible = false;
					panConNameEmail.Visible = true;
					
					//sets the which social network the user is usiing
					hfObituaryCondolenceSocialNetwork.Value = "2";
													
					//sets the username and actully name
					lblConnectedUser.Text = twitterResponse.ResponseObject.ScreenName;
					lblConnectedName.Text = twitterResponse.ResponseObject.Name;

					//define an HTML meta twitter:creator in the header 
					hmTwitter.Name = "twitter:creator";
					hmTwitter.Content = lblConnectedUser.Text;
					hmTwitter.Controls.Add(hmTwitter);
					
					//because twitter does not allow access to the email address this should not be display
					panConnectedEmail.Visible = false;
				}//end of if
			}//end of if
				

                DataTable dtObituaryDetails = null;
                if (panSidebarLinks.Visible)
                    dtObituaryDetails = DAL.getRow("", "WHERE  = '" + General.ObituaryStatus.Published.ToString() + "' AND  = " + hfObituaryID.Value);//holds the Obituary details
                else
                    dtObituaryDetails = DAL.getRow("", "WHERE  = " + hfObituaryID.Value);//holds the Obituary details
								
				//checks if there is any details for this obituary
				if (dtObituaryDetails != null && dtObituaryDetails.Rows.Count > 0)
				{
					int intIndexServiceID = 0;//holds the unquie id of the row
					string strLastFHID = "";//holds what is the last FHID
					string strShareDescription = "";//holds the description that will share to the world
					DataTable dtImage = DAL.getRow("","Where  = " + hfObituaryID.Value + " Order by ");//holds the Images for this Obituary
					DataTable dtObitService = DAL.getRow("", "WHERE  = " + hfObituaryID.Value + " Order by , , ");//gets all services for this obituary
					DataTable dtObitFlowers = DAL.getRow("", "WHERE  = " + hfObituaryID.Value + " AND  = 1 Order by , ");//holds the Flower Recipient
					DataTable dtObitCards = DAL.queryDbTable("SELECT , ,  + ' ' +   FROM  WHERE  = " + hfObituaryID.Value + " AND  = 1");

					//checks if there is any images to display
					if(dtImage.Rows.Count > 0)
						//sets an iframe to display the image slider as the image slider uses a advance jquery
						//the DNN does not run
						litImageSlider.Text = "<iframe id='iframeImageSlider' src='/ImageSlider.aspx?=" + hfObituaryID.Value + "' scrolling='no'></iframe>";

                    //checks if this is PrePla or Memorial n if so then change the text for Leave Condolence
                    if (dtObituaryDetails.Rows[0][""].ToString() == ((int)General.ObituaryType.PrePlan).ToString() || dtObituaryDetails.Rows[0][""].ToString() == ((int)General.ObituaryType.Memorial).ToString())
                    {
                        hlLeaveCondolence.Text = "Leave a Condolence or Message";
                        lblNoOfCondolences.Text = " Condolences or Messages";
						panLeaveCondolenceTitleLeft.CssClass += " divLeaveCondolenceMmemoralTitleLeft";
                    }//end of if
					
					//checks if this user is logged in and they have not logged into a soical network
					//get there there details instead of typing it in
					if(Session[""] != null && panConNameEmail.Visible == false)
					{
						DataTable dtUserDetails = DAL.getRow("", "WHERE  = " + Session[""].ToString());//holds the this user detail that is logged in
						
						//sets the users name and email to tell the user that this is what is going to be displayed
						lblConnectedName.Text = dtUserDetails.Rows[0][""].ToString() + " " + dtUserDetails.Rows[0][""].ToString();
						lblConnectedEmail.Text = dtUserDetails.Rows[0][""].ToString();
						lblReminderEmail.Text = dtUserDetails.Rows[0][""].ToString();
																		
						//displays the user name and email to tell the user who the are login in as
						panConNameEmail.Visible = true;
						//lblReminderEmail.Visible = true;
						panConEnterNameEmail.Visible = false;
						panCondolenceSocialOption.Visible = false;                        
					}//end of if

					//sets the condolences, the number of them for this Obituary and paging
			        BindDesignsPanel();
					
					//sets the on Click for the link Leave Condolence to turn it off and on
					hlLeaveCondolence.Attributes.Add("onClick", "javascript:toggleLayer('" + panLeaveCondolence.ClientID + "', '', '');");
                    hlSectionCondolence.Attributes.Add("onClick", "javascript:toggleLayer('" + panLeaveCondolence.ClientID + "', '', '');" + panLeaveCondolence.ClientID + ".scrollIntoView(true);");
					
					//sets the basis settings
					lblName.Text = dtObituaryDetails.Rows[0][""].ToString() + ", "  + dtObituaryDetails.Rows[0][""].ToString() + " " + dtObituaryDetails.Rows[0][""].ToString();
					lblPrintName.Text = dtObituaryDetails.Rows[0][""].ToString() + ", "  + dtObituaryDetails.Rows[0][""].ToString() + " " + dtObituaryDetails.Rows[0][""].ToString();
					
					//sets the sidebar name of the user
                    lblShareForName.Text = dtObituaryDetails.Rows[0][""].ToString() + " " + dtObituaryDetails.Rows[0][""].ToString();
					lblReminderForName.Text = dtObituaryDetails.Rows[0][""].ToString() + " " + dtObituaryDetails.Rows[0][""].ToString();
					lblReminderForName2.Text = dtObituaryDetails.Rows[0][""].ToString() + " " + dtObituaryDetails.Rows[0][""].ToString();
					
					//sets the title of the page
					Page.Title = "The Obituaries - Details for " + dtObituaryDetails.Rows[0][""].ToString() + " " + dtObituaryDetails.Rows[0][""].ToString();
					
					//sets the flower FH sidebar
					panFlowersFH.Visible = Convert.ToBoolean(dtObituaryDetails.Rows[0][""].ToString());
					
					//checks if there is a FHID
                    if (Convert.ToInt32(dtObituaryDetails.Rows[0][""].ToString()) > 0)
                    {
                        //sets the FHID for this flower request
                        hlFlowerFH.NavigateUrl += dtObituaryDetails.Rows[0][""].ToString() + "&oid=" + hfObituaryID.Value;
                        chkFuneralHome.Text = " All announcements from <strong>" + DAL.queryDbScalar("SELECT  FROM  WHERE  = '" + dtObituaryDetails.Rows[0][""].ToString() + "'") + "</strong>";
                    }//end of if
                    else
                        //removes the Flowers FH is there is no FHID to use
                        panFlowersFH.Visible = chkFuneralHome.Visible = false;
					
					//sets the Anotehr Address URL for flowers and cards	
					hlSendCardsAnotherAddress.NavigateUrl = "/Obituaries/sympathycards.aspx?ObituariesID=" + hfObituaryID.Value;
					hlSendFlowersAnotherAddress.NavigateUrl = "/Obituaries/flower.aspx?person=2&FHPID=0&oid=" + hfObituaryID.Value;
					
					//checks if there is any flowsers 
					if(dtObitFlowers.Rows.Count > 0)
					{
						//sets the flowers recipient in the sidebar
						dlFlowerRecipient.DataSource = dtObitFlowers;
						dlFlowerRecipient.DataBind();
					}//end of if
					else
						//removes the 'or' flowers from view
                        panFlowersOr.Visible = false;
											
					//checks if there is any cards 
					if (dtObitCards != null && dtObitCards.Rows.Count > 0)
					{
						//sets the card recipient in the sidebar
						rpCardReceiver.DataSource = dtObitCards;
						rpCardReceiver.DataBind();
					}//end of if
					else
						//removes the 'or' card from view
						panCardOr.Visible = false;
						
					//resets lblBirthDateAndPassingDate
					lblBirthDateAndPassingDate.Text = "";
		
					//checks if there is a birth date
					if(!string.IsNullOrEmpty(dtObituaryDetails.Rows[0][""].ToString()))
						lblBirthDateAndPassingDate.Text += Convert.ToDateTime(dtObituaryDetails.Rows[0][""].ToString()).ToString("MMMM dd, yyyy");
						
					//checks that there must be both a birth\death date for - to display
					if(!string.IsNullOrEmpty(dtObituaryDetails.Rows[0][""].ToString()) && !string.IsNullOrEmpty(dtObituaryDetails.Rows[0][""].ToString())) 
						lblBirthDateAndPassingDate.Text += " - ";
						
					//checks if there is a death date or a is this a pre-plan obituarie
					if(!string.IsNullOrEmpty(dtObituaryDetails.Rows[0][""].ToString()))
						//sets the death year
						lblBirthDateAndPassingDate.Text += Convert.ToDateTime(dtObituaryDetails.Rows[0][""].ToString()).ToString("MMMM dd, yyyy");
						
					//checks if there is any text in the lblBirthDateAndPassingDate
					//in order to add it to the print version
					if(!string.IsNullOrEmpty(lblBirthDateAndPassingDate.Text))
						lblPrintBirthDateAndPassingDate.Text = lblBirthDateAndPassingDate.Text;
						
					//sets the twitter sharing for this obituery
					hlShareTwiiter.NavigateUrl = "https://twitter.com/share?url=" + Server.UrlEncode("http://theobituaries.ca/Obituaries.aspx?ObituariesID=" + hfObituaryID.Value) + "&text=Obituary for " + lblName.Text;
					
					//sets the linkin sharing for this obituery
					ltlLinkin.Text = "<script type='IN/Share' data-url='" + Server.UrlEncode("http://theobituaries.ca/Obituaries.aspx?ObituariesID=" + hfObituaryID.Value) + "'></script>";
					
					//checks if strShareDescription has any content
					if(string.IsNullOrEmpty(strShareDescription))
						//uses a default text as to not have DNN text display on the user's condolences
						strShareDescription = "A condolence for " + dtObituaryDetails.Rows[0][""].ToString() + " "  + dtObituaryDetails.Rows[0][""].ToString();
					
					//sets the Facebook sharing for this obituery
					litFB.Text = "<iframe src='http://www.facebook.com/plugins/like.php?href=http%3A%2F%2F" + Request.Url.Host + "%2FObituaries.aspx%3FObituariesID%3D" + hfObituaryID.Value + "&amp;send=false&amp;layout=button_count&amp;width=60&amp;show_faces=false&amp;font&amp;colorscheme=light&amp;action=like&amp;height=21&amp;appId=574005609290762' scrolling='no' frameborder='0' style='overflow:hidden;height:21px;' allowTransparency='true'></iframe>";
								  
					HtmlMeta hmFB = new HtmlMeta();//holds the meta takes that will go into the header
				    HtmlHead head = (HtmlHead)Page.Header;//holds the reference of the Header
					
                    lnkSendCardToFuneralHome.Visible = false;

                    if (dtObituaryDetails.Rows[0][""].ToString() == "True" && !string.IsNullOrEmpty(dtObituaryDetails.Rows[0][""].ToString()) && dtObituaryDetails.Rows[0][""].ToString() != "0")
                    {
                        lnkSendCardToFuneralHome.NavigateUrl = "/Obituaries/sympathycards.aspx?ObituariesID=" + hfObituaryID.Value + "&FuneralHomeID=" + dtObituaryDetails.Rows[0][""].ToString();
                        lnkSendCardToFuneralHome.Visible = true;
                    }//end of if

                    if (!string.IsNullOrEmpty(dtObituaryDetails.Rows[0][""].ToString()))
                        hfObituaryCreatorEmail.Value = DAL.queryDbScalar("SELECT  FROM  WHERE  = '" + dtObituaryDetails.Rows[0][""].ToString() + "'");
						
					//resets litObituaryServices
					litObituaryServices.Text = "";
							
					//checks if there is any sevices
					if(dtObitService.Rows.Count > 0)
					{
						//goes around adding the services of the Obituary
						foreach (DataRow drObitService in dtObitService.Rows)
						{
							//checks if there is a FHID or or  is the different
							//this is different from the next one as this will skip the whole row
							if(Convert.ToInt32(drObitService[""].ToString()) != 0 || Convert.ToInt32(drObitService[""].ToString()) == 0 && strLastFHID != drObitService[""].ToString())
							{ 
								//checks if the last FHID or  is the different
								if(strLastFHID != drObitService[""].ToString() || Convert.ToInt32(drObitService[""].ToString()) == 0 && strLastFHID != drObitService[""].ToString())
								{
									DataTable dtFHDetails = DAL.getRow("", "WHERE  = " + Convert.ToInt32(drObitService[""].ToString()));//holds the Funeral Home details
						
									//checks if this is a the first service
									//as there is not last service yet
									if(!string.IsNullOrEmpty(strLastFHID))			
										//create a ends the last serivce 
										litObituaryServices.Text += "</div>";
									
									//starts a new one
									litObituaryServices.Text += "<div class='customContainer divObiturayDetailsServiceContainer'>" + 
										"<div class='customLeft divObiturayDetailsServiceLeft'>";
										
											//checks if this FH is in the database and if it is a partner
											if (dtFHDetails != null && dtFHDetails.Rows.Count > 0)
											{
												string strSearchItemMap = "";//holds the map of the search itme
												
												//checks if there is a address to search for the google map
												if(!string.IsNullOrEmpty(dtFHDetails.Rows[0][""].ToString()) && !string.IsNullOrEmpty(dtFHDetails.Rows[0][""].ToString()) && !string.IsNullOrEmpty(dtFHDetails.Rows[0][""].ToString()))
												{
													//checks if there is a already a location that the user wants to use
													if(dtFHDetails.Rows[0][""] == null || string.IsNullOrEmpty(dtFHDetails.Rows[0][""].ToString().Trim()))					
														//adds the funcation that will activate the google map hidden
														strSearchItemMap = "getLocationHiddenGeo(&quot;" + dtFHDetails.Rows[0][""].ToString().Replace("'", "&lsquo;").Replace("\"", "&quot;") + "," + dtFHDetails.Rows[0][""].ToString().Replace("'", "&lsquo;").Replace("\"", "&quot;") + "," + dtFHDetails.Rows[0][""] + "&quot;,&quot;" + dtFHDetails.Rows[0][""].ToString().Replace("'", "&lsquo;").Replace("\"", "&quot;") + "&quot;,43.64100156269233,-79.38599562435303);";
													else
														//adds funcation that will activate the google map hidden what the user want to display
														strSearchItemMap = "getLocationHiddenGeo(&quot;" + dtFHDetails.Rows[0][""].ToString().Replace("'", "&lsquo;").Replace("\"", "&quot;") + "," + dtFHDetails.Rows[0][""].ToString().Replace("'", "&lsquo;").Replace("\"", "&quot;") + "," + dtFHDetails.Rows[0][""] + "&quot;,&quot;" + dtFHDetails.Rows[0][""].ToString().Replace("'", "&lsquo;").Replace("\"", "&quot;") + "&quot;," + dtFHDetails.Rows[0][""] + ");";
												}//end of if
																								
												//loads the ability to display the map
                                                if (panSidebarLinks.Visible)
                                                    litObituaryServices.Text += "<a href='javascript:void(0);' onClick='" + strSearchItemMap + "toggleLayer(&quot;divHiddenHeaderMap&quot;,&quot;divGrayBG&quot;,&quot;&quot;);getDocID(&quot;lblHiddenMapName&quot;).innerHTML=&quot;Location for " + dtFHDetails.Rows[0][""].ToString().Replace("'", "&lsquo;").Replace("\"", "&quot;") + " - " + dtFHDetails.Rows[0][""].ToString().Replace("'", "&lsquo;").Replace("\"", "&quot;") + ", " + dtFHDetails.Rows[0][""].ToString() + "&quot;;'><img alt='Map' src='/Portals/_default/skins/Obit/Images/obits-map.jpg' /></a>";
                                                else
                                                    litObituaryServices.Text += "<img alt='Map' src='/Portals/_default/skins/Obit/Images/obits-map.jpg' />";
											}//end of if
											//displays a custom FH that the user has create
											else
												litObituaryServices.Text += "<a href='javascript:void(0);' onClick='getLocationHiddenGeo(&quot;" + drObitService[""].ToString().Replace("'", "&lsquo;").Replace("\"", "&quot;") + "," + drObitService[""].ToString().Replace("'", "&lsquo;").Replace("\"", "&quot;") + "," + drObitService[""] + "&quot;,&quot;" + drObitService[""].ToString().Replace("'","&lsquo;").Replace("\"","&quot;") + "&quot;,43.64100156269233,-79.38599562435303);toggleLayer(&quot;divHiddenHeaderMap&quot;,&quot;divGrayBG&quot;,&quot;&quot;);getDocID(&quot;lblHiddenMapName&quot;).innerHTML=&quot;Location for " + drObitService[""].ToString().Replace("'","&lsquo;").Replace("\"","&quot;") + " - " + drObitService[""].ToString().Replace("'","&lsquo;").Replace("\"","&quot;") + ", " + drObitService[""].ToString() + "&quot;;'><img alt='Map' src='/Portals/_default/skins/Obit/Images/obits-map.jpg' /></a>";
												
										litObituaryServices.Text += "</div>" + 
										"<div class='customRight divObiturayDetailsServiceRight'>";
											
												//checks if this FH is in the database and if it is a partner
												if (dtFHDetails != null && dtFHDetails.Rows.Count > 0)
												{
													//checks if this is a Publish FH or non
                                                    if (dtFHDetails.Rows[0][""].ToString() == "1" && panSidebarLinks.Visible)
														litObituaryServices.Text += "<a href='/FuneralHome.aspx?FuneralHomeId=" + dtFHDetails.Rows[0][""].ToString() + "'>" + 
															dtFHDetails.Rows[0][""].ToString() + 
														"</a>";
													else
														litObituaryServices.Text += "<label class='lblObituaryFHServiceName'>" +
															dtFHDetails.Rows[0][""].ToString() + 
														"</label>";
													
													litObituaryServices.Text += "<div class='divObiturayServiceLocation'>" +
														dtFHDetails.Rows[0][""].ToString() + ", " + dtFHDetails.Rows[0][""].ToString() + ", " + dtFHDetails.Rows[0][""].ToString() + ", " + dtFHDetails.Rows[0][""].ToString() + 
													"</div>";
												}//end of if
												//displays a custom FH that the user has create
												else
												{
													//checks if there is a custom name for this custom FH
													if(!string.IsNullOrEmpty(drObitService[""].ToString()))
														litObituaryServices.Text += "<label class='lblObituaryFHServiceName'>" +
															drObitService[""].ToString() + 
														"</label>" +
														"<div class='divObiturayServiceLocation'>";
													else
														//beacuse divObiturayServiceLocation has a padding this will move
														//make the address not appeaeal with the map icon this wll fix it
														litObituaryServices.Text += "<div class='divObiturayServiceNoCustomLocationName'>";
														
													litObituaryServices.Text += drObitService[""].ToString() + ", " + drObitService[""].ToString() + ", " + drObitService[""].ToString() + ", " + drObitService[""].ToString() + 
													"</div>";
												}//end of else
												 
											//sets the link to open the location and start the service detail div
											litObituaryServices.Text += "<a href='#service-location-" + intIndexServiceID + "' class='toggle-location scroll-show'>Open Location</a>" + 
										"</div>" + 
										"<div class='customFooter divObiturayDetailsServiceFooter'></div>";
									
									//checks if this row is a FH or a custom address
									//and then updates the strLastFHID
									if(Convert.ToInt32(drObitService[""].ToString()) == 0)
										strLastFHID = drObitService[""].ToString();
									else
										strLastFHID = drObitService[""].ToString();
								}//end of if
								
								//displays the details of the service
								litObituaryServices.Text += "<div class='divObiturayServiceDateTime'>" + 
									"<div class='divObiturayServiceDate'>" + 
										"<label>" + Convert.ToDateTime(drObitService[""].ToString()).ToString("dddd, MMMM d, yyyy") + "</label>" + 
									"</div>" + 
									"<div class='divObiturayServiceTime'>" + 
										"<label><strong>"; 
				
									//checks which Obituary Service Type is this and displays it
									switch(Convert.ToInt32(drObitService[""].ToString()))
									{
										case 0:
											litObituaryServices.Text += "Visitation";
										break;
										case 1:
											litObituaryServices.Text += "Funeral Service";
										break;
										case 2:
											litObituaryServices.Text += "Graveside Service";
										break;
										case 3:
											litObituaryServices.Text += "Memorial Service";
										break;
										case 4:
											litObituaryServices.Text += "Non Commemorative Funeral";
										break;
										case 6:
											litObituaryServices.Text += "Celebration of Life";
										break;
										default:
											litObituaryServices.Text += drObitService[""].ToString();
										break;										
									}//end of switch
										
									//displays the service start time
									litObituaryServices.Text += " - " + Convert.ToDateTime(drObitService[""].ToString()).ToString("h:mm tt");
									
									//checks if there is a service end time
									if(!string.IsNullOrEmpty(drObitService[""].ToString()) && drObitService[""].ToString() != "00:00:00")
										//displays the service end time
										litObituaryServices.Text += " - " + Convert.ToDateTime(drObitService[""].ToString()).ToString("h:mm tt");
									
								//end the details of the service
								litObituaryServices.Text += "</strong></label>" + 
									"</div>" + 
								"</div>";
								
								intIndexServiceID++;
							}//end of if
						}//end of foreach
						
						//closes the last service
						litObituaryServices.Text += "</div>";
					}//end of if
					else
						//remvoes the service section from display	
						panObituaryServices.Visible = false;
				}//end of if
			//}//end of if				
		}//end of try
        catch (Exception ex)
        {
            lblMainError.Text = ex.Message;// + " " + ex.StackTrace;
            lblMainError.Visible = true;
        }//end of catch
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
		
            if (panSidebarLinks.Visible)
                imgPreviewAccordion.Visible = false;
            else
                imgPreviewAccordion.Visible = true;

            txtConName.Attributes.Add("onFocus", "var txtConName = document.getElementById('" + txtConName.ClientID + "'); if ( txtConName.value == 'Your Name *') {txtConName.value = ''}");
            txtConName.Attributes.Add("onblur", "var txtConName = document.getElementById('" + txtConName.ClientID + "'); if ( txtConName.value == '') {txtConName.value = 'Your Name *'}");

            txtConEMail.Attributes.Add("onFocus", "var txtConEMail = document.getElementById('" + txtConEMail.ClientID + "'); if ( txtConEMail.value == 'Your Email Address *') {txtConEMail.value = ''}");
            txtConEMail.Attributes.Add("onblur", "var txtConEMail = document.getElementById('" + txtConEMail.ClientID + "'); if ( txtConEMail.value == '') {txtConEMail.value = 'Your Email Address *'}");

            txtConMessage.Attributes.Add("onFocus", "var txtConMessage = document.getElementById('" + txtConMessage.ClientID + "'); if ( txtConMessage.value == 'Your Message *') {txtConMessage.value = ''}");
            txtConMessage.Attributes.Add("onblur", "var txtConMessage = document.getElementById('" + txtConMessage.ClientID + "'); if ( txtConMessage.value == '') {txtConMessage.value = 'Your Message *'}");

            btnCancel.Attributes.Add("onClick", "javascript:toggleLayer('" + panLeaveCondolence.ClientID + "', '', '');");
        }//end of if
		
		SetPagerIndex();
    }//end of //end of Page_Load()
	
	protected void ibTwitterLogin_Click(object sender, ImageClickEventArgs e)
	{
		try
		{			
			OAuthTokenResponse reqToken = OAuthUtility.GetRequestToken(ConfigurationManager.AppSettings[""],ConfigurationManager.AppSettings[""],Request.Url.AbsoluteUri);

			Response.Redirect(string.Format("http://twitter.com/oauth/authorize?oauth_token={0}", reqToken.Token));
		}//end of try
        catch (Exception ex)
        {
            lblMainError.Text = ex.Message;
            lblMainError.Visible = true;
        }//end of catch
	}//end of ibTwitterLogin_Click()
	
	protected void dlCondolence_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
			Label lblConCreated = (Label)e.Item.FindControl("lblConCreated");//holds the created date
			Label lblSocialNetwork = (Label)e.Item.FindControl("lblSocialNetwork");//holds the social network id
			Label lblUserID = (Label)e.Item.FindControl("lblUserID");//holds the user id
			Label lblConName = (Label)e.Item.FindControl("lblConName");//holds the user name
			Image imgConUser = (Image)e.Item.FindControl("imgConUser");//holds the avatar for this condolence
			
			//sets the current format for the created date hh:mm tt
			lblConCreated.Text = Convert.ToDateTime(lblConCreated.Text).ToString("MMM d, yyyy");
			
			//checks if there is a user name if not and there is a User id then get it from teh database
			if(string.IsNullOrEmpty(lblConName.Text) && !string.IsNullOrEmpty(lblUserID.Text) && Convert.ToInt32(lblUserID.Text) > 0)
			{
				DataTable dtUserDetails = DAL.getRow("", "WHERE  = " + lblUserID.Text);//holds the this user detail
	
				//displays the users name					
				lblConName.Text = dtUserDetails.Rows[0][""].ToString() + " " + dtUserDetails.Rows[0][""].ToString();
			}//end of if
			
			imgConUser.ImageUrl = "/Portals/_default/skins/Obit/Images/DefaultCondolenceIcon.png";
        }//end of if
    }//end of dlCondolence_ItemDataBound()

	protected void cmdSaveCondolonces_Click(object sender, EventArgs e)
    {
		try
		{
            lblConError.Text = string.Empty;
            lblConError.Visible = false;
			
			//checks if the page is valid if so then prcess the event
			if (Page.IsValid)
			{			
				//checks if the Captcha is validated
                if (Captcha.Validate(txtCaptchaCode.Text.Trim().ToUpper()))
                {
                    string strUserEmail = lblConnectedEmail.Text;//holds the users email address
                    string strAddSubject = "condolences";//holds the and the condolences as they could be two ways of sending it public or private
					DataTable dtObitUsers = DAL.getRow("", "WHERE  = " + hfObituaryID.Value);//gets all users that are either creators or co-owners

                    //checks if there is a user email addres if the user has not loged in 
                    //and needs to type it out
                    if (string.IsNullOrEmpty(strUserEmail))
                        strUserEmail = txtConEMail.Text;

                    //checks if the user is log into the site
                    if (Session[""] != null)
                        //adds a new condolence with the user who is logged into the site
                        DAL.addUpdateObituaryCondolence(0, Convert.ToInt32(hfObituaryID.Value), Convert.ToInt32(Session[""].ToString()), 0, "", "", txtConMessage.Text, "", chkPrivateCon.Checked);
                    else
                    {
                        //checks if the user is logged into the site or soical site
                        if (panConNameEmail.Visible == false)
                            //adds a new condolence with the user who is not logged in
                            DAL.addUpdateObituaryCondolence(0, Convert.ToInt32(hfObituaryID.Value), 0, 0, txtConName.Text, strUserEmail, txtConMessage.Text, "", chkPrivateCon.Checked);
                        else
						{
							FacebookApp facebookApp = new FacebookApp();//holds an object of the FB
							
                            //adds a new condolence with the user who is logged into soical site
                            //for intObituaryCondolenceSocialNetwork
							/*
								1 = FB
								2 = Twitter
								3 = Linkin
								4 = Google
							*/
                            DAL.addUpdateObituaryCondolence(0, Convert.ToInt32(hfObituaryID.Value), 0, Convert.ToInt32(hfObituaryCondolenceSocialNetwork.Value), lblConnectedName.Text, strUserEmail, txtConMessage.Text, lblConnectedUser.Text, chkPrivateCon.Checked);
							
							//checks if the user is connected to FB
							if (facebookApp.Session != null && facebookApp.AccessToken != null && chkPrivateCon.Checked == false)
							{
								var fb = new FacebookClient(facebookApp.AccessToken);
								
								//publish the user's public condolence to their FB wall
								dynamic result = fb.Post("me/feed", new { 
								    message = txtConMessage.Text,
									link = "http://" + Request.Url.Host + "/ObituaryDetailShare.aspx?id=" + hfObituaryID.Value
								});
							}//end of if
							//tweets to the user
							else if (Request["oauth_token"] != null && chkPrivateCon.Checked == false)
							{
								var tokens = OAuthUtility.GetAccessToken(
                    				ConfigurationManager.AppSettings[""],
                    				ConfigurationManager.AppSettings[""],
            				        Session["TwitterRequestToken"].ToString(),
			                	    Session["TwitterPin"].ToString());
				 
								OAuthTokens oatAccess = new OAuthTokens()
									{
										AccessToken = tokens.Token,
										AccessTokenSecret = tokens.TokenSecret,
										ConsumerKey = ConfigurationManager.AppSettings[""],
										ConsumerSecret = ConfigurationManager.AppSettings[""]
									};
								
								TwitterResponse<TwitterStatus> tsResponse = TwitterStatus.Update(oatAccess, txtConMessage.Text);
							}//end of else if
						}//end of else
                    }//end of else

                    //checks if this is a Private Condolence for the admin send out
                    if (chkPrivateCon.Checked == true)
                        //changes the subject for the send out to the admin
                        strAddSubject = "a Private Condolence";

					//checks if there is a email 
    	            if (!string.IsNullOrEmpty(hfObituaryCreatorEmail.Value))
		            	//sends out email to creators
						sendCondolencesEmail(hfObituaryCreatorEmail.Value, "You have received " + strAddSubject, "send-condolence-nophoto", chkPrivateCon.Checked, true);
						
					//goes around send it to all user who are any coowners
					foreach (DataRow drObitUsers in dtObitUsers.Rows)
					{
						//checks if there is a email 
    		            if (!string.IsNullOrEmpty(drObitUsers[""].ToString()))
			            	//sends out email to any coowners
							sendCondolencesEmail(drObitUsers[""].ToString(), "You have received " + strAddSubject, "send-condolence-nophoto", chkPrivateCon.Checked, true);
					}//end of for loop
					
                    //checks if this is a Private Condolence for the user send out
                    if (chkPrivateCon.Checked == true)
					{
                        //changes the subject for private condolence
                        strAddSubject = "PRIVATE Condolences";
						
						//changes the thank you message for private condolence
						litThankYou.Text = "Your private condolence will be shared with the family. A copy has also been sent to your email address.";
					}//end of if

					//checks if there is a email to send to as if the user uses twitter it will be not avialable
					if (!string.IsNullOrEmpty(strUserEmail))
                    	//sends out email to admin to send to user
                    	sendCondolencesEmail(strUserEmail, "Your " + strAddSubject + " have been sent", "send-condolence", chkPrivateCon.Checked);

                    //resets the condolences, the number of them for this Obituary and paging
			      					
					//gets the number of condolences
					InitializePagingVars(true);
			
					//resets the condolences
					Bind(-1);
			
					//resets the paging contorls
					BindPagingControls();
					
                    //turns on the thank you message
                    //panCondoloncesFourm.Visible = false;
                    panCondoloncesThankYou.Visible = true;
                    chkPrivateCon.Checked = false;
                    txtCaptchaCode.Text = string.Empty;
                    txtConName.Text = "Your Name *";
                    txtConEMail.Text = "Your Email Address *";
                    txtConMessage.Text = "Your Message *";
                }//end of if
                else
                {
                    lblConError.Text = "Invalid captcha!!";
                    lblConError.Visible = true;
                    panCondoloncesThankYou.Visible = false;
                }//end of else
			}//end of if
		}//end of try
        catch (Exception ex)
        {
            lblConError.Text = ex.Message;
            lblConError.Visible = true;
            panCondoloncesThankYou.Visible = false;
        }//end of catch
    }//end of cmdSaveCondolonces_Click()
		
	protected void cmdSaveReminder_Click(object sender, EventArgs e)
    {
		try
		{
			//displays the Share section
			panReminders.Style.Add("display", "block");
			
			//resets the error message
			lblReminderEmail.Visible = false;
            panReminderThankYou.Visible = false;
            lblReminderError.Visible = false;

			//checks if the page is valid if so then prcess the event
			if (Page.IsValid)
			{
				//throw new Exception(strWhereReminder);
				DataTable dtReminderCheck = DAL.getRow("", "WHERE  = " + hfObituaryID.Value + " AND  = '" + DAL.safeSql(txtReminderEMail.Text) + "'");//holds all of the condolence for this obituary for this email
				
				//checks if this user is already in the database for this Obituaries
				if (dtReminderCheck.Rows.Count == 0)
					//turns on the thank you message
					panReminderThankYou.Visible = true;
				else
					throw new Exception("This email is already going to be reminded.");
			}//end of if
		}//end of try
        catch (Exception ex)
        {
            lblReminderError.Text = ex.Message;
            lblReminderError.Visible = true;
        }//end of catch
    }//end of cmdSaveReminder_Click()
	
    protected void btnShareEmail_Click(object sender, EventArgs e)
    {
		try
		{
			//displays the Share section
			panShare.Style.Add("display", "block");
			
			//resets the error message and the thank you message
			lblShareEmailError.Visible = false;
			panShareEmailThankYou.Visible = false;
			
			//checks if the user had at least used one email
			if (!string.IsNullOrEmpty(txtShareEmail1.Text) || !string.IsNullOrEmpty(txtShareEmail2.Text) || !string.IsNullOrEmpty(txtShareEmail3.Text))
			{
				string strThankYouSubject = "Announcement Details and Services for {0}";//holds the subject link for the email
				
				//checks which if this is a memoral as it uses a different subject line for share a email
				if(panSectionFlowers.Visible == false)
					strThankYouSubject = "A Memorial for {0} has been shared with you";
				
				//send an email that will be shared
				sendObituaryEmail(txtShareEmail1.Text, strThankYouSubject);
				sendObituaryEmail(txtShareEmail2.Text, strThankYouSubject);
				sendObituaryEmail(txtShareEmail3.Text, strThankYouSubject);
				
				//resets the emails textbox
				txtShareEmail1.Text = "";
				txtShareEmail2.Text = "";
				txtShareEmail3.Text = "";
				
				//turns on the thank you message
				//panShareEmailBody.Visible = false;
				panShareEmailThankYou.Visible = true;
			}//end of if
			else
				throw new Exception("At least one email is required");
		}//end of try
        catch (Exception ex)
        {
            lblShareEmailError.Text = ex.Message;
            lblShareEmailError.Visible = true;
        }//end of catch
    }//end of btnShareEmail_Click()

	#region PagingControls

    protected void BindPagingControls()
    {
        if (_LastPageIndex > 0)
        {
            PagerPanel.Visible = true;
			PagerPanel2.Visible = true;
			
            List<PagerLinkData> pagerLinkData = new List<PagerLinkData>();
            float tempIndex = ((float)_HiddenPageIndex / 10) * 10;
            int currentPagerIndex = (int)tempIndex ;

            int lastPagerIndex = currentPagerIndex + _PageSize;
            if (lastPagerIndex > _LastPageIndex) lastPagerIndex = _LastPageIndex;

			string baseUrl = "/Obituaries.aspx?ObituariesID=" + hfObituaryID.Value + "&p=";
            string navigateUrl;
			
			//checks if there is any prev link to use
            if (currentPagerIndex > 0)
            {
				//checks if the currentOager is a least two away from the Pager Index
				//in order for the arrow to work
				if((currentPagerIndex - 1) > 0)
					pagerLinkData.Add(new PagerLinkData("Prev", (baseUrl + (currentPagerIndex - 1).ToString()), (currentPagerIndex - 1), true, "aPrevPage aPagingContorls  aPage"));
				
				//goes around for the last up to the last 5 pages for display
				for(int intPagerIndex = (currentPagerIndex - 5); (currentPagerIndex - intPagerIndex) > 0; intPagerIndex++)
				{
					//checks if to make sure that it is more the zero in order to display it
					if(intPagerIndex >= 0)
						//adds the pager link
						pagerLinkData.Add(new PagerLinkData(((int)((intPagerIndex + 1))).ToString(), (baseUrl + intPagerIndex), intPagerIndex, (currentPagerIndex != _HiddenPageIndex), "aNumberPage aPage"));
				}//end of for loop
            }//end of if
            
			//goes around the next pages up to the 12 pages
			while (currentPagerIndex <= lastPagerIndex)
            {
                string linkText = ((int)(currentPagerIndex + 1)).ToString();
				string strBasicCSSClass = "aNumberPage aPage";//holds the css class that will be used in all items
				
				//checks if this is the last number link if so then add a aNumberPageLastChild
				if(currentPagerIndex == lastPagerIndex)
					strBasicCSSClass += " aNumberPageLastChild";
				
                if (currentPagerIndex != _HiddenPageIndex)
                {
                    navigateUrl = baseUrl + currentPagerIndex.ToString();
					
					//adds the pager link
                    pagerLinkData.Add(new PagerLinkData(linkText, navigateUrl, currentPagerIndex, (currentPagerIndex != _HiddenPageIndex), strBasicCSSClass));
                }//end of if
                else
                {
                    navigateUrl = "javascript:void(0);";
					
					//adds the pager link
                    pagerLinkData.Add(new PagerLinkData(linkText, navigateUrl, currentPagerIndex, (currentPagerIndex != _HiddenPageIndex), "aCurrentPage " + strBasicCSSClass));
                }//end of else
				
                currentPagerIndex++;
            }//end of while loop
			
            if (lastPagerIndex < _LastPageIndex)
            {
                navigateUrl = baseUrl + (lastPagerIndex + 1).ToString();
				
				//adds the pager link
                pagerLinkData.Add(new PagerLinkData("Next", navigateUrl,lastPagerIndex + 1, true, "aNextPage aPagingContorls aPage"));
            }//end of if			
			
            PagerControls2.DataSource = pagerLinkData;
            PagerControls2.DataBind();
        }//end of if
        else
		{
            PagerPanel2.Visible = false;
		}//end of else
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
		
        public PagerLinkData(string text, string navigateUrl, int pageIndex, bool enabled)
        {
            _Text = text;
            _NavigateUrl = navigateUrl;
            _PageIndex = pageIndex;
            _Enabled = enabled;
        }//end of PagerLinkData()

        public PagerLinkData(string text, string navigateUrl, int pageIndex, bool enabled, string tagClass)
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
			
            if (_HiddenPageIndex < 0) 
				_HiddenPageIndex = 0;

            if (_HiddenPageIndex > _LastPageIndex) 
				_HiddenPageIndex = _LastPageIndex;
				
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
        
		if (_HiddenPageIndex < 0)
			_HiddenPageIndex = 0;
			
        if (_HiddenPageIndex > _LastPageIndex) 
			_HiddenPageIndex = _LastPageIndex;
			
        HiddenPageIndex.Value = _HiddenPageIndex.ToString();
    }//end of SetPagerIndex()
	
    #endregion
}//end of User Contorl