// Displays the search result s to the user

public partial class SearchResultsForObituaries : PortalModuleBase
{
	//creates the URL for searching
	private string createSearchURL(string strSearch, string strSelectCity, string strSelectProv, DropDownList ddlSortResult = null)
	{
		string strProvinces = "";//holds the Provinces selected
		string strCity = "";//holds the city text
		string strCurrentTab = "";//holds the tab to use
		string strSort = "";//holds the sorting
		
		//checks if there is a search text to use 
		//as only a keyword or a fh can be use
		if (!string.IsNullOrEmpty(strSearch))
			//sets the search text for the next screen
			strSearch = "?q=" + strSearch;
		//checks if this is a FH service meaning only display for one FH and all of its Obituary and Memorial
		else if(!string.IsNullOrEmpty(Request.QueryString["fh"]))
			//sets the FH id for the next screen
			strSearch = "?fh=" + Request.QueryString["fh"];

		//checks if there is a provinces selected to use 
		if (!string.IsNullOrEmpty(strSelectProv))
		{
			//checks if there is a text in q if so then add in & instead of ? for the start of the URL query
			if (!string.IsNullOrEmpty(strSearch))
				strProvinces = "&";
			else
				strProvinces = "?";			
			
			//sets the search text for the next screen
			strProvinces += "p=" + Server.UrlEncode(strSelectProv);
		}//end of if
		
		//checks if there is a city text to use 
		if (!string.IsNullOrEmpty(strSelectCity))
		{
			//checks if there is a text in q if so then add in & instead of ? for the start of the URL query
			if (!string.IsNullOrEmpty(strSearch) || !string.IsNullOrEmpty(strProvinces))
				strCity = "&";
			else
				strCity = "?";			
			
			//sets the search text for the next screen
			strCity += "c=" + Server.UrlEncode(strSelectCity);
		}//end of if
		
		//checks if there is a ddlSortResult
		if(ddlSortResult != null)
		{
			//checks if there is a text in q if so then add in & instead of ? for the start of the URL query
			if (!string.IsNullOrEmpty(strSearch) || !string.IsNullOrEmpty(strProvinces) || !string.IsNullOrEmpty(strCity))
				strSort = "&";
			else
				strSort = "?";
			
			//sets the search text for the next screen
			strSort += "s=" + Server.UrlEncode(ddlSortResult.Text);
			
			//checks which tab to use
			if(ddlSortResult.ID.IndexOf("Obit") > 0)
				//sets strCurrentTab to be Obituaries
				strCurrentTab = "&t=0";
			else if(ddlSortResult.ID.IndexOf("FH") > 0)
				//sets strCurrentTab to be FH
				strCurrentTab = "&t=1";
			else
				//sets strCurrentTab to be Memorial
				strCurrentTab = "&t=2";
		}//end of if
		
		//reloads the page with a new keyword search
		return "/SearchResult.aspx" + strSearch + strProvinces + strCity + strSort + strCurrentTab;
	}//end of createSearchURL()
	
	protected void Page_PreRender(object sender, EventArgs e)
    {
		try
		{
			int intSectionTotal = 0;//holds the number of Total for this section found for Search
			int intGrandTotal = 0;//holds the number of items found for Search
			int intGothicIDIndex = 4;//holds which GothicID is currently is avaiable on the page
			int intPerSection = 20;//holds the the number of section alloed for this seciton
			int intObituaryType = 0;//holds which type of Obituary Type to use Obituary or Memorial 
			string strSearch = "";//holds the text search
			string[] arrTableToSearch = new string[]{"", "", ""};//holds the tables that will be search for
			string[] arrSection = new string[]{"Obit", "FH", "Mem"};//holds the UnqiueID for each section in order to find contorls
			string[] arrFilters = new string[]{"City", "Province"};//holds the fliters as they display teh same
			List<CitySearchItem> listCitySearchItems = new List<CitySearchItem>();//holds the list of the city search items
			List<CitySearchItem> listProvSearchItems = new List<CitySearchItem>();//holds the list of the province search items
							
			//checks if there is a FH id to use
			if (!string.IsNullOrEmpty(Request.QueryString["q"]))
				//sets the search text
				strSearch = DAL.safeSql(Server.UrlDecode(Request.QueryString["q"]));
						
			//sets the keyword of what the user had search for
			txtKeywords.Text = DAL.safeSql(strSearch);
			
			//goes around for each table that is being search for and displays the results
			for(int intIndex = 0;intIndex < arrTableToSearch.Length;intIndex++)
			{
				string strSortSearchField = " DESC, ";//holds what fields to sort
				string strUnqieSearchField = "";//holds what field makes this row unqiue
				string strWhere = "";//holds the where clouses
				string strMiddleName = "";//holds the Middle Name for Obituary or Memorial
				string strLastName = "";//holds the Last Name for Obituary or Memorial
				string strFilter = "";//holds the the filters
				string strDisplayBanner = "getDocID('" + panFHBanner.ClientID + "').style.display = '';";//holds the the JS statment to display the banner or not
				int intSearchItemIndex = 1;//holds where which row the foreach is one
				Label lblTabName = (Label)panSearchResultsTabs.FindControl("lblTabName" + arrSection[intIndex]);//holds the names of the tab
				Literal litResultsFound = (Literal)panSearchResultPageBody.FindControl("litResultsFound" + arrSection[intIndex]);//holds the display results contorl
				Literal litSearchBody = (Literal)panSearchResultPageBody.FindControl("litSearchBody" + arrSection[intIndex]);//holds the display to search the body
				DropDownList ddlSortResult = (DropDownList)panSearchResultPageBody.FindControl("ddlSortResult" + arrSection[intIndex]);//holds the sort results contorl
				Panel panSearchResultsTab = (Panel)panSearchResultsTabs.FindControl("panSearchResultsTab" + arrSection[intIndex]);//holds the search results tab
				Panel panSearchResultsTabBody = (Panel)panSearchResultsTabs.FindControl("panSearchResultsTabBody" + arrSection[intIndex]);//holds the search results tab
				Panel panSortResult = (Panel)panSearchResultPageBody.FindControl("panSortResult" + arrSection[intIndex]);//holds the sort results container
				
				//checks if t is either 1 for FH or 2 for Memorials else just display the Obituraies tab
				if(!string.IsNullOrEmpty(Request.QueryString["t"]) && Request.QueryString["t"] == "1")
				{
					//displays the FH tab
					panSearchResultsTabBodyFH.Style.Add("display", "block");
					panSearchResultsTabFH.CssClass += " divSearchResultPageHeaderTabLeftSelection";
					
					//displays a banner the FH tab
					panFHBanner.Attributes.Add("style", "display: block;");
				}//end of if
				else if(!string.IsNullOrEmpty(Request.QueryString["t"]) && Request.QueryString["t"] == "2")
				{
					//displays the Memorial tab
					panSearchResultsTabBodyMem.Style.Add("display", "block");
					panSearchResultsTabMem.CssClass += " divSearchResultPageHeaderTabLeftSelection";
				}//end of else if
				else
				{
					//displays the Obituraies tab and default tab
					panSearchResultsTabBodyObit.Style.Add("display", "block");
					panSearchResultsTabObit.CssClass += " divSearchResultPageHeaderTabLeftSelection";
				}//end of else
				
				//checks if this is a FH service meaning only display for one FH and all of its Obituary and Memorial
				if(!string.IsNullOrEmpty(Request.QueryString["fh"]))
				{
					//checks if this is the FH or Obituary or Memorial
					if(intIndex == 1)
					{
						//does the search for for this FH 
						strWhere = "{0}";
						
						//turns off the sorting as it si not need for this FH
						panSortResult.Visible = false;
						
						//displays a banner when it is in FH tab
						strDisplayBanner = "getDocID('" + panFHBanner.ClientID + "').style.display = 'block';";
					}//end of if
					else if(intIndex == 0 || intIndex == 2)
						//does the search for for all items that start with strSearch
						strWhere = "{0} = 'Published' AND  = " + intObituaryType + "{1}";
				}//end of if
				else
				{
					//checks if this is the FH or Obituary or Memorial
					if(intIndex == 1)
					{
						//all items that start with strSearch does search for either inactive FH or Publish FH
						strWhere = "{0} = 1{1} OR {0} = -1{1} ";
						
						//displays a banner when it is in FH tab
						strDisplayBanner = "getDocID('" + panFHBanner.ClientID + "').style.display = 'block';";
					}//end of if
					else if(intIndex == 0 || intIndex == 2)
						//does the search for for all items that start with strSearch
						strWhere = "{0} = 'Published' AND  = " + intObituaryType + "{1} OR {2} = 'Published' AND  = " + intObituaryType + "{1} OR {3} = 'Published' AND  = " +  intObituaryType + "{1}";
				}//end of if
				
				//adds the client click to each of the tabs
				panSearchResultsTab.Attributes.Add("onclick", "classToggleLayer(getDocID('" + panSearchResultPageBody.ClientID + "'),getDocID('" + panSearchResultsTabBody.ClientID + "'),'divSearchResultsTab divJustHidden','div');classToggleLayerChangeClass(getDocID('divSearchResultPageTab'),getDocID('" + panSearchResultsTab.ClientID + "'),'customLeft divSearchResultPageHeaderTabLeft divSearchResultPageHeaderTabLeftSelection','customLeft divSearchResultPageHeaderTabLeft','div');" + strDisplayBanner);
								
				//checks if there is something to use in strWhere
				if(!string.IsNullOrEmpty(strSearch) && string.IsNullOrEmpty(Request.QueryString["fh"]))
				{
					string strSearchKeyword = txtKeywords.Text.Replace("'", "''");//holds the keyword to search for
					
					//checks if this is the FH or Obituary or Memorial
					if(intIndex == 1)
						//uses the keyword for the FH Name
						strSearch = " LIKE '%" + strSearchKeyword + "%' AND ";
					else if(intIndex == 0 || intIndex == 2)
					{
						//uses the keyword for either First Name, Middle Name and Last Name
						strSearch = " LIKE '%" + strSearchKeyword + "%' AND ";
						strMiddleName = " LIKE '%" + strSearchKeyword + "%' AND ";
						strLastName = " LIKE '%" + strSearchKeyword + "%' AND ";
					}//end of else if
				}//end of if
				else if(!string.IsNullOrEmpty(Request.QueryString["fh"]))
				{
					//searches only for the id of this FH
					strSearch = " = " + DAL.safeSql(Server.UrlDecode(Request.QueryString["fh"])) + " AND ";
					
					//checks if this is the FH
					if(intIndex == 1)
						//removes the AND for the FH as it is not need
						strSearch = strSearch.Substring(0, (strSearch.Length - 4));
				}//end of else if
				
				//checks if there is a province that nees to be search for
				if (!string.IsNullOrEmpty(Request.QueryString["p"]))
					strFilter += " AND  = '" + DAL.safeSql(Server.UrlDecode(Request.QueryString["p"])) + "'";
				
				//checks if there is a city that needs to be search for
				if (!string.IsNullOrEmpty(Request.QueryString["c"]))
				{
					//checks if this is the FH or Obituary or Memorial and uses the name of the city field for it
					if(intIndex == 1)
						strFilter += " AND  LIKE '%" + DAL.safeSql(Server.UrlDecode(Request.QueryString["c"])) + "%'";
					else if(intIndex == 0 || intIndex == 2)
						strFilter += " AND  LIKE '%" + DAL.safeSql(Server.UrlDecode(Request.QueryString["c"])) + "%'";
				}//end of if

				DataTable dtPageCount = DAL.countRows(arrTableToSearch[intIndex],string.Format(strWhere,strSearch,strFilter,strMiddleName,strLastName));//holds the total number of items found
				
				//gets the number of items for this sectoin
				intGrandTotal += Convert.ToInt32(dtPageCount.Rows[0][""].ToString());
				
				//updates the total for this sectoin
				intSectionTotal = Convert.ToInt32(dtPageCount.Rows[0][""].ToString());
				
				//checks if there is a new sort option to use
				if (!string.IsNullOrEmpty(Request.QueryString["s"]))
				{					
					//checks if this is the FH or Obituary or Memorial
					if(intIndex == 1)
					{							
						//checks in order to know which tab to sort it uses the URL valuable t
						//if empty then sort just FH
						if (!string.IsNullOrEmpty(Request.QueryString["t"]) && Request.QueryString["t"] == "1")
						{
							//sets the sort dropdown for this tab
							ddlSortResult.Items.FindByValue(Request.QueryString["s"]).Selected = true;
							
							//check which sort to use
							switch(Server.UrlEncode(Request.QueryString["s"]))
							{
								case "0":
									//sets the sorting to what the user
									strSortSearchField = " DESC, ";
								break;
								case "1":
									//sets the sorting to what the user
									strSortSearchField = " DESC,  DESC";
								break;
								case "2":
									//sets the sorting to what the user
									strSortSearchField = " DESC";
								break;
								case "3":
									//sets the sorting to what the user
									strSortSearchField = "";
								break;
							}//end of switch
						}//end of if
					}//end of if
					else if(intIndex == 0 || intIndex == 2)
					{
						//checks if there is any activate order to know which tab 
						//to sort it uses the URL valuable t
						if (!string.IsNullOrEmpty(Request.QueryString["t"]))
						{
							//checks in order to know which tab to sort it uses the URL valuable t
							if (Request.QueryString["t"] == "0" && intIndex == 0 || Request.QueryString["t"] == "2" && intIndex == 2)
							{
								//sets the sort dropdown for this tab for Memorial
								ddlSortResult.Items.FindByValue(Request.QueryString["s"]).Selected = true;
										
								//check which sort to use
								switch(Server.UrlEncode(Request.QueryString["s"]))
								{
									case "0":
										//sets the sorting to what the user
										strSortSearchField = ", ";
									break;
									case "1":
										//sets the sorting to what the user
										strSortSearchField = " DESC,  DESC";
									break;
									case "2":
										//sets the sorting to what the user
										strSortSearchField = "";
									break;
									case "3":
										//sets the sorting to what the user
										strSortSearchField = " DESC";
									break;
									case "4":
										//sets the sorting to what the user
										strSortSearchField = " DESC, , ";
									break;
									case "5":
										//sets the sorting to what the user
										strSortSearchField = ", , ";
									break;
								}//end of switch
							}//end of if
						}//end of if
					}//end of else if
				}//end of if
				
				//checks if this is a Obituary or Memorial
				if(intIndex == 0 || intIndex == 2)
				{
					//checks if the sort or tab is not being used or if the tab is not the active tab
					if(strSortSearchField == " DESC, ")
						//changes the search field for sorting
						strSortSearchField = ", ";
				
					//since both the Obituary and Memorial uses the same strUnqieSearchField it is speortate
					//form the sort which need to only be used when sort\tab is not being used or it is not the 
					//active tab changes the search field for unique row as the unqiue will always change
					strUnqieSearchField = "";
				}//end of if
				
				//checks if there is no search text if so then removes the AND
				if (!string.IsNullOrEmpty(strWhere))
					//adds the Where at the front of the strWhere for getRow to use it
					strWhere = "Where " + strWhere;

				//gets the search results for this table
				DataTable dtSearchResults = DAL.getRow(arrTableToSearch[intIndex], string.Format(strWhere, strSearch, strFilter, strMiddleName, strLastName) + " Order by " + strSortSearchField);
				
				//checks if there is any data to display
				if(dtSearchResults.Rows.Count > 0)
				{
					int intIndexRowID = 0;//holds the row id
											
					//starts the part of the results
					litSearchBody.Text = "<div>";
											
					//goes around for each search results found and formats it for display to the user
					foreach(DataRow drSearchResults in dtSearchResults.Rows)
					{
						string strSearchItemName = "";//holds the Name of the search item
						string strSearchItemPlanName = "";//holds the plan name
						string strSearchItemCity = "";//holds the name of the city
						string strSearchItemMap = "";//holds the map of the search itme
						string strSearchItemFooter = "";//holds the footer of the search results item
						string strSearchItemPhone = "&nbsp;";//holds the phone number of the search item
						string strSearchLeftResultItem = "";//holds the left section of the search results item
						int intSearchItemID = 0;//holds the id of the search item
                        string divStyle = string.Empty;
											
						//checks if this is the FH or a Obituary
						if(intIndex == 1)
						{
							//sets the FH items for this search results
							strSearchItemName = "{0}" + drSearchResults[""].ToString() + "{1}";
							strSearchItemPlanName = drSearchResults[""].ToString().Trim().Replace("'","&lsquo;").Replace("\"","&quot;");
							strSearchItemCity = drSearchResults[""].ToString().Trim();
							intSearchItemID = Convert.ToInt32(drSearchResults[""].ToString());
							strSearchItemPhone = "<a href='tel:" + drSearchResults[""].ToString() + "'>" + drSearchResults[""].ToString() + "</a>";

							//checks if this is a FH has been publish if so then go to the details page
							if(drSearchResults[""].ToString() == "1")
							{
								//sets the displays for Publish FH
								strSearchItemFooter = drSearchResults[""].ToString().Trim();
								strSearchItemName = string.Format(strSearchItemName,"<a href='/FuneralHome.aspx?=" + intSearchItemID + "'><strong>","</strong></a>");
							}//end of if
							else
								//sets the displays for non-Publish FH
								strSearchItemName = string.Format(strSearchItemName,"<label class='lblSearchResultPageName'><strong>","</strong></label>");
							
							//checks if there is a search item footer if so then add in a break before the address
							if(!string.IsNullOrEmpty(strSearchItemFooter))
								strSearchItemFooter += "<br/>";
							
							//adds the basic for the FH footer 
							strSearchItemFooter += drSearchResults[""].ToString() + 
							"<br/>" + 
							drSearchResults[""].ToString() + ", " + drSearchResults[""].ToString() + 
							"<br/>" + 
							drSearchResults[""].ToString();
							
							//checks if there is a logo for this FH
							if(!string.IsNullOrEmpty(drSearchResults[""].ToString()))
							{
								//begin the logo image
								strSearchLeftResultItem = "<a href='/FuneralHome.aspx?=" + intSearchItemID + "'><img alt='" + drSearchResults[""].ToString() + "' src='/Images/FH/" + drSearchResults[""].ToString() + "/";
							
								//checks if there is a thumbnail for the logo
								if (File.Exists(Server.MapPath("~/Images/FH/" + drSearchResults[""].ToString() + "/" + drSearchResults[""].ToString().Replace(".","_upload_thumbnail."))))
									//uses the logo thumbnial
									strSearchLeftResultItem += drSearchResults[""].ToString().Replace(".","_upload_thumbnail.");
								else
									//uses the logo as there is currenly no thumbnial for this logo
									strSearchLeftResultItem += drSearchResults[""].ToString();
							
								//ends the logo image
								strSearchLeftResultItem += "' /></a>";
							}//end of if
								
							//checks if there is a address to search for the google map
							if(!string.IsNullOrEmpty(drSearchResults[""].ToString()) && !string.IsNullOrEmpty(drSearchResults[""].ToString()) && !string.IsNullOrEmpty(drSearchResults[""].ToString()))
							{
								//checks if there is a already a location that the user wants to use
								if(drSearchResults[""] == null || string.IsNullOrEmpty(drSearchResults[""].ToString().Trim()))					
									//adds the funcation that will activate the google map hidden
									strSearchItemMap = "getLocationHiddenGeo(&quot;" + drSearchResults[""] + "," + drSearchResults[""] + "," + drSearchResults[""] + "&quot;,&quot;" + strSearchItemPlanName + "&quot;,43.64100156269233,-79.38599562435303);";
								else
									//adds funcation that will activate the google map hidden what the user want to display
									strSearchItemMap = "getLocationHiddenGeo(&quot;" + drSearchResults[""] + "," + drSearchResults[""] + "," + drSearchResults[""] + "&quot;,&quot;" + strSearchItemPlanName + "&quot;," + drSearchResults[""] + ");";
							}//end of if
						}//end of if
						else if(intIndex == 0 || intIndex == 2)
						{
                            strSearchItemPhone = string.Empty;
                            divStyle = "style='width:560px;' ";
							string strObituaryText = General.stripHtml(Server.HtmlDecode(drSearchResults[""].ToString().Trim().Replace("'","&lsquo;").Replace("\"","&quot;")));//holds the obituary text
							string[] arrObituaryText = strObituaryText.Split(' ');//holds all of the words form strSearchItemFooter 
							
							//checks if the arrObituaryText is larger then 25 words
							if(arrObituaryText.Length > 25)
							{
								//resets the strSearchItemFooter for the first word that is in arrObituaryText
								strObituaryText = arrObituaryText[0];
				
								//goes around for the each other word that is in arrObituaryText for displays 
								for(int intCaptionIndex = 1; intCaptionIndex < 25; intCaptionIndex++)
								{
									strObituaryText += " " + arrObituaryText[intCaptionIndex];
								}//end of for loop
	
								//ends the strCaption with dots to tell that there is more items to dispaly							
								strObituaryText += "...";
							}//end of if
							
							intSearchItemID = Convert.ToInt32(drSearchResults[""].ToString());
							DataTable dtSearchItemImage = DAL.getColData(" Where  = " + intSearchItemID + " Order by ", "");//gets the main image for the obituary
							
							//sets the Obituaries items for this search results
							strSearchItemName = "<a href='/Obituaries.aspx?ObituariesID=" + intSearchItemID + "'>" + drSearchResults[""].ToString().Trim().Replace("'","&lsquo;").Replace("\"","&quot;") + ", " + drSearchResults[""].ToString().Trim().Replace("'","&lsquo;").Replace("\"","&quot;") + "</a>";
							strSearchItemPlanName = drSearchResults[""].ToString().Trim().Replace("'","&lsquo;").Replace("\"","&quot;") + ", " + drSearchResults[""].ToString().Trim().Replace("'","&lsquo;").Replace("\"","&quot;");
							strSearchItemCity = drSearchResults[""].ToString().Trim();
							intSearchItemID = Convert.ToInt32(drSearchResults[""].ToString());
							strSearchItemFooter = "<label>";
							
							//checks if there is a birth date
							if(!string.IsNullOrEmpty(drSearchResults[""].ToString()))
								strSearchItemFooter += Convert.ToDateTime(drSearchResults[""].ToString()).ToString("MMMM dd, yyyy");
								
							//checks that there must be both a birth\death date for - to display
							if(!string.IsNullOrEmpty(drSearchResults[""].ToString()) && !string.IsNullOrEmpty(drSearchResults[""].ToString())) 
								strSearchItemFooter += "-";
								
							//checks if there is a death date or a is this a pre-plan obituarie
							if(!string.IsNullOrEmpty(drSearchResults[""].ToString()))
								//sets the death year
								strSearchItemFooter += Convert.ToDateTime(drSearchResults[""].ToString()).ToString("MMMM dd, yyyy");
								
							strSearchItemFooter += "<br/>" + strObituaryText + "</label>";
							
							//checks if there is a least one image to display
							if(dtSearchItemImage != null)
							{
								//begin the primay image
								strSearchLeftResultItem = "<a href='/Obituaries.aspx?ObituariesID=" + intSearchItemID + "'><img alt='" + drSearchResults[""].ToString().Trim().Replace("'","&lsquo;").Replace("\"","&quot;") + ", " + drSearchResults[""].ToString().Trim().Replace("'","&lsquo;").Replace("\"","&quot;") + "' src='/Images/user/" + intSearchItemID + "/";
							
								//checks if there is a thumbnail for the primay image
								if (File.Exists(Server.MapPath("~/Images/user/" + intSearchItemID + "/" + dtSearchItemImage.Rows[0][""].ToString().Replace(".","_upload_thumbnail."))))
									//uses the primay image thumbnial
									strSearchLeftResultItem += dtSearchItemImage.Rows[0][""].ToString().Replace(".","_upload_thumbnail.");
								else
									//uses the primay image as there is currenly no thumbnial for this primay image
									strSearchLeftResultItem += dtSearchItemImage.Rows[0][""].ToString();
							
								//ends the primay image
								strSearchLeftResultItem += "' /></a>";
							}//end of if
						}//end of else
						
						//displays the results to the user
						litSearchBody.Text += "<div id='divSearchResultPage" + intIndexRowID + "' class='customContainer divSearchPageContainer'>" + 
							"<div class='customLeft";
							
							//checks if there is any items in in the divSearchPageLeft 
							//as there will be times that there is no items to display
							if(!string.IsNullOrEmpty(strSearchLeftResultItem))
								litSearchBody.Text += " divSearchPageLeft'>" + strSearchLeftResultItem;
							else	
								litSearchBody.Text += "'>";
								
							litSearchBody.Text += "</div>" + 
								"<div class='customRight divSearchPageRight";
							
								//checks if there is any items in in the divSearchPageLeft 
								//as there will be times that there is no items to display
								if(!string.IsNullOrEmpty(strSearchLeftResultItem))
									litSearchBody.Text += " divSearchPageRightWhenLeftIsBeingUse'>";
								else	
									litSearchBody.Text += "'>";
							
							litSearchBody.Text += "<div class='customContainer divSearchPageRightFHServiceLocationContainer'>" + 
									"<div class='customLeft divSearchPageRightFHServiceLocationLeft' " + divStyle + ">" + 
										strSearchItemName + 
										"<div class='divSearchItemDetails'>" + 
											"<label class='lblSearchPageResults'>" + strSearchItemFooter + "</label>" + 
										"</div>" + 
									"</div>" + 
									"<div class='customMiddle divSearchPageRightFHServiceLocationMiddle'>" + 
										strSearchItemPhone + 
									"</div>" + 
									"<div class='customMiddle divSearchPageRightFHServiceLocationMiddleRight'>" + 
										"<label class='lblSearchPageResults'>" + 
											strSearchItemCity;
										
										//checks if there will be text to displays for the City/Province
										if(!string.IsNullOrEmpty(strSearchItemCity) && !string.IsNullOrEmpty(drSearchResults[""].ToString()))
											//sets the septortator
											litSearchBody.Text += ", ";
										
										//sets the full name of the short province
										litSearchBody.Text += drSearchResults[""].ToString() + "</label>" + 
									"</div>" + 
									"<div class='customRight divSearchPageRightFHServiceLocationRight'>";
							
								//checks if there is a map to display
								if(!string.IsNullOrEmpty(strSearchItemMap))
									litSearchBody.Text += "<a href='javascript:void(0);' class='aSearchItemMap' onClick='" + strSearchItemMap + "toggleLayer(&quot;divHiddenHeaderMap&quot;,&quot;divGrayBG&quot;,&quot;&quot;);getDocID(&quot;lblHiddenMapName&quot;).innerHTML=&quot;Location for " + strSearchItemPlanName + " - " + strSearchItemCity.Replace("'","&lsquo;").Replace("\"","&quot;") + ", " + drSearchResults[""].ToString() + "&quot;;'>Map</a>";

							litSearchBody.Text += "</div>" + 
									"<div class='customFooter divSearchPageRightFHServiceLocationFooter'></div>" + 
								"</div>" + 
							"</div>" + 
							"<div class='customFooter divSearchPageFooter'></div>" + 
						"</div>";
						
						//checkes if intSearchItemIndex is more then 20
						if(intSearchItemIndex > 20)
						{																		
							//displays the how many items are being display
							litSearchBody.Text += "<div class='divSearchResultsPageFooterNumberDisplay divJustHidden' style='display:block;' id='divSearchResults" + intIndexRowID + "NumberOfDisplay'>" +
								"<div class='customContainer divSearchPageBottomDisplayContainer'>" + 
									"<div class='customLeft divSearchPageBottomDisplayLeft'>" + 
										"<label>Displaying 1-" + intIndexRowID + " of " + intSectionTotal + "</label>" +
									"</div>" + 
									"<div class='customRight divSearchPageBottomDisplayRight'>";
									
									//checks if the button should be display
									if(intSectionTotal > intIndexRowID)
									{
										//displays the how many items are being display
										litSearchBody.Text += "<div class='divSearchResultsPageFooterButton divJustHidden' style='display:block;' id='divSearchResults" + intIndexRowID + "Button'>" +
											"<a href='javascript:void(0);' onclick='toggleLayer(&quot;divSearchResults" + intIndexRowID + "&quot;,&quot;&quot;,&quot;&quot;);toggleLayer(&quot;divSearchResults" + intIndexRowID + "Button&quot;,&quot;&quot;,&quot;&quot;);toggleLayer(&quot;divSearchResults" + intIndexRowID + "NumberOfDisplay&quot;,&quot;&quot;,&quot;&quot;);' class='green-button'>Load Next ";
							
										//checks if there the total is more then intPerPageMax if so then just say intPerPageMax
										//if not then tell the user the differnace
										if((intSectionTotal - intIndexRowID) > intPerSection)							 
											litSearchBody.Text += intPerSection;
										else
											litSearchBody.Text += (intSectionTotal - intIndexRowID);
							
										litSearchBody.Text += " Results</a>" +
										"</div>";
									}//end of if
									
							litSearchBody.Text += "</div>" + 
									"<div class='customFooter divSearchPageBottomDisplayFooter'></div>" + 
								"</div>" + 
							"</div>";
															
							//close the div and open a new one to how the rest of the paging
							litSearchBody.Text += "</div>" + 
								"<div class='divJustHidden' id='divSearchResults" + intIndexRowID + "'>";
								
							//resets intSearchItemIndex for the next sectoin
							intSearchItemIndex = 1;
						}//end of if
						else
							intSearchItemIndex++;
						
						//checks if this city in the Province is in this Province is already in listCitySearchItems
						//if there is no item found add it to the list if it is in then 
						//add one to the intNumberOf in the funcation
						if(General.searchCityNumberOf(drSearchResults[""].ToString(), strSearchItemCity, listCitySearchItems) == 0)
							//adds to the list of City search item
							listCitySearchItems.Add(new CitySearchItem(strSearchItemCity, drSearchResults[""].ToString(), drSearchResults[""].ToString(), 1));
							
						//checks if this Province is already in listProvSearchItems
						//if there is no item found add it to the list if it is in then 
						//add one to the intNumberOf in the funcation
						if(General.searchProvNumberOf(drSearchResults[""].ToString(), listProvSearchItems) == 0)
							//adds to the list of City search item
							listProvSearchItems.Add(new CitySearchItem(drSearchResults[""].ToString(), drSearchResults[""].ToString(), 1));
						
						intIndexRowID++;
					}//end of foreach
				
					//displays the how many items are being display and closes the last parts of the results
					litSearchBody.Text += "<div class='divSearchResultsPageFooterNumberDisplay'>" +
							"<label>Displaying 1-" + intIndexRowID + " of " + intSectionTotal + "</label>" +
						"</div>";
											
					litSearchBody.Text += "</div>";
				}//end of if
				else
					//turns off the sort as it is not need now
					panSortResult.Visible = false;
				
				//sets the restuls for this section in the header of the results part
				litResultsFound.Text = "<span class='teal lblBlueTitleResults'>(" + intSectionTotal + ")</span>";
								
				//updates intGothicIDIndex
				intGothicIDIndex++;

				//checks if this is the FH or Obituary or Memorial
				if(intIndex == 0)
				{
					//adds it to the resutls title and resets the tile of the tab
					litResultsFound.Text += " Obituaries ";
					lblTabName.Text = "Obituaries";
				}//end of if
				else if(intIndex == 1)
				{
					//adds it to the resutls title and resets the tile of the tab
					litResultsFound.Text += " Funeral Homes ";
					lblTabName.Text = "Funeral Homes";
				}//end of else if
				else if(intIndex == 2)
				{
					//adds it to the resutls title and resets the tile of the tab
					litResultsFound.Text += " Memorials ";
					lblTabName.Text = "Memorials";
				}//end of else if
								
				//set the restuls for this section in the tab name
				lblTabName.Text += " (" + intSectionTotal + ")";
					
				//displays the results in the top of the search back
				litResultsFound.Text += "Found";
				
				//checks if htere is any keyword that was search for
				if (!string.IsNullOrEmpty(Request.QueryString["q"]))
					litResultsFound.Text += " for <span class='teal lblBlueTitleResults'>&quot;" + Request.QueryString["q"] + "&quot;</span>";
				
				//checks if there is a city or province if so then then add in 'in' to 
				//to make the english sectince
				if (!string.IsNullOrEmpty(Request.QueryString["c"]) || !string.IsNullOrEmpty(Request.QueryString["p"]))
					litResultsFound.Text += " in ";

				//checks if there is a city to display the name
				if (!string.IsNullOrEmpty(Request.QueryString["c"]))
					litResultsFound.Text += Request.QueryString["c"];
					
				//checks if there is a city and province if so then then add in ','
				//to septatate the city with the province
				if (!string.IsNullOrEmpty(Request.QueryString["c"]) && !string.IsNullOrEmpty(Request.QueryString["p"]))
					litResultsFound.Text += ", ";
								
				//checks if there is a province to display the name
				if (!string.IsNullOrEmpty(Request.QueryString["p"]))
					litResultsFound.Text += Request.QueryString["p"];
				
				//updates intGothicIDIndex
				intGothicIDIndex++;
				
				//checks if this is the FH or a Obituary
				if(intIndex == 0 || intIndex == 2)
					//for the Obituary Type as in the database Obituary is 1 and Memorial is 2
					intObituaryType++;
			}//end of for loop
			
			//goes around for each filter as they do the same set up and desgin
			for(int intIndex = 0;intIndex < arrFilters.Length;intIndex++)
			{
				int intCitySearchItemIndex = 1;//holds where which row the foreach is one for the city search
				int intIndexCityRowID = 0;//holds the row id for the city search display
				Literal litSearch = (Literal)panRefineSearch.FindControl("litSearch" + arrFilters[intIndex]);//holds the filter
				List<CitySearchItem> listSearchItems;//holds the list either search items
				CitySearchItemComparer csiCompare = new CitySearchItemComparer();//holds which compare to use for sorting
				
				//checks which list to use city or province
				if(intIndex == 0)
				{
					//sets the city list to the empty list as to make 
					//it easiter to use only one list in the for each
					listSearchItems = listCitySearchItems;
					
					//changes the compare to city
					csiCompare.ComparisonMethod = CitySearchItemComparer.ComparisonType.City;
				}//end of if
				else
				{
					//sets the province list to the empty list as to make 
					//it easiter to use only one list in the for each
					listSearchItems = listProvSearchItems;
					
					//changes the compare to province
					csiCompare.ComparisonMethod = CitySearchItemComparer.ComparisonType.Province;
				}//end of else
									
				//sort listSearchItems
				listSearchItems.Sort();

				//starts the city search display section
				litSearch.Text = "<div>";
				
				//goes around for each city in the search results found
				//and display the those to the user
				foreach(CitySearchItem siCitySearch in listSearchItems)
				{
					string strSearchLinkText = siCitySearch.SearchProvince;//holds the text that will be link
					
					//checks which list to use city or province
					if(intIndex == 0)
					{
						//sets the city name
						strSearchLinkText = siCitySearch.SearchCity;
						
						//checks if there will be text to displays for the City/Province
						if(!string.IsNullOrEmpty(siCitySearch.SearchCity) && !string.IsNullOrEmpty(siCitySearch.SearchProvince))
							//sets the septortator
							strSearchLinkText += ", ";
						
						//sets the full name of the short province
						strSearchLinkText += siCitySearch.SearchProvince;
					}//end of if

					//checks if there is any text to displays for the City/Province or Province
					if(!string.IsNullOrEmpty(strSearchLinkText))
					{
						//checkes if intCitySearchItemIndex is more then 5
						if(intCitySearchItemIndex > 5)
						{
							//checks if the button should be display
							if(listSearchItems.Count > intIndexCityRowID)
							{
								//displays the how many items are being display" + strWhere + "
								litSearch.Text += "<div class='divCitySearchResultsPageFooterButton divJustHidden' style='display:block;' id='divCitySearchResults" + intIndex + intIndexCityRowID + "Button'>" +
									"<a href='javascript:void(0);' onclick='toggleLayer(&quot;divCitySearchResults" + intIndex + intIndexCityRowID + "&quot;,&quot;&quot;,&quot;&quot;);toggleLayer(&quot;divCitySearchResults" + intIndex + intIndexCityRowID + "Button&quot;,&quot;&quot;,&quot;&quot;);'>show more</a>" +
								"</div>";
							}//end of if
																				
							//close the div and open a new one to how the rest of the paging
							litSearch.Text += "</div>" + 
							"<div class='divJustHidden' id='divCitySearchResults" + intIndex + intIndexCityRowID + "'>";
								
							//resets intCitySearchItemIndex for the next sectoin
							intCitySearchItemIndex = 1;
						}//end of if
						else
							intCitySearchItemIndex++;
					
						//displays all of the cities that are in the search
						litSearch.Text += "<div class='customContainer divSearchResultPageCitySearchContainer'>" + 
							"<div class='customLeft divSearchResultPageCitySearchLeft'>" + 
								"<a href='javascript:void(0)' onClick='javascript:refineSearchResults(&quot;" + createSearchURL(txtKeywords.Text, siCitySearch.SearchCity, siCitySearch.SearchProvinceShortName) + "&quot;, getDocID(&quot;divSearchResultPageTab&quot;))'>" + 
									strSearchLinkText + 
								"</a>" + 
							"</div>" + 
							"<div class='customRight divSearchResultPageCitySearchRight'>" + 
								"<label>" + siCitySearch.NumberOf + "</label>" + 
							"</div>" + 
							"<div class='customFooter divSearchResultPageCitySearchFooter'></div>" + 
						"</div>";
					
						intIndexCityRowID++;
					}//end of if
				}//end of foreach
				
				//closes the city search display section
				litSearch.Text += "</div>";	
			}//end of for loop	
		}//end of try
        catch (Exception ex)
        {
            lblError.Text = ex.Message;// + " " + ex.StackTrace;
            lblError.Visible = true;
        }//end of catch
    }//end of Page_PreRender()
	
	protected void ibKeywords_Click(object sender, EventArgs e)
    {
		try
		{
			//checks if there is at least 2 chars to do a search in order not to 
			if(txtKeywords.Text.Length > 2)
				//reloads the page with a new keyword search
				Response.Redirect("/SearchResult.aspx?q=" + Server.UrlEncode(txtKeywords.Text));
			else
				throw new Exception("Please enter a minimum of 3 letters to start your search");
		}//end of try
        catch (Exception ex)
        {
            lblError.Text = ex.Message;// + " " + ex.StackTrace;
            lblError.Visible = true;
        }//end of catch
	}//end of ibKeywords_Click()
	
	protected void ibCitySearch_Click(object sender, EventArgs e)
    {
		try
		{
			//checks if there is at least 2 chars to do a search in order not to 
			if(txtCitySearch.Text.Length > 2)
			{
				string strProvince = "";//holds the province  that was filter
				
				//checks if there is a province that was filter already
				if (!string.IsNullOrEmpty(Request.QueryString["p"]))
					strProvince = Request.QueryString["p"];
				
				//reloads the page with for the keyword/city/province search
				Response.Redirect(createSearchURL(txtKeywords.Text, txtCitySearch.Text, strProvince));
			}//end of if
			else
				throw new Exception("Please enter a minimum of 3 letters to start your search");
		}//end of try
        catch (Exception ex)
        {
            lblError.Text = ex.Message;// + " " + ex.StackTrace;
            lblError.Visible = true;
        }//end of catch
	}//end of ibCitySearch_Click()
		
	protected void ddlSortResult_SelectedIndexChanged(object sender, EventArgs e)
    {
		string strProvince = "";//holds the province that was filter	
		string strCity = "";//holds the city that was filter
		string strKeyword = "";//holds the keyword that was filter
		
		//checks if there is a province that was filter already
		if (!string.IsNullOrEmpty(Request.QueryString["q"]))
			strKeyword = Request.QueryString["q"];
		
		//checks if there is a province that was filter already
		if (!string.IsNullOrEmpty(Request.QueryString["p"]))
			strProvince = Request.QueryString["p"];
				
		//checks if there is a province that was filter already
		if (!string.IsNullOrEmpty(Request.QueryString["c"]))
			strCity = Request.QueryString["c"];
		
		//reloads the page with for the keyword/city/province/sorting search
		Response.Redirect(createSearchURL(strKeyword, strCity, strProvince, (DropDownList)sender));
	}//end of ddlSortResult_SelectedIndexChanged()
}//end of Page