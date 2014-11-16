// Display Flowers For Obituary

public partial class ObituaryFlower : PortalModuleBase
{
	private int _Cols = 4;
    private int _Rows = 2;
    private int _PageSize;
    private int _HiddenPageIndex;
    private int _LastPageIndex;
	private int _Count;
	private bool _PagingVarsInitialized = false;
	private string strCurrentCate = "fa";//holds the current category that is being search for
	
	private void Bind(int intSelectedIndex)
    {
		//UPDATE THE RESULT INDEX MESSAGE
        int startRowIndex = (_PageSize * _HiddenPageIndex);
        int endRowIndex = startRowIndex + _PageSize;
	
        if (endRowIndex > _Count) 
			endRowIndex = _Count;
		
		if (_HiddenPageIndex > 0) 
			startRowIndex++;

		//create the search 
		createSearchItems(startRowIndex, 12);
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
	
	//creates a new search items whent the user search for flowers
	private void createSearchItems(int intStartIndex, int intMaxDisplay)
	{
		try
		{
			//resets the flower error
			litFlowerError.Text = "";
			
			//checks if is lesser then 1 as the API only allows 1 not zero
			if(intStartIndex < 1)
				//floors it to the closes one
				intStartIndex = 1;
													
			Flower.FlowerShopService flsService = new Flower.FlowerShopService();//holds the flower Service 
			Flower.CartService csService = new Flower.CartService();//holds the flower Shoring Cart Service 
			Flower.GetProductsResponse gprProducts = flsService.getProducts(ConfigurationManager.AppSettings[""], ConfigurationManager.AppSettings[""], strCurrentCate, intMaxDisplay, intStartIndex);//holds the all of the products for a category sympathy
			
			//creates a flower shopping cart
			csService.createShoppingCart(ConfigurationManager.AppSettings[""], ConfigurationManager.AppSettings[""], System.Web.HttpContext.Current.Session.SessionID + hfObituatyId.Value);

			//checks if there is any errors
			if (gprProducts.errors.Length > 0)
			{
				//goes around display each of the errors
				foreach (Flower.Error err in gprProducts.errors)
				{
					litFlowerError.Text += "<div>" + err.field + "</div><div>" + err.message + "</div>";
				}//end of foreach
			}//end of if
			else 
			{
				int intFlowerPerRowIndex = 1;//holds number of items per row
				int intFlowerIndexID = 1;//holds unqiue number of items id
				Panel panFlowersDisplayHolderContainer = new Panel();//holds the flower display holder Container
				Panel panFlowersDisplayHolderOuterFooter = new Panel();//holds the flowers display holder Footer

				//adds the classes to the panels and container
				panFlowersDisplayHolderContainer.CssClass = "customContainer divObiturayFlowersDisplayContainer";
				panFlowersDisplayHolderOuterFooter.CssClass = "customFooter divObiturayFlowersDisplayFooter";
				
				//starts the flower display
				phFlowerProducts.Controls.Add(panFlowersDisplayHolderContainer);
				
				//goes around displaying the Flowsers
				foreach (Flower.Product productDetail in gprProducts.products)
				{
					Panel panFlowersDisplayHolder = new Panel();//holds the flower display holder
					Panel panFlowersDisplayButtonContainer = new Panel();//holds the flower display button Container
					Panel panFlowersDisplayButtonHeader = new Panel();//holds the flower display button header
					Panel panFlowersDisplayButtonLeft = new Panel();//holds the flower display button left
					Panel panFlowersDisplayButtonRight = new Panel();//holds the flower display button right
					Panel panFlowersDisplayButtonFooter = new Panel();//holds the flower display button footer
					Label lblFlowerName = new Label();//holds the flower name
					Label lblFlowerPrice = new Label();//holds the flower price
					LinkButton lbBuyNow = new LinkButton();//holds the Buy Now 
					HyperLink hlHeaderDetails = new HyperLink();//holds the Detail link for the header
					HyperLink hlDetails = new HyperLink();//holds the Detail link for the button 
										
					//adds the classes to the Panels
					panFlowersDisplayHolder.CssClass = "customLeft divObiturayFlowersDisplayLeft";
					panFlowersDisplayButtonContainer.CssClass ="customContainer divObiturayFlowersDisplayButtonContainer";
					panFlowersDisplayButtonHeader.CssClass = "customHeader divObiturayFlowersDisplayButtonHeader";
					panFlowersDisplayButtonLeft.CssClass = "customLeft divObiturayFlowersDisplayButtonLeft";
					panFlowersDisplayButtonRight.CssClass = "ustomRight divObiturayFlowersDisplayButtonRight";
					panFlowersDisplayButtonFooter.CssClass = "customFooter divObiturayFlowersDisplayButtonFooter";
					
					//adds to the Flowers Display Button to the Flowers Display Holder
					panFlowersDisplayHolder.Controls.Add(panFlowersDisplayButtonContainer);
					
					//adds the header, left, right and footer Flowers Display Button
					panFlowersDisplayButtonContainer.Controls.Add(panFlowersDisplayButtonHeader);
					panFlowersDisplayButtonContainer.Controls.Add(panFlowersDisplayButtonLeft);
					panFlowersDisplayButtonContainer.Controls.Add(panFlowersDisplayButtonRight);
					panFlowersDisplayButtonContainer.Controls.Add(panFlowersDisplayButtonFooter);
										
					//sets the url and image url to the details page
					hlHeaderDetails.NavigateUrl = "/Obituaries/flower/Details.aspx?person=" + hfPersonId.Value + "&FHPID=" + hfFHPID.Value + "&oid=" + hfObituatyId.Value + "&flowerID=" + Server.UrlEncode(productDetail.code);
					hlHeaderDetails.ImageUrl = productDetail.image;
					
					//adds the HyperLink Image to Flowers Display Button header 
					panFlowersDisplayButtonHeader.Controls.Add(hlHeaderDetails);
					
					//sets the url and image url to the details page
					hlDetails.NavigateUrl = "/Obituaries/flower/Details.aspx?person=" + hfPersonId.Value + "&FHPID=" + hfFHPID.Value + "&oid=" + hfObituatyId.Value + "&flowerID=" + Server.UrlEncode(productDetail.code);
					hlDetails.Text = "Details";
					hlDetails.CssClass = "green-button";
					
					//adds the HyperLink to Flowers Display Button left 
					panFlowersDisplayButtonLeft.Controls.Add(hlDetails);
					
					//sets the flower name, priceing and classes
					lblFlowerName.Text = "<span class='lblFontSize14'><strong>" + productDetail.name + "</strong></span>";
					lblFlowerName.CssClass = "divFlowerName";
					lblFlowerPrice.Text = "$" + string.Format("{0:F2}", productDetail.price.ToString());
					lblFlowerPrice.CssClass = "divFlowerPrice";
					
					//adds the flower name, new line and flower price to Flowers Display Button footer
					panFlowersDisplayButtonFooter.Controls.Add(lblFlowerName);
					panFlowersDisplayButtonFooter.Controls.Add(new LiteralControl("<br />"));
					panFlowersDisplayButtonFooter.Controls.Add(lblFlowerPrice);
					
					//adds text, CommandArgument and OnCommand to the LinkButton
					lbBuyNow.Text = "Buy Now";
					lbBuyNow.ID = "lbBuyNow" + intFlowerIndexID;
					lbBuyNow.CssClass = "green-button";
					lbBuyNow.Command += lbBuyNow_Command;
					lbBuyNow.CommandArgument = productDetail.code;
					
					//adds the linkbutton to Flowers Display Button right 
					panFlowersDisplayButtonRight.Controls.Add(lbBuyNow);
					
					//displays the flower display holder to the screen
					panFlowersDisplayHolderContainer.Controls.Add(panFlowersDisplayHolder);
					
					//checkes if intFlowerItemIndex is more then 3
					if(intFlowerPerRowIndex > 3)
					{				
						Panel panFlowersDisplayHolderFooter = new Panel();//holds the flower display holder Footer
						
						//sets the class for flower display holder Footer
						panFlowersDisplayHolderFooter.CssClass = "customFooter divObiturayFlowersDisplayFooter";
							
						//adds the footer and the container for this row
						panFlowersDisplayHolderContainer.Controls.Add(panFlowersDisplayHolderFooter);																
						phFlowerProducts.Controls.Add(panFlowersDisplayHolderContainer);
						
						//resets the flower display holder Container and class
						panFlowersDisplayHolderContainer = new Panel();
						panFlowersDisplayHolderContainer.CssClass = "customContainer divObiturayFlowersDisplayContainer";				
						
						//resets intFlowerPerRowIndex for the next row
						intFlowerPerRowIndex = 1;
					}//end of if
					else
						intFlowerPerRowIndex++;
						
					intFlowerIndexID++;
				}//end of foreach
				
				//adds the footer and the container for this row
				panFlowersDisplayHolderContainer.Controls.Add(panFlowersDisplayHolderOuterFooter);																
				phFlowerProducts.Controls.Add(panFlowersDisplayHolderContainer);
			}//end of else
		}//end of try
		catch (Exception ex)
		{
			lblError.Text = ex.Message;// + " " + ex.StackTrace;
            lblError.Visible = true;
		}//end of catch
	}//end of createSearchItems()
	
    private void InitializePagingVars(bool forceRefresh)
    {
        if (!_PagingVarsInitialized || forceRefresh)
        {
			Flower.FlowerShopService flsService = new Flower.FlowerShopService();//holds the flower Service 
			Flower.CartService csService = new Flower.CartService();//holds the flower Shoring Cart Service 
			Flower.GetProductsResponse gprProducts = flsService.getProducts(ConfigurationManager.AppSettings[""], ConfigurationManager.AppSettings[""], strCurrentCate, 1, 1);//holds the all of the products for a category sympathy
			
			//creates a flower shopping cart
			csService.createShoppingCart(ConfigurationManager.AppSettings[""], ConfigurationManager.AppSettings[""], System.Web.HttpContext.Current.Session.SessionID + hfObituatyId.Value);

			//checks if there is any errors
			if (gprProducts.errors.Length > 0)
			{
				//goes around display each of the errors
				foreach (Flower.Error err in gprProducts.errors)
				{
					lblError.Text += "<div>" + err.field + "</div><div>" + err.message + "</div>";
				}//end of foreach
			}//end of if
			else 
			{													
				//displays the results of the number of items this category
				litSearchResultsText.Text = "Your search has produced <span id='lblSearchResultCount'>(" + gprProducts.total  + ")</span> results";
				
				_HiddenPageIndex = Convert.ToInt32(HiddenPageIndex.Value);
				_Count = Convert.ToInt32(gprProducts.total.ToString());
				_LastPageIndex = ((int)Math.Ceiling(((double)_Count / (double)_PageSize))) - 1;
				_PagingVarsInitialized = true;
			}//end of if
        }//end of if
    }//end of InitializePagingVars()
	
	protected void Page_PreRender(object sender, EventArgs e)
    {
		try
		{			
			if (!IsPostBack)
			{
				//checks if allof the items needs to be in the URL in order for this to work otherwise redirect the suer
				//to the homepage as they are chaging the URL
				if (Request.QueryString["oid"] != null && Request.QueryString["FHPID"] != null && Request.QueryString["person"] != null)
				{
					//sets the id and where the flower will be going
					hfFHPID.Value = DAL.safeSql(Request.QueryString["FHPID"].ToString());
					hfObituatyId.Value = DAL.safeSql(Request.QueryString["oid"].ToString());
					hfPersonId.Value = DAL.safeSql(Request.QueryString["person"].ToString());
				}//end of if
				else
					//sends the user to the homepage if there is no ids or where the flower will be going was change
					Response.Redirect("/Home.aspx", true);
				
				//creates a search items
				//BIND PAGE
		        BindDesignsPanel();
			}//end of if
		}//end of try
        catch (Exception ex)
        {
            lblError.Text = ex.Message;// + " " + ex.StackTrace;
            lblError.Visible = true;
        }//end of catch
    }//end of Page_PreRender()
	
	protected void Page_Load(object sender, EventArgs e)
    {
		try
		{
			//sets the size of the page
			_PageSize = (_Cols * _Rows);
			
			//checks if there is a which category to use
			if (Request.QueryString["cate"] != null)
				strCurrentCate = Request.QueryString["cate"];
			
			if (IsPostBack)
			{
				//recreates the search items when the page is postback when the 
				//BIND PAGE
		        BindDesignsPanel();
			}//end of if
			else
			{
				//INITIALIZE SEARCH CRITERIA ON FIRST VISIT
				
				//checks if there is a p to uses it else then give it a zero
				if(!string.IsNullOrEmpty(Request.QueryString["p"]))
					//INITIALIZE SEARCH CRITERIA ON FIRST VISIT
					HiddenPageIndex.Value = Request.QueryString["p"];
				else
					//INITIALIZE SEARCH CRITERIA ON FIRST VISIT
					HiddenPageIndex.Value = "0";				
			}//end of else
		}//end of try
        catch (Exception ex)
        {
            lblError.Text = ex.Message;// + " " + ex.StackTrace;
            lblError.Visible = true;
        }//end of catch
	}//end of Page_Load()
	
	protected void lbBuyNow_Command(object sender, CommandEventArgs e)
	{
		try
		{
			string strFlowerCode = (string)e.CommandArgument;//holds the value of the flower of what the user has selected
			Flower.CartService csService = new Flower.CartService();//holds the flower Shoring Cart Service 
			Flower.FlowerShopService flsService = new Flower.FlowerShopService();//holds the flower Service 
			Flower.GetProductResponse gprProduct = flsService.getProduct(ConfigurationManager.AppSettings[""], ConfigurationManager.AppSettings[""], strFlowerCode);//holds the details for this flower
			
			//checks if there is any errors
			if (gprProduct.errors.Length > 0)
			{
				//goes around display each of the errors
				foreach (Flower.Error err in gprProduct.errors)
				{
					litFlowerError.Text += "<div>" + err.field + "</div><div>" + err.message + "</div>";
				}//end of foreach
			}//end of if
			else
			{
				Flower.OrderItem orProduct = new Flower.OrderItem();//holds the order item enter into the shopping cart
							
				//sets the proties for the OrderItem
				orProduct.code = gprProduct.product.code;
				orProduct.price = gprProduct.product.price;
				
				Flower.AddItemShoppingCartResponse aiscrProduct = csService.addItemShoppingCart(ConfigurationManager.AppSettings[""], ConfigurationManager.AppSettings[""], System.Web.HttpContext.Current.Session.SessionID + hfObituatyId.Value, orProduct);//holes the response of adding the flower to the cart
							
				//checks if there is any errors
				if (aiscrProduct.errors.Length > 0)
				{
					//goes around display each of the errors
					foreach (Flower.Error err in aiscrProduct.errors)
					{
						lblError.Text += "<div>" + err.field + "</div><div>" + err.message + "</div>";
					}//end of foreach
				}//end of if
				else
					//goes to that cart
					Response.Redirect("/Obituaries/flower/ordering.aspx?person=" + hfPersonId.Value + "&FHPID=" + hfFHPID.Value + "&oid=" + hfObituatyId.Value, true);
			}//end of else
		}//end of try
		catch (Exception ex)
		{
			lblError.Text = ex.Message;// + " " + ex.StackTrace;
			lblError.Visible = true;
		}//end of catch
	}//end of lbBuyNow_Command()
	
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

			string baseUrl = "/Obituaries/flower.aspx?person=" + hfPersonId.Value + "&FHPID=" + hfFHPID.Value + "&oid=" + hfObituatyId.Value + "&cate=" + strCurrentCate + "&p=";
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
			
			PagerControls.DataSource = pagerLinkData;
            PagerControls.DataBind();
			
            PagerControls2.DataSource = pagerLinkData;
            PagerControls2.DataBind();
        }//end of if
        else
		{
			PagerPanel.Visible = false;
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
}//end of Module