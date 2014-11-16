// Display The Ooituery Details

public partial class ObitueryDetails : PortalModuleBase
{
	protected void Page_PreRender(object sender, EventArgs e)
    {
		//checks if there is a id of the Obituery if not then 
		//send the user to the home page
		if (!string.IsNullOrEmpty(Request.QueryString["ObituariesID"]))
		{
			DataTable dtObituaryDetails = DAL.getRow("", "WHERE  = " + Convert.ToInt32(Request.QueryString["ObituariesID"]));//holds the Obituary details

			//set it to the details page
			ObituaryPreview.ObituaryID = Convert.ToInt32(Request.QueryString["ObituariesID"]);
		}//end of if
		else
			//sends the user to the homepage if there is no id
			Response.Redirect("/Home.aspx", true);
    }//end of Page_PreRender()
}//end of Page