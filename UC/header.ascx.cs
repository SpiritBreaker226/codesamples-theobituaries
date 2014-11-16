// header for the whole site

partial class header : System.Web.UI.UserControl
{
	protected void Page_PreRender(object sender, System.EventArgs e)
	{
		//checks if the user is already logged in and
		if (Session["MemberLogin"] != null)
		{
			//change the header for members mode
			hlAccount.Text = "My Account";
			hlAccount.NavigateUrl = "/MyAccount.aspx";
			hlLog.Text = "Log Out";
			hlLog.NavigateUrl = "/MyAccount/Logout.aspx";
			
			//turns an all of the links thae user can access
			panCreObUser.Visible = true;
			panCreObNonUser.Visible = false;
			panCreMemUser.Visible = true;
			panCreMemNonUser.Visible = false;
			panRegUser.Visible = true;
			panRegNonUser.Visible = false;
			panPreplanUser.Visible = true;
			panPreplanNonUser.Visible = false;
			panNotifUser.Visible = true;
			panNotifNonUser.Visible = false;
			panPlanUser.Visible = true;
			panPlanNonUser.Visible = false;
			panGiveUser.Visible = true;
			panGiveNonUser.Visible = false;
			panSupportUser.Visible = true;
			panSupportNonUser.Visible = false;
		}//end of if
		
		lnkOverview.Attributes.Add("onmouseover", "this.firstChild.src='/Portals/_default/Skins/Obit/Images/ob-icon-over2-on.png'");
        lnkOverview.Attributes.Add("onmouseout", "this.firstChild.src='/Portals/_default/Skins/Obit/images/ob-icon-over2-off.png'");

		lnkProdDetails.Attributes.Add("onmouseover", "this.firstChild.src='/Portals/_default/Skins/Obit/Images/ob-icon-over-on.png'");
        lnkProdDetails.Attributes.Add("onmouseout", "this.firstChild.src='/Portals/_default/Skins/Obit/images/ob-icon-over-off.png'");
		
		lnkCompare.Attributes.Add("onmouseover", "this.firstChild.src='/Portals/_default/Skins/Obit/Images/ob-icon-comp-on.png'");
        lnkCompare.Attributes.Add("onmouseout", "this.firstChild.src='/Portals/_default/Skins/Obit/images/ob-icon-comp-off.png'");
		
		lnkPricing.Attributes.Add("onmouseover", "this.firstChild.src='/Portals/_default/Skins/Obit/Images/ob-icon-pricing-on.png'");
        lnkPricing.Attributes.Add("onmouseout", "this.firstChild.src='/Portals/_default/Skins/Obit/images/ob-icon-pricing-off.png'");
		
		lnkFH.Attributes.Add("onmouseover", "this.firstChild.src='/Portals/_default/Skins/Obit/Images/ob-icon-fh-on.png'");
        lnkFH.Attributes.Add("onmouseout", "this.firstChild.src='/Portals/_default/Skins/Obit/images/ob-icon-fh-off.png'");
				
		lnkContact.Attributes.Add("onmouseover", "this.firstChild.src='/Portals/_default/Skins/Obit/Images/ob-icon-contact-on.png'");
        lnkContact.Attributes.Add("onmouseout", "this.firstChild.src='/Portals/_default/Skins/Obit/images/ob-icon-contact-off.png'");
		
		lnkFaq.Attributes.Add("onmouseover", "this.firstChild.src='/Portals/_default/Skins/Obit/Images/ob-icon-faq-on.png'");
        lnkFaq.Attributes.Add("onmouseout", "this.firstChild.src='/Portals/_default/Skins/Obit/images/ob-icon-faq-off.png'");
		
		lnkAbout.Attributes.Add("onmouseover", "this.firstChild.src='/Portals/_default/Skins/Obit/Images/ob-icon-about-on.png'");
        lnkAbout.Attributes.Add("onmouseout", "this.firstChild.src='/Portals/_default/Skins/Obit/images/ob-icon-about-off.png'");

        string currentPageId = TabController.CurrentPage.TabID.ToString();
        
        if (currentPageId != "123" && currentPageId != "132")
        {
            HtmlMeta hmFB = new HtmlMeta();//holds the meta takes that will go into the header
            HtmlHead head = (HtmlHead)Page.Header;//holds the reference of the Header

            //define an HTML meta fb:admins in the header 
            hmFB.Name = "fb:admins";
            hmFB.Content = "";
            head.Controls.Add(hmFB);
            hmFB = new HtmlMeta();

            //define an HTML meta fb:app_id in the header 
            hmFB.Name = "fb:app_id";
            hmFB.Content = "";
            head.Controls.Add(hmFB);
            hmFB = new HtmlMeta();

            //define an HTML meta og:type in the header 
            hmFB.Name = "og:type";
            hmFB.Content = "website";
            head.Controls.Add(hmFB);
            hmFB = new HtmlMeta();

            //define an HTML meta og:description in the header 
            hmFB.Name = "og:description";
            hmFB.Content = this.Page.Title;
            head.Controls.Add(hmFB);
            hmFB = new HtmlMeta();

            //define an HTML meta og:title in the header 
            hmFB.Name = "og:title";
            hmFB.Content = this.Page.Title;
            head.Controls.Add(hmFB);
            hmFB = new HtmlMeta();

            //define an HTML meta og:site_name in the header 
            hmFB.Name = "og:site_name";
            hmFB.Content = "theobituaries.ca";
            head.Controls.Add(hmFB);
            hmFB = new HtmlMeta();

            //define an HTML meta og:image in the header 
            hmFB.Name = "og:image";
            hmFB.Content = "http://" + Request.Url.Host + "/Portals/_default/Skins/Obit/Images/ob-header-logo.png";
            head.Controls.Add(hmFB);
            hmFB = new HtmlMeta();

            //define an HTML meta og:url in the header 
            hmFB.Name = "og:url";
            hmFB.Content = Request.Url.AbsolutePath;
            head.Controls.Add(hmFB);
            hmFB = new HtmlMeta();

            //define an HTML meta twitter:card in the header 
            hmFB.Name = "twitter:card";
            hmFB.Content = "summary";
            head.Controls.Add(hmFB);
            hmFB = new HtmlMeta();

            //define an HTML meta twitter:site in the header 
            hmFB.Name = "twitter:site";
            hmFB.Content = "@theobituariesca";
            head.Controls.Add(hmFB);
            hmFB = new HtmlMeta();

            //define an HTML meta twitter:title in the header 
            hmFB.Name = "twitter:title";
            hmFB.Content = this.Page.Title;
            head.Controls.Add(hmFB);
            hmFB = new HtmlMeta();

            //define an HTML meta twitter:description in the header 
            hmFB.Name = "twitter:description";
            hmFB.Content = this.Page.Title;
            head.Controls.Add(hmFB);
            hmFB = new HtmlMeta();
            
            //define an HTML meta twitter:domain in the header 
            hmFB.Name = "twitter:domain";
            hmFB.Content = this.Page.Title;
            head.Controls.Add(hmFB);
        }
	}//end of Page_PreRender()
}//end of Page