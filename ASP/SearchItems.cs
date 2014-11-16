// Holds a search item object whne the user does a search on the site

public class SearchItems : System.Web.UI.UserControl
{
	private string strSearchItemName;//holds the Name of the search item
	private string strSearchItemCity;//holds the name of the city
	private string strSearchItemFooter;//holds the footer of the search results item
	private string strSearchLeftResultItem;//holds the left section of the search results item
	private string strSearchItemPhone;//holds the phone the FH
	private string strSearchItemMap;//holds the location of the search item in map
	private int intSearchItemID;//holds the search item id
	private int intSearchTable;//holds which table to this search item belongs to 0 = FH, 1 = Obituaries, 2 = Memeorial 
	private int intSearchItemOrder;//holds where this item is in the order of the search 0 = First, 1 = Middle, 2 = Last
	private bool boolIsPublish;//holds the if this is publish for FH
	
	#region "Contstutor funcation"
	
	//Default Contstutor
	
	public SearchItems()
	{
		//sets the default for the valuables
		strSearchItemName = "";
		strSearchItemCity = "";
		strSearchItemFooter = "";
		strSearchLeftResultItem = "";
		strSearchItemPhone = "";
		strSearchItemMap = "";
		intSearchTable = 0;
		intSearchItemOrder = 0;
		intSearchItemID = -1;
		boolIsPublish = false;
	}//end of Default Contstutor()
	
	//1st Contstutor
	
	public SearchItems(string strSearchItemNameNewData, string strSearchItemCityNewData, string strSearchItemFooterNewData, string strSearchLeftResultItemNewData, string strSearchItemPhoneData, string strSearchItemMapData, int intSearchItemIDData, int intSearchTableNewData, int intSearchItemOrderNewData, bool boolIsPublishData)
	{
		//sets the values that the user what to use
		strSearchItemName = strSearchItemNameNewData;
		strSearchItemCity = strSearchItemCityNewData;
		strSearchItemFooter = strSearchItemFooterNewData;
		strSearchLeftResultItem = strSearchLeftResultItemNewData;
		strSearchItemPhone = strSearchItemPhoneData;
		strSearchItemMap = strSearchItemMapData;
		intSearchItemID = intSearchItemIDData;
		intSearchTable = intSearchTableNewData;
		intSearchItemOrder = intSearchItemOrderNewData;
		boolIsPublish = boolIsPublishData;
	}//end of 1st Contstutor()
	
	#endregion

    #region Properties
		
	public string SearchItemName
    { 
        get { return strSearchItemName; } 
        set { strSearchItemName = value; }
    }//end of SearchItemName
	
	public string SearchItemCity
    { 
        get { return strSearchItemCity; } 
        set { strSearchItemCity = value; }
    }//end of SearchItemCity
	
	public string SearchItemFooter
    { 
        get { return strSearchItemFooter; } 
        set { strSearchItemFooter = value; }
    }//end of SearchItemFooter
	
	public string SearchLeftResultItem
    { 
        get { return strSearchLeftResultItem; } 
        set { strSearchLeftResultItem = value; }
    }//end of SearchLeftResultItem
	
	public string SearchItemPhone
    { 
        get { return strSearchItemPhone; } 
        set { strSearchItemPhone = value; }
    }//end of SearchItemPhone 
	
	public string SearchItemMap
    { 
        get { return strSearchItemMap; } 
        set { strSearchItemMap = value; }
    }//end of SearchItemPhone 
	
	public int SearchItemID
    { 
        get { return intSearchItemID; } 
        set { intSearchItemID = value; }
    }//end of SearchItemID
	
	public int SearchTable
    { 
        get { return intSearchTable; } 
        set { intSearchTable = value; }
    }//end of SearchTable
	
	public int SearchItemOrder
    { 
        get { return intSearchItemOrder; } 
        set { intSearchItemOrder = value; }
    }//end of SearchItemOrder
	
	public bool IsPublish
    { 
        get { return boolIsPublish; } 
        set { boolIsPublish = value; }
    }//end of IsPublish
			
	#endregion
}//end of class SearchItems