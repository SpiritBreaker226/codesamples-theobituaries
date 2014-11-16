// Administrtoation of the Rotating banners

public partial class RotatingBannersAdmin : PortalModuleBase
{	
	private int _Cols = 4;
    private int _Rows = 3;
    private int _PageSize;
    private int _HiddenPageIndex;
    private int _LastPageIndex;
	private int _Count;
	private bool _PagingVarsInitialized = false;
	private string strSearchWhere = "";//holds the where for the search
	
	private void Bind(int intSelectedIndex)
    {
		//UPDATE THE RESULT INDEX MESSAGE
        int startRowIndex = (_PageSize * _HiddenPageIndex);
        int endRowIndex = startRowIndex + _PageSize;
		
        if (endRowIndex > _Count) 
			endRowIndex = _Count;
		
		if (_HiddenPageIndex > 0) 
			startRowIndex++;

		DataTable dtBanner = DAL.getRows(""," DESC",startRowIndex,endRowIndex,strSearchWhere);//holds the where will be display to the user
			
		//checks if there is any items to find if not then dispaly it to the user and change the text around
		gdRotatingBanners.Visible = !(dtBanner.Rows.Count == 0);
		litNoFound.Visible = (dtBanner.Rows.Count == 0);
		litNoFound.Text = "There are no banners<br/><br/>";
				
		//gets the data
        gdRotatingBanners.DataSource = dtBanner;
		gdRotatingBanners.DataBind();
    }//end of Bind()
	
    private void InitializePagingVars(bool forceRefresh)
    {
        if (!_PagingVarsInitialized || forceRefresh)
        {
			DataTable dtCount = DAL.countRows("",strSearchWhere);

			_HiddenPageIndex = Convert.ToInt32(HiddenPageIndex.Value);
			_Count = Convert.ToInt32(dtCount.Rows[0][""].ToString());
			_LastPageIndex = ((int)Math.Ceiling(((double)_Count / (double)_PageSize))) - 1;
			_PagingVarsInitialized = true;
        }//end of if
    }//end of InitializePagingVars()
	
	private void BindDesignsPanel()
    {
        //INITIALIZE PAGING VARIABLES
        InitializePagingVars(false);

        //BIND THE ASK QUESTION
        Bind(-1);

        //BIND THE PAGING CONTROLS FOOTER
        BindPagingControls();
    }//end of BindDesignsPanel()
	
	protected void Page_PreRender(object sender, EventArgs e)
    {
		//sets strSearchWhere before the page is rendered.
	    ViewState.Add("", strSearchWhere);
		
        //BIND PAGE
        BindDesignsPanel();
    }//end of Page_PreRender()
	
	protected void Page_Init(object sender, EventArgs e)
	{
	}//end of Page_Init()
			
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
				strSearchWhere = " LIKE '%" + DAL.safeSql(txtSearch.Text) + "%'";
			}//end of if
			else
				//resets strSearch
				strSearchWhere = "";
        }//end of if
		
		//checks if there is something in viewState for where if so then
		if (ViewState[""] != null)
			//sets the search where for the search
			strSearchWhere = (string)ViewState[""];
		else			
			SetPagerIndex();
	}//end of Page_Load()
			
	protected void gdRotatingBanners_ItemDataBound(object sender, DataGridItemEventArgs e)
	{
		if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
		{
			
		}//end of if
	}//end of gdRotatingBanners_ItemDataBound()
	
	protected void gdRotatingBanners_ItemCommand(object sender, DataGridCommandEventArgs e)
	{
		//checks which Command to use
        if (e.CommandName == "Delete")
        {
			//deletes the image from the server for this banner
			if (File.Exists(Server.MapPath(".\\Portals\\_default\\Skins\\Obit\\Images\\Banners\\") + e.Item.Cells[5].Text))
                File.Delete(Server.MapPath(".\\Portals\\_default\\Skins\\Obit\\Images\\Banners\\") + e.Item.Cells[5].Text);
			
            //Deltes a Row from Rotating Banners table
            DAL.deleteRotatingBanners(Convert.ToInt32(e.Item.Cells[0].Text));

			//reloads the users
            cmdCancel_Click(sender, e);
        }//end of if
		
        if (e.CommandName == "Update")
        {
			//sets the basic items
			lblID.Text = e.Item.Cells[0].Text;
			txtTitle.Text = e.Item.Cells[1].Text;
			rdoEnable.SelectedValue = e.Item.Cells[4].Text;
			rdoImagePostion.SelectedValue = e.Item.Cells[7].Text;
			txtBannersLink.Text = e.Item.Cells[8].Text;
			txtBannersOrder.Text = e.Item.Cells[10].Text;
			
			//checks if this cells content exits
			if(!string.IsNullOrEmpty(e.Item.Cells[5].Text.Replace("&nbsp;","").Trim()))
			{
				lblImageSource.Text = e.Item.Cells[5].Text;
				imgBanner.ImageUrl = "/Portals/_default/Skins/Obit/Images/Banners/" + e.Item.Cells[5].Text;
			}//end of if
			
			//checks if this cells content exits
			if(!string.IsNullOrEmpty(e.Item.Cells[6].Text.Replace("&nbsp;","").Trim()))
				txtHTML.Text = e.Item.Cells[6].Text;
					
            //checks if this cells content exits
			if(!string.IsNullOrEmpty(e.Item.Cells[2].Text.Replace("&nbsp;","").Trim()))
			{
				string[] arrStartDate = e.Item.Cells[2].Text.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);//holds the start date split in bettween the date and time 0 = Date, 1 = Time, 2 = AM/PM designator	
				//sets the calendar date
				calStartDate.SelectedDate = Convert.ToDateTime(arrStartDate[0]);
				calStartDate.VisibleDate = Convert.ToDateTime(arrStartDate[0]);
				
				//sets the calendar time
				lblStartTime.Text = " " + arrStartDate[1] + " " + arrStartDate[2];
			}//end of if
			
			 //checks if this cells content exits
			if(!string.IsNullOrEmpty(e.Item.Cells[3].Text.Replace("&nbsp;","").Trim()))
			{
				string[] arrEndDate = e.Item.Cells[3].Text.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);//holds the end date split in bettween the date and time 0 = Date, 1 = Time, 2 = AM/PM designator	
				//sets the calendar date
				calEndDate.SelectedDate = Convert.ToDateTime(arrEndDate[0]);
				calEndDate.VisibleDate = Convert.ToDateTime(arrEndDate[0]);
				
				//sets the calendar time
				lblEndTime.Text = " " + arrEndDate[1] + " " + arrEndDate[2];
			}//end of if
									
			//changes the layout for Updating
			panRotatingBannersDisplay.Visible = false;
			panRotatingBannersAdd.Visible = true;
			cmdCancel.Visible = true;
			cmdSave.Text = "Update";
			lblMainTitle.Text = "Fill the form to update the Item data";
        }//end of if
	}//end of gdRotatingBanners_ItemCommand()
			
	protected void cmdSave_Click(object sender, EventArgs e)
	{
		try
		{
			//checks if either a Image or HTML is beeing used
			if(fulImageUpload.HasFile || !string.IsNullOrEmpty(lblImageSource.Text) || !string.IsNullOrEmpty(txtHTML.Text.Trim()))
			{
				string strImageLoc = "";//holds the loction
			
				//Change if they what to changes the Main Image of the Category
				if (fulImageUpload.HasFile) 
				{
					//uplaods the Image to the site
					strImageLoc = General.uploadImage("/Portals/_default/Skins/Obit/Images/Banners/",fulImageUpload.PostedFile);
					
					//checks if there was an error with the upliad
					if(strImageLoc.IndexOf("ERROR! ") >= 0)
						throw new Exception(strImageLoc);
					else
						//removes the /Portals/_default/Skins/Obit/Images/Banners/ from the start of the start of the files in order 
						//to make it more flexiable to use in different places
						strImageLoc = strImageLoc.Replace("/Portals/_default/Skins/Obit/Images/Banners/","");
				}//end of if
				else
					strImageLoc = lblImageSource.Text;
					
				//checks if there is any data in the Start Calendar if so then get the current time
				if(!string.IsNullOrEmpty(calStartDate.SelectedDate.ToString().Replace("&nbsp;","").Trim()))
				{
					string[] arrStartDate = calStartDate.SelectedDate.ToString().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);//holds the start date split in bettween the date and time 0 = Date, 1 = Time, 2 = AM/PM designator
					DateTime dtStartDate = Convert.ToDateTime(arrStartDate[0]);//holds the date as it needs to be formated currently
					
					//checks if there is a play time if so then use that with what the user has selected
					//if not then get current time with what the user has selected on the calendar
					if(!string.IsNullOrEmpty(lblStartTime.Text.Replace("&nbsp;","").Trim()))
						lblStartTime.Text = dtStartDate.ToString("yyyy-MM-dd") + lblStartTime.Text;
					else
						lblStartTime.Text = dtStartDate.ToString("yyyy-MM-dd") + DateTime.Now.ToString(" h:mm:ss tt");
				}//end of if
				
				//checks if there is any data in the Start Calendar if so then get the current time
				if(!string.IsNullOrEmpty(calEndDate.SelectedDate.ToString().Replace("&nbsp;","").Trim()))
				{
					string[] arrEndDate = calEndDate.SelectedDate.ToString().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);//holds the end date split in bettween the date and time 0 = Date, 1 = Time, 2 = AM/PM designator
					DateTime dtEndDate = Convert.ToDateTime(arrEndDate[0]);//holds the date as it needs to be formated currently
					
					//checks if there is a play time if so then use that with what the user has selected
					//if not then get current time with what the user has selected on the calendar
					if(!string.IsNullOrEmpty(lblEndTime.Text.Replace("&nbsp;","").Trim()))
						lblEndTime.Text = dtEndDate.ToString("yyyy-MM-dd") + lblEndTime.Text;
					else
						lblEndTime.Text = dtEndDate.ToString("yyyy-MM-dd") + DateTime.Now.ToString(" h:mm:ss tt");
				}//end of if

				//checks if there is a id if so then do an Update on the Users if not then do Insert 
				if(!string.IsNullOrEmpty(lblID.Text))
					DAL.addUpdateRotatingBanners(Convert.ToInt32(lblID.Text), txtTitle.Text, strImageLoc, rdoImagePostion.SelectedValue, txtHTML.Text, lblStartTime.Text, lblEndTime.Text, Convert.ToBoolean(rdoEnable.SelectedValue), txtBannersLink.Text, Convert.ToInt32(txtBannersOrder.Text));
				else
					DAL.addUpdateRotatingBanners(0, txtTitle.Text, strImageLoc, rdoImagePostion.SelectedValue, txtHTML.Text, lblStartTime.Text, lblEndTime.Text, Convert.ToBoolean(rdoEnable.SelectedValue), txtBannersLink.Text, Convert.ToInt32(txtBannersOrder.Text));
					
				//resets the page
				cmdCancel_Click(sender,e);
			}//end of if
			else
			{
				lblError.Text = "You must have either a image or text for a banner";
				lblError.Visible = true;
			}//end of else
		}//end of try
		catch (Exception ex)
		{
			lblError.Text = ex.Message;
			lblError.Visible = true;
		}//end of catch
	}//end of cmdSave_Click()
	
	protected void cmdCancel_Click(object sender, EventArgs e)
	{		
		//clears all of the Items in the Fourm
		lblID.Text = "";
		txtTitle.Text = "";
		txtHTML.Text = "";
		txtBannersLink.Text = "";
		imgBanner.ImageUrl = "";
		lblImageSource.Text = "";
		lblStartTime.Text = "";
		lblEndTime.Text = "";
		txtBannersOrder.Text = "";
		
		rdoImagePostion.SelectedValue = "C";
		rdoEnable.SelectedValue = "False";
		calStartDate.SelectedDates.Clear();		
		calEndDate.SelectedDates.Clear();
				
		//changes the layout for Canceling
		panRotatingBannersDisplay.Visible = true;
		panRotatingBannersAdd.Visible = false;
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
		panRotatingBannersDisplay.Visible = false;
		panRotatingBannersAdd.Visible = true;
		cmdCancel.Visible = true;
	}//end of cmdAdd_Click()
		
	protected void cmdSearch_Click(object sender, EventArgs e)
	{
		//sets the search where so that when the page reloads it does the search
		strSearchWhere = " LIKE '%" + DAL.safeSql(txtSearch.Text) + "%'";
	}//end of cmdSearch_Click()
	
	protected void cmdClearSearch_Click(object sender, EventArgs e)
	{
		//resets the search parts
		strSearchWhere = "";
		txtSearch.Text = "";
	}//end of cmdClearSearch_Click()
		
	protected void lbEndClearDate_Click(object sender, EventArgs e)
	{		
		//clears the Date of Played for part of reseting played
		calEndDate.SelectedDates.Clear();
	}//end of lbEndClearDate_Click()
	
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
            
			string baseUrl = "ManageBanners.aspx?search=" + Server.UrlEncode(txtSearch.Text) + "&p=";
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