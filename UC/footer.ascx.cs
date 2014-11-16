// footer for the whole site

partial class footer : System.Web.UI.UserControl
{
    protected void Page_PreRender(object sender, System.EventArgs e)
	{
        if (!IsPostBack)
        {
			//check is the language is other then english
			// if so then changes this file images and text to fit that language
            if (Thread.CurrentThread.CurrentCulture.Name == "fr-FR")
            {
                
            }//end of if
        }//end of if
	}//end of Page_PreRender()
}//end of Page