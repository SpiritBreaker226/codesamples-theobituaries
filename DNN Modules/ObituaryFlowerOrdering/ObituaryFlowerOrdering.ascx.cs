// Order Flowers For Obituary

public partial class ObituaryFlowerOrdering : PortalModuleBase
{
	//load the current shopping cart
	private bool loadCart()
	{
		try
		{
			Flower.CartService csService = new Flower.CartService();//holds the flower Shoring Cart Service 
			Flower.GetShoppingCartResponse gpscrProducts = csService.getShoppingCart(ConfigurationManager.AppSettings[""], ConfigurationManager.AppSettings[""], System.Web.HttpContext.Current.Session.SessionID + hfObituatyId.Value);//holds the shopping cart that belongs to this user
						
			//checks if there is any errors
			if (gpscrProducts.errors.Length > 0)
			{
				//goes around display each of the errors
				foreach (Flower.Error err in gpscrProducts.errors)
				{
					lblError.Text += "<div>" + err.field + "</div><div>" + err.message + "</div>";
				}//end of foreach
				
				//turns on the error
				lblError.Visible = true;
				
				return false;
			}//end of if
			else
			{
				//checks if there is any items in gpscrProducts 
				if(gpscrProducts.items.Length == 0)
				{
					//displays a no cart message if there is no items in gpscrProducts
					panBody.Visible = false;
					panNoCart.Visible = true;
					
					return false;
				}//end of if
				else
				{
					int intFlowerIndexID = 1;//holds unqiue number of items id
					Flower.FlowerShopService flsService = new Flower.FlowerShopService();//holds the flower Service 
			
					//resets the shopping cart information items in order not to the current information in the payment area
					litFlowerShoppingCartInforItems.Text = "";
					litFlowerShoppingCartInforItemsSeller.Text = "";
					litFlowerShoppingCartInforItemsPrice.Text = "";
					
					//goes around displaying the items that are in this shopping carc
					foreach (Flower.OrderItem liDetail in gpscrProducts.items)
					{
						Flower.GetProductResponse gprProduct = flsService.getProduct(ConfigurationManager.AppSettings[""], ConfigurationManager.AppSettings[""], liDetail.code);//holds the details for this flower
						
						//checks if there is any errors
						if (gprProduct.errors.Length > 0)
						{
							//goes around display each of the errors
							foreach (Flower.Error err in gprProduct.errors)
							{
								lblError.Text += "<div>" + err.field + "</div><div>" + err.message + "</div>";
							}//end of foreach
							
							//turns on the error
							lblError.Visible = true;
							
							return false;
						}//end of if
						else
						{
							Panel panFlowersShoppingCartContainer = new Panel();//holds flower shopping cart Container
							Panel panFlowersShoppingCartLeft = new Panel();//holds the flower shopping cart left
							Panel panFlowersShoppingCartMiddle = new Panel();//holds the flower shopping cart middle
							Panel panFlowersShoppingCartRight = new Panel();//holds the flower shopping cart right
							Panel panFlowersShoppingCartFooter = new Panel();//holds the flower shopping cart footer
							Panel panFlowersShoppingCartNameImageContainer = new Panel();//holds flower shopping cart Container for the Name & Image in panFlowersShoppingCartLeft
							Panel panFlowersShoppingCartNameImageLeft = new Panel();//holds the flower shopping cart left for the Name & Image in panFlowersShoppingCartLeft
							Panel panFlowersShoppingCartNameImageMiddle = new Panel();//holds the flower shopping cart middle for the Name & Image in panFlowersShoppingCartLeft
							Panel panFlowersShoppingCartNameImageRight = new Panel();//holds the flower shopping cart right for the Name & Image in panFlowersShoppingCartLeft
							Panel panFlowersShoppingCartNameImageFooter = new Panel();//holds the flower shopping cart footer for the Name & Image in panFlowersShoppingCartLeft 
							Panel panFlowersShoppingCartRemove = new Panel();//holds the flower shopping cart remove item
							Label lblFlowerPrice = new Label();//holds the flower price
							LinkButton lbRemove = new LinkButton();//holds the Buy Now 
							HyperLink hlFlowerImage = new HyperLink();//holds the Image/Link of the Flower
							HyperLink hlDetails = new HyperLink();//holds the Detail link for the button 
												
							//adds the classes to the Panels
							panFlowersShoppingCartContainer.CssClass = "customContainer divObiturayFlowersOrderingShoppingCartContainer";
							panFlowersShoppingCartLeft.CssClass = "customLeft divObiturayFlowersOrderingShoppingCartLeft";
							panFlowersShoppingCartMiddle.CssClass = "customMiddle divObiturayFlowersOrderingShoppingCartMiddle";
							panFlowersShoppingCartRight.CssClass = "customRight divObiturayFlowersOrderingShoppingCartRight";
							panFlowersShoppingCartFooter.CssClass = "customFooter divObiturayFlowersOrderingShoppingCartFooter";//divObiturayFlowersOrderingShoppingCartItemFooter
							panFlowersShoppingCartNameImageContainer.CssClass = "customContainer divObiturayFlowersOrderingShoppingCartNameImageContainer";
							panFlowersShoppingCartNameImageLeft.CssClass = "customLeft divObiturayFlowersOrderingShoppingCartNameImageLeft";
							panFlowersShoppingCartNameImageRight.CssClass = "customRight divObiturayFlowersOrderingShoppingCartNameImageRight";
							panFlowersShoppingCartNameImageFooter.CssClass = "customFooter divObiturayFlowersOrderingShoppingCartNameImageFooter";
							panFlowersShoppingCartRemove.CssClass = "divFlowersShoppingCartRemove";
							
							//adds the left, middle, right and footer Flowers Display Button
							panFlowersShoppingCartContainer.Controls.Add(panFlowersShoppingCartLeft);
							panFlowersShoppingCartContainer.Controls.Add(panFlowersShoppingCartMiddle);
							panFlowersShoppingCartContainer.Controls.Add(panFlowersShoppingCartRight);
							panFlowersShoppingCartContainer.Controls.Add(panFlowersShoppingCartFooter);
							
							//adds the left, right and footer for the Name & Image in panFlowersShoppingCartLeft
							panFlowersShoppingCartNameImageContainer.Controls.Add(panFlowersShoppingCartNameImageLeft);
							panFlowersShoppingCartNameImageContainer.Controls.Add(panFlowersShoppingCartNameImageRight);
							panFlowersShoppingCartNameImageContainer.Controls.Add(panFlowersShoppingCartNameImageFooter);
							
							//sets the url and image url to the details page
							hlFlowerImage.NavigateUrl = "/Obituaries/flower/Details.aspx?person=" + hfPersonId.Value + "&FHPID=" + hfFHPID.Value + "&oid=" + hfObituatyId.Value + "&flowerID=" + Server.UrlEncode(liDetail.code);
							hlFlowerImage.ImageUrl = gprProduct.product.thumbnail;
							
							//because this is a recipt of sorts this should display the seller per line 
							//even throw it will always be 
							litFlowerShoppingCartInforItemsSeller.Text += "<label>theObituaries.ca</label><br/>";
							
							//sets the url and image url to the details page and litFlowerShoppingCartInforItems in the Payment area
							litFlowerShoppingCartInforItems.Text += "<label>" + gprProduct.product.name + "</label><br />";
							hlDetails.NavigateUrl = "/Obituaries/flower/Details.aspx?person=" + hfPersonId.Value + "&FHPID=" + hfFHPID.Value + "&oid=" + hfObituatyId.Value + "&flowerID=" + Server.UrlEncode(liDetail.code);
							hlDetails.Text = gprProduct.product.name;
							
							//adds the name of the flower to Flowers shopping cart middle 
							panFlowersShoppingCartNameImageLeft.Controls.Add(hlFlowerImage);
							panFlowersShoppingCartNameImageRight.Controls.Add(hlDetails);
							
							//adds panFlowersShoppingCartNameImageContainer to panFlowersShoppingCartLeft
							panFlowersShoppingCartLeft.Controls.Add(panFlowersShoppingCartNameImageContainer);
														
							//sets the proerties for lblFlowerPrice and litFlowerShoppingCartInforItemsPrice in the Payment area for the user to see what they are being before paying for it
							litFlowerShoppingCartInforItemsPrice.Text += "<label>$" + string.Format("{0:F2}", gprProduct.product.price.ToString()) + " USD</label><br />";
							lblFlowerPrice.Text = "$" + string.Format("{0:F2}", gprProduct.product.price.ToString()) + " USD";
							lblFlowerPrice.CssClass = "divFlowerPrice";
							
							//adds the flower price to Flowers shopping cart Right
							panFlowersShoppingCartMiddle.Controls.Add(lblFlowerPrice);
																					
							//adds text, CommandArgument and OnCommand to the LinkButton green-button
							lbRemove.Text = "Remove";
							lbRemove.CssClass = "aFlowersShoppingCartRemove";
							lbRemove.ID = "lbRemove" + intFlowerIndexID;
							lbRemove.OnClientClick += "javascript: return confirm('Do you what to remove " + gprProduct.product.name + "?');";
							lbRemove.Command += lbRemoveItem_Command;
							lbRemove.CommandArgument = gprProduct.product.code;
							
							//adds the linkbutton to panFlowersShoppingCartRemove and then to Flowers shopping cart Right as panFlowersShoppingCartRemove will be able to move in panFlowersShoppingCartMiddle without affect the other divs
							panFlowersShoppingCartRemove.Controls.Add(lbRemove);
							panFlowersShoppingCartRight.Controls.Add(panFlowersShoppingCartRemove);
							
							//displays the flower display holder to the screen
							phCartContent.Controls.Add(panFlowersShoppingCartContainer);
							
							intFlowerIndexID++;
						}//end of else
					}//end of foreach
				}//end of else
			}//end of else
		}//end of try
        catch (Exception ex)
        {
            lblError.Text = ex.Message;// + " " + ex.StackTrace;
            lblError.Visible = true;
        }//end of catch
						
		return true;
	}//end of loadCart()
	
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
					
				//loads in the current shopping cart of the user
				if(loadCart() == false)
					return;	
									
				string strRecipientPostalCode = "";//holds the Recipient Postal code
				
				//checks if the user wants to send the flowers a person or FH
				if(hfPersonId.Value == "0")
				{
					DataTable dtFHDetails = DAL.getRow("", "Where  = " + hfFHPID.Value);//holds the details of the FH
					
					//checks if there is any data found
					if (dtFHDetails != null && dtFHDetails.Rows.Count > 0)
						//sets the PC for this FH
						strRecipientPostalCode = dtFHDetails.Rows[0][""].ToString();
					else
						//sends the user to the homepage if there is no Obituaty found
						Response.Redirect("/Home.aspx", true);
				}//end of if
				else if(hfPersonId.Value == "1")
				{
					DataTable dtPersonDetails = DAL.getRow("", "Where = " + hfFHPID.Value);//holds the details of the FH
					
					//checks if there is any data found
					if (dtPersonDetails != null && dtPersonDetails.Rows.Count > 0)
						//sets the PC for this FH
						strRecipientPostalCode = dtPersonDetails.Rows[0][""].ToString();
					else
						//sends the user to the homepage if there is no Obituaty found
						Response.Redirect("/Home.aspx", true);
				}//end of else if
				else
				{
					//disables the checkout button and sub total as the user has not selected a Recipent
					cmdFlowerCheckout.Visible = false;
					panSubTotal.Visible = false;
					return;
				}//end of else
																		
				Flower.FlowerShopService flsService = new Flower.FlowerShopService();//holds the flower Service 
				Flower.CartService csService = new Flower.CartService();//holds the flower Shoring Cart Service 
				Flower.GetShoppingCartResponse gpscrProducts = csService.getShoppingCart(ConfigurationManager.AppSettings[""], ConfigurationManager.AppSettings[""], System.Web.HttpContext.Current.Session.SessionID + hfObituatyId.Value);//holds the shopping cart that belongs to this user
				
				//checks if there is any errors
				if (gpscrProducts.errors.Length > 0)
				{
					//goes around display each of the errors
					foreach (Flower.Error err in gpscrProducts.errors)
					{
						lblError.Text += "<div>" + err.field + "</div><div>" + err.message + "</div>";
					}//end of foreach
	
					//turns on the error
					lblError.Visible = true;
				}//end of if
				else
				{
					//sets the country and province
					ddlBillingCountry.DataSource = DAL.getRow("", "");
					ddlBillingCountry.DataBind();
					ddlBillingProvince.DataSource = DAL.getRow("", "Where = 1 Order by ");
					ddlBillingProvince.DataBind();
					
					Flower.PlaceOrderResponse porCart = flsService.getTotal(ConfigurationManager.AppSettings[""], ConfigurationManager.AppSettings[""],strRecipientPostalCode,gpscrProducts.items,0d,0d);//holds the taxes and the total of the shopping cart
					
					//checks if there is any errors
					if (porCart.errors.Length > 0)
					{
						//goes around display each of the errors
						foreach (Flower.Error err in porCart.errors)
						{
							lblError.Text += "<div>" + err.field + "</div><div>" + err.message + "</div>";
						}//end of foreach
		
						//turns on the error
						lblError.Visible = true;
					}//end of if
					else
					{						
						Flower.GetDeliveryDatesResponse gddValidDates = flsService.getDeliveryDates(ConfigurationManager.AppSettings[""], ConfigurationManager.AppSettings[""],strRecipientPostalCode);//holds the delivery dates allowed
						//checks if there is any errors
						if (gddValidDates.errors.Length > 0)
						{
							//goes around display each of the errors
							foreach (Flower.Error err in gddValidDates.errors)
							{
								lblError.Text += "<div>" + err.field + "</div><div>" + err.message + "</div>";
							}//end of foreach
			
							//turns on the error
							lblError.Visible = true;
						}//end of if
						else
						{
							//goes around adding the allowed delivery dates to the delivery date dropdown
							for(int intDateIndex = 0;intDateIndex < gddValidDates.dates.Length; intDateIndex++)
							{
								Flower.CheckDeliveryDateResponse cddValidDate = flsService.checkDeliveryDate(ConfigurationManager.AppSettings[""], ConfigurationManager.AppSettings[""], strRecipientPostalCode, Convert.ToDateTime(gddValidDates.dates[intDateIndex]));//holds the if the delivery date is valid
								
								//checks if there is any errors
								if (cddValidDate.errors.Length > 0)
								{
									//goes around display each of the errors
									foreach (Flower.Error err in cddValidDate.errors)
									{
										lblError.Text += "<div>" + err.field + "</div><div>" + err.message + "</div>";
									}//end of foreach
			
									//turns on the error
									lblError.Visible = true;
								}//end of if
								//checks if the date is valid
								else if(cddValidDate.valid == true)
									//adds the Day to ddlDeliveryDate base on todays date
									//in order to always be up to date with the and in the future Days 
									ddlDeliveryDate.Items.Add(new ListItem(Convert.ToDateTime(gddValidDates.dates[intDateIndex]).ToString("dddd MMMM dd, yyyy"), Convert.ToDateTime(gddValidDates.dates[intDateIndex]).ToString()));
							}//end of foreach .ToString("MM/dd/yyyy")

							//displays the sub total
							lblCartSubTotal.Text = "$" + string.Format("{0:F2}", porCart.subTotal) + " USD";
						}//end of else
					}//end of else
				}//end of else
				
				//goes around for the number of years base on todays year and adds it to ddlAsOfYear
				for(int intYearIndex = 0; intYearIndex <= 11; intYearIndex++)
				{
					//adds the year to ddlExpiryYear base on todays date
					//in order to always be up to date with the and in the future years
					ddlExpiryYear.Items.Add(new ListItem(DateTime.Now.AddYears(intYearIndex).ToString("yyyy"),DateTime.Now.AddYears(intYearIndex).ToString("yy")));
				}//end of for loop
						
				//sets the first entry for the year and Delivery Date
				ddlExpiryYear.Items.Insert(0, new ListItem("YYYY", "-1"));
				ddlDeliveryDate.Items.Insert(0, new ListItem("Select a Delivery Date", "-1"));
			}//end of if
			
			//adds a javascript for RegularExpressionValidator to check if the 
			//txtbox that is connect to the field needs to be hightlight for the error
			txtBillingFirstName.Attributes.Add("onblur", "validateExpressionCheck('" + revBillingFirstName.ClientID + "', '" + txtBillingFirstName.ClientID + "');");
			txtBillingLastName.Attributes.Add("onblur", "validateExpressionCheck('" + revBillingLastName.ClientID + "', '" + txtBillingLastName.ClientID + "');");
			txtBillingEmail.Attributes.Add("onblur", "validateExpressionCheck('" + revBillingEmail.ClientID + "', '" + txtBillingEmail.ClientID + "');");
			txtBillingPhoneNo.Attributes.Add("onblur", "validateExpressionCheck('" + revBillingPhone.ClientID + "', '" + txtBillingPhoneNo.ClientID + "');");
			txtBillingPostalCode.Attributes.Add("onblur", "validateExpressionCheck('" + revPC.ClientID + "', '" + txtBillingPostalCode.ClientID + "');");
			txtBillingNameOnCard.Attributes.Add("onblur", "validateExpressionCheck('" + revBillingNameOnCard.ClientID + "', '" + txtBillingNameOnCard.ClientID + "');");
			txtBillingCreditCardNumber.Attributes.Add("onblur", "validateExpressionCheck('" + revBillingCreditCardNumber.ClientID + "', '" + txtBillingCreditCardNumber.ClientID + "');");
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
			if (IsPostBack)
			{
				//loads in the current shopping cart of the user whent the user
				loadCart();
			}//end of if
		}//end of try
        catch (Exception ex)
        {
            lblError.Text = ex.Message;// + " " + ex.StackTrace;
            lblError.Visible = true;
        }//end of catch
    }//end of Page_Load()
	
	protected void lbRemoveItem_Command(object sender, CommandEventArgs e)
	{
		try
		{
			Flower.CartService csService = new Flower.CartService();//holds the flower Shoring Cart Service 
			Flower.FlowerShopService flsService = new Flower.FlowerShopService();//holds the flower Service 
			Flower.GetProductResponse gprProduct = flsService.getProduct(ConfigurationManager.AppSettings[""], ConfigurationManager.AppSettings[""], e.CommandArgument.ToString());//holds the details for this flower
			//checks if there is any errors
			if (gprProduct.errors.Length > 0)
			{
				//goes around display each of the errors
				foreach (Flower.Error err in gprProduct.errors)
				{
					lblError.Text += "<div>" + err.field + "</div><div>" + err.message + "</div>";
				}//end of foreach
			
				//turns on the error
				lblError.Visible = true;
			}//end of if
			else
			{
				Flower.OrderItem orProduct = new Flower.OrderItem();//holds the order item will enter into shopping cart
				
				//sets the proties for the OrderItem
				orProduct.code = gprProduct.product.code;
				orProduct.price = gprProduct.product.price;
				
				Flower.RemoveItemShoppingCartResponse riscrProduct = csService.removeItemShoppingCart(ConfigurationManager.AppSettings[""], ConfigurationManager.AppSettings[""], System.Web.HttpContext.Current.Session.SessionID + hfObituatyId.Value, orProduct);//holes the response of removes the flower to the cart
				
				//checks if there is any errors
				if (riscrProduct.errors.Length > 0)
				{
					//goes around display each of the errors
					foreach (Flower.Error err in riscrProduct.errors)
					{
						lblError.Text += "<div>" + err.field + "</div><div>" + err.message + "</div>";
					}//end of foreach
			
					//turns on the error
					lblError.Visible = true;
				}//end of if
				else
					//reloads the page to display the new cart
					Response.Redirect("/Obituaries/flower/ordering.aspx?person=" + hfPersonId.Value + "&FHPID=" + hfFHPID.Value + "&oid=" + hfObituatyId.Value, true);
			}//end of else
		}//end of try
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            lblError.Visible = true;
        }//end of catch
	}//end of lbRemoveItem_Command()
	
	protected void cmdFlowerCheckout_Click(object sender, EventArgs e)
    {
		//displays the dheckout details and remove the shopping cart
		panShoppingCart.Visible = false;
		panCheckOut.Visible = true;
	}//end of cmdFlowerCheckout_Click()
	
	protected void cmdFlowerBackCheckout_Click(object sender, EventArgs e)
    {
		//displays the shopping cart and remove the checkout details
		panShoppingCart.Visible = true;
		panCheckOut.Visible = false;
	}//end of cmdFlowerBackCheckout_Click()
	
	protected void cmdGoToCredit_Click(object sender, EventArgs e)
    {		
		try
		{
			//resets the error message
			litFlowerError.Text = "";
			
			//checks if the page is valid if so then prcess the event
			if (Page.IsValid)
			{
				//checks if the special delivery text is lesser 100
				if(txtSpecialDelivery.Text.Length < 100)
				{
					//checks if the card message text is lesser 200
					if(txtCardMessage.Text.Length < 200)
					{
						Flower.CartService csService = new Flower.CartService();//holds the flower Shoring Cart Service 
						Flower.GetShoppingCartResponse gpscrProducts = csService.getShoppingCart(ConfigurationManager.AppSettings[""], ConfigurationManager.AppSettings[""], System.Web.HttpContext.Current.Session.SessionID + hfObituatyId.Value);//holds the shopping cart that belongs to this user
								
						//checks if there is any errors
						if (gpscrProducts.errors.Length > 0)
						{
							//goes around display each of the errors
							foreach (Flower.Error err in gpscrProducts.errors)
							{
								litFlowerError.Text += "<div>" + err.field + "</div><div>" + err.message + "</div>";
							}//end of foreach
				
							//turns on the error
							litFlowerError.Visible = true;
						}//end of if
						else
						{
							string strRecipientPostalCode = "";//holds the Recipient Postal code
							string strTaxTotal = General.calculateTax(Convert.ToInt32(ddlBillingCountry.SelectedValue), Convert.ToInt32(ddlBillingProvince.SelectedValue), lblCartSubTotal.Text.Replace("$","").Replace(" USD",""), true);//holds the tax total
							
							//checks if the user wants to send the flowers a person or FH
							if(hfPersonId.Value == "0")
							{
								DataTable dtFHDetails = DAL.getRow("", "Where  = " + hfFHPID.Value);//holds the details of the FH
								
								//checks if there is any data found
								if (dtFHDetails != null && dtFHDetails.Rows.Count > 0)
									//sets the PC for this FH
									strRecipientPostalCode = dtFHDetails.Rows[0][""].ToString();
								else
									//sends the user to the homepage if there is no Obituaty found
									Response.Redirect("/Home.aspx", true);
							}//end of if
							else
							{
								DataTable dtPersonDetails = DAL.getRow("", "Where = " + hfFHPID.Value);//holds the details of the FH
								
								//checks if there is any data found
								if (dtPersonDetails != null && dtPersonDetails.Rows.Count > 0)
									//sets the PC for this FH
									strRecipientPostalCode = dtPersonDetails.Rows[0][""].ToString();
								else
									//sends the user to the homepage if there is no Obituaty found
									Response.Redirect("/Home.aspx", true);
							}//end of else
							
							Flower.FlowerShopService flsService = new Flower.FlowerShopService();//holds the flower Service
							Flower.PlaceOrderResponse porCart = flsService.getTotal(ConfigurationManager.AppSettings[""], ConfigurationManager.AppSettings[""],strRecipientPostalCode.Replace(" ",""),gpscrProducts.items,0d,Convert.ToDouble(strTaxTotal));//holds the taxes and the total of the shopping cart
							
							//checks if there is any errors
							if (porCart.errors.Length > 0)
							{
								//goes around display each of the errors
								foreach (Flower.Error err in porCart.errors)
								{
									litFlowerError.Text += "<div>" + err.field + "</div><div>" + err.message + "</div>";
								}//end of foreach
											
								//turns on the error
								litFlowerError.Visible = true;
							}//end of if
							else
							{
								//displays the totals, taxes and the service charges
								lblTaxes.Text = "$" + string.Format("{0:F2}", porCart.affiliateTax) + " USD";
								lblServiceCharage.Text = "$" + string.Format("{0:F2}", porCart.floristOneServiceCharge) + " USD";
								lblSubTotal.Text = "$" + string.Format("{0:F2}", porCart.subTotal) + " USD";
								lblTotal.Text = "$" + string.Format("{0:F2}", porCart.orderTotal) + " USD";
								
								//displays what was enter into the delivery / address
								litPlaceOrderAddress.Text = "<label>" + txtBillingFirstName.Text.Trim() + " " + txtBillingLastName.Text.Trim() + "<br/>" + txtBillingAddress1.Text + "<br/>";
								
								//checks if there is a Billing Address 2 to display as well
								if(!string.IsNullOrEmpty(txtBillingAddress2.Text))
									litPlaceOrderAddress.Text += txtBillingAddress2.Text.Trim() +"<br/>";
								
								//displays the rest of what was enter into the delivery / address
								litPlaceOrderAddress.Text += txtBillingCity.Text + ", " + 
									DAL.GetStateNameById(ddlBillingProvince.SelectedValue) + "<br/>" +
									txtBillingPostalCode.Text.Trim() + ", " +  
									DAL.getCountryShortNameById(Convert.ToInt32(ddlBillingCountry.SelectedValue), true) + "<br/>" + 
									
									txtBillingPhoneNo.Text.Trim() + "</label>";
									
								//displays the Delivery Date
								lblPlaceOrderDeliveryDate.Text = Convert.ToDateTime(ddlDeliveryDate.SelectedValue).ToString("dddd MMMM dd, yyyy");
								
								//displays the Card Message
								litPlaceOrderCardMessage.Text = "<label>" + txtCardMessage.Text.Trim() + "</label>";
				
								//checks if there is any special delivery
								if(!string.IsNullOrEmpty(txtSpecialDelivery.Text))
									//displays the special delivery
									litPlaceOrderSpecialDelivery.Text = "<label>" + txtSpecialDelivery.Text + "</label>";
								else
									//does not display the special delivery
									panPlaceOrderSpecialDelivery.Visible = false;
		
								//displays the payment area and remove the checkout details
								panPlaceOrder.Visible = true;
								panCheckOut.Visible = false;
							}//end of else
						}//end of else
					}//end of if
					else
						throw new Exception("Card Message can only be a maximum of 200 characters");
				}//end of if
				else
					throw new Exception("Special Delivery can only be a maximum of 100 characters");
			}//end of if
		}//end of try
        catch (Exception ex)
        {
            litFlowerError.Text = ex.Message;// + " " + ex.StackTrace;
            litFlowerError.Visible = true;
        }//end of catch
	}//end of cmdGoToCredit_Click()
	
	protected void cmdGoToFlowerCheckout_Click(object sender, EventArgs e)
    {
		//displays the checkout details and remove the payment area
		panPlaceOrder.Visible = false;
		panCheckOut.Visible = true;
	}//end of cmdGoToFlowerCheckout_Click()
	
	protected void cmdEmptyCart_Click(object sender, EventArgs e)
    {
		Flower.CartService csService = new Flower.CartService();//holds the flower Shoring Cart Service 
		Flower.EmptyShoppingCartResponse escrCart = csService.emptyShoppingCart(ConfigurationManager.AppSettings[""], ConfigurationManager.AppSettings[""], System.Web.HttpContext.Current.Session.SessionID + hfObituatyId.Value);//holds the resposne to if the cart was empty
		
		//checks if there is any errors
		if (escrCart.errors.Length > 0)
		{
			//goes around display each of the errors
			foreach (Flower.Error err in escrCart.errors)
			{
				lblError.Text += "<div>" + err.field + "</div><div>" + err.message + "</div>";
			}//end of foreach
			
			//turns on the error
			lblError.Visible = true;
		}//end of if
		else
			//reloads the page to display the new cart
			Response.Redirect("/Obituaries/flower/ordering.aspx?person=" + hfPersonId.Value + "&FHPID=" + hfFHPID.Value + "&oid=" + hfObituatyId.Value, true);
	}//end of cmdEmptyCart_Click()
	
	protected void cmdBack_Click(object sender, EventArgs e)
    {
		//goes back to the category page
		Response.Redirect("/Obituaries/flower.aspx?person=" + hfPersonId.Value + "&FHPID=" + hfFHPID.Value + "&oid=" + hfObituatyId.Value);
	}//end of cmdBack_Click()
		
	protected void cmdPayment_Click(object sender, EventArgs e)
    {
		try
		{
			//turns off the error messages
			lblError.Visible = false;
			litPaymentError.Text = "";
			
			//checks if the page is valid if so then prcess the event
			if (Page.IsValid)
			{
				//checks if the user agree Terms of Use
				if(chkAgreeTerms.Checked == true)
				{
					Flower.Address addRecipient = new Flower.Address();//holds the recipient address for the placeholder
					Flower.Address addCustomer = new Flower.Address();//holds the customer address for the placeholder
					Flower.CCInfo ccCustomer = new Flower.CCInfo();//holds the credit card of the customer
					
					//sets the proerties for the credit card of the customer
					ccCustomer.ccExpMonth = Convert.ToDouble(ddlExpiryMonth.SelectedValue);
					ccCustomer.ccExpYear = Convert.ToDouble(ddlExpiryYear.SelectedValue);
					ccCustomer.ccNum = txtBillingCreditCardNumber.Text;
					ccCustomer.ccSecCode = Convert.ToDouble(txtBillingSecurityCode.Text);
					ccCustomer.ccType = ddlBillingCreditCardType.SelectedValue;
					
					//sets the proerties for the customer
					addCustomer.name = txtBillingFirstName.Text + " " + txtBillingLastName.Text;
					addCustomer.institution = "";
					addCustomer.address1 = txtBillingAddress1.Text;
					addCustomer.address2 = txtBillingAddress2.Text;
					addCustomer.city = txtBillingCity.Text;
					addCustomer.state = DAL.GetStateNameById(ddlBillingProvince.SelectedValue);
					addCustomer.country = DAL.getCountryShortNameById(Convert.ToInt32(ddlBillingCountry.SelectedValue));
					addCustomer.zip = txtBillingPostalCode.Text;
					addCustomer.phone = txtBillingPhoneNo.Text.Replace("(","").Replace("-","").Replace(" ","");					
					addCustomer.email = txtBillingEmail.Text;
										
					//checks if the user wants to send the flowers a person or FH
					if(hfPersonId.Value == "0")
					{
						DataTable dtFHDetails = DAL.getRow("", "Where  = " + DAL.safeSql(hfFHPID.Value));//holds the details of the FH
						DataTable dtObitDetails = DAL.getRow("", "Where = " + DAL.safeSql(hfObituatyId.Value));//holds the details of this obit
						
						//checks if there is any data found
						if (dtFHDetails.Rows.Count > 0 && dtObitDetails.Rows.Count > 0)
						{
							//sets the Recipient for this order
							addRecipient.name = dtObitDetails.Rows[0][""].ToString() + " " + dtObitDetails.Rows[0][""].ToString();
							addRecipient.institution = dtFHDetails.Rows[0][""].ToString();
							addRecipient.address1 = dtFHDetails.Rows[0][""].ToString();
							addRecipient.address2 = dtFHDetails.Rows[0][""].ToString();
							addRecipient.city = dtFHDetails.Rows[0][""].ToString();
							addRecipient.state = dtFHDetails.Rows[0][""].ToString();
							addRecipient.country = dtFHDetails.Rows[0][""].ToString();
							addRecipient.zip = dtFHDetails.Rows[0][""].ToString();
							addRecipient.phone = dtFHDetails.Rows[0][""].ToString().Replace("(","").Replace("-","").Replace(" ","");
						}//end of if
						else
							//sends the user to the homepage if there is no Obituaty found
							Response.Redirect("/Home.aspx", true);
					}//end of if
					else
					{
						DataTable dtPersonDetails = DAL.getRow("", "Where = " + DAL.safeSql(hfFHPID.Value));//holds the details of the FH
						
						//checks if there is any data found
						if (dtPersonDetails != null && dtPersonDetails.Rows.Count > 0)
						{
							//sets the Recipient for this order
							addRecipient.name = dtPersonDetails.Rows[0][""].ToString() + " " + dtPersonDetails.Rows[0][""].ToString();
							addRecipient.institution = "";
							addRecipient.address1 = dtPersonDetails.Rows[0][""].ToString();
							addRecipient.address2 = dtPersonDetails.Rows[0][""].ToString();
							addRecipient.city = dtPersonDetails.Rows[0][""].ToString();
							addRecipient.state = dtPersonDetails.Rows[0][""].ToString();
							addRecipient.country = dtPersonDetails.Rows[0][""].ToString();
							addRecipient.zip = dtPersonDetails.Rows[0][""].ToString();
							addRecipient.phone = dtPersonDetails.Rows[0][""].ToString().Replace("(","").Replace("-","").Replace(" ","");
							addRecipient.email = "";
						}//end of if
						else
							//sends the user to the homepage if there is no Obituaty found
							Response.Redirect("/Home.aspx", true);
					}//end of else
										
					Flower.CartService csService = new Flower.CartService();//holds the flower Shoring Cart Service 
					Flower.FlowerShopService flsService = new Flower.FlowerShopService();//holds the flower Service
					Flower.GetShoppingCartResponse gpscrProducts = csService.getShoppingCart(ConfigurationManager.AppSettings[""], ConfigurationManager.AppSettings[""], System.Web.HttpContext.Current.Session.SessionID + hfObituatyId.Value);//holds the shopping cart that belongs to this user
					
					//checks if there is any errors
					if (gpscrProducts.errors.Length > 0)
					{
						//goes around display each of the errors
						foreach (Flower.Error err in gpscrProducts.errors)
						{
							litPaymentError.Text += "<div>" + err.field + "</div><div>" + err.message + "</div>";
						}//end of foreach
			
						//turns on the error
						litPaymentError.Visible = true;
					}//end of if
					else
					{
						Flower.PlaceOrderResponse porCart = flsService.getTotal(ConfigurationManager.AppSettings[""], ConfigurationManager.AppSettings[""], addRecipient.zip, gpscrProducts.items, 0d, Convert.ToDouble(lblTaxes.Text.Replace("$","").Replace(" USD","")));//holds the taxes and the total of the shopping cart
		
						//checks if there is any errors
						if (porCart.errors.Length > 0)
						{
							//goes around display each of the errors
							foreach (Flower.Error err in porCart.errors)
							{
								litPaymentError.Text += "<div>" + err.field + "</div><div>" + err.message + "</div>";
							}//end of foreach
				
							//turns on the error
							litPaymentError.Visible = true;
						}//end of if
						else
						{
							//place the order to florst one
							porCart = flsService.placeOrder(ConfigurationManager.AppSettings[""], ConfigurationManager.AppSettings[""], addRecipient, addCustomer, HttpContext.Current.Request.UserHostAddress, ccCustomer, gpscrProducts.items, txtCardMessage.Text, txtSpecialDelivery.Text, Convert.ToDateTime(ddlDeliveryDate.SelectedValue), 0d, Convert.ToDouble(lblTaxes.Text.Replace("$","").Replace(" USD","")), Convert.ToDouble(porCart.orderTotal), 0);
						
							//checks if there is any errors
							if (porCart.errors.Length > 0)
							{
								//goes around display each of the errors
								foreach (Flower.Error err in porCart.errors)
								{
									litPaymentError.Text += "<div>" + err.field + "</div><div>" + err.message + "</div>";
								}//end of foreach
					
								//turns on the error
								litPaymentError.Visible = true;
							}//end of if
							else
							{
								//checks if the user is logged in
								if(Session[""] != null)
									//attaches the order to this account
                                    DAL.addUpdateObituaryFlowerOrder(0, Convert.ToInt32(hfObituatyId.Value), Convert.ToInt32(Session[""].ToString()), Convert.ToInt32(porCart.orderNumber), "", lblSubTotal.Text.Replace("$", "").Replace(" USD", ""), lblTaxes.Text.Replace("$", "").Replace(" USD", ""), lblServiceCharage.Text.Replace("$", "").Replace(" USD", ""), lblTotal.Text.Replace("$", "").Replace(" USD", ""), txtBillingFirstName.Text, txtBillingLastName.Text, txtBillingCity.Text, Convert.ToInt16(ddlBillingProvince.SelectedValue), Convert.ToInt16(ddlBillingCountry.SelectedValue), txtBillingPhoneNo.Text, txtBillingAddress1.Text, txtBillingPostalCode.Text, litFlowerShoppingCartInforItems.Text);
								else
									//adds the order for this email adddress
                                    DAL.addUpdateObituaryFlowerOrder(0, Convert.ToInt32(hfObituatyId.Value), 0, Convert.ToInt32(porCart.orderNumber), txtBillingEmail.Text, lblSubTotal.Text.Replace("$", "").Replace(" USD", ""), lblTaxes.Text.Replace("$", "").Replace(" USD", ""), lblServiceCharage.Text.Replace("$", "").Replace(" USD", ""), lblTotal.Text.Replace("$", "").Replace(" USD", ""), txtBillingFirstName.Text, txtBillingLastName.Text, txtBillingCity.Text, Convert.ToInt16(ddlBillingProvince.SelectedValue), Convert.ToInt16(ddlBillingCountry.SelectedValue), txtBillingPhoneNo.Text, txtBillingAddress1.Text, txtBillingPostalCode.Text, litFlowerShoppingCartInforItems.Text);
									
								Flower.EmptyShoppingCartResponse escrCart = csService.emptyShoppingCart(ConfigurationManager.AppSettings[""], ConfigurationManager.AppSettings[""], System.Web.HttpContext.Current.Session.SessionID + hfObituatyId.Value);//holds the resposne to if the cart was empty
			
								//checks if there is any errors
								if (escrCart.errors.Length > 0)
								{
									//goes around display each of the errors
									foreach (Flower.Error err in escrCart.errors)
									{
										litPaymentError.Text += "<div>" + err.field + "</div><div>" + err.message + "</div>";
									}//end of foreach
						
									//turns on the error
									litPaymentError.Visible = true;
								}//end of if
								else
								{
									//sets the order number
									lblOrderNumber.Text = Convert.ToString(porCart.orderNumber);
									
									DataTable dtObituary = DAL.queryDbTable("SELECT * FROM  WHERE  = " + hfObituatyId.Value);//holds the details of the obituary
		
									//checks if there is any obituary found
									if (dtObituary != null)
									{
										string strImagePath = DAL.queryDbScalar("SELECT  WHERE  = '" + hfObituatyId.Value + "' ORDER BY ");//holds the path of the main image for this obituary
										string strNote = Server.HtmlDecode(dtObituary.Rows[0][""].ToString());//holds any notes for the user
										string strCartContent = "";//holds the contents of the cart to be sent to the user
										string strSpecialDelivery = "";//holds the Special Delivery item
										string strDelivery = "";//holds the Delivery Address
										string birthAndPassingDate = string.Empty;//holds the birth and death date				
										string firstName = string.Empty;
										string lastName = string.Empty;
										
										if (strNote.Length > 180)
											strNote = strNote.Substring(0, 180);
							
										//checks if there is a FirstName
										if (!string.IsNullOrEmpty(dtObituary.Rows[0][""].ToString()))
											firstName = dtObituary.Rows[0][""].ToString();
							
										//checks if there is a LastName
										if (!string.IsNullOrEmpty(dtObituary.Rows[0][""].ToString()))
											lastName = dtObituary.Rows[0][""].ToString();
							
										//checks if there is a birth date
										if (!string.IsNullOrEmpty(dtObituary.Rows[0][""].ToString()))
											birthAndPassingDate = string.Format("{0:MMMM dd, yyyy}", Convert.ToDateTime(dtObituary.Rows[0][""].ToString()));
							
										//checks if there is a death date and if there should be a -
										if (!string.IsNullOrEmpty(dtObituary.Rows[0][""].ToString()))
											birthAndPassingDate += (!string.IsNullOrEmpty(birthAndPassingDate) ? " - " + string.Format("{0:MMMM dd, yyyy}", Convert.ToDateTime(dtObituary.Rows[0][""].ToString())) : string.Format("{0:MMMM dd, yyyy}", Convert.ToDateTime(dtObituary.Rows[0][""].ToString())));
							
										//checks if there is a there is a image if not then use the default image
										if (!string.IsNullOrEmpty(strImagePath))
											strImagePath = "http://" + Request.Url.Host + "/images/User/" + hfObituatyId.Value + "/" + strImagePath;
										else
											strImagePath = "http://" + Request.Url.Host + "/EmailTemplate/images/ob-header-logo.png";
											
										//goes around add to strCartContent for the items that are in this shopping cart
										foreach (Flower.OrderItem liDetail in gpscrProducts.items)
										{
											Flower.GetProductResponse gprProduct = flsService.getProduct(ConfigurationManager.AppSettings[""], ConfigurationManager.AppSettings[""], liDetail.code);//holds the details for this flower
											
											//checks if there is any errors
											if (gprProduct.errors.Length > 0)
											{
												//goes around display each of the errors
												foreach (Flower.Error err in gprProduct.errors)
												{
													litPaymentError.Text += "<div>" + err.field + "</div><div>" + err.message + "</div>";
												}//end of foreach
												
												//turns on the error
												litPaymentError.Visible = true;
											}//end of if
											else
												strCartContent += "<td colspan='2'><label style='font-family:Helvetica, sans-serif; font-size:18px; color:#4d4d4d;'>" + gprProduct.product.name + "</label></td><td colspan='2'><label style='font-family:Helvetica, sans-serif; font-size:18px; color:#4d4d4d;'>$" + string.Format("{0:F2}", gprProduct.product.price.ToString()) + " USD</label></td>";
										}//end of foreach
										
										//checks if there is a any Special Deliverys
										if(!string.IsNullOrEmpty(txtSpecialDelivery.Text))
											//displays the special delivery
											strSpecialDelivery = "<tr>" + 
												"<td width='130' valign='top'>" + 
													"<label style='font-family:Helvetica, sans-serif; font-size:18px; color:#4d4d4d;'>Special Delivery:</label>" + 
												"</td>" + 
												"<td width='155' valign='top' colspan='3'>" + 
													"<label style='font-family:Helvetica, sans-serif; font-size:18px; color:#4d4d4d;'>" + litPlaceOrderSpecialDelivery.Text + "</label>" + 
												"</td>" + 
											"</tr>";
																					
										//checks if the user wants to send the flowers a person or FH
										if(Request.QueryString["person"] == "0")
										{
											DataTable dtFHDetails = DAL.getRow("", "Where  = " + DAL.safeSql(Request.QueryString["FHPID"]));//holds the details of the FH
											
											//checks if there is any data found
											if (dtFHDetails != null && dtFHDetails.Rows.Count > 0)
											{
												//displays who will get the order in the header
												strDelivery = dtFHDetails.Rows[0][""].ToString() + "<br/>" + 
													dtFHDetails.Rows[0][""].ToString() + "<br/>";
												
												//checks if there is a FH Address 2 to display as well
												if(!string.IsNullOrEmpty(dtFHDetails.Rows[0][""].ToString()))
													strDelivery += dtFHDetails.Rows[0][""].ToString() +"<br/>";
												
												//displays the rest of who will get the order
												strDelivery += dtFHDetails.Rows[0][""].ToString() + ", " + 
													dtFHDetails.Rows[0][""].ToString() + "<br/>" + 
													dtFHDetails.Rows[0][""].ToString() + "<br/>" + 
													dtFHDetails.Rows[0][""].ToString() + "<br/>" + 
													dtFHDetails.Rows[0][""].ToString();
											}//end of if
											else
												//sends the user to the homepage if there is no FH found
												Response.Redirect("/Home.aspx", true);
										}//end of if
										else
										{
											DataTable dtPersonDetails = DAL.getRow("", "Where = " + DAL.safeSql(Request.QueryString["FHPID"]));//holds the details of the FH
											
											//checks if there is any data found
											if (dtPersonDetails != null && dtPersonDetails.Rows.Count > 0)
											{
												//displays who will get the order in the header
												strDelivery = dtPersonDetails.Rows[0][""].ToString() + " " + dtPersonDetails.Rows[0][""].ToString() + "<br/>" + 
												dtPersonDetails.Rows[0][""].ToString() + ", " + 
												dtPersonDetails.Rows[0][""].ToString();
											}//end of if
											else
												//sends the user to the homepage if there is no Person found
												Response.Redirect("/Home.aspx", true);
										}//end of else
							
										//sends out the email
										General.sendHTMLMail(txtBillingEmail.Text, "Confirmation of Your Order from theObituaries.ca", string.Format(File.ReadAllText(Server.MapPath("~/EmailTemplate/FlowerPurchaseThankYou.html")), (General.ObituaryType)Enum.Parse(typeof(General.ObituaryType), dtObituary.Rows[0][""].ToString()), firstName, lastName, birthAndPassingDate, strNote, hfObituatyId.Value, strImagePath, "flowers", lblOrderNumber.Text, lblPlaceOrderDeliveryDate.Text, strSpecialDelivery, litPlaceOrderCardMessage.Text, litPlaceOrderAddress.Text, lblSubTotal.Text, lblServiceCharage.Text, lblTaxes.Text, lblTotal.Text, strCartContent, DateTime.Now.ToString("MMMM dd, yyyy"), ddlBillingCreditCardType.SelectedValue, strDelivery), "", "");
									}//end of if
																	
									//displays the thank you message and remove the payment section and the ablity to edit
									panCreditCard.Visible = false;
									panPlaceOrderEdit.Visible = false;
									panThankYou.Visible = true;
								}//end of else
							}//end of else
						}//end of else
					}//end of else
				}//end of if
				else
					throw new Exception("You must agree to the terms of user before you can buy flowers");
			}//end of if
		}//end of try
        catch (Exception ex)
        {
            litPaymentError.Text = ex.Message;// + " " + ex.StackTrace;
            litPaymentError.Visible = true;
        }//end of catch
	}//end of cmdPayment_Click()
		
	protected void ddlBillingCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
		//checks which contry the user is using Canada or use and change the name of the postal to 
		if(ddlBillingCountry.SelectedValue == "2")
		{
			//sets the display format for US
			txtBillingPostalCode.Attributes.Add("placeholder", "Zip Code");
			lblPCEx.Text = "12345";
            revPC.Enabled = true;
			revPC.ErrorMessage = "Invalid zip code format";
			revPC.ValidationExpression = @"^\d{5}$";
		}//end of if
        else if (ddlBillingCountry.SelectedValue == "1")
		{
			//sets the display format for Canada/Other
			txtBillingPostalCode.Attributes.Add("placeholder", "Postal Code");
			lblPCEx.Text = "A1A 1A1";
            revPC.Enabled = true;
			revPC.ErrorMessage = "Invalid postal code format";
			revPC.ValidationExpression = @"^[A-Z]\d[A-Z][ ]\d[A-Z]\d$";
		}//end of else
        else
        {
            //sets the display format for Canada/Other
            txtBillingPostalCode.Attributes.Add("placeholder", "Postal Code");
            lblPCEx.Text = "A1A 1A1";
            revPC.Enabled = false;
            revPC.ErrorMessage = "Invalid postal code format";
            revPC.ValidationExpression = string.Empty;
        }
		
        ddlBillingProvince.Visible = false;
        if (ddlBillingCountry.SelectedValue == "1" || ddlBillingCountry.SelectedValue == "2")
        {
            //changes the Provance to the country selected
            ddlBillingProvince.Visible = true;
            ddlBillingProvince.DataSource = DAL.getRow("", "Where  != 64 AND  = " + ddlBillingCountry.SelectedValue + " Order by ");
            ddlBillingProvince.DataBind();
        }
    }//end of ddlBillingCountry_SelectedIndexChanged()
	
	protected void ddlDeliveryDate_SelectedIndexChanged(object sender, EventArgs e)
    {
		DateTime dtSlectedDevliverDate = Convert.ToDateTime(ddlDeliveryDate.SelectedValue);//holds the selectedDevliverDate
		TimeSpan tsFromNowUntilDeliver = dtSlectedDevliverDate.Subtract(DateTime.Today);//holds the different between the deliver date and today
		
		//checks if the different is more then seven days 
		if(tsFromNowUntilDeliver.Days > 7)
			//display a message that the delivery maybe slow
			lblDeliveryError.Visible = true;
		else
			//removes the message
			lblDeliveryError.Visible = false;
    }//end of ddlDeliveryDate_SelectedIndexChanged()
	
	protected void ddlBillingCreditCardType_SelectedIndexChanged(object sender, EventArgs e)
    {
		//checks check Credit Card the user is using as American Express as 
		//it uses 4 security codes while others uses 3 security codes
		if(ddlBillingCreditCardType.SelectedValue == "AX")
			txtBillingSecurityCode.MaxLength = 4;
		else
			txtBillingSecurityCode.MaxLength = 3;
    }//end of ddlBillingCreditCardType_SelectedIndexChanged()
}//end of Module