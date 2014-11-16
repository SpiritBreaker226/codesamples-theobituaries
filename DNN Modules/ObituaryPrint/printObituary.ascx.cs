// Print Out Obityary

public partial class printObituary : PortalModuleBase
{	
	protected void Page_PreRender(object sender, EventArgs e)
    {
		try
		{
			//checks if there is a obituary id to use
			if (!string.IsNullOrEmpty(Request.QueryString["id"]))
	        {
				string strObitID = DAL.safeSql(Request.QueryString["id"]);//holds the id of the obituary
				DataTable dtObituaryDetails = DAL.getRow("", "WHERE  = " + strObitID);//holds the Obituary details
				
				//checks if there is any details for this obituary
				if (dtObituaryDetails != null && dtObituaryDetails.Rows.Count > 0)
				{
					int intIndexServiceID = 0;//holds the unquie id of the row
					string strLastFHID = "";//holds what is the last FHID
					DataTable dtObitImage = DAL.getRow("", "WHERE  = " + strObitID + " Order by ");//gets all image for this obituary
										
					//sets the basis settings
					lblName.Text = dtObituaryDetails.Rows[0][""].ToString() + ", "  + dtObituaryDetails.Rows[0][""].ToString() + " " + dtObituaryDetails.Rows[0][""].ToString();
																				
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
						
					//checks if there is any sevices
					if(dtObitImage.Rows.Count > 0)
					{
						//checks if the file in file system if so then display it
						if (File.Exists(Server.MapPath("~\\images\\User\\" + strObitID + "\\" + dtObitImage.Rows[0][""].ToString())))
						{
							//sets this obituary image
							imgObituary.AlternateText = lblName.Text;
							imgObituary.ImageUrl = "/images/User/" + strObitID + "/" + dtObitImage.Rows[0][""].ToString();
							//adds Image for display
							imgObituary.Visible = true;					
						}//end of if
					}//end of if
																
					//checks if there is obituary text is being use
					if (!string.IsNullOrEmpty(dtObituaryDetails.Rows[0][""].ToString()))
						//gets the first half the obituary text
						litObituaryDetails.Text = Server.HtmlDecode(dtObituaryDetails.Rows[0][""].ToString());
				}//end of if
			}//end of if
			else
				throw new Exception("Unable to find this Obituary");
		}//end of try
        catch (Exception ex)
        {
            lblMainError.Text = ex.Message;
            panMainError.Visible = true;
        }//end of catch
    }//end of Page_PreRender()
}//end of Module