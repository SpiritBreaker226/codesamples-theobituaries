// Header for the flower sections

public partial class FlowerHeader : System.Web.UI.UserControl
{	
	protected void Page_PreRender(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			try
			{
				//resets the litContentCartError
				litContentCartError.Text = "";
				
				string strCurrentCate = "fa";//holds the current category that is being search for
				string[] arrFlowerCate = new string[]{"fa","fb","fs","p","fl","fw","fh","fx","fc"};//holds the categorties for the flowers that are on sale
				
				//checks if all of the items needs to be in the URL in order for this to work otherwise redirect the suer
				//to the homepage as they are chaging the URL
				if (Request.QueryString["oid"] != null)
				{
					string strObituaryID = DAL.safeSql(Request.QueryString["oid"]);//holds the id of the Obituary
					DataTable dtObitDetails = DAL.getRow("", "Where  = " + strObituaryID);//holds the details of this obit
					
					//checks if there is a obituary 
					if (dtObitDetails != null && dtObitDetails.Rows.Count > 0)
					{
						string strBirthDateAndPassingDate = "";///holds the birth and passing date
						DataTable dtObitImage = DAL.getRow("", "WHERE  = " + strObituaryID + " Order by ");//holds the images for this Obitay
						
						//sets the Obituaries ID for the another address lightbox
						raaFlowers.setObituatyID = Convert.ToInt32(strObituaryID);
						
						//sets the link to the cart
						lnkMyCart.NavigateUrl = "/Obituaries/flower/ordering.aspx?person=" + Request.QueryString["person"] + "&FHPID=" + Request.QueryString["FHPID"] + "&oid=" + strObituaryID;
						
						//sets the name and URL of the person for this obituary
						hlFullName.Text = dtObitDetails.Rows[0][""].ToString() + " " + dtObitDetails.Rows[0][""].ToString();
						hlFullName.NavigateUrl = "/Obituaries.aspx?ObituariesID=" + strObituaryID;
																	
						//checks if there is a birthdate
						if (!string.IsNullOrEmpty(dtObitDetails.Rows[0][""].ToString()))
							strBirthDateAndPassingDate = string.Format("{0:MMMM dd, yyyy}", Convert.ToDateTime(dtObitDetails.Rows[0][""].ToString()));
						
						//checks if there is a passing date and if so then check if a - is needed
						if (!string.IsNullOrEmpty(dtObitDetails.Rows[0][""].ToString()))
							strBirthDateAndPassingDate += (!string.IsNullOrEmpty(strBirthDateAndPassingDate) ? " - " + string.Format("{0:MMMM dd, yyyy}", Convert.ToDateTime(dtObitDetails.Rows[0][""].ToString())) : string.Format("{0:MMMM dd, yyyy}", Convert.ToDateTime(dtObitDetails.Rows[0][""].ToString())));
						
						//sets strBirthDateAndPassingDate into the lblBirthDateAndPassingDate 
						lblBirthDateAndPassingDate.Text = strBirthDateAndPassingDate;
							
						//checks if thre is a image
						if (dtObitImage.Rows.Count > 0)
						{
							//checks if there is a image for this obitayr
							if (File.Exists(Server.MapPath("~\\images\\User\\" + strObituaryID + "\\" + dtObitImage.Rows[0][""].ToString())))
							{
								//checks if there is a thumbnail for the image
								if (File.Exists(Server.MapPath("~/Images/User/" + strObituaryID + "/" + dtObitImage.Rows[0][""].ToString().Replace(".","_upload_thumbnail."))))
									//uses the image thumbnial
									hlObituaryImage.ImageUrl = "/Images/User/" + strObituaryID + "/" + dtObitImage.Rows[0][""].ToString().Replace(".","_upload_thumbnail.");
								else
									//uses the image as there is currenly no thumbnial for this image
									hlObituaryImage.ImageUrl = "/Images/User/" + strObituaryID + "/" + dtObitImage.Rows[0][""].ToString();
								
								hlObituaryImage.NavigateUrl = "/Obituaries.aspx?ObituariesID=" + strObituaryID;
								hlObituaryImage.Visible = true;
							}//end of if
						}//end of if
						
						//checks if this is in the flower order area or flower shoping area
						if(Request.Url.ToString().IndexOf("TabID=159") == -1)
						{
							//checks if there is a which category to use
							if (Request.QueryString["cate"] != null)
								strCurrentCate = Request.QueryString["cate"];
									
							//goes around for each flower category and adds the URL to it
							for(int intIndex = 0;intIndex < arrFlowerCate.Length;intIndex++)
							{				
								HyperLink hlHeaderCategory = (HyperLink)panHeaderFlowersRightContent.FindControl("hlHeaderCategory" + arrFlowerCate[intIndex].ToUpper());//holds the selected category that the user has selected
						
								//checks if there is a hlHeaderCategory
								if(hlHeaderCategory != null)	
								{
									//checks if it is the current category and changes the class for it
									if(strCurrentCate == arrFlowerCate[intIndex])
									{
										//sets the which category is selected in the header
										hlHeaderCategory.CssClass = "lblSelectedFlowerCateogry";
									}//end fo if
									
									//sets the URL
									hlHeaderCategory.NavigateUrl = "/Obituaries/flower.aspx?person=" + Request.QueryString["person"] + "&FHPID=" + Request.QueryString["FHPID"] + "&oid=" + strObituaryID + "&cate=" + arrFlowerCate[intIndex];
								}//end of if
							}//end of for loop
						}//end of if
						else
						{
							DataTable dtPersonRecipient = DAL.getRow("", "Where  = " + strObituaryID + " AND  = 1");//holds the Recipients for this FH
							
							//sets the on Clicks for flower FH and Another Address
							rdoFlowersFH.Attributes.Add("onClick", "javascript:clearAllOtherRadioChecks(getDocID('divObiturayFlowersRight'), getDocID('" + rdoFlowersFH.ClientID + "'), 'input');");
							rdoFlowersAnotherPerson.Attributes.Add("onClick", "javascript:clearAllOtherRadioChecks(getDocID('divObiturayFlowersRight'), getDocID('" + rdoFlowersAnotherPerson.ClientID + "'), 'input');toggleLayer('divAnotherPerson', 'divGrayOrderBG', '');");
														
							//sets the flower FH sidebar
							panFlowersFH.Visible = Convert.ToBoolean(dtObitDetails.Rows[0][""].ToString());
							
							//checks if there is a FHID
							if(Convert.ToInt32(dtObitDetails.Rows[0][""].ToString()) == 0)
								//removes the Flowers FH is there is no FHID to use
								panFlowersFH.Visible = false;
							
							//turns on the Send To Section for the cart/ area
							panHeaderFlowersRightContent.Visible = false;
							panHeaderFlowersRightContentCartHeader.Visible = true;
																		
							//checks if the user wants to send the flowers a person or FH
							if(Request.QueryString["person"] == "0")
							{
								DataTable dtFHDetails = DAL.getRow("", "Where  = " + DAL.safeSql(Request.QueryString["FHPID"]));//holds the details of the FH
								
								//checks if there is any data found
								if (dtFHDetails != null && dtFHDetails.Rows.Count > 0)
								{
									//displays who will get the order in the header
									litCartHeader.Text = dtFHDetails.Rows[0][""].ToString() + "<br/>" + 
										dtFHDetails.Rows[0][""].ToString() + "<br/>";
									
									//checks if there is a FH Address 2 to display as well
									if(!string.IsNullOrEmpty(dtFHDetails.Rows[0][""].ToString()))
										litCartHeader.Text += dtFHDetails.Rows[0][""].ToString() + "<br/>";
									
									//displays the rest of who will get the order
									litCartHeader.Text += dtFHDetails.Rows[0][""].ToString() + ", " + 
										dtFHDetails.Rows[0][""].ToString() + "<br/>" + 
										dtFHDetails.Rows[0][""].ToString() + "<br/>" + 
										dtFHDetails.Rows[0][""].ToString();

									//turns on rdoFlowersFH as FH has been selected
									rdoFlowersFH.Checked = true;
								}//end of if
								else
									//sends the user to the homepage if there is no FH found
									Response.Redirect("/Home.aspx", true);
							}//end of if
							else if(Request.QueryString["person"] == "1")
							{
								DataTable dtPersonDetails = DAL.getRow("", "Where Id = " + DAL.safeSql(Request.QueryString["FHPID"]));//holds the details of the FH
								
								//checks if there is any data found
								if (dtPersonDetails != null && dtPersonDetails.Rows.Count > 0)
								{
									//displays who will get the order in the header
									litCartHeader.Text = dtPersonDetails.Rows[0][""].ToString() + " " + dtPersonDetails.Rows[0][""].ToString() + "<br/>";
									
									//displays the Full Address for the Person
									//if they where created by the user in the cart
									if(dtPersonDetails.Rows[0][""].ToString() == "False")
									{
										//sets the main address
										litCartHeader.Text += dtPersonDetails.Rows[0][""].ToString() + "<br/>";
										
										//checks if there is a FH Address 2 to display as well
										if(!string.IsNullOrEmpty(dtPersonDetails.Rows[0][""].ToString()))
											litCartHeader.Text += dtPersonDetails.Rows[0][""].ToString() + "<br/>";
										
										//sets the reset of the address
										litCartHeader.Text += dtPersonDetails.Rows[0][""].ToString() + ", " + 
										dtPersonDetails.Rows[0][""].ToString() + "<br/> " + 
										dtPersonDetails.Rows[0][""].ToString() + "<br/>" + 
										dtPersonDetails.Rows[0][""].ToString();
										
										//turns on rdoFlowersAnotherPerson as person has been selected
										rdoFlowersAnotherPerson.Checked = true;
									}//end of if
									else
										//sets the order for the person who is in the database
										litCartHeader.Text += dtPersonDetails.Rows[0][""].ToString() + ", " + dtPersonDetails.Rows[0][""].ToString();
								}//end of if
								else
									//sends the user to the homepage if there is no Person found
									Response.Redirect("/Home.aspx", true);
							}//end of else if
							else
								//changes the Header if there is no Recipent
								lblDeliveryTo.Text = "Select a Recipent:";
						
							//checks if there is a more Recipient to use 
							if (dtPersonRecipient != null && dtPersonRecipient.Rows.Count > 0)
							{
								dlRecipient.DataSource = dtPersonRecipient;
								dlRecipient.DataBind();
							}//end of if
							else
								//removes the Flowers Recipient as it is in current use
								panFlowersRecipient.Visible = false;
						}//end of else
					}//end of if
					else
						//sends the user to the homepage if there is no Obituaty found
						Response.Redirect("/Home.aspx", true);
				}//end of if
				else
					//sends the user to the homepage if there is no ids or where the flower will be going was change
					Response.Redirect("/Home.aspx", true);
			}//end of try
			catch (Exception ex)
			{
				litContentCartError.Text = "<div class='divError'>" + ex.Message + "</div>";
			}//end of catch
		}//end of if
    }//end of Page_PreRender()
	
	protected void rdoFlowersFH_CheckedChanged(object sender, EventArgs e)
    {
		string strObituaryID = DAL.safeSql(Request.QueryString["oid"]);//holds the id of the Obituary
		DataTable dtObitDetails = DAL.getRow("", "Where  = " + strObituaryID);//holds the details of this obit
		
		//checks if there is an obituary
		if (dtObitDetails != null && dtObitDetails.Rows.Count > 0)
			//reloads the page to load the FH
			Response.Redirect("/Obituaries/flower/ordering.aspx?person=0&FHPID=" + dtObitDetails.Rows[0][""].ToString() + "&oid=" + strObituaryID);
    }//end of rdoFlowersFH_CheckedChanged()
	
	protected void rdoFlowersPerson_CheckedChanged(object sender, EventArgs e)
    {
		string[] arrFlowersPerson = ((RadioButton)sender).CssClass.Split(' ');//holds the id and the class as there is no commend property for a RadioButton
		
		//reloads the page to load the Person
		Response.Redirect("/Obituaries/flower/ordering.aspx?person=1&FHPID=" + arrFlowersPerson[0] + "&oid=" + Request.QueryString["oid"]);
    }//end of rdoFlowersPerson_CheckedChanged()
	
	protected void dlRecipient_ItemDataBound(object sender, DataListItemEventArgs e)
    {
		try
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				//checks if all of the items needs to be in the URL in order for
				//this to work otherwise redirect the user to the homepage as they are chaging the URL
				if (Request.QueryString["FHPID"] != null)
				{
					RadioButton rdoFlowersPerson = (RadioButton)e.Item.FindControl("rdoFlowersPerson");//holds the radio button being used
					string[] arrFlowersPerson = rdoFlowersPerson.CssClass.Split(' ');//holds the id and the class as there is no commend property for a RadioButton
					
					//sets the on Clicks for flower Person
					rdoFlowersPerson.Attributes.Add("onClick", "javascript:clearAllOtherRadioChecks(getDocID('divObiturayFlowersRight'), getDocID('" + rdoFlowersPerson.ClientID + "'), 'input');");
					
					//checks if this the item that has been selected
					if(arrFlowersPerson[0] == Request.QueryString["FHPID"].ToString())
						//checks the radiobutton that is current being used
						rdoFlowersPerson.Checked = true;
				}//end of if
				else
					//sends the user to the homepage if there is no FH found
					Response.Redirect("/Home.aspx", true);
			}//end of if
		}//end of try
        catch (Exception ex)
        {
            litContentCartError.Text = "<div class='divError'>" + ex.Message + "</div>";;
        }//end of catch
    }//end of dlRecipient_ItemDataBound()
}//end of User Contorl