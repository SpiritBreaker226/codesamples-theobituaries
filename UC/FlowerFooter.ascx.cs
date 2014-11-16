// Footer For the flowers sections

public partial class FlowerFooter : System.Web.UI.UserControl
{	
	protected void Page_PreRender(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{			
			//checks if allof the items needs to be in the URL in order for this to work otherwise redirect the suer
			//to the homepage as they are chaging the URL
			if (Request.QueryString["oid"] != null)
			{
				DataTable dtObitDetails = DAL.getRow("", "Where  = " + DAL.safeSql(Request.QueryString["oid"]));//holds the details of this obit
							
				//checks if there is a obituary dournd
				if (dtObitDetails != null && dtObitDetails.Rows.Count > 0)
					//sets the name of the person of the obituary in the button to make it more personal
					cmdBackToMain.Text += dtObitDetails.Rows[0][""].ToString() + " " + dtObitDetails.Rows[0][""].ToString() + "'s " + (General.ObituaryType)Enum.Parse(typeof(General.ObituaryType), dtObitDetails.Rows[0][""].ToString());
				else
					//sends the user to the homepage if there is no Obituaty found
					Response.Redirect("/Home.aspx", true);
			}//end of if
			else
				//sends the user to the homepage if there is no ids or where the flower will be going was change
				Response.Redirect("/Home.aspx", true);
		}//end of if
    }//end of Page_PreRender()
	
	protected void cmdBackToMain_Click(object sender, EventArgs e)
	{
		//goes back to the Obituaries page
		Response.Redirect("/Obituaries.aspx?ObituariesID=" + Request.QueryString["oid"]);		
	}//end of cmdBackToMain_Click()
}//end of User Contorl