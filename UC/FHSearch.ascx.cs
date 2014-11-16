// Search For Funeral Homes In Sign Up To Attach the new user to a funeral home

public partial class FHSearch : System.Web.UI.UserControl
{
	private int intDisplayResults = 10;//holds the number of items to display
	private int intMaxResults = 10;//holds the number of items the user is allowed to selected
	private string strPostBackLoc = "ctr561$SignUp$FHSignUp$FHSearch";//holds the holds the location of the post back for lbChooseFH
	private string strAjaxFile = "/ASP/SearchFHSignUp.aspx";//holds the file where the process of the search will happen
	private string strTypeName = "Funeral Homes";//holds the type that will be for display and if this sia FH or User
	private string strFuneralHomeStatus = "-1";//holds which status to search for
	
	#region properties
	
	public string AjaxFile
    { 
        get { return strAjaxFile; } 
        set { strAjaxFile = value; }
    }//end of AjaxFile
	
	public int DisplayResults
    { 
        get { return intDisplayResults; } 
        set { 
		
			//checks if the value is below zero as there is no MaxResults belwo zero
			if(value < 0)
				intDisplayResults = 10; 	
			else
				intDisplayResults = value;
		}
    }//end of DisplayResults
	
	public string FuneralHomeStatus
    { 
        get { return strFuneralHomeStatus; } 
        set { strFuneralHomeStatus = value; }
    }//end of FuneralHomeStatus
		
	public int MaxResults
    { 
        get { return intMaxResults; } 
        set { 
		
			//checks if the value is below zero as there is no MaxResults belwo zero
			if(value < 0)
				intMaxResults = 10; 	
			else
				intMaxResults = value;
		}
    }//end of MaxResults
	
	public string PostBackLoc
    { 
        get { return strPostBackLoc; } 
        set { strPostBackLoc = value; }
    }//end of PostBackLoc
	
	public string TypeName 
    { 
        get { return strTypeName; } 
        set { strTypeName = value;}
    }//end of TypeName
	
	#endregion
			
	#region Public Funcations
	
	public string[] getValues()
	{
		//returns name and value of each item the user has search and selected
		return hfChooseFHItems.Value.Split(new string[] {"@*"}, StringSplitOptions.RemoveEmptyEntries);
	}//end of getValues()
	
	//loads the values from hfChooseFHItems into lbChooseFH
	public void loadChooseFH()
	{
		string[] arrFHItems = hfChooseFHItems.Value.Split(new string[] {"@*"}, StringSplitOptions.RemoveEmptyEntries);//holds the items that will be going into lbChooseFH
		int intNumberRows = 0;//holds the number of rows that are beind display
		
		//resets the error mesage
		lblError.Visible = false;
		
		//resets the search textbox for next use
		txtSearch.Text = "";
		
		//clears the lbChooseFH.Items in order to make sure only one of the items shows up
		lbChooseFH.Items.Clear();
		
		//goes around for each item and adds them to lbChooseFH
		//first item is the name and the secound one is the value so it skip one always
		for(int intIndex = 0;intIndex < arrFHItems.Length;intIndex = intIndex + 3)
		{
			intNumberRows++;
			
			//checks if the lesser then the amount allow to be display
			//if greater then display a message
			if(intNumberRows > intMaxResults)
			{
				lblError.Text = "You are only allowed " + intMaxResults + " " + strTypeName + " that can be selected";
				lblError.Visible = true;
				break;
			}//end of if
			else
				//adds the items into lbChooseFH from hfChooseFHItems 
				lbChooseFH.Items.Add(new ListItem(arrFHItems[intIndex] + " - " + arrFHItems[(intIndex + 2)],arrFHItems[(intIndex + 1)]));
		}//end of for loop
		
		//checks if there is anything items in hfChooseFHItems
		if(!string.IsNullOrEmpty(hfChooseFHItems.Value))
		{
			//turns on the Choose Funeral Home Currently Selected and the bottom in order to clear it
			panChooseFuneralHomeCurrentlySelected.Style.Add("display", "block");
			panChooseFuneralHomeBottom.Style.Add("display", "block");
		}//end of if
		else
		{
			//turns off the Choose Funeral Home Currently Selected and the bottom in order to clear it
			panChooseFuneralHomeCurrentlySelected.Style.Add("display", "");
			panChooseFuneralHomeBottom.Style.Add("display", "");
		}//end of else
	}//end of loadChooseFH()
	
	public void setsValues(string[] arrNewValues)
	{
		//resets hfChooseFHItems to have clear selection
		hfChooseFHItems.Value = "";
		
		//goes around adding back in the values as there maybe other values
		for(int intIndex = 0;intIndex < arrNewValues.Length;intIndex = intIndex + 3)
		{
			//sets items back into hfChooseFHItems
			hfChooseFHItems.Value += "@*" + arrNewValues[intIndex] + "@*" + arrNewValues[intIndex + 1] + "@*" + arrNewValues[(intIndex + 2)];
		}//end of for loop
		
		//reloads the search to have the new items
		loadChooseFH();
	}//end of resetValues()
	
	public void resetValues()
	{
		//resets the Hidden Field values in order not to be use if the user wants to try something else
		hfChooseFHItems.Value = "";
	}//end of resetValues()
	
	#endregion
	
	protected void Page_PreRender(object sender, EventArgs e)
    {
		string strJSRemoveFunction = "clearAllFHSignUp";//holds the JS function that remove items from lbChooseFH
		
		//sets the instane search 
		txtSearch.Attributes.Add("onKeyup", "javascript:sendSearchSignUpFuneralHome('" + strAjaxFile + "','divSearchMessage',getDocID('divSearchResults'),getDocID('" + txtSearch.ClientID + "'), " + intDisplayResults + ", '" + strPostBackLoc + "','" + lbChooseFH.ClientID + "', '" + hfChooseFHItems.ClientID + "', '" + panChooseFuneralHomeBottom.ClientID + "', '" + panChooseFuneralHomeCurrentlySelected.ClientID + "', '" + strFuneralHomeStatus + "');");
		
		//checks if this is from FH or User
		if(strTypeName == "Users")
		{
			strJSRemoveFunction = "clearAllUserSignUp";
			lblBelowTextOfFHRemove.Text = "User";
		}//end of if
		
		//sets the client clicks for both the selected remove and all
		lbRemoveSelected.OnClientClick += "javascript:" + strJSRemoveFunction + "('divSearchMessage',getDocID('" + txtSearch.ClientID + "'),getDocID('" + panChooseFuneralHomeCurrentlySelected.ClientID + "'),getDocID('" + panChooseFuneralHomeBottom.ClientID + "'),getDocID('" + hfChooseFHItems.ClientID + "'),getDocID('" + lbChooseFH.ClientID + "'), false, '" + strPostBackLoc + "');";
		lbRemoveAll.OnClientClick += "javascript:" + strJSRemoveFunction + "('divSearchMessage',getDocID('" + txtSearch.ClientID + "'),getDocID('" + panChooseFuneralHomeCurrentlySelected.ClientID + "'),getDocID('" + panChooseFuneralHomeBottom.ClientID + "'),getDocID('" + hfChooseFHItems.ClientID + "'),getDocID('" + lbChooseFH.ClientID + "'), true, '" + strPostBackLoc + "');";
		
		//sets the search to beused for both FH or User
		lblCurrentlySelected.Text = "Currently Selected " + strTypeName + ":";
		txtSearch.Attributes.Add("placeholder", "Search For " + strTypeName);
		
		if (IsPostBack)
			//loads the items back into lbChooseFH after postback
			loadChooseFH();
    }//end of Page_PreRender()
}//end of User Contorl