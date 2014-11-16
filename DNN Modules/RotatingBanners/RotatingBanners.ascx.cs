// Dispalys the banners on the frount page

public partial class RotatingBanners : PortalModuleBase
{		
	protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtBanners = DAL.getData(" Where ( = 1) AND CONVERT(VARCHAR(26), , 23) <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' OR ( = 1) AND CONVERT(VARCHAR(26), , 23) <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND CONVERT(VARCHAR(26), , 23) <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' Order by , ");//holds the number of banners that will be display
            int intBannerIndex = 0;//contorls the number of banners that will be displayed

            //checks if there is any banner to display
            if (dtBanners != null)
            {
                litBannerContent.Text += "<script type='text/javascript'>\n" +
                      "$(document).ready(function(){\n" +
                          "featuredcontentglider.init({\n" +
                          "gliderid: 'divBanner',\n" +
                          "contentclass: 'glidecontent',\n" +
                          "togglerid: 'p-select',\n" +
                          "remotecontent: '',\n" +
                          "selected: 0,\n" +
                          "persiststate: false,\n" +
                          "speed: 500,\n" +
                          "direction: 'rightleft',\n" +
                          "autorotate: true,\n" +
                          "autorotateconfig: [7000, 9999]\n" +
                      "})\n" +
                  "});" +
                "</script>" +
                "<div id='divBanner' class='glidecontentwrapper'>";

                //goes around for each banner that can be displayed
                foreach (DataRow drBanner in dtBanners.Rows)
                {
                    string strPostTitle = drBanner[""].ToString();//holds the post title
                    string strPostURL = drBanner[""].ToString();//holds the url of the posst
                    int intPostID = Convert.ToInt32(drBanner[""].ToString());//holds the post id

                    //check if the post has a Post Thumbnail assigned to it.
                    //if so then displays it
                    if (!string.IsNullOrEmpty(drBanner[""].ToString()) || !string.IsNullOrEmpty(drBanner[""].ToString()))
                    {
                        //checks if there will be a image
                        if (string.IsNullOrEmpty(drBanner[""].ToString()) || !string.IsNullOrEmpty(drBanner[""].ToString()) && Convert.ToDateTime(drBanner[""].ToString()) >= DateTime.Now)
                        {
                            litBannerContent.Text += "<div class='glidecontent'>" +
                                "<a class='lblBannerDescTitle' href='" + strPostURL + "'>" +
                                    "<img src='/Portals/_default/Skins/Obit/Images/Banners/" + drBanner[""].ToString() + "' alt='" + strPostTitle + "' />" +
                                "</a>" +
                            "</div>";

                            intBannerIndex++;
                        }//end of if
                    }//end of if
                }//end of foreach loop

                //checks if there is anybanners display if not then dispaly the placeholder
                if (intBannerIndex == 0)
                    //displays the at least one banner in order for the page to not error out
                    litBannerContent.Text += "<div class='glidecontent'>" +
                        "<a href='/Default.aspx' shape='rect'><img src='/Portals/_default/Skins/Obit/Images/ob-banner1.jpg' alt='Default Image' /></a>" +
                    "</div>";

                //adds the finally part and closes the glidecontentwrapper
                litBannerContent.Text += "<div id='p-select' class='glidecontenttoggler divBannerDescFooter'></div>" +
                        "<div class='customFooter'></div>" +
                    "</div>" +
                "</div>";
            }//end of if
        }
    }//end of Page_PreRender()
}//end of Page