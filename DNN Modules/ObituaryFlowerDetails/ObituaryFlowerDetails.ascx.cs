// Flowers Details For Obituary

public partial class ObituaryFlowerDetails : PortalModuleBase
{
	protected void Page_PreRender(object sender, EventArgs e)
    {
		try
		{
			string strCurrentCate = "sy";//holds the current category that is being search for
			
			//checks if allof the items needs to be in the URL in order for this to work otherwise redirect the suer
			//to the homepage as they are chaging the URL
			if (Request.QueryString["oid"] != null && Request.QueryString["FHPID"] != null && Request.QueryString["flowerID"] != null && Request.QueryString["person"] != null)
            {
				//sets the id and where the flower will be going
				hfFlowerId.Value = DAL.safeSql(Request.QueryString["flowerID"].ToString());
				hfFHPID.Value = DAL.safeSql(Request.QueryString["FHPID"].ToString());
				hfObituatyId.Value = DAL.safeSql(Request.QueryString["oid"].ToString());
				hfPersonId.Value = DAL.safeSql(Request.QueryString["person"].ToString());
            }//end of if
			else
				//sends the user to the homepage if there is no ids or where the flower will be going was change
				Response.Redirect("/Home.aspx", true);
				
			//sets the URL fro teh back button
			hlBack.NavigateUrl = "/Obituaries/flower.aspx?person=" + hfPersonId.Value + "&FHPID=" + hfFHPID.Value + "&oid=" + hfObituatyId.Value;
															
            Flower.FlowerShopService flsService = new Flower.FlowerShopService();//holds the flower Service 
            Flower.GetProductResponse gprProduct = flsService.getProduct(ConfigurationManager.AppSettings[""], ConfigurationManager.AppSettings[""], hfFlowerId.Value);//holds the details for this flower

			//checks if there is any errors
			if (gprProduct.errors.Length > 0)
			{
				//goes around display each of the errors
                foreach (Flower.Error err in gprProduct.errors)
				{
					lblError.Text += "<div>" + err.field + "</div><div>" + err.message + "</div>";
				}//end of foreach
			}//end of if
			else
			{
				//sets the basic details
				imgMainFlower.ImageUrl = gprProduct.product.image;
				imgMainFlower.AlternateText = gprProduct.product.name;
				lblFlowerName.Text = gprProduct.product.name;
				lblFlowerItem.Text += gprProduct.product.code;
				lblFlowerPrice.Text = "$" + string.Format("{0:F2}", Convert.ToString(gprProduct.product.price)) + " USD";
				litFlowerDetails.Text = gprProduct.product.description;
			}//end of else
		}//end of try
        catch (Exception ex)
        {
            lblError.Text = ex.Message;// + " " + ex.StackTrace;
            lblError.Visible = true;
        }//end of catch
    }//end of Page_PreRender()
	
	protected void lbAddCart_Click(object sender, EventArgs e)
    {
		Flower.CartService csService = new Flower.CartService();//holds the flower Shoring Cart Service 
		Flower.FlowerShopService flsService = new Flower.FlowerShopService();//holds the flower Service 
		Flower.GetProductResponse gprProduct = flsService.getProduct(ConfigurationManager.AppSettings[""], ConfigurationManager.AppSettings[""], hfFlowerId.Value);//holds the details for this flower
		
		//checks if there is any errors
		if (gprProduct.errors.Length > 0)
		{
			//goes around display each of the errors
			foreach (Flower.Error err in gprProduct.errors)
			{
				lblError.Text += "<div>" + err.field + "</div><div>" + err.message + "</div>";
			}//end of foreach
		}//end of if
		else
		{
			Flower.OrderItem orProduct = new Flower.OrderItem();//holds the order item will enter into the shopping cart
			
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
	}//end of lbAddCart_Click()
}//end of Module