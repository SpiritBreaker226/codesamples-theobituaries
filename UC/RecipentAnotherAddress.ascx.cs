// sets the recipt to go to another address

public partial class RecipentAnotherAddress : System.Web.UI.UserControl
{
	private bool boolIsFlowers = true;//holds if this is a Flower or a card
	
	#region Properties
		
	public bool IsFlowers
    { 
        get { return boolIsFlowers; } 
        set { boolIsFlowers = value; }
    }//end of IsFlowers
	
	public int setObituatyID
    {
        set { hfObituatyId.Value = Convert.ToString(value);}
    }//end of setObituatyID
	
	public string setBodyClass
    {
        set { panAnotherPersonBody.CssClass = value;}
    }//end of setBodyClass
	
	#endregion
	
	#region Event Funcations

	protected void Page_PreRender(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			//sets the country and province
			ddlRecipientCountry.DataSource = DAL.getRow("", "");
			ddlRecipientCountry.DataBind();
			ddlRecipientProvince.DataSource = DAL.getRow("", "Where  = 1 Order by ");
			ddlRecipientProvince.DataBind();
	
			//checks if this is for flowers = true or card = false
			if(boolIsFlowers == false)
			{
				//disables the phone requiment if it is a card as it is not needed
				rfvRecipientPhoneNo.Visible = false;
				PhoneNoValidator.Visible = false;
				lblPhoneRequiredStar.Text = "";
			}//end of if
	
			txtRecipientFirstName.Attributes.Add("onFocus", "var txtRecipientFirstName = document.getElementById('" + txtRecipientFirstName.ClientID + "'); if ( txtRecipientFirstName.value == 'First Name') {txtRecipientFirstName.value = ''}");
			txtRecipientFirstName.Attributes.Add("onblur", "var txtRecipientFirstName = document.getElementById('" + txtRecipientFirstName.ClientID + "'); if ( txtRecipientFirstName.value == '') {txtRecipientFirstName.value = 'First Name'}");
			txtRecipientLastName.Attributes.Add("onFocus", "var txtRecipientLastName = document.getElementById('" + txtRecipientLastName.ClientID + "'); if ( txtRecipientLastName.value == 'Last Name') {txtRecipientLastName.value = ''}");
			txtRecipientLastName.Attributes.Add("onblur", "var txtRecipientLastName = document.getElementById('" + txtRecipientLastName.ClientID + "'); if ( txtRecipientLastName.value == '') {txtRecipientLastName.value = 'Last Name'}");
			txtRecipientEmail.Attributes.Add("onFocus", "var txtRecipientEmail = document.getElementById('" + txtRecipientEmail.ClientID + "'); if ( txtRecipientEmail.value == 'Email') {txtRecipientEmail.value = ''}");
			txtRecipientEmail.Attributes.Add("onblur", "var txtRecipientEmail = document.getElementById('" + txtRecipientEmail.ClientID + "'); if ( txtRecipientEmail.value == '') {txtRecipientEmail.value = 'Email'}");
			txtRecipientAddress1.Attributes.Add("onFocus", "var txtRecipientAddress1 = document.getElementById('" + txtRecipientAddress1.ClientID + "'); if ( txtRecipientAddress1.value == 'Address 1') {txtRecipientAddress1.value = ''}");
			txtRecipientAddress1.Attributes.Add("onblur", "var txtRecipientAddress1 = document.getElementById('" + txtRecipientAddress1.ClientID + "'); if ( txtRecipientAddress1.value == '') {txtRecipientAddress1.value = 'Address 1'}");
			txtRecipientAddress2.Attributes.Add("onFocus", "var txtRecipientAddress2 = document.getElementById('" + txtRecipientAddress2.ClientID + "'); if ( txtRecipientAddress2.value == 'Address 2') {txtRecipientAddress2.value = ''}");
			txtRecipientAddress2.Attributes.Add("onblur", "var txtRecipientAddress2 = document.getElementById('" + txtRecipientAddress2.ClientID + "'); if ( txtRecipientAddress2.value == '') {txtRecipientAddress2.value = 'Address 2'}");
			txtRecipientCity.Attributes.Add("onFocus", "var txtRecipientCity = document.getElementById('" + txtRecipientCity.ClientID + "'); if ( txtRecipientCity.value == 'City Or Town') {txtRecipientCity.value = ''}");
			txtRecipientCity.Attributes.Add("onblur", "var txtRecipientCity = document.getElementById('" + txtRecipientCity.ClientID + "'); if ( txtRecipientCity.value == '') {txtRecipientCity.value = 'City Or Town'}");
			txtRecipientPostalCode.Attributes.Add("onFocus", "var txtRecipientPostalCode = document.getElementById('" + txtRecipientPostalCode.ClientID + "'); if ( txtRecipientPostalCode.value == 'Postal Code') {txtRecipientPostalCode.value = ''}");
			txtRecipientPostalCode.Attributes.Add("onblur", "var txtRecipientPostalCode = document.getElementById('" + txtRecipientPostalCode.ClientID + "'); if ( txtRecipientPostalCode.value == '') {txtRecipientPostalCode.value = 'Postal Code'}");
			txtRecipientPhoneNo.Attributes.Add("onFocus", "var txtRecipientPhoneNo = document.getElementById('" + txtRecipientPhoneNo.ClientID + "'); if ( txtRecipientPhoneNo.value == 'Phone') {txtRecipientPhoneNo.value = ''}");
			txtRecipientPhoneNo.Attributes.Add("onblur", "var txtRecipientPhoneNo = document.getElementById('" + txtRecipientPhoneNo.ClientID + "'); if ( txtRecipientPhoneNo.value == '') {txtRecipientPhoneNo.value = 'Phone'}");
		}//end of if
    }//end of Page_PreRender()
	
	protected void ddlRecipientCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
		//checks which contry the user is using Canada or use and change the name of the postal to 
		if(ddlRecipientCountry.SelectedValue == "2")
		{
			//sets the display format for US
			txtRecipientPostalCode.Text = "Zip Code";
			rfvRecipientPostalCode.InitialValue = "Zip Code";
			txtRecipientPostalCode.Attributes.Add("onFocus", "var txtRecipientPostalCode = document.getElementById('" + txtRecipientPostalCode.ClientID + "'); if ( txtRecipientPostalCode.value == 'Zip Code') {txtRecipientPostalCode.value = ''}");
			txtRecipientPostalCode.Attributes.Add("onblur", "var txtRecipientPostalCode = document.getElementById('" + txtRecipientPostalCode.ClientID + "'); if ( txtRecipientPostalCode.value == '') {txtRecipientPostalCode.value = 'Zip Code'}");
			lblPCEx.Text = "12345";
            revPC.Enabled = true;
			revPC.ErrorMessage = "Invalid zip code format";
			revPC.ValidationExpression = @"^\d{5}$";
		}//end of if
        else if (ddlRecipientCountry.SelectedValue == "1")
		{
			//sets the display format for Canada/Other
			txtRecipientPostalCode.Text = "Postal Code";
			rfvRecipientPostalCode.InitialValue = "Postal Code";
			txtRecipientPostalCode.Attributes.Add("onFocus", "var txtRecipientPostalCode = document.getElementById('" + txtRecipientPostalCode.ClientID + "'); if ( txtRecipientPostalCode.value == 'Postal Code') {txtRecipientPostalCode.value = ''}");
			txtRecipientPostalCode.Attributes.Add("onblur", "var txtRecipientPostalCode = document.getElementById('" + txtRecipientPostalCode.ClientID + "'); if ( txtRecipientPostalCode.value == '') {txtRecipientPostalCode.value = 'Postal Code'}");
			lblPCEx.Text = "A1A 1A1";
            revPC.Enabled = true;
			revPC.ErrorMessage = "Invalid postal code format";
			revPC.ValidationExpression = @"^[A-Z]\d[A-Z][ ]\d[A-Z]\d$";
		}//end of else
        else
        {
            //sets the display format for Canada/Other
            txtRecipientPostalCode.Text = "Postal Code";
            rfvRecipientPostalCode.InitialValue = "Postal Code";
            txtRecipientPostalCode.Attributes.Add("onFocus", "var txtRecipientPostalCode = document.getElementById('" + txtRecipientPostalCode.ClientID + "'); if ( txtRecipientPostalCode.value == 'Postal Code') {txtRecipientPostalCode.value = ''}");
            txtRecipientPostalCode.Attributes.Add("onblur", "var txtRecipientPostalCode = document.getElementById('" + txtRecipientPostalCode.ClientID + "'); if ( txtRecipientPostalCode.value == '') {txtRecipientPostalCode.value = 'Postal Code'}");
            lblPCEx.Text = "A1A 1A1";
            revPC.Enabled = false;
            revPC.ErrorMessage = "Invalid postal code format";
            revPC.ValidationExpression = string.Empty;
        }

        ddlRecipientProvince.Visible = false;
        if (ddlRecipientCountry.SelectedValue == "1" || ddlRecipientCountry.SelectedValue == "2")
        {
            //changes the Provance to the country selected
            ddlRecipientProvince.Visible = true;
            ddlRecipientProvince.DataSource = DAL.getRow("", "Where  != 64 AND  = " + ddlRecipientCountry.SelectedValue + " Order by ");
            ddlRecipientProvince.DataBind();
        }
    }//end of ddlRecipientCountry_SelectedIndexChanged()
	
	protected void cmdSaveRecipient_Click(object sender, EventArgs e)
    {
		try
		{
			//checks if the page is valid
			if(Page.IsValid)
			{
				int intRecipientID = 0;//holds the id for the new recipient
				
				//checks if this is for flowers or card in order to save to the database and send it back
				//to the page what uses the new recipient
				if(boolIsFlowers == true)
				{
					//save the recipient to the database
					intRecipientID = DAL.SaveObituaryFlowerRecipient(0, Convert.ToInt32(hfObituatyId.Value), txtRecipientFirstName.Text, txtRecipientLastName.Text, txtRecipientEmail.Text, txtRecipientAddress1.Text, txtRecipientAddress2.Text, txtRecipientCity.Text, Convert.ToInt32(ddlRecipientProvince.SelectedValue), txtRecipientPostalCode.Text, txtRecipientPhoneNo.Text, false);//holds the new Recipient
		
					//refreshes the page with the new Recipient
					Response.Redirect("/Obituaries/flower/ordering.aspx?person=1&FHPID=" + intRecipientID + "&oid=" + hfObituatyId.Value);
				}//end of if
				else
				{
					//save the recipient to the database
					intRecipientID = DAL.SaveObituaryCardRecipient(0, Convert.ToInt32(hfObituatyId.Value), txtRecipientFirstName.Text, txtRecipientLastName.Text, txtRecipientEmail.Text, txtRecipientAddress1.Text, txtRecipientAddress2.Text, txtRecipientCity.Text, Convert.ToInt32(ddlRecipientProvince.SelectedValue), txtRecipientPostalCode.Text, txtRecipientPhoneNo.Text, false);

					//refreshes the page with the new Recipient
					Response.Redirect("/Obituaries/sympathycards.aspx?ObituariesID=" + hfObituatyId.Value + "&CardReceiverId=" + intRecipientID);
				}//end of else
			}//end of if
		}//end of try
        catch (Exception ex)
        {
            litError.Text = ex.Message;
            litError.Visible = true;
        }//end of catch
    }//end of cmdSaveRecipient_Click()
	
	#endregion
}//end of User Contorl